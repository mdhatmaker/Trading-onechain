using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using T4;
using T4.API;
using T4.API.ChartData;
using System.Xml;

namespace MonkeyLightning
{
    public partial class MonkeyLightningForm : Form
    {

        public MonkeyLightningForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //

            tradeBuilderForm.Show();
            dashboardForm.Show();

            return;

            // Finally register Form events.
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Closed += new System.EventHandler(this.frmMain_Closed);
        }

        #region Delegates

        //internal delegate void Test();
        private delegate void OnAccountDetailsDelegate(T4.API.AccountList.UpdateList poAccounts);
        private delegate void OnAccountUpdateDelegate(T4.API.AccountList.UpdateList poAccounts);
        private delegate void OnPositionUpdateDelegate(T4.API.Position poPosition);
        private delegate void OnMarketDepthUpdateDelegate(Market poMarket);
        private delegate void OnAccountOrderUpdateDelegate(T4.API.Account poAccount, T4.API.Position poPosition, T4.API.OrderList.UpdateList poOrders);
        private delegate void OnAccountOrderAddedDelegate(T4.API.Account poAccount, T4.API.Position poPosition, T4.API.OrderList.UpdateList poOrders);

        #endregion

        #region Member Variables

        // Reference to the main api host object.
        internal Host moHost;

        //  Reference to the current account.
        internal Account moAccount;

        // Reference to the exchange list.
        internal ExchangeList moExchanges;

        // Reference to the current exchange.
        internal Exchange moExchange;

        // Reference to an exchange's contract list.
        internal ContractList moContracts;

        // Reference to the current contract.
        internal Contract moContract;

        // Reference to a contract's market list.
        internal MarketList moPickerMarkets;

        // Reference to the current market.
        internal Market moPickerMarket;

        // Market1 filter.
        internal MarketList moMarkets1Filter;
        internal MarketList moMarkets2Filter;

        // References to selected markets.
        internal Market moMarket1;
        internal Market moMarket2;

        // References to marketid's retrieved from saved settings.
        internal string mstrMarketID1;
        internal string mstrMarketID2;

        // Reference to the accounts list.
        internal AccountList moAccounts;

        // Reference to Order arraylist.
        // Stores the collection of orders.
        internal ArrayList moOrderArrayList = new ArrayList();

        private ChartDataForm chartDataForm = new ChartDataForm();
        private TradeBuilderForm tradeBuilderForm = new TradeBuilderForm();
        private DashboardForm dashboardForm = new DashboardForm();

        #endregion

        #region " Initialization "

