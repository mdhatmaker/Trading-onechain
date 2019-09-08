using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MSChart = System.Windows.Forms.DataVisualization.Charting;
using EZAPI.Toolbox.DataStructures;

namespace EZAPI.Framework.Chart.Indicators
{
    public class ExponentialMovingAverage : Indicator
    {

        public ExponentialMovingAverage(string name = "Exponential Moving Average", string description = "", string moreInfoWebPage = "")
            : base(name)
        {
            this.description = description;
            this.moreInfoWebPage = moreInfoWebPage;
            this.indicatorType = IndicatorChartArea.MainChart;

            // If we are passed a blank description or web page, then use our default values.
            // If 'null' is passed to the constructor for either of those values, they stay null (in case the user of this class doesn't want them displayed).
            if (description == "")
                this.description = "The exponential moving average formula is a moving average of data that gives more weight to the more recent data in the period and less weight to the older data in the period. This formula produces a moving average that follows the market trend much more quickly than the Weighted Moving Average Formula. This formula smoothes a data series. This makes analyzing volatile data easier.";
            if (moreInfoWebPage == "")
                this.moreInfoWebPage = "http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:moving_averages";

            // ADD PARAMETERS HERE
            AddParameter(new IndicatorParameter<int>("EMAPeriod", "Length (in bars) of the exponential moving average.", 10));

            this["Color"] = Color.DarkGray;
            this["Line Style"] = zChartLineStyle.Dot;
        }

        /// <summary>
        /// Override the Draw method to display the indicator.
        /// </summary>
        public override void Draw(MSChart.Chart chart1, bool enable = true)
        {
            string EMA = "ExponentialMovingAverage";

            int emaPeriod = (int)parameters["EMAPeriod"].Value;
            MSChart.ChartDashStyle lineStyle = Parse.ParseEnum<MSChart.ChartDashStyle>(parameters["Line Style"].Value.ToString());
            Color indicatorColor = (Color)parameters["Color"].Value;

            if (chart1.Series.IndexOf(EMA) == -1)
            {
                chart1.Series.Add(EMA);
                chart1.Series[EMA].IsXValueIndexed = true;
                chart1.Series[EMA].ChartType = MSChart.SeriesChartType.Line;
            }
            else
            {
                chart1.Series[EMA].Points.Clear();
            }

            chart1.Series[EMA].BorderWidth = 2;
            chart1.Series[EMA].BorderDashStyle = lineStyle;
            chart1.Series[EMA].Color = indicatorColor;

            string emaPeriodStr = emaPeriod.ToString();

            chart1.DataManipulator.IsStartFromFirst = true;
            chart1.DataManipulator.FinancialFormula(MSChart.FinancialFormula.ExponentialMovingAverage, emaPeriodStr, "candlestickSeries:Y3", EMA + ":Y");

            AlignChartData(chart1.Series["candlestickSeries"], chart1.Series[EMA]);

            RescaleAxes(chart1, "ChartAreaMain");
        }

    } // class
} // namespace
