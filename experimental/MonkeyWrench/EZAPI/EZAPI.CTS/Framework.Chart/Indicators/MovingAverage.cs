using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MSChart = System.Windows.Forms.DataVisualization.Charting;
using EZAPI.Toolbox.DataStructures;

namespace EZAPI.Framework.Chart.Indicators
{
    public class MovingAverage : Indicator
    {

        public MovingAverage(string name = "Moving Average", string description = "", string moreInfoWebPage = "")
            : base(name)
        {
            this.description = description;
            this.moreInfoWebPage = moreInfoWebPage;
            this.indicatorType = IndicatorChartArea.MainChart;

            // If we are passed a blank description or web page, then use our default values.
            // If 'null' is passed to the constructor for either of those values, they stay null (in case the user of this class doesn't want them displayed).
            if (description == "")
                this.description = "The simple moving average formula is takes the average of data over a period of time, and 'moves' the period across the data series one data point at a time. This formula smoothes a data series and makes analyzing volatile data easier.";
            if (moreInfoWebPage == "")
                this.moreInfoWebPage = "http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:moving_averages";

            // ADD PARAMETERS HERE
            AddParameter(new IndicatorParameter<int>("MAPeriod", "Length (in bars) of the moving average.", 10));

            this["Color"] = Color.Gray;
            this["Line Style"] = zChartLineStyle.Dash;
        }

        /// <summary>
        /// Override the Draw method to display the indicator.
        /// </summary>
        public override void Draw(MSChart.Chart chart1, bool enable = true)
        {
            string SMA = "MovingAverage";

            int maPeriod = (int) parameters["MAPeriod"].Value;
            MSChart.ChartDashStyle lineStyle = Parse.ParseEnum<MSChart.ChartDashStyle>(parameters["Line Style"].Value.ToString());
            Color indicatorColor = (Color)parameters["Color"].Value;

            if (chart1.Series.IndexOf(SMA) == -1)
            {
                chart1.Series.Add(SMA);
                chart1.Series[SMA].IsXValueIndexed = true;
                chart1.Series[SMA].ChartType = MSChart.SeriesChartType.Line;
            }
            else
            {
                chart1.Series[SMA].Points.Clear();
            }

            chart1.Series[SMA].BorderWidth = 2;
            chart1.Series[SMA].BorderDashStyle = lineStyle;
            chart1.Series[SMA].Color = indicatorColor;

            string maPeriodStr = maPeriod.ToString();

            chart1.DataManipulator.IsStartFromFirst = true;
            chart1.DataManipulator.FinancialFormula(MSChart.FinancialFormula.MovingAverage, maPeriodStr, "candlestickSeries:Y3", SMA + ":Y");

            AlignChartData(chart1.Series["candlestickSeries"], chart1.Series[SMA]);

            RescaleAxes(chart1, "ChartAreaMain");
        }

    } // class
} // namespace
