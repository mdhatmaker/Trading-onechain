using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MSChart = System.Windows.Forms.DataVisualization.Charting;
using EZAPI.Toolbox.DataStructures;

namespace EZAPI.Framework.Chart.Indicators
{
    public class KeltnerChannels : Indicator
    {
        public KeltnerChannels(string name = "Keltner Channel", string description = "", string moreInfoWebPage = "") : base(name)
        {
            this.description = description;
            this.moreInfoWebPage = moreInfoWebPage;
            this.indicatorType = IndicatorChartArea.MainChart;

            // If we are passed a blank description or web page, then use our default values.
            // If 'null' is passed to the constructor for either of those values, they stay null (in case the user of this class doesn't want them displayed).
            if (description == "")
                this.description = "Keltner Channels are volatility-based envelopes set above and below an exponential moving average. This indicator is similar to Bollinger Bands, which use the standard deviation to set the bands. Instead of using the standard deviation, Keltner Channels use the Average True Range (ATR) to set channel distance.";
            if (moreInfoWebPage == "")
                this.moreInfoWebPage = "http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:keltner_channels";

            // ADD PARAMETERS HERE
            AddParameter(new IndicatorParameter<int>("MAPeriod", "Length (in bars) of the moving average.", 10));
            AddParameter(new IndicatorParameter<int>("ATRPeriod", "Length (in bars) of the ATR to use in calculating channel width.", 10));
            AddParameter(new IndicatorParameter<double>("ATRMultiplier", "Multiplier of ATR factor to use in calculating channel width.", 2.0));

            this["Color"] = Color.Navy;
            this["Line Style"] = zChartLineStyle.Solid;

        }

        public override void Draw(MSChart.Chart chart1, bool enable = true)
        {
            int maPeriod = (int)parameters["MAPeriod"].Value;
            int atrPeriod = (int)parameters["ATRPeriod"].Value;
            double atrMultiplier = (double)parameters["ATRMultiplier"].Value;
            MSChart.ChartDashStyle lineStyle = Parse.ParseEnum<MSChart.ChartDashStyle>(parameters["Line Style"].Value.ToString());
            Color indicatorColor = (Color)parameters["Color"].Value;

            string MA = "MovingAverage";

            if (chart1.Series.IndexOf(MA) == -1)
            {
                chart1.Series.Add(MA);
                chart1.Series[MA].IsXValueIndexed = true;
                chart1.Series[MA].ChartType = MSChart.SeriesChartType.Line;
                chart1.Series[MA].BorderWidth = 2;
                chart1.Series[MA].BorderDashStyle = MSChart.ChartDashStyle.Dash;
                chart1.Series[MA].Color = Color.Gray;
            }
            else
            {
                chart1.Series[MA].Points.Clear();
            }

            string ATR = "ATR";

            if (chart1.Series.IndexOf(ATR) == -1)
            {
                chart1.Series.Add(ATR);
                chart1.Series[ATR].IsXValueIndexed = true;
                chart1.Series[ATR].ChartType = MSChart.SeriesChartType.Bar;
                chart1.Series[ATR].Enabled = false;
            }
            else
            {
                chart1.Series[ATR].Points.Clear();
            }

            string maPeriodStr = maPeriod.ToString();
            string atrPeriodStr = atrPeriod.ToString();

            chart1.DataManipulator.IsStartFromFirst = true;
            chart1.DataManipulator.FinancialFormula(MSChart.FinancialFormula.MovingAverage, maPeriodStr, "candlestickSeries:Y3", MA + ":Y");
            chart1.DataManipulator.IsStartFromFirst = true;
            chart1.DataManipulator.FinancialFormula(MSChart.FinancialFormula.AverageTrueRange, atrPeriodStr, "candlestickSeries:Y1,candlestickSeries:Y2,candlestickSeries:Y3", ATR + ":Y");

            string KC_LOWER = "KC_LOWER";

            if (chart1.Series.IndexOf(KC_LOWER) == -1)
            {
                chart1.Series.Add(KC_LOWER);
                chart1.Series[KC_LOWER].IsXValueIndexed = true;
                chart1.Series[KC_LOWER].ChartType = MSChart.SeriesChartType.Line;
            }
            else
            {
                chart1.Series[KC_LOWER].Points.Clear();
            }

            chart1.Series[KC_LOWER].BorderWidth = 2;
            chart1.Series[KC_LOWER].BorderDashStyle = lineStyle;
            chart1.Series[KC_LOWER].Color = indicatorColor;
            chart1.Series[KC_LOWER].Enabled = false;

            string KC_UPPER = "KC_UPPER";

            if (chart1.Series.IndexOf(KC_UPPER) == -1)
            {
                chart1.Series.Add(KC_UPPER);
                chart1.Series[KC_UPPER].IsXValueIndexed = true;
                chart1.Series[KC_UPPER].ChartType = MSChart.SeriesChartType.Line;
            }
            else
            {
                chart1.Series[KC_UPPER].Points.Clear();
            }

            chart1.Series[KC_UPPER].BorderWidth = 2;
            chart1.Series[KC_UPPER].BorderDashStyle = lineStyle;
            chart1.Series[KC_UPPER].Color = indicatorColor;
            chart1.Series[KC_UPPER].Enabled = false;

            int maPointCount = chart1.Series[MA].Points.Count;
            int atrPointCount = chart1.Series[ATR].Points.Count;
            int atrPointOffset = maPointCount - atrPointCount;

            for (int i = 0; i < maPointCount; i++)
            {
                double x = chart1.Series[MA].Points[i].XValue;
                double y = chart1.Series[MA].Points[i].YValues[0];
                if (i < atrPointOffset)
                {
                    chart1.Series[KC_LOWER].Points.Add(EmptyDP);
                    chart1.Series[KC_UPPER].Points.Add(EmptyDP);
                }
                else
                {
                    double atr = chart1.Series[ATR].Points[i - atrPointOffset].YValues[0];
                    chart1.Series[KC_LOWER].Points.AddXY(x, y - (atr * atrMultiplier));
                    chart1.Series[KC_UPPER].Points.AddXY(x, y + (atr * atrMultiplier));
                }
            }

            chart1.Series[KC_LOWER].Enabled = true;
            chart1.Series[KC_UPPER].Enabled = true;

            AlignChartData(chart1.Series["candlestickSeries"], chart1.Series[KC_UPPER]);
            AlignChartData(chart1.Series["candlestickSeries"], chart1.Series[KC_LOWER]);

            RescaleAxes(chart1);
        }

    } // class
} // namespace
