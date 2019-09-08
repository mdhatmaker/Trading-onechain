using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZAPI.Framework;

namespace EZAPI.Data
{
    public enum TradeExitMethod { EXIT, STOP, INVALID }

    public class TradeCycleDetail
    {
        public event Action TradeCycleUpdated;

        public EZInstrument TradeInstrument { get { return tradeInstrument; } }

        public DateTime EntryStartTime { get { return entryStartTime; } }
        public DateTime EntryCompleteTime { get { return entryCompleteTime; } }
        public DateTime ExitStartTime { get { return exitStartTime; } }
        public DateTime ExitCompleteTime { get { return exitCompleteTime; } }

        public TradeExitMethod ExitMethod { get { return exitMethod; } }

        public decimal ProfitLoss { get { return profitLoss; } }
        public decimal EntryPriceAverage { get { return entryPriceAverage; } }
        public decimal ExitPriceAverage { get { return exitPriceAverage; } }

        private EZInstrument tradeInstrument;
        private DateTime entryStartTime;
        private DateTime entryCompleteTime;
        private DateTime exitStartTime;
        private DateTime exitCompleteTime;
        private TradeExitMethod exitMethod;
        private decimal profitLoss;
        private decimal entryPriceAverage;
        private decimal exitPriceAverage;

        public TradeCycleDetail()
        {
            // Initialize the times to "MinValue" (equivalent to "null" for our needs).
            entryStartTime = DateTime.MinValue;
            entryCompleteTime = DateTime.MinValue;
            exitStartTime = DateTime.MinValue;
            exitCompleteTime = DateTime.MinValue;
            exitMethod = TradeExitMethod.INVALID;
            profitLoss = 0;
            entryPriceAverage = 0;
            exitPriceAverage = 0;
        }

        void FireUpdateEvent()
        {
            if (TradeCycleUpdated != null) TradeCycleUpdated();
        }

        public void SetTradeInstrument(EZInstrument instrument)
        {
            tradeInstrument = instrument;
            FireUpdateEvent();
        }

        public void SetProfitLoss(decimal pl)
        {
            profitLoss = pl;
            FireUpdateEvent();
        }

        public void SetEntryPriceAverage(decimal entryPrice)
        {
            entryPriceAverage = entryPrice;
            FireUpdateEvent();
        }

        public void SetExitPriceAverage(decimal exitPrice)
        {
            exitPriceAverage = exitPrice;
            FireUpdateEvent();
        }

        public void SetEntryStartTime(DateTime time)
        {
            entryStartTime = time;
            FireUpdateEvent();
        }

        public void SetEntryCompleteTime(DateTime time)
        {
            entryCompleteTime = time;
            FireUpdateEvent();
        }

        public void SetExitStartTime(DateTime time)
        {
            exitStartTime = time;
            FireUpdateEvent();
        }

        public void SetExitCompleteTime(DateTime time)
        {
            exitCompleteTime = time;
            FireUpdateEvent();
        }

        public void SetExitMethod(TradeExitMethod tradeExitMethod)
        {
            exitMethod = tradeExitMethod;
            FireUpdateEvent();
        }

    } // class
} // namespace
