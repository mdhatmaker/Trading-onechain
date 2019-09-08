' Import the T4 definitions namespace.
Imports T4

' Import the API namespace.
Imports T4.API

' Import XML for saving and retriving markets.
Imports System.Xml

' API specific initialization occurs in the moHost_LoginSuccess routin.
' No data can be pulled from the API without a successfull login.
Public Class frmMain
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

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
    Friend WithEvents grpMarketPicker As System.Windows.Forms.GroupBox
    Friend WithEvents cmdOpen As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmMain))
        Me.cboMarkets = New System.Windows.Forms.ComboBox
        Me.cboExchanges = New System.Windows.Forms.ComboBox
        Me.cboContracts = New System.Windows.Forms.ComboBox
        Me.lblMarket = New System.Windows.Forms.Label
        Me.lblContract = New System.Windows.Forms.Label
        Me.lblExchange = New System.Windows.Forms.Label
        Me.cmdOpen = New System.Windows.Forms.Button
        Me.grpMarketPicker = New System.Windows.Forms.GroupBox
        Me.cmdSave = New System.Windows.Forms.Button
        Me.grpMarketPicker.SuspendLayout()
        Me.SuspendLayout()
        '
        'cboMarkets
        '
        Me.cboMarkets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMarkets.Location = New System.Drawing.Point(86, 70)
        Me.cboMarkets.Name = "cboMarkets"
        Me.cboMarkets.Size = New System.Drawing.Size(196, 21)
        Me.cboMarkets.Sorted = True
        Me.cboMarkets.TabIndex = 6
        Me.cboMarkets.TabStop = False
        '
        'cboExchanges
        '
        Me.cboExchanges.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboExchanges.Location = New System.Drawing.Point(86, 22)
        Me.cboExchanges.Name = "cboExchanges"
        Me.cboExchanges.Size = New System.Drawing.Size(196, 21)
        Me.cboExchanges.Sorted = True
        Me.cboExchanges.TabIndex = 7
        Me.cboExchanges.TabStop = False
        '
        'cboContracts
        '
        Me.cboContracts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboContracts.Location = New System.Drawing.Point(86, 46)
        Me.cboContracts.Name = "cboContracts"
        Me.cboContracts.Size = New System.Drawing.Size(196, 21)
        Me.cboContracts.Sorted = True
        Me.cboContracts.TabIndex = 8
        Me.cboContracts.TabStop = False
        '
        'lblMarket
        '
        Me.lblMarket.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMarket.Location = New System.Drawing.Point(22, 69)
        Me.lblMarket.Name = "lblMarket"
        Me.lblMarket.Size = New System.Drawing.Size(62, 21)
        Me.lblMarket.TabIndex = 11
        Me.lblMarket.Text = "Market:"
        Me.lblMarket.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblContract
        '
        Me.lblContract.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContract.Location = New System.Drawing.Point(22, 45)
        Me.lblContract.Name = "lblContract"
        Me.lblContract.Size = New System.Drawing.Size(62, 21)
        Me.lblContract.TabIndex = 10
        Me.lblContract.Text = "Contract:"
        Me.lblContract.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblExchange
        '
        Me.lblExchange.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExchange.Location = New System.Drawing.Point(22, 21)
        Me.lblExchange.Name = "lblExchange"
        Me.lblExchange.Size = New System.Drawing.Size(62, 21)
        Me.lblExchange.TabIndex = 9
        Me.lblExchange.Text = "Exchange:"
        Me.lblExchange.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdOpen
        '
        Me.cmdOpen.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOpen.Location = New System.Drawing.Point(194, 100)
        Me.cmdOpen.Name = "cmdOpen"
        Me.cmdOpen.Size = New System.Drawing.Size(44, 20)
        Me.cmdOpen.TabIndex = 10
        Me.cmdOpen.TabStop = False
        Me.cmdOpen.Text = "Open"
        '
        'grpMarketPicker
        '
        Me.grpMarketPicker.Controls.Add(Me.cmdSave)
        Me.grpMarketPicker.Controls.Add(Me.lblMarket)
        Me.grpMarketPicker.Controls.Add(Me.cboMarkets)
        Me.grpMarketPicker.Controls.Add(Me.lblContract)
        Me.grpMarketPicker.Controls.Add(Me.lblExchange)
        Me.grpMarketPicker.Controls.Add(Me.cboContracts)
        Me.grpMarketPicker.Controls.Add(Me.cboExchanges)
        Me.grpMarketPicker.Controls.Add(Me.cmdOpen)
        Me.grpMarketPicker.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpMarketPicker.Location = New System.Drawing.Point(8, 8)
        Me.grpMarketPicker.Name = "grpMarketPicker"
        Me.grpMarketPicker.Size = New System.Drawing.Size(306, 124)
        Me.grpMarketPicker.TabIndex = 62
        Me.grpMarketPicker.TabStop = False
        Me.grpMarketPicker.Text = "Market Picker"
        '
        'cmdSave
        '
        Me.cmdSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.Location = New System.Drawing.Point(242, 100)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(44, 20)
        Me.cmdSave.TabIndex = 12
        Me.cmdSave.TabStop = False
        Me.cmdSave.Text = "Save"
        '
        'frmMain
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(322, 140)
        Me.Controls.Add(Me.grpMarketPicker)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMain"
        Me.Text = "T4 Example 3"
        Me.grpMarketPicker.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region " Member Variables "

    ' Reference to the main api host object.
    Private WithEvents moHost As Host

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
    Private WithEvents moMarket1 As Market
    Private WithEvents moMarket2 As Market

    ' References to marketid's retrieved from saved settings..
    Dim mstrMarketID1 As String
    Dim mstrMarketID2 As String

    ' Forms collection.
    Private moMarketForms As New ArrayList

