#define DEEPDEBUG
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using EZAPI.Framework;
using EZAPI.Framework.Base;
using EZAPI.Execution;
using EZAPI.Data;
using EZAPI.Toolbox.Debug;

namespace MonkeyLightning.Framework
{
    public enum TradeCycleUpdateType { TRADE_ENTERED, TRADE_EXITED, TRADE_STOPPED }

    public class Trade
    {
        public event EventHandler StateChanged;
        public event NotificationEventHandler NotificationArrived;
        public event EventHandler ThrottleCountdownTimer;
        public event EventHandler AutoRestartSignal;
        
        public bool Active
        {
            get { return tradeActive; }
            set
            {
                tradeActive = value;
                if (tradeActive == false) SetAllTradeStepStates(TradeStepState.INACTIVE);
            }
        }
        public string Name { get; set; }
        public int VirtualQuantity { get; set; }
        public double QuantityMultiplier { get; set; }
        public int RealQuantity { get { return (int) (VirtualQuantity * QuantityMultiplier); } }
        public zBuySell BuySell { get; set; }
        public EZInstrument TradeInstrument { get { return tradeInstrument; } }
        public TradeState State { get { return currentTradeState; } }
        public bool AutoRestart { get; set; }
        public double Throttle { get; set; }
        public int ThrottleCountdown { get { return throttleCountdown; } }
        public Dictionary<TradeStepType, TradeStep> Steps { get { return tradeSteps; } }
        public TradeMetrics Metrics { get; set; }

        private IExecutionEngine execution;

        private TradeState currentTradeState;
        private Dictionary<TradeStepType,TradeStep> tradeSteps;
        private bool tradeActive;
        private EZInstrument tradeInstrument;
        private Timer throttleTimer;
        private DateTime throttleStartTime;
        private int throttleCountdown;
        private TradeCycleDetail tradeCycle;
        private bool inTradeValueChangedHandler = false;

        public Trade(string tradeName, EZInstrument tradeInstrument)
        {
            this.Name = tradeName;
            this.tradeInstrument = tradeInstrument;
            VirtualQuantity = 1;
            QuantityMultiplier = 1;
            BuySell = zBuySell.Buy;  // TODO: Not sure if we want this to be initialized
            this.currentTradeState = TradeState.NOT_STARTED;
            AutoRestart = false;
            Throttle = 5;
            this.tradeActive = true;
            this.Metrics = null;

            throttleTimer = new Timer();
            throttleTimer.Elapsed += throttleTimer_Elapsed;

            execution = new SimpleExecutionEngine();
            execution.PartialFill += execution_PartialFill;
            execution.CompleteFill += execution_CompleteFill;

            // Create the TradeSteps that make up this trade.
            tradeSteps = new Dictionary<TradeStepType, TradeStep>();
            tradeSteps[TradeStepType.PRECONDITIONS] = new TradeStep(TradeStepType.PRECONDITIONS, BooleanRuleCombination.AND);
            tradeSteps[TradeStepType.ENTRY] = new TradeStep(TradeStepType.ENTRY);
            tradeSteps[TradeStepType.EXIT] = new TradeStep(TradeStepType.EXIT);
            tradeSteps[TradeStepType.STOP] = new TradeStep(TradeStepType.STOP);
        }

        void execution_CompleteFill(EZFill fill)
        {
            if (Metrics != null)
            {
                ExecutionStatus status = execution.GetExecutionStatus(this.Name);
                Spy.Print("METRICS (complete fill): {0} {1}", status.AverageBuyPrice, status.AverageSellPrice);
                // If the Fill side matches the Trade side then it is an ENTRY (otherwise it is an EXIT).
                if (fill.BuySell == this.BuySell)
                {
                    tradeCycle.SetEntryPriceAverage(status.AverageBuyPrice);
                    if (currentTradeState == TradeState.ENTERING)
                        ChangeState(TradeState.ENTERED);
                }
                else
                {
                    tradeCycle.SetExitPriceAverage(status.AverageSellPrice);
                    if (currentTradeState == TradeState.EXITING)
                        ChangeState(TradeState.EXITED);
                    else if (currentTradeState == TradeState.STOPPING)
                        ChangeState(TradeState.STOPPED);

                }
            }
        }

