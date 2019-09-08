using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZAPI.Framework;

namespace EZAPI.Framework.Chart
{
    public class ezBarDataPointTicks : IEZDataPoint 
    {
        public int OpenTicks { get; set; }
        public int CloseTicks { get; set; }
        public int HighTicks { get; set; }
        public int LowTicks { get; set; }
        public int TradeCount { get; set; }
        public int Volume { get; set; }

        //public EZInstrument Market { get { return instrument; } }
        public DateTime? Time
        {
            get { return time; }
            set
            {
                time = null;
                if (value != null)
                    time = value.Value;
            }
        }
        public DateTime? TradeDate
        {
            get { return tradeDate; }
            set
            {
                tradeDate = null;
                if (value != null)
                    tradeDate = value.Value;
            }
        }

        //private EZInstrument instrument;
        private DateTime? time = null;
        private DateTime? tradeDate = null;

        public ezBarDataPointTicks(int open, int high, int low, int close, int tradeCount, int volume, DateTime? tradeDate = null, DateTime? time = null)
        {
            OpenTicks = open;
            HighTicks = high;
            LowTicks = low;
            CloseTicks = close;
            TradeCount = tradeCount;
            Volume = volume;

            if (tradeDate != null)
                this.tradeDate = tradeDate.Value;

            if (time != null)
                this.time = time.Value;
        }

        public override string ToString()
        {
            return string.Format("{0}  o:{1} h:{2} l:{3} c:{4}", TradeDate, OpenTicks, HighTicks, LowTicks, CloseTicks);
        }

    } // class

} // namespace
