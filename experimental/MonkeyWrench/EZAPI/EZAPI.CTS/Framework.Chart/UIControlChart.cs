using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using EZAPI;
using EZAPI.Framework;
using EZAPI.Framework.Base;
using EZAPI.Framework.Chart.Indicators;
using EZAPI.Toolbox;
using EZAPI.Toolbox.Debug;

namespace EZAPI.Framework.Chart
{
    public partial class UIControlChart : UserControl
    {
        public event Action<IChartDataProvider> LoadingNewChart;
        public event Action<double> CurrentValueChanged;

        public string ChartDescriptorID { get { return chartDescriptor == null ? null : chartDescriptor.UniqueID; } }    // UniqueID for the ChartDescriptor associated with this chart
        public int BarIndex { get; private set; }           // BarIndex has zero for the "oldest" point in the chart and increases for future data points
        public int ReverseBarIndex { get; private set; }    // ReverseBarIndex has zero for the "current" bar in the chart and increases for older data points
        public string SelectedSeries { get; private set; }  // Name of the series that the user has selected (ex: "open", "KC_LOWER", "volume", etc)
        public ChartDescriptor ChartDescriptor { get { return chartDescriptor; } }
        public double CurrentValue { get; private set; }
        public bool BarSelectEnabled { get; set; }

        #region PRIVATE MEMBER VARIABLES
        private bool changingActiveChart = false;
        private APIMain api;
        private Indicator currentIndicator;
        private float originalMainChartHeight;
        private float originalBottomChartHeight;
        private UIFormChooseBarData chooseBarDataForm;
        private IChartDataProvider chartDataProvider;
        private ChartIndicatorsBottom bottomChartIndicators;
        private ChartIndicatorsMain mainChartIndicators;
        private ChartDescriptor chartDescriptor;
        private IndicatorMap activeIndicators;

        private DataPoint previousSelectedDataPoint = null;

        private int mouseX;
        private int mouseY;
        private Axis activeAxis;
        private bool leftButtonDown = false;
        private bool rightButtonDown = false;

        private Axis xAxis;
        #endregion

        public UIControlChart()
        {
            InitializeComponent();

            activeIndicators = new IndicatorMap();

            InitializeChart();

            this.MouseWheel += ChartDataForm_MouseWheel;

            foreach (string intervalName in Enum.GetNames(typeof(zChartInterval)))
            {
                comboChartInterval.Items.Add(intervalName);
            }
            comboChartInterval.SelectedIndex = 0;

            // Trigger the ActiveChartChanged method to populate the initial indicator list.
            ActiveChartChanged();           

            status.Text = "Select a time period and interval, and use the [Select market...] button to display a chart.";

            api = APIMain.Instance;
        }

        /// <summary>
        /// Initialize the chart for initial display (use ResetChart for loading subsequent instruments).
        /// </summary>
        void InitializeChart()
        {
            chart1.ChartAreas["ChartAreaMain"].AxisX.LabelStyle.Format = "ddd h:mmtt";
            chart1.ChartAreas["ChartAreaBottom"].AxisX.LabelStyle.Format = "M/d h:mmtt";

            Color gridLineColor = Color.LightGray;
            chart1.ChartAreas["ChartAreaMain"].AxisX.MajorGrid.LineColor = gridLineColor;
            chart1.ChartAreas["ChartAreaMain"].AxisY.MajorGrid.LineColor = gridLineColor;
            chart1.ChartAreas["ChartAreaBottom"].AxisX.MajorGrid.LineColor = gridLineColor;
            chart1.ChartAreas["ChartAreaBottom"].AxisY.MajorGrid.LineColor = gridLineColor;

            chart1.Series[0].IsXValueIndexed = true;
            chart1.Series[1].IsXValueIndexed = true;

            originalMainChartHeight = chart1.ChartAreas["ChartAreaMain"].Position.Height;
            originalBottomChartHeight = chart1.ChartAreas["ChartAreaBottom"].Position.Height;

            bottomChartIndicators = new ChartIndicatorsBottom();
            mainChartIndicators = new ChartIndicatorsMain();

            //TODO remove previous SlopeChanged event.
            // Set up an event handler for slope changing.
            bottomChartIndicators.SlopeChanged += BottomChartIndicators_SlopeChanged;         
        }

