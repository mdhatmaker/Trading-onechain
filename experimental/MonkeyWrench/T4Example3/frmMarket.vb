Imports T4
Imports T4.API
Imports System.Xml

Public Class frmMarket
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal poAPI As T4.API.Host, ByVal pstrExchangeID As String, ByVal pstrContractID As String, ByVal pstrMarketID As String)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Reference the api.
        moHost = poAPI

        ' Reference the marketid.
        mstrMarketID = pstrMarketID

        ' Create a market filter for the desired exchange and contract.
        moMarkets = moHost.MarketData.CreateMarketFilter(pstrExchangeID, pstrContractID)

        If moMarkets.Complete Then

            ' Call the event handler directly as the list is already complete.
            OnMarketListComplete(moMarkets)

        End If


    End Sub

    Private Sub OnMarketListComplete(ByVal poMarketList As T4.API.MarketList) Handles moMarkets.MarketListComplete

        ' Invoke the update.
        ' This places process on GUI thread.
        If Me.IsHandleCreated Then
            Me.BeginInvoke(New MethodInvoker(AddressOf MarketListComplete))
        Else
            MarketListComplete()
        End If

    End Sub

    Private Sub MarketListComplete()

        ' Reference the desired market.
        Dim oMarket As Market = moMarkets(mstrMarketID)

        ' Subscribe to market.
        Subscribe(oMarket)

    End Sub

    Public Sub New(ByVal poAPI As API.Host, ByVal poMarket As Market)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        ' Reference the api.
        moHost = poAPI

        ' Subscribe to the market.
        Subscribe(poMarket)

    End Sub

    ' Subscribe to the market.
    Private Sub Subscribe(ByVal poMarket As Market)

        If poMarket Is Nothing Then

            ' Throws a new exception.
            Throw New System.Exception("Invalid Market")

        Else

            ' Store a reference to the market.
            moMarket = poMarket

            ' Subscribe to the market.
            ' Use smart buffering.
            moMarket.DepthSubscribe(DepthBuffer.Smart, DepthLevels.BestOnly)

            ' Display the market description.
            Me.Text = moMarket.Description

        End If

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then

            If Not moMarket Is Nothing Then

                ' Unsubscribe to the market.
                moMarket.DepthUnsubscribe()

                ' Set the object to nothing to prevent events.
                moMarket = Nothing

            End If

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
    Friend WithEvents lblLastPrice As System.Windows.Forms.Label
    Friend WithEvents lblOfferPrice As System.Windows.Forms.Label
    Friend WithEvents lblBidPrice As System.Windows.Forms.Label
    Friend WithEvents lblLast As System.Windows.Forms.Label
    Friend WithEvents lblBid As System.Windows.Forms.Label
    Friend WithEvents lblOffer As System.Windows.Forms.Label
    Friend WithEvents lblTotalVol As System.Windows.Forms.Label
    Friend WithEvents lblLastVol As System.Windows.Forms.Label
    Friend WithEvents lblOfferVol As System.Windows.Forms.Label
    Friend WithEvents lblBidVol As System.Windows.Forms.Label
    Friend WithEvents txtOfferVol As System.Windows.Forms.TextBox
    Friend WithEvents txtLastVolTotal As System.Windows.Forms.TextBox
    Friend WithEvents txtLast As System.Windows.Forms.TextBox
    Friend WithEvents txtLastVol As System.Windows.Forms.TextBox
    Friend WithEvents txtBid As System.Windows.Forms.TextBox
    Friend WithEvents txtOffer As System.Windows.Forms.TextBox
    Friend WithEvents txtBidVol As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lblLastPrice = New System.Windows.Forms.Label
        Me.lblOfferPrice = New System.Windows.Forms.Label
        Me.lblBidPrice = New System.Windows.Forms.Label
        Me.txtOfferVol = New System.Windows.Forms.TextBox
        Me.txtLastVolTotal = New System.Windows.Forms.TextBox
        Me.txtLast = New System.Windows.Forms.TextBox
        Me.lblLast = New System.Windows.Forms.Label
        Me.txtLastVol = New System.Windows.Forms.TextBox
        Me.txtBid = New System.Windows.Forms.TextBox
        Me.lblBid = New System.Windows.Forms.Label
        Me.lblOffer = New System.Windows.Forms.Label
        Me.txtOffer = New System.Windows.Forms.TextBox
        Me.txtBidVol = New System.Windows.Forms.TextBox
        Me.lblTotalVol = New System.Windows.Forms.Label
        Me.lblLastVol = New System.Windows.Forms.Label
        Me.lblOfferVol = New System.Windows.Forms.Label
        Me.lblBidVol = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lblLastPrice
        '
        Me.lblLastPrice.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastPrice.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblLastPrice.Location = New System.Drawing.Point(200, 24)
        Me.lblLastPrice.Name = "lblLastPrice"
        Me.lblLastPrice.Size = New System.Drawing.Size(60, 20)
        Me.lblLastPrice.TabIndex = 44
        Me.lblLastPrice.Text = "Price:"
        Me.lblLastPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblOfferPrice
        '
        Me.lblOfferPrice.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOfferPrice.ForeColor = System.Drawing.Color.Crimson
        Me.lblOfferPrice.Location = New System.Drawing.Point(104, 24)
        Me.lblOfferPrice.Name = "lblOfferPrice"
        Me.lblOfferPrice.Size = New System.Drawing.Size(60, 20)
        Me.lblOfferPrice.TabIndex = 43
        Me.lblOfferPrice.Text = "Price:"
        Me.lblOfferPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBidPrice
        '
        Me.lblBidPrice.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBidPrice.ForeColor = System.Drawing.Color.RoyalBlue
        Me.lblBidPrice.Location = New System.Drawing.Point(8, 24)
        Me.lblBidPrice.Name = "lblBidPrice"
        Me.lblBidPrice.Size = New System.Drawing.Size(60, 20)
        Me.lblBidPrice.TabIndex = 42
        Me.lblBidPrice.Text = "Price:"
        Me.lblBidPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtOfferVol
        '
        Me.txtOfferVol.BackColor = System.Drawing.Color.MistyRose
        Me.txtOfferVol.Location = New System.Drawing.Point(168, 48)
        Me.txtOfferVol.Name = "txtOfferVol"
        Me.txtOfferVol.ReadOnly = True
        Me.txtOfferVol.Size = New System.Drawing.Size(28, 20)
        Me.txtOfferVol.TabIndex = 36
        Me.txtOfferVol.TabStop = False
        Me.txtOfferVol.Text = ""
        Me.txtOfferVol.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtLastVolTotal
        '
        Me.txtLastVolTotal.BackColor = System.Drawing.Color.Honeydew
        Me.txtLastVolTotal.Location = New System.Drawing.Point(296, 48)
        Me.txtLastVolTotal.Name = "txtLastVolTotal"
        Me.txtLastVolTotal.ReadOnly = True
        Me.txtLastVolTotal.Size = New System.Drawing.Size(60, 20)
        Me.txtLastVolTotal.TabIndex = 40
        Me.txtLastVolTotal.TabStop = False
        Me.txtLastVolTotal.Text = ""
        Me.txtLastVolTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtLast
        '
        Me.txtLast.BackColor = System.Drawing.Color.Honeydew
        Me.txtLast.Location = New System.Drawing.Point(200, 48)
        Me.txtLast.Name = "txtLast"
        Me.txtLast.ReadOnly = True
        Me.txtLast.Size = New System.Drawing.Size(60, 20)
        Me.txtLast.TabIndex = 37
        Me.txtLast.TabStop = False
        Me.txtLast.Text = ""
        Me.txtLast.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblLast
        '
        Me.lblLast.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLast.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblLast.Location = New System.Drawing.Point(200, 8)
        Me.lblLast.Name = "lblLast"
        Me.lblLast.Size = New System.Drawing.Size(152, 18)
        Me.lblLast.TabIndex = 38
        Me.lblLast.Text = "Last Trade:"
        Me.lblLast.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtLastVol
        '
        Me.txtLastVol.BackColor = System.Drawing.Color.Honeydew
        Me.txtLastVol.Location = New System.Drawing.Point(264, 48)
        Me.txtLastVol.Name = "txtLastVol"
        Me.txtLastVol.ReadOnly = True
        Me.txtLastVol.Size = New System.Drawing.Size(28, 20)
        Me.txtLastVol.TabIndex = 39
        Me.txtLastVol.TabStop = False
        Me.txtLastVol.Text = ""
        Me.txtLastVol.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBid
        '
        Me.txtBid.BackColor = System.Drawing.Color.LightCyan
        Me.txtBid.Location = New System.Drawing.Point(8, 48)
        Me.txtBid.Name = "txtBid"
        Me.txtBid.ReadOnly = True
        Me.txtBid.Size = New System.Drawing.Size(60, 20)
        Me.txtBid.TabIndex = 31
        Me.txtBid.TabStop = False
        Me.txtBid.Text = ""
        Me.txtBid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblBid
        '
        Me.lblBid.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBid.ForeColor = System.Drawing.Color.RoyalBlue
        Me.lblBid.Location = New System.Drawing.Point(8, 8)
        Me.lblBid.Name = "lblBid"
        Me.lblBid.Size = New System.Drawing.Size(89, 18)
        Me.lblBid.TabIndex = 32
        Me.lblBid.Text = "Best Bid:"
        Me.lblBid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblOffer
        '
        Me.lblOffer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOffer.ForeColor = System.Drawing.Color.Crimson
        Me.lblOffer.Location = New System.Drawing.Point(104, 8)
        Me.lblOffer.Name = "lblOffer"
        Me.lblOffer.Size = New System.Drawing.Size(90, 18)
        Me.lblOffer.TabIndex = 34
        Me.lblOffer.Text = "Best Offer:"
        Me.lblOffer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtOffer
        '
        Me.txtOffer.BackColor = System.Drawing.Color.MistyRose
        Me.txtOffer.Location = New System.Drawing.Point(104, 48)
        Me.txtOffer.Name = "txtOffer"
        Me.txtOffer.ReadOnly = True
        Me.txtOffer.Size = New System.Drawing.Size(60, 20)
        Me.txtOffer.TabIndex = 33
        Me.txtOffer.TabStop = False
        Me.txtOffer.Text = ""
        Me.txtOffer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBidVol
        '
        Me.txtBidVol.BackColor = System.Drawing.Color.LightCyan
        Me.txtBidVol.Location = New System.Drawing.Point(72, 48)
        Me.txtBidVol.Name = "txtBidVol"
        Me.txtBidVol.ReadOnly = True
        Me.txtBidVol.Size = New System.Drawing.Size(28, 20)
        Me.txtBidVol.TabIndex = 35
        Me.txtBidVol.TabStop = False
        Me.txtBidVol.Text = ""
        Me.txtBidVol.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblTotalVol
        '
        Me.lblTotalVol.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalVol.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblTotalVol.Location = New System.Drawing.Point(296, 24)
        Me.lblTotalVol.Name = "lblTotalVol"
        Me.lblTotalVol.Size = New System.Drawing.Size(60, 20)
        Me.lblTotalVol.TabIndex = 48
        Me.lblTotalVol.Text = "Total Vol:"
        Me.lblTotalVol.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLastVol
        '
        Me.lblLastVol.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastVol.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblLastVol.Location = New System.Drawing.Point(264, 24)
        Me.lblLastVol.Name = "lblLastVol"
        Me.lblLastVol.Size = New System.Drawing.Size(28, 20)
        Me.lblLastVol.TabIndex = 47
        Me.lblLastVol.Text = "Vol:"
        Me.lblLastVol.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblOfferVol
        '
        Me.lblOfferVol.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOfferVol.ForeColor = System.Drawing.Color.Crimson
        Me.lblOfferVol.Location = New System.Drawing.Point(168, 24)
        Me.lblOfferVol.Name = "lblOfferVol"
        Me.lblOfferVol.Size = New System.Drawing.Size(28, 20)
        Me.lblOfferVol.TabIndex = 46
        Me.lblOfferVol.Text = "Vol:"
        Me.lblOfferVol.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBidVol
        '
        Me.lblBidVol.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBidVol.ForeColor = System.Drawing.Color.RoyalBlue
        Me.lblBidVol.Location = New System.Drawing.Point(72, 24)
        Me.lblBidVol.Name = "lblBidVol"
        Me.lblBidVol.Size = New System.Drawing.Size(28, 20)
        Me.lblBidVol.TabIndex = 45
        Me.lblBidVol.Text = "Vol:"
        Me.lblBidVol.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmMarket
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(360, 76)
        Me.Controls.Add(Me.lblLastPrice)
        Me.Controls.Add(Me.lblOfferPrice)
        Me.Controls.Add(Me.lblBidPrice)
        Me.Controls.Add(Me.txtOfferVol)
        Me.Controls.Add(Me.txtLastVolTotal)
        Me.Controls.Add(Me.txtLast)
        Me.Controls.Add(Me.lblLast)
        Me.Controls.Add(Me.txtLastVol)
        Me.Controls.Add(Me.txtBid)
        Me.Controls.Add(Me.lblBid)
        Me.Controls.Add(Me.lblOffer)
        Me.Controls.Add(Me.txtOffer)
        Me.Controls.Add(Me.txtBidVol)
        Me.Controls.Add(Me.lblTotalVol)
        Me.Controls.Add(Me.lblLastVol)
        Me.Controls.Add(Me.lblOfferVol)
        Me.Controls.Add(Me.lblBidVol)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmMarket"
        Me.Text = "Market"
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region " Member variables "

    '' Host reference.
    Private moHost As API.Host

    ' Market list object.
    Private WithEvents moMarkets As MarketList

    ' Market object.
    Private WithEvents moMarket As Market

    ' MarketID pulled from saved settings.
    Private mstrMarketID As String

