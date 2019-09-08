using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MSChart = System.Windows.Forms.DataVisualization.Charting;
using EZAPI.Toolbox.DataStructures;

namespace EZAPI.Framework.Chart.Indicators
{
    public class TriangularMovingAverage : Indicator
    {

        public TriangularMovingAverage(string name = "Triangular Moving Average", string description = "", string moreInfoWebPage = "")
            : base(name)
        {
            this.description = description;
            this.moreInfoWebPage = moreInfoWebPage;
            this.indicatorType = IndicatorChartArea.MainChart;

            // If we are passed a blank description or web page, then use our default values.
            // If 'null' is passed to the constructor for either of those values, they stay null (in case the user of this class doesn't want them displayed).
            if (description == "")
                this.description = "The triangular moving average formula takes a simple moving average of data and applies a simple moving average on the first moving average. It is a lagging indicator, and is always behind the price. The triangular moving average gives the most weight to the middle portion of the data.";
            if (moreInfoWebPage == "")
                this.moreInfoWebPage = "http://www.blastchart.com/Community/IndicatorGuide/Overlays/TriangularMovingAverage.aspx";

            // ADD PARAMETERS HERE
            AddParameter(new IndicatorParameter<int>("Period", "Period for calculating the triangular moving average.", 20));

            this["Color"] = Color.SlateGray;
            this["Line Style"] = zChartLineStyle.Dash;
        }

        /// <summary>
        /// Override the Draw method to display the indicator.
        /// </summary>
        public override void Draw(MSChart.Chart chart1, bool enable = true)
        {
            string INDICATOR = "TriangularMovingAverage";

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
            chart1.DataManipulator.FinancialFormula(MSChart.FinancialFormula.TriangularMovingAverage, formulaParameters, "candlestickSeries:Y3", INDICATOR + ":Y");

            AlignChartData(chart1.Series["candlestickSeries"], chart1.Series[INDICATOR]);

            RescaleAxes(chart1, "ChartAreaMain");
        }

    } // class
} // namespace
