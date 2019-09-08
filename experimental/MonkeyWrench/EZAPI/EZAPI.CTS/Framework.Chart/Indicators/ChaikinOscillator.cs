using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MSChart = System.Windows.Forms.DataVisualization.Charting;
using EZAPI.Toolbox.DataStructures;

namespace EZAPI.Framework.Chart.Indicators
{
    public class ChaikinOscillator : Indicator
    {
        public ChaikinOscillator(string name = "Chaikin Oscillator", string description = "", string moreInfoWebPage = "") : base(name)
        {
            this.description = description;
            this.moreInfoWebPage = moreInfoWebPage;
            this.indicatorType = IndicatorChartArea.BottomChart;

            // If we are passed a blank description or web page, then use our default values.
            // If 'null' is passed to the constructor for either of those values, they stay null (in case the user of this class doesn't want them displayed).
            if (description == "")
                this.description = "The Chaikin Oscillator formula is useful for monitoring volume flow in a market. It applies the Accumulation Distribution Formula on the input, calculates the exponential moving average of the result for a short period and a long period, and then outputs the difference between the two. This formula should be used together with the Envelopes Formula.";
            if (moreInfoWebPage == "")
                this.moreInfoWebPage = "http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:chaikin_oscillator";

            AddParameter(new IndicatorParameter<int>("PeriodShort", "Length (in bars) for calculating the short period exponential moving average.", 3));
            AddParameter(new IndicatorParameter<int>("PeriodLong", "Length (in bars) for calculating the long period exponential moving average.", 10));
            this["Color"] = Color.Navy;
            this["Line Style"] = zChartLineStyle.Solid;
        }

        public override void Draw(MSChart.Chart chart1, bool enable = true)
        {
            int periodShort = (int)parameters["PeriodShort"].Value;
            int periodLong = (int)parameters["PeriodLong"].Value;
            MSChart.ChartDashStyle lineStyle = Parse.ParseEnum<MSChart.ChartDashStyle>(parameters["Line Style"].Value.ToString());
            Color indicatorColor = (Color)parameters["Color"].Value;

            string INDICATOR = "ChaikinOscillator";

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

            string formulaParameters = string.Format("{0},{1}", periodShort, periodLong);

            chart1.DataManipulator.IsStartFromFirst = true;
            // OPEN=candlestickSeries:Y HIGH=candlestickSeries:Y1 LOW=candlestickSeries:Y2 CLOSE=candlestickSeries:Y3 VOLUME=candlestickVolumeSeries:Y
            // Inputs to this indicator: High, Low, Close, Volume
            chart1.DataManipulator.FinancialFormula(MSChart.FinancialFormula.ChaikinOscillator, formulaParameters, "candlestickSeries:Y1,candlestickSeries:Y2,candlestickSeries:Y3,candlestickVolumeSeries:Y", INDICATOR + ":Y");

            AlignChartData(chart1.Series["candlestickSeries"], chart1.Series[INDICATOR]);

            RescaleAxes(chart1);
        }
    } // class
} // namespace
