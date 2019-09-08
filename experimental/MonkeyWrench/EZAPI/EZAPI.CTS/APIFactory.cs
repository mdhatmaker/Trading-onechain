#define DEEPDEBUG
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using EZAPI.Framework;
using EZAPI.Framework.Base;
using EZAPI.Framework.Chart;
using EZAPI.Toolbox.Debug;
using T4;
using T4.API;
using T4.API.ChartData;

namespace EZAPI
{
    public static class APIFactory
    {
        static Dictionary<string, ezInstrumentKey> keyLookupFromString;
        static Dictionary<ezInstrumentKey, string> stringLookupFromKey;

        // Static constructor - cool
        static APIFactory()
        {
            keyLookupFromString = new Dictionary<string, ezInstrumentKey>();
            stringLookupFromKey = new Dictionary<ezInstrumentKey, string>();
        }

        static public EZInstrument MakeInstrument(Market market)
        {
            ezInstrumentKey iKey = MakeInstrumentKey(market);
            var ezi = new EZInstrument(iKey);
            ezi.Key = iKey;
            ezi.Name = market.Description;
            ezi.Description = market.Description;
            return ezi;
        }
        
        static public ezInstrumentKey MakeInstrumentKey(Market market)
        {
            ezInstrumentKey iKey;
            string keyString = CreateKeyString(market);
            if (keyLookupFromString.ContainsKey(keyString))
            {
                iKey = keyLookupFromString[keyString];
            }
            else
            {
                iKey = new ezInstrumentKey(market.ExchangeID, market.ContractID, market.MarketID, null);
                keyLookupFromString[keyString] = iKey;
                stringLookupFromKey[iKey] = keyString;
            }
            return iKey;
        }

        static public void ParseInstrumentKey(string keyString, out string exchangeID, out string contractID, out string marketID)
        {
            string[] split = keyString.Split('@');
            exchangeID = split[0];
            contractID = split[1];
            marketID = split[2];
        }

        static public string StringFromInstrumentKey(ezInstrumentKey ikey)
        {
            return stringLookupFromKey[ikey];
        }

        static public ezInstrumentKey InstrumentKeyFromString(string keyString)
        {
            if (keyString == null)
                return null;
            else
                return keyLookupFromString[keyString];
        }

        /*static public ezBarDataPoint MakeBarDataPoint(EZInstrument instrument, HistoricalQuote hq)
        {
            Market market = APIFunctions.FromInstrument(instrument);

            int openTicks = market.CalcTicks(hq.Open);
            int highTicks = market.CalcTicks(hq.High);
            int lowTicks = market.CalcTicks(hq.Low);
            int closeTicks = market.CalcTicks(hq.Close);

            return new ezBarDataPoint(openTicks, highTicks, lowTicks, closeTicks, 0, hq.Volume);
        }*/

        static public string CreateKeyString(Market market)
        {
            return market.ExchangeID + "@" + market.ContractID + "@" + market.MarketID;
        }

        /// <summary>
        /// Example: to get 15 minute bars,
        ///    interval = ChartInterval.Minute
        ///    period = 15
        /// </summary>
        /// <param name="market"></param>
        /// <param name="interval"></param>
        /// <param name="period"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate">Leave endDate = null for realtime (endDate = current date)</param>
        static public EZChartDataSeries MakeChartData(EZInstrument instrument, ezBarInterval barInterval, DateTime startDate, DateTime? endDate = null)
        {
            Market market = APIMain.MarketFromInstrument(instrument);

            // Load the data for the selected market.
            BarInterval interval = new BarInterval(APIConvert.ToChartInterval(barInterval.Interval), barInterval.Period);
            ChartDataSeries ctsChartData = new ChartDataSeries(market, interval, SessionTimeRange.Empty);

            var session = new ezSessionTimeRange();
            EZChartDataSeries chartData = new EZChartDataSeries(market.Description, barInterval, session);

            var chartThread = new ChartDataThread(ctsChartData, chartData, startDate, endDate);
            var thread = new Thread(new ThreadStart(chartThread.Run));
            thread.Name = market.Description + ":" + barInterval;
            thread.Start();

            return chartData;
            //dataPoints = new List<ezBarDataPoint>();
            /*foreach (HistoricalQuote hq in historical)
            {
                ezBarDataPoint dp = new ezBarDataPoint(hq.Open, hq.High, hq.Low, hq.Close, 0, hq.Volume);
                dataPoints.Add(dp);
            }*/
        }


    } // class

