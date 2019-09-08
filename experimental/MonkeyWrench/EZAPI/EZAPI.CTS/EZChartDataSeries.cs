using System;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using System.Linq;
using System.Text;
using EZAPI.Framework.Base;
using EZAPI.Framework.Chart;

namespace EZAPI.Framework
{
    public class EZChartDataSeries
    {
        public string Name { get { return dataSeriesName; } }
        public ezBarInterval BarInterval { get { return interval; } }
        public ezSessionTimeRange SessionTimeRange { get { return session; } }

        public event ezDataSeriesUpdatedEventHandler DataSeriesUpdated;
        public event ezDataLoadCompleteEventHandler DataLoadComplete;

        //private EZInstrument instrument;
        private ezBarInterval interval;
        private ezSessionTimeRange session;
        private string dataSeriesName;

        private List<ezBarDataPointTicks> tickDataPoints;
        private List<ezBarDataPoint> dataPoints;

        public EZChartDataSeries(string dataSeriesName, ezBarInterval interval, ezSessionTimeRange session)
        {
            this.dataSeriesName = dataSeriesName;
            this.interval = interval;
            this.session = session;

            tickDataPoints = new List<ezBarDataPointTicks>();
            dataPoints = new List<ezBarDataPoint>();
        }

        /*public EZChartDataSeries(ezContract contract, ezBarInterval interval, ezSessionTimeRange session, ezContinuationSettings continuation)
        {

        }*/

        public void LoadRealTimeChartData(DateTime startDate)
        {
            DateTime endDate = DateTime.Now;


            dataPoints = new List<ezBarDataPoint>();
            /*foreach (HistoricalQuote hq in historical)
            {
                ezBarDataPoint dp = new ezBarDataPoint(hq.Open, hq.High, hq.Low, hq.Close, 0, hq.Volume);
                dataPoints.Add(dp);
            }*/

            Console.WriteLine("load realtime chart data - complete");
        }

        public void LoadHistoricalChartData(DateTime startDate, DateTime endDate)
        {

        }

        public void Lock()
        {
        }

        public void Unlock()
        {
        }

        public void DataProviderLoadComplete()
        {
            if (DataLoadComplete != null)
            {
                ezDateRange requested = new ezDateRange();
                ezDateRange processed = new ezDateRange();                
                DataLoadComplete(this, new ezDataLoadCompleteEventArgs(zDataLoadStatus.Success, requested, processed));
            }
        }

        public void DataProviderSeriesUpdated(ezDataSeriesUpdatedEventArgs update)
        {
            if (DataSeriesUpdated != null) DataSeriesUpdated(this, update);
        }

        public List<ezBarDataPoint> TradeBars
        {
            get { return dataPoints; }
        }

        public void UpdateTradeBar(int index, ezBarDataPoint barDataPoint)
        {
            dataPoints[index] = barDataPoint;
        }

    } // class
} // namespace
