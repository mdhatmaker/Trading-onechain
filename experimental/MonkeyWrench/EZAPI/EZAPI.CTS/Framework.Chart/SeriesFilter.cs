using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSChart = System.Windows.Forms.DataVisualization.Charting;

namespace EZAPI.Framework.Chart
{
    public class SeriesFilter : MSChart.IDataPointFilter 
    {
        public string Name { get { return filteredSeriesName; } }
        public MSChart.Series Series { get { return filteredSeries; } }

        private static Dictionary<string, SeriesFilter> storeFilters = new Dictionary<string, SeriesFilter>();

        private MSChart.Chart chart;
        private MSChart.Series originalSeries;
        private MSChart.Series filteredSeries;
        private MSChart.DataManipulator dataManipulator;
        private double startX;
        private double endX;
        private string filteredSeriesName;

        public static SeriesFilter Create(string filteredSeriesName, MSChart.Chart chart, string seriesName)
        {
            if (!storeFilters.ContainsKey(filteredSeriesName) || storeFilters[filteredSeriesName] == null)
                storeFilters[filteredSeriesName] = new SeriesFilter(filteredSeriesName, chart, seriesName);

            return storeFilters[filteredSeriesName];
        }

        private SeriesFilter(string filteredSeriesName, MSChart.Chart chart, string seriesName)        
        {
            this.filteredSeriesName = filteredSeriesName;
            this.chart = chart;
            this.dataManipulator = chart.DataManipulator;
            this.originalSeries = chart.Series[seriesName];

            filteredSeries = new MSChart.Series(filteredSeriesName);
            filteredSeries.Enabled = false;
            filteredSeries.IsXValueIndexed = true;
            chart.Series.Add(filteredSeries);
        }

        public void FilterSeriesRange(int dpIndex1, int dpIndex2)
        {
            MSChart.DataPoint dp1 = originalSeries.Points[dpIndex1];
            MSChart.DataPoint dp2 = originalSeries.Points[dpIndex2];

            FilterSeriesRange(dp1, dp2);
        }

        public void FilterSeriesRange(MSChart.DataPoint startDP, MSChart.DataPoint endDP)
        {
            startX = startDP.XValue;
            endX = endDP.XValue;

            dataManipulator.Filter(this, originalSeries, filteredSeries);
        }

        public bool FilterDataPoint(MSChart.DataPoint point, MSChart.Series series, int pointIndex)
        {
            if (point.XValue < startX || point.XValue > endX)
                return true;
            else
                return false;
        }
    } // class
} // namespace
