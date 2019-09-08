using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZAPI.Data
{
    public class TradeMetrics
    {
        public event EventHandler TradeMetricsUpdated;

        public int EntryCount { get { return entryCount; } }
        public int ExitCount { get { return exitCount; } }
        public int StopCount { get { return stopCount; } }

        public double WinPercent { get { return (ExitCount + StopCount) == 0 ? 0 : Math.Round((double)ExitCount / (double)(ExitCount + StopCount) * 100.0, 2); } }
        public double LosePercent { get { return (ExitCount + StopCount) == 0 ? 0 : Math.Round((double)StopCount / (double)(ExitCount + StopCount) * 100.0, 2); } }

        public double ProfitLoss { get { return profitLoss; } }

        public TimeSpan RunTime
        {
            get
            {
                if (tradeIsRunning == false)
                {
                    return accumulatedRunningTime;
                }
                else
                {
                    TimeSpan span = DateTime.Now.Subtract(runningStartTime);
                    return accumulatedRunningTime.Add(span);
                }
            }
        }

        public bool TradeIsRunning
        {
            set
            {
                // We only need to take action if the TradeIsRunning value is changing.
                if (value != tradeIsRunning)
                {
                    tradeIsRunning = value;
                    // TradeIsRunning has been set to true.
                    if (tradeIsRunning == true)
                    {
                        runningStartTime = DateTime.Now;
                    }
                    else  // TradeIsRunning has been set to false.
                    {
                        TimeSpan span = DateTime.Now.Subtract(runningStartTime);
                        accumulatedRunningTime = accumulatedRunningTime.Add(span);
                    }
                }
            }
        }

        private List<TradeCycleDetail> tradeCycles;
        private int entryCount;
        private int exitCount;
        private int stopCount;
        private double profitLoss;
        private bool tradeIsRunning;
        private DateTime runningStartTime;
        private TimeSpan accumulatedRunningTime;

        public TradeMetrics()
        {
            entryCount = 0;
            exitCount = 0;
            stopCount = 0;
            profitLoss = 0.0;
            tradeIsRunning = false;
            accumulatedRunningTime = new TimeSpan(0);

            tradeCycles = new List<TradeCycleDetail>();
        }

        public void AddTradeCycle(TradeCycleDetail tradeCycle)
        {
            tradeCycles.Add(tradeCycle);
            
            // TODO: Some trade cycles may exist without an entry (i.e. an Abort occurred).
            ++entryCount;

            if (TradeMetricsUpdated != null) TradeMetricsUpdated(this, EventArgs.Empty);
        }

        public void CompleteTradeCycleExit(TradeCycleDetail tradeCycle)
        {
            tradeCycle.SetExitMethod(TradeExitMethod.EXIT);
            ++exitCount;

            if (TradeMetricsUpdated != null) TradeMetricsUpdated(this, EventArgs.Empty);
        }

        public void CompleteTradeCycleStop(TradeCycleDetail tradeCycle)
        {
            tradeCycle.SetExitMethod(TradeExitMethod.STOP);
            ++stopCount;

            if (TradeMetricsUpdated != null) TradeMetricsUpdated(this, EventArgs.Empty);
        }

    } // class
} // namespace
