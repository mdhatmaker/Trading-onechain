using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MSChart = System.Windows.Forms.DataVisualization.Charting;
using EZAPI.Toolbox.DataStructures;

namespace EZAPI.Framework.Chart.Indicators
{
    public class Envelopes : Indicator
    {

        public Envelopes(string name = "Envelopes", string description = "", string moreInfoWebPage = "")
            : base(name)
        {
            this.description = description;
            this.moreInfoWebPage = moreInfoWebPage;
            this.indicatorType = IndicatorChartArea.MainChart;

            // If we are passed a blank description or web page, then use our default values.
            // If 'null' is passed to the constructor for either of those values, they stay null (in case the user of this class doesn't want them displayed).
            if (description == "")
                this.description = "The envelopes formula calculates 'envelopes' above and below a moving average using a specified percentage as the shift. The envelopes indicator is used to create signals for buying and selling. You can specify the percentage the formula uses to calculate the envelopes.";
            if (moreInfoWebPage == "")
                this.moreInfoWebPage = "http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:moving_average_envel";

            // ADD PARAMETERS HERE
            AddParameter(new IndicatorParameter<int>("MAPeriod", "Length (in bars) of the period used to calculate the moving average for the Bollinger Bands.", 20));
            AddParameter(new IndicatorParameter<double>("Shift", "Percentage used to shift the upper and lower envelopes from the moving average.", 4.0));

            this["Color"] = Color.LightBlue;
            this["Line Style"] = zChartLineStyle.Solid;
        }

        /// <summary>
        /// Override the Draw method to display the indicator.
        /// </summary>
        public override void Draw(MSChart.Chart chart1, bool enable = true)
        {
            string EU = "Envelopes_Upper";
            string EL = "Envelopes_Lower";

            int maPeriod = (int)parameters["MAPeriod"].Value;
            double shift = (double)parameters["Shift"].Value;
            MSChart.ChartDashStyle lineStyle = Parse.ParseEnum<MSChart.ChartDashStyle>(parameters["Line Style"].Value.ToString());
            Color indicatorColor = (Color)parameters["Color"].Value;

            // Upper Band
            if (chart1.Series.IndexOf(EU) == -1)
            {
                chart1.Series.Add(EU);
                chart1.Series[EU].IsXValueIndexed = true;
                chart1.Series[EU].ChartType = MSChart.SeriesChartType.Line;
            }
            else
            {
                chart1.Series[EU].Points.Clear();
            }

            chart1.Series[EU].BorderWidth = 2;
            chart1.Series[EU].BorderDashStyle = lineStyle;
            chart1.Series[EU].Color = indicatorColor;

            // Lower Band
            if (chart1.Series.IndexOf(EL) == -1)
            {
                chart1.Series.Add(EL);
                chart1.Series[EL].IsXValueIndexed = true;
                chart1.Series[EL].ChartType = MSChart.SeriesChartType.Line;
            }
            else
            {
                chart1.Series[EL].Points.Clear();
            }

            chart1.Series[EL].BorderWidth = 2;
            chart1.Series[EL].BorderDashStyle = lineStyle;
            chart1.Series[EL].Color = indicatorColor;

            string formulaParameters = string.Format("{0},{1}", maPeriod, shift);

            chart1.DataManipulator.IsStartFromFirst = true;
            chart1.DataManipulator.FinancialFormula(MSChart.FinancialFormula.Envelopes, formulaParameters, "candlestickSeries:Y3", EU + ":Y," + EL + ":Y");

            AlignChartData(chart1.Series["candlestickSeries"], chart1.Series[EL]);
            AlignChartData(chart1.Series["candlestickSeries"], chart1.Series[EU]);

            RescaleAxes(chart1, "ChartAreaMain");
        }

    } // class
} // namespace