        void execution_PartialFill(EZFill fill)
        {
            if (Metrics != null)
            {
                ExecutionStatus status = execution.GetExecutionStatus(this.Name);
                Spy.Print("METRICS (partial fill): {0} {1}", status.AverageBuyPrice, status.AverageSellPrice);
                if (fill.BuySell == this.BuySell)
                {
                    tradeCycle.SetEntryPriceAverage(status.AverageBuyPrice);
                }
                else
                {
                    tradeCycle.SetExitPriceAverage(status.AverageSellPrice);
                }
            }
        }

        public void Start()
        {
            // Request notification if the value of ANY TradeStep changes.
            tradeSteps[TradeStepType.PRECONDITIONS].ValueChanged += Trade_ValueChanged;
            tradeSteps[TradeStepType.ENTRY].ValueChanged += Trade_ValueChanged;
            tradeSteps[TradeStepType.EXIT].ValueChanged += Trade_ValueChanged;
            tradeSteps[TradeStepType.STOP].ValueChanged += Trade_ValueChanged;

            ChangeState(TradeState.WAITING);
        }

        public void Abort()
        {
            throttleTimer.Stop();

            // Remove event handlers for the TradeSteps.
            tradeSteps[TradeStepType.PRECONDITIONS].ValueChanged -= Trade_ValueChanged;
            tradeSteps[TradeStepType.ENTRY].ValueChanged -= Trade_ValueChanged;
            tradeSteps[TradeStepType.EXIT].ValueChanged -= Trade_ValueChanged;
            tradeSteps[TradeStepType.STOP].ValueChanged -= Trade_ValueChanged;

            ChangeState(TradeState.ABORTED);
        }

        public void Reset()
        {
            ChangeState(TradeState.NOT_STARTED);
        }

        void Trade_ValueChanged(object sender, EventArgs e)
        {
            if (Active == false)
            {
                inTradeValueChangedHandler = false;
                return;
            }

            if (inTradeValueChangedHandler == true)
                return;

            inTradeValueChangedHandler = true;

            switch (currentTradeState)
            {
                case TradeState.NOT_STARTED:
                    break;
                case TradeState.WAITING:
                    OpenNewTradeCycle();
                    if (tradeSteps[TradeStepType.PRECONDITIONS].Evaluate() == true)
                        ChangeState(TradeState.RUNNING);
                    break;
                case TradeState.RUNNING:
                    if (tradeSteps[TradeStepType.PRECONDITIONS].Evaluate() == false)
                        ChangeState(TradeState.WAITING);
                    else if (tradeSteps[TradeStepType.ENTRY].Evaluate() == true)
                    {
                        EnterTrade();
                        ChangeState(TradeState.ENTERING);
                    }
                    break;
                case TradeState.ENTERING:
                case TradeState.ENTERED:
                    if (tradeSteps[TradeStepType.EXIT].Evaluate() == true)
                    {
                        ExitTrade();
                        ChangeState(TradeState.EXITING);
                    }
                    else if (tradeSteps[TradeStepType.STOP].Evaluate() == true)
                    {
                        StopOutTrade();
                        ChangeState(TradeState.STOPPING);
                    }
                    break;
                case TradeState.EXITING:
                    break;
                case TradeState.EXITED:
                    CloseTradeCycle();
                    CheckAutoRestart();
                    break;
                case TradeState.STOPPING:
                    break;
                case TradeState.STOPPED:
                    CloseTradeCycle();
                    CheckAutoRestart();
                    break;
                case TradeState.ABORTED:
                    break;
                default:
                    break;
            }

            inTradeValueChangedHandler = false;
        }

