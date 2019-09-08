using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZAPI.Framework.Base;
using EZAPI.Framework.Chart;
using T4;
using T4.API;
using T4.API.ChartData;

namespace EZAPI
{
    // Convert TO/FROM API-specific objects.
    public static class APIConvert
    {
        public static zChartInterval FromChartInterval(ChartInterval chartInterval)
        {
            string sInterval = chartInterval.ToString();
            zChartInterval interval = (zChartInterval)Enum.Parse(typeof(zChartInterval), sInterval, true);
            return interval;
        }

        public static ChartInterval ToChartInterval(zChartInterval chartInterval)
        {
            string sInterval = chartInterval.ToString();
            ChartInterval interval = (ChartInterval)Enum.Parse(typeof(ChartInterval), sInterval, true);
            return interval;
        }

        public static BuySell ToBuySell(zBuySell buySell)
        {
            string sSide = buySell.ToString();
            BuySell side = (BuySell)Enum.Parse(typeof(BuySell), sSide, true);
            return side;
        }

        public static zBuySell FromBuySell(BuySell buySell)
        {
            string sSide = buySell.ToString();
            zBuySell side = (zBuySell)Enum.Parse(typeof(zBuySell), sSide, true);
            return side;
        }

    } // class
} // namespace