#End Region

#Region " Login Result"

    ' Run the application logic post successful login.
    Private Sub PostLogin()

        Trace.WriteLine("Login Success")

        ' Populate the available exchanges.
        moExchanges = moHost.MarketData.Exchanges

        ' Check to see if the data is already loaded.
        If moExchanges.Complete Then

            ' Call the event handler ourselves as the data is 
            ' already loaded.
            moExchanges_ExchangeListComplete(moExchanges)

        End If

        ' Invoke the loading of the settings as it involves the GUI thread to create new windows.
        ' This places process on GUI thread.
        If Me.IsHandleCreated Then
            Me.BeginInvoke(New MethodInvoker(AddressOf LoadSettings))
        Else
            LoadSettings()
        End If

    End Sub

    ' Event raised if login failed.
    Private Sub moHost_LoginFailure(ByVal penReason As LoginResult) _
        Handles moHost.LoginFailure

        Trace.WriteLine("Login Failed due to " & penReason.ToString)

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

        ' Populate the list of exchanges.

        If Not moExchanges Is Nothing Then

            Try

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

    Private Sub cboExchanges_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboExchanges.SelectedIndexChanged

        ' Populate the current exchange's available contracts.
        If Not cboExchanges.SelectedItem Is Nothing Then

            ' Reference the current exchange.
            moExchange = CType(cboExchanges.SelectedItem, ExchangeItem).Exchange

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

        If Not moContracts Is Nothing Then

            Try

                ' First clear all the combo's.
                cboContracts.Items.Clear()
                cboMarkets.Items.Clear()

                ' Eliminate any previous references.
                moContract = Nothing
                moPickerMarkets = Nothing
                moPickerMarket = Nothing

                ' Lock the API while traversing the api collection.
                ' Lock at the lowest level object for the shortest period of time.
                moContracts.EnterLock()

                ' Add the exchanges to the dropdown list.
                For Each oContract As Contract In moContracts
                    cboContracts.Items.Add(New ContractItem(oContract))
                Next

            Catch ex As Exception

                ' Trace the error.
                Trace.WriteLine("Error " & ex.ToString)

            Finally

                ' This is guarenteed to execute last.
                moContracts.ExitLock()

            End Try

        End If

    End Sub

    Private Sub cboContracts_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboContracts.SelectedIndexChanged

        ' Populate the current contract's available markets.
        If Not cboContracts.SelectedItem Is Nothing Then

            ' Reference the current contract.
            moContract = CType(cboContracts.SelectedItem, ContractItem).Contract

            ' Reference the contract's available markets.
            moPickerMarkets = moContract.Markets

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

        If Not moPickerMarkets Is Nothing Then

            Try

                ' First clear the combo.
                cboMarkets.Items.Clear()

                ' Eliminate any previous references.
                moPickerMarket = Nothing

                ' Lock the API while traversing the api collection.
                ' Lock at the lowest level object for the shortest period of time.
                moContract.EnterLock()

                ' Add the exchanges to the dropdown list.
                For Each oMarket As Market In moPickerMarkets
                    cboMarkets.Items.Add(New MarketItem(oMarket))
                Next

            Catch ex As Exception

                ' Trace the error.
                Trace.WriteLine("Error " & ex.ToString)

            Finally

                ' This is guarenteed to execute last.
                moContract.ExitLock()

            End Try

        End If

    End Sub

    Private Sub cboMarkets_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMarkets.SelectedIndexChanged

        ' Store a reference to the current market.
        If Not cboMarkets.SelectedItem Is Nothing Then
            moPickerMarket = CType(cboMarkets.SelectedItem, MarketItem).Market
        End If

    End Sub

