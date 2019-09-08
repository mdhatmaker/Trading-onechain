using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZAPI.Framework;
using EZAPI.Framework.Base;

namespace EZAPI.Framework.Spread
{
    public class ezSpreadLeg
    {
        public EZInstrument Instrument { get; set; }
        public zBuySell BuySell { get { return buySell; } }
        public int ExecuteSize { get { return executeSize; } }
        public double PriceMultiplier { get { return priceMultiplier; } }

        zBuySell buySell;
        int executeSize;
        double priceMultiplier;

        EZInstrument instrument;

        public ezSpreadLeg(EZInstrument instrument, zBuySell buySell, int executeSize, double priceMultiplier)
        {
            this.Instrument = instrument;
            this.buySell = buySell;
            this.executeSize = executeSize;
            this.priceMultiplier = priceMultiplier;
        }

    } // class
} // namespace
