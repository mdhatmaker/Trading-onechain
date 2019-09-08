using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using EZAPI.Toolbox.Debug;
using T4;
using T4.API;
using T4.API.ChartData;

namespace EZAPI.Framework.Chart
{
    public class ChartDataProviderCTS : IChartDataProvider
    {
        public event ezDataLoadCompleteEventHandler DataLoadComplete;
        public event ezDataSeriesUpdatedEventHandler DataSeriesUpdated;

        public string Name { get { return chartName; } }
        public EZChartDataSeries ChartData { get { return chartData; } }
        public object ChartIdentifier { get; private set; }
        public ezBarInterval BarInterval { get; private set; }
        public ezSessionTimeRange Session { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        private EZChartDataSeries chartData;
        private string chartName;

        public ChartDataProviderCTS(string chartName, ezBarInterval barInterval, ezSessionTimeRange session)
        {
            this.chartName = chartName;

            // If null has been passed as barInterval or session, then we will use default values for those.
            if (barInterval == null)
                barInterval = new ezBarInterval(zChartInterval.Day, 1);

            if (session == null)
                session = new ezSessionTimeRange();

            this.BarInterval = barInterval;
            this.Session = session;

            // Create the EZChartDataSeries - this will be available via the ChartData property (but the caller
            // should subscribe to the events to know when the data has finished loading or updates occur).
            this.chartData = new EZChartDataSeries(chartName, barInterval, session);

            this.chartData.DataLoadComplete += chartData_DataLoadComplete;
            this.chartData.DataSeriesUpdated += chartData_DataSeriesUpdated;
        }

        void chartData_DataSeriesUpdated(object sender, ezDataSeriesUpdatedEventArgs e)
        {
            if (DataSeriesUpdated != null) DataSeriesUpdated(this, e);
        }

        void chartData_DataLoadComplete(object sender, ezDataLoadCompleteEventArgs e)
        {
            if (DataLoadComplete != null) DataLoadComplete(this, e);
        }

        public void Lock()
        {
        }

        public void Unlock()
        {
        }

        /// <summary>
        /// There is no "real time" data for Yahoo historical, so we will use the LoadHistorical method and supply today's date as the end date.
        /// </summary>
        public void LoadRealTimeChartData(object chartIdentifier, DateTime startDate)
        {
            LoadHistoricalChartData(chartIdentifier, startDate, DateTime.MaxValue);
        }

        public void LoadHistoricalChartData(object chartIdentifier, DateTime startDate, DateTime endDate)
        {
            ChartIdentifier = chartIdentifier;
            StartDate = startDate;
            EndDate = endDate;

            Market market = chartIdentifier as Market;

            ezDateRange dateRangeRequested = new ezDateRange();
            ezDateRange dateRangeProcessed = new ezDateRange();

            // Start with an empty set of TradeBars.
            chartData.TradeBars.Clear();
            
            // Load the data for the selected market.
            BarInterval interval = new BarInterval(APIConvert.ToChartInterval(BarInterval.Interval), BarInterval.Period);
            ChartDataSeries ctsChartData = new ChartDataSeries(market, interval, SessionTimeRange.Empty);

            var session = new ezSessionTimeRange();
            /*EZChartDataSeries myChartData = new EZChartDataSeries(market.Description, BarInterval, session);
            myChartData.DataLoadComplete += myChartData_DataLoadComplete;
            myChartData.DataSeriesUpdated += myChartData_DataSeriesUpdated;*/

            var chartThread = new ChartDataThread(ctsChartData, chartData, startDate, endDate);
            
            var thread = new Thread(new ThreadStart(chartThread.Run));
            thread.Name = market.Description + ":" + BarInterval;
            thread.Start();


            //if (DataLoadComplete != null) DataLoadComplete(this, new ezDataLoadCompleteEventArgs(zDataLoadStatus.Success, dateRangeRequested, dateRangeProcessed));
        }


    } // class

    class ChartDataThread
    {
        ChartDataSeries ctsChartData;
        EZChartDataSeries chartData;
        DateTime startDate;
        DateTime endDate;

        public ChartDataThread(ChartDataSeries ctsChartData, EZChartDataSeries chartData, DateTime startDate, DateTime endDate)
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

            if (endDate == DateTime.MaxValue)
            {
                // Request the chart data and subscribe for updates.
                ctsChartData.LoadRealTimeChartData(startDate);
            }
            else
            {
                // Load only the historical chart data.
                ctsChartData.LoadHistoricalChartData(startDate, endDate);
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


    } // class

} // namespace