        /// <summary>
        /// Reset the chart to load a new instrument (remove all indicators, etc).
        /// </summary>
        void ResetChart()
        {
            for (int i = 2; i < chart1.Series.Count; i++)
            {
                chart1.Series.RemoveAt(i);
            }
            chart1.Series[0].Enabled = true;
            chart1.Series[1].Enabled = true;

            activeIndicators.Clear();

            cboIndicator.SelectedIndex = -1;
        }

        void BottomChartIndicators_SlopeChanged(double slope)
        {
            status.Text = string.Format("Slope = {0}", slope);
        }

        public void SetChartDataProvider(IChartDataProvider provider)
        {
            // TODO: Need to detach any old events (in the case that the user loads a different chart)
            //if (this.chartDataProvider != null && this.chartDataProvider.DataSeriesUpdated != null) this.chartDataProvider.DataSeriesUpdated -= moBarData_DataSeriesUpdated;
            //if (this.chartDataProvider != null && this.chartDataProvider.DataLoadComplete != null) this.chartDataProvider.DataLoadComplete -= moBarData_DataSeriesUpdated;

            chartDescriptor = new ChartDescriptor(provider, activeIndicators);
            
            ResetChart();

            this.chartDataProvider = provider;
            this.chartDataProvider.DataSeriesUpdated += moBarData_DataSeriesUpdated;
            this.chartDataProvider.DataLoadComplete += moBarData_DataLoadComplete;

            chooseBarDataForm = new UIFormChooseBarData(chartDataProvider);
            chooseBarDataForm.SelectedBarChanged += chooseBarDataForm_SelectedBarChanged;
        }

        void chooseBarDataForm_SelectedBarChanged(int barIndex)
        {
            HighlightDataPoint(chart1.Series[0].Points[barIndex]);
            var seriesMap = GetSeriesMapForBarIndex(barIndex);
            chooseBarDataForm.SetSeriesData(seriesMap);
        }

        /// <summary>
        /// For a specified bar index, create a dictionary of "series name" to value (as string).
        /// </summary>
        Dictionary<string, string> GetSeriesMapForBarIndex(int barIndex)
        {
            // Create a dictionary of the series name (or an amended name for the basic series) and
            // a value (also a string so we can handle a variety of value types by converting to string).
            Dictionary<string, string> seriesMap = new Dictionary<string, string>();
            foreach (Series series in chart1.Series)
            {
                if (series.Name == "candlestickSeries")
                {
                    seriesMap["open"] = series.Points[barIndex].YValues[0].ToString();
                    seriesMap["high"] = series.Points[barIndex].YValues[1].ToString();
                    seriesMap["low"] = series.Points[barIndex].YValues[2].ToString();
                    seriesMap["close"] = series.Points[barIndex].YValues[3].ToString();

                }
                else if (series.Name == "candlestickVolumeSeries")
                {
                    seriesMap["volume"] = series.Points[barIndex].YValues[0].ToString();
                }
                else if (series.Name == "TrendLine")
                {
                    seriesMap["TrendLine"] = bottomChartIndicators.Slope.ToString();
                }
                else
                {
                    if (barIndex < series.Points.Count)
                        seriesMap[series.Name] = series.Points[barIndex].YValues[0].ToString("0.00");
                }
            }
            return seriesMap;            
        }

        void UpdateCurrentValue()
        {
            var seriesMap = GetSeriesMapForBarIndex(BarIndex);
            CurrentValue = Convert.ToDouble(seriesMap[SelectedSeries]);
            if (CurrentValueChanged != null) CurrentValueChanged(CurrentValue);
        }

        #region CHART DATA EVENT HANDLERS (DOWNLOAD COMPLETE and DATA SERIES UPDATED)

        /// <summary>
        /// The ChartDataSeries.DataLoadComplete event is raised upon completion of loading historical chart data. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>

