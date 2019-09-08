Imports T4
Imports T4.API
Imports T4.API.ChartData
Imports System.Xml

Public Class frmMain
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblMarket As System.Windows.Forms.Label
    Friend WithEvents cboMarkets As System.Windows.Forms.ComboBox
    Friend WithEvents lblContract As System.Windows.Forms.Label
    Friend WithEvents lblExchange As System.Windows.Forms.Label
    Friend WithEvents cboContracts As System.Windows.Forms.ComboBox
    Friend WithEvents cboExchanges As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents radDayBars As System.Windows.Forms.RadioButton
    Friend WithEvents radHourBars As System.Windows.Forms.RadioButton
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents DataSet1 As System.Data.DataSet
    Friend WithEvents cmdChartRequest As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.radDayBars = New System.Windows.Forms.RadioButton()
        Me.radHourBars = New System.Windows.Forms.RadioButton()
        Me.cmdChartRequest = New System.Windows.Forms.Button()
        Me.lblMarket = New System.Windows.Forms.Label()
        Me.cboMarkets = New System.Windows.Forms.ComboBox()
        Me.lblContract = New System.Windows.Forms.Label()
        Me.lblExchange = New System.Windows.Forms.Label()
        Me.cboContracts = New System.Windows.Forms.ComboBox()
        Me.cboExchanges = New System.Windows.Forms.ComboBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.DataSet1 = New System.Data.DataSet()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.cmdChartRequest)
        Me.Panel1.Controls.Add(Me.lblMarket)
        Me.Panel1.Controls.Add(Me.cboMarkets)
        Me.Panel1.Controls.Add(Me.lblContract)
        Me.Panel1.Controls.Add(Me.lblExchange)
        Me.Panel1.Controls.Add(Me.cboContracts)
        Me.Panel1.Controls.Add(Me.cboExchanges)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(232, 453)
        Me.Panel1.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.radDayBars)
        Me.GroupBox1.Controls.Add(Me.radHourBars)
        Me.GroupBox1.Location = New System.Drawing.Point(72, 83)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(152, 68)
        Me.GroupBox1.TabIndex = 71
        Me.GroupBox1.TabStop = False
        '
        'radDayBars
        '
        Me.radDayBars.AutoSize = True
        Me.radDayBars.Location = New System.Drawing.Point(7, 44)
        Me.radDayBars.Name = "radDayBars"
        Me.radDayBars.Size = New System.Drawing.Size(68, 17)
        Me.radDayBars.TabIndex = 1
        Me.radDayBars.Text = "Day Bars"
        Me.radDayBars.UseVisualStyleBackColor = True
        '
        'radHourBars
        '
        Me.radHourBars.AutoSize = True
        Me.radHourBars.Checked = True
        Me.radHourBars.Location = New System.Drawing.Point(7, 20)
        Me.radHourBars.Name = "radHourBars"
        Me.radHourBars.Size = New System.Drawing.Size(72, 17)
        Me.radHourBars.TabIndex = 0
        Me.radHourBars.TabStop = True
        Me.radHourBars.Text = "Hour Bars"
        Me.radHourBars.UseVisualStyleBackColor = True
        '
        'cmdChartRequest
        '
        Me.cmdChartRequest.Enabled = False
        Me.cmdChartRequest.Location = New System.Drawing.Point(72, 157)
        Me.cmdChartRequest.Name = "cmdChartRequest"
        Me.cmdChartRequest.Size = New System.Drawing.Size(88, 23)
        Me.cmdChartRequest.TabIndex = 70
        Me.cmdChartRequest.Text = "Load Chart"
        '
        'lblMarket
        '
        Me.lblMarket.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMarket.Location = New System.Drawing.Point(8, 56)
        Me.lblMarket.Name = "lblMarket"
        Me.lblMarket.Size = New System.Drawing.Size(62, 21)
        Me.lblMarket.TabIndex = 69
        Me.lblMarket.Text = "Market:"
        Me.lblMarket.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboMarkets
        '
        Me.cboMarkets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMarkets.Location = New System.Drawing.Point(72, 56)
        Me.cboMarkets.Name = "cboMarkets"
        Me.cboMarkets.Size = New System.Drawing.Size(152, 21)
        Me.cboMarkets.Sorted = True
        Me.cboMarkets.TabIndex = 64
        Me.cboMarkets.TabStop = False
        '
        'lblContract
        '
        Me.lblContract.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContract.Location = New System.Drawing.Point(8, 32)
        Me.lblContract.Name = "lblContract"
        Me.lblContract.Size = New System.Drawing.Size(62, 21)
        Me.lblContract.TabIndex = 68
        Me.lblContract.Text = "Contract:"
        Me.lblContract.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblExchange
        '
        Me.lblExchange.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExchange.Location = New System.Drawing.Point(8, 8)
        Me.lblExchange.Name = "lblExchange"
        Me.lblExchange.Size = New System.Drawing.Size(62, 21)
        Me.lblExchange.TabIndex = 67
        Me.lblExchange.Text = "Exchange:"
        Me.lblExchange.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboContracts
        '
        Me.cboContracts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboContracts.Location = New System.Drawing.Point(72, 32)
        Me.cboContracts.Name = "cboContracts"
        Me.cboContracts.Size = New System.Drawing.Size(152, 21)
        Me.cboContracts.Sorted = True
        Me.cboContracts.TabIndex = 66
        Me.cboContracts.TabStop = False
        '
        'cboExchanges
        '
        Me.cboExchanges.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboExchanges.Location = New System.Drawing.Point(72, 8)
        Me.cboExchanges.Name = "cboExchanges"
        Me.cboExchanges.Size = New System.Drawing.Size(152, 21)
        Me.cboExchanges.Sorted = True
        Me.cboExchanges.TabIndex = 65
        Me.cboExchanges.TabStop = False
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(232, 0)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(712, 453)
        Me.DataGridView1.TabIndex = 1
        '
        'DataSet1
        '
        Me.DataSet1.DataSetName = "NewDataSet"
        '
        'frmMain
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(944, 453)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmMain"
        Me.Text = "T4 Chart"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region " Member Variables "

    ' Reference to the main api host object.
    Private WithEvents moHost As Host

    ' Reference to the exchange list.
    Private WithEvents moExchanges As ExchangeList

    ' Reference to the contract list.
    Private WithEvents moContracts As ContractList

    ' Reference to the market list.
    Private WithEvents moMarkets As MarketList

    ' Refrence to the selected exchange.
    Private WithEvents moSelectedExchange As Exchange

    ' Reference to the selected market.
    Private WithEvents moSelectedContract As Contract

    ' Reference to the selected market.
    Private WithEvents moMarket As Market

    ' Reference to the chart data object that is going to hold and update the chart data we request from the server.
    Private WithEvents moBarData As ChartDataSeries

    ' Used to bind the chart data to the grid.
    Private moTable As DataTable