#End Region

#Region " Delegates "

    Private Delegate Sub OnMarketDepthUpdateDelegate(ByVal poMarket As T4.API.Market)

#End Region

#Region " Market Updates "

    Private Sub moMarket_MarketDepthUpdate(ByVal poMarket As T4.API.Market) Handles moMarket.MarketDepthUpdate

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

        If Not poMarket Is Nothing Then

            Try

                ' Lock the market while we retrive details.
                poMarket.EnterLock()

                ' Display the best bid/offer.
                If Not poMarket.LastDepth Is Nothing Then

                    ' Best bid.
                    If poMarket.LastDepth.Bids.Count > 0 Then
                        txtBid.Text = poMarket.ConvertTicksDisplay(poMarket.LastDepth.Bids(0).Ticks)
                        txtBidVol.Text = poMarket.LastDepth.Bids(0).Volume.ToString
                    End If

                    ' Best offer.
                    If poMarket.LastDepth.Offers.Count > 0 Then
                        txtOffer.Text = poMarket.ConvertTicksDisplay(poMarket.LastDepth.Offers(0).Ticks)
                        txtOfferVol.Text = poMarket.LastDepth.Offers(0).Volume.ToString
                    End If

                    ' Last trade.
                    txtLast.Text = poMarket.ConvertTicksDisplay(poMarket.LastDepth.LastTradeTicks)
                    txtLastVol.Text = poMarket.LastDepth.LastTradeVolume.ToString
                    txtLastVolTotal.Text = poMarket.LastDepth.LastTradeTotalVolume.ToString

                End If

            Catch ex As Exception
                ' Trace the error.
                Trace.WriteLine("Error " & ex.ToString)
            Finally

                ' Unlock the market object.
                poMarket.ExitLock()

            End Try

        End If

    End Sub