        void ChangeState(TradeState updatedState)
        {
            if (currentTradeState != updatedState)
            {
                switch (updatedState)
                {
                    case TradeState.NOT_STARTED:
                        SetAllTradeStepStates(TradeStepState.INACTIVE);
                        if (Metrics != null) Metrics.TradeIsRunning = false;
                        break;
                    case TradeState.WAITING:
                        tradeSteps[TradeStepType.PRECONDITIONS].State = TradeStepState.ACTIVE;
                        if (Metrics != null) Metrics.TradeIsRunning = true;
                        break;
                    case TradeState.RUNNING:
                        tradeSteps[TradeStepType.PRECONDITIONS].State = TradeStepState.COMPLETED;
                        tradeSteps[TradeStepType.ENTRY].State = TradeStepState.ACTIVE;
                        if (Metrics != null) Metrics.TradeIsRunning = true;
                        break;
                    case TradeState.ENTERING:
                        tradeSteps[TradeStepType.ENTRY].State = TradeStepState.WORKING;
                        tradeSteps[TradeStepType.EXIT].State = TradeStepState.ACTIVE;
                        tradeSteps[TradeStepType.STOP].State = TradeStepState.ACTIVE;
                        if (Metrics != null) Metrics.TradeIsRunning = true;
                        break;
                    case TradeState.ENTERED:
                        tradeSteps[TradeStepType.ENTRY].State = TradeStepState.COMPLETED;
                        tradeSteps[TradeStepType.EXIT].State = TradeStepState.ACTIVE;
                        tradeSteps[TradeStepType.STOP].State = TradeStepState.ACTIVE;
                        if (Metrics != null) Metrics.TradeIsRunning = true;
                        break;
                    case TradeState.EXITING:
                        tradeSteps[TradeStepType.EXIT].State = TradeStepState.WORKING;
                        tradeSteps[TradeStepType.STOP].State = TradeStepState.INACTIVE;
                        if (Metrics != null) Metrics.TradeIsRunning = false;
                        break;
                    case TradeState.EXITED:
                        tradeSteps[TradeStepType.EXIT].State = TradeStepState.COMPLETED;
                        tradeSteps[TradeStepType.STOP].State = TradeStepState.INACTIVE;
                        if (Metrics != null) Metrics.TradeIsRunning = false;
                        break;
                    case TradeState.STOPPING:
                        tradeSteps[TradeStepType.EXIT].State = TradeStepState.INACTIVE;
                        tradeSteps[TradeStepType.STOP].State = TradeStepState.WORKING;
                        if (Metrics != null) Metrics.TradeIsRunning = false;
                        break;
                    case TradeState.STOPPED:
                        tradeSteps[TradeStepType.EXIT].State = TradeStepState.INACTIVE;
                        tradeSteps[TradeStepType.STOP].State = TradeStepState.COMPLETED;
                        if (Metrics != null) Metrics.TradeIsRunning = false;
                        break;
                    case TradeState.ABORTED:
                        SetAllTradeStepStates(TradeStepState.ABORTED);
                        if (Metrics != null) Metrics.TradeIsRunning = false;
                        break;
                    default:
                        throw new NotImplementedException("An invalid TradeState has been encountered.");
                        break;
                }

                currentTradeState = updatedState;
                if (StateChanged != null) StateChanged(this, EventArgs.Empty);
                
                // Fire the ValueChanged event manually for the first time for cases
                // such as when there are no Preconditions...
                Trade_ValueChanged(this, EventArgs.Empty);
            }
        }

        void SetAllTradeStepStates(TradeStepState state)
        {
            tradeSteps[TradeStepType.PRECONDITIONS].State = state;
            tradeSteps[TradeStepType.ENTRY].State = state;
            tradeSteps[TradeStepType.EXIT].State = state;
            tradeSteps[TradeStepType.STOP].State = state;
        }

        void EnterTrade()
        {
            if (TradeInstrument != null)
            {
                if (BuySell == zBuySell.Buy)
                    execution.Buy(TradeInstrument, RealQuantity, this.Name);
                else
                    execution.Sell(TradeInstrument, RealQuantity, this.Name);
            }

            if (Metrics != null) UpdateTradeCycle(TradeCycleUpdateType.TRADE_ENTERED);

            Notify(NotificationType.ENTER_TRADE, "Entered trade.");
        }

        void ExitTrade()
        {
            if (TradeInstrument != null)
            {
                if (BuySell == zBuySell.Sell)
                    execution.Buy(TradeInstrument, RealQuantity, this.Name);
                else
                    execution.Sell(TradeInstrument, RealQuantity, this.Name);
            }

            if (Metrics != null) UpdateTradeCycle(TradeCycleUpdateType.TRADE_EXITED);

            Notify(NotificationType.EXIT_TRADE, "Exited trade.");

            //CloseTradeCycle();

            //CheckAutoRestart();
        }