#End Region

#Region " **** Request and Display Chart Data from the API **** (Look here for the ChartData API details) "

    Private Sub RequestChartData()

        ' Set EndDate to the current trading date.
        Dim dtEndDate As DateTime = moSelectedContract.GetTradeDate(DateTime.Now)
        Dim dtStartDate As DateTime
        Dim enBarInterval As T4.API.ChartData.ChartInterval = ChartInterval.Hour

        If radDayBars.Checked Then


            enBarInterval = ChartInterval.Day

            ' User select Day bars, load a month of them.
            dtStartDate = dtEndDate.AddMonths(-1)

        Else

            enBarInterval = ChartInterval.Hour

            ' User selected Hour bars, load a couple of days of them.
            dtStartDate = dtEndDate

            ' This little loop here will ensure that we load the previous trade date as well as today and will account for weekends
            ' and holidays.
            While (moSelectedContract.GetTradeDate(dtStartDate) = dtEndDate)
                dtStartDate = dtStartDate.AddDays(-1)
            End While

        End If

        ' Create a BarInterval object to tell the API what bar interval we want.
        ' So for example, if we wanted a 15 minute bar, we would do:  New T4.API.ChartData.BarInterval(ChartDataType.Minute, 15)
        Dim oBarIntvl As New T4.API.ChartData.BarInterval(enBarInterval, 1)

        If Not moMarket Is Nothing Then

            ' A market was selected. Load the data for the selected market.

            ' First, create a new ChartDataSeries. This object store the chart data from the server as well as keep it updated as live trades
            ' come in. You REALLY should be careful to ensure that you properly Dispose this object when you are no longer using it.
            moBarData = New T4.API.ChartData.ChartDataSeries(moMarket, oBarIntvl, SessionTimeRange.Empty)

            ' Request the chart data.
            ' LoadRealTimeChartData() takes only a start date. It will load all historical chart data from that date to now and will keep
            '    that data updated as trades get posted to the system.
            moBarData.LoadRealTimeChartData(dtStartDate)

            ' Alternatively, LoadHistoricalChartData() takes a start and end date and simply only load historical chart data. This method does
            '    NOT keep the data updated as live trades come in.
            'moBarData.LoadHistoricalChartData(dtStartDate, dtEndDate)

        Else

            ' A market was not selected. Load "continuation" data.
            ' The default contructor for ContinuationSettings instructs the API to create a long-term historical series of chart data
            '   for the contract by looking at the total traded volume for each day. The market that has the high trade volume is the
            '   market from which data will be pulled for that trade date. This is called a "volume continuation" and is the most common
            '   method of constructing such chart data sets for a contract. There are other types of continuations that you can create, however
            '   they won't be discussed in this example.
            Dim oContinuation As New T4.API.ChartData.ContinuationSettings()

            ' First, create a new ChartDataSeries. This object store the chart data from the server as well as keep it updated as live trades
            ' come in. You REALLY should be careful to ensure that you properly Dispose this object when you are no longer using it.
            moBarData = New T4.API.ChartData.ChartDataSeries(moSelectedContract, oBarIntvl, SessionTimeRange.Empty, oContinuation)

            ' Request the chart data.
            ' LoadRealTimeChartData() takes only a start date. It will load all historical chart data from that date to now and will keep
            '    that data updated as trades get posted to the system.
            moBarData.LoadRealTimeChartData(dtStartDate)

            ' Alternatively, LoadHistoricalChartData() takes a start and end date and simply only load historical chart data. This method does
            '    NOT keep the data updated as live trades come in.
            'moBarData.LoadHistoricalChartData(dtStartDate, dtEndDate)

        End If

    End Sub

    ''' <summary>
    ''' The ChartDataSeries.DataLoadComplete event is raised upon completion of loading historical chart data. 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub moBarData_DataLoadComplete(ByVal sender As Object, ByVal e As T4.API.ChartData.DataLoadCompleteEventArgs) Handles moBarData.DataLoadComplete

        ' NOTE: This event is raised on it's own thread. If you are updating a GUI application with the chart data (like this example), don't forget that you
        '       MUST marshall this update onto the GUI thread before you modify any GUI components.
        ' (This is the same for all T4 API events.)
        If Me.InvokeRequired Then

            ' An invoke is required to update the GUI, simply invoke back to this same mthod.
            Me.BeginInvoke(New T4.API.ChartData.DataLoadCompleteEventHandler(AddressOf moBarData_DataLoadComplete), New Object() {sender, e})

            ' Don't forget to exit now!
            Return

        End If

        ' On the GUI thread now, update the chart.
        If e.Status = DataLoadStatus.Failed Then

            Trace.WriteLine("IChartDataRequest_ChartDataComplete: Chart data request failed.")
            Return

        Else

            ' Sometimes, you won't get all the historical data you requested. This could happen if there isn't chart data available all the back to the start date you requested.
            Trace.WriteLine(String.Format("IChartDataRequest_ChartDataComplete: Status: {0}, Dates Requested: {1}, Dates Received: {2}", e.Status, e.DateRangeRequested, e.DateRangeProcessed))

        End If

        ' Before iterating the data collection, you MUST lock it. This will prevent live trade updates from modifying the collection as you read it.
        moBarData.Lock()

        Try

            For i As Integer = 0 To moBarData.TradeBars.Count - 1

                Dim oBar As T4.API.ChartData.BarDataPoint = DirectCast(moBarData.TradeBars(i), T4.API.ChartData.BarDataPoint)

                Trace.WriteLine(String.Format("TradeDate: {6}, Time: {5}, Open: {0}, High: {1}, Low: {2}, Close: {3}, Volume: {4}", oBar.OpenTicks, oBar.HighTicks, oBar.LowTicks, oBar.CloseTicks, oBar.Volume, oBar.Time, oBar.TradeDate))

                moTable.Rows.Add(
                    i, _
                    oBar.TradeDate, _
                    oBar.Time, _
                    oBar.OpenTicks, _
                    oBar.HighTicks, _
                    oBar.LowTicks, _
                    oBar.CloseTicks, _
                    oBar.Volume)
            Next

            DataGridView1.AutoResizeColumns()

        Catch ex As Exception

            Trace.WriteLine("Error loading chart data: " & ex.ToString())

        Finally

            ' If you don't be sure to unlock the data collection, you probably won't get any live trade updates.
            moBarData.Unlock()

        End Try

    End Sub

    ''' <summary>
    ''' The ChartDataSeries.DataSeriesUpdated event is raised upon the data series being update by a live trade (only raised when the chart data was loaded
    ''' using the LoadRealTimeChartData() method. This event will not be fired more frequently than every 100msec by default.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub moBarData_DataSeriesUpdated(ByVal sender As Object, ByVal e As T4.API.ChartData.DataSeriesUpdatedEventArgs) Handles moBarData.DataSeriesUpdated

        ' NOTE: This event is raised on it's own thread. If you are updating a GUI application with the chart data (like this example), don't forget that you
        '       MUST marshall this update onto the GUI thread before you modify any GUI components.
        ' (This is the same for all T4 API events.)
        If Me.InvokeRequired Then

            ' An invoke is required to update the GUI, simply invoke back to this same mthod.
            Me.BeginInvoke(New T4.API.ChartData.DataSeriesUpdatedEventHandler(AddressOf moBarData_DataSeriesUpdated), New Object() {sender, e})

            ' Don't forget to exit now!
            Return

        End If

        'On the GUI thread now, update the chart.

        ' Before iterating the data collection, you MUST lock it. This will prevent live trade updates from modifying the collection as you read it.
        moBarData.Lock()

        Try
            'e.TradeBarIndexUpdate is the index of the first bar that was added or updated since the last update event.
            For i As Integer = e.TradeBarIndexUpdate To moBarData.TradeBars.Count - 1

                Dim oBar As T4.API.ChartData.BarDataPoint = DirectCast(moBarData.TradeBars(i), T4.API.ChartData.BarDataPoint)

                If i < moTable.Rows.Count Then

                    ' Update the chart series.
                    Dim oRow As DataRow = moTable.Rows(i)
                    oRow.ItemArray = New Object() {
                                                    i, _
                                                    oBar.TradeDate, _
                                                    oBar.Time, _
                                                    oBar.OpenTicks, _
                                                    oBar.HighTicks, _
                                                    oBar.LowTicks, _
                                                    oBar.CloseTicks, _
                                                    oBar.Volume}

                Else

                    ' Add a new bar.
                    moTable.Rows.Add(
                        i, _
                        oBar.TradeDate, _
                        oBar.Time, _
                        oBar.OpenTicks, _
                        oBar.HighTicks, _
                        oBar.LowTicks, _
                        oBar.CloseTicks, _
                        oBar.Volume)

                End If

            Next

        Catch ex As Exception

            Trace.WriteLine("Error updating chart data: " & ex.ToString())

        Finally

            ' If you don't be sure to unlock the data collection, you probably won't get any live trade updates.
            moBarData.Unlock()

        End Try

    End Sub

#End Region

#Region " Form and Control Events "

    ''' <summary>
    ''' Display the login dialog.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' For sim:
        moHost = Host.Login(APIServerType.Simulator, "T4Example", "112A04B0-5AAF-42F4-994E-FA7CB959C60B")
        ' For live:
        'moHost = Host.Login( APIServerType.Live, "T4Example", "112A04B0-5AAF-42F4-994E-FA7CB959C60B")

        ConfigureGridControl()

        If Not moHost Is Nothing Then

            ' We will have missed this event so call it explicitly.
            PostLogin()

        Else
            Me.Close()
        End If

    End Sub

    ''' <summary>
    ''' Dispose the API host object to cleanly exit.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmMain_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

        If Not moHost Is Nothing Then

            moHost.Dispose()

        End If

    End Sub

    ''' <summary>
    ''' An exchange was selected by the user.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboExchanges_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboExchanges.SelectedIndexChanged

        ' Clear the contract and market lists.
        cboContracts.Items.Clear()
        cboContracts.Enabled = False
        cboMarkets.Items.Clear()
        cboMarkets.Enabled = False
        cmdChartRequest.Enabled = False

        If (cboExchanges.SelectedItem Is Nothing) Then
            ' Invalid exchange selection.
            Return
        End If

        ' Remember the selected exchange.
        moSelectedExchange = DirectCast(cboExchanges.SelectedItem, ExchangeItem).Exchange

        ' Load the contract list for the exchange.
        moContracts = moSelectedExchange.Contracts

        If moContracts.Complete Then

            ' The contract list for the selected exchange is already loaded, add them to the contracts combo.
            ContractListComplete()

        End If

    End Sub

    ''' <summary>
    ''' A contract was selected by the user.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboContracts_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboContracts.SelectedIndexChanged

        ' Clear the market combo.
        cboMarkets.Items.Clear()
        cboMarkets.Enabled = False
        cmdChartRequest.Enabled = False

        If cboContracts.SelectedItem Is Nothing Then
            ' Invalid contract selection.
            Return
        End If

        cmdChartRequest.Enabled = True

        ' Remember the selected contract.
        moSelectedContract = DirectCast(cboContracts.SelectedItem, ContractItem).Contract

        ' Load the market list for the contract. This is done with a market fileter so as to prevent loading spread markets.
        ' Spread chart data is not yet available through the T4 API.
        moMarkets = moHost.MarketData.CreateMarketFilter(moSelectedExchange.ExchangeID, moSelectedContract.ContractID, 0, ContractType.Any, StrategyType.None)

        If moMarkets.Complete Then

            ' The market list for the selected market is already loaded, add them to the markets combo.
            MarketListComplete()

        End If
    End Sub

    ''' <summary>
    ''' A market was selected by the user.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboMarkets_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMarkets.SelectedIndexChanged

        If Not cboMarkets.SelectedItem Is Nothing Then

            ' Save the selected market.
            moMarket = DirectCast(cboMarkets.SelectedItem, MarketItem).Market

        End If

    End Sub

    ''' <summary>
    ''' The Load button was clicked by the user.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdChartRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChartRequest.Click

        ' Configure the chart control.
        ConfigureGridControl()

        ' Request chart data.
        RequestChartData()

    End Sub

#End Region

#Region " Operations "

    Private Sub ExchangeListComplete()

        ' Populate the list of exchanges.

        If Not moExchanges Is Nothing Then

            Try

                ' First clear all the combo's.
                cboExchanges.Items.Clear()
                cboContracts.Items.Clear()
                cboMarkets.Items.Clear()

                cboExchanges.Enabled = True
                cboContracts.Enabled = False
                cboMarkets.Enabled = False

                ' Lock the API while traversing the api collection.
                ' Lock at the lowest level object for the shortest period of time.
                moExchanges.EnterLock()

                ' Add the exchanges to the dropdown list.
                For Each oExchange As Exchange In moExchanges
                    cboExchanges.Items.Add(New ExchangeItem(oExchange))
                Next

            Catch ex As Exception

                ' Trace the error.
                Trace.WriteLine("Error " & ex.ToString)

            Finally

                ' This is guarenteed to execute last.
                moExchanges.ExitLock()

            End Try

        End If

    End Sub

    Private Sub ContractListComplete()

        If Not moContracts Is Nothing Then

            Try

                ' Clear the contract combo and enable it.
                cboContracts.Items.Clear()
                cboContracts.Enabled = True


                ' Lock the API while iterating contract list.
                moContracts.EnterLock()

                ' Add the contracts to the combo.
                For Each oContract As Contract In moContracts
                    cboContracts.Items.Add(New ContractItem(oContract))
                Next

            Catch ex As Exception

                ' Trace the error.
                Trace.WriteLine("Error " & ex.ToString)

            Finally

                moContracts.ExitLock()

            End Try

        End If

    End Sub

    Private Sub MarketListComplete()

        If Not moMarkets Is Nothing Then

            ' Lock the API while iterating market list.
            moMarkets.EnterLock()

            Try

                ' Clear the market combo and enable it.
                cboMarkets.Items.Clear()
                cboMarkets.Enabled = True
                cboMarkets.Items.Add(New MarketItem(Nothing))

                For Each oMarket As Market In moMarkets
                    cboMarkets.Items.Add(New MarketItem(oMarket))
                Next

            Catch ex As Exception

                ' Trace the error.
                Trace.WriteLine("Error " & ex.ToString)

            Finally

                moMarkets.ExitLock()

            End Try

        End If

    End Sub

    Private Sub ConfigureGridControl()

        DataSet1.Tables.Clear()

        moTable = DataSet1.Tables.Add("BarData")
        moTable.Columns.Add("Index", GetType(Integer))
        moTable.Columns.Add("TradeDate", GetType(DateTime))
        moTable.Columns.Add("Time", GetType(DateTime))
        moTable.Columns.Add("Open", GetType(Double))
        moTable.Columns.Add("High", GetType(Double))
        moTable.Columns.Add("Low", GetType(Double))
        moTable.Columns.Add("Close", GetType(Double))
        moTable.Columns.Add("Volume", GetType(Integer))

        DataGridView1.DataSource = moTable

        DataGridView1.AutoResizeColumns()

        DataGridView1.Sort(DataGridView1.Columns("Time"), System.ComponentModel.ListSortDirection.Descending)

    End Sub

#End Region

#Region " API Events "

    ' Run the application post successful login.
    Private Sub PostLogin()

        Trace.WriteLine("Login Success")

        ' Check to see if the user has chart data permission.
        If Not (moHost.IsInRole("Charting") OrElse moHost.IsInRole("BasicCharting")) Then

            MsgBox("Your user does not have Charts or Advanced Charting roles set. Please contact your administrator to enable them.")

            ' End the application.
            Me.Close()

        End If

        ' Populate the available exchanges.
        moExchanges = moHost.MarketData.Exchanges

        ' Check to see if the data is already loaded.
        If moExchanges.Complete Then

            ' Call the event handler ourselves as the data is 
            ' already loaded.
            moExchanges_ExchangeListComplete(moExchanges)

        End If

    End Sub

    ' Event raised if login failed.
    Private Sub moHost_LoginFailure(ByVal penReason As LoginResult) Handles moHost.LoginFailure

        Trace.WriteLine("Login Failed due to " & penReason.ToString)

    End Sub

    Private Sub moExchanges_ExchangeListComplete(ByVal poExchangeList As T4.API.ExchangeList) Handles moExchanges.ExchangeListComplete

        ' Invoke the update to the GUI thread.
        If Me.IsHandleCreated Then
            Me.BeginInvoke(New MethodInvoker(AddressOf ExchangeListComplete))
        Else
            ExchangeListComplete()
        End If

    End Sub


    Private Sub moContracts_ContractListComplete(ByVal poContractList As T4.API.ContractList) Handles moContracts.ContractListComplete

        ' Invoke the update to the GUI thread.
        If Me.InvokeRequired Then
            Me.BeginInvoke(New MethodInvoker(AddressOf ContractListComplete))
        Else
            ExchangeListComplete()
        End If

    End Sub

    Private Sub moMarkets_MarketListComplete(ByVal poMarketList As T4.API.MarketList) Handles moMarkets.MarketListComplete

        ' Invoke the update to the GUI thread.
        If Me.InvokeRequired Then
            Me.BeginInvoke(New MethodInvoker(AddressOf MarketListComplete))
        Else
            ExchangeListComplete()
        End If

    End Sub

#End Region

#Region " Helper Classes "

    ''' <summary>
    ''' Class to hold an exchange refernce in a drop down list.
    ''' </summary>
    ''' <remarks></remarks>
    Private Class ExchangeItem

        ''' <summary>
        ''' Reference to the exchange.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Exchange As Exchange

        ''' <summary>
        ''' Constructor.
        ''' </summary>
        ''' <param name="poExchange"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal poExchange As Exchange)

            Exchange = poExchange

        End Sub

        ''' <summary>
        ''' Display the exchange description.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overrides Function ToString() As String
            Return Exchange.Description
        End Function

    End Class

    ''' <summary>
    ''' Class to hold a contract refernce in a drop down list.
    ''' </summary>
    ''' <remarks></remarks>
    Private Class ContractItem

        ''' <summary>
        ''' Reference to the contract.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Contract As Contract

        ''' <summary>
        ''' Constructor.
        ''' </summary>
        ''' <param name="poContract"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal poContract As Contract)

            Contract = poContract

        End Sub

        ''' <summary>
        ''' Display the contract description.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overrides Function ToString() As String
            Return Contract.Description
        End Function

    End Class

    ''' <summary>
    ''' Class to hold an Market refernce in a drop down list.
    ''' </summary>
    ''' <remarks></remarks>
    Private Class MarketItem

        ''' <summary>
        ''' Reference to the Market.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Market As Market

        ''' <summary>
        ''' Constructor.
        ''' </summary>
        ''' <param name="poMarket"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal poMarket As Market)

            Market = poMarket

        End Sub

        ''' <summary>
        ''' Display the Market description.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overrides Function ToString() As String
            If Market Is Nothing Then
                Return ""
            Else
                Return Market.Description
            End If
        End Function

    End Class

#End Region

End Class
