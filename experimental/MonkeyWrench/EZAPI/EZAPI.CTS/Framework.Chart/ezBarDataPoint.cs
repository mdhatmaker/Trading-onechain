using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZAPI.Framework;

namespace EZAPI.Framework.Chart
{
    public class ezBarDataPoint : IEZDataPoint 
    {
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public int TradeCount { get; set; }
        public int Volume { get; set; }

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

        private DateTime? time = null;
        private DateTime? tradeDate = null;

        public ezBarDataPoint()
        {

        }

        public ezBarDataPoint(double open, double high, double low, double close, int volume)
        {
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
        }

        public override string ToString()
        {
            return string.Format("{0}  o:{1} h:{2} l:{3} c:{4}", TradeDate, Open, High, Low, Close);
        }

    } // class

} // namespace
