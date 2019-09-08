using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZAPI.Framework.Chart
{
    public class ezDataSeriesUpdatedEventArgs : EventArgs
    {
        public int ModeIndexUpdate { get { return modeIndexUpdate; } }
        public int SettlementIndexUpdate { get { return settlementIndexUpdate; } }
        public int TradeBarIndexUpdate { get { return tradeBarIndexUpdate; } }

        private int modeIndexUpdate;
        private int settlementIndexUpdate;
        private int tradeBarIndexUpdate;

        public ezDataSeriesUpdatedEventArgs(int modeIndexUpdate, int settlementIndexUpdate, int tradeBarIndexUpdate)
        {
            this.modeIndexUpdate = modeIndexUpdate;
            this.settlementIndexUpdate = settlementIndexUpdate;
            this.tradeBarIndexUpdate = tradeBarIndexUpdate;
        }

        private ezDataSeriesUpdatedEventArgs()
        {
        }

        public static new ezDataSeriesUpdatedEventArgs Empty
        {
            get { return new ezDataSeriesUpdatedEventArgs(); }
        }
    } // class
} // namespace
