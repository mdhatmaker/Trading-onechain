using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;
using T4;
using T4.API;
using T4.API.ChartData;

namespace MonkeyLightning
{
    public partial class ChartDataForm : Form
    {
        public ChartDataForm()
        {
            InitializeComponent();

            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "ddd h:mmtt";
            chart1.ChartAreas[1].AxisX.LabelStyle.Format = "M/d h:mmtt";
        }

        #region " Member Variables "

        // Reference to the main api host object.

        public Exchange moSelectedExchange { get; set; }
        // Reference to the selected market.

        public Contract moSelectedContract { get; set; }
        // Reference to the selected market.

        public Market moMarket { get; set; }
        // Reference to the chart data object that is going to hold and update the chart data we request from the server.

        private ChartDataSeries moBarData;
        // Used to bind the chart data to the grid.

        private DataTable moTable;
        #endregion

        #region " **** Request and Display Chart Data from the API **** (Look here for the ChartData API details) "


        public void RequestChartData(ChartInterval interval)
        {
            this.Text = moMarket.Description;

	        // Set EndDate to the current trading date.
	        DateTime dtEndDate = moSelectedContract.GetTradeDate(DateTime.Now);
	        DateTime dtStartDate;
	        T4.API.ChartData.ChartInterval enBarInterval = ChartInterval.Hour;


	        if (interval == ChartInterval.Day) {

		        enBarInterval = ChartInterval.Day;

		        // User select Day bars, load a month of them.
		        dtStartDate = dtEndDate.AddMonths(-1);


	        } else {
		        //enBarInterval = ChartInterval.Hour;
                enBarInterval = ChartInterval.Minute;

		        // User selected Hour bars, load a couple of days of them.
		        dtStartDate = dtEndDate;

		        // This little loop here will ensure that we load the previous trade date as well as today and will account for weekends
		        // and holidays.
		        while ((moSelectedContract.GetTradeDate(dtStartDate) == dtEndDate)) {
			        dtStartDate = dtStartDate.AddDays(-1);
		        }

	        }

	        // Create a BarInterval object to tell the API what bar interval we want.
	        // So for example, if we wanted a 15 minute bar, we would do:  New T4.API.ChartData.BarInterval(ChartDataType.Minute, 15)
	        //T4.API.ChartData.BarInterval oBarIntvl = new T4.API.ChartData.BarInterval(enBarInterval, 2);
            T4.API.ChartData.BarInterval oBarIntvl = new T4.API.ChartData.BarInterval(enBarInterval, 5);


	        if (moMarket != null) {
		        // A market was selected. Load the data for the selected market.

		        // First, create a new ChartDataSeries. This object store the chart data from the server as well as keep it updated as live trades
		        // come in. You REALLY should be careful to ensure that you properly Dispose this object when you are no longer using it.
		        moBarData = new T4.API.ChartData.ChartDataSeries(moMarket, oBarIntvl, SessionTimeRange.Empty);

                moBarData.DataSeriesUpdated += moBarData_DataSeriesUpdated;
                moBarData.DataLoadComplete += moBarData_DataLoadComplete;

		        // Request the chart data.
		        // LoadRealTimeChartData() takes only a start date. It will load all historical chart data from that date to now and will keep
		        //    that data updated as trades get posted to the system.
		        moBarData.LoadRealTimeChartData(dtStartDate);

            // Alternatively, LoadHistoricalChartData() takes a start and end date and simply only load historical chart data. This method does
	        //    NOT keep the data updated as live trades come in.
	        //moBarData.LoadHistoricalChartData(dtStartDate, dtEndDate)


	        } else {
		        // A market was not selected. Load "continuation" data.
		        // The default contructor for ContinuationSettings instructs the API to create a long-term historical series of chart data
		        //   for the contract by looking at the total traded volume for each day. The market that has the high trade volume is the
		        //   market from which data will be pulled for that trade date. This is called a "volume continuation" and is the most common
		        //   method of constructing such chart data sets for a contract. There are other types of continuations that you can create, however
		        //   they won't be discussed in this example.
		        T4.API.ChartData.ContinuationSettings oContinuation = new T4.API.ChartData.ContinuationSettings();

		        // First, create a new ChartDataSeries. This object store the chart data from the server as well as keep it updated as live trades
		        // come in. You REALLY should be careful to ensure that you properly Dispose this object when you are no longer using it.
		        moBarData = new T4.API.ChartData.ChartDataSeries(moSelectedContract, oBarIntvl, SessionTimeRange.Empty, oContinuation);

		        // Request the chart data.
		        // LoadRealTimeChartData() takes only a start date. It will load all historical chart data from that date to now and will keep
		        //    that data updated as trades get posted to the system.
		        moBarData.LoadRealTimeChartData(dtStartDate);

		        // Alternatively, LoadHistoricalChartData() takes a start and end date and simply only load historical chart data. This method does
		        //    NOT keep the data updated as live trades come in.
		        //moBarData.LoadHistoricalChartData(dtStartDate, dtEndDate)

	        }

        }

