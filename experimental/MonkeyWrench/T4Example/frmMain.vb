' Import the T4 definitions namespace.
Imports T4

' Import the API namespace.
Imports T4.API

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
    Friend WithEvents cmdSubmitOrder As System.Windows.Forms.Button
    Friend WithEvents cmdReviseOrder As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdSelectAccount As System.Windows.Forms.Button
    Friend WithEvents cmdSelectMarket As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmdCancelOrder As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdSubmitOrder = New System.Windows.Forms.Button()
        Me.cmdReviseOrder = New System.Windows.Forms.Button()
        Me.cmdCancelOrder = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdSelectAccount = New System.Windows.Forms.Button()
        Me.cmdSelectMarket = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'cmdSubmitOrder
        '
        Me.cmdSubmitOrder.Location = New System.Drawing.Point(68, 214)
        Me.cmdSubmitOrder.Name = "cmdSubmitOrder"
        Me.cmdSubmitOrder.Size = New System.Drawing.Size(96, 23)
        Me.cmdSubmitOrder.TabIndex = 0
        Me.cmdSubmitOrder.Text = "Submit Order"
        '
        'cmdReviseOrder
        '
        Me.cmdReviseOrder.Location = New System.Drawing.Point(170, 214)
        Me.cmdReviseOrder.Name = "cmdReviseOrder"
        Me.cmdReviseOrder.Size = New System.Drawing.Size(96, 23)
        Me.cmdReviseOrder.TabIndex = 1
        Me.cmdReviseOrder.Text = "Revise Order"
        '
        'cmdCancelOrder
        '
        Me.cmdCancelOrder.Location = New System.Drawing.Point(272, 214)
        Me.cmdCancelOrder.Name = "cmdCancelOrder"
        Me.cmdCancelOrder.Size = New System.Drawing.Size(96, 23)
        Me.cmdCancelOrder.TabIndex = 2
        Me.cmdCancelOrder.Text = "Cancel Order"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(447, 43)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "This example project shows the basics of logging into the API, getting market and" & _
    " account data and submitting, revising and cancelling an order."
        '
        'cmdSelectAccount
        '
        Me.cmdSelectAccount.Location = New System.Drawing.Point(170, 82)
        Me.cmdSelectAccount.Name = "cmdSelectAccount"
        Me.cmdSelectAccount.Size = New System.Drawing.Size(96, 23)
        Me.cmdSelectAccount.TabIndex = 4
        Me.cmdSelectAccount.Text = "Select Account"
        '
        'cmdSelectMarket
        '
        Me.cmdSelectMarket.Location = New System.Drawing.Point(170, 118)
        Me.cmdSelectMarket.Name = "cmdSelectMarket"
        Me.cmdSelectMarket.Size = New System.Drawing.Size(96, 23)
        Me.cmdSelectMarket.TabIndex = 5
        Me.cmdSelectMarket.Text = "Select Market"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(12, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(447, 30)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Output from your actions appears in Trace and the Studio debugger Output window."
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(12, 82)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(152, 23)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "First, select an account:"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(12, 123)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(152, 23)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Now, select a market:"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(12, 169)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(447, 42)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Submit an order (details are hardcoded), then revise or cancel it if it doesn't f" & _
    "ill. See the code behind the Submit Order button to change the order details"
        '
        'frmMain
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(471, 264)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmdSelectMarket)
        Me.Controls.Add(Me.cmdSelectAccount)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmdCancelOrder)
        Me.Controls.Add(Me.cmdReviseOrder)
        Me.Controls.Add(Me.cmdSubmitOrder)
        Me.Name = "frmMain"
        Me.Text = "T4Example"
        Me.ResumeLayout(False)

    End Sub

#End Region

    ''' <summary>
    ''' Reference to the main api host object.
    ''' </summary>
    ''' <remarks></remarks>
    Private WithEvents moHost As Host

    ''' <summary>
    ''' Reference to a market.
    ''' </summary>
    ''' <remarks></remarks>
    Private WithEvents moMarket As Market

    ''' <summary>
    ''' Reference to an account.
    ''' </summary>
    ''' <remarks></remarks>
    Private WithEvents moAccount As Account

    ''' <summary>
    ''' Reference to a submitted order.
    ''' </summary>
    ''' <remarks></remarks>
    Private WithEvents moOrder As Order

#Region " Startup and shutdown code "

    ''' <summary>
    ''' Initialise the api when the application starts.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' Create the api host object using the built in Login dialog.
        moHost = Host.Login(APIServerType.Simulator, "T4Example", "112A04B0-5AAF-42F4-994E-FA7CB959C60B")

        ' Check for success.
        If moHost Is Nothing Then

            ' Host object not returned which means the user cancelled the login dialog.
            Me.Close()

        Else

            ' Login was successfull.
            Trace.WriteLine("Login Success")

        End If

    End Sub

    ''' <summary>
    ''' Shutdown the api when the application exits.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmMain_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed

        ' Check to see that we have an api object.
        If Not moHost Is Nothing Then

            ' Dispose of the api.
            moHost.Dispose()
            moHost = Nothing

        End If

    End Sub

#End Region