        private void  // ERROR: Handles clauses are not supported in C#
        moBarData_DataLoadComplete(object sender, ezDataLoadCompleteEventArgs e)
        {
	        // NOTE: This event is raised on it's own thread. If you are updating a GUI application with the chart data (like this example), don't forget that you
	        //       MUST marshall this update onto the GUI thread before you modify any GUI components.
	        // (This is the same for all T4 API events.)

	        if (this.InvokeRequired) {
		        // An invoke is required to update the GUI, simply invoke back to this same mthod.
		        this.BeginInvoke(new ezDataLoadCompleteEventHandler(moBarData_DataLoadComplete), new object[] {
			        sender,
			        e
		        });

		        // Don't forget to exit now!
		        return;
	        }

	        // On the GUI thread now, update the chart.
	        if (e.Status == zDataLoadStatus.Failed) {
                status.Text = "ERROR: Chart data request failed.";
		        Spy.Print("IChartDataRequest_ChartDataComplete: Chart data request failed.");
		        return;
	        }
            else
            {
		        // Sometimes, you won't get all the historical data you requested. This could happen if there isn't chart data available all the back to the start date you requested.
		        Spy.Print("IChartDataRequest_ChartDataComplete: Status: {0}, Dates Requested: {1}, Dates Received: {2}", e.Status, e.DateRangeRequested, e.DateRangeProcessed);
	        }

            status.Text = "To add an indicator to the chart, choose a chart indicator from the indicator dropdown list.";

            // Date/time label formats are different for Day than they are for "shorter than day" periods.
            if (chartDataProvider.BarInterval.Interval == zChartInterval.Day)
            {
                chart1.ChartAreas["ChartAreaMain"].AxisX.LabelStyle.Format = "ddd M/d";
                chart1.ChartAreas["ChartAreaBottom"].AxisX.LabelStyle.Format = "ddd M/d";
            }
            else
            {
                chart1.ChartAreas["ChartAreaMain"].AxisX.LabelStyle.Format = "ddd h:mmtt";
                chart1.ChartAreas["ChartAreaBottom"].AxisX.LabelStyle.Format = "M/d h:mmtt";
            }

            chart1.Series["candlestickSeries"].Points.Clear();
            chart1.Series["candlestickVolumeSeries"].Points.Clear();

            // Remove all but the two main series.
            for (int i = 2; i < chart1.Series.Count; i++)
                chart1.Series.Remove(chart1.Series[i]);

            double dataPointHigh = double.MinValue;
            double dataPointLow = double.MaxValue;

	        // Before iterating the data collection, you MUST lock it. This will prevent live trade updates from modifying the collection as you read it.
	        chartDataProvider.Lock();

	        try
            {
		        /*for (int i = 0; i <= chartBarData.TradeBars.Count - 1; i++) {
			        ezBarDataPoint oBar = (ezBarDataPoint)chartBarData.TradeBars[i];

			        Trace.WriteLine(string.Format("TradeDate: {6}, Time: {5}, Open: {0}, High: {1}, Low: {2}, Close: {3}, Volume: {4}", oBar.OpenTicks, oBar.HighTicks, oBar.LowTicks, oBar.CloseTicks, oBar.Volume, oBar.Time, oBar.TradeDate));

			        moTable.Rows.Add(i, oBar.TradeDate, oBar.Time, oBar.OpenTicks, oBar.HighTicks, oBar.LowTicks, oBar.CloseTicks, oBar.Volume);

                    // Add candlestick to chart (high, low, open, close).
                    object[] multipleValues = new object[] { oBar.HighTicks, oBar.LowTicks, oBar.OpenTicks, oBar.CloseTicks };
                    chart1.Series[0].Points.AddXY(oBar.Time, multipleValues);
                    chart1.Series[1].Points.AddXY(oBar.Time, oBar.Volume);

                    // See if our maximum and minimum values on the chart have changed (we will use this for axis scaling).
                    dataPointHigh = Math.Max(dataPointHigh, oBar.HighTicks);
                    dataPointLow = Math.Min(dataPointLow, oBar.LowTicks);
		        }*/

                for (int i = 0; i <= chartDataProvider.ChartData.TradeBars.Count - 1; i++)
                {
                    ezBarDataPoint oBar = (ezBarDataPoint)chartDataProvider.ChartData.TradeBars[i];

                    // Add candlestick to chart (high, low, open, close).
                    AddBarToChart(oBar);

                    // See if our maximum and minimum values on the chart have changed (we will use this for axis scaling).
                    dataPointHigh = Math.Max(dataPointHigh, oBar.High);
                    dataPointLow = Math.Min(dataPointLow, oBar.Low);
                }

                chart1.ChartAreas["ChartAreaMain"].AxisY.Minimum = dataPointLow;
                chart1.ChartAreas["ChartAreaMain"].AxisY.Maximum = dataPointHigh;

                //this.Text = "Monkey Chart - " + chartDataProvider.Name + string.Format(" - {0} {1}", chartDataProvider.BarInterval.Period, chartDataProvider.BarInterval.Interval);
	        }
            catch (Exception ex)
            {
		        Spy.Print("Error loading chart data: " + ex.ToString());
                status.Text = "Error loading chart data: " + ex.ToString();
	        }
            finally
            {
		        // If you don't be sure to unlock the data collection, you probably won't get any live trade updates.
		        chartDataProvider.Unlock();
	        }
        }