        void StopOutTrade()
        {
            if (TradeInstrument != null)
            {
                if (BuySell == zBuySell.Sell)
                    execution.Buy(TradeInstrument, RealQuantity, this.Name);
                else
                    execution.Sell(TradeInstrument, RealQuantity, this.Name);
            }

            if (Metrics != null) UpdateTradeCycle(TradeCycleUpdateType.TRADE_STOPPED);

            Notify(NotificationType.STOP_TRADE, "Stopped out of trade.");

            //CloseTradeCycle();

            //CheckAutoRestart();
        }

        void Notify(NotificationType nType, string msg)
        {
            if (NotificationArrived != null) NotificationArrived(this, new NotificationEventArgs(nType, msg));
        }

        void CheckAutoRestart()
        {
            if (AutoRestart == true)
            {
                throttleTimer.Interval = 1000;
                throttleStartTime = DateTime.Now;
                
                if (ThrottleCountdownTimer != null) ThrottleCountdownTimer(this, EventArgs.Empty);
                throttleTimer.Start();
            }
        }

        void throttleTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            TimeSpan elapsedTime = DateTime.Now.Subtract(throttleStartTime);
            double countdownInSeconds = Math.Round(Throttle - elapsedTime.TotalSeconds);
            throttleCountdown = Math.Max(0, (int)countdownInSeconds);            
            
            if (ThrottleCountdownTimer != null) ThrottleCountdownTimer(this, EventArgs.Empty);

            if (throttleCountdown == 0)
            {
                throttleTimer.Stop();
                throttleCountdown = (int)Throttle;
                if (AutoRestartSignal != null) AutoRestartSignal(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Establish the beginning of a new Trade Cycle. If our current ("working")
        /// trade cycle is null, then we need to create one and add it to our Trade Metrics.
        /// </summary>
        void OpenNewTradeCycle()
        {
            if (tradeCycle == null)
            {
                tradeCycle = new TradeCycleDetail();
                Metrics.AddTradeCycle(tradeCycle);

                // Set this Trade Cycle as the current trade cycle for
                // all of the DataProviders in our trade.
                SetTradeCycleForDataProviders(tradeCycle);
            }
        }

        /// <summary>
        /// Establish the end of the Trade Cycle. We set our current ("working") trade
        /// cycle to null in order to indicate we are in a state where we have closed
        /// out our open trade cycle.
        /// </summary>
        void CloseTradeCycle()
        {

            SetTradeCycleForDataProviders(null);
            tradeCycle = null;
        }

        /// <summary>
        /// For all DataProviders in this Trade, set the current ("working") trade cycle.
        /// </summary>
        /// <param name="cycle">the TradeCycleDetail object to pass to the DataProviders</param>
        void SetTradeCycleForDataProviders(TradeCycleDetail tc)
        {
            foreach (TradeStep step in tradeSteps.Values)
            {
                foreach (TradeRule rule in step.Rules)
                {
                    foreach (RuleCondition condition in rule.Conditions)
                    {
                        condition.Value1.DataProvider.TradeDetail = tc;
                        condition.Value2.DataProvider.TradeDetail = tc;
                    }
                }
            }
        }

        void UpdateTradeCycle(TradeCycleUpdateType updateType)
        {
            switch (updateType)
            {
                case TradeCycleUpdateType.TRADE_ENTERED:
                    tradeCycle.SetEntryStartTime(DateTime.Now);
                    tradeCycle.SetTradeInstrument(this.tradeInstrument);
                    break;
                case TradeCycleUpdateType.TRADE_EXITED:
                    tradeCycle.SetExitStartTime(DateTime.Now);
                    Metrics.CompleteTradeCycleExit(tradeCycle);
                    break;
                case TradeCycleUpdateType.TRADE_STOPPED:
                    tradeCycle.SetExitStartTime(DateTime.Now);
                    Metrics.CompleteTradeCycleStop(tradeCycle);
                    break;
            }
        }

    } // class
} // namespace