#End Region

#Region " Startup and shutdown code "

    ' Initialise the api when the application starts.
    Private Sub frmMain_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        ' For sim:
        moHost = Host.Login(APIServerType.Simulator, "T4Example", "112A04B0-5AAF-42F4-994E-FA7CB959C60B")
        ' For live:
        'moHost = Host.Login(APIServerType.Live, "T4Example", "112A04B0-5AAF-42F4-994E-FA7CB959C60B")

        If Not moHost Is Nothing Then

            ' We will have missed this event so call it explicitly.
            PostLogin()

        Else
            Me.Close()
        End If


    End Sub

    ' Shutdown the api when the application exits.
    Private Sub frmMain_Closed(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles MyBase.Closed

        ' Check to see that we have an api object.
        If Not moHost Is Nothing Then

            ' Dispose of the api.
            moHost.Dispose()
            moHost = Nothing

        End If

    End Sub

#End Region

#Region " Market Windows "

    Private Sub cmdOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOpen.Click

        ' Open a market window.
        OpenMarket(moPickerMarket)

    End Sub

    Private Sub OnMarketFormClosed(ByVal sender As Object, ByVal e As System.EventArgs)

        ' Reference the form.
        Dim oFrm As frmMarket = CType(sender, frmMarket)

        ' Remove the event handler.
        RemoveHandler oFrm.Closed, AddressOf OnMarketFormClosed

        ' Remove the market from the collection.
        If moMarketForms.Contains(oFrm) Then
            moMarketForms.Remove(oFrm)
        End If

    End Sub

    ' Open a market window.
    Private Sub OpenMarket(ByVal poMarket As Market)

        If Not poMarket Is Nothing Then

            ' Create the market form.
            Dim oFrm As New frmMarket(moHost, moPickerMarket)

            ' Add the closed event handler so that we know how to remove the
            ' form from the forms collection.
            AddHandler oFrm.Closed, AddressOf OnMarketFormClosed

            ' Add the form to the forms collection.
            moMarketForms.Add(oFrm)

            ' Display the form.
            oFrm.Show()

        End If

    End Sub

    ' Open the market form.
    Private Sub OpenMarket(ByVal pstrExchangeID As String, ByVal pstrContractID As String, ByVal pstrMarketID As String)

        If Not pstrExchangeID = "" AndAlso Not pstrContractID = "" AndAlso Not pstrMarketID = "" Then

            ' Create the market form.
            Dim oFrm As New frmMarket(moHost, pstrExchangeID, pstrContractID, pstrMarketID)

            ' Set the owner of the form.
            oFrm.Owner = Me

            ' Add the closed event handler so that we know how to remove the
            ' form from the forms collection.
            AddHandler oFrm.Closed, AddressOf OnMarketFormClosed

            ' Add the form to the forms collection.
            moMarketForms.Add(oFrm)

            ' Display the form.
            oFrm.Show()

        End If

    End Sub


#End Region

#Region " User Settings "

    Private Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        ' Save the user settings.
        SaveSettings()

    End Sub

    ' Save settings.
    Private Sub SaveSettings()

        ' Create a new xmldoc.
        Dim poXMLDoc As New XmlDocument

        ' Itterate through all the forms and save their settings.
        For Each oFrm As frmMarket In moMarketForms

            ' Save the form's settings.
            oFrm.SaveSettings(poXMLDoc)

        Next

        ' Set the xmldoc.
        moHost.UserSettings = poXMLDoc

        ' Save the xml to the server.
        moHost.SaveUserSettings()

    End Sub

    ' Load the settings.
    Private Sub LoadSettings()

        If Not moHost Is Nothing Then

            Dim oMarkets As XmlNode = moHost.UserSettings.SelectSingleNode("markets")

            If Not oMarkets Is Nothing AndAlso oMarkets.ChildNodes.Count > 0 Then

                Dim strMarketID, strContractID, strExchangeID As String

                For Each oMarket As XmlNode In oMarkets

                    ' Reference market, contract, and exchangeid's.
                    strMarketID = oMarket.Attributes("MarketID").Value
                    strExchangeID = oMarket.Attributes("ExchangeID").Value
                    strContractID = oMarket.Attributes("ContractID").Value

                    ' Open the market.
                    Me.OpenMarket(strExchangeID, strContractID, strMarketID)

                Next
            End If
        End If

    End Sub

#End Region


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
            Return Market.Description
        End Function

    End Class

End Class