        void AddBarToChart(ezBarDataPoint barDP)
        {
            object[] multipleValues = new object[] { barDP.High, barDP.Low, barDP.Open, barDP.Close };
            // We don't use these indexes right now, but the AddXY method returns the index of the point it adds.
            int index1 = chart1.Series[0].Points.AddXY(barDP.Time, multipleValues);
            int index2 = chart1.Series[1].Points.AddXY(barDP.Time, barDP.Volume);
        }

        void RecalcAllActiveSeries()
        {
            foreach (Indicator ind in activeIndicators.Values)
            {
                ind.Draw(chart1, false);
            }
        }

        /// <summary>
        /// The ChartDataSeries.DataSeriesUpdated event is raised upon the data series being update by a live trade (only raised when the chart data was loaded
        /// using the LoadRealTimeChartData() method. This event will not be fired more frequently than every 100msec by default.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>

        private void  // ERROR: Handles clauses are not supported in C#
        moBarData_DataSeriesUpdated(object sender, ezDataSeriesUpdatedEventArgs e)
        {
	        // NOTE: This event is raised on it's own thread. If you are updating a GUI application with the chart data (like this example), don't forget that you
	        //       MUST marshall this update onto the GUI thread before you modify any GUI components.
	        // (This is the same for all T4 API events.)

	        if (this.InvokeRequired) {
		        // An invoke is required to update the GUI, simply invoke back to this same mthod.
		        this.BeginInvoke(new ezDataSeriesUpdatedEventHandler(moBarData_DataSeriesUpdated), new object[] {
			        sender,
			        e
		        });

		        // Don't forget to exit now!
		        return;
	        }

	        //On the GUI thread now, update the chart.

	        // Before iterating the data collection, you MUST lock it. This will prevent live trade updates from modifying the collection as you read it.
	        chartDataProvider.Lock();

	        try
            {
		        //e.TradeBarIndexUpdate is the index of the first bar that was added or updated since the last update event.
                for (int i = e.TradeBarIndexUpdate; i <= chartDataProvider.ChartData.TradeBars.Count - 1; i++)
                {
                    ezBarDataPoint oBar = (ezBarDataPoint)chartDataProvider.ChartData.TradeBars[i];

                    if (i < chart1.Series[0].Points.Count)
                    {
                        Spy.Print("Updating bar index {0} - close = {1}", i, oBar.Close);

                        // Update the chart series.
                        DataPoint dp1 = chart1.Series[0].Points[i];
                        dp1.YValues[0] = oBar.High;
                        dp1.YValues[1] = oBar.Low;
                        dp1.YValues[2] = oBar.Open;
                        dp1.YValues[3] = oBar.Close;
                        DataPoint dp2 = chart1.Series[1].Points[i];
                        dp2.YValues[0] = oBar.Volume;

                        RecalcAllActiveSeries();
                        chart1.Refresh();
                    }
                    else
                    {
                        xAxis = chart1.ChartAreas["ChartAreaMain"].AxisX;
                        bool scrollToFurthest = (xAxis.ScaleView.ViewMaximum == xAxis.Maximum);
                        AddBarToChart(oBar);
                        RecalcAllActiveSeries();
                        chart1.Refresh();
                        if (scrollToFurthest == true) xAxis.ScaleView.Scroll(xAxis.Maximum);
                    }
                }
	        }
            catch (Exception ex)
            {
		        Spy.Print("Error updating chart data: " + ex.ToString());
                status.Text = "Error updating chart data: " + ex.ToString();
            }
            finally
            {
		        // If you don't be sure to unlock the data collection, you probably won't get any live trade updates.
		        chartDataProvider.Unlock();
	        }
        }

        #endregion

        #region UI EVENT HANDLERS
        private void ChartDataForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void ChartDataForm_MouseWheel(object sender, MouseEventArgs e)
        {
            ScrollType scrollType = ScrollType.SmallIncrement;

            if (e.Delta > 0)
                scrollType = ScrollType.SmallIncrement;
            else if (e.Delta < 0)
                scrollType = ScrollType.SmallDecrement;

            int scrollTicks = 1;
            int wheelTurns = Math.Abs(e.Delta / 120);
            if (wheelTurns > 1)
            {
                scrollTicks = (int)Math.Pow(2, wheelTurns);
            }

            ScrollChartHorizontal(scrollType, scrollTicks);
        }

