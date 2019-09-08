#define DEEPDEBUG
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using EZAPI.Framework;
using EZAPI.Framework.Base;
using EZAPI.Framework.Chart;
using EZAPI.Framework.Spread;
using EZAPI.Toolbox.Debug;
using T4;
using T4.API;

/*
 * To use this API wrapper, your startup code should resemble this:
            string loginUsername = "USERNAME";
            string loginPassword = "PASSWORD";

            api = new CTSAPIFunctions(loginUsername, loginPassword);
            api.OnInitialize += new InitializeHandler(api_OnInitialize);
            api.OnInsideMarketUpdate += new InsideMarketHandler(api_OnInsideMarketUpdate);
            api.OnFill += new FillHandler(api_OnFill);
            api.OnOrder += new OrderHandler(api_OnOrder);
 * 
 * And your event handlers should be threadsafe similar to the following:
        void api_OnOrder(Instrument instrument, TTOrder order)
        {
            //cross thread - so you don't get the cross theading exception
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    api_OnOrder(instrument, order);
                });
                return;
            } 
        }
 */
namespace EZAPI
{
    public delegate void InitializeHandler(bool success, string message);
    public delegate void InstrumentFoundHandler(EZInstrument ttInstrument, bool success);
    public delegate void InsideMarketHandler(EZInstrument ttInstrument);
    public delegate void MarketDepthHandler(EZInstrument ttInstrument, EZMarketDepth marketDepth);
    public delegate void SimpleFillHandler(EZFill fill);
    public delegate void OrderHandler(EZOrderStatus status, EZInstrument ttInstrument, EZOrder order, EZOrder newOrder);
    public delegate void SystemMessageHandler(SystemMessage systemMessage);
    public delegate void TimeAndSalesHandler(EZInstrument ttInstrument, DateTime timeStamp, ezPrice LTP, ezQuantity LTQ, TradeDirection direction, bool isOTC);

    public delegate void EventHandlerEx(EventArgsEx ex);

    /// <summary>
    /// This class contains the base-level functionality for EZAPI
    /// </summary>
    /// <remarks>This class is responsible for communication with TTAPI</remarks>
    public class APIMain : IDisposable
    {
        #region PROPERTIES

        public static APIMain Instance
        {
            get
            {
                if (apiFunctionsInstance != null)
                    return apiFunctionsInstance;
                else
                {
                    apiFunctionsInstance = new APIMain();
                    return apiFunctionsInstance;
                }
            }
        }

        public Host Base { get { return ctsAPI; } }

        /// <summary>
        /// Setting this property to true causes EZAPI to subscribe to any instruments for fills
        /// or orders that come through the system (so that the user does not have to subscribe
        /// to these explicitly)
        /// </summary>
        public bool AutoSubscribeInstruments { get { return autoSubscribeInstruments; } }
        /// <summary>
        /// Setting this property to true indicates the user wants to receive market depth events from EZAPI
        /// </summary>
        public bool SubscribeMarketDepth { get { return subscribeMarketDepth; } }
        /// <summary>
        /// Setting this property to true indicates the user wants to receive time and sales events from EZAPI
        /// </summary>
        public bool SubscribeTimeAndSales { get { return subscribeTimeAndSales; } }

        /// <summary>
        /// Retrieve the instruments currently maintained by EZAPI
        /// </summary>
        public List<EZInstrument> Instruments { get { return zzInstruments.Values.ToList<EZInstrument>(); } }
        /// <summary>
        /// Retrieve all orders in EZAPI
        /// </summary>
        /// <remarks>This property will likely not be used much compared to WorkingOrders</remarks>
        public List<EZOrder> Orders { get { return ttOrders.Values.ToList<EZOrder>(); } }
        /// <summary>
        /// Retrieve all working orders in EZAPI
        /// </summary>
        public List<EZOrder> WorkingOrders { get { return workingTTOrders.Values.ToList<EZOrder>(); } }
        /// <summary>
        /// Retrieve all fills in EZAPI
        /// </summary>
        /// <remarks>This property will likely not be used much compared to CurrentFills</remarks>
        public List<EZFill> Fills { get { return ttFills.Values.ToList<EZFill>(); } }
        /// <summary>
        /// Retrieve all current fills in EZAPI (with amends, etc. already accounted for)
        /// </summary>
        public List<EZFill> CurrentFills { get { return currentTTFills.Values.ToList<EZFill>(); } }
        /// <summary>
        /// Retrieve the market depth for all instruments in EZAPI
        /// </summary>
        #endregion

        #region EVENTS
        public event InstrumentFoundHandler OnInstrumentFound;
        public event InsideMarketHandler OnInsideMarketUpdate;
        public event MarketDepthHandler OnMarketDepthUpdate;
        public event TimeAndSalesHandler OnTimeAndSales;
        public event OrderHandler OnOrder;
        public event SimpleFillHandler OnFill;
        public event SystemMessageHandler OnSystemMessage;

        public event InitializeHandler OnInitialize;    // OnInitialize is fired when the EZAPI is initialized (successfully or not)

        public event EventHandlerEx LoginSuccess;
        public event EventHandlerEx LoginFailure;

        private InstrumentFoundHandler _onInstrumentFound;
        private InsideMarketHandler _onInsideMarketUpdate;
        private MarketDepthHandler _onMarketDepthUpdate;
        private TimeAndSalesHandler _onTimeAndSales;
        private OrderHandler _onOrder;
        private SimpleFillHandler _onFill;
        private SystemMessageHandler _onSystemMessage;
        #endregion

        #region PRIVATE
        internal Account moAccount;             // reference to the current account
        internal ExchangeList moExchanges;      // reference to the exchange list
        internal Exchange moExchange;           // reference to the current exchange
        internal ContractList moContracts;      // reference to an exchange's contract list
        internal Contract moContract;           // reference to the current contract
        internal MarketList moPickerMarkets;    // reference to a contract's market list
        internal Market moPickerMarket;         // reference to the current market
        internal MarketList moMarkets1Filter;   // market filters
        internal MarketList moMarkets2Filter;
        internal MarketList moMarkets;
        internal Market moMarket;
        internal Market moMarket1;          // references to selected markets
        internal Market moMarket2;
        internal string mstrMarketID1;      // references to marketid's retrieved from saved settings
        internal string mstrMarketID2;
        internal AccountList moAccounts;    // reference to the accounts list
        internal ArrayList moOrderArrayList = new ArrayList();  // stores the collection of orders

        // Store CTS exchanges, contracts, markets
        private List<Exchange> ctsExchanges = null;
        private Dictionary<Exchange, List<Contract>> ctsContracts = new Dictionary<Exchange, List<Contract>>();
        private Dictionary<Contract, List<Market>> ctsMarkets = new Dictionary<Contract, List<Market>>();

        private bool historicalDataLoadComplete;

        private static APIMain apiFunctionsInstance = null;

        private Host ctsAPI;
        private ezSession apiSession = null;
        private ezAutospreaderManager autospreaderManager = null;
        private bool disposed = false;
        //private WorkerDispatcher disp = null;
        private bool initSuccess = true;
        private string initMessage = "";
        
        private string loginUsername;
        private string loginPassword;
        private bool autoSubscribeInstruments;
        private bool subscribeMarketDepth;
        private bool subscribeTimeAndSales;

        private const string VERSION = "v1.0";
        private const bool DEBUG = true;

