using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MSChart = System.Windows.Forms.DataVisualization.Charting;
using EZAPI.Toolbox.DataStructures;

namespace EZAPI.Framework.Chart.Indicators
{
    public class TrendLine : Indicator
    {
        public event Action<double> SlopeChanged;

        public double Slope { get; private set; }

        public TrendLine(string name = "Trend Line", string description = "", string moreInfoWebPage = "")
            : base(name)
        {
            this.description = description;
            this.moreInfoWebPage = moreInfoWebPage;
            this.indicatorType = IndicatorChartArea.BottomChart;

            // If we are passed a blank description or web page, then use our default values.
            // If 'null' is passed to the constructor for either of those values, they stay null (in case the user of this class doesn't want them displayed).
            if (description == "")
                this.description = null;
            if (moreInfoWebPage == "")
                this.moreInfoWebPage = null;

            // ADD PARAMETERS HERE
            AddParameter(new IndicatorParameter<int>("StartOffset", "Bars back from current bar for the START of the trend line calculation.", 15));
            AddParameter(new IndicatorParameter<int>("EndOffset", "Bars back from current bar for the END of the trend line calculation.", 0));

            this["Color"] = Color.Navy;
            this["Line Style"] = zChartLineStyle.Solid;
        }

        /// <summary>
        /// Override the Draw method to display the indicator.
        /// </summary>
        public override void Draw(MSChart.Chart chart1, bool enable = true)
        {
            string TREND = "TrendLine";

            int startIndexOffset = (int)parameters["StartOffset"].Value;
            int endIndexOffset = (int)parameters["EndOffset"].Value;

            // Convert from an "offset" (bars back from the current bar - zero = current bar) to an index into the series Points.
            int startIndex;
            int endIndex;
            int pointCount = chart1.Series["candlestickSeries"].Points.Count;
            if (startIndexOffset <= endIndexOffset)
            {
                startIndex = startIndexOffset;
                endIndex = endIndexOffset;
            }
            else
            {
                startIndex = pointCount - startIndexOffset - 1;
                endIndex = pointCount - endIndexOffset - 1;
            }

            // Make sure start and end index are within the range of the series points.
            startIndex = Math.Min(startIndex, pointCount - 1);
            startIndex = Math.Max(startIndex, 0);
            endIndex = Math.Min(endIndex, pointCount - 1);
            endIndex = Math.Max(endIndex, 0);

            if (chart1.Series.IndexOf(TREND) == -1)
            {
                chart1.Series.Add(TREND);
                chart1.Series[TREND].IsXValueIndexed = true;
                chart1.Series[TREND].ChartType = MSChart.SeriesChartType.Line;
                chart1.Series[TREND].BorderWidth = 3;
                chart1.Series[TREND].Color = Color.Red;
            }
            else
            {
                chart1.Series[TREND].Points.Clear();
            }

            //chart1.DataManipulator.IsStartFromFirst = true;

            chart1.Series[TREND].ChartArea = "ChartAreaBottom";

            if (enable == true) EnableOneBottomSeries(chart1, TREND);

            // Line of best fit is linear
            string typeRegression = "Linear";//"Exponential";//
            // The number of days for Forecasting
            string forecasting = "1";
            // Show Error as a range chart.
            string error = "false";
            // Show Forecasting Error as a range chart.
            string forecastingError = "false";
            // Formula parameters
            string parameterStr = typeRegression + ',' + forecasting + ',' + error + ',' + forecastingError;

            SeriesFilter filter = SeriesFilter.Create("Filtered", chart1, "candlestickSeries");
            filter.FilterSeriesRange(startIndex, endIndex);
            filter.Series.Sort(MSChart.PointSortOrder.Ascending, "X");

            //chart1.Series["candlestickSeries"].Sort(MSChart.PointSortOrder.Ascending, "X");
            // Create Forecasting Series.
            //chart1.DataManipulator.FinancialFormula(MSChart.FinancialFormula.Forecasting, parameterStr, chart1.Series["candlestickSeries"], chart1.Series[TREND]);
            chart1.DataManipulator.FinancialFormula(MSChart.FinancialFormula.Forecasting, parameterStr, "Filtered:Y", TREND + ":Y");

            // Remove the last value from the TrendLine series (it adds one value to the input range for the forecast), and
            // match up the X values from the input series.
            chart1.Series[TREND].RemoveLastPoint();
            for (int i = 0; i < filter.Series.Points.Count; i++)
            {
                chart1.Series[TREND].Points[i].XValue = filter.Series.Points[i].XValue;
            }

            double scaleMax = Math.Round(Math.Max(chart1.Series[TREND].GetFirstPoint().YValues[0], chart1.Series[TREND].GetLastPoint().YValues[0]), 2);
            double scaleMin = Math.Round(Math.Min(chart1.Series[TREND].GetFirstPoint().YValues[0], chart1.Series[TREND].GetLastPoint().YValues[0]), 2);

            // At this point, we have the first and last points of the trend line, so we can calculate slope.
            double yStart = chart1.Series[TREND].GetFirstPoint().YValues[0];            
            double yEnd = chart1.Series[TREND].GetLastPoint().YValues[0];
            double slope = (yEnd - yStart) / chart1.Series[TREND].Points.Count;
            double theta = Math.Round(Math.Atan(slope) * 180 / Math.PI, 2);
            Slope = theta;
            if (SlopeChanged != null) SlopeChanged(theta);

            // Now add any missing data points so the trend series matches up with the candlestickSeries.
            double startX = chart1.Series[TREND].GetFirstPoint().XValue;
            double endX = chart1.Series[TREND].GetLastPoint().XValue;
            for (int i = 0; i < chart1.Series["candlestickSeries"].Points.Count; i++)
            {
                MSChart.DataPoint emptyDP = new MSChart.DataPoint();
                emptyDP.IsEmpty = true;

                if (chart1.Series["candlestickSeries"].Points[i].XValue < startX)
                {
                    chart1.Series[TREND].InsertFirst(emptyDP);
                }
                else if (chart1.Series["candlestickSeries"].Points[i].XValue > endX)
                {
                    chart1.Series[TREND].InsertLast(emptyDP);   
                }
            }

            // And finally, match up the X values in trend series to those in the candlestickSeries.
            for (int i = 0; i < chart1.Series["candlestickSeries"].Points.Count; i++)
            {
                chart1.Series[TREND].Points[i].XValue = chart1.Series["candlestickSeries"].Points[i].XValue;
            }

            // Update the axes scale for this indicator.
            chart1.ChartAreas["ChartAreaBottom"].AxisY.Minimum = scaleMin;
            chart1.ChartAreas["ChartAreaBottom"].AxisY.Maximum = scaleMax;
        }

    } // class
} // namespace