        #region ZOOM, SCALE, TOGGLE
        private void btnZoomPlus_Click(object sender, EventArgs e)
        {
            ZoomIn();
        }

        private void btnZoomMinus_Click(object sender, EventArgs e)
        {
            ZoomOut();
        }

        private void btnResetZoom_Click(object sender, EventArgs e)
        {
            ZoomReset();
        }

        private void chart1_SelectionRangeChanged(object sender, CursorEventArgs e)
        {
            Console.WriteLine("selection");
        }

        private void chkGridLines_CheckedChanged(object sender, EventArgs e)
        {
            EnableGridLines(chkGridLines.Checked);
        }

        private void chkVolume_CheckedChanged(object sender, EventArgs e)
        {
            EnableBottomChart(chkVolume.Checked);
        }

        private void btnScaleYAxis_Click(object sender, EventArgs e)
        {
            ScaleYAxisToFit();
        }
        #endregion

        #region MOUSE CLICK HIGHLIGHT AND AXIS SCALING
        private void chart1_MouseDown(object sender, MouseEventArgs e)
        {
            HitTestResult result = chart1.HitTest(e.X, e.Y);

            // Check to see if the user has CLICKED ON A CANDLESTICK BAR.
            if (result.ChartElementType == ChartElementType.DataPoint)
            {
                DataPoint dp = result.Object as DataPoint;

                // Only going to check this out if the user has specifically enabled bar select (may not want it on a "normal" chart)
                if (BarSelectEnabled == true)
                {
                    // Try to find the index of this data point in the main chart series.
                    int barIndex = chart1.Series[0].Points.IndexOf(dp);

                    // If the user has clicked on a valid data point, display the form to select a series.
                    if (chooseBarDataForm != null && barIndex >= 0)
                    {
                        HighlightDataPoint(dp);

                        WinForms.SetFormRelativePosition(chooseBarDataForm, this);
                        chooseBarDataForm.BarIndex = barIndex;
                        chooseBarDataForm.ShowDialog(this);

                        if (chooseBarDataForm.UserSelection == DialogResult.OK && chooseBarDataForm.SelectedSeries != null)
                        {
                            BarIndex = chooseBarDataForm.BarIndex;
                            ReverseBarIndex = chooseBarDataForm.ReverseBarIndex;
                            SelectedSeries = chooseBarDataForm.SelectedSeries;
                            UpdateCurrentValue();
                        }
                    }
                }
            }
            // Check to see if the user has CLICKED ON AN AXIS.
            else if (result.ChartElementType == ChartElementType.AxisLabels)
            {
                CustomLabel label = result.Object as CustomLabel;

                Axis yAxisMain = chart1.ChartAreas["ChartAreaMain"].AxisY;
                Axis yAxisBottom = chart1.ChartAreas["ChartAreaBottom"].AxisY;

                // This code deals only with the Y-AXIS of the MAIN chart area.
                if (label.Axis == yAxisMain || label.Axis == yAxisBottom)
                {
                    mouseX = e.X;
                    mouseY = e.Y;
                    activeAxis = label.Axis;

                    // Check for either left or right mouse button.
                    if (e.Button == MouseButtons.Left)
                        leftButtonDown = true;
                    else if (e.Button == MouseButtons.Right)
                        rightButtonDown = true;
                    
                    this.Cursor = Cursors.HSplit;
                }                
            }
        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftButtonDown == true)
            {
                if (e.Y > mouseY)
                    AdjustAxisScale(1, 1);
                else if (e.Y < mouseY)
                    AdjustAxisScale(-1, -1);

                mouseX = e.X;
                mouseY = e.Y;
            }
            else if (rightButtonDown == true)
            {
                if (e.Y > mouseY)
                    AdjustAxisScale(-1, 1);
                else if (e.Y < mouseY)
                    AdjustAxisScale(1, -1);

                mouseX = e.X;
                mouseY = e.Y;
            }
        }

        private void chart1_MouseUp(object sender, MouseEventArgs e)
        {
            leftButtonDown = false;
            rightButtonDown = false;
            this.Cursor = Cursors.Default;
        }

        void AdjustAxisScale(int minAdjust, int maxAdjust)
        {
            double interval = activeAxis.LabelStyle.Interval / 2;

            activeAxis.Minimum = activeAxis.Minimum + (minAdjust * interval);
            activeAxis.Maximum = activeAxis.Maximum + (maxAdjust * interval);
        }

