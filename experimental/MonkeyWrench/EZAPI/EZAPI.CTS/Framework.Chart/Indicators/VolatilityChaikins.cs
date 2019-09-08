using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MSChart = System.Windows.Forms.DataVisualization.Charting;
using EZAPI.Toolbox.DataStructures;

namespace EZAPI.Framework.Chart.Indicators
{
    public class VolatilityChaikins : Indicator
    {

        public VolatilityChaikins(string name = "Volatility Chaikins", string description = "", string moreInfoWebPage = "")
            : base(name)
        {
            this.description = description;
            this.moreInfoWebPage = moreInfoWebPage;
            this.indicatorType = IndicatorChartArea.BottomChart;

            // If we are passed a blank description or web page, then use our default values.
            // If 'null' is passed to the constructor for either of those values, they stay null (in case the user of this class doesn't want them displayed).
            if (description == "")
                this.description = "The Volatility Chaikins formula calculates the exponential moving average of the difference between daily high and low prices, then calculates the rate of change of the exponential moving average.";
            if (moreInfoWebPage == "")
                this.moreInfoWebPage = "http://www.incrediblecharts.com/indicators/chaikin_volatility.php";

            // ADD PARAMETERS HERE
            AddParameter(new IndicatorParameter<int>("PeriodEMA", "Period for calculating the exponential moving average of the difference between the high and low prices.", 10));
            AddParameter(new IndicatorParameter<int>("PeriodROC", "Period for calculating the rate of change.", 10));

            this["Color"] = Color.Navy;
            this["Line Style"] = zChartLineStyle.Solid;
        }

        /// <summary>
        /// Override the Draw method to display the indicator.
        /// </summary>
        public override void Draw(MSChart.Chart chart1, bool enable = true)
        {
            int periodEMA = (int)parameters["PeriodEMA"].Value;
            int periodROC = (int)parameters["PeriodROC"].Value;
            MSChart.ChartDashStyle lineStyle = Parse.ParseEnum<MSChart.ChartDashStyle>(parameters["Line Style"].Value.ToString());
            Color indicatorColor = (Color)parameters["Color"].Value;

            string INDICATOR = "VolatilityChaikins";

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

            chart1.Series[INDICATOR].ChartArea = "ChartAreaBottom";

            if (enable == true) EnableOneBottomSeries(chart1, INDICATOR);

            string formulaParameters = string.Format("{0},{1}", periodEMA, periodROC);

            chart1.DataManipulator.IsStartFromFirst = true;
            // OPEN=candlestickSeries:Y HIGH=candlestickSeries:Y1 LOW=candlestickSeries:Y2 CLOSE=candlestickSeries:Y3 VOLUME=candlestickVolumeSeries:Y
            // Inputs to this indicator: High, Low
            chart1.DataManipulator.FinancialFormula(MSChart.FinancialFormula.VolatilityChaikins, formulaParameters, "candlestickSeries:Y1,candlestickSeries:Y2", INDICATOR + ":Y");

            AlignChartData(chart1.Series["candlestickSeries"], chart1.Series[INDICATOR]);

            RescaleAxes(chart1);
        }

    } // class
} // namespace
