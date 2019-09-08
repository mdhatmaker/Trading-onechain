' Import the T4 definitions namespace.
Imports T4

' Import the API namespace.
Imports T4.API

' Import XML for saving and retriving markets.
Imports System.Xml

' Generic collections.
Imports System.Collections.Generic

' API specific initialization occurs in the moHost_LoginSuccess routin.
' No data can be pulled from the API without a successfull login.
Public Class frmMain
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        SetupMiscExamples()

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
    Friend WithEvents cboMarkets As System.Windows.Forms.ComboBox
    Friend WithEvents cboExchanges As System.Windows.Forms.ComboBox
    Friend WithEvents cboContracts As System.Windows.Forms.ComboBox
    Friend WithEvents lblExchange As System.Windows.Forms.Label
    Friend WithEvents lblContract As System.Windows.Forms.Label
    Friend WithEvents lblMarket As System.Windows.Forms.Label
    Friend WithEvents txtMarketDescription1 As System.Windows.Forms.TextBox
    Friend WithEvents txtBid1 As System.Windows.Forms.TextBox
    Friend WithEvents txtOffer1 As System.Windows.Forms.TextBox
    Friend WithEvents txtBidVol1 As System.Windows.Forms.TextBox
    Friend WithEvents txtOfferVol1 As System.Windows.Forms.TextBox
    Friend WithEvents txtLastVol1 As System.Windows.Forms.TextBox
    Friend WithEvents txtLast1 As System.Windows.Forms.TextBox
    Friend WithEvents txtLastVolTotal1 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblBidPrice As System.Windows.Forms.Label
    Friend WithEvents lblOfferPrice As System.Windows.Forms.Label
    Friend WithEvents lblLastPrice As System.Windows.Forms.Label
    Friend WithEvents lblBidVol As System.Windows.Forms.Label
    Friend WithEvents lblOfferVol As System.Windows.Forms.Label
    Friend WithEvents lblLastVol As System.Windows.Forms.Label
    Friend WithEvents lblTotalVol As System.Windows.Forms.Label
    Friend WithEvents txtLastVolTotal2 As System.Windows.Forms.TextBox
    Friend WithEvents txtLastVol2 As System.Windows.Forms.TextBox
    Friend WithEvents txtLast2 As System.Windows.Forms.TextBox
    Friend WithEvents txtOfferVol2 As System.Windows.Forms.TextBox
    Friend WithEvents txtBidVol2 As System.Windows.Forms.TextBox
    Friend WithEvents txtOffer2 As System.Windows.Forms.TextBox
    Friend WithEvents txtBid2 As System.Windows.Forms.TextBox
    Friend WithEvents txtMarketDescription2 As System.Windows.Forms.TextBox
    Friend WithEvents cmdGet1 As System.Windows.Forms.Button
    Friend WithEvents cmdGet2 As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents lblAccount As System.Windows.Forms.Label
    Friend WithEvents cboAccounts As System.Windows.Forms.ComboBox
    Friend WithEvents txtNet1 As System.Windows.Forms.TextBox
    Friend WithEvents txtNet2 As System.Windows.Forms.TextBox
    Friend WithEvents lblNet As System.Windows.Forms.Label
    Friend WithEvents lblBuys As System.Windows.Forms.Label
    Friend WithEvents lblSells As System.Windows.Forms.Label
    Friend WithEvents txtBuys1 As System.Windows.Forms.TextBox
    Friend WithEvents txtBuys2 As System.Windows.Forms.TextBox
    Friend WithEvents txtSells1 As System.Windows.Forms.TextBox
    Friend WithEvents txtSells2 As System.Windows.Forms.TextBox
    Friend WithEvents cmdBuy1 As System.Windows.Forms.Button
    Friend WithEvents cmdBuy2 As System.Windows.Forms.Button
    Friend WithEvents cmdSell1 As System.Windows.Forms.Button
    Friend WithEvents lstOrders As System.Windows.Forms.ListBox
    Friend WithEvents cmdSell2 As System.Windows.Forms.Button
    Friend WithEvents txtCash As System.Windows.Forms.TextBox
    Friend WithEvents lblCash As System.Windows.Forms.Label
    Friend WithEvents grpAccount As System.Windows.Forms.GroupBox
    Friend WithEvents grpOrders As System.Windows.Forms.GroupBox
    Friend WithEvents lblSaveInfo As System.Windows.Forms.Label
    Friend WithEvents lblOrderInfo As System.Windows.Forms.Label
    Friend WithEvents lblAccountInfo As System.Windows.Forms.Label
    Friend WithEvents cboMisc2 As System.Windows.Forms.ComboBox
    Friend WithEvents lblMisc2 As System.Windows.Forms.Label
    Friend WithEvents cmdRunMisc2 As System.Windows.Forms.Button
    Friend WithEvents lblMisc1 As System.Windows.Forms.Label
    Friend WithEvents cmdRunMisc1 As System.Windows.Forms.Button
    Friend WithEvents grpMarket1 As System.Windows.Forms.GroupBox
    Friend WithEvents grpMarket2 As System.Windows.Forms.GroupBox
    Friend WithEvents cboMisc1 As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.cboMarkets = New System.Windows.Forms.ComboBox()
        Me.cboExchanges = New System.Windows.Forms.ComboBox()
        Me.cboContracts = New System.Windows.Forms.ComboBox()
        Me.lblMarket = New System.Windows.Forms.Label()
        Me.lblContract = New System.Windows.Forms.Label()
        Me.lblExchange = New System.Windows.Forms.Label()
        Me.cmdGet1 = New System.Windows.Forms.Button()
        Me.txtMarketDescription1 = New System.Windows.Forms.TextBox()
        Me.txtBid1 = New System.Windows.Forms.TextBox()
        Me.txtOffer1 = New System.Windows.Forms.TextBox()
        Me.txtBidVol1 = New System.Windows.Forms.TextBox()
        Me.txtOfferVol1 = New System.Windows.Forms.TextBox()
        Me.txtLastVol1 = New System.Windows.Forms.TextBox()
        Me.txtLast1 = New System.Windows.Forms.TextBox()
        Me.txtLastVolTotal1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblBidPrice = New System.Windows.Forms.Label()
        Me.lblOfferPrice = New System.Windows.Forms.Label()
        Me.lblLastPrice = New System.Windows.Forms.Label()
        Me.lblBidVol = New System.Windows.Forms.Label()
        Me.lblOfferVol = New System.Windows.Forms.Label()
        Me.lblLastVol = New System.Windows.Forms.Label()
        Me.lblTotalVol = New System.Windows.Forms.Label()
        Me.txtLastVolTotal2 = New System.Windows.Forms.TextBox()
        Me.txtLastVol2 = New System.Windows.Forms.TextBox()
        Me.txtLast2 = New System.Windows.Forms.TextBox()
        Me.txtOfferVol2 = New System.Windows.Forms.TextBox()
        Me.txtBidVol2 = New System.Windows.Forms.TextBox()
        Me.txtOffer2 = New System.Windows.Forms.TextBox()
        Me.txtBid2 = New System.Windows.Forms.TextBox()
        Me.txtMarketDescription2 = New System.Windows.Forms.TextBox()
        Me.cmdGet2 = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.lblAccount = New System.Windows.Forms.Label()
        Me.cboAccounts = New System.Windows.Forms.ComboBox()
        Me.txtCash = New System.Windows.Forms.TextBox()
        Me.lblCash = New System.Windows.Forms.Label()
        Me.txtNet1 = New System.Windows.Forms.TextBox()
        Me.txtNet2 = New System.Windows.Forms.TextBox()
        Me.lblNet = New System.Windows.Forms.Label()
        Me.lblBuys = New System.Windows.Forms.Label()
        Me.lblSells = New System.Windows.Forms.Label()
        Me.txtBuys1 = New System.Windows.Forms.TextBox()
        Me.txtBuys2 = New System.Windows.Forms.TextBox()
        Me.txtSells1 = New System.Windows.Forms.TextBox()
        Me.txtSells2 = New System.Windows.Forms.TextBox()
        Me.cmdBuy1 = New System.Windows.Forms.Button()
        Me.cmdBuy2 = New System.Windows.Forms.Button()
        Me.cmdSell1 = New System.Windows.Forms.Button()
        Me.cmdSell2 = New System.Windows.Forms.Button()
        Me.lstOrders = New System.Windows.Forms.ListBox()
        Me.grpAccount = New System.Windows.Forms.GroupBox()
        Me.lblAccountInfo = New System.Windows.Forms.Label()
        Me.grpOrders = New System.Windows.Forms.GroupBox()
        Me.lblOrderInfo = New System.Windows.Forms.Label()
        Me.lblMisc1 = New System.Windows.Forms.Label()
        Me.cmdRunMisc1 = New System.Windows.Forms.Button()
        Me.cboMisc1 = New System.Windows.Forms.ComboBox()
        Me.lblMisc2 = New System.Windows.Forms.Label()
        Me.cmdRunMisc2 = New System.Windows.Forms.Button()
        Me.cboMisc2 = New System.Windows.Forms.ComboBox()
        Me.lblSaveInfo = New System.Windows.Forms.Label()
        Me.grpMarket1 = New System.Windows.Forms.GroupBox()
        Me.grpMarket2 = New System.Windows.Forms.GroupBox()
        Me.grpAccount.SuspendLayout()
        Me.grpOrders.SuspendLayout()
        Me.grpMarket1.SuspendLayout()
        Me.grpMarket2.SuspendLayout()
        Me.SuspendLayout()
        '
        'cboMarkets
        '
        Me.cboMarkets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMarkets.Location = New System.Drawing.Point(90, 67)
        Me.cboMarkets.Name = "cboMarkets"
        Me.cboMarkets.Size = New System.Drawing.Size(257, 21)
        Me.cboMarkets.TabIndex = 6
        Me.cboMarkets.TabStop = False
        '
        'cboExchanges
        '
        Me.cboExchanges.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboExchanges.Location = New System.Drawing.Point(90, 19)
        Me.cboExchanges.Name = "cboExchanges"
        Me.cboExchanges.Size = New System.Drawing.Size(257, 21)
        Me.cboExchanges.Sorted = True
        Me.cboExchanges.TabIndex = 7
        Me.cboExchanges.TabStop = False
        '
        'cboContracts
        '
        Me.cboContracts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboContracts.Location = New System.Drawing.Point(90, 43)
        Me.cboContracts.Name = "cboContracts"
        Me.cboContracts.Size = New System.Drawing.Size(257, 21)
        Me.cboContracts.Sorted = True
        Me.cboContracts.TabIndex = 8
        Me.cboContracts.TabStop = False
        '
        'lblMarket
        '
        Me.lblMarket.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMarket.Location = New System.Drawing.Point(10, 66)
        Me.lblMarket.Name = "lblMarket"
        Me.lblMarket.Size = New System.Drawing.Size(78, 21)
        Me.lblMarket.TabIndex = 11
        Me.lblMarket.Text = "Market:"
        Me.lblMarket.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblContract
        '
        Me.lblContract.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContract.Location = New System.Drawing.Point(8, 42)
        Me.lblContract.Name = "lblContract"
        Me.lblContract.Size = New System.Drawing.Size(80, 21)
        Me.lblContract.TabIndex = 10
        Me.lblContract.Text = "Contract:"
        Me.lblContract.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblExchange
        '
        Me.lblExchange.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExchange.Location = New System.Drawing.Point(14, 18)
        Me.lblExchange.Name = "lblExchange"
        Me.lblExchange.Size = New System.Drawing.Size(74, 21)
        Me.lblExchange.TabIndex = 9
        Me.lblExchange.Text = "Exchange:"
        Me.lblExchange.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmdGet1
        '
        Me.cmdGet1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGet1.Location = New System.Drawing.Point(311, 94)
        Me.cmdGet1.Name = "cmdGet1"
        Me.cmdGet1.Size = New System.Drawing.Size(36, 20)
        Me.cmdGet1.TabIndex = 10
        Me.cmdGet1.TabStop = False
        Me.cmdGet1.Text = "Get"
        '
        'txtMarketDescription1
        '
        Me.txtMarketDescription1.BackColor = System.Drawing.Color.White
        Me.txtMarketDescription1.Location = New System.Drawing.Point(11, 140)
        Me.txtMarketDescription1.Name = "txtMarketDescription1"
        Me.txtMarketDescription1.ReadOnly = True
        Me.txtMarketDescription1.Size = New System.Drawing.Size(208, 20)
        Me.txtMarketDescription1.TabIndex = 11
        Me.txtMarketDescription1.TabStop = False
        '
        'txtBid1
        '
        Me.txtBid1.BackColor = System.Drawing.Color.LightCyan
        Me.txtBid1.Location = New System.Drawing.Point(221, 140)
        Me.txtBid1.Name = "txtBid1"
        Me.txtBid1.ReadOnly = True
        Me.txtBid1.Size = New System.Drawing.Size(60, 20)
        Me.txtBid1.TabIndex = 12
        Me.txtBid1.TabStop = False
        Me.txtBid1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtOffer1
        '
        Me.txtOffer1.BackColor = System.Drawing.Color.MistyRose
        Me.txtOffer1.Location = New System.Drawing.Point(313, 140)
        Me.txtOffer1.Name = "txtOffer1"
        Me.txtOffer1.ReadOnly = True
        Me.txtOffer1.Size = New System.Drawing.Size(60, 20)
        Me.txtOffer1.TabIndex = 14
        Me.txtOffer1.TabStop = False
        Me.txtOffer1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBidVol1
        '
        Me.txtBidVol1.BackColor = System.Drawing.Color.LightCyan
        Me.txtBidVol1.Location = New System.Drawing.Point(283, 140)
        Me.txtBidVol1.Name = "txtBidVol1"
        Me.txtBidVol1.ReadOnly = True
        Me.txtBidVol1.Size = New System.Drawing.Size(28, 20)
        Me.txtBidVol1.TabIndex = 16
        Me.txtBidVol1.TabStop = False
        Me.txtBidVol1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtOfferVol1
        '
        Me.txtOfferVol1.BackColor = System.Drawing.Color.MistyRose
        Me.txtOfferVol1.Location = New System.Drawing.Point(375, 140)
        Me.txtOfferVol1.Name = "txtOfferVol1"
        Me.txtOfferVol1.ReadOnly = True
        Me.txtOfferVol1.Size = New System.Drawing.Size(28, 20)
        Me.txtOfferVol1.TabIndex = 17
        Me.txtOfferVol1.TabStop = False
        Me.txtOfferVol1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtLastVol1
        '
        Me.txtLastVol1.BackColor = System.Drawing.Color.Honeydew
        Me.txtLastVol1.Location = New System.Drawing.Point(467, 140)
        Me.txtLastVol1.Name = "txtLastVol1"
        Me.txtLastVol1.ReadOnly = True
        Me.txtLastVol1.Size = New System.Drawing.Size(28, 20)
        Me.txtLastVol1.TabIndex = 20
        Me.txtLastVol1.TabStop = False
        Me.txtLastVol1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtLast1
        '
        Me.txtLast1.BackColor = System.Drawing.Color.Honeydew
        Me.txtLast1.Location = New System.Drawing.Point(405, 140)
        Me.txtLast1.Name = "txtLast1"
        Me.txtLast1.ReadOnly = True
        Me.txtLast1.Size = New System.Drawing.Size(60, 20)
        Me.txtLast1.TabIndex = 18
        Me.txtLast1.TabStop = False
        Me.txtLast1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtLastVolTotal1
        '
        Me.txtLastVolTotal1.BackColor = System.Drawing.Color.Honeydew
        Me.txtLastVolTotal1.Location = New System.Drawing.Point(497, 140)
        Me.txtLastVolTotal1.Name = "txtLastVolTotal1"
        Me.txtLastVolTotal1.ReadOnly = True
        Me.txtLastVolTotal1.Size = New System.Drawing.Size(60, 20)
        Me.txtLastVolTotal1.TabIndex = 21
        Me.txtLastVolTotal1.TabStop = False
        Me.txtLastVolTotal1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(10, 119)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(129, 20)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Market Description:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBidPrice
        '
        Me.lblBidPrice.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBidPrice.ForeColor = System.Drawing.Color.RoyalBlue
        Me.lblBidPrice.Location = New System.Drawing.Point(219, 120)
        Me.lblBidPrice.Name = "lblBidPrice"
        Me.lblBidPrice.Size = New System.Drawing.Size(60, 20)
        Me.lblBidPrice.TabIndex = 23
        Me.lblBidPrice.Text = "Price:"
        Me.lblBidPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblOfferPrice
        '
        Me.lblOfferPrice.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOfferPrice.ForeColor = System.Drawing.Color.Crimson
        Me.lblOfferPrice.Location = New System.Drawing.Point(311, 120)
        Me.lblOfferPrice.Name = "lblOfferPrice"
        Me.lblOfferPrice.Size = New System.Drawing.Size(60, 20)
        Me.lblOfferPrice.TabIndex = 24
        Me.lblOfferPrice.Text = "Price:"
        Me.lblOfferPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLastPrice
        '
        Me.lblLastPrice.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastPrice.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblLastPrice.Location = New System.Drawing.Point(403, 120)
        Me.lblLastPrice.Name = "lblLastPrice"
        Me.lblLastPrice.Size = New System.Drawing.Size(60, 20)
        Me.lblLastPrice.TabIndex = 25
        Me.lblLastPrice.Text = "Price:"
        Me.lblLastPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBidVol
        '
        Me.lblBidVol.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBidVol.ForeColor = System.Drawing.Color.RoyalBlue
        Me.lblBidVol.Location = New System.Drawing.Point(281, 120)
        Me.lblBidVol.Name = "lblBidVol"
        Me.lblBidVol.Size = New System.Drawing.Size(30, 20)
        Me.lblBidVol.TabIndex = 26
        Me.lblBidVol.Text = "Vol:"
        Me.lblBidVol.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblOfferVol
        '
        Me.lblOfferVol.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOfferVol.ForeColor = System.Drawing.Color.Crimson
        Me.lblOfferVol.Location = New System.Drawing.Point(373, 120)
        Me.lblOfferVol.Name = "lblOfferVol"
        Me.lblOfferVol.Size = New System.Drawing.Size(30, 20)
        Me.lblOfferVol.TabIndex = 27
        Me.lblOfferVol.Text = "Vol:"
        Me.lblOfferVol.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLastVol
        '
        Me.lblLastVol.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastVol.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblLastVol.Location = New System.Drawing.Point(465, 120)
        Me.lblLastVol.Name = "lblLastVol"
        Me.lblLastVol.Size = New System.Drawing.Size(30, 20)
        Me.lblLastVol.TabIndex = 28
        Me.lblLastVol.Text = "Vol:"
        Me.lblLastVol.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTotalVol
        '
        Me.lblTotalVol.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalVol.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblTotalVol.Location = New System.Drawing.Point(495, 120)
        Me.lblTotalVol.Name = "lblTotalVol"
        Me.lblTotalVol.Size = New System.Drawing.Size(62, 20)
        Me.lblTotalVol.TabIndex = 29
        Me.lblTotalVol.Text = "Total Vol:"
        Me.lblTotalVol.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtLastVolTotal2
        '
        Me.txtLastVolTotal2.BackColor = System.Drawing.Color.Honeydew
        Me.txtLastVolTotal2.Location = New System.Drawing.Point(497, 44)
        Me.txtLastVolTotal2.Name = "txtLastVolTotal2"
        Me.txtLastVolTotal2.ReadOnly = True
        Me.txtLastVolTotal2.Size = New System.Drawing.Size(60, 20)
        Me.txtLastVolTotal2.TabIndex = 38
        Me.txtLastVolTotal2.TabStop = False
        Me.txtLastVolTotal2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtLastVol2
        '
        Me.txtLastVol2.BackColor = System.Drawing.Color.Honeydew
        Me.txtLastVol2.Location = New System.Drawing.Point(467, 44)
        Me.txtLastVol2.Name = "txtLastVol2"
        Me.txtLastVol2.ReadOnly = True
        Me.txtLastVol2.Size = New System.Drawing.Size(28, 20)
        Me.txtLastVol2.TabIndex = 37
        Me.txtLastVol2.TabStop = False
        Me.txtLastVol2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtLast2
        '
        Me.txtLast2.BackColor = System.Drawing.Color.Honeydew
        Me.txtLast2.Location = New System.Drawing.Point(405, 44)
        Me.txtLast2.Name = "txtLast2"
        Me.txtLast2.ReadOnly = True
        Me.txtLast2.Size = New System.Drawing.Size(60, 20)
        Me.txtLast2.TabIndex = 36
        Me.txtLast2.TabStop = False
        Me.txtLast2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtOfferVol2
        '
        Me.txtOfferVol2.BackColor = System.Drawing.Color.MistyRose
        Me.txtOfferVol2.Location = New System.Drawing.Point(375, 44)
        Me.txtOfferVol2.Name = "txtOfferVol2"
        Me.txtOfferVol2.ReadOnly = True
        Me.txtOfferVol2.Size = New System.Drawing.Size(28, 20)
        Me.txtOfferVol2.TabIndex = 35
        Me.txtOfferVol2.TabStop = False
        Me.txtOfferVol2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBidVol2
        '
        Me.txtBidVol2.BackColor = System.Drawing.Color.LightCyan
        Me.txtBidVol2.Location = New System.Drawing.Point(283, 44)
        Me.txtBidVol2.Name = "txtBidVol2"
        Me.txtBidVol2.ReadOnly = True
        Me.txtBidVol2.Size = New System.Drawing.Size(28, 20)
        Me.txtBidVol2.TabIndex = 34
        Me.txtBidVol2.TabStop = False
        Me.txtBidVol2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtOffer2
        '
        Me.txtOffer2.BackColor = System.Drawing.Color.MistyRose
        Me.txtOffer2.Location = New System.Drawing.Point(313, 44)
        Me.txtOffer2.Name = "txtOffer2"
        Me.txtOffer2.ReadOnly = True
        Me.txtOffer2.Size = New System.Drawing.Size(60, 20)
        Me.txtOffer2.TabIndex = 33
        Me.txtOffer2.TabStop = False
        Me.txtOffer2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBid2
        '
        Me.txtBid2.BackColor = System.Drawing.Color.LightCyan
        Me.txtBid2.Location = New System.Drawing.Point(221, 44)
        Me.txtBid2.Name = "txtBid2"
        Me.txtBid2.ReadOnly = True
        Me.txtBid2.Size = New System.Drawing.Size(60, 20)
        Me.txtBid2.TabIndex = 32
        Me.txtBid2.TabStop = False
        Me.txtBid2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtMarketDescription2
        '
        Me.txtMarketDescription2.BackColor = System.Drawing.Color.White
        Me.txtMarketDescription2.Location = New System.Drawing.Point(13, 44)
        Me.txtMarketDescription2.Name = "txtMarketDescription2"
        Me.txtMarketDescription2.ReadOnly = True
        Me.txtMarketDescription2.Size = New System.Drawing.Size(206, 20)
        Me.txtMarketDescription2.TabIndex = 31
        Me.txtMarketDescription2.TabStop = False
        '
        'cmdGet2
        '
        Me.cmdGet2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGet2.Location = New System.Drawing.Point(13, 19)
        Me.cmdGet2.Name = "cmdGet2"
        Me.cmdGet2.Size = New System.Drawing.Size(51, 20)
        Me.cmdGet2.TabIndex = 30
        Me.cmdGet2.TabStop = False
        Me.cmdGet2.Text = "Picker"
        '
        'cmdSave
        '
        Me.cmdSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.Location = New System.Drawing.Point(12, 398)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(140, 26)
        Me.cmdSave.TabIndex = 40
        Me.cmdSave.TabStop = False
        Me.cmdSave.Text = "Save Selected Markets"
        '
        'lblAccount
        '
        Me.lblAccount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccount.Location = New System.Drawing.Point(6, 22)
        Me.lblAccount.Name = "lblAccount"
        Me.lblAccount.Size = New System.Drawing.Size(107, 21)
        Me.lblAccount.TabIndex = 41
        Me.lblAccount.Text = "Current Account:"
        Me.lblAccount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboAccounts
        '
        Me.cboAccounts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAccounts.Location = New System.Drawing.Point(117, 22)
        Me.cboAccounts.Name = "cboAccounts"
        Me.cboAccounts.Size = New System.Drawing.Size(208, 21)
        Me.cboAccounts.Sorted = True
        Me.cboAccounts.TabIndex = 42
        Me.cboAccounts.TabStop = False
        '
        'txtCash
        '
        Me.txtCash.BackColor = System.Drawing.Color.White
        Me.txtCash.Location = New System.Drawing.Point(400, 23)
        Me.txtCash.Name = "txtCash"
        Me.txtCash.ReadOnly = True
        Me.txtCash.Size = New System.Drawing.Size(108, 20)
        Me.txtCash.TabIndex = 43
        Me.txtCash.TabStop = False
        Me.txtCash.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblCash
        '
        Me.lblCash.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCash.Location = New System.Drawing.Point(337, 22)
        Me.lblCash.Name = "lblCash"
        Me.lblCash.Size = New System.Drawing.Size(57, 21)
        Me.lblCash.TabIndex = 44
        Me.lblCash.Text = "Cash:"
        Me.lblCash.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNet1
        '
        Me.txtNet1.BackColor = System.Drawing.Color.White
        Me.txtNet1.Location = New System.Drawing.Point(559, 140)
        Me.txtNet1.Name = "txtNet1"
        Me.txtNet1.ReadOnly = True
        Me.txtNet1.Size = New System.Drawing.Size(38, 20)
        Me.txtNet1.TabIndex = 46
        Me.txtNet1.TabStop = False
        Me.txtNet1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtNet2
        '
        Me.txtNet2.BackColor = System.Drawing.Color.White
        Me.txtNet2.Location = New System.Drawing.Point(559, 44)
        Me.txtNet2.Name = "txtNet2"
        Me.txtNet2.ReadOnly = True
        Me.txtNet2.Size = New System.Drawing.Size(38, 20)
        Me.txtNet2.TabIndex = 47
        Me.txtNet2.TabStop = False
        Me.txtNet2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblNet
        '
        Me.lblNet.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNet.Location = New System.Drawing.Point(557, 121)
        Me.lblNet.Name = "lblNet"
        Me.lblNet.Size = New System.Drawing.Size(38, 18)
        Me.lblNet.TabIndex = 48
        Me.lblNet.Text = "Net:"
        Me.lblNet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBuys
        '
        Me.lblBuys.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBuys.Location = New System.Drawing.Point(597, 121)
        Me.lblBuys.Name = "lblBuys"
        Me.lblBuys.Size = New System.Drawing.Size(38, 18)
        Me.lblBuys.TabIndex = 49
        Me.lblBuys.Text = "Buys:"
        Me.lblBuys.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSells
        '
        Me.lblSells.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSells.Location = New System.Drawing.Point(637, 121)
        Me.lblSells.Name = "lblSells"
        Me.lblSells.Size = New System.Drawing.Size(38, 18)
        Me.lblSells.TabIndex = 50
        Me.lblSells.Text = "Sells:"
        Me.lblSells.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtBuys1
        '
        Me.txtBuys1.BackColor = System.Drawing.Color.White
        Me.txtBuys1.ForeColor = System.Drawing.Color.RoyalBlue
        Me.txtBuys1.Location = New System.Drawing.Point(599, 140)
        Me.txtBuys1.Name = "txtBuys1"
        Me.txtBuys1.ReadOnly = True
        Me.txtBuys1.Size = New System.Drawing.Size(38, 20)
        Me.txtBuys1.TabIndex = 51
        Me.txtBuys1.TabStop = False
        Me.txtBuys1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBuys2
        '
        Me.txtBuys2.BackColor = System.Drawing.Color.White
        Me.txtBuys2.ForeColor = System.Drawing.Color.RoyalBlue
        Me.txtBuys2.Location = New System.Drawing.Point(599, 44)
        Me.txtBuys2.Name = "txtBuys2"
        Me.txtBuys2.ReadOnly = True
        Me.txtBuys2.Size = New System.Drawing.Size(38, 20)
        Me.txtBuys2.TabIndex = 52
        Me.txtBuys2.TabStop = False
        Me.txtBuys2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSells1
        '
        Me.txtSells1.BackColor = System.Drawing.Color.White
        Me.txtSells1.ForeColor = System.Drawing.Color.Crimson
        Me.txtSells1.Location = New System.Drawing.Point(639, 140)
        Me.txtSells1.Name = "txtSells1"
        Me.txtSells1.ReadOnly = True
        Me.txtSells1.Size = New System.Drawing.Size(38, 20)
        Me.txtSells1.TabIndex = 53
        Me.txtSells1.TabStop = False
        Me.txtSells1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSells2
        '
        Me.txtSells2.BackColor = System.Drawing.Color.White
        Me.txtSells2.ForeColor = System.Drawing.Color.Crimson
        Me.txtSells2.Location = New System.Drawing.Point(639, 44)
        Me.txtSells2.Name = "txtSells2"
        Me.txtSells2.ReadOnly = True
        Me.txtSells2.Size = New System.Drawing.Size(38, 20)
        Me.txtSells2.TabIndex = 54
        Me.txtSells2.TabStop = False
        Me.txtSells2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmdBuy1
        '
        Me.cmdBuy1.BackColor = System.Drawing.Color.RoyalBlue
        Me.cmdBuy1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBuy1.ForeColor = System.Drawing.Color.White
        Me.cmdBuy1.Location = New System.Drawing.Point(219, 162)
        Me.cmdBuy1.Name = "cmdBuy1"
        Me.cmdBuy1.Size = New System.Drawing.Size(60, 20)
        Me.cmdBuy1.TabIndex = 55
        Me.cmdBuy1.TabStop = False
        Me.cmdBuy1.Text = "Buy"
        Me.cmdBuy1.UseVisualStyleBackColor = False
        '
        'cmdBuy2
        '
        Me.cmdBuy2.BackColor = System.Drawing.Color.RoyalBlue
        Me.cmdBuy2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBuy2.ForeColor = System.Drawing.Color.White
        Me.cmdBuy2.Location = New System.Drawing.Point(219, 66)
        Me.cmdBuy2.Name = "cmdBuy2"
        Me.cmdBuy2.Size = New System.Drawing.Size(60, 20)
        Me.cmdBuy2.TabIndex = 56
        Me.cmdBuy2.TabStop = False
        Me.cmdBuy2.Text = "Buy"
        Me.cmdBuy2.UseVisualStyleBackColor = False
        '
        'cmdSell1
        '
        Me.cmdSell1.BackColor = System.Drawing.Color.Crimson
        Me.cmdSell1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSell1.ForeColor = System.Drawing.Color.White
        Me.cmdSell1.Location = New System.Drawing.Point(311, 162)
        Me.cmdSell1.Name = "cmdSell1"
        Me.cmdSell1.Size = New System.Drawing.Size(60, 20)
        Me.cmdSell1.TabIndex = 58
        Me.cmdSell1.TabStop = False
        Me.cmdSell1.Text = "Sell"
        Me.cmdSell1.UseVisualStyleBackColor = False
        '
        'cmdSell2
        '
        Me.cmdSell2.BackColor = System.Drawing.Color.Crimson
        Me.cmdSell2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSell2.ForeColor = System.Drawing.Color.White
        Me.cmdSell2.Location = New System.Drawing.Point(311, 66)
        Me.cmdSell2.Name = "cmdSell2"
        Me.cmdSell2.Size = New System.Drawing.Size(60, 20)
        Me.cmdSell2.TabIndex = 59
        Me.cmdSell2.TabStop = False
        Me.cmdSell2.Text = "Sell"
        Me.cmdSell2.UseVisualStyleBackColor = False
        '
        'lstOrders
        '
        Me.lstOrders.Location = New System.Drawing.Point(8, 21)
        Me.lstOrders.Name = "lstOrders"
        Me.lstOrders.Size = New System.Drawing.Size(669, 134)
        Me.lstOrders.TabIndex = 60
        Me.lstOrders.TabStop = False
        '
        'grpAccount
        '
        Me.grpAccount.Controls.Add(Me.lblAccountInfo)
        Me.grpAccount.Controls.Add(Me.cboAccounts)
        Me.grpAccount.Controls.Add(Me.lblCash)
        Me.grpAccount.Controls.Add(Me.lblAccount)
        Me.grpAccount.Controls.Add(Me.txtCash)
        Me.grpAccount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpAccount.Location = New System.Drawing.Point(12, 12)
        Me.grpAccount.Name = "grpAccount"
        Me.grpAccount.Size = New System.Drawing.Size(691, 51)
        Me.grpAccount.TabIndex = 63
        Me.grpAccount.TabStop = False
        Me.grpAccount.Text = "Account"
        '
        'lblAccountInfo
        '
        Me.lblAccountInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccountInfo.Location = New System.Drawing.Point(70, 95)
        Me.lblAccountInfo.Name = "lblAccountInfo"
        Me.lblAccountInfo.Size = New System.Drawing.Size(188, 30)
        Me.lblAccountInfo.TabIndex = 68
        Me.lblAccountInfo.Text = "An Account must be selected to view positions and submit trades."
        Me.lblAccountInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grpOrders
        '
        Me.grpOrders.Controls.Add(Me.lstOrders)
        Me.grpOrders.Controls.Add(Me.lblOrderInfo)
        Me.grpOrders.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpOrders.Location = New System.Drawing.Point(12, 430)
        Me.grpOrders.Name = "grpOrders"
        Me.grpOrders.Size = New System.Drawing.Size(691, 190)
        Me.grpOrders.TabIndex = 64
        Me.grpOrders.TabStop = False
        Me.grpOrders.Text = "Orders"
        '
        'lblOrderInfo
        '
        Me.lblOrderInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOrderInfo.Location = New System.Drawing.Point(40, 166)
        Me.lblOrderInfo.Name = "lblOrderInfo"
        Me.lblOrderInfo.Size = New System.Drawing.Size(634, 18)
        Me.lblOrderInfo.TabIndex = 67
        Me.lblOrderInfo.Text = "Double Click orders to Pull them.  Volume is displayed Filled/Working to clarify " & _
    "which orders have been Pulled."
        Me.lblOrderInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMisc1
        '
        Me.lblMisc1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMisc1.Location = New System.Drawing.Point(382, 162)
        Me.lblMisc1.Name = "lblMisc1"
        Me.lblMisc1.Size = New System.Drawing.Size(70, 20)
        Me.lblMisc1.TabIndex = 67
        Me.lblMisc1.Text = "Misc Code:"
        Me.lblMisc1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmdRunMisc1
        '
        Me.cmdRunMisc1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRunMisc1.Location = New System.Drawing.Point(640, 162)
        Me.cmdRunMisc1.Name = "cmdRunMisc1"
        Me.cmdRunMisc1.Size = New System.Drawing.Size(38, 20)
        Me.cmdRunMisc1.TabIndex = 66
        Me.cmdRunMisc1.TabStop = False
        Me.cmdRunMisc1.Text = "Run"
        '
        'cboMisc1
        '
        Me.cboMisc1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMisc1.Location = New System.Drawing.Point(456, 162)
        Me.cboMisc1.Name = "cboMisc1"
        Me.cboMisc1.Size = New System.Drawing.Size(180, 21)
        Me.cboMisc1.Sorted = True
        Me.cboMisc1.TabIndex = 65
        Me.cboMisc1.TabStop = False
        '
        'lblMisc2
        '
        Me.lblMisc2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMisc2.Location = New System.Drawing.Point(382, 66)
        Me.lblMisc2.Name = "lblMisc2"
        Me.lblMisc2.Size = New System.Drawing.Size(70, 20)
        Me.lblMisc2.TabIndex = 64
        Me.lblMisc2.Text = "Misc Code:"
        Me.lblMisc2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmdRunMisc2
        '
        Me.cmdRunMisc2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRunMisc2.Location = New System.Drawing.Point(640, 66)
        Me.cmdRunMisc2.Name = "cmdRunMisc2"
        Me.cmdRunMisc2.Size = New System.Drawing.Size(38, 20)
        Me.cmdRunMisc2.TabIndex = 63
        Me.cmdRunMisc2.TabStop = False
        Me.cmdRunMisc2.Text = "Run"
        '
        'cboMisc2
        '
        Me.cboMisc2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMisc2.Location = New System.Drawing.Point(456, 66)
        Me.cboMisc2.Name = "cboMisc2"
        Me.cboMisc2.Size = New System.Drawing.Size(180, 21)
        Me.cboMisc2.Sorted = True
        Me.cboMisc2.TabIndex = 62
        Me.cboMisc2.TabStop = False
        '
        'lblSaveInfo
        '
        Me.lblSaveInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSaveInfo.Location = New System.Drawing.Point(153, 396)
        Me.lblSaveInfo.Name = "lblSaveInfo"
        Me.lblSaveInfo.Size = New System.Drawing.Size(370, 26)
        Me.lblSaveInfo.TabIndex = 66
        Me.lblSaveInfo.Text = "Click Save to store the current markets in an XML file on the server.  The market" & _
    "s will be loaded automatically on the next login."
        Me.lblSaveInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'grpMarket1
        '
        Me.grpMarket1.Controls.Add(Me.cboExchanges)
        Me.grpMarket1.Controls.Add(Me.lblMisc1)
        Me.grpMarket1.Controls.Add(Me.lblMarket)
        Me.grpMarket1.Controls.Add(Me.cmdRunMisc1)
        Me.grpMarket1.Controls.Add(Me.cboContracts)
        Me.grpMarket1.Controls.Add(Me.cboMisc1)
        Me.grpMarket1.Controls.Add(Me.cboMarkets)
        Me.grpMarket1.Controls.Add(Me.lblExchange)
        Me.grpMarket1.Controls.Add(Me.lblContract)
        Me.grpMarket1.Controls.Add(Me.Label1)
        Me.grpMarket1.Controls.Add(Me.lblLastPrice)
        Me.grpMarket1.Controls.Add(Me.lblBidVol)
        Me.grpMarket1.Controls.Add(Me.lblOfferPrice)
        Me.grpMarket1.Controls.Add(Me.lblOfferVol)
        Me.grpMarket1.Controls.Add(Me.lblBidPrice)
        Me.grpMarket1.Controls.Add(Me.lblLastVol)
        Me.grpMarket1.Controls.Add(Me.lblTotalVol)
        Me.grpMarket1.Controls.Add(Me.txtOfferVol1)
        Me.grpMarket1.Controls.Add(Me.txtBidVol1)
        Me.grpMarket1.Controls.Add(Me.txtLastVolTotal1)
        Me.grpMarket1.Controls.Add(Me.cmdBuy1)
        Me.grpMarket1.Controls.Add(Me.txtOffer1)
        Me.grpMarket1.Controls.Add(Me.txtLast1)
        Me.grpMarket1.Controls.Add(Me.lblBuys)
        Me.grpMarket1.Controls.Add(Me.cmdSell1)
        Me.grpMarket1.Controls.Add(Me.txtBid1)
        Me.grpMarket1.Controls.Add(Me.txtMarketDescription1)
        Me.grpMarket1.Controls.Add(Me.cmdGet1)
        Me.grpMarket1.Controls.Add(Me.txtSells1)
        Me.grpMarket1.Controls.Add(Me.txtBuys1)
        Me.grpMarket1.Controls.Add(Me.lblSells)
        Me.grpMarket1.Controls.Add(Me.txtNet1)
        Me.grpMarket1.Controls.Add(Me.txtLastVol1)
        Me.grpMarket1.Controls.Add(Me.lblNet)
        Me.grpMarket1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpMarket1.Location = New System.Drawing.Point(12, 84)
        Me.grpMarket1.Name = "grpMarket1"
        Me.grpMarket1.Size = New System.Drawing.Size(691, 195)
        Me.grpMarket1.TabIndex = 66
        Me.grpMarket1.TabStop = False
        Me.grpMarket1.Text = "Market 1"
        '
        'grpMarket2
        '
        Me.grpMarket2.Controls.Add(Me.cmdGet2)
        Me.grpMarket2.Controls.Add(Me.txtMarketDescription2)
        Me.grpMarket2.Controls.Add(Me.lblMisc2)
        Me.grpMarket2.Controls.Add(Me.txtLastVolTotal2)
        Me.grpMarket2.Controls.Add(Me.cmdRunMisc2)
        Me.grpMarket2.Controls.Add(Me.txtLastVol2)
        Me.grpMarket2.Controls.Add(Me.cboMisc2)
        Me.grpMarket2.Controls.Add(Me.txtSells2)
        Me.grpMarket2.Controls.Add(Me.txtLast2)
        Me.grpMarket2.Controls.Add(Me.cmdSell2)
        Me.grpMarket2.Controls.Add(Me.cmdBuy2)
        Me.grpMarket2.Controls.Add(Me.txtOfferVol2)
        Me.grpMarket2.Controls.Add(Me.txtBuys2)
        Me.grpMarket2.Controls.Add(Me.txtBidVol2)
        Me.grpMarket2.Controls.Add(Me.txtNet2)
        Me.grpMarket2.Controls.Add(Me.txtOffer2)
        Me.grpMarket2.Controls.Add(Me.txtBid2)
        Me.grpMarket2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpMarket2.Location = New System.Drawing.Point(12, 285)
        Me.grpMarket2.Name = "grpMarket2"
        Me.grpMarket2.Size = New System.Drawing.Size(691, 98)
        Me.grpMarket2.TabIndex = 67
        Me.grpMarket2.TabStop = False
        Me.grpMarket2.Text = "Market 2"
        '
        'frmMain
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(714, 631)
        Me.Controls.Add(Me.grpMarket2)
        Me.Controls.Add(Me.grpMarket1)
        Me.Controls.Add(Me.grpAccount)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.grpOrders)
        Me.Controls.Add(Me.lblSaveInfo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMain"
        Me.Text = "T4 Example 2"
        Me.grpAccount.ResumeLayout(False)
        Me.grpAccount.PerformLayout()
        Me.grpOrders.ResumeLayout(False)
        Me.grpMarket1.ResumeLayout(False)
        Me.grpMarket1.PerformLayout()
        Me.grpMarket2.ResumeLayout(False)
        Me.grpMarket2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region " Delegates "

    Private Delegate Sub OnAccountDetailsDelegate(ByVal poAccounts As T4.API.AccountList.UpdateList)
    Private Delegate Sub OnAccountUpdateDelegate(ByVal poAccounts As T4.API.AccountList.UpdateList)
    Private Delegate Sub OnPositionUpdateDelegate(ByVal poPosition As T4.API.Position)
    Private Delegate Sub OnMarketDepthUpdateDelegate(ByVal poMarket As Market)
    Private Delegate Sub OnAccountOrderUpdateDelegate(ByVal poAccount As T4.API.Account, ByVal poPosition As T4.API.Position, ByVal poOrders As T4.API.OrderList.UpdateList)
    Private Delegate Sub OnAccountOrderAddedDelegate(ByVal poAccount As T4.API.Account, ByVal poPosition As T4.API.Position, ByVal poOrders As T4.API.OrderList.UpdateList)

#End Region

#Region " Member Variables "

    ' Reference to the main api host object.
    Private WithEvents moHost As Host

    ' Reference to the current account.
    Private WithEvents moAccount As Account

    ' Reference to the exchange list.
    Private WithEvents moExchanges As ExchangeList

    ' Reference to the current exchange.
    Private moExchange As Exchange

    ' Reference to an exchange's contract list.
    Private WithEvents moContracts As ContractList

    ' Reference to the current contract.
    Private moContract As Contract

    ' Reference to a contract's market list.
    Private WithEvents moPickerMarkets As MarketList

    ' Reference to the current market.
    Private moPickerMarket As Market

    ' Market1 filter.
    Private WithEvents moMarkets1Filter As MarketList
    Private WithEvents moMarkets2Filter As MarketList

    ' References to selected markets.
    Private moMarket1 As Market
    Private moMarket2 As Market

    ' References to marketid's retrieved from saved settings..
    Dim mstrMarketID1 As String
    Dim mstrMarketID2 As String

    ' Reference to the accounts list.
    Private WithEvents moAccounts As AccountList

    ' Reference to Order arraylist.
    ' Stores the collection of orders.
    Private moOrderArrayList As New ArrayList

#End Region

#Region " Initialization "

    ' Initialize the application.
    Private Sub Init()

        Trace.WriteLine("Init")

        ' Populate the available exchanges.
        moExchanges = moHost.MarketData.Exchanges

        ' Check to see if the data is already loaded.
        If moExchanges.Complete Then

            ' Call the event handler ourselves as the data is 
            ' already loaded.
            moExchanges_ExchangeListComplete(moExchanges)

        End If

        ' Subscribe to accounts.
        SubscribeToAccount()

        Try

            ' Read saved markets.
            ' XML Doc.
            Dim oDoc As New XmlDocument

            ' XML Nodes for viewing the doc.
            Dim oMarket As XmlNode
            Dim oMarkets As XmlNode

            ' Temporary string variables for referencing contract and exchange details.
            Dim strExchangeID As String
            Dim strContractID As String

            ' Pull the xml doc from the server.
            oDoc = moHost.UserSettings

            ' Reference the saved markets via xml node.
            oMarkets = oDoc.ChildNodes(0)

            ' Load the saved markets.
            For Each oMarket In oMarkets
                ' Check each child node for existance of saved markets.
                Select Case oMarket.Name
                    Case "market1"
                        mstrMarketID1 = oMarket.Attributes("MarketID").Value
                        strExchangeID = oMarket.Attributes("ExchangeID").Value
                        strContractID = oMarket.Attributes("ContractID").Value

                        ' Create a market filter for the desired exchange and contract.
                        moMarkets1Filter = moHost.MarketData.CreateMarketFilter(strExchangeID, strContractID)

                        If moMarkets1Filter.Complete Then

                            ' Call the event handler directly as the list is already complete.
                            moMarkets1Filter_MarketListComplete(moMarkets1Filter)

                        End If

                    Case "market2"
                        mstrMarketID2 = oMarket.Attributes("MarketID").Value
                        strExchangeID = oMarket.Attributes("ExchangeID").Value
                        strContractID = oMarket.Attributes("ContractID").Value

                        ' Create a market filter for the desired exchange and contract.
                        moMarkets2Filter = moHost.MarketData.CreateMarketFilter(strExchangeID, strContractID)

                        If moMarkets2Filter.Complete Then

                            ' Call the event handler directly as the list is already complete.
                            moMarkets2Filter_MarketListComplete(moMarkets2Filter)

                        End If

                End Select
            Next

        Catch ex As Exception

            ' Trace the exception.
            Trace.WriteLine("Error: " & ex.ToString)

        End Try

    End Sub

#End Region

#Region " Market Filters "

    Private Sub moMarkets1Filter_MarketListComplete(ByVal poMarketList As T4.API.MarketList) Handles moMarkets1Filter.MarketListComplete

        ' Invoke the update.
        ' This places process on GUI thread.
        If Me.IsHandleCreated Then
            Me.BeginInvoke(New MethodInvoker(AddressOf Markets1ListComplete))
        Else
            Markets1ListComplete()
        End If

    End Sub

    Private Sub Markets1ListComplete()

        ' Reference the desired market.
        Dim oMarket1 As Market = moMarkets1Filter(mstrMarketID1)

        ' Subscribe to market1.
        NewMarketSubscription(moMarket1, oMarket1)

    End Sub

    Private Sub moMarkets2Filter_MarketListComplete(ByVal poMarketList As T4.API.MarketList) Handles moMarkets2Filter.MarketListComplete

        ' Invoke the update.
        ' This places process on GUI thread.
        If Me.IsHandleCreated Then
            Me.BeginInvoke(New MethodInvoker(AddressOf Markets2ListComplete))
        Else
            Markets2ListComplete()
        End If

    End Sub

    Private Sub Markets2ListComplete()

        ' Reference the desired market.
        Dim oMarket2 As Market = moMarkets1Filter(mstrMarketID2)

        ' Subscribe to market1.
        NewMarketSubscription(moMarket2, oMarket2)

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

#Region " Market Picker "

    Private Sub moExchanges_ExchangeListComplete(ByVal poExchangeList As T4.API.ExchangeList) Handles moExchanges.ExchangeListComplete

        ' Invoke the update.
        ' This places process on GUI thread.
        If Me.IsHandleCreated Then
            Me.BeginInvoke(New MethodInvoker(AddressOf ExchangeListComplete))
        Else
            ExchangeListComplete()
        End If

    End Sub

    Private Sub ExchangeListComplete()

        ' First clear all the combo's.
        cboExchanges.Items.Clear()
        cboContracts.Items.Clear()
        cboMarkets.Items.Clear()

        ' Eliminate any previous references.
        moExchange = Nothing
        moContracts = Nothing
        moContract = Nothing
        moPickerMarkets = Nothing
        moPickerMarket = Nothing

        ' Populate the list of exchanges.
        If Not moExchanges Is Nothing Then

            Try

                ' Lock the API while traversing the api collection.
                ' Lock at the lowest level object for the shortest period of time.
                moHost.EnterLock("ExchangeList")

                ' Add the exchanges to the dropdown list.
                For Each oExchange As Exchange In moExchanges
                    '  cboExchanges.Items.Add(New ExchangeItem(oExchange))
                    cboExchanges.Items.Add(oExchange)
                Next

            Catch ex As Exception

                ' Trace the error.
                Trace.WriteLine("Error " & ex.ToString)

            Finally

                ' This is guarenteed to execute last.
                moHost.ExitLock("ExchangeList")

            End Try

        End If

    End Sub

    Private Sub cboExchanges_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboExchanges.SelectedIndexChanged

        ' Populate the current exchange's available contracts.
        If Not cboExchanges.SelectedItem Is Nothing Then

            ' Reference the current exchange.
            ' moExchange = CType(cboExchanges.SelectedItem, ExchangeItem).Exchange
            moExchange = DirectCast(cboExchanges.SelectedItem, Exchange)

            ' Reference the exchange's available contracts.
            moContracts = moExchange.Contracts

            ' Check to see if the data is already loaded.
            If moContracts.Complete Then

                ' Call the event handler ourselves as the data is 
                ' already loaded.
                moContracts_ContractListComplete(moContracts)

            End If

        End If

    End Sub

    Private Sub moContracts_ContractListComplete(ByVal poContractList As T4.API.ContractList) Handles moContracts.ContractListComplete

        ' Invoke the update.
        ' This places process on GUI thread.
        If Me.IsHandleCreated Then
            Me.BeginInvoke(New MethodInvoker(AddressOf ContractListComplete))
        Else
            ContractListComplete()
        End If

    End Sub

    Private Sub ContractListComplete()

        ' Populate the list of contracts available for the current exchange.

        ' First clear all the combo's.
        cboContracts.Items.Clear()
        cboMarkets.Items.Clear()

        ' Eliminate any previous references.
        moContract = Nothing
        moPickerMarkets = Nothing
        moPickerMarket = Nothing

        If Not moContracts Is Nothing Then

            Try

                ' Lock the API while traversing the api collection.
                ' Lock at the lowest level object for the shortest period of time.
                moHost.EnterLock("ContractList")

                ' Add the exchanges to the dropdown list.
                For Each oContract As Contract In moContracts
                    cboContracts.Items.Add(oContract)
                Next

            Catch ex As Exception

                ' Trace the error.
                Trace.WriteLine("Error " & ex.ToString)

            Finally

                ' This is guarenteed to execute last.
                moHost.ExitLock("ContractList")

            End Try

        End If

    End Sub

    Private Sub cboContracts_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboContracts.SelectedIndexChanged

        ' Populate the current contract's available markets.
        If Not cboContracts.SelectedItem Is Nothing Then

            ' Reference the current contract.
            moContract = DirectCast(cboContracts.SelectedItem, Contract)

            ' Reference the contract's available markets.

            ' This would return all markets for the contract.
            ' moPickerMarkets = moContract.Markets

            ' This will return outright futures only.
            moPickerMarkets = moHost.MarketData.CreateMarketFilter(moContract.ExchangeID, moContract.ContractID, 0, ContractType.Future, StrategyType.None)

            ' Check to see if the data is already loaded.
            If moPickerMarkets.Complete Then

                ' Call the event handler ourselves as the data is 
                ' already loaded.
                moPickerMarkets_MarketListComplete(moPickerMarkets)

            End If

        End If

    End Sub

    Private Sub moPickerMarkets_MarketListComplete(ByVal poMarketList As T4.API.MarketList) Handles moPickerMarkets.MarketListComplete

        ' Invoke the update.
        ' This places process on GUI thread.
        If Me.IsHandleCreated Then
            Me.BeginInvoke(New MethodInvoker(AddressOf MarketListComplete))
        Else
            MarketListComplete()
        End If

    End Sub

    Private Sub MarketListComplete()

        ' Populate the list of markets available for the current contract.

        ' First clear the combo.
        cboMarkets.Items.Clear()

        ' Eliminate any previous references.
        moPickerMarket = Nothing

        If Not moPickerMarkets Is Nothing Then

            Try

                ' Lock the API while traversing the api collection.
                ' Lock at the lowest level object for the shortest period of time.
                moHost.EnterLock("MarketList")

                ' Create a sorted list of the markets.
                ' Remember to turn sorting off on the combo or it will do a text sort.
                Dim oSortedList As New Generic.SortedList(Of Integer, Market)

                For Each oMarket As Market In moPickerMarkets

                    oSortedList.Add(oMarket.ExpiryDate, oMarket)

                Next

                ' Add the exchanges to the dropdown list.
                For Each oMarket As Market In oSortedList.Values

                    cboMarkets.Items.Add(oMarket)

                Next

            Catch ex As Exception

                ' Trace the error.
                Trace.WriteLine("Error " & ex.ToString)

            Finally

                ' This is guarenteed to execute last.
                moHost.ExitLock("MarketList")

            End Try

        End If

    End Sub

    Private Sub cboMarkets_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMarkets.SelectedIndexChanged

        ' Store a reference to the current market.
        If Not cboMarkets.SelectedItem Is Nothing Then
            moPickerMarket = DirectCast(cboMarkets.SelectedItem, Market)
        End If

    End Sub

#End Region

#Region " Account Data "

    ' Method called following login success to get the data for 
    ' an account and subscribe to it.
    Private Sub SubscribeToAccount()

        Try

            ' Lock the API.
            moHost.EnterLock("AccountSubscribe")

            ' Set the account list reference so that we can get 
            ' Account and order events.
            moAccounts = moHost.Accounts

            ' Display the account list.
            For Each oAccount As Account In moHost.Accounts

                ' Add the account to the combo.
                cboAccounts.Items.Add(oAccount)

                ' Subscribe to the account.
                oAccount.Subscribe()

            Next

            If cboAccounts.Items.Count > 0 Then
                cboAccounts.SelectedIndex = 0
            End If

        Catch ex As Exception
            ' Trace Errors.
            Trace.WriteLine(ex.ToString)
        Finally
            ' Unlock the api.
            moHost.ExitLock("AccountSubscribe")
        End Try

    End Sub

    ' Event that is raised when details for an account have 
    ' changed, or a new account is recieved.
    Private Sub moAccounts_AccountDetails( _
        ByVal poAccounts As T4.API.AccountList.UpdateList) _
        Handles moAccounts.AccountDetails

        ' Invoke the update.
        ' This places process on GUI thread.
        ' Must use a delegate to pass arguments.
        If Me.IsHandleCreated Then
            Me.BeginInvoke(New OnAccountDetailsDelegate(AddressOf OnAccountDetails), New Object() {poAccounts})
        Else
            OnAccountDetails(poAccounts)
        End If

    End Sub

    Private Sub OnAccountDetails(ByVal poAccounts As AccountList.UpdateList)

        ' Display the account list.
        For Each oAccount As Account In poAccounts

            ' Check to see if the account exists prior to adding/subscribing to it.
            If Not oAccount.Subscribed Then

                ' Add the account to the list.
                cboAccounts.Items.Add(oAccount)

                ' Subscribe to the account.
                oAccount.Subscribe()

            End If

        Next

    End Sub

    ' Event that is raised when the accounts overall balance,
    ' P&L or margin details have changed.
    Private Sub moAccounts_AccountUpdate( _
        ByVal poAccounts As T4.API.AccountList.UpdateList) _
        Handles moAccounts.AccountUpdate

        ' Invoke the update.
        ' This places process on GUI thread.
        ' Must use a delegate to pass arguments.
        If Me.IsHandleCreated Then
            Me.BeginInvoke(New OnAccountUpdateDelegate(AddressOf OnAccountUpdate), New Object() {poAccounts})
        Else
            OnAccountUpdate(poAccounts)
        End If

    End Sub

    Private Sub OnAccountUpdate(ByVal poAccounts As T4.API.AccountList.UpdateList)

        ' Just refresh the current account.
        DisplayAccount(moAccount)

    End Sub

    ' Event that is raised when positions for accounts have
    ' changed.
    Private Sub moAccounts_PositionUpdate(ByVal poPositions As AccountList.PositionUpdateList) Handles moAccounts.PositionUpdate

        ' Display the position details.
        For Each oUpdate As AccountList.PositionUpdateList.PositionUpdate In poPositions

            ' If the position is for the current account
            ' then update the value.
            If oUpdate.Account Is moAccount Then

                ' Invoke the update.
                ' This places process on GUI thread.
                ' Must use a delegate to pass arguments.
                If Me.IsHandleCreated Then
                    Me.BeginInvoke(New OnPositionUpdateDelegate(AddressOf OnPositionUpdate), New Object() {oUpdate.Position})
                Else
                    OnPositionUpdate(oUpdate.Position)
                End If

                Exit For

            End If

        Next

    End Sub

    Private Sub OnPositionUpdate(ByVal poPosition As T4.API.Position)

        If poPosition.Market Is moMarket1 Then

            ' Display the position details.
            DisplayPosition(poPosition.Market, 1)

        ElseIf poPosition.Market Is moMarket2 Then
          
            ' Display the position details.
            DisplayPosition(poPosition.Market, 2)

        End If

    End Sub

    Private Sub DisplayAccount(ByVal poAccount As Account)

        If Not moAccount Is Nothing Then

            Try

                ' Lock the host while we retrive details.
                moHost.EnterLock("DisplayAccount")

                ' Display the current account balance.
                txtCash.Text = String.Format("{0:#,###,##0.00}", moAccount.AvailableCash)

            Catch ex As Exception
                ' Trace the error.
                Trace.WriteLine("Error: " & ex.ToString)
            Finally

                ' Unlock the host object.
                moHost.ExitLock("DisplayAccount")

            End Try

        End If

    End Sub

    Private Sub DisplayPosition(ByVal poMarket As Market, piID As Integer)

        Dim strNet As String = ""
        Dim strBuys As String = ""
        Dim strSells As String = ""

        Dim blnLocked As Boolean = False

        Try

            If Not poMarket Is Nothing AndAlso Not moAccount Is Nothing Then

                ' Lock the host while we retrive details.
                moHost.EnterLock("DisplayPositions")

                ' Update the locked flag.
                blnLocked = True

                ' Temporary position object used for referencing the account's positions.
                Dim oPosition As Position

                ' Display positions for current account and market1.

                ' Reference the market's positions.
                oPosition = moAccount.Positions(poMarket.MarketID)

                If Not oPosition Is Nothing Then
                    ' Reference the net position.
                    strNet = oPosition.Net.ToString
                    strBuys = oPosition.Buys.ToString
                    strSells = oPosition.Sells.ToString
                End If

                Select Case piID
                    Case 1

                        ' Display the net position.
                        txtNet1.Text = strNet
                        ' Display the total Buys.
                        txtBuys1.Text = strBuys
                        ' Display the total Sells.
                        txtSells1.Text = strSells

                    Case 2

                        ' Display the net position.
                        txtNet2.Text = strNet
                        ' Display the total Buys.
                        txtBuys2.Text = strBuys
                        ' Display the total Sells.
                        txtSells2.Text = strSells

                End Select

            End If

        Catch ex As Exception
            ' Trace the error.
            Trace.WriteLine("Error " & ex.ToString)
        Finally

            ' Unlock the host object.
            If blnLocked Then moHost.ExitLock("DisplayPositions")

        End Try


    End Sub

    Private Sub cboAccounts_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAccounts.SelectedIndexChanged

        If Not cboAccounts.SelectedItem Is Nothing Then

            ' Reference the current account.
            moAccount = DirectCast(cboAccounts.SelectedItem, Account)

            ' Display the current account balance.
            DisplayAccount(moAccount)

            ' Refresh positions.
            DisplayPosition(moMarket1, 1)
            DisplayPosition(moMarket2, 2)

        End If

    End Sub

#End Region

#Region " Startup and shutdown code "

    ' Initialise the api when the application starts.
    Private Sub frmMain_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        ' Create the api host object using the built in Login dialog.
        moHost = Host.Login(APIServerType.Simulator, "T4Example", "112A04B0-5AAF-42F4-994E-FA7CB959C60B")

        ' Check for success.
        If moHost Is Nothing Then

            ' Host object not returned which means the user cancelled the login dialog.
            Me.Close()

        Else

            ' Login was successfull.
            Trace.WriteLine("Login Success")

            ' Initialize.
            Init()

        End If

    End Sub

    ' Shutdown the api when the application exits.
    Private Sub frmMain_Closed(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles MyBase.Closed

        ' Check to see that we have an api object.
        If Not moHost Is Nothing Then

            If Not moMarket1 Is Nothing Then
                RemoveHandler moMarket1.MarketCheckSubscription, AddressOf Markets_MarketCheckSubscription
                RemoveHandler moMarket1.MarketDepthUpdate, AddressOf Markets_MarketDepthUpdate
            End If

            If Not moMarket2 Is Nothing Then
                RemoveHandler moMarket2.MarketCheckSubscription, AddressOf Markets_MarketCheckSubscription
                RemoveHandler moMarket2.MarketDepthUpdate, AddressOf Markets_MarketDepthUpdate
            End If

            ' Dispose of the api.
            moHost.Dispose()
            moHost = Nothing

        End If

    End Sub

#End Region

#Region " Market Subscription "

    Private Sub cmdGet1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGet1.Click

        ' Clear the values.
        DisplayMarketDetails(Nothing, 1)

        ' Subscribe to market1.
        NewMarketSubscription(moMarket1, moPickerMarket)

        ' Refresh the positions.
        DisplayPosition(moMarket1, 1)

    End Sub

    Private Sub cmdGet2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGet2.Click

        Dim oMarket As Market = moHost.MarketData.MarketPicker(moMarket2)

        ' Clear the values.
        DisplayMarketDetails(Nothing, 2)

        ' Refresh the positions.
        DisplayPosition(moMarket2, 2)

        ' Subscribe to market2.
        NewMarketSubscription(moMarket2, oMarket)

    End Sub

    Private Sub NewMarketSubscription(ByRef poMarket As Market, ByRef poNewMarket As Market)
        ' Update an existing market reference to subscribe to a new/different market.

        ' If they are the same then don't do anything.
        ' We don't need to resubscribe to the same market.

        ' Explicitly register events as opposed to declaring withevents.
        ' This gives us more control.  
        ' It is important to unregister the marketchecksubscription prior to unsubscribing or the event will override and maintain the subscription.

        If (Not Object.ReferenceEquals(poMarket, poNewMarket)) Then
            ' Unsubscribe from the currently selected market.
            If (poMarket IsNot Nothing) Then

                ' Unregister the events for this market.
                RemoveHandler poMarket.MarketCheckSubscription, AddressOf Markets_MarketCheckSubscription
                RemoveHandler poMarket.MarketDepthUpdate, AddressOf Markets_MarketDepthUpdate

                poMarket.DepthUnsubscribe()

            End If

            ' Update the market reference.
            poMarket = poNewMarket

            If (poMarket IsNot Nothing) Then

                ' Register the events.
                AddHandler poMarket.MarketCheckSubscription, AddressOf Markets_MarketCheckSubscription
                AddHandler poMarket.MarketDepthUpdate, AddressOf Markets_MarketDepthUpdate

                ' Subscribe to the market.
                ' Use smart buffering.

                poMarket.DepthSubscribe(DepthBuffer.Smart, DepthLevels.BestOnly)

            End If
        End If

    End Sub


    Private Sub Markets_MarketCheckSubscription(ByVal poMarket As T4.API.Market, ByRef penDepthBuffer As T4.DepthBuffer, ByRef penDepthLevels As T4.DepthLevels)

        ' No need to invoke on the gui thread.
        penDepthBuffer = poMarket.DepthSubscribeAtLeast(DepthBuffer.Smart, penDepthBuffer)
        penDepthLevels = poMarket.DepthSubscribeAtLeast(DepthLevels.BestOnly, penDepthLevels)

    End Sub

    Private Sub Markets_MarketDepthUpdate(ByVal poMarket As T4.API.Market)

        ' Invoke the update.
        ' This places process on GUI thread.
        ' Must use a delegate to pass arguments.
        If Me.IsHandleCreated Then
            Me.BeginInvoke(New OnMarketDepthUpdateDelegate(AddressOf OnMarketDepthUpdate), New Object() {poMarket})
        Else
            OnMarketDepthUpdate(poMarket)
        End If

    End Sub

    Private Sub OnMarketDepthUpdate(ByVal poMarket As Market)

        Try

            If poMarket Is moMarket1 Then

                DisplayMarketDetails(poMarket, 1)

            ElseIf poMarket Is moMarket2 Then

                DisplayMarketDetails(poMarket, 2)

            End If

        Catch ex As Exception

            ' Trace the error.
            Trace.WriteLine("Error " & ex.ToString)

        End Try

    End Sub

    ''' <summary>
    ''' Update the market display values.
    ''' </summary>
    Private Sub DisplayMarketDetails(poMarket As Market, piID As Integer)

        Dim strDescription As String = ""
        Dim strBid As String = ""
        Dim strBidVol As String = ""
        Dim strOffer As String = ""
        Dim strOfferVol As String = ""
        Dim strLast As String = ""
        Dim strLastVol As String = ""
        Dim strLastVolTotal As String = ""

        If Not poMarket Is Nothing Then

            Try

                ' Lock the host while we retrive details.
                moHost.EnterLock("DisplayMarketDetails")

                ' Display the market description.
                strDescription = poMarket.Description

                If Not poMarket.LastDepth Is Nothing Then

                    ' Best bid.
                    If poMarket.LastDepth.Bids.Count > 0 Then
                        strBid = poMarket.ConvertTicksDisplay(poMarket.LastDepth.Bids(0).Ticks)
                        strBidVol = poMarket.LastDepth.Bids(0).Volume.ToString
                    End If

                    ' Best offer.
                    If poMarket.LastDepth.Offers.Count > 0 Then
                        strOffer = poMarket.ConvertTicksDisplay(poMarket.LastDepth.Offers(0).Ticks)
                        strOfferVol = poMarket.LastDepth.Offers(0).Volume.ToString
                    End If

                    ' Last trade.
                    strLast = poMarket.ConvertTicksDisplay(poMarket.LastDepth.LastTradeTicks)
                    strLastVol = poMarket.LastDepth.LastTradeVolume.ToString
                    strLastVolTotal = poMarket.LastDepth.LastTradeTotalVolume.ToString

                End If

            Catch ex As Exception
                ' Trace the error.
                Trace.WriteLine("Error " & ex.ToString)
            Finally

                ' Unlock the host object.
                moHost.ExitLock("DisplayMarketDetails")

            End Try

        End If

        Select Case piID
            Case 1

                ' Update the market1 display values.
                txtMarketDescription1.Text = strDescription
                txtBid1.Text = strBid
                txtBidVol1.Text = strBidVol
                txtOffer1.Text = strOffer
                txtOfferVol1.Text = strOfferVol
                txtLast1.Text = strLast
                txtLastVol1.Text = strLastVol
                txtLastVolTotal1.Text = strLastVolTotal

            Case 2

                ' Update the market2 display values.
                txtMarketDescription2.Text = strDescription
                txtBid2.Text = strBid
                txtBidVol2.Text = strBidVol
                txtOffer2.Text = strOffer
                txtOfferVol2.Text = strOfferVol
                txtLast2.Text = strLast
                txtLastVol2.Text = strLastVol
                txtLastVolTotal2.Text = strLastVolTotal

        End Select

    End Sub

#End Region

#Region " Save Settings "

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Try

            ' XML Doc.
            Dim oDoc As New XmlDocument

            ' XML Node.
            Dim oMarket As XmlNode
            Dim oMarkets As XmlNode
            Dim oAttribute As XmlAttribute

            ' Create the main node.
            oMarkets = oDoc.CreateNode(XmlNodeType.Element, "markets", "")
            oDoc.AppendChild(oMarkets)

            If Not moMarket1 Is Nothing Then

                ' Create a node.
                oMarket = oDoc.CreateNode(XmlNodeType.Element, "market1", "")

                ' Exchange ID.
                oAttribute = oDoc.CreateAttribute("ExchangeID")
                oAttribute.Value = moMarket1.ExchangeID
                oMarket.Attributes.Append(oAttribute)

                ' Contract ID.
                oAttribute = oDoc.CreateAttribute("ContractID")
                oAttribute.Value = moMarket1.ContractID
                oMarket.Attributes.Append(oAttribute)

                ' Market ID.
                oAttribute = oDoc.CreateAttribute("MarketID")
                oAttribute.Value = moMarket1.MarketID
                oMarket.Attributes.Append(oAttribute)

                ' Add the node to the xml document.
                oMarkets.AppendChild(oMarket)

            End If

            If Not moMarket2 Is Nothing Then

                ' Create a node.
                oMarket = oDoc.CreateNode(XmlNodeType.Element, "market2", "")

                ' Exchange ID.
                oAttribute = oDoc.CreateAttribute("ExchangeID")
                oAttribute.Value = moMarket2.ExchangeID
                oMarket.Attributes.Append(oAttribute)

                ' Contract ID.
                oAttribute = oDoc.CreateAttribute("ContractID")
                oAttribute.Value = moMarket2.ContractID
                oMarket.Attributes.Append(oAttribute)

                ' Market ID.
                oAttribute = oDoc.CreateAttribute("MarketID")
                oAttribute.Value = moMarket2.MarketID
                oMarket.Attributes.Append(oAttribute)

                ' Add the node to the xml document.
                oMarkets.AppendChild(oMarket)

            End If

            ' Save the xml to the server.
            moHost.UserSettings = oDoc
            moHost.SaveUserSettings()

        Catch ex As Exception

            ' Trace.
            Trace.WriteLine(ex.ToString)

        End Try

    End Sub

    Public Function App_Path() As String
        Return System.AppDomain.CurrentDomain.BaseDirectory()
    End Function

#End Region

#Region " Single Order "

    ' Method that submits a single order.
    Private Sub SubmitSingleOrder(ByVal poMarket As Market, ByVal peBuySell As BuySell, ByVal pdblLimitPrice As Double)

        If Not moAccount Is Nothing AndAlso Not poMarket Is Nothing Then
            ' Submit an order.
            Dim oOrder As Order = moAccounts.SubmitNewOrder( _
                moAccount, _
                poMarket, _
                peBuySell, _
                PriceType.Limit, _
                TimeType.Normal, _
                1, _
                pdblLimitPrice)

            ' Add the order to the arraylist.
            AddOrder(oOrder)

            ' Display the orders.
            DisplayOrders()

        End If

    End Sub

    ' Pull the single order that was submitted.
    Private Sub PullSingleOrder(ByVal poOrder As Order)

        ' Check to see that we have an order.
        If Not poOrder Is Nothing Then

            ' Check to see if the order is working.
            If poOrder.IsWorking Then

                ' Pull the order.
                poOrder.Pull()

            End If

        End If

    End Sub

#End Region

#Region " Submission/Cancelation "

    Private Sub cmdBuy1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBuy1.Click
        ' Submit a single order.
        SubmitSingleOrder(moMarket1, BuySell.Buy, Val(txtBid1.Text))
    End Sub
    Private Sub cmdSell1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSell1.Click
        ' Submit a single order.
        SubmitSingleOrder(moMarket1, BuySell.Sell, Val(txtOffer1.Text))
    End Sub

    Private Sub cmdSell2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSell2.Click
        ' Submit a single order.
        SubmitSingleOrder(moMarket2, BuySell.Sell, Val(txtOffer2.Text))
    End Sub

    Private Sub cmdBuy2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBuy2.Click
        ' Submit a single order.
        SubmitSingleOrder(moMarket2, BuySell.Buy, Val(txtBid2.Text))
    End Sub

    Private Sub lstOrders_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstOrders.DoubleClick

        ' Pull the order that has been double clicked on.
        Dim iOrderIndex As Integer

        ' Be sure that the selected index is valid.
        If lstOrders.SelectedIndex >= 0 And lstOrders.SelectedIndex <= lstOrders.Items.Count - 1 Then

            ' The orders were listed in reverse so we need 
            ' to calculate the index of the order within the arraylist.
            iOrderIndex = lstOrders.Items.Count - lstOrders.SelectedIndex - 1

            ' Reference the order in the collection.
            Dim oOrder As Order = CType(moOrderArrayList(iOrderIndex), Order)

            ' Attempt to pull the order.
            PullSingleOrder(oOrder)

        End If

    End Sub

#End Region

#Region " Order Data "

    Private Sub moAccount_OrderUpdate(ByVal poAccount As T4.API.Account, ByVal poPosition As T4.API.Position, ByVal poOrders As T4.API.OrderList.UpdateList) Handles moAccount.OrderUpdate

        ' Trace.WriteLine("moAccount_OrderUpdate Start")

        ' Invoke the update.
        ' This places process on GUI thread.
        ' Must use a delegate to pass arguments.
        If Me.IsHandleCreated Then
            Me.BeginInvoke(New OnAccountOrderUpdateDelegate(AddressOf OnAccountOrderUpdate), New Object() {poAccount, poPosition, poOrders})
        Else
            OnAccountOrderUpdate(poAccount, poPosition, poOrders)
        End If

        ' Trace.WriteLine("moAccount_OrderUpdate End")

    End Sub

    Private Sub moAccount_OrderAdded(ByVal poAccount As T4.API.Account, ByVal poPosition As T4.API.Position, ByVal poOrders As T4.API.OrderList.UpdateList) Handles moAccount.OrderAdded

        ' Invoke the update.
        ' This places process on GUI thread.
        ' Must use a delegate to pass arguments.
        If Me.IsHandleCreated Then
            Me.BeginInvoke(New OnAccountOrderAddedDelegate(AddressOf OnAccountOrderAdded), New Object() {poAccount, poPosition, poOrders})
        Else
            OnAccountOrderAdded(poAccount, poPosition, poOrders)
        End If

    End Sub

    Private Sub OnAccountOrderUpdate(ByVal poAccount As T4.API.Account, ByVal poPosition As T4.API.Position, ByVal poOrders As T4.API.OrderList.UpdateList)

        ' Trace.WriteLine("OnAccountOrderUpdate Start")

        ' Redraw the order list.
        DisplayOrders()

        ' Trace.WriteLine("OnAccountOrderUpdate End")

    End Sub

    Private Sub OnAccountOrderAdded(ByVal poAccount As T4.API.Account, ByVal poPosition As T4.API.Position, ByVal poOrders As T4.API.OrderList.UpdateList)

        ' Add all the orders to the arraylist.
        For Each oOrder As Order In poOrders

            ' Add the order.
            AddOrder(oOrder)

        Next

        ' Redraw the order list.
        DisplayOrders()

    End Sub

    Private Sub AddOrder(ByVal poOrder As Order)

        ' Add the order to the arraylist.

        If Not poOrder Is Nothing Then
            If Not moOrderArrayList.Contains(poOrder) Then

                ' Add the order to the arraylist.
                moOrderArrayList.Add(poOrder)

            End If
        End If

    End Sub

    Private Sub DisplayOrders()

        ' Trace.WriteLine("DisplayOrders Start")

        Try

            ' Lock the api.
            moHost.EnterLock("DisplayOrders")

            ' Suspend the layout of the listbox.
            lstOrders.SuspendLayout()

            ' Clear and repopulate the list.
            lstOrders.Items.Clear()

            ' Temporary order object.
            Dim oOrder As Order

            ' Itterate through the collection backwards.
            For i As Integer = moOrderArrayList.Count - 1 To 0 Step -1

                ' Reference an order.
                oOrder = CType(moOrderArrayList(i), Order)

                ' Display some order details.
                With oOrder

                    lstOrders.Items.Add(.Market.Description & "   " & _
                    .BuySell.ToString & "   " & _
                    .TotalFillVolume & "/" & .CurrentVolume & " @ " & _
                    .Market.ConvertTicksDisplay(.CurrentLimitTicks) & "   " & _
                    .Status.ToString & "   " & _
                    .StatusDetail & "  " & _
                    .SubmitTime)

                End With

            Next

        Catch ex As Exception

            ' Trace the error.
            Trace.WriteLine("Error: " & ex.ToString)

        Finally

            ' Unlock the api.
            moHost.ExitLock("DisplayOrders")

            ' Resume layout of the listbox.
            lstOrders.ResumeLayout()

        End Try

        ' Trace.WriteLine("DisplayOrders End")

    End Sub

#End Region

#Region " Misc Examples "

    Const AUTOOCO As String = "Submit Auto OCO"
    Const FIVETICKSOFF As String = "Work 5 Ticks Off Market"
    Const TIMEACTIVATION As String = "Time Activation"
    Const PRICEACTIVATION As String = "Price Activation"
    Const SPARK As String = "Spark Order"
    Const REVISESPARKORDER As String = "Revise Spark"

    ' Setup misc example combos.
    Private Sub SetupMiscExamples()

        ' Add examples to combos.
        cboMisc1.Items.Add(AUTOOCO)
        cboMisc1.Items.Add(SPARK)
        cboMisc1.Items.Add(REVISESPARKORDER)
        cboMisc1.Items.Add(FIVETICKSOFF)
        cboMisc1.Items.Add(TIMEACTIVATION)
        cboMisc1.Items.Add(PRICEACTIVATION)


        cboMisc2.Items.Add(AUTOOCO)
        cboMisc2.Items.Add(SPARK)
        cboMisc2.Items.Add(REVISESPARKORDER)
        cboMisc2.Items.Add(FIVETICKSOFF)
        cboMisc2.Items.Add(TIMEACTIVATION)
        cboMisc2.Items.Add(PRICEACTIVATION)


        ' Be sure the first items are selected.
        cboMisc1.SelectedIndex = 0
        cboMisc2.SelectedIndex = 0

    End Sub

    Private Sub cmdRunMisc1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRunMisc1.Click

        If Not moMarket1 Is Nothing Then

            Select Case cboMisc1.Text
                Case AUTOOCO

                    ' Run autooco sample code.
                    SubmitAOCO(moMarket1, BuySell.Buy, txtBid1.Text)

                Case FIVETICKSOFF

                    ' Run the five ticks off code.
                    SubmitFiveTicksOff(moMarket1, BuySell.Buy, txtBid1.Text)

                Case TIMEACTIVATION

                    ' Time activation order.
                    SubmitTimeActivation(moMarket1, BuySell.Buy, txtBid1.Text)

                Case PRICEACTIVATION

                    ' Time activation order.
                    SubmitPriceActivation(moMarket1, BuySell.Buy, txtBid1.Text)

                Case SPARK

                    ' Spark order.
                    SubmitSpark(moMarket1, BuySell.Sell, txtOffer1.Text)

                Case REVISESPARKORDER

                    ' Revise the spark.
                    ReviseSpark()

            End Select

        End If

    End Sub

    Private Sub cmdRunMisc2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRunMisc2.Click

        If Not moMarket2 Is Nothing Then

            Select Case cboMisc2.Text
                Case AUTOOCO

                    ' Run autooco sample code.
                    SubmitAOCO(moMarket2, BuySell.Sell, txtOffer2.Text)

                Case FIVETICKSOFF

                    ' Run the five ticks off code.
                    SubmitFiveTicksOff(moMarket2, BuySell.Sell, txtOffer2.Text)


                Case TIMEACTIVATION

                    ' Time activation order.
                    SubmitTimeActivation(moMarket2, BuySell.Buy, txtBid2.Text)

                Case PRICEACTIVATION

                    ' Time activation order.
                    SubmitPriceActivation(moMarket2, BuySell.Buy, txtBid2.Text)

                Case SPARK

                    ' Spark order.
                    SubmitSpark(moMarket2, BuySell.Sell, txtOffer2.Text)

            End Select

        End If

    End Sub

#Region " Auto OCO "

    ' Simple example of how to submit and cancel an Auto OCO.
    Private Sub SubmitAOCO(ByVal poMarket As Market, ByVal peBuySell As BuySell, ByVal pstrLimitDisplayPrice As String)


        If Not moAccount Is Nothing AndAlso Not poMarket Is Nothing Then

            ' Limit price reference.
            ' Convert the limit price to a double.
            Dim dblLimitPrice As Double = Val(pstrLimitDisplayPrice)

            ' Create the batch submission object.
            Dim oBatch As OrderList.Submission
            oBatch = moAccounts.SubmitOrders(moAccount, poMarket)

            ' Set the order link.
            oBatch.OrderLink = OrderLink.AutoOCO

            ' Add an order to the batch.
            ' This is the trigger order.
            Dim oOrder1 As Order = oBatch.Add(peBuySell, _
                PriceType.Limit, _
                TimeType.Normal, _
                1, _
                dblLimitPrice)

            If peBuySell = BuySell.Buy Then

                ' Add an order to the batch.
                ' This is the sell limit of the oco above the market.
                ' Note the flip of Buy/Sell.
                ' Note the ticks is a distance not a price representation.
                Dim oOrder2 As Order = oBatch.Add(BuySell.Sell, _
                    PriceType.Limit, _
                    TimeType.Normal, _
                     0, _
                    poMarket.ConvertTicks(poMarket.TicksAdd(5, 0)))

                ' Add an order to the batch.
                ' This is the stop of the oco below the market.
                ' Note the flip of Buy/Sell.
                ' Note the ticks is a distance not a price representation.
                Dim oOrder3 As Order = oBatch.Add(BuySell.Sell, _
                    PriceType.StopLimit, _
                    TimeType.Normal, _
                    0, _
                    poMarket.ConvertTicks(poMarket.TicksAdd(-7, 0)), _
                    poMarket.ConvertTicks(poMarket.TicksAdd(-5, 0)), _
                    OpenClose.Undefined, "", 0, ActivationType.Immediate, "", 0, Nothing, Nothing, True, Nothing, True)

            Else

                ' Add an order to the batch.
                ' This is the buy limit of the oco below the market.
                ' Note the flip of Buy/Sell.
                ' Note the ticks is a distance not a price representation.
                Dim oOrder2 As Order = oBatch.Add(BuySell.Buy, _
                    PriceType.Limit, _
                    TimeType.Normal, _
                    0, _
                    poMarket.ConvertTicks(poMarket.TicksAdd(-5, 0)))

                ' Add an order to the batch.
                ' This is the buy stop of the oco above the market.
                ' Note the flip of Buy/Sell.
                ' Note the ticks is a distance not a price representation.
                Dim oOrder3 As Order = oBatch.Add(BuySell.Buy, _
                    PriceType.StopMarket, _
                    TimeType.Normal, _
                    0, _
                    0.0, _
                    poMarket.ConvertTicks(poMarket.TicksAdd(5, 0)), _
                    OpenClose.Undefined, "", 0, ActivationType.Immediate, "", 0, Nothing, Nothing, True, Nothing, True)

            End If


            ' Submit the batch.
            oBatch.Submit()

            ' Display the orders.
            DisplayOrders()


            ' Pull may fail if attempted too soon.
            ' Like 1 millisecond later.

            '' This is how you would cancel the batch.
            'Dim oBatchPull As OrderList.Pull = moAccounts.PullOrders(moAccount, poMarket)

            '' Add the orders to the pull.
            'oBatchPull.Add(oOrder1)
            'oBatchPull.Add(oOrder2)
            'oBatchPull.Add(oOrder3)

            '' Pull the batch.
            'oBatchPull.Pull()

            '' Add the orders to the arraylist.
            'AddOrder(oOrder1)
            'AddOrder(oOrder2)
            'AddOrder(oOrder3)


        End If

    End Sub

#End Region

#Region " Spark order "

    Private moOrder1 As Order
    Private moOrder2 As Order

    ' Simple example of how to submit and cancel a Spark order.
    Private Sub SubmitSpark(ByVal poMarket As Market, ByVal peBuySell As BuySell, ByVal pstrLimitDisplayPrice As String)


        If Not moAccount Is Nothing AndAlso Not poMarket Is Nothing Then

            ' Limit price reference.
            ' Convert the limit price to a double.
            Dim dblLimitPrice As Double = Val(pstrLimitDisplayPrice)

            ' Create the batch submission object.
            Dim oBatch As OrderList.Submission
            oBatch = moAccounts.SubmitOrders(moAccount, poMarket)

            ' Set the order link.
            oBatch.OrderLink = OrderLink.Spark

            ' Add an order to the batch.
            ' This is the trigger order.
            Dim oOrder1 As Order = oBatch.Add(peBuySell, _
                PriceType.Limit, _
                TimeType.Normal, _
                1, _
                dblLimitPrice)

            Dim oOrder2 As Order

            If peBuySell = BuySell.Buy Then

                ' Add an order to the batch.
                ' This is the sell limit of the oco above the market.
                ' Note the flip of Buy/Sell.
                ' Note the ticks is a distance not a price representation.
                oOrder2 = oBatch.Add(BuySell.Sell, _
                    PriceType.Limit, _
                    TimeType.Normal, _
                     0, _
                    poMarket.ConvertTicks(poMarket.TicksAdd(5, 0)))

            Else

                ' Add an order to the batch.
                ' This is the buy limit of the oco below the market.
                ' Note the flip of Buy/Sell.
                ' Note the ticks is a distance not a price representation.
                oOrder2 = oBatch.Add(BuySell.Buy, _
                    PriceType.Limit, _
                    TimeType.Normal, _
                    0, _
                     poMarket.ConvertTicks(poMarket.TicksAdd(-5, 0)))


            End If

            moOrder1 = oOrder1
            moOrder2 = oOrder2

            ' Submit the batch.
            oBatch.Submit()

            ' Display the orders.
            DisplayOrders()

        End If

    End Sub

    Private Sub ReviseSpark()

        If Not moOrder1 Is Nothing AndAlso Not moOrder2 Is Nothing Then

            ' Create the batch revision object.
            Dim oBatch As OrderList.Revision
            oBatch = moAccounts.ReviseOrders(moAccount, moOrder1.Market)

            oBatch.AddTicks(moOrder1, 2, moOrder1.CurrentLimitTicks)
            oBatch.AddTicks(moOrder2, 2, moOrder2.CurrentLimitTicks)

            oBatch.Revise()

        End If

    End Sub

#End Region

#Region " Work Order Five Ticks From Market "

    ' Place an order five ticks off the market.
    Private Sub SubmitFiveTicksOff(ByVal poMarket As Market, ByVal peBuySell As BuySell, ByVal pstrLimitDisplayPrice As String)

        ' Limit price reference.
        ' Convert the limit price to a double.
        Dim dblLimitPrice As Double = Val(pstrLimitDisplayPrice)

        ' Convert the price to ticks.
        Dim iTicks As Integer = poMarket.ConvertPrice(dblLimitPrice)
        Dim iNewTicks As Integer

        ' Add or subtract five ticks from the current price depending on what side of the market we are.
        If peBuySell = BuySell.Buy Then
            iNewTicks = poMarket.TicksAdd(-5, iTicks)
        Else
            iNewTicks = poMarket.TicksAdd(5, iTicks)
        End If

        Dim iNewPrice As Double = poMarket.ConvertTicks(iNewTicks)

        ' Submit a single order five ticks off the market.
        SubmitSingleOrder(poMarket, peBuySell, iNewPrice)

    End Sub

#End Region

#Region " Submit Activation "

    ' Place an order five ticks off the market.
    Private Sub SubmitTimeActivation(ByVal poMarket As Market, ByVal peBuySell As BuySell, ByVal pstrLimitDisplayPrice As String)

        ' Limit price reference.
        ' Convert the limit price to a double.
        Dim dblLimitPrice As Double = Val(pstrLimitDisplayPrice)

        If Not moAccount Is Nothing AndAlso Not poMarket Is Nothing Then

            Dim dServerTime As DateTime = moHost.RemoteTime

            Dim dSubmit As DateTime = dServerTime.AddSeconds(10)
            Dim dCancel As DateTime = dServerTime.AddSeconds(30)

            ' Submit an order.
            Dim oOrder As Order = moAccounts.SubmitNewOrder( _
                moAccount, _
                poMarket, _
                peBuySell, _
                PriceType.Limit, _
                TimeType.Normal, _
                1, _
                dblLimitPrice, 0, OpenClose.Undefined, "", 0, ActivationType.AtOrAfterTime, dSubmit.ToString("dd MMM yyyy hh:mm:ss tt") & ";" & dCancel.ToString("dd MMM yyyy hh:mm:ss tt"), Nothing, Nothing)

            ' The following also works.  The integer values represent seconds of time laps from now.
            '' Submit an order.
            'Dim oOrder As Order = moAccounts.SubmitNewOrder3( _
            '    moAccount, _
            '    poMarket, _
            '    peBuySell, _
            '    PriceType.Limit, _
            '    TimeType.Normal, _
            '    1, _
            '    dblLimitPrice, , , , , ActivationType.AtOrAfterTime, "10;30")

            ' Add the order to the arraylist.
            AddOrder(oOrder)

            ' Display the orders.
            DisplayOrders()

        End If

    End Sub

    ' Place an order five ticks off the market.
    Private Sub SubmitPriceActivation(ByVal poMarket As Market, ByVal peBuySell As BuySell, ByVal pstrLimitDisplayPrice As String)

        ' Limit price reference.
        ' Convert the limit price to a double.
        Dim dblLimitPrice As Double = Val(pstrLimitDisplayPrice)

        If Not moAccount Is Nothing AndAlso Not poMarket Is Nothing Then

            Dim dServerTime As DateTime = moHost.RemoteTime
            Dim dCancel As DateTime = dServerTime.AddSeconds(30)

            ' Submit an order.
            ' This order will activate if a price trades that greater than or equal to five ticks above the current best bid.
            ' The order will be canceled after 30 seconds.
            Dim oOrder As Order = moAccounts.SubmitNewOrder( _
                moAccount, _
                poMarket, _
                peBuySell, _
                PriceType.Limit, _
                TimeType.Normal, _
                1, _
                dblLimitPrice, 0, OpenClose.Undefined, "", 0, ActivationType.AtOrAboveTradeTicks, poMarket.TicksAdd(5, poMarket.LastDepth.Bids(0).Ticks).ToString & ";" & dCancel.ToString("dd MMM yyyy hh:mm:ss tt"), Nothing, Nothing)

            ' Add the order to the arraylist.
            AddOrder(oOrder)

            ' Display the orders.
            DisplayOrders()

        End If

    End Sub

#End Region

#End Region

End Class