        void HighlightDataPoint(DataPoint dp)
        {
            if (previousSelectedDataPoint != null)
            {
                previousSelectedDataPoint["PriceUpColor"] = chart1.Series[0]["PriceUpColor"];
                previousSelectedDataPoint["PriceDownColor"] = chart1.Series[0]["PriceDownColor"];
            }
            previousSelectedDataPoint = dp;
            dp["PriceUpColor"] = "Yellow";
            dp["PriceDownColor"] = "Yellow";
        }
        #endregion

        #endregion

        #region ZOOM
        public void ZoomIn()
        {
            try
            {
                double xMin = chart1.ChartAreas["ChartAreaMain"].AxisX.ScaleView.ViewMinimum;
                double xMax = chart1.ChartAreas["ChartAreaMain"].AxisX.ScaleView.ViewMaximum;

                Point zoomPoint = new Point();
                zoomPoint.X = chart1.Location.X + (chart1.ClientRectangle.Width / 2);

                double posXStart = chart1.ChartAreas["ChartAreaMain"].AxisX.PixelPositionToValue(zoomPoint.X) - (xMax - xMin) / 3;
                double posXFinish = chart1.ChartAreas["ChartAreaMain"].AxisX.PixelPositionToValue(zoomPoint.X) + (xMax - xMin) / 3;

                chart1.ChartAreas["ChartAreaMain"].AxisX.ScaleView.Zoom(posXStart, posXFinish);

                ezBarInterval barInterval = chartDataProvider.BarInterval;
                string sInterval = barInterval.Interval.ToString() + "s";   // DateTimeIntervalType enum values are plural
                DateTimeIntervalType intervalType = (DateTimeIntervalType)Enum.Parse(typeof(DateTimeIntervalType), sInterval, true);
                chart1.ChartAreas["ChartAreaMain"].AxisX.ScaleView.SmallScrollSize = 1;
                chart1.ChartAreas["ChartAreaMain"].AxisX.ScaleView.SmallScrollSizeType = DateTimeIntervalType.Auto;
            }
            catch (Exception ex) { ExceptionHandler.TraceException(ex); }            
        }

        public void ZoomOut()
        {
            try
            {
                double xMin = chart1.ChartAreas["ChartAreaMain"].AxisX.ScaleView.ViewMinimum;
                double xMax = chart1.ChartAreas["ChartAreaMain"].AxisX.ScaleView.ViewMaximum;

                Point zoomPoint = new Point();
                zoomPoint.X = chart1.Location.X + (chart1.ClientRectangle.Width / 2);

                double posXStart = chart1.ChartAreas["ChartAreaMain"].AxisX.PixelPositionToValue(zoomPoint.X) - (xMax - xMin) / 2;
                double posXFinish = chart1.ChartAreas["ChartAreaMain"].AxisX.PixelPositionToValue(zoomPoint.X) + (xMax - xMin) / 2;

                chart1.ChartAreas["ChartAreaMain"].AxisX.ScaleView.Zoom(posXStart, posXFinish);
            }
            catch (Exception ex) { ExceptionHandler.TraceException(ex); }
        }

        public void ZoomReset()
        {
            chart1.ChartAreas["ChartAreaMain"].AxisX.ScaleView.ZoomReset();
            chart1.ChartAreas["ChartAreaMain"].AxisY.ScaleView.ZoomReset();
        }
        #endregion

        #region ENABLE/DISABLE CHART FEATURES
        public void EnableGridLines(bool showGridLines)
        {
            chart1.ChartAreas["ChartAreaMain"].AxisX.MajorGrid.Enabled = showGridLines;
            chart1.ChartAreas["ChartAreaMain"].AxisY.MajorGrid.Enabled = showGridLines;
            chart1.ChartAreas["ChartAreaBottom"].AxisX.MajorGrid.Enabled = showGridLines;
            chart1.ChartAreas["ChartAreaBottom"].AxisY.MajorGrid.Enabled = showGridLines;
        }

        public void EnableBottomChart(bool showBottomChart)
        {
            chart1.ChartAreas["ChartAreaBottom"].Visible = showBottomChart;
            //chart1.ChartAreas["ChartArea1"].AlignmentStyle = AreaAlignmentStyles.
            if (showBottomChart)
            {
                chart1.ChartAreas["ChartAreaMain"].Position.Height = originalMainChartHeight;
                chart1.ChartAreas["ChartAreaBottom"].Position.Height = originalBottomChartHeight;
            }
            else
            {
                chart1.ChartAreas["ChartAreaMain"].Position.Height = 100;
                chart1.ChartAreas["ChartAreaBottom"].Position.Height = 0;
            }
        }
        #endregion

