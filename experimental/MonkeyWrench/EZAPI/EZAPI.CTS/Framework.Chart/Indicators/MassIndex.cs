using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MSChart = System.Windows.Forms.DataVisualization.Charting;
using EZAPI.Toolbox.DataStructures;

namespace EZAPI.Framework.Chart.Indicators
{
    public class MassIndex : Indicator
    {

        public MassIndex(string name = "Mass Index", string description = "", string moreInfoWebPage = "")
            : base(name)
        {
            this.description = description;
            this.moreInfoWebPage = moreInfoWebPage;
            this.indicatorType = IndicatorChartArea.BottomChart;

            // If we are passed a blank description or web page, then use our default values.
            // If 'null' is passed to the constructor for either of those values, they stay null (in case the user of this class doesn't want them displayed).
            if (description == "")
                this.description = "The mass index formula predicts trend reversals by calculating the range between high and low prices for each period. A bulge in the index line signals a possible trend reversal. You can use a 9-day Exponential Moving Average Formula to determine whether the bulge is a buy or sell signal.";
            if (moreInfoWebPage == "")
                this.moreInfoWebPage = "http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:mass_index";

            // ADD PARAMETERS HERE
            AddParameter(new IndicatorParameter<int>("PeriodA", "Length (in bars) for period of accumulation.", 25));
            AddParameter(new IndicatorParameter<int>("PeriodEMA", "Length (in bars) for period for calculating the exponential moving average for the mass index.", 9));

            this["Color"] = Color.Navy;
            this["Line Style"] = zChartLineStyle.Solid;
        }

        /// <summary>
        /// Override the Draw method to display the indicator.
        /// </summary>
        public override void Draw(MSChart.Chart chart1, bool enable = true)
        {
            int periodA = (int)parameters["PeriodA"].Value;
            int periodEMA = (int)parameters["PeriodEMA"].Value;
            MSChart.ChartDashStyle lineStyle = Parse.ParseEnum<MSChart.ChartDashStyle>(parameters["Line Style"].Value.ToString());
            Color indicatorColor = (Color)parameters["Color"].Value;

            string INDICATOR = "MassIndex";

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

            string formulaParameters = string.Format("{0},{1}", periodA, periodEMA);

            chart1.DataManipulator.IsStartFromFirst = true;
            // OPEN=candlestickSeries:Y HIGH=candlestickSeries:Y1 LOW=candlestickSeries:Y2 CLOSE=candlestickSeries:Y3 VOLUME=candlestickVolumeSeries:Y
            // Inputs to this indicator: High, Low
            chart1.DataManipulator.FinancialFormula(MSChart.FinancialFormula.MassIndex, formulaParameters, "candlestickSeries:Y1,candlestickSeries:Y2", INDICATOR + ":Y");

            AlignChartData(chart1.Series["candlestickSeries"], chart1.Series[INDICATOR]);

            RescaleAxes(chart1);
        }

    } // class
} // namespace