        /// <summary>
        /// The ChartDataSeries.DataLoadComplete event is raised upon completion of loading historical chart data. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>

        private void  // ERROR: Handles clauses are not supported in C#
        moBarData_DataLoadComplete(object sender, T4.API.ChartData.DataLoadCompleteEventArgs e)
        {
	        // NOTE: This event is raised on it's own thread. If you are updating a GUI application with the chart data (like this example), don't forget that you
	        //       MUST marshall this update onto the GUI thread before you modify any GUI components.
	        // (This is the same for all T4 API events.)

	        if (this.InvokeRequired) {
		        // An invoke is required to update the GUI, simply invoke back to this same mthod.
		        this.BeginInvoke(new T4.API.ChartData.DataLoadCompleteEventHandler(moBarData_DataLoadComplete), new object[] {
			        sender,
			        e
		        });

		        // Don't forget to exit now!
		        return;

	        }

	        // On the GUI thread now, update the chart.

	        if (e.Status == DataLoadStatus.Failed) {
		        Trace.WriteLine("IChartDataRequest_ChartDataComplete: Chart data request failed.");
		        return;


	        } else {
		        // Sometimes, you won't get all the historical data you requested. This could happen if there isn't chart data available all the back to the start date you requested.
		        Trace.WriteLine(string.Format("IChartDataRequest_ChartDataComplete: Status: {0}, Dates Requested: {1}, Dates Received: {2}", e.Status, e.DateRangeRequested, e.DateRangeProcessed));

	        }

            chart1.Series["candlestickSeries"].Points.Clear();
            chart1.Series["candlestickVolumeSeries"].Points.Clear();
            chart1.Series["movingAverageSeries"].Points.Clear();
            int dataPointHigh = int.MinValue;
            int dataPointLow = int.MaxValue;

	        // Before iterating the data collection, you MUST lock it. This will prevent live trade updates from modifying the collection as you read it.
	        moBarData.Lock();


	        try {

		        for (int i = 0; i <= moBarData.TradeBars.Count - 1; i++) {
			        T4.API.ChartData.BarDataPoint oBar = (T4.API.ChartData.BarDataPoint)moBarData.TradeBars[i];

			        Trace.WriteLine(string.Format("TradeDate: {6}, Time: {5}, Open: {0}, High: {1}, Low: {2}, Close: {3}, Volume: {4}", oBar.OpenTicks, oBar.HighTicks, oBar.LowTicks, oBar.CloseTicks, oBar.Volume, oBar.Time, oBar.TradeDate));

			        moTable.Rows.Add(i, oBar.TradeDate, oBar.Time, oBar.OpenTicks, oBar.HighTicks, oBar.LowTicks, oBar.CloseTicks, oBar.Volume);

                    // Add candlestick to chart (high, low, open, close).
                    object[] multipleValues = new object[] { oBar.HighTicks, oBar.LowTicks, oBar.OpenTicks, oBar.CloseTicks };
                    chart1.Series[0].Points.AddXY(oBar.Time, multipleValues);
                    chart1.Series[1].Points.AddXY(oBar.Time, oBar.Volume);

                    // See if our maximum and minimum values on the chart have changed (we will use this for axis scaling).
                    dataPointHigh = Math.Max(dataPointHigh, oBar.HighTicks);
                    dataPointLow = Math.Min(dataPointLow, oBar.LowTicks);
		        }

		        DataGridView1.AutoResizeColumns();
                chart1.ChartAreas[0].AxisY.Minimum = dataPointLow;
                chart1.ChartAreas[0].AxisY.Maximum = dataPointHigh;

                /*
                IList<DateTime> listx = new List<DateTime>();
                IList<double> listy = new List<double>();

                //iterate and add your values to the two lists: listx and listy

                //when you're done, bind the lists to the points collection
                yourSeries.Points.DataBindXY(listx, listy);
                */

	        } catch (Exception ex) {
		        Trace.WriteLine("Error loading chart data: " + ex.ToString());


	        } finally {
		        // If you don't be sure to unlock the data collection, you probably won't get any live trade updates.
		        moBarData.Unlock();

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
        moBarData_DataSeriesUpdated(object sender, T4.API.ChartData.DataSeriesUpdatedEventArgs e)
        {
	        // NOTE: This event is raised on it's own thread. If you are updating a GUI application with the chart data (like this example), don't forget that you
	        //       MUST marshall this update onto the GUI thread before you modify any GUI components.
	        // (This is the same for all T4 API events.)

	        if (this.InvokeRequired) {
		        // An invoke is required to update the GUI, simply invoke back to this same mthod.
		        this.BeginInvoke(new T4.API.ChartData.DataSeriesUpdatedEventHandler(moBarData_DataSeriesUpdated), new object[] {
			        sender,
			        e
		        });

		        // Don't forget to exit now!
		        return;

	        }

	        //On the GUI thread now, update the chart.

	        // Before iterating the data collection, you MUST lock it. This will prevent live trade updates from modifying the collection as you read it.
	        moBarData.Lock();

	        try {
		        //e.TradeBarIndexUpdate is the index of the first bar that was added or updated since the last update event.

		        for (int i = e.TradeBarIndexUpdate; i <= moBarData.TradeBars.Count - 1; i++) {
			        T4.API.ChartData.BarDataPoint oBar = (T4.API.ChartData.BarDataPoint)moBarData.TradeBars[i];


			        if (i < moTable.Rows.Count) {
				        // Update the chart series.
				        DataRow oRow = moTable.Rows[i];
				        oRow.ItemArray = new object[] {
					        i,
					        oBar.TradeDate,
					        oBar.Time,
					        oBar.OpenTicks,
					        oBar.HighTicks,
					        oBar.LowTicks,
					        oBar.CloseTicks,
					        oBar.Volume
				        };


			        } else {
				        // Add a new bar.
				        moTable.Rows.Add(i, oBar.TradeDate, oBar.Time, oBar.OpenTicks, oBar.HighTicks, oBar.LowTicks, oBar.CloseTicks, oBar.Volume);

			        }

		        }


	        } catch (Exception ex) {
		        Trace.WriteLine("Error updating chart data: " + ex.ToString());


	        } finally {
		        // If you don't be sure to unlock the data collection, you probably won't get any live trade updates.
		        moBarData.Unlock();

	        }

        }

        #endregion

        #region " Form and Control Events "

        /// <summary>
        /// Display the login dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>

        private void  // ERROR: Handles clauses are not supported in C#
        frmMain_Load(object sender, System.EventArgs e)
        {
	        ConfigureGridControl();
        }

        /// <summary>
        /// Dispose the API host object to cleanly exit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>

        private void  // ERROR: Handles clauses are not supported in C#
        frmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
        #endregion

        #region " Operations "

        public void ConfigureGridControl()
        {
	        dataSet1.Tables.Clear();

	        moTable = dataSet1.Tables.Add("BarData");
	        moTable.Columns.Add("Index", typeof(int));
	        moTable.Columns.Add("TradeDate", typeof(DateTime));
	        moTable.Columns.Add("Time", typeof(DateTime));
	        moTable.Columns.Add("Open", typeof(double));
	        moTable.Columns.Add("High", typeof(double));
	        moTable.Columns.Add("Low", typeof(double));
	        moTable.Columns.Add("Close", typeof(double));
	        moTable.Columns.Add("Volume", typeof(int));

	        DataGridView1.DataSource = moTable;

	        DataGridView1.AutoResizeColumns();

	        DataGridView1.Sort(DataGridView1.Columns["Time"], System.ComponentModel.ListSortDirection.Descending);

        }

        #endregion

        #region " API Events "

        // Run the application post successful login.

        private void PostLogin()
        {
            /*
	        Trace.WriteLine("Login Success");

	        // Check to see if the user has chart data permission.

	        if (!(moHost.IsInRole("Charting") || moHost.IsInRole("BasicCharting"))) {
		        MessageBox.Show("Your user does not have Charts or Advanced Charting roles set. Please contact your administrator to enable them.");

		        // End the application.
		        this.Close();

	        }
            */

            /*
	        // Populate the available exchanges.
	        moExchanges = moHost.MarketData.Exchanges;

	        // Check to see if the data is already loaded.

	        if (moExchanges.Complete) {
		        // Call the event handler ourselves as the data is 
		        // already loaded.
		        moExchanges_ExchangeListComplete(moExchanges);

	        }
            */

        }

        #endregion

        #region " Helper Classes "

        /// <summary>
        /// Class to hold an exchange refernce in a drop down list.
        /// </summary>
        /// <remarks></remarks>
        private class ExchangeItem
        {

	        /// <summary>
	        /// Reference to the exchange.
	        /// </summary>
	        /// <remarks></remarks>

	        public readonly Exchange Exchange;
	        /// <summary>
	        /// Constructor.
	        /// </summary>
	        /// <param name="poExchange"></param>
	        /// <remarks></remarks>

	        public ExchangeItem(Exchange poExchange)
	        {
		        Exchange = poExchange;

	        }

	        /// <summary>
	        /// Display the exchange description.
	        /// </summary>
	        /// <returns></returns>
	        /// <remarks></remarks>
	        public override string ToString()
	        {
		        return Exchange.Description;
	        }

        }

        /// <summary>
        /// Class to hold a contract refernce in a drop down list.
        /// </summary>
        /// <remarks></remarks>
        private class ContractItem
        {

	        /// <summary>
	        /// Reference to the contract.
	        /// </summary>
	        /// <remarks></remarks>

	        public readonly Contract Contract;
	        /// <summary>
	        /// Constructor.
	        /// </summary>
	        /// <param name="poContract"></param>
	        /// <remarks></remarks>

	        public ContractItem(Contract poContract)
	        {
		        Contract = poContract;

	        }

	        /// <summary>
	        /// Display the contract description.
	        /// </summary>
	        /// <returns></returns>
	        /// <remarks></remarks>
	        public override string ToString()
	        {
		        return Contract.Description;
	        }

        }

        /// <summary>
        /// Class to hold an Market refernce in a drop down list.
        /// </summary>
        /// <remarks></remarks>
        private class MarketItem
        {

	        /// <summary>
	        /// Reference to the Market.
	        /// </summary>
	        /// <remarks></remarks>

	        public readonly Market Market;
	        /// <summary>
	        /// Constructor.
	        /// </summary>
	        /// <param name="poMarket"></param>
	        /// <remarks></remarks>

	        public MarketItem(Market poMarket)
	        {
		        Market = poMarket;

	        }

	        /// <summary>
	        /// Display the Market description.
	        /// </summary>
	        /// <returns></returns>
	        /// <remarks></remarks>
	        public override string ToString()
	        {
		        if (Market == null) {
			        return "";
		        } else {
			        return Market.Description;
		        }
	        }

        }
        #endregion

        private void ChartDataForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series["movingAverageSeries"].Points.Clear();
            chart1.DataManipulator.FinancialFormula(FinancialFormula.MovingAverage, txtIndicator.Text.Trim(), "candlestickSeries:Y3", "movingAverageSeries:Y");
        }

        private void btnZoomPlus_Click(object sender, EventArgs e)
        {
            //double size = chart1.ChartAreas[0].AxisX.ScaleView.Size;
            //chart1.ChartAreas[0].AxisX.ScaleView.Size = size + 100;

            try
            {
                    //chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                    //chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset();

                    double xMin = chart1.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                    double xMax = chart1.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
                    //double yMin = chart1.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
                    //double yMax = chart1.ChartAreas[0].AxisY.ScaleView.ViewMaximum;

                    Point zoomPoint = new Point();
                    zoomPoint.X = chart1.Location.X + (chart1.ClientRectangle.Width / 2);
                    //zoomPoint.Y = chart1.Location.Y + (chart1.ClientRectangle.Height / 2);

                    double posXStart = chart1.ChartAreas[0].AxisX.PixelPositionToValue(zoomPoint.X) - (xMax - xMin) / 3;
                    double posXFinish = chart1.ChartAreas[0].AxisX.PixelPositionToValue(zoomPoint.X) + (xMax - xMin) / 3;
                    //double posYStart = chart1.ChartAreas[0].AxisY.PixelPositionToValue(zoomPoint.Y) - (yMax - yMin) / 4;
                    //double posYFinish = chart1.ChartAreas[0].AxisY.PixelPositionToValue(zoomPoint.Y) + (yMax - yMin) / 4;

                    chart1.ChartAreas[0].AxisX.ScaleView.Zoom(posXStart, posXFinish);
                    //chart1.ChartAreas[0].AxisY.ScaleView.Zoom(posYStart, posYFinish);
            }
            catch { }            

        }

        private void btnZoomMinus_Click(object sender, EventArgs e)
        {
            try
            {
                double xMin = chart1.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                double xMax = chart1.ChartAreas[0].AxisX.ScaleView.ViewMaximum;

                Point zoomPoint = new Point();
                zoomPoint.X = chart1.Location.X + (chart1.ClientRectangle.Width / 2);

                double posXStart = chart1.ChartAreas[0].AxisX.PixelPositionToValue(zoomPoint.X) - (xMax - xMin) / 2;
                double posXFinish = chart1.ChartAreas[0].AxisX.PixelPositionToValue(zoomPoint.X) + (xMax - xMin) / 2;

                chart1.ChartAreas[0].AxisX.ScaleView.Zoom(posXStart, posXFinish);
            }
            catch { }            

        }

        private void btnResetZoom_Click(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset();
            chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset();
        }
    
    } // class
} // namespace