        public void ScrollChartHorizontal(ScrollType scrollType, int scrollTicks)
        {
            // Scroll the chart (assume AT LEAST one mouse wheel turn).           
            for (int i = 0; i < scrollTicks; i++)
            {
                chart1.ChartAreas["ChartAreaMain"].AxisX.ScaleView.Scroll(scrollType);
            }
        }

        public void ScaleYAxisToFit()
        {
            int start = (int)chart1.ChartAreas["ChartAreaMain"].AxisX.ScaleView.ViewMinimum;
            int end = (int)chart1.ChartAreas["ChartAreaMain"].AxisX.ScaleView.ViewMaximum;

            // Series ss = chart1.Series.FindByName("SeriesName");
            // use ss instead of chart1.Series[0]

            double[] temp = chart1.Series[0].Points.Where((x, i) => i >= start && i <= end).Select(x => x.YValues[0]).ToArray();
            double ymax = temp.Max();
            temp = chart1.Series[0].Points.Where((x, i) => i >= start && i <= end).Select(x => x.YValues[1]).ToArray();
            double ymin = temp.Min();

            chart1.ChartAreas["ChartAreaMain"].AxisY.ScaleView.Position = ymin;
            chart1.ChartAreas["ChartAreaMain"].AxisY.ScaleView.Size = ymax - ymin;
        }

        private void RequestChartData(EZInstrument instrument, zChartInterval interval, int period)
        {
            // Set EndDate to the current trading date.
            //DateTime dtEndDate = selectedContract.GetTradeDate(DateTime.Now);
            DateTime dtEndDate = DateTime.Now;

            DateTime dtStartDate;
            zChartInterval enBarInterval = zChartInterval.Hour;

            if (interval == zChartInterval.Day)
            {
                enBarInterval = zChartInterval.Day;

                // User select Day bars, load a few months of them.
                dtStartDate = dtEndDate.AddMonths(-3);
            }
            else
            {
                enBarInterval = interval;

                // User selected non-Day bars (Hour, Minute, etc.), load a couple of days of them.
                dtStartDate = dtEndDate;
                dtStartDate = dtStartDate.AddDays(-3);

                /*// This little loop here will ensure that we load the previous trade date as well as today and will account for weekends
                // and holidays.
                while ((selectedContract.GetTradeDate(dtStartDate) == dtEndDate))
                {
                    dtStartDate = dtStartDate.AddDays(-1);
                }*/
            }

            // Create a BarInterval object to tell the API what bar interval we want.
            // So for example, if we wanted a 15 minute bar, we would do:  New ezBarInterval(zChartDataType.Minute, 15)
            ezBarInterval oBarIntvl = new ezBarInterval(enBarInterval, period);

            string chartName = instrument.Name + " : " + oBarIntvl;
            IChartDataProvider provider = new ChartDataProviderCTS(chartName, oBarIntvl, ezSessionTimeRange.Empty);
            if (LoadingNewChart != null) LoadingNewChart(provider);
            this.SetChartDataProvider(provider);
            WinForms.SetWaitCursor(true);
            provider.LoadHistoricalChartData(APIMain.MarketFromInstrument(instrument), dtStartDate, dtEndDate);
        }

        private void btnChartMarket_Click(object sender, EventArgs e)
        {
            WinForms.SetWaitCursor(true);

            try
            {
                zChartInterval interval = (zChartInterval)Enum.Parse(typeof(zChartInterval), comboChartInterval.Text);

                EZInstrument instrument = APIMain.Instance.ShowInstrumentDialog();

                if (instrument == null)
                {
                    status.Text = "No instrument selected.";
                }
                else
                {
                    status.Text = "*** LOADING CHART ***";
                    RequestChartData(instrument, interval, Convert.ToInt32(numericPeriod.Value));
                }
            }
            finally
            {
                WinForms.SetWaitCursor(false);
            }
        }