    class ChartDataThread
    {
        ChartDataSeries ctsChartData;
        EZChartDataSeries chartData;
        DateTime startDate;
        DateTime? endDate;

        public ChartDataThread(ChartDataSeries ctsChartData, EZChartDataSeries chartData, DateTime startDate, DateTime? endDate)
        {
            this.ctsChartData = ctsChartData;
            this.chartData = chartData;
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public void Run()
        {
            Spy.Print("Starting thread...");

            ctsChartData.DataLoadComplete += chartDataSeries_DataLoadComplete;
            ctsChartData.DataSeriesUpdated += chartDataSeries_DataSeriesUpdated;

            if (endDate == null)
            {
                // Request the chart data and subscribe for updates.
                ctsChartData.LoadRealTimeChartData(startDate);
            }
            else
            {
                // Load only the historical chart data.
                ctsChartData.LoadHistoricalChartData(startDate, endDate.Value);
            }

            Console.WriteLine("load realtime chart data - complete");
        }

        void chartDataSeries_DataSeriesUpdated(object sender, DataSeriesUpdatedEventArgs e)
        {
            Spy.Print("DataSeriesUpdated (on thread)");

            if (e.TradeBarIndexUpdate == -1)
                return;

            var update = new ezDataSeriesUpdatedEventArgs(e.ModeIndexUpdate, e.SettlementIndexUpdate, e.TradeBarIndexUpdate);

            for (int i = e.TradeBarIndexUpdate; i < ctsChartData.TradeBars.Count; i++)
            {
                if (i >= chartData.TradeBars.Count)
                {
                    BarDataPoint bdp = ctsChartData.TradeBars[i] as BarDataPoint;
                    var ezBar = new ezBarDataPoint();
                    ezBar.Open = bdp.OpenTicks;
                    ezBar.High = bdp.HighTicks;
                    ezBar.Low = bdp.LowTicks;
                    ezBar.Close = bdp.CloseTicks;
                    ezBar.TradeCount = bdp.TradeCount;
                    ezBar.Volume = bdp.Volume;
                    ezBar.TradeDate = bdp.TradeDate;
                    ezBar.Time = bdp.Time;
                    chartData.TradeBars.Add(ezBar);
                }
                else
                {
                    BarDataPoint bdp = ctsChartData.TradeBars[i] as BarDataPoint;
                    //var ezBar = new ezBarDataPoint(bdp.OpenTicks, bdp.HighTicks, bdp.LowTicks, bdp.CloseTicks, bdp.TradeCount, bdp.Volume, bdp.TradeDate, bdp.Time);
                    var ezBar = new ezBarDataPoint();
                    ezBar.Open = bdp.OpenTicks;
                    ezBar.High = bdp.HighTicks;
                    ezBar.Low = bdp.LowTicks;
                    ezBar.Close = bdp.CloseTicks;
                    ezBar.TradeCount = bdp.TradeCount;
                    ezBar.Volume = bdp.Volume;
                    ezBar.TradeDate = bdp.TradeDate;
                    ezBar.Time = bdp.Time;
                    chartData.TradeBars.Add(ezBar);
                    chartData.UpdateTradeBar(i, ezBar);
                }
            }
            chartData.DataProviderSeriesUpdated(update);
        }

        void chartDataSeries_DataLoadComplete(object sender, DataLoadCompleteEventArgs e)
        {
            Spy.Print("DataLoadComplete (on thread)");

            for (int i = 0; i < ctsChartData.TradeBars.Count; i++)
            {
                BarDataPoint bdp = ctsChartData.TradeBars[i] as BarDataPoint;
                //var ezBar = new ezBarDataPoint(bdp.OpenTicks, bdp.HighTicks, bdp.LowTicks, bdp.CloseTicks, bdp.TradeCount, bdp.Volume, bdp.TradeDate, bdp.Time);
                var ezBar = new ezBarDataPoint();
                ezBar.Open = bdp.OpenTicks;
                ezBar.High = bdp.HighTicks;
                ezBar.Low = bdp.LowTicks;
                ezBar.Close = bdp.CloseTicks;
                ezBar.TradeCount = bdp.TradeCount;
                ezBar.Volume = bdp.Volume;
                ezBar.TradeDate = bdp.TradeDate;
                ezBar.Time = bdp.Time;
                chartData.TradeBars.Add(ezBar);
            }
            chartData.DataProviderLoadComplete();
        }


    }

} // namespace
