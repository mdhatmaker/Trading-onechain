using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MSChart = System.Windows.Forms.DataVisualization.Charting;
using EZAPI.Toolbox.DataStructures;

namespace EZAPI.Framework.Chart.Indicators
{
    public class StochasticIndicator : Indicator
    {

        public StochasticIndicator(string name = "Stochastic Indicator", string description = "", string moreInfoWebPage = "")
            : base(name)
        {
            this.description = description;
            this.moreInfoWebPage = moreInfoWebPage;
            this.indicatorType = IndicatorChartArea.BottomChart;

            // If we are passed a blank description or web page, then use our default values.
            // If 'null' is passed to the constructor for either of those values, they stay null (in case the user of this class doesn't want them displayed).
            if (description == "")
                this.description = "The stochastic indicator formula calculates the simple stochastic indicator (%K) and the smoothed stochastic indicator (%D). %D is a moving average of %K. The output is a percentage. A value more than 80% indicates that the current price is close to the price high, and a value less than 20% indicates that the current price is close to the price low. The stochastic indicator is an indicator of upward and downward market trends.";
            if (moreInfoWebPage == "")
                this.moreInfoWebPage = "http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:stochastic_oscillator";

            // ADD PARAMETERS HERE
            AddParameter(new IndicatorParameter<int>("PeriodK", "Period for calculating %K.", 10));
            AddParameter(new IndicatorParameter<int>("PeriodD", "Period for calculating %D.", 10));

            this["Color"] = Color.Navy;
            this["Line Style"] = zChartLineStyle.Solid;
        }

        /// <summary>
        /// Override the Draw method to display the indicator.
        /// </summary>
        public override void Draw(MSChart.Chart chart1, bool enable = true)
        {
            int periodK = (int)parameters["PeriodK"].Value;
            int periodD = (int)parameters["PeriodD"].Value;
            MSChart.ChartDashStyle lineStyle = Parse.ParseEnum<MSChart.ChartDashStyle>(parameters["Line Style"].Value.ToString());
            Color indicatorColor = (Color)parameters["Color"].Value;

            string INDICATOR = "StochasticIndicatorSimple";
            string SMOOTHED = "StochasticIndicatorSmoothed";

            if (chart1.Series.IndexOf(INDICATOR) == -1)
            {
                chart1.Series.Add(INDICATOR);
                chart1.Series[INDICATOR].IsXValueIndexed = true;
                chart1.Series[INDICATOR].ChartType = MSChart.SeriesChartType.Line;
                chart1.Series.Add(SMOOTHED);
                chart1.Series[SMOOTHED].IsXValueIndexed = true;
                chart1.Series[SMOOTHED].ChartType = MSChart.SeriesChartType.Line;
            }
            else
            {
                chart1.Series[INDICATOR].Points.Clear();
                chart1.Series[SMOOTHED].Points.Clear();
            }
            chart1.Series[INDICATOR].BorderWidth = 2;
            chart1.Series[INDICATOR].BorderDashStyle = lineStyle;
            chart1.Series[INDICATOR].Color = indicatorColor;
            chart1.Series[SMOOTHED].BorderWidth = 2;
            chart1.Series[SMOOTHED].BorderDashStyle = MSChart.ChartDashStyle.Dot;
            chart1.Series[SMOOTHED].Color = indicatorColor;

            chart1.Series[INDICATOR].ChartArea = "ChartAreaBottom";
            chart1.Series[SMOOTHED].ChartArea = "ChartAreaBottom";

            if (enable == true) EnableOneBottomSeries(chart1, "StochasticIndicator");

            string formulaParameters = string.Format("{0},{1}", periodK, periodD);

            chart1.DataManipulator.IsStartFromFirst = true;
            // OPEN=candlestickSeries:Y HIGH=candlestickSeries:Y1 LOW=candlestickSeries:Y2 CLOSE=candlestickSeries:Y3 VOLUME=candlestickVolumeSeries:Y
            // Inputs to this indicator: High, Low, Close
            chart1.DataManipulator.FinancialFormula(MSChart.FinancialFormula.StochasticIndicator, formulaParameters, "candlestickSeries:Y1,candlestickSeries:Y2,candlestickSeries:Y3", INDICATOR + ":Y," + SMOOTHED + ":Y");

            AlignChartData(chart1.Series["candlestickSeries"], chart1.Series[INDICATOR]);
            AlignChartData(chart1.Series["candlestickSeries"], chart1.Series[SMOOTHED]);

            RescaleAxes(chart1);
        }

    } // class
} // namespace