        #region INDICATORS
        private void cboIndicator_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboIndicator.SelectedIndex < 0)
                return;

            string indicatorName = cboIndicator.Text;

            try
            {
                switch (ActiveChart)
                {
                    case IndicatorChartArea.MainChart:
                        currentIndicator = mainChartIndicators[indicatorName];
                        currentIndicator.Draw(chart1);
                        activeIndicators.Add(currentIndicator);
                        break;
                    case IndicatorChartArea.BottomChart:
                        currentIndicator = bottomChartIndicators[indicatorName];
                        currentIndicator.Draw(chart1);
                        activeIndicators.Add(currentIndicator);
                        break;
                }

                indicatorParams.Parameters = currentIndicator.Parameters;
                indicatorParams.ParameterChangedHandler = indicatorParams_ParameterChanged;
                indicatorParams.ParameterSelectedHandler = indicatorParams_ParameterSelected;
                //indicatorParams.ParameterChanged += indicatorParams_ParameterChanged;
                //indicatorParams.ParameterSelected += indicatorParams_ParameterSelected;

                // Display the indicator description and enable/disable the button to show a web page with more info.
                txtIndicatorDescription.Text = currentIndicator.Description ?? "";
                if (currentIndicator.MoreInfoWebPage == null)
                    btnMoreIndicatorDescription.Enabled = false;
                else
                    btnMoreIndicatorDescription.Enabled = true;
            }
            catch (Exception ex)
            {
                Spy.Print("EXCEPTION occurred drawing chart indicator: " + ex.Message);
                status.Text = string.Format("ERROR: A problem occurred attempting to add indicator '{0}'.", indicatorName);
            }
        }

        void indicatorParams_ParameterChanged()
        {
            // Catch (and ignore) an exception generated when redrawing a chart (in case
            // it is caused by a bad user-entered parameter value.
            try
            {
                currentIndicator.Draw(chart1);
            }
            catch (Exception ex)
            {
                Spy.Print("EXCEPTION occurred drawing chart indicator: " + ex.Message);
            }
        }

        void indicatorParams_ParameterSelected(string textToDisplay)
        {
            status.Text = textToDisplay;
        }

        void PopulateIndicatorDropdownList(IndicatorChartArea activeChart)
        {
            cboIndicator.Items.Clear();

            switch (activeChart)
            {
                case IndicatorChartArea.MainChart:
                    foreach (string indicatorName in mainChartIndicators.Indicators.Keys)
                    {
                        cboIndicator.Items.Add(indicatorName);
                    }
                    break;
                case IndicatorChartArea.BottomChart:
                    foreach (string indicatorName in bottomChartIndicators.Indicators.Keys)
                    {
                        cboIndicator.Items.Add(indicatorName);
                    }
                    break;
            }
        }

        private void btnMoreIndicatorDescription_Click(object sender, EventArgs e)
        {
            if (currentIndicator.MoreInfoWebPage != null)
                System.Diagnostics.Process.Start(currentIndicator.MoreInfoWebPage);
        }
        #endregion

        #region CHART AREA - MAIN or BOTTOM
        IndicatorChartArea ActiveChart
        {
            get
            {
                if (chkMainChart.Checked == true)
                    return IndicatorChartArea.MainChart;
                else
                    return IndicatorChartArea.BottomChart;
            }
        }

        private void chkMainChart_CheckedChanged(object sender, EventArgs e)
        {
            ChangeActiveChart(chkMainChart.Checked ? IndicatorChartArea.MainChart : IndicatorChartArea.BottomChart);
        }

        private void chkBottomChart_CheckedChanged(object sender, EventArgs e)
        {
            ChangeActiveChart(chkBottomChart.Checked ? IndicatorChartArea.BottomChart : IndicatorChartArea.MainChart);
        }

        void ChangeActiveChart(IndicatorChartArea selectedChartArea)
        {
            if (changingActiveChart == true)
                return;

            changingActiveChart = true;
            chkMainChart.Checked = (selectedChartArea == IndicatorChartArea.MainChart);
            chkBottomChart.Checked = !chkMainChart.Checked;
            changingActiveChart = false;

            ActiveChartChanged();
        }

        void ActiveChartChanged()
        {
            bool b1, b2;

            b1 = chkMainChart.Checked;
            chkMainChart.BackColor = b1 ? Color.Blue : Color.FromKnownColor(KnownColor.Control);
            chkMainChart.ForeColor = b1 ? Color.White : Color.FromKnownColor(KnownColor.ControlText);

            b2 = chkBottomChart.Checked;
            chkBottomChart.BackColor = b2 ? Color.Blue : Color.FromKnownColor(KnownColor.Control);
            chkBottomChart.ForeColor = b2 ? Color.White : Color.FromKnownColor(KnownColor.ControlText);

            PopulateIndicatorDropdownList(ActiveChart);
        }
        #endregion

    
    } // class
} // namespace