#Region " Login Result"

    ''' <summary>
    ''' Event raised if login failed. When using the Host.Login method to login this will only be raised in the event of a disconnection from the server.
    ''' </summary>
    ''' <param name="penReason"></param>
    ''' <remarks></remarks>
    Private Sub moHost_LoginFailure(ByVal penReason As LoginResult) Handles moHost.LoginFailure

        Trace.WriteLine(String.Format("Login Failed due to {0}", penReason.ToString))

    End Sub

    ''' <summary>
    ''' Event raised if login is successful. When using the Host.Login method to login this will ONLY be raised in the event of a reconnection to the server.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub moHost_LoginSuccess() Handles moHost.LoginSuccess

        ' Login was successfull.
        Trace.WriteLine("Login Success")

        ' Nothing else needs to be done here when Host.Login is used to login - any market and account subscriptions active when disconnection occurred
        ' will automatically be restored. 

    End Sub

#End Region

#Region " Getting Market Data "

    ''' <summary>
    ''' Allow the user to select a market, then subscribe to it.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub cmdSelectMarket_Click(sender As System.Object, e As System.EventArgs) Handles cmdSelectMarket.Click

        ' Display the market picker to the user for them to select a market.
        moMarket = moHost.MarketData.MarketPicker(moMarket)

        ' Check to see if a market was selected.
        If Not moMarket Is Nothing Then

            ' Subscribe to the market.
            moMarket.DepthSubscribe()

        End If

    End Sub

    ''' <summary>
    ''' Event raised when there is a new depth update for the market.
    ''' </summary>
    ''' <param name="poMarket"></param>
    ''' <remarks></remarks>
    Private Sub moMarket_MarketDepthUpdate(ByVal poMarket As T4.API.Market) Handles moMarket.MarketDepthUpdate

        ' Display the last trade price and volume.
        Trace.WriteLine(String.Format("DepthUpdate: {0}, LastTrade: {1}@{2}", poMarket.Description, poMarket.LastDepth.LastTradeVolume, poMarket.ConvertTicksDisplay(poMarket.LastDepth.LastTradeTicks)))

    End Sub

#End Region

#Region " Account Data "

    ''' <summary>
    ''' Allow the user to select an account.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub cmdSelectAccount_Click(sender As System.Object, e As System.EventArgs) Handles cmdSelectAccount.Click

        ' Display the account picker to the user and allow them to select an account.
        moAccount = moHost.Accounts.AccountPicker(moAccount)

        ' Check to see if the user selected anything.
        If Not moAccount Is Nothing Then

            ' Subscribe to the account.
            moAccount.Subscribe()

        End If

    End Sub

    ''' <summary>
    ''' Event that is raised when the accounts overall balance, P&amp;L or margin details have changed.
    ''' </summary>
    ''' <param name="poAccount"></param>
    ''' <remarks></remarks>
    Private Sub moAccount_AccountUpdate(ByVal poAccount As Account) Handles moAccount.AccountUpdate

        ' Display the account balance.
        Trace.WriteLine(String.Format("Account: {0}, Balance: {1}", poAccount.Description, poAccount.Balance))

    End Sub

    ''' <summary>
    ''' Event that is raised when positions for the account have changed.
    ''' </summary>
    ''' <param name="poAccount"></param>
    ''' <param name="poPosition"></param>
    ''' <remarks></remarks>
    Private Sub moAccount_PositionUpdate(poAccount As Account, ByVal poPosition As Position) Handles moAccount.PositionUpdate

        ' Display the account, market and net position.
        Trace.WriteLine(String.Format("Account: {0}, Market: {1}, Net: {2}", poAccount.Description, poPosition.Market.Description, poPosition.Net))

    End Sub

#End Region

#Region " Order Handling"

    ''' <summary>
    ''' Submit a single order into the previously selected market and account.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdSubmitOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSubmitOrder.Click

        ' Check that we have an account and market to submit with.
        If Not moMarket Is Nothing AndAlso Not moAccount Is Nothing Then

            ' Use the settlement price of the market for the order limit price.
            Dim iTicks As Integer = moMarket.LastSettlement.Ticks

            ' Submit a buy limit order for 1 lot at the last settlement price.
            moOrder = moAccount.SubmitNewOrder(moMarket, BuySell.Buy, PriceType.Limit, TimeType.Normal, 1, iTicks)

        End If

    End Sub

    ''' <summary>
    ''' Revise the previously submitted order.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdReviseOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReviseOrder.Click

        ' Check to see that we have an order.
        If Not moOrder Is Nothing Then

            ' Check to see if the order is working.
            If moOrder.IsWorking Then

                ' Revise the order to be a 2 lot with the same price.
                moOrder.ReviseTicks(2, moOrder.CurrentLimitTicks)

            End If

        End If

    End Sub

    ''' <summary>
    ''' Pull the single order that was submitted.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdCancelOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancelOrder.Click

        ' Check to see that we have an order.
        If Not moOrder Is Nothing Then

            ' Check to see if the order is working.
            If moOrder.IsWorking Then

                ' Pull the order.
                moOrder.Pull()

            End If

        End If

    End Sub

    ''' <summary>
    ''' Event raised when the order has been updated.
    ''' </summary>
    ''' <param name="poOrder"></param>
    ''' <remarks></remarks>
    Private Sub moOrder_OrderUpdate(ByVal poOrder As T4.API.Order) Handles moOrder.OrderUpdate

        ' Display some of the order details.
        Trace.WriteLine(String.Format("OrderUpdate: {0}, Status: {1}, {2}, Change: {3}", poOrder.UniqueID, poOrder.Status.ToString, poOrder.StatusDetail, poOrder.Change.ToString))

    End Sub

    ''' <summary>
    ''' Event raised when the order has received a fill.
    ''' </summary>
    ''' <param name="poOrder"></param>
    ''' <param name="poTrade"></param>
    ''' <remarks></remarks>
    Private Sub moOrder_OrderFill(ByVal poOrder As T4.API.Order, ByVal poTrade As T4.API.Order.Trade) Handles moOrder.OrderFill

        ' Display some of the fill details.
        Trace.WriteLine(String.Format("OrderUpdate: {0}, Fill: {1}@{2}", poOrder.UniqueID, poTrade.Volume, poOrder.Market.ConvertTicksDisplay(poTrade.Ticks)))

    End Sub

#End Region

End Class