        //private EventInstruments instrumentEvents;
        //private EventOrders orderEvents;
        //private EventFills fillEvents;

        private static Dictionary<string, string> simpleTag = new Dictionary<string, string>();
        private static Dictionary<ezInstrumentKey, EZInstrument> zzInstruments = new Dictionary<ezInstrumentKey, EZInstrument>();
        private static Dictionary<ezInstrumentKey, Market> zzMarkets = new Dictionary<ezInstrumentKey, Market>();
        private static Dictionary<ezInstrumentKey, List<EZSpreadInstrument>> zzSpreads = new Dictionary<ezInstrumentKey, List<EZSpreadInstrument>>();
        private static Dictionary<string, Position> zzPositions = new Dictionary<string, Position>();   // <marketID, Position>

        private Dictionary<string, EZOrder> ttOrders = new Dictionary<string, EZOrder>();
        private Dictionary<string, EZOrder> workingTTOrders = new Dictionary<string, EZOrder>();
        private Dictionary<string, EZFill> ttFills = new Dictionary<string, EZFill>();
        private Dictionary<string, EZFill> currentTTFills = new Dictionary<string, EZFill>();
        private Dictionary<string, EZOrder> sentOrders = new Dictionary<string, EZOrder>();
        private Dictionary<string, EZInstrument> sentInstrumentRequests = new Dictionary<string, EZInstrument>();
        private Dictionary<ezInstrumentTradeSubscription, EZInstrument> tradeSubscriptions = new Dictionary<ezInstrumentTradeSubscription,EZInstrument>();
        private Dictionary<string, EZOrderRouteInfo> orderRouteInfo = new Dictionary<string, EZOrderRouteInfo>();

        private Dictionary<string, List<ezFill>> hashSymbols = new Dictionary<string, List<ezFill>>();

        private APIMarketSelectForm marketSelectForm;
        #endregion

        #region CONSTRUCTORS
        /// <summary>
        /// Construct an empty APIFunctions object
        /// </summary>
        /// <param name="autoSubscribeInstruments">set to true to automatically subscribe to instruments in fills and orders</param>
        /// <param name="subscribeBookDepth">set to true to subscribe to book depth (false for inside market only)</param>
        /// <param name="subscribeBookDepth">set to true to subscribe to time and sales</param>
        /// <remarks>In XTrader mode, no username or password are required</remarks>
        private APIMain(bool autoSubscribeInstruments = false, bool subscribeMarketDepth = false, bool subscribeTimeAndSales = false)
        {
            this.autoSubscribeInstruments = autoSubscribeInstruments;
            this.subscribeMarketDepth = subscribeMarketDepth;
            this.subscribeTimeAndSales = subscribeTimeAndSales;
        }

        /// <summary>
        /// Construct an APIFunctions object using the specified username/password
        /// </summary>
        /// <param name="autoSubscribeInstruments">set to true to automatically subscribe to instruments in fills and orders</param>
        /// <param name="subscribeBookDepth">set to true to subscribe to book depth (false for inside market only)</param>
        /// <param name="subscribeBookDepth">set to true to subscribe to time and sales</param>
        /// <param name="username">TT username to use for this TTAPI functions object</param>
        /// <param name="password">TT password to use for this TTAPI functions object</param>
        /// <remarks>In Universal Login mode, a TT username and password are required</remarks>
        private APIMain(bool autoSubscribeInstruments, bool subscribeMarketDepth, bool subscribeTimeAndSales, string username, string password)
        {
            this.autoSubscribeInstruments = autoSubscribeInstruments;
            this.subscribeMarketDepth = subscribeMarketDepth;
            this.subscribeTimeAndSales = subscribeTimeAndSales;
            loginUsername = username;
            loginPassword = password;
        }
        #endregion