        // Initialize the application.
        private void Init()
        {

            Trace.WriteLine("Init");

            // Populate the available exchanges.
            moExchanges = moHost.MarketData.Exchanges;

            // Register the exchangelist events.
            moExchanges.ExchangeListComplete += new T4.API.ExchangeList.ExchangeListCompleteEventHandler(moExchanges_ExchangeListComplete);

            // Check to see if the data is already loaded.
            if (moExchanges.Complete)
            {
                // Call the event handler ourselves as the data is 
                // already loaded.
                moExchanges_ExchangeListComplete(moExchanges);

            }

            // Subscribe to accounts.
            SubscribeToAccount();

            // Register the accountlist events.
            moAccounts.AccountDetails += new T4.API.AccountList.AccountDetailsEventHandler(moAccounts_AccountDetails);
            moAccounts.PositionUpdate += new T4.API.AccountList.PositionUpdateEventHandler(moAccounts_PositionUpdate);
            moAccounts.AccountUpdate += new T4.API.AccountList.AccountUpdateEventHandler(moAccounts_AccountUpdate);
            moAccounts.PositionUpdate += new T4.API.AccountList.PositionUpdateEventHandler(moAccounts_PositionUpdate);

            try
            {

                // Read saved markets.
                // XML Doc.
                XmlDocument oDoc;

                // XML Nodes for viewing the doc.
                XmlNode oMarkets;

                // Temporary string variables for referencing contract and exchange details.
                string strContractID;
                string strExchangeID;


                // Pull the xml doc from the server.
                oDoc = moHost.UserSettings;

                // Reference the saved markets via xml node.
                oMarkets = oDoc.ChildNodes[0];

                // Load the saved markets.
                foreach (XmlNode oMarket in oMarkets)
                {

                    // Check each child node for existance of saved markets.
                    switch (oMarket.Name)
                    {
                        case "market1":
                            {

                                mstrMarketID1 = oMarket.Attributes["MarketID"].Value;
                                strExchangeID = oMarket.Attributes["ExchangeID"].Value;
                                strContractID = oMarket.Attributes["ContractID"].Value;

                                // Create a market filter for the desired exchange and contract.
                                moMarkets1Filter = moHost.MarketData.CreateMarketFilter(strExchangeID, strContractID, 0, T4.ContractType.Any, T4.StrategyType.Any);

                                // Register the events.
                                moMarkets1Filter.MarketListComplete += new T4.API.MarketList.MarketListCompleteEventHandler(moMarkets1Filter_MarketListComplete);

                                if (moMarkets1Filter.Complete)
                                {
                                    // Call the event handler directly as the list is already complete.
                                    moMarkets1Filter_MarketListComplete(moMarkets1Filter);

                                }
                                break;
                            }
                        case "market2":
                            {

                                mstrMarketID2 = oMarket.Attributes["MarketID"].Value;
                                strExchangeID = oMarket.Attributes["ExchangeID"].Value;
                                strContractID = oMarket.Attributes["ContractID"].Value;

                                //Create a market filter for the desired exchange and contract.
                                moMarkets2Filter = moHost.MarketData.CreateMarketFilter(strExchangeID, strContractID, 0, T4.ContractType.Any, T4.StrategyType.Any);

                                // Register the events.
                                moMarkets2Filter.MarketListComplete += new T4.API.MarketList.MarketListCompleteEventHandler(moMarkets2Filter_MarketListComplete);

                                if (moMarkets2Filter.Complete)
                                {
                                    // Call the event handler directly as the list is already complete.
                                    moMarkets2Filter_MarketListComplete(moMarkets2Filter);
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                // Trace the exception.
                Trace.WriteLine("Error: " + ex.ToString());
            }
        }

        #endregion 

        #region Market Filters

        private void moMarkets1Filter_MarketListComplete(T4.API.MarketList poMarketList)
        {
            // Invoke the update.
            // This places process on GUI thread.
            if (this.IsHandleCreated)
                this.BeginInvoke(new MethodInvoker(Markets1ListComplete));
            else
                Markets1ListComplete();
        }

        private void Markets1ListComplete()
        {
            // Reference the desired market.
            Market oMarket1 = moMarkets1Filter[mstrMarketID1];

            // Subscribe to market1.
            NewMarketSubscription(ref moMarket1, ref  oMarket1);

        }

        private void moMarkets2Filter_MarketListComplete(T4.API.MarketList poMarketList)
        {
            // Invoke the update.
            // This places process on GUI thread.
            if (this.IsHandleCreated)
                this.BeginInvoke(new MethodInvoker(Markets2ListComplete));
            else
                Markets2ListComplete();
        }

        private void Markets2ListComplete()
        {
            // Reference the desired market.
            Market oMarket2 = moMarkets1Filter[mstrMarketID2];

            // Subscribe to market1.
            NewMarketSubscription(ref moMarket2, ref moPickerMarket);
        }


        #endregion

        #region Market Picker

        private void moExchanges_ExchangeListComplete(T4.API.ExchangeList poExchangeList)
        {
            // Invoke the update.
            // This places process on GUI thread.
            if (this.IsHandleCreated)
            {
                this.BeginInvoke(new MethodInvoker(ExchangeListComplete));
            }
            else
            {
                ExchangeListComplete();
            }
        }

        private void ExchangeListComplete()
        {
            // First clear all the combo's.
            cboExchanges.Items.Clear();
            cboContracts.Items.Clear();
            cboMarkets.Items.Clear();

            // Eliminate any previous references.
            moExchange = null;
            moContracts = null;
            moContract = null;
            moPickerMarkets = null;
            moPickerMarket = null;

            // Populate the list of exchanges.

            if ((moExchanges != null))
            {

                try
                {
                    // Lock the API while traversing the api collection.
                    // Lock at the lowest level object for the shortest period of time.
                    moHost.EnterLock("ExchangeList");

                    // Add the exchanges to the dropdown list.
                    foreach (Exchange oExchange in moExchanges)
                    {
                        //  cboExchanges.Items.Add(New ExchangeItem(oExchange))
                        cboExchanges.Items.Add(oExchange);
                    }


                }
                catch (Exception ex)
                {
                    // Trace the error.
                    Trace.WriteLine("Error " + ex.ToString());


                }
                finally
                {
                    // This is guarenteed to execute last.
                    moHost.ExitLock("ExchangeList");

                }

            }

        }

        private void cboExchanges_SelectedIndexChanged(Object sender, System.EventArgs e)
        {
            // Populate the current exchange's available contracts.
            if (cboExchanges.SelectedItem != null)
            {

                // Reference the current exchange.
                moExchange = ((Exchange)(cboExchanges.SelectedItem));

                // Unregister previous events.
                if (moContracts != null)
                {
                    moContracts.ContractListComplete -= new T4.API.ContractList.ContractListCompleteEventHandler(moContracts_ContractListComplete);
                }

                // Reference the exchange's available contracts.
                moContracts = moExchange.Contracts;

                // Register the events.
                if (moContracts != null)
                {
                    moContracts.ContractListComplete += new T4.API.ContractList.ContractListCompleteEventHandler(moContracts_ContractListComplete);
                }

                // Check to see if the data is already loaded.
                if (moContracts.Complete)
                {
                    // Call the event handler ourselves as the data is 
                    // already loaded.
                    moContracts_ContractListComplete(moContracts);
                }
            }
        }


        private void moContracts_ContractListComplete(T4.API.ContractList poContractList)
        {
            // Invoke the update.
            // This places process on GUI thread.
            if (this.IsHandleCreated)
            {
                this.BeginInvoke(new MethodInvoker(ContractListComplete));
            }
            else
            {
                ContractListComplete();
            }
        }

        private void ContractListComplete()
        {
            // Populate the list of contracts available for the current exchange.

            // First clear all the combo's.
            cboContracts.Items.Clear();
            cboMarkets.Items.Clear();

            // Eliminate any previous references.
            moContract = null;
            moPickerMarkets = null;
            moPickerMarket = null;


            if ((moContracts != null))
            {

                try
                {
                    // Lock the API while traversing the api collection.
                    // Lock at the lowest level object for the shortest period of time.
                    moHost.EnterLock("ContractList");

                    // Add the exchanges to the dropdown list.
                    foreach (Contract oContract in moContracts)
                    {
                        cboContracts.Items.Add(oContract);
                    }


                }
                catch (Exception ex)
                {
                    // Trace the error.
                    Trace.WriteLine("Error " + ex.ToString());


                }
                finally
                {
                    // This is guarenteed to execute last.
                    moHost.ExitLock("ContractList");

                }

            }

        }

        private void cboContracts_SelectedIndexChanged(Object sender, System.EventArgs e)
        {
            // Populate the current contract's available markets.

            {

                if ((cboContracts.SelectedItem != null))
                {
                    // Reference the current contract.
                    moContract = (Contract)cboContracts.SelectedItem;

                    // This would return all markets for the contract.
                    // moPickerMarkets = moContract.Markets

                    // This will return outright futures only.
                    moPickerMarkets = moHost.MarketData.CreateMarketFilter(moContract.ExchangeID, moContract.ContractID, 0, ContractType.Future, StrategyType.None);

                    // Register the events.
                    if (moPickerMarkets != null)
                    {
                        moPickerMarkets.MarketListComplete += new T4.API.MarketList.MarketListCompleteEventHandler(moPickerMarkets_MarketListComplete);
                    }

                    // Check to see if the data is already loaded.
                    if (moPickerMarkets.Complete)
                    {
                        // Call the event handler ourselves as the data is 
                        // already loaded.
                        moPickerMarkets_MarketListComplete(moPickerMarkets);

                    }

                }
            }
        }

        private void moPickerMarkets_MarketListComplete(T4.API.MarketList poMarketList)
        {

            // Invoke the update.
            // This places process on GUI thread.
            if (this.IsHandleCreated)
            {
                this.BeginInvoke(new MethodInvoker(MarketListComplete));
            }
            else
            {
                MarketListComplete();
            }
        }

        private void MarketListComplete()
        {
            // Populate the list of markets available for the current contract.

            // First clear the combo.
            cboMarkets.Items.Clear();

            // Eliminate any previous references.
            moPickerMarket = null;


            if ((moPickerMarkets != null))
            {

                try
                {
                    // Lock the API while traversing the api collection.
                    // Lock at the lowest level object for the shortest period of time.
                    moHost.EnterLock("MarketList");

                    // Create a sorted list of the markets.
                    // Remember to turn sorting off on the combo or it will do a text sort.
                    System.Collections.Generic.SortedList<int, Market> oSortedList = new System.Collections.Generic.SortedList<int, Market>();

                    foreach (Market oMarket in moPickerMarkets)
                    {
                        oSortedList.Add(oMarket.ExpiryDate, oMarket);

                    }

                    // Add the exchanges to the dropdown list.

                    foreach (Market oMarket in oSortedList.Values)
                    {
                        cboMarkets.Items.Add(oMarket);

                    }


                }
                catch (Exception ex)
                {
                    // Trace the error.
                    Trace.WriteLine("Error " + ex.ToString());


                }
                finally
                {
                    // This is guarenteed to execute last.
                    moHost.ExitLock("MarketList");

                }

            }

        }


        private void cboMarkets_SelectedIndexChanged(Object sender, System.EventArgs e)
        {
            if (cboMarkets.SelectedItem != null)
            {
                // Store a reference to the current market.
                moPickerMarket = ((Market)(cboMarkets.SelectedItem));
            }
        }

        #endregion

        #region Account Data

        // Method called following login success to get the data for 
        // an account and subscribe to it.

        private void SubscribeToAccount()
        {

            try
            {
                // Lock the API.
                moHost.EnterLock("AccountSubscribe");

                // Set the account list reference so that we can get 
                // Account and order events.
                moAccounts = moHost.Accounts;

                // Display the account list.

                foreach (Account oAccount in moHost.Accounts)
                {
                    // Add the account to the combo.
                    cboAccounts.Items.Add(oAccount);

                    // Subscribe to the account.
                    oAccount.Subscribe();

                }

                // Select the first item by default.
                if (cboAccounts.Items.Count > 0)
                {
                    cboAccounts.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                // Trace Errors.
                Trace.WriteLine(ex.ToString());
            }
            finally
            {
                // Unlock the api.
                moHost.ExitLock("AccountSubscribe");
            }

        }


        // Event that is raised when details for an account have 
        // changed, or a new account is recieved.
        private void moAccounts_AccountDetails(T4.API.AccountList.UpdateList poAccounts)
        {

            //
            //  Invoke the update.
            //  This places process on GUI thread.
            //  Must use a delegate to pass arguments.
            if (this.IsHandleCreated)
            {
                BeginInvoke(new OnAccountDetailsDelegate(OnAccountDetails), new Object[] { poAccounts });
            }
            else
            {
                OnAccountDetails(poAccounts);
            }
        }

        private void OnAccountDetails(AccountList.UpdateList poAccounts)
        {

            // Display the account list.
            foreach (Account oAccount in poAccounts)
            {
                // Check to see if the account exists prior to adding/subscribing to it.
                if (oAccount.Subscribed != true)
                {

                    // Add the account to the list.
                    cboAccounts.Items.Add(oAccount);

                    // Subscribe to the account.
                    oAccount.Subscribe();

                }
            }
        }

        // Event that is raised when the accounts overall balance,
        // P&L or margin details have changed.
        private void moAccounts_AccountUpdate(T4.API.AccountList.UpdateList poAccounts)
        {
            // Invoke the update.
            // This places process on GUI thread.
            // Must use a delegate to pass arguments.

            if (this.IsHandleCreated)
            {
                BeginInvoke(new OnAccountUpdateDelegate(OnAccountUpdate), new Object[] { poAccounts });
            }
            else
            {
                OnAccountUpdate(poAccounts);
            }

        }

        private void OnAccountUpdate(T4.API.AccountList.UpdateList poAccounts)
        {
            // Just refresh the current account.
            DisplayAccount(moAccount);

        }

        //' Event that is raised when positions for accounts have changed.
        private void moAccounts_PositionUpdate(AccountList.PositionUpdateList poPositions)
        {

            // Display the position details.

            {

                foreach (AccountList.PositionUpdateList.PositionUpdate oUpdate in poPositions)
                {
                    // If the position is for the current account
                    // then update the value.

                    if (object.ReferenceEquals(oUpdate.Account, moAccount))
                    {
                        // Invoke the update.
                        // This places process on GUI thread.
                        // Must use a delegate to pass arguments.
                        if (this.IsHandleCreated)
                        {
                            this.BeginInvoke(new OnPositionUpdateDelegate(OnPositionUpdate), new object[] { oUpdate.Position });
                        }
                        else
                        {
                            OnPositionUpdate(oUpdate.Position);
                        }

                        break; // TODO: might not be correct. Was : Exit For

                    }

                }
            }

        }

        private void OnPositionUpdate(T4.API.Position poPosition)
        {

            if (object.ReferenceEquals(poPosition.Market, moMarket1))
            {
                // Display the position details.
                DisplayPosition(poPosition.Market, 1);

            }
            else if (object.ReferenceEquals(poPosition.Market, moMarket2))
            {

                // Display the position details.
                DisplayPosition(poPosition.Market, 2);

            }

        }


        private void DisplayAccount(Account poAccount)
        {

            if ((moAccount != null))
            {

                try
                {
                    // Lock the host while we retrive details.
                    moHost.EnterLock("DisplayAccount");

                    // Display the current account balance.
                    txtCash.Text = String.Format("{0:#,###,##0.00}", moAccount.AvailableCash);

                }
                catch (Exception ex)
                {
                    // Trace the error.
                    Trace.WriteLine("Error: " + ex.ToString());

                }
                finally
                {
                    // Unlock the host object.
                    moHost.ExitLock("DisplayAccount");

                }

            }

        }

        private void DisplayPosition(Market poMarket, int piID)
        {
            string strNet = "";
            string strBuys = "";
            string strSells = "";

            bool blnLocked = false;

            try
            {

                if ((poMarket != null) && (moAccount != null))
                {
                    // Lock the host while we retrive details.
                    moHost.EnterLock("DisplayPositions");

                    // Update the locked flag.
                    blnLocked = true;

                    // Temporary position object used for referencing the account's positions.
                    Position oPosition = default(Position);

                    // Display positions for current account and market1.

                    // Reference the market's positions.
                    oPosition = moAccount.Positions[poMarket.MarketID];

                    if ((oPosition != null))
                    {
                        // Reference the net position.
                        strNet = oPosition.Net.ToString();
                        strBuys = oPosition.Buys.ToString();
                        strSells = oPosition.Sells.ToString();
                    }

                    switch (piID)
                    {
                        case 1:

                            // Display the net position.
                            txtNet1.Text = strNet;
                            // Display the total Buys.
                            txtBuys1.Text = strBuys;
                            // Display the total Sells.
                            txtSells1.Text = strSells;

                            break;
                        case 2:

                            // Display the net position.
                            txtNet2.Text = strNet;
                            // Display the total Buys.
                            txtBuys2.Text = strBuys;
                            // Display the total Sells.
                            txtSells2.Text = strSells;

                            break;
                    }

                }

            }
            catch (Exception ex)
            {
                // Trace the error.
                Trace.WriteLine("Error " + ex.ToString());

            }
            finally
            {
                // Unlock the host object.
                if (blnLocked)
                    moHost.ExitLock("DisplayPositions");

            }

        }

        private void cboAccounts_SelectedIndexChanged(Object sender, System.EventArgs e)
        {
            if ((cboAccounts.SelectedItem != null))
            {
                // Reference the current account.
                moAccount = (Account)cboAccounts.SelectedItem;

                // Register the account's events.
                if (moAccount != null)
                {
                    moAccount.OrderAdded += new T4.API.Account.OrderAddedEventHandler(moAccount_OrderAdded);
                    moAccount.OrderUpdate += new T4.API.Account.OrderUpdateEventHandler(moAccount_OrderUpdate);
                }

                // Display the current account balance.
                DisplayAccount(moAccount);

                // Refresh positions.
                DisplayPosition(moMarket1, 1);
                DisplayPosition(moMarket2, 2);

            }

        }


        #endregion


        #region Startup and shutdown code

        // Initialise the api when the application starts.
        private void frmMain_Load(object sender, System.EventArgs e)
        {

            //moHost = Host.Login(APIServerType.Simulator, "EZTrader", "112A04B0-5AAF-42F4-994E-FA7CB959C60B");
            moHost = Host.Login(APIServerType.Simulator, "T4Example", "112A04B0-5AAF-42F4-994E-FA7CB959C60B");

            // Check for success.

            if (moHost == null)
            {
                // Host object not returned which means the user cancelled the login dialog.
                this.Close();

            }
            else
            {
                // Login was successfull.
                Trace.WriteLine("Login Success");

                // Register the host events.			
                this.moHost.LoginSuccess += new T4.API.Host.LoginSuccessEventHandler(Init);
                this.moHost.LoginFailure += new T4.API.Host.LoginFailureEventHandler(moHost_LoginFailure);

                // Initialize.
                Init();

            }

        }

        // Shutdown the api when the application exits.
        private void frmMain_Closed(object sender, System.EventArgs e)
        {

            // Check to see that we have an api object.
            if (moHost != null)
            {

                // Unregister events.

                // Markets.
                if (moMarket1 != null)
                {
                    moMarket1.MarketCheckSubscription -= new T4.API.Market.MarketCheckSubscriptionEventHandler(Markets_MarketCheckSubscription);
                    moMarket1.MarketDepthUpdate -= new T4.API.Market.MarketDepthUpdateEventHandler(Markets_MarketDepthUpdate);
                }
                if (moMarket2 != null)
                {
                    moMarket2.MarketCheckSubscription -= new T4.API.Market.MarketCheckSubscriptionEventHandler(Markets_MarketCheckSubscription);
                    moMarket2.MarketDepthUpdate -= new T4.API.Market.MarketDepthUpdateEventHandler(Markets_MarketDepthUpdate);
                }

                // Market Filters.
                if (moMarkets1Filter != null)
                {
                    moMarkets1Filter.MarketListComplete -= new T4.API.MarketList.MarketListCompleteEventHandler(moMarkets1Filter_MarketListComplete);
                }
                if (moMarkets2Filter != null)
                {
                    moMarkets2Filter.MarketListComplete -= new T4.API.MarketList.MarketListCompleteEventHandler(moMarkets2Filter_MarketListComplete);
                }

                // Account events.
                if (moAccounts != null)
                {
                    moAccounts.AccountDetails -= new T4.API.AccountList.AccountDetailsEventHandler(moAccounts_AccountDetails);
                    moAccounts.PositionUpdate -= new T4.API.AccountList.PositionUpdateEventHandler(moAccounts_PositionUpdate);
                    moAccounts.AccountUpdate -= new T4.API.AccountList.AccountUpdateEventHandler(moAccounts_AccountUpdate);
                    moAccounts.PositionUpdate -= new T4.API.AccountList.PositionUpdateEventHandler(moAccounts_PositionUpdate);
                }

                if (moAccount != null)
                {
                    moAccount.OrderAdded -= new T4.API.Account.OrderAddedEventHandler(moAccount_OrderAdded);
                    moAccount.OrderUpdate -= new T4.API.Account.OrderUpdateEventHandler(moAccount_OrderUpdate);
                }

                // Exchange list events.
                if (moExchanges != null)
                {
                    moExchanges.ExchangeListComplete -= new T4.API.ExchangeList.ExchangeListCompleteEventHandler(moExchanges_ExchangeListComplete);
                }

                // Contract list events.
                if (moContracts != null)
                {
                    moContracts.ContractListComplete -= new T4.API.ContractList.ContractListCompleteEventHandler(moContracts_ContractListComplete);
                }

                // Market list events.
                if (moPickerMarkets != null)
                {
                    moPickerMarkets.MarketListComplete -= new T4.API.MarketList.MarketListCompleteEventHandler(moPickerMarkets_MarketListComplete);
                }

                // Market events.
                if (moMarket1 != null)
                {
                    // Register to the events.
                    moMarket1.MarketDepthUpdate -= new T4.API.Market.MarketDepthUpdateEventHandler(Markets_MarketDepthUpdate);
                }

                if (moMarket2 != null)
                {
                    // Register to the events.
                    moMarket2.MarketDepthUpdate -= new T4.API.Market.MarketDepthUpdateEventHandler(Markets_MarketDepthUpdate);
                }

                // Host events.
                if (moHost != null)
                {
                    moHost.LoginSuccess -= new T4.API.Host.LoginSuccessEventHandler(Init);
                    moHost.LoginFailure -= new T4.API.Host.LoginFailureEventHandler(moHost_LoginFailure);

                    // Dispose of the api.
                    moHost.Dispose();
                    moHost = null;
                }
            }
        }

        #endregion


        /// <summary>
        /// The Load button was clicked by the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void cmdChartRequest_Click(object sender, System.EventArgs e)
        {
            chartDataForm.moSelectedExchange = moExchange;
            chartDataForm.moSelectedContract = moContract;
            chartDataForm.moMarket = moPickerMarket;

            // Configure the chart control.
            chartDataForm.ConfigureGridControl();

            chartDataForm.Show();

            // Request chart data.
            ChartInterval interval = ChartInterval.Day;
            if (radHourBars.Checked)
                interval = ChartInterval.Hour;
            chartDataForm.RequestChartData(interval);
        }


        #region " Login Result"

        /// <summary>
        /// Event raised if login failed. When using the Host.Login method to login this will only be raised in the event of a disconnection from the server.
        /// </summary>
        /// <param name="penReason"></param>
        /// <remarks></remarks>

        private void moHost_LoginFailure(LoginResult penReason)
        {

            Trace.WriteLine("Login Failed due to " + penReason.ToString());

        }

        /// <summary>
        /// Event raised if login is successful. When using the Host.Login method to login this will ONLY be raised in the event of a reconnection to the server.
        /// </summary>
        /// <remarks></remarks>

        private void moHost_LoginSuccess()
        {
            // Login was successfull.
            Trace.WriteLine("Login Success");

            // Nothing else needs to be done here when Host.Login is used to login - any market and account subscriptions active when disconnection occurred
            // will automatically be restored. 

        }

        #endregion


        #region " operations "

        #endregion 


        #region " Market Subscription "


        private void cmdGet1_Click(System.Object sender, System.EventArgs e)
        {

            // Clear the values.
            DisplayMarketDetails(null, 1);

            // Subscribe to market1.
            NewMarketSubscription(ref moMarket1, ref moPickerMarket);

            // Refresh the positions.
            DisplayPosition(moMarket1, 1);

        }


        private void cmdGet2_Click(System.Object sender, System.EventArgs e)
        {
            Market oMarket = moHost.MarketData.MarketPicker(ref moMarket2);

            // Clear the values.
            DisplayMarketDetails(null, 2);

            // Subscribe to market2.
            NewMarketSubscription(ref moMarket2, ref oMarket);

            // Refresh the positions.
            DisplayPosition(moMarket2, 2);

        }

        private void NewMarketSubscription(ref Market poMarket, ref Market poNewMarket)
        {
            // Update an existing market reference to subscribe to a new/different market.

            // If they are the same then don't do anything.
            // We don't need to resubscribe to the same market.

            // Explicitly register events as opposed to declaring withevents.
            // This gives us more control.  
            // It is important to unregister the marketchecksubscription prior to unsubscribing or the event will override and maintain the subscription.


            if ((!object.ReferenceEquals(poMarket, poNewMarket)))
            {
                // Unsubscribe from the currently selected market.
                if ((poMarket != null))
                {
                    // Unregister the events for this market.
                    poMarket.MarketCheckSubscription -= new T4.API.Market.MarketCheckSubscriptionEventHandler(Markets_MarketCheckSubscription);
                    poMarket.MarketDepthUpdate -= new T4.API.Market.MarketDepthUpdateEventHandler(Markets_MarketDepthUpdate);

                    poMarket.DepthUnsubscribe();

                }

                // Update the market reference.
                poMarket = poNewMarket;

                if ((poMarket != null))
                {

                    // Register the events.
                    poMarket.MarketCheckSubscription += new T4.API.Market.MarketCheckSubscriptionEventHandler(Markets_MarketCheckSubscription);
                    poMarket.MarketDepthUpdate += new T4.API.Market.MarketDepthUpdateEventHandler(Markets_MarketDepthUpdate);

                    // Subscribe to the market.
                    // Use smart buffering.
                    poMarket.DepthSubscribe(DepthBuffer.Smart, DepthLevels.BestOnly);

                }

            }

        }

        private void Markets_MarketCheckSubscription(T4.API.Market poMarket, ref T4.DepthBuffer penDepthBuffer, ref T4.DepthLevels penDepthLevels)
        {
            // No need to invoke on the gui thread.
            penDepthBuffer = poMarket.DepthSubscribeAtLeast(DepthBuffer.Smart, penDepthBuffer);
            penDepthLevels = poMarket.DepthSubscribeAtLeast(DepthLevels.BestOnly, penDepthLevels);

        }

        private void Markets_MarketDepthUpdate(T4.API.Market poMarket)
        {
            // Invoke the update.
            // This places process on GUI thread.
            // Must use a delegate to pass arguments.
            if (this.IsHandleCreated)
            {
                this.BeginInvoke(new OnMarketDepthUpdateDelegate(OnMarketDepthUpdate), new object[] { poMarket });
            }
            else
            {
                OnMarketDepthUpdate(poMarket);
            }

        }

        private void OnMarketDepthUpdate(Market poMarket)
        {

            try
            {

                if (object.ReferenceEquals(poMarket, moMarket1))
                {
                    DisplayMarketDetails(poMarket, 1);


                }
                else if (object.ReferenceEquals(poMarket, moMarket2))
                {
                    DisplayMarketDetails(poMarket, 2);

                }


            }
            catch (Exception ex)
            {
                // Trace the error.
                Trace.WriteLine("Error " + ex.ToString());

            }

        }

        /// <summary>
        /// Update the market display values.
        /// </summary>

        private void DisplayMarketDetails(Market poMarket, int piID)
        {
            string strDescription = "";
            string strBid = "";
            string strBidVol = "";
            string strOffer = "";
            string strOfferVol = "";
            string strLast = "";
            string strLastVol = "";
            string strLastVolTotal = "";


            if ((poMarket != null))
            {

                try
                {
                    // Lock the host while we retrive details.
                    moHost.EnterLock("DisplayMarketDetails");

                    // Display the market description.
                    strDescription = poMarket.Description;


                    if ((poMarket.LastDepth != null))
                    {
                        // Best bid.
                        if (poMarket.LastDepth.Bids.Count > 0)
                        {
                            strBid = poMarket.ConvertTicksDisplay(poMarket.LastDepth.Bids[0].Ticks);
                            strBidVol = poMarket.LastDepth.Bids[0].Volume.ToString();
                        }

                        // Best offer.
                        if (poMarket.LastDepth.Offers.Count > 0)
                        {
                            strOffer = poMarket.ConvertTicksDisplay(poMarket.LastDepth.Offers[0].Ticks);
                            strOfferVol = poMarket.LastDepth.Offers[0].Volume.ToString();
                        }

                        // Last trade.
                        strLast = poMarket.ConvertTicksDisplay(poMarket.LastDepth.LastTradeTicks);
                        strLastVol = poMarket.LastDepth.LastTradeVolume.ToString();
                        strLastVolTotal = poMarket.LastDepth.LastTradeTotalVolume.ToString();

                    }

                }
                catch (Exception ex)
                {
                    // Trace the error.
                    Trace.WriteLine("Error " + ex.ToString());

                }
                finally
                {
                    // Unlock the host object.
                    moHost.ExitLock("DisplayMarketDetails");

                }

            }

            switch (piID)
            {
                case 1:

                    // Update the market1 display values.
                    txtMarketDescription1.Text = strDescription;
                    txtBid1.Text = strBid;
                    txtBidVol1.Text = strBidVol;
                    txtOffer1.Text = strOffer;
                    txtOfferVol1.Text = strOfferVol;
                    txtLast1.Text = strLast;
                    txtLastVol1.Text = strLastVol;
                    txtLastVolTotal1.Text = strLastVolTotal;

                    break;
                case 2:

                    // Update the market2 display values.
                    txtMarketDescription2.Text = strDescription;
                    txtBid2.Text = strBid;
                    txtBidVol2.Text = strBidVol;
                    txtOffer2.Text = strOffer;
                    txtOfferVol2.Text = strOfferVol;
                    txtLast2.Text = strLast;
                    txtLastVol2.Text = strLastVol;
                    txtLastVolTotal2.Text = strLastVolTotal;

                    break;
            }

        }

        #endregion


        #region Save Settings

        private void cmdSave_Click(System.Object sender, System.EventArgs e)
        {
            try
            {

                // XML Doc.
                XmlDocument oDoc = new XmlDocument();

                // XML Node.
                XmlNode oMarket;
                XmlNode oMarkets;
                XmlAttribute oAttribute;

                // Create the main node.
                oMarkets = oDoc.CreateNode(XmlNodeType.Element, "markets", "");
                oDoc.AppendChild(oMarkets);

                if (moMarket1 != null)
                {

                    // Create a node.
                    oMarket = oDoc.CreateNode(XmlNodeType.Element, "market1", "");

                    // Exchange ID.
                    oAttribute = oDoc.CreateAttribute("ExchangeID");
                    oAttribute.Value = moMarket1.ExchangeID;
                    oMarket.Attributes.Append(oAttribute);

                    // Contract ID.
                    oAttribute = oDoc.CreateAttribute("ContractID");
                    oAttribute.Value = moMarket1.ContractID;
                    oMarket.Attributes.Append(oAttribute);

                    // Market ID.
                    oAttribute = oDoc.CreateAttribute("MarketID");
                    oAttribute.Value = moMarket1.MarketID;
                    oMarket.Attributes.Append(oAttribute);

                    // Add the node to the xml document.
                    oMarkets.AppendChild(oMarket);
                }

                if (moMarket2 != null)
                {

                    // Create a node.
                    oMarket = oDoc.CreateNode(XmlNodeType.Element, "market2", "");

                    // Exchange ID.
                    oAttribute = oDoc.CreateAttribute("ExchangeID");
                    oAttribute.Value = moMarket2.ExchangeID;
                    oMarket.Attributes.Append(oAttribute);

                    // Contract ID.
                    oAttribute = oDoc.CreateAttribute("ContractID");
                    oAttribute.Value = moMarket2.ContractID;
                    oMarket.Attributes.Append(oAttribute);

                    // Market ID.
                    oAttribute = oDoc.CreateAttribute("MarketID");
                    oAttribute.Value = moMarket2.MarketID;
                    oMarket.Attributes.Append(oAttribute);

                    // Add the node to the xml document.
                    oMarkets.AppendChild(oMarket);

                }

                // Save the xml to the server.
                moHost.UserSettings = oDoc;
                moHost.SaveUserSettings();
            }
            catch (Exception ex)
            {
                // Trace.
                Trace.WriteLine(ex.ToString());
            }
        }

        public string App_Path()
        {
            return System.AppDomain.CurrentDomain.BaseDirectory;
        }


        #endregion

        #region Single Order

        // Method that submits a single order.
        private void SubmitSingleOrder(Market poMarket, BuySell peBuySell, Double pdblLimitPrice)
        {
            if (moAccount != null && poMarket != null)
            {

                // Submit an order.
                Order oOrder = moAccounts.SubmitNewOrder(
                    moAccount,
                    poMarket,
                    peBuySell,
                    PriceType.Limit,
                    TimeType.Normal,
                    1,
                    pdblLimitPrice);

                // Add the order to the arraylist.
                AddOrder(oOrder);

                // Display the orders.
                DisplayOrders();

            }
        }

        // Pull the single order that was submitted.
        private void PullSingleOrder(Order poOrder)
        {
            // Check to see that we have an order.
            if (poOrder != null)
            {
                // Check to see if the order is working.
                if (poOrder.IsWorking)
                {
                    // Pull the order.
                    poOrder.Pull();
                }
            }
        }


        #endregion

        #region Submission/Cancelation

        private void cmdBuy1_Click(System.Object sender, System.EventArgs e)
        {
            // Submit a single order.
            if (txtBid1.Text != "")
            {
                SubmitSingleOrder(moMarket1, BuySell.Buy, System.Convert.ToDouble(txtBid1.Text));
            }
        }
        private void cmdSell1_Click(System.Object sender, System.EventArgs e)
        {
            // Submit a single order.
            if (txtOffer1.Text != "")
            {
                SubmitSingleOrder(moMarket1, BuySell.Sell, System.Convert.ToDouble(txtOffer1.Text));
            }
        }

        private void cmdBuy2_Click(System.Object sender, System.EventArgs e)
        {
            // Submit a single order.
            if (txtBid2.Text != "")
            {
                SubmitSingleOrder(moMarket2, BuySell.Buy, System.Convert.ToDouble(txtBid2.Text));
            }
        }

        private void cmdSell2_Click(System.Object sender, System.EventArgs e)
        {
            // Submit a single order.
            if (txtOffer2.Text != "")
            {
                SubmitSingleOrder(moMarket2, BuySell.Sell, System.Convert.ToDouble(txtOffer2.Text));
            }
        }

        private void lstOrders_DoubleClick(Object sender, System.EventArgs e)
        {
            // Pull the order that has been double clicked on.
            int iOrderIndex;

            // Be sure that the selected index is valid.
            if (lstOrders.SelectedIndex >= 0 & lstOrders.SelectedIndex <= lstOrders.Items.Count - 1)
            {

                // The orders were listed in reverse so we need 
                // to calculate the index of the order within the arraylist.
                iOrderIndex = (lstOrders.Items.Count - lstOrders.SelectedIndex - 1);

                // Reference the order in the collection.
                Order oOrder = (Order)(moOrderArrayList[iOrderIndex]);

                // Attempt to pull the order.
                PullSingleOrder(oOrder);
            }
        }

        #endregion

        #region  Order Data

        private void moAccount_OrderUpdate(T4.API.Account poAccount, T4.API.Position poPosition, T4.API.OrderList.UpdateList poOrders)
        {
            // Invoke the update.
            // This places process on GUI thread.
            // Must use a delegate to pass arguments.
            if (this.IsHandleCreated)
            {

                this.BeginInvoke(new OnAccountOrderUpdateDelegate(OnAccountOrderUpdate), new Object[] { poAccount, poPosition, poOrders });
            }
            else
            {
                OnAccountOrderUpdate(poAccount, poPosition, poOrders);
            }
        }

        private void moAccount_OrderAdded(T4.API.Account poAccount, T4.API.Position poPosition, T4.API.OrderList.UpdateList poOrders)
        {
            // Invoke the update.
            // This places process on GUI thread.
            // Must use a delegate to pass arguments.
            if (this.IsHandleCreated)
                this.BeginInvoke(new OnAccountOrderAddedDelegate(OnAccountOrderAdded), new Object[] { poAccount, poPosition, poOrders });
            else
                OnAccountOrderAdded(poAccount, poPosition, poOrders);
        }

        private void OnAccountOrderUpdate(T4.API.Account poAccount, T4.API.Position poPosition, T4.API.OrderList.UpdateList poOrders)
        {
            // Redraw the order list.
            DisplayOrders();
        }

        private void OnAccountOrderAdded(T4.API.Account poAccount, T4.API.Position poPosition, T4.API.OrderList.UpdateList poOrders)
        {
            // Add all the orders to the arraylist.
            foreach (Order oOrder in poOrders)
            {
                // Add the order.
                AddOrder(oOrder);
            }

            // Redraw the order list.
            DisplayOrders();
        }

        private void AddOrder(Order poOrder)
        {
            // Add the order to the arraylist.
            if (poOrder != null)
            {
                if (moOrderArrayList.Contains(poOrder) == false)
                {
                    // Add the order to the arraylist.
                    moOrderArrayList.Add(poOrder);
                }
            }
        }


        private void DisplayOrders()
        {
            try
            {

                // Lock the api.
                moHost.EnterLock();

                // Suspend the layout of the listbox.
                lstOrders.SuspendLayout();

                // Clear and repopulate the list.
                lstOrders.Items.Clear();

                // Temporary order object.
                Order oOrder;

                // Itterate through the collection backwards.
                for (int i = moOrderArrayList.Count - 1; i >= 0; i--)
                {

                    // Reference an order.
                    oOrder = (Order)(moOrderArrayList[i]);

                    // Display some order details.
                    lstOrders.Items.Add(oOrder.Market.Description + "   " +
                        oOrder.BuySell.ToString() + "   " +
                        oOrder.TotalFillVolume + "/" + oOrder.CurrentVolume + " @ " +
                        oOrder.Market.ConvertTicksDisplay(oOrder.CurrentLimitTicks, false) + "   " +
                        oOrder.Status.ToString() + "   " +
                        oOrder.StatusDetail + "  " +
                        oOrder.SubmitTime);

                }
            }
            catch (Exception ex)
            {
                // Trace the error.
                Trace.WriteLine("Error: " + ex.ToString());
            }
            finally
            {
                // Unlock the api.
                moHost.ExitLock();

                // Resume layout of the listbox.
                lstOrders.ResumeLayout();
            }
        }

        #endregion

        #region Misc Examples

        const string AUTOOCO = "Submit Auto OCO";
        const string FIVETICKSOFF = "Work 5 Ticks Off Market";

        // Setup misc example combos.
        private void SetupMiscExamples()
        {
            // Add examples to combos.
            cboMisc1.Items.Add(AUTOOCO);
            cboMisc1.Items.Add(FIVETICKSOFF);

            cboMisc2.Items.Add(AUTOOCO);
            cboMisc2.Items.Add(FIVETICKSOFF);

            // Be sure the first items are selected.
            cboMisc1.SelectedIndex = 0;
            cboMisc2.SelectedIndex = 0;

        }

        private void cmdRunMisc1_Click(Object sender, System.EventArgs e)
        {
            if (moMarket1 != null)
            {
                switch (cboMisc1.Text)
                {
                    case AUTOOCO:
                        {
                            // Run autooco sample code.
                            SubmitAOCO(moMarket1, BuySell.Buy, txtBid1.Text);
                            break;
                        }
                    case FIVETICKSOFF:
                        {
                            // Run the five ticks off code.
                            SubmitFiveTicksOff(moMarket1, BuySell.Buy, txtBid1.Text);
                            break;
                        }
                }
            }
        }

        private void cmdRunMisc2_Click(Object sender, System.EventArgs e)
        {
            if (moMarket2 != null)
            {

                switch (cboMisc2.Text)
                {
                    case AUTOOCO:
                        {
                            // Run autooco sample code.
                            SubmitAOCO(moMarket2, BuySell.Sell, txtOffer2.Text);
                            break;
                        }
                    case FIVETICKSOFF:
                        {
                            // Run the five ticks off code.
                            SubmitFiveTicksOff(moMarket2, BuySell.Sell, txtOffer2.Text);
                            break;
                        }
                }
            }
        }

        #region Auto OCO

        // Simple example of how to submit and cancel an Auto OCO.
        private void SubmitAOCO(Market poMarket, BuySell peBuySell, string pstrLimitDisplayPrice)
        {
            if (moAccount != null && poMarket != null)
            {

                // Limit price reference.
                // Convert the limit price to a double.
                Double dblLimitPrice = System.Convert.ToDouble(pstrLimitDisplayPrice);

                // Create the batch submission object.
                OrderList.Submission oBatch;
                oBatch = moAccounts.SubmitOrders(moAccount, poMarket);

                // Set the order link.
                oBatch.OrderLink = OrderLink.AutoOCO;

                // Add an order to the batch.
                // This is the trigger order.
                Order oOrder1 = oBatch.Add(peBuySell,
                    PriceType.Limit,
                    TimeType.Normal,
                    1,
                    dblLimitPrice);

                if (peBuySell == BuySell.Buy)
                {

                    // Add an order to the batch.
                    // This is the sell limit of the oco above the market.
                    // Note the flip of Buy/Sell.
                    // Note the ticks is a distance not a price representation.
                    Order oOrder2 = oBatch.Add(BuySell.Sell,
                        PriceType.Limit,
                        TimeType.Normal,
                        0,
                        poMarket.ConvertTicks(poMarket.TicksAdd(5, 0, false), false));

                    // Add an order to the batch.
                    // This is the stop of the oco below the market.
                    // Note the flip of Buy/Sell.
                    // Note the ticks is a distance not a price representation.
                    Order oOrder3 = oBatch.Add(BuySell.Sell,
                        PriceType.StopMarket,
                        TimeType.Normal,
                        0,
                        0.0,
                        poMarket.ConvertTicks(poMarket.TicksAdd(-5, 0, false), false),
                        OpenClose.Undefined, "", 0, ActivationType.Immediate, "", 0, null, null, true, null, true);
                }
                else
                {

                    // Add an order to the batch.
                    // This is the buy limit of the oco below the market.
                    // Note the flip of Buy/Sell.
                    // Note the ticks is a distance not a price representation.
                    Order oOrder2 = oBatch.Add(BuySell.Buy,
                        PriceType.Limit,
                        TimeType.Normal,
                        0,
                        poMarket.ConvertTicks(poMarket.TicksAdd(-5, 0, false), false));

                    // Add an order to the batch.
                    // This is the buy stop of the oco above the market.
                    // Note the flip of Buy/Sell.
                    // Note the ticks is a distance not a price representation.
                    Order oOrder3 = oBatch.Add(BuySell.Buy,
                        PriceType.StopMarket,
                        TimeType.Normal,
                        0, 0.0,
                        poMarket.ConvertTicks(poMarket.TicksAdd(5, 0, false), false),
                        OpenClose.Undefined, "", 0, ActivationType.Immediate, "", 0, null, null, true, null, true);

                }


                // Submit the batch.
                oBatch.Submit();

                // Display the orders.
                DisplayOrders();


                // Pull may fail if attempted too soon.
                // Like 1 millisecond later.

                //// This is how you would cancel the batch.
                //Dim oBatchPull As OrderList.Pull = moAccounts.PullOrders(moAccount, poMarket)

                //// Add the orders to the pull.
                //oBatchPull.Add(oOrder1)
                //oBatchPull.Add(oOrder2)
                //oBatchPull.Add(oOrder3)

                //// Pull the batch.
                //oBatchPull.Pull()

                //// Add the orders to the arraylist.
                //AddOrder(oOrder1)
                //AddOrder(oOrder2)
                //AddOrder(oOrder3)


            }

        }

        #endregion

        #region  Work Order Five Ticks From Market

        // Place an order five ticks off the market.
        private void SubmitFiveTicksOff(Market poMarket, BuySell peBuySell, string pstrLimitDisplayPrice)
        {
            // Limit price reference.
            // Convert the limit price to a double.
            Double dblLimitPrice = System.Convert.ToDouble(pstrLimitDisplayPrice);

            // Convert the price to ticks.
            int iTicks = poMarket.ConvertPrice(dblLimitPrice, false);
            int iNewTicks;

            // Add or subtract five ticks from the current price depending on what side of the market we are.
            if (peBuySell == BuySell.Buy)
                iNewTicks = poMarket.TicksAdd(-5, iTicks, false);
            else
                iNewTicks = poMarket.TicksAdd(5, iTicks, false);

            Double iNewPrice = poMarket.ConvertTicks(iNewTicks, false);

            // Submit a single order five ticks off the market.
            SubmitSingleOrder(poMarket, peBuySell, iNewPrice);

        }

        #endregion

        #endregion

    } // class
} // namespace
