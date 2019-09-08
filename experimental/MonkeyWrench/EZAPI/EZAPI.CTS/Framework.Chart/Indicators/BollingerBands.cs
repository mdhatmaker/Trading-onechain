using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MSChart = System.Windows.Forms.DataVisualization.Charting;
using EZAPI.Toolbox.DataStructures;

namespace EZAPI.Framework.Chart.Indicators
{
    public class BollingerBands : Indicator
    {

        public BollingerBands(string name = "Bollinger Bands", string description = "", string moreInfoWebPage = "")
            : base(name)
        {
            this.description = description;
            this.moreInfoWebPage = moreInfoWebPage;
            this.indicatorType = IndicatorChartArea.MainChart;

            // If we are passed a blank description or web page, then use our default values.
            // If 'null' is passed to the constructor for either of those values, they stay null (in case the user of this class doesn't want them displayed).
            if (description == "")
                this.description = "The Bollinger Bands formula calculates the standard deviation above and below a simple moving average of the data. Since standard deviation is a measure of volatility, a large standard deviation indicates a volatile market, and a smaller standard deviation indicates a calmer market.";
            if (moreInfoWebPage == "")
                this.moreInfoWebPage = "http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:bollinger_bands";

            // ADD PARAMETERS HERE
            AddParameter(new IndicatorParameter<int>("MAPeriod", "Length (in bars) of the period used to calculate the moving average for the Bollinger Bands.", 20));
            AddParameter(new IndicatorParameter<double>("StdDev", "Number of standard deviations for calculating the upper and lower bands.", 2.0));

            this["Color"] = Color.Green;
            this["Line Style"] = zChartLineStyle.Solid;
        }

        /// <summary>
        /// Override the Draw method to display the indicator.
        /// </summary>
        public override void Draw(MSChart.Chart chart1, bool enable = true)
        {
            string BBU = "BollingerBand_Upper";
            string BBL = "BollingerBand_Lower";

            int maPeriod = (int)parameters["MAPeriod"].Value;
            double stdDev = (double)parameters["StdDev"].Value;
            MSChart.ChartDashStyle lineStyle = Parse.ParseEnum<MSChart.ChartDashStyle>(parameters["Line Style"].Value.ToString());
            Color indicatorColor = (Color)parameters["Color"].Value;

            // Upper Band
            if (chart1.Series.IndexOf(BBU) == -1)
            {
                chart1.Series.Add(BBU);
                chart1.Series[BBU].IsXValueIndexed = true;
                chart1.Series[BBU].ChartType = MSChart.SeriesChartType.Line;
            }
            else
            {
                chart1.Series[BBU].Points.Clear();
            }

            chart1.Series[BBU].BorderWidth = 2;
            chart1.Series[BBU].BorderDashStyle = lineStyle;
            chart1.Series[BBU].Color = indicatorColor;

            // Lower Band
            if (chart1.Series.IndexOf(BBL) == -1)
            {
                chart1.Series.Add(BBL);
                chart1.Series[BBL].IsXValueIndexed = true;
                chart1.Series[BBL].ChartType = MSChart.SeriesChartType.Line;
            }
            else
            {
                chart1.Series[BBL].Points.Clear();
            }

            chart1.Series[BBL].BorderWidth = 2;
            chart1.Series[BBL].BorderDashStyle = lineStyle;
            chart1.Series[BBL].Color = indicatorColor;

            string formulaParameters = string.Format("{0},{1}", maPeriod, stdDev);

            chart1.DataManipulator.IsStartFromFirst = true;
            chart1.DataManipulator.FinancialFormula(MSChart.FinancialFormula.BollingerBands, formulaParameters, "candlestickSeries:Y3", BBU + ":Y," + BBL + ":Y");

            AlignChartData(chart1.Series["candlestickSeries"], chart1.Series[BBU]);
            AlignChartData(chart1.Series["candlestickSeries"], chart1.Series[BBL]);

            RescaleAxes(chart1, "ChartAreaMain");
        }

    } // class
} // namespace