#End Region

#Region " Save Settings "

    Public Sub SaveSettings(ByVal poXMLDoc As XmlDocument)

        Try

            If Not poXMLDoc Is Nothing AndAlso Not moMarket Is Nothing Then

                ' XML Nodes.
                Dim oMarket As XmlNode
                Dim oMarkets As XmlNode
                Dim oAttribute As XmlAttribute

                ' Attempt to reference the markets node.
                oMarkets = poXMLDoc.SelectSingleNode("markets")

                If oMarkets Is Nothing Then

                    ' Create the main node.
                    oMarkets = poXMLDoc.CreateNode(XmlNodeType.Element, "markets", "")
                    ' Add the node to the xmldoc.
                    poXMLDoc.AppendChild(oMarkets)

                End If

                ' Create a node.
                oMarket = poXMLDoc.CreateNode(XmlNodeType.Element, "market", "")

                ' Exchange ID.
                oAttribute = poXMLDoc.CreateAttribute("ExchangeID")
                oAttribute.Value = moMarket.ExchangeID
                oMarket.Attributes.Append(oAttribute)

                ' Contract ID.
                oAttribute = poXMLDoc.CreateAttribute("ContractID")
                oAttribute.Value = moMarket.ContractID
                oMarket.Attributes.Append(oAttribute)

                ' Market ID.
                oAttribute = poXMLDoc.CreateAttribute("MarketID")
                oAttribute.Value = moMarket.MarketID
                oMarket.Attributes.Append(oAttribute)

                ' Add the node to the xml document.
                oMarkets.AppendChild(oMarket)

            End If

        Catch ex As Exception

            ' Trace.
            Trace.WriteLine(ex.ToString)

        End Try

    End Sub

#End Region

End Class
