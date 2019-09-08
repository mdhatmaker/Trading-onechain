using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MSChart = System.Windows.Forms.DataVisualization.Charting;
using EZAPI.Toolbox.DataStructures;

namespace EZAPI.Framework.Chart.Indicators
{
    public class TripleExponentialMovingAverage : Indicator
    {

        public TripleExponentialMovingAverage(string name = "Triple Exponential Moving Average", string description = "", string moreInfoWebPage = "")
            : base(name)
        {
            this.description = description;
            this.moreInfoWebPage = moreInfoWebPage;
            this.indicatorType = IndicatorChartArea.MainChart;

            // If we are passed a blank description or web page, then use our default values.
            // If 'null' is passed to the constructor for either of those values, they stay null (in case the user of this class doesn't want them displayed).
            if (description == "")
                this.description = "The triple exponential moving average formula is useful for eliminating short and insignificant cycles in the data. It smoothes the data three times using the Exponential Moving Average Formula, and then calculates the rate of change in the moving average based on the result for the previous day.";
            if (moreInfoWebPage == "")
                this.moreInfoWebPage = "http://www.chart-formations.com/indicators/trix.aspx?cat=momentum";

            // ADD PARAMETERS HERE
            AddParameter(new IndicatorParameter<int>("Period", "Period for calculating the triple exponential moving average.", 15));

            this["Color"] = Color.DimGray;
            this["Line Style"] = zChartLineStyle.Dot;
        }

        /// <summary>
        /// Override the Draw method to display the indicator.
        /// </summary>
        public override void Draw(MSChart.Chart chart1, bool enable = true)
        {
            string INDICATOR = "TripleExponentialMovingAverage";

            int period = (int)parameters["Period"].Value;
            MSChart.ChartDashStyle lineStyle = Parse.ParseEnum<MSChart.ChartDashStyle>(parameters["Line Style"].Value.ToString());
            Color indicatorColor = (Color)parameters["Color"].Value;

            if (chart1.Series.IndexOf(INDICATOR) == -1)
            {
                chart1.Series.Add(INDICATOR);
                chart1.Series[INDICATOR].IsXValueIndexed = true;
                chart1.Series[INDICATOR].ChartType = MSChart.SeriesChartType.Line;
            }
            else
            {
                chart1.Series[INDICATOR].Points.Clear();
            }

            chart1.Series[INDICATOR].BorderWidth = 2;
            chart1.Series[INDICATOR].BorderDashStyle = lineStyle;
            chart1.Series[INDICATOR].Color = indicatorColor;

            string formulaParameters = string.Format("{0}", period);

            chart1.DataManipulator.IsStartFromFirst = true;
            chart1.DataManipulator.FinancialFormula(MSChart.FinancialFormula.TripleExponentialMovingAverage, formulaParameters, "candlestickSeries:Y3", INDICATOR + ":Y");

            AlignChartData(chart1.Series["candlestickSeries"], chart1.Series[INDICATOR]);

            RescaleAxes(chart1, "ChartAreaMain");
        }

    } // class
} // namespace
