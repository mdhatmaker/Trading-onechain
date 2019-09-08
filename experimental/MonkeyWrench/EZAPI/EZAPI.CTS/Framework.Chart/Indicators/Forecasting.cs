using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MSChart = System.Windows.Forms.DataVisualization.Charting;
using EZAPI.Toolbox.DataStructures;

namespace EZAPI.Framework.Chart.Indicators
{
    public class Forecasting : Indicator
    {

        public Forecasting(string name = "Forecasting", string description = "", string moreInfoWebPage = "")
            : base(name)
        {
            this.description = description;
            this.moreInfoWebPage = moreInfoWebPage;
            this.indicatorType = IndicatorChartArea.MainChart;

            // If we are passed a blank description or web page, then use our default values.
            // If 'null' is passed to the constructor for either of those values, they stay null (in case the user of this class doesn't want them displayed).
            if (description == "")
                this.description = "The forecasting formula attempts to fit the historical data to a regression function and forecast future values of the data best on the best fit.";
            if (moreInfoWebPage == "")
                this.moreInfoWebPage = null;

            // ADD PARAMETERS HERE
            AddParameter(new IndicatorParameter<int>("RegressionType", "Indicates polynomial regression of a specific degree: 2=linear", 2));
            AddParameter(new IndicatorParameter<int>("Period", "Length (in bars) of forecasting period. The formula predicts data for this period into the future.", 10));
            AddParameter(new IndicatorParameter<bool>("ApproxError", "Whether to output the approximation error. If set to false, output error series contain no data for the corresponding historical data.", true));
            AddParameter(new IndicatorParameter<bool>("ForecastError", "Whether to output the forecasting error. If set to false, output error series contain the approximation error for all predicted data points if ApproxError is true.", true));

            this["Color"] = Color.Navy;
            this["Line Style"] = zChartLineStyle.Solid;
        }

        /// <summary>
        /// Override the Draw method to display the indicator.
        /// </summary>
        public override void Draw(MSChart.Chart chart1, bool enable = true)
        {
            int regressionType = (int)parameters["RegressionType"].Value;
            int period = (int)parameters["Period"].Value;
            bool approxError = (bool)parameters["ApproxError"].Value;
            bool forecastError = (bool)parameters["ForecastError"].Value;

            MSChart.ChartDashStyle lineStyle = Parse.ParseEnum<MSChart.ChartDashStyle>(parameters["Line Style"].Value.ToString());
            Color indicatorColor = (Color)parameters["Color"].Value;

            string INDICATOR = "Forecasting";
            string FUE = "Forecasting Upper Error";
            string FLE = "Forecasting Lower Error";

            if (chart1.Series.IndexOf(INDICATOR) == -1)
            {
                chart1.Series.Add(INDICATOR);
                chart1.Series[INDICATOR].IsXValueIndexed = true;
                chart1.Series[INDICATOR].ChartType = MSChart.SeriesChartType.Line;
                chart1.Series.Add(FUE);
                chart1.Series[FUE].IsXValueIndexed = true;
                chart1.Series[FUE].ChartType = MSChart.SeriesChartType.Line;
                chart1.Series.Add(FLE);
                chart1.Series[FLE].IsXValueIndexed = true;
                chart1.Series[FLE].ChartType = MSChart.SeriesChartType.Line;
            }
            else
            {
                chart1.Series[INDICATOR].Points.Clear();
                chart1.Series[FUE].Points.Clear();
                chart1.Series[FLE].Points.Clear();
            }
            chart1.Series[INDICATOR].BorderWidth = 2;
            chart1.Series[INDICATOR].BorderDashStyle = lineStyle;
            chart1.Series[INDICATOR].Color = indicatorColor;
            chart1.Series[FUE].BorderWidth = 1;
            chart1.Series[FUE].BorderDashStyle = MSChart.ChartDashStyle.Dot;
            chart1.Series[FUE].Color = indicatorColor;
            chart1.Series[FLE].BorderWidth = 1;
            chart1.Series[FLE].BorderDashStyle = MSChart.ChartDashStyle.Dot;
            chart1.Series[FLE].Color = indicatorColor;

            string formulaParameters = string.Format("{0},{1},{2},{3}", regressionType, period, approxError, forecastError);

            chart1.DataManipulator.IsStartFromFirst = true;
            // OPEN=candlestickSeries:Y HIGH=candlestickSeries:Y1 LOW=candlestickSeries:Y2 CLOSE=candlestickSeries:Y3 VOLUME=candlestickVolumeSeries:Y
            // Inputs to this indicator: Close
            chart1.DataManipulator.FinancialFormula(MSChart.FinancialFormula.Forecasting, formulaParameters, "candlestickSeries:Y3", INDICATOR + ":Y," + FUE + ":Y," + FLE + ":Y");
        }

    } // class
} // namespace
