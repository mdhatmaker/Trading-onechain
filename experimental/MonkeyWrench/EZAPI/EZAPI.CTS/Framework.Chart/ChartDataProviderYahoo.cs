using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using EZAPI.Toolbox.Debug;
using EZAPI.Financial.Historical;

namespace EZAPI.Framework.Chart
{
    public class ChartDataProviderYahoo : IChartDataProvider
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

        public ChartDataProviderYahoo(string chartName, ezBarInterval barInterval, ezSessionTimeRange session)
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

            //var chartThread = new ChartDataThread(this, chartData, startDate, endDate);
            //var thread = new Thread(new ThreadStart(chartThread.Run));
            //thread.Name = chartName + ":" + barInterval;
            //thread.Start();
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
            LoadHistoricalChartData(chartIdentifier, startDate, DateTime.Now);
        }

        public void LoadHistoricalChartData(object chartIdentifier, DateTime startDate, DateTime endDate)
        {
            string ticker = chartIdentifier as string;

            int startYear = startDate.Year;
            int startMonth = startDate.Month - 1;   // have to subtract 1 from the month to get the right data from Yahoo
            int startDay = startDate.Day;
            int endYear = endDate.Year;
            int endMonth = endDate.Month - 1;       // have to subtract 1 from the month to get the right data from Yahoo
            int endDay = endDate.Day;

            ezDateRange dateRangeRequested = new ezDateRange();
            ezDateRange dateRangeProcessed = new ezDateRange();

            List<HistoricalStock> historical = YahooHistoricalDownloader.Instance.DownloadData(ticker, startDate, endDate);

            // Start with an empty set of TradeBars.
            chartData.TradeBars.Clear();

            // Yahoo downloads the data in reverse date order (most recent dates first, then previous day, and so on). So
            // we need to reverse it when we put it into our chart data.
            for (int i = historical.Count - 1; i >= 0; i--)
            {
                HistoricalStock hs = historical[i];
                ezBarDataPoint bdp = new ezBarDataPoint();
                bdp.TradeDate = hs.Date;
                bdp.Open = hs.Open;
                bdp.High = hs.High;
                bdp.Low = hs.Low;
                bdp.Close = hs.Close;
                bdp.Volume = hs.Volume;

                chartData.TradeBars.Add(bdp);
            }

            if (DataLoadComplete != null) DataLoadComplete(this, new ezDataLoadCompleteEventArgs(zDataLoadStatus.Success, dateRangeRequested, dateRangeProcessed));
        }
    } // class


    /*
    class ChartDataThread
    {
        IChartDataProvider provider;
        EZChartDataSeries chartData;
        DateTime startDate;
        DateTime? endDate;

        public ChartDataThread(IChartDataProvider provider, EZChartDataSeries chartData, DateTime startDate, DateTime? endDate)
        {
            this.provider = provider;
            this.chartData = chartData;
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public void Run()
        {
            Spy.Print("Starting thread...");

            provider.DataLoadComplete += chartDataSeries_DataLoadComplete;
            provider.DataSeriesUpdated += chartDataSeries_DataSeriesUpdated;

            if (endDate == null)
            {
                // Request the chart data and subscribe for updates.
                provider.LoadRealTimeChartData(startDate);
            }
            else
            {
                // Load only the historical chart data.
                provider.LoadHistoricalChartData(startDate, endDate.Value);
            }

            Spy.Print("load realtime chart data - complete");
        }

        void chartDataSeries_DataSeriesUpdated(object sender, ezDataSeriesUpdatedEventArgs e)
        {
            Spy.Print("DataSeriesUpdated (on thread)");

            if (e.TradeBarIndexUpdate == -1)
                return;

            var update = new ezDataSeriesUpdatedEventArgs(e.ModeIndexUpdate, e.SettlementIndexUpdate, e.TradeBarIndexUpdate);

            for (int i = e.TradeBarIndexUpdate; i < providerChartData.TradeBars.Count; i++)
            {
                if (i >= chartData.TradeBars.Count)
                {
                    BarDataPoint bdp = providerChartData.TradeBars[i] as BarDataPoint;
                    var ezBar = new ezBarDataPoint(bdp.OpenTicks, bdp.HighTicks, bdp.LowTicks, bdp.CloseTicks, bdp.TradeCount, bdp.Volume, bdp.TradeDate, bdp.Time);
                    chartData.TradeBars.Add(ezBar);
                }
                else
                {
                    BarDataPoint bdp = providerChartData.TradeBars[i] as BarDataPoint;
                    var ezBar = new ezBarDataPoint(bdp.OpenTicks, bdp.HighTicks, bdp.LowTicks, bdp.CloseTicks, bdp.TradeCount, bdp.Volume, bdp.TradeDate, bdp.Time);
                    chartData.UpdateTradeBar(i, ezBar);
                }
            }
            chartData.DataProviderSeriesUpdated(update);
        }

        void chartDataSeries_DataLoadComplete(object sender, ezDataLoadCompleteEventArgs e)
        {
            Spy.Print("DataLoadComplete (on thread)");

            for (int i = 0; i < providerChartData.TradeBars.Count; i++)
            {
                BarDataPoint bdp = providerChartData.TradeBars[i] as BarDataPoint;
                var ezBar = new ezBarDataPoint(bdp.OpenTicks, bdp.HighTicks, bdp.LowTicks, bdp.CloseTicks, bdp.TradeCount, bdp.Volume, bdp.TradeDate, bdp.Time);
                chartData.TradeBars.Add(ezBar);
            }
            chartData.DataProviderLoadComplete();
        }
    }*/

}