        #region API Shutdown
        /// <summary>
        /// Shutdown and clean up the API
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Check to see that we have an api object.
                    if (ctsAPI != null)
                    {
                        // Dispose of the api.
                        ctsAPI.Dispose();
                        ctsAPI = null;
                    }
                }
            }

            disposed = true;
        }
        #endregion

        #region START AND STOP API
        /// <summary>
        /// Start TTAPI in XTrader mode
        /// </summary>
        public void StartAPIUnattended(string username, string password, string Firm = "")
        {
            try
            {
                ctsAPI = new Host(APIServerType.Simulator, "T4Example", "112A04B0-5AAF-42F4-994E-FA7CB959C60B", Firm, username, password);
                ctsAPI.LoginSuccess += new T4.API.Host.LoginSuccessEventHandler(ctsAPI_LoginSuccess);
                ctsAPI.LoginFailure += new T4.API.Host.LoginFailureEventHandler(ctsAPI_LoginFailure);
            }
            catch (Exception ex)
            {
                ctsAPI_LoginFailure(LoginResult.UnexpectedError);
            }
        }

        /// <summary>
        /// Start TTAPI in Universal Login mode
        /// </summary>
        public void StartAPIUserInteraction()
        {
            // Register the host events.			
            //ctsAPI.LoginSuccess += new T4.API.Host.LoginSuccessEventHandler(ctsAPI_LoginSuccess);
            //ctsAPI.LoginFailure += new T4.API.Host.LoginFailureEventHandler(ctsAPI_LoginFailure);
            
            ctsAPI = Host.Login(APIServerType.Simulator, "T4Example", "112A04B0-5AAF-42F4-994E-FA7CB959C60B");

            // Check for success.

            if (ctsAPI == null)
            {
                // Host object not returned which means the user cancelled the login dialog.
                //this.Close();
                ctsAPI_LoginFailure(LoginResult.Logout);
            }
            else
            {
                // Login was successfull.
                Trace.WriteLine("Login Success");

                ctsAPI_LoginSuccess();
                // Initialize.
                //Init();

            }
        }

        public void StopAPI()
        {
            if (marketSelectForm != null)
            {
                marketSelectForm.IsClosing = true;
                marketSelectForm.Close();
            }

            if (ctsAPI != null)
            {
                ctsAPI.Dispose();
                ctsAPI = null;
            }
        }

        /// <summary>
        /// Event raised if login failed. When using the Host.Login method to login this will only be raised in the event of a disconnection from the server.
        /// </summary>
        /// <param name="penReason"></param>
        /// <remarks></remarks>

        private void ctsAPI_LoginFailure(LoginResult penReason)
        {

            Trace.WriteLine("Login Failed due to " + penReason.ToString());
            EventArgs e = new EventArgs();
            
            if (LoginFailure != null) LoginFailure(new EventArgsEx(penReason.ToString()));
        }

        /// <summary>
        /// Event raised if login is successful. When using the Host.Login method to login this will ONLY be raised in the event of a reconnection to the server.
        /// </summary>
        /// <remarks></remarks>

        private void ctsAPI_LoginSuccess()
        {
            // Login was successfull.
            Trace.WriteLine("Login Success");

            // TODO Start thread here to read in the Exchanges, Contracts, Markets...


            // Nothing else needs to be done here when Host.Login is used to login - any market and account subscriptions active when disconnection occurred
            // will automatically be restored. 
            if (LoginSuccess != null) LoginSuccess(new EventArgsEx("login success"));

            Init();
        }
        #endregion

        #region INITIALIZE THE APPLICATION
        private void Init()
        {
            Spy.Print("Init (initializing CTS API)");

            // Populate the available exchanges.
            moExchanges = ctsAPI.MarketData.Exchanges;

            // Register the exchangelist events.
            moExchanges.ExchangeListComplete += new T4.API.ExchangeList.ExchangeListCompleteEventHandler(moExchanges_ExchangeListComplete);

            // Check to see if the data is already loaded.
            if (moExchanges.Complete)
            {
                // Call the event handler ourselves as the data is already loaded.
                moExchanges_ExchangeListComplete(moExchanges);
            }

            //marketSelectForm = new APIMarketSelectForm();

            // Subscribe to accounts.
            SubscribeToAccount();

            Spy.Print("Registering the AccountList events...");
            // Register the accountlist events.
            moAccounts.AccountDetails += new T4.API.AccountList.AccountDetailsEventHandler(moAccounts_AccountDetails);
            moAccounts.AccountUpdate += new T4.API.AccountList.AccountUpdateEventHandler(moAccounts_AccountUpdate);
            moAccounts.PositionUpdate += new T4.API.AccountList.PositionUpdateEventHandler(moAccounts_PositionUpdate);
            //moAccounts.PositionUpdate += new T4.API.AccountList.PositionUpdateEventHandler(moAccounts_PositionUpdate);
                        
            #region READ SAVED MARKETS
            //SaveMarketList();
            #endregion
        }        
        #endregion

        #region EZAPI Instrument, Order, Fill METHODS
        /// <summary>
        /// Retrieve an instrument (TTInstrument) given an InstrumentKey
        /// </summary>
        /// <param name="key">Instrument key to look up</param>
        /// <returns>TTInstrument object associated with the specified InstrumentKey</returns>
        public EZInstrument GetInstrument(ezInstrumentKey key)
        {
            EZInstrument result = null;
            if (zzInstruments.ContainsKey(key))
                result = zzInstruments[key];

            return result;
        }

        /// <summary>
        /// Retrieve a TTInstrument whose name contains the string value passed.
        /// </summary>
        /// <param name="search">text to look for in the instrument name (i.e. 'CL' or 'CL Nov12')</param>
        /// <returns>a TTInstrument whose name contains your text (if one exists)</returns>
        public EZInstrument GetInstrument(string search)
        {
            EZInstrument result = null;
            foreach (EZInstrument tti in zzInstruments.Values)
            {
                if (tti.Name != null && tti.Name.ToUpper().Contains(search.ToUpper()))
                {
                    result = tti;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Retrieve a list of all orders in the system (working and not working)
        /// </summary>
        /// <param name="key">InstrumentKey of instrument in the requested orders</param>
        /// <returns>List of TTOrder objects representing all orders for the specified InstrumentKey</returns>
        public List<EZOrder> GetOrders(ezInstrumentKey key)
        {
            List<EZOrder> results = new List<EZOrder>();

            foreach (EZOrder order in ttOrders.Values)
            {
                if (order.InstrumentKey == key)
                    results.Add(order);
            }
            return results;
        }

        /// <summary>
        /// Retrieve a list of all fills in the system (current and non-current)
        /// </summary>
        /// <param name="key">InstrumentKey of instrument in the requested fills</param>
        /// <returns>List of TTFill objects representing all fills for the specified InstrumentKey</returns>
        /// <remarks>Non-current fills would include amend fill messages, etc.</remarks>
        public List<EZFill> GetFills(ezInstrumentKey key)
        {
            List<EZFill> results = new List<EZFill>();

            foreach (EZFill fill in ttFills.Values)
            {
                if (fill.InstrumentKey == key)
                    results.Add(fill);
            }
            return results;
        }
        #endregion

        #region METHODS TO SUBSCRIBE TO INSTRUMENTS
        /// <summary>
        /// Subscribe to an instrument using an InstrumentDescriptor
        /// </summary>
        /// <param name="descriptor">InstrumentDescriptor containing the instrument information</param>
        public void SubscribeToInstrument(EZInstrumentDescriptor descriptor)
        {
            //EventInstruments.Instance.SubscribeToInstrument(descriptor);
            /*InstrumentLookupSubscription req = new InstrumentLookupSubscription(apiSession, Dispatcher.Current, descriptor.ProductKey, descriptor.ContractName);
            req.Update += new EventHandler<InstrumentLookupSubscriptionEventArgs>(instrumentLookupRequest_Update);
            req.Start();*/
        }

        /// <summary>
        /// Subscribe to an instrument using an InstrumentDescriptor and unique Tag
        /// </summary>
        /// <param name="descriptor">InstrumentDescriptor containing the instrument information</param>
        /// <param name="instrumentRequestTag">unique Tag that will become part of the instrument request</param>
/*        public void SubscribeToInstrument(InstrumentDescriptor descriptor, string tag)
        {
            InstrumentLookupSubscription req = new InstrumentLookupSubscription(apiSession, Dispatcher.Current, descriptor.ProductKey, descriptor.ContractName);
            req.Update += new EventHandler<InstrumentLookupSubscriptionEventArgs>(instrumentLookupRequest_Update);
            req.Tag = tag;  // Tag our request with our unique tag string
            req.Start();
            //Console.WriteLine("SUBSCRIPTION REQUEST: " + descriptor.ToString());
        }
*/
        /// <summary>
        /// Subscribe to an instrument using only the InstrumentKey
        /// </summary>
        /// <param name="key">InstrumentKey of the instrument to which we want to subscribe</param>
        public void SubscribeToInstrument(ezInstrumentKey key)
        {
  
            /*InstrumentLookupSubscription req = new InstrumentLookupSubscription(apiSession, Dispatcher.Current, key);
            req.Update += new EventHandler<InstrumentLookupSubscriptionEventArgs>(instrumentLookupRequest_Update);
            req.Start();*/
        }

        public EZInstrument SubscribeToInstrument(string instrumentKeyString)
        {
            Market market = MarketFromInstrumentKeyString(instrumentKeyString);

            return StartMarketSubscription(market);
        }

        public void UnsubscribeInstrument(EZInstrument instrument)
        {
            if (instrument == null)
                return;

            if (zzInstruments.ContainsKey(instrument.Key))
            {
                zzInstruments.Remove(instrument.Key);
                Market market = zzMarkets[instrument.Key];
                market.MarketDepthUpdate -= Markets_MarketDepthUpdate;
                market.MarketTradeVolume -= Market_MarketTradeVolume;
                market.DepthUnsubscribe();
                zzMarkets.Remove(instrument.Key);
            }
        }

        public EZInstrument CreateInstrument(string instrumentName)
        {
            EZInstrument tti = null;

            string[] items = instrumentName.Split(' ');
            if (items.Length != 3 && items.Length != 4)
                return tti;

            if (items.Length == 4)
            {
                tti = CreateInstrument(items[0], items[1], items[2], items[3]);
            }
            else  // length == 3
            {
                // Assume this is a future if no product type provided
                tti = CreateInstrument(items[0], "FUTURE", items[1], items[2]);
            }

            return tti;
        }

        public EZInstrument CreateInstrument(string marketName, string productTypeName, string productName, string contractName)
        {
            string tag = EZHelper.GetUniqueTag();
            //EventInstruments.Instance.CreateInstrument(marketName, productTypeName, productName, contractName, tag);
            /*InstrumentDescriptor descriptor = new InstrumentDescriptor(marketName, productTypeName, productName, contractName);
            string tag = TTHelper.GetUniqueTag();
            SubscribeToInstrument(descriptor, tag);*/

            EZInstrument tti = null;
            // Loop here to wait for the order to be returned via the API callbacks
            const int TIMEOUT_COUNT = 100;
            for (int i = 0; i < TIMEOUT_COUNT; i++)
            {
                if (sentInstrumentRequests.ContainsKey(tag))
                {
                    tti = sentInstrumentRequests[tag];
                    sentInstrumentRequests.Remove(tag);
                    for (int j=0; j<TIMEOUT_COUNT; j++)
                    {
                        if (tti.Last.IsValid == true)
                            break;
                        Thread.Sleep(50);
                    }
                    break;
                }
                Thread.Sleep(100);
            }
            return tti;
        }

        /// <summary>
        /// Subscribe to a Autospreader instrument with the specified instrument key
        /// </summary>
        /// <param name="key">InstrumentKey of the AS instrument to which we are subscribing</param>
        /*        public void SubscribeToSpread(InstrumentKey key)
                {
                    SpreadDetails details = autospreaderManager.GetSpreadDetails(key);
            
                    CreateAutospreaderInstrumentRequest casReq = new CreateAutospreaderInstrumentRequest(apiSession, Dispatcher.Current, details);
                    casReq.Completed += new EventHandler<CreateAutospreaderInstrumentRequestEventArgs>(autospreaderLookupRequest_Completed);
                    casReq.Submit();
                }
        */
        #endregion

        #region *** ORDERS ***
        public void SetOrderRouteInfo(string marketName, string orderFeedName, string accountTypeName, string accountName)
        {
            EZOrderRouteInfo info = new EZOrderRouteInfo();
            info.MarketName = marketName;
            info.OrderFeedName = orderFeedName;
            info.AccountTypeName = accountTypeName;
            info.AccountName = accountName;

            orderRouteInfo[marketName] = info;
        }

        public EZOrder BuyLimit(EZInstrument tti, int quantity, double price, string Tag = "")
        {
            return SendOrder(tti, zBuySell.Buy, zOrderType.Limit, ezQuantity.FromInt(tti, quantity), ezPrice.FromDouble(tti, price), Tag);
        }

        public EZOrder SellLimit(EZInstrument tti, int quantity, double price, string Tag = "")
        {
            return SendOrder(tti, zBuySell.Sell, zOrderType.Limit, ezQuantity.FromInt(tti, quantity), ezPrice.FromDouble(tti, price), Tag);
        }

        public EZOrder BuyMarket(EZInstrument tti, int quantity, string Tag = "")
        {
            return SendOrder(tti, zBuySell.Buy, zOrderType.Market, ezQuantity.FromInt(tti, quantity), ezPrice.Invalid, Tag);
        }

        public EZOrder SellMarket(EZInstrument tti, int quantity, string Tag = "")
        {
            return SendOrder(tti, zBuySell.Sell, zOrderType.Market, ezQuantity.FromInt(tti, quantity), ezPrice.Invalid, Tag);
        }

        public EZOrder SendOrder(EZInstrument instrument, zBuySell buySell, zOrderType orderType, ezQuantity quantity, ezPrice price, string Tag = "")
        {
            EZOrder result = null;

            try
            {
                Account account = moAccounts[0];
                Market market = MarketFromInstrument(instrument);
                Order order = moAccounts.SubmitNewOrder(account, market, APIConvert.ToBuySell(buySell), PriceType.Limit, TimeType.Normal, quantity, price);
                StoreTag(order.UniqueID, Tag);

                order.OrderFill += order_OrderFill;

                result = APIMain.GetOrder(order);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TraceException(ex);
            }

            return result;            
        }

        void order_OrderFill(Order poOrder, Order.Trade poTrade)
        {
            EZFill ezf = GetFill(poOrder, poTrade);
            ezf.Tag = RetrieveTag(poOrder.UniqueID);

            if (OnFill != null) OnFill(ezf);
        }

        public static void SendOrder()
        {
            throw new NotImplementedException();
        }

        void tto_OnOrderModify(EZModify modify, EZOrder order, ezPrice price, ezQuantity quantity)
        {
            switch (modify)
            {
                case EZModify.Price:
                    ModifyPrice(order, price);
                    break;
                case EZModify.Quantity:
                    ModifyQuantity(order, quantity);
                    break;
            }
        }

        public void ModifyPrice(EZOrder order, ezPrice price)
        {
            EZOrder tto = null;
            string tag = order.Tag;
            order.TTAPI_Order.ModifyPrice(price);

            // Loop here to wait for the order to be returned via the API callbacks
            const int TIMEOUT_COUNT = 300;
            for (int i = 0; i < TIMEOUT_COUNT; i++)
            {
                if (sentOrders.ContainsKey(tag))
                {
                    tto = sentOrders[tag];
                    sentOrders.Remove(tag);
                    break;
                }
                Thread.Sleep(10);
            }
        }

        public void ModifyQuantity(EZOrder order, ezQuantity quantity)
        {
            EZOrder tto = null;
            string tag = order.Tag;
            order.TTAPI_Order.ModifyQuantity(quantity);

            // Loop here to wait for the order to be returned via the API callbacks
            const int TIMEOUT_COUNT = 300;
            for (int i = 0; i < TIMEOUT_COUNT; i++)
            {
                if (sentOrders.ContainsKey(tag))
                {
                    tto = sentOrders[tag];
                    sentOrders.Remove(tag);
                    break;
                }
                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// Cancel all working orders in EZAPI
        /// </summary>
        public void CancelAllOrders()
        {
            foreach (EZOrder order in workingTTOrders.Values)
                order.Cancel();
        }

        /// <summary>
        /// Hold or "remove-from-hold" all working orders in EZAPI
        /// </summary>
        /// <param name="hold"></param>
        public void HoldAllOrders(bool hold)
        {
            foreach (EZOrder order in workingTTOrders.Values)
                order.Hold(hold);
        }
        #endregion

        #region HELPER METHODS
        void DisplayOrderSuccess(bool b)
        {
            if (b == true)
            {
                //listOrders.Items.Insert(0, "Success!!!");
            }
            else
            {
                //listOrders.Items.Insert(0, "ERROR: SendOrder failed.");
            }
        }

        string FillToString(ezFill fill)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" " + fill.BuySell.ToString() + " ");
            sb.Append(" " + fill.Quantity.ToInt().ToString() + " ");
            sb.Append(" " + fill.InstrumentKey + " ");
            sb.Append("@ " + fill.MatchPrice + " ");
            return (sb.ToString());
        }

        string OrderToString(ezOrder order)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{" + order.BuySell.ToString() + "}");
            sb.Append("{" + order.OrderQuantity.ToString() + "}");
            sb.Append("{" + order.InstrumentKey + "}");
            sb.Append("{" + order.LimitPrice + "}");
            if (order.IsSynthetic)
                sb.Append(" SYNTHETIC ");
            if (order.IsParent)
                sb.Append("  PARENT  ");
            if (order.IsChild)
                sb.Append("  CHILD  ");
            return (sb.ToString());
        }

        void Message(string msg)
        {
            if (DEBUG) Console.WriteLine(msg);
        }

        void ErrorMessage(string msg)
        {
            if (DEBUG) Console.WriteLine("ERROR: " + msg);
        }
        #endregion

        #region EXCHANGES, CONTRACTS, MARKETS
        public List<Exchange> GetExchangeList()
        {
            /*// Unregister previous events.
            if (moExchanges != null)
                moExchanges.ExchangeListComplete -= new T4.API.ExchangeList.ExchangeListCompleteEventHandler(moExchanges_ExchangeListComplete);

            // Register the events.
            if (moExchanges != null)
                moExchanges.ExchangeListComplete += moExchanges_ExchangeListComplete;

            // Check to see if the data is already loaded.
            if (moExchanges.Complete)
                moExchanges_ExchangeListComplete(moExchanges);*/

            int retryCount = 0;
            while (!moExchanges.Complete && retryCount < 10)
            {
                Spy.Print("Sleeping to wait for exchanges load to complete...");
                Thread.Sleep(500);
                ++retryCount;
            }

            return ctsExchanges;
        }

        public Dictionary<string, Exchange> GetExchangeDictionary()
        {
            Dictionary<string, Exchange> result = new Dictionary<string, Exchange>();

            foreach (Exchange exchange in ctsExchanges)
            {
                result[exchange.Description] = exchange;
            }

            return result;
        }

        void moExchanges_ExchangeListComplete(T4.API.ExchangeList poExchangeList)
        {
            ExchangeListComplete();
        }

        void ExchangeListComplete()
        {
            // Populate the list of exchanges.
            if (ctsExchanges == null)
            {
                ctsExchanges = new List<Exchange>();
                try
                {
                    // Lock the API while traversing the api collection.
                    // Lock at the lowest level object for the shortest period of time.
                    ctsAPI.EnterLock("ExchangeList");

                    // Add the exchanges to the list.
                    foreach (Exchange oExchange in moExchanges)
                    {
                        ctsExchanges.Add(oExchange);
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
                    ctsAPI.ExitLock("ExchangeList");
                }
            }
        }

        public List<Contract> GetContractList(Exchange exchange)
        {
            if (!ctsContracts.ContainsKey(exchange) || ctsContracts[exchange] == null)
            {
                ctsContracts[exchange] = new List<Contract>();

                moExchange = exchange;

                // Unregister previous events.
                if (moContracts != null)
                    moContracts.ContractListComplete -= new T4.API.ContractList.ContractListCompleteEventHandler(moContracts_ContractListComplete);

                // Reference the contracts for this exchange.
                moContracts = exchange.Contracts;

                // Register the events.
                if (moContracts != null)
                    moContracts.ContractListComplete += moContracts_ContractListComplete;

                // Check to see if the data is already loaded.
                if (moContracts.Complete)
                    moContracts_ContractListComplete(moContracts);

                int retryCount = 0;
                while (!moContracts.Complete && retryCount < 10)
                {
                    Spy.Print("Sleeping to wait for contracts load to complete...");
                    Thread.Sleep(500);
                    ++retryCount;
                    /*if (exchange != null && ctsContracts.ContainsKey(exchange))
                    {
                        moContracts_ContractListComplete(moContracts);
                    }*/
                }
            }

            return ctsContracts[exchange];
        }

        public Dictionary<string, Contract> GetContractDictionary(Exchange exchange)
        {
            Dictionary<string, Contract> result = new Dictionary<string, Contract>();

            foreach (Contract contract in GetContractList(exchange))
            {
                result[contract.Description] = contract;
            }

            return result;
        }

        void moContracts_ContractListComplete(ContractList poContractList)
        {
            ContractListComplete();
        }

        void ContractListComplete()
        {
            // Populate the list of contracts available for the current exchange.
            if ((moContracts != null))
            {
                try
                {
                    // Lock the API while traversing the api collection.
                    // Lock at the lowest level object for the shortest period of time.
                    ctsAPI.EnterLock("ContractList");

                    // Add the exchanges to the dropdown list.
                    foreach (Contract oContract in moContracts)
                    {
                        ctsContracts[moExchange].Add(oContract);
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
                    ctsAPI.ExitLock("ContractList");
                }
            }
        }

        public List<Market> GetMarketList(Contract contract)
        {
            if (!ctsMarkets.ContainsKey(contract) || ctsMarkets[contract] == null)
            {
                ctsMarkets[contract] = new List<Market>();

                moContract = contract;

                // Unregister previous events.
                if (moMarkets != null)
                    moMarkets.MarketListComplete -= new T4.API.MarketList.MarketListCompleteEventHandler(moMarkets_MarketListComplete);

                // Reference the contracts for this exchange.
                moMarkets = contract.Markets;

                // Register the events.
                if (moMarkets != null)
                    moMarkets.MarketListComplete += moMarkets_MarketListComplete;

                // Check to see if the data is already loaded.
                if (moMarkets.Complete)
                    moMarkets_MarketListComplete(moMarkets);

                int retryCount = 0;
                while (!moMarkets.Complete && retryCount < 10)
                {
                    Spy.Print("Sleeping to wait for markets load to complete...");
                    Thread.Sleep(500);
                    ++retryCount;
                }
            }

            return ctsMarkets[contract];
        }

        public Dictionary<string, Market> GetMarketDictionary(Contract contract)
        {
            Dictionary<string, Market> result = new Dictionary<string, Market>();

            foreach (Market market in GetMarketList(contract))
            {
                result[market.Description] = market;
            }

            return result;
        }

        void moMarkets_MarketListComplete(MarketList poMarketList)
        {
            MarketListComplete();
        }

        void MarketListComplete()
        {
            // Populate the list of contracts available for the current exchange.
            if ((moMarkets != null))
            {
                try
                {
                    // Lock the API while traversing the api collection.
                    // Lock at the lowest level object for the shortest period of time.
                    ctsAPI.EnterLock("MarketList");

                    // Add the exchanges to the dropdown list.
                    foreach (Market oMarket in moMarkets)
                    {
                        ctsMarkets[moContract].Add(oMarket);
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
                    ctsAPI.ExitLock("MarketList");
                }
            }
        }

        public void SaveXmlMarketList(List<Market> markets, string filename)
        {
            try
            {
                var saveMarkets = new EZAPI.Framework.Xml.XmlSaveMarketList();

                // For each Market in the list, create a key string ("exchange@contract@market") and store it in our XmlSaveMarketList.
                foreach (var market in markets)
                {
                    saveMarkets.MarketKeyStrings.Add(APIFactory.CreateKeyString(market));
                }

                EZAPI.Toolbox.Serialization.Xml.Serialize(saveMarkets, typeof(EZAPI.Framework.Xml.XmlSaveMarketList), filename);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TraceExceptionDetail(ex);
            }
        }

        internal List<Market> LoadXmlMarketList(string filename)
        {
            List<Market> markets = new List<Market>();

            try
            {
                var saveMarkets = EZAPI.Toolbox.Serialization.Xml.Deserialize(typeof(EZAPI.Framework.Xml.XmlSaveMarketList), filename) as EZAPI.Framework.Xml.XmlSaveMarketList;

                if (saveMarkets != null)
                {
                    foreach (string keyString in saveMarkets.MarketKeyStrings)
                    {
                        Market market = MarketFromInstrumentKeyString(keyString);
                        if (market != null)
                            markets.Add(market);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TraceExceptionDetail(ex);
            }

            return markets;
        }

        // Returns various "API Directory" names (such as a directory to store XML files, etc)
        // and creates the directory if it does not exist.
        public static string GetAPIDirectory(string directoryName)
        {
            string result = null;

            try
            {
                if (directoryName.ToUpper().Trim() == "XML")
                {
                    result = System.Windows.Forms.Application.StartupPath + "\\XMLfiles\\";
                }
                else
                {
                    throw new ArgumentOutOfRangeException("directoryName", "Cannot find APIDirectory requested: '" + directoryName + "'");
                }

                // Create the directory if it does not exist.
                if (!System.IO.Directory.Exists(result))
                {
                    System.IO.Directory.CreateDirectory(result);
                }
            }
            catch (Exception ex)
            {
                EZAPI.Toolbox.Debug.ExceptionHandler.TraceException(ex);
            }

            return result;
        }

        /*
        void SaveMarketList()
        {
            try
            {
                // Read saved markets from XML Doc.
                XmlDocument oDoc;

                // XML Nodes for viewing the doc.
                XmlNode oMarkets;

                // Temporary string variables for referencing contract and exchange details.
                string strContractID;
                string strExchangeID;

                // Pull the xml doc from the server.
                oDoc = ctsAPI.UserSettings;

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
                                moMarkets1Filter = ctsAPI.MarketData.CreateMarketFilter(strExchangeID, strContractID, 0, T4.ContractType.Any, T4.StrategyType.Any);

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
                                moMarkets2Filter = ctsAPI.MarketData.CreateMarketFilter(strExchangeID, strContractID, 0, T4.ContractType.Any, T4.StrategyType.Any);

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
        }*/
        #endregion

        #region ACCOUNT METHODS AND EVENT HANDLERS
        // Method called following login success to get the data for 
        // an account and subscribe to it.
        private void SubscribeToAccount()
        {
            Spy.Print("SubscribeToAccount() method...");

            try
            {
                // Lock the API.
                ctsAPI.EnterLock("AccountSubscribe");

                // Set the account list reference so that we can get 
                // Account and order events.
                moAccounts = ctsAPI.Accounts;

                // Display the account list.
                foreach (Account oAccount in ctsAPI.Accounts)
                {
                    Spy.Print("Subscribing to account {0}...", oAccount.AccountNumber);

                    // Subscribe to the account.
                    oAccount.Subscribe();
                    //oAccount.PositionUpdate += oAccount_PositionUpdate;
                    // Get initial positions for the account.
                    /*for (int i = 0; i < oAccount.Positions.Count; i++)
                    {
                        zzPositions[oAccount.Positions[i].MarketID] = oAccount.Positions[i];
                    }*/
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
                ctsAPI.ExitLock("AccountSubscribe");
            }
        }

        void oAccount_PositionUpdate(Account poAccount, Position poPosition)
        {
            zzPositions[poPosition.MarketID] = poPosition;
        }


        // Event that is raised when details for an account have 
        // changed, or a new account is recieved.
        private void moAccounts_AccountDetails(T4.API.AccountList.UpdateList poAccounts)
        {
            //Spy.Print("moAccounts_AccountDetails EVENT!");
            
            OnAccountDetails(poAccounts);
        }

        private void OnAccountDetails(AccountList.UpdateList poAccounts)
        {
            // Display the account list.
            foreach (Account oAccount in poAccounts)
            {
                Spy.Print("AccountDetails: {0} {1} ${2:0.00} ${3:0.00}", oAccount.AccountNumber, oAccount.Description, oAccount.Balance, oAccount.AvailableCash);

                // Check to see if the account exists prior to adding/subscribing to it.
                if (oAccount.Subscribed != true)
                {
                    // Subscribe to the account.
                    oAccount.Subscribe();
                }
            }
        }

        // Event that is raised when the accounts overall balance,
        // P&L or margin details have changed.
        private void moAccounts_AccountUpdate(T4.API.AccountList.UpdateList poAccounts)
        {
            //Spy.Print("moAccounts_AccountUpdate EVENT!");

            OnAccountUpdate(poAccounts);
        }

        private void OnAccountUpdate(T4.API.AccountList.UpdateList poAccounts)
        {
            // Just refresh the current account.
            //DisplayAccount(moAccount);
        }
        #endregion

        #region POSITION METHODS AND EVENT HANDLERS
        // Event that is raised when positions for accounts have changed.
        private void moAccounts_PositionUpdate(AccountList.PositionUpdateList poPositions)
        {
            //Spy.Print("moAccounts_PositionUpdate EVENT!");

            // Display the position details.
            {
                foreach (AccountList.PositionUpdateList.PositionUpdate oUpdate in poPositions)
                {
                    // If the position is for the current account
                    // then update the value.
                    if (object.ReferenceEquals(oUpdate.Account, moAccount))
                    {
                        OnPositionUpdate(oUpdate.Position);
                        break;
                    }
                }
            }
        }

        private void OnPositionUpdate(T4.API.Position poPosition)
        {
            // Retrieve the position details.
            //RetrievePosition(poPosition.Market);
            zzPositions[poPosition.MarketID] = poPosition;
        }

        /*
        private void RetrievePosition(Market poMarket)
        {
            bool blnLocked = false;

            try
            {
                if ((poMarket != null) && (moAccount != null))
                {
                    // From the Market, get an ezInstrumentKey (and subsequently, an EZInstrument).
                    ezInstrumentKey iKey = APIFactory.MakeInstrumentKey(poMarket);
                    EZInstrument instrument = zzInstruments[iKey];

                    // Lock the host while we retrieve details.
                    ctsAPI.EnterLock("DisplayPositions");

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
                        instrument.NetPos = oPosition.Net;
                        instrument.NetBuys = oPosition.Buys;
                        instrument.NetSells = oPosition.Sells;
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
                    ctsAPI.ExitLock("RetrievePositions");
            }
        }
        */
        #endregion

        #region MARKET FILTERS
        private void moMarkets1Filter_MarketListComplete(T4.API.MarketList poMarketList)
        {
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

        public EZInstrument ShowInstrumentDialog(EZInstrument defaultInstrument = null)
        {
            EZInstrument instrument = null;

            //moMarket = ctsAPI.MarketData.MarketPicker(ref moMarket);
            Market defaultMarket = null;
            if (defaultInstrument != null)
                defaultMarket = zzMarkets[defaultInstrument.Key];
            
            /*moMarket = ctsAPI.MarketData.MarketPicker(ref defaultMarket);*/

            if (marketSelectForm == null)
                marketSelectForm = new APIMarketSelectForm();

            marketSelectForm.ShowDialog();
            
            moMarket = marketSelectForm.SelectedMarket;
            if (moMarket != null)
            {
                instrument = StartMarketSubscription(moMarket);
            }

            return instrument;
        }

        Market MarketFromInstrumentKeyString(string instrumentKeyString)
        {
            Market foundMarket = null;

            string exchangeID, contractID, marketID;
            APIFactory.ParseInstrumentKey(instrumentKeyString, out exchangeID, out contractID, out marketID);

            // First, find the exchange by exchangeID.
            Exchange foundExchange = null;
            List<Exchange> exchanges = GetExchangeList();
            foreach (var exchange in exchanges)
            {
                if (exchange.ExchangeID == exchangeID)
                {
                    foundExchange = exchange;
                    break;
                }
            }

            // Second, find the contract by contractID.
            if (foundExchange != null)
            {
                Contract foundContract = null;
                List<Contract> contracts = GetContractList(foundExchange);
                foreach (var contract in contracts)
                {
                    if (contract.ContractID == contractID)
                    {
                        foundContract = contract;
                        break;
                    }
                }

                // Finally, find the market by marketID.
                if (foundContract != null)
                {
                    List<Market> markets = GetMarketList(foundContract);
                    foreach (var market in markets)
                    {
                        if (market.MarketID == marketID)
                        {
                            foundMarket = market;
                            break;
                        }
                    }
                }
            }

            return foundMarket;
        }

        EZInstrument StartMarketSubscription(Market market)
        {            
            EZInstrument instrument = APIFactory.MakeInstrument(market);
            zzInstruments[instrument.Key] = instrument;
            zzMarkets[instrument.Key] = market;
            market.MarketDepthUpdate += Markets_MarketDepthUpdate;
            market.MarketTradeVolume += Market_MarketTradeVolume;
            market.DepthSubscribe(DepthBuffer.Smart, DepthLevels.Normal);

            // If we have a position for this market, then subscribe to position update events.
            if (zzPositions.ContainsKey(market.MarketID))
            {
                zzPositions[market.MarketID].PositionUpdate += APIFunctions_PositionUpdate;
            }

            return instrument;
        }

        public void SubscribeToSpread(EZSpreadInstrument spread)
        {
            foreach (ezSpreadLeg leg in spread.Legs)
            {
                TrackSpreadLeg(spread, leg.Instrument, true);
            }
        }

        void TrackSpreadLeg(EZSpreadInstrument spread, EZInstrument instrument, bool enableTracking)
        {
            if (enableTracking == true)
            {
                if (!zzSpreads.ContainsKey(instrument.Key) || zzSpreads[instrument.Key] == null)
                    zzSpreads[instrument.Key] = new List<EZSpreadInstrument>();

                zzSpreads[instrument.Key].Add(spread);
            }
            else
            {
                // Stop tracking instrument for spread leg
            }
        }

        List<EZSpreadInstrument> GetSpreadsFromInstrument(EZInstrument instrument)
        {
            List<EZSpreadInstrument> result = null;

            if (zzSpreads.ContainsKey(instrument.Key) && zzSpreads[instrument.Key] != null)
            {
                result = zzSpreads[instrument.Key];
            }

            return result;
        }

        void APIFunctions_PositionUpdate(Position poPosition)
        {
            // From the Market, get an ezInstrumentKey (and subsequently, an EZInstrument).
            ezInstrumentKey iKey = APIFactory.MakeInstrumentKey(poPosition.Market);
            EZInstrument instrument = zzInstruments[iKey];

            instrument.NetPos = poPosition.Net;
            instrument.NetBuys = poPosition.Buys;
            instrument.NetSells = poPosition.Sells;
            instrument.Profit = poPosition.PL;
        }

        void Market_MarketTradeVolume(Market poMarket, Market.TradeVolume poChanges)
        {
            /*var sText = poMarket.Description;
            
            for (int i=0; i<poChanges.Count; i++)
            {
                var tVol = poChanges[i];
                sText += ", " + tVol.Volume.ToString() + "@" + poMarket.ConvertTicksDisplay(tVol.Ticks);
            }

            Trace.WriteLine(sText);*/
        }

        void Markets_MarketDepthUpdate(Market poMarket)
        {
            // From the Market, get an ezInstrumentKey (and subsequently, an EZInstrument).
            ezInstrumentKey iKey = APIFactory.MakeInstrumentKey(poMarket);
            EZInstrument instrument = zzInstruments[iKey];

            // Get information from Market object.
            //int depthLevels = poMarket.DepthLevels;
            Market.Depth depth = poMarket.LastDepth;
            int ltv = depth.LastTradeVolume;
            int lttv = depth.LastTradeTotalVolume;
            int ltpTicks = depth.LastTradeTicks;
            double ltp = poMarket.ConvertTicksToDecimal(ltpTicks);
            int btv = 0;
            int btpTicks = 0;
            double btp = 0.0;
            int atv = 0;
            int atpTicks = 0;
            double atp = 0.0;
            bool invalidBid = false;
            bool invalidAsk = false;
            //string ltpDisplay = poMarket.ConvertTicksDisplay(ltpTicks);
            if (depth.Bids.Count <= 0)
                invalidBid = true;
            else
            {
                btv = depth.Bids[0].Volume;
                btpTicks = depth.Bids[0].Ticks;
                btp = poMarket.ConvertTicksToDecimal(btpTicks);
            }
            if (depth.Offers.Count <= 0)
                invalidAsk = true;
            else
            {
                atv = depth.Offers[0].Volume;
                atpTicks = depth.Offers[0].Ticks;
                atp = poMarket.ConvertTicksToDecimal(atpTicks);
            }
            int totalVolume = depth.LastTradeTotalVolume;

            /*for (int i = 0; i < depth.Bids.Count; i++)
            {
                int dTicks = depth.Bids[i].Ticks;
                int dVol = depth.Bids[i].Volume;
                Trace.WriteLine("bid " + dVol.ToString() + " @ " + poMarket.ConvertTicksDisplay(dTicks));
            }
            for (int i = 0; i < depth.Offers.Count; i++)
            {
                int dTicks = depth.Offers[i].Ticks;
                int dVol = depth.Offers[i].Volume;
                Trace.WriteLine("ask " + dVol.ToString() + " @ " + poMarket.ConvertTicksDisplay(dTicks));
            }*/

            // Populate EZInstrument object with information.
            instrument.Last = ezPrice.FromDouble(instrument, ltp);
            instrument.LastQty = ezQuantity.FromInt(instrument, ltv);
            instrument.LastTotalVolume = lttv;
            instrument.Bid = ezPrice.FromDouble(instrument, btp);
            instrument.BidQty = ezQuantity.FromInt(instrument, btv);
            instrument.Ask = ezPrice.FromDouble(instrument, atp);
            instrument.AskQty = ezQuantity.FromInt(instrument, atv);
            instrument.Volume = ezQuantity.FromInt(instrument, totalVolume);
            
            // If bid/offer is invalid, then we won't return a valid price/qty.
            if (invalidBid)
            {
                instrument.Bid = ezPrice.Invalid;
                instrument.BidQty = ezQuantity.Invalid;
            }
            if (invalidAsk)
            {
                instrument.Ask = ezPrice.Invalid;
                instrument.AskQty = ezQuantity.Invalid;
            }

            if (OnInsideMarketUpdate != null) OnInsideMarketUpdate(instrument);

            List<EZSpreadInstrument> spreads = GetSpreadsFromInstrument(instrument);
            if (spreads != null)
            {
                foreach (EZSpreadInstrument spread in spreads)
                {
                    spread.UpdatePrice();
                    if (OnInsideMarketUpdate != null) OnInsideMarketUpdate(spread);
                }
            }
        }

        public static EZOrder GetOrder(Order order)
        {
            ezInstrumentKey iKey = APIFactory.MakeInstrumentKey(order.Market);
            EZInstrument ezi = zzInstruments[iKey];
            zBuySell buySell = APIConvert.FromBuySell(order.BuySell);
            ezQuantity quantity = ezQuantity.FromInt(ezi, order.NewVolume);
            ezPrice price = PriceFromTicks(ezi, order.NewLimitTicks);
            EZOrder ezo = new EZOrder(iKey, buySell, quantity, price);

            return ezo;
        }

        public static EZFill GetFill(Order order, Order.Trade trade)
        {
            EZInstrument ezi = GetInstrument(order.Market);

            zFillType fillType = zFillType.Full;
            if (trade.ResidualVolume != 0)
                fillType = zFillType.Partial;
            
            EZFill ezf = new EZFill(trade.Time, ezi, APIConvert.FromBuySell(order.BuySell), ezQuantity.FromInt(ezi, trade.Volume), PriceFromTicks(ezi, trade.Ticks), fillType);
            return ezf;
        }

        public static EZInstrument GetInstrument(Market market)
        {
            ezInstrumentKey iKey = APIFactory.MakeInstrumentKey(market);
            EZInstrument ezi = zzInstruments[iKey];
            return ezi;
        }

        public static ezPrice PriceFromTicks(EZInstrument instrument, int ticks)
        {
            Market market = MarketFromInstrument(instrument);
            double price = market.ConvertTicksToDecimal(ticks);

            return ezPrice.FromDouble(instrument, price);
        }

        public static EZInstrument InstrumentFromKey(ezInstrumentKey ikey)
        {
            if (ikey == null)
                return null;
            else
                return zzInstruments[ikey];
        }

        public static Market MarketFromInstrument(EZInstrument ezi)
        {
            if (ezi == null)
                return null;
            else
                return zzMarkets[ezi.Key];
        }

        public static ezPrice AddTicks(EZInstrument ezi, ezPrice price, int add)
        {
            Market market = zzMarkets[ezi.Key];
            int ticks = market.ConvertDecimalToTicks(price.ToDouble());
            ticks += add;
            double decimalPrice = market.ConvertTicksToDecimal(ticks);

            return ezPrice.FromDouble(ezi, decimalPrice);
        }

        public static ezPrice IncrementTick(EZInstrument ezi, ezPrice price)
        {
            return AddTicks(ezi, price, 1);
        }

        public static ezPrice DecrementTick(EZInstrument ezi, ezPrice price)
        {
            return AddTicks(ezi, price, -1);
        }

        void StoreTag(string uniqueKey, string value)
        {
            simpleTag[uniqueKey] = value;
        }

        string RetrieveTag(string uniqueKey)
        {
            return simpleTag[uniqueKey];
        }

        /// <summary>
        /// Very similar to the method to load a chart. The only difference is that this method
        /// is designed to retrieve historical data for use in places OTHER than a chart.
        /// </summary>
        /// <param name="instrument">instrument for which we are requesting data</param>
        /// <param name="interval">time frame for "bars" we are requesting (ex: interval=[minute] for 15 minute "bars")</param>
        /// <param name="period">period for each "bar" (ex: period=15 for 15 minute "bars")</param>
        /// <returns>an EZChartDataSeries containing the "bars" of historical data</returns>
        public EZChartDataSeries RequestHistoricalData(EZInstrument instrument, zChartInterval interval, int period, int timeoutMilliseconds = 10000)
        {
            EZChartDataSeries chartBarData;

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

            historicalDataLoadComplete = false;

            chartBarData = APIFactory.MakeChartData(instrument, oBarIntvl, dtStartDate);
            chartBarData.DataLoadComplete += historicalData_DataLoadComplete;

            DateTime startTime = DateTime.Now;
            TimeSpan elapsedTime = DateTime.Now.Subtract(startTime);
            while (historicalDataLoadComplete == false && elapsedTime.TotalMilliseconds < timeoutMilliseconds)
            {
                Spy.Print("Sleeping to wait for historical data to load...");
                Thread.Sleep(500);
                elapsedTime = DateTime.Now.Subtract(startTime);
            }

            // If we haven't completed the historical data download, then return null.
            if (historicalDataLoadComplete == false)
                chartBarData = null;

            return chartBarData;
        }

        void historicalData_DataLoadComplete(object sender, ezDataLoadCompleteEventArgs e)
        {
            historicalDataLoadComplete = true;
        }


    } // class


    /// <summary>
    /// Class to pass ONE value in an EventArg (default EventArgs passes zero values).
    /// </summary>
    public class EventArgsEx : EventArgs
    {
        public object Value { get { return value; } }

        private object value; 

        public EventArgsEx(object returnValue)
        {
            value = returnValue;
        }
    } // class

} // namespace



/*
MarketLoader
Print
RSS
The Market list is loaded dynamically for each Contract and you need to check if the Exchange list, Contract list and Market list are loaded or listen for the complete events. A simpler way of dealing with this is to use the MarketLoader object. You create this and then call the LoadMarket method and it will raise an event when the Market is loaded. If the Market is already loaded then the event is raised immediately, if it isn't loaded then it will be raised asynchronously. If the Market could not be loaded, for example if the user does not have permission to see it or if it has expired, then the complete event is raised with Nothing as the Market parameter.

' Reference to the market loader.
Private WithEvents moMarketLoader As MarketLoader

...

' Load the market.
moMarketLoader = New MarketLoader(moHost)
moMarketLoader.LoadMarket(msExchangeID, msContractID, msMarketID)

...

' Deal with the market loader completing.
Private Sub moMarketLoader_MarketLoadComplete(ByVal poMarketLoader As MarketLoader, poMarket As Market) Handles moMarketLoader.MarketLoadComplete

If Not poMarket Is Nothing Then

' Market is loaded.

Else

' Market failed to load.

End If

End Sub


ContractLoader
Print
RSS
The Contract list is loaded dynamically for each Exchange and you need to check if the Exchange list and Contract list are loaded or listen for the complete events. A simpler way of dealing with this is to use the ContractLoader object. You create this and then call the LoadContract method and it will raise an event when the Contract is loaded. If the Contract is already loaded then the event is raised immediately, if it isn't loaded then it will be raised asynchronously. If the Contract could not be loaded, for example if the user does not have permission to see it, then the complete event is raised with Nothing as the Contract parameter.

' Reference to the contract loader.
Private WithEvents moContractLoader As ContractLoader

...

' Load the contract.
moContractLoader = New ContractLoader(moHost)
moContractLoader.LoadContract(msExchangeID, msContractID)

...

' Deal with the contract loader completing.
Private Sub moContractLoader_ContractLoadComplete(ByVal poContractLoader As ContractLoader, poContract As Contract) Handles moContractLoader.ContractLoadComplete

If Not poContract Is Nothing Then

' Contract is loaded.

Else

' Contract failed to load.

End If

End Sub
 

ExchangeLoader
Print
RSS
The Exchange list is loaded dynamically and you need to check if the list is loaded or listen for the complete event. A simpler way of dealing with this is to use the ExchangeLoader object. You create this and then call the LoadExchange method and it will raise an event when the Exchange is loaded. If the Exchange is already loaded then the event is raised immediately, if it isn't loaded then it will be raised asynchronously. If the Exchange could not be loaded, for example if the user does not have permission to see it, then the complete event is raised with Nothing as the Exchange parameter.

' Reference to the exchange loader.
Private WithEvents moExchangeLoader As ExchangeLoader

...

' Load the exchange.
moExchangeLoader = New ExchangeLoader(moHost)
moExchangeLoader.LoadExchange(msExchangeID)

...

' Deal with the exchange loader completing.
Private Sub moExchangeLoader_ExchangeLoadComplete(ByVal poExchangeLoader As ExchangeLoader, poExchange As Exchange) Handles moExchangeLoader.ExchangeLoadComplete

If Not poExchange Is Nothing Then

' Exchange is loaded.

Else

' Exchange failed to load.

End If

End Sub 
*/