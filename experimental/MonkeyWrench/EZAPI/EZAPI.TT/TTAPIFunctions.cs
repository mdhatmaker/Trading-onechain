using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using TradingTechnologies.TTAPI;
using TradingTechnologies.TTAPI.Tradebook;
using TradingTechnologies.TTAPI.Autospreader;
using EZAPI.Containers;

/*
 * To use this API wrapper, your startup code should resemble this:
            string ttUsername = "TTUSERNAME";
            string ttPassword = "TTPASSWORD";

            api = new TTAPIFunctions(ttUsername, ttPassword);
            api.OnInitialize += new InitializeHandler(api_OnInitialize);
            api.OnInsideMarketUpdate += new InsideMarketHandler(api_OnInsideMarketUpdate);
            api.OnFill += new FillHandler(api_OnFill);
            api.OnOrder += new OrderHandler(api_OnOrder);

            Thread workerThread = new Thread(api.Start);
            workerThread.Name = "TTAPI Thread";
            workerThread.Start();
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
    public delegate void InstrumentFoundHandler(TTInstrument ttInstrument, bool success);
    public delegate void InsideMarketHandler(TTInstrument ttInstrument);
    public delegate void MarketDepthHandler(TTInstrument ttInstrument, TTMarketDepth marketDepth);
    public delegate void FillHandler(FillOriginator originator, FillAction action, TTInstrument ttInstrument, TTFill fill, TTFill newFill);
    public delegate void OrderHandler(TTOrderStatus status, TTInstrument ttInstrument, TTOrder order, TTOrder newOrder);
    public delegate void SystemMessageHandler(SystemMessage systemMessage);
    public delegate void TimeAndSalesHandler(TTInstrument ttInstrument, DateTime timeStamp, Price LTP, Quantity LTQ, TradeDirection direction, bool isOTC);

    /// <summary>
    /// This class contains the base-level functionality for EZAPI
    /// </summary>
    /// <remarks>This class is responsible for communication with TTAPI</remarks>
    public class TTAPIFunctions : IDisposable
    {
        /// <summary>
        /// OnInitialize is fired when the EZAPI is initialized (successfully or not)
        /// </summary>
        public event InitializeHandler OnInitialize;
        /// <summary>
        /// This event is fired when an subscribed-to instrument is found in EZAPI
        /// </summary>
        //public event InstrumentFoundHandler OnInstrumentFound;
        /// <summary>
        /// This event is fired when the inside market is updated for any subscribed-to instrument in EZAPI
        /// </summary>
        //public event InsideMarketHandler OnInsideMarketUpdate;
        /// <summary>
        /// This event is fired when a book depth update occurs for any subscribed-to instrument in EZAPI
        /// </summary>
        /// <remarks>This event is only fired if SubscribeMarketDepth is set to true</remarks>
        //public event MarketDepthHandler OnMarketDepthUpdate;
        /// <summary>
        /// This event is fired when a fill occurs in EZAPI
        /// </summary>
        //public event FillHandler OnFill;
        /// <summary>
        /// This event is fired when an order event occurs in EZAPI (order added, deleted, etc.)
        /// </summary>
        //public event OrderHandler OnOrder;
        /// <summary>
        /// A system message event sends information like session rollovers, fill book start/end, etc.
        /// </summary>
        //public event SystemMessageHandler OnSystemMessage;
        /// <summary>
        /// This event is fired when a time and sales event occurs
        /// </summary>
        /// <remarks>These events are fired only if the SubscribeTimeAndSales is set to true</remarks>
        //public event TimeAndSalesHandler OnTimeAndSales;

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
        public List<TTInstrument> Instruments { get { return ttInstruments.Values.ToList<TTInstrument>(); } }
        /// <summary>
        /// Retrieve all orders in EZAPI
        /// </summary>
        /// <remarks>This property will likely not be used much compared to WorkingOrders</remarks>
        public List<TTOrder> Orders { get { return ttOrders.Values.ToList<TTOrder>(); } }
        /// <summary>
        /// Retrieve all working orders in EZAPI
        /// </summary>
        public List<TTOrder> WorkingOrders { get { return workingTTOrders.Values.ToList<TTOrder>(); } }
        /// <summary>
        /// Retrieve all fills in EZAPI
        /// </summary>
        /// <remarks>This property will likely not be used much compared to CurrentFills</remarks>
        public List<TTFill> Fills { get { return ttFills.Values.ToList<TTFill>(); } }
        /// <summary>
        /// Retrieve all current fills in EZAPI (with amends, etc. already accounted for)
        /// </summary>
        public List<TTFill> CurrentFills { get { return currentTTFills.Values.ToList<TTFill>(); } }
        /// <summary>
        /// Retrieve the market depth for all instruments in EZAPI
        /// </summary>


        #region EVENTS
        public event InstrumentFoundHandler OnInstrumentFound
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                _onInstrumentFound = (InstrumentFoundHandler)Delegate.Combine(_onInstrumentFound, value);
                EventInstruments.Instance.OnInstrumentFound += value;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                _onInstrumentFound = (InstrumentFoundHandler)Delegate.Remove(_onInstrumentFound, value);
                EventInstruments.Instance.OnInstrumentFound -= value;
            }
        }

        public event InsideMarketHandler OnInsideMarketUpdate
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                _onInsideMarketUpdate = (InsideMarketHandler)Delegate.Combine(_onInsideMarketUpdate, value);
                EventInstruments.Instance.OnInsideMarketUpdate += value;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                _onInsideMarketUpdate = (InsideMarketHandler)Delegate.Remove(_onInsideMarketUpdate, value);
                EventInstruments.Instance.OnInsideMarketUpdate -= value;
            }
        }

        public event MarketDepthHandler OnMarketDepthUpdate
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                _onMarketDepthUpdate = (MarketDepthHandler)Delegate.Combine(_onMarketDepthUpdate, value);
                EventInstruments.Instance.OnMarketDepthUpdate += value;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                _onMarketDepthUpdate = (MarketDepthHandler)Delegate.Remove(_onMarketDepthUpdate, value);
                EventInstruments.Instance.OnMarketDepthUpdate -= value;
            }
        }

        public event TimeAndSalesHandler OnTimeAndSales
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                _onTimeAndSales = (TimeAndSalesHandler)Delegate.Combine(_onTimeAndSales, value);
                EventInstruments.Instance.OnTimeAndSales += value;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                _onTimeAndSales = (TimeAndSalesHandler)Delegate.Remove(_onTimeAndSales, value);
                EventInstruments.Instance.OnTimeAndSales -= value;
            }
        }

        public event OrderHandler OnOrder
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                _onOrder = (OrderHandler)Delegate.Combine(_onOrder, value);
                EventOrders.Instance.OnOrder += value;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                _onOrder = (OrderHandler)Delegate.Remove(_onOrder, value);
                EventOrders.Instance.OnOrder -= value;
            }
        }

        public event FillHandler OnFill
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                _onFill = (FillHandler)Delegate.Combine(_onFill, value);
                EventFills.Instance.OnFill += value;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                _onFill = (FillHandler)Delegate.Remove(_onFill, value);
                EventFills.Instance.OnFill -= value;
            }
        }

        public event SystemMessageHandler OnSystemMessage
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                _onSystemMessage = (SystemMessageHandler)Delegate.Combine(_onSystemMessage, value);
                EventInstruments.Instance.OnSystemMessage += value;
                EventOrders.Instance.OnSystemMessage += value;
                EventFills.Instance.OnSystemMessage += value;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                _onSystemMessage = (SystemMessageHandler)Delegate.Remove(_onSystemMessage, value);
                EventInstruments.Instance.OnSystemMessage -= value;
                EventOrders.Instance.OnSystemMessage -= value;
                EventFills.Instance.OnSystemMessage -= value;
            }
        }

        private InstrumentFoundHandler _onInstrumentFound;
        private InsideMarketHandler _onInsideMarketUpdate;
        private MarketDepthHandler _onMarketDepthUpdate;
        private TimeAndSalesHandler _onTimeAndSales;
        private OrderHandler _onOrder;
        private FillHandler _onFill;
        private SystemMessageHandler _onSystemMessage;
        #endregion

        #region PRIVATE
        private XTraderModeTTAPI XTraderApiInstance = null;
        private UniversalLoginTTAPI UniversalApiInstance = null;
        private Session apiSession = null;
        private AutospreaderManager autospreaderManager = null;
        private bool disposed = false;
        private WorkerDispatcher disp = null;
        private bool initSuccess = true;
        private string initMessage = "";
        
        private string ttUsername;
        private string ttPassword;
        private bool autoSubscribeInstruments;
        private bool subscribeMarketDepth;
        private bool subscribeTimeAndSales;

        private const string VERSION = "v1.0";
        private const bool DEBUG = true;

        //private EventInstruments instrumentEvents;
        //private EventOrders orderEvents;
        //private EventFills fillEvents;

        private Dictionary<InstrumentKey, TTInstrument> ttInstruments = new Dictionary<InstrumentKey, TTInstrument>();
        private Dictionary<string, TTOrder> ttOrders = new Dictionary<string, TTOrder>();
        private Dictionary<string, TTOrder> workingTTOrders = new Dictionary<string, TTOrder>();
        private Dictionary<string, TTFill> ttFills = new Dictionary<string, TTFill>();
        private Dictionary<string, TTFill> currentTTFills = new Dictionary<string, TTFill>();
        private Dictionary<string, TTOrder> sentOrders = new Dictionary<string, TTOrder>();
        private Dictionary<string, TTInstrument> sentInstrumentRequests = new Dictionary<string, TTInstrument>();
        private Dictionary<InstrumentTradeSubscription, TTInstrument> tradeSubscriptions = new Dictionary<InstrumentTradeSubscription,TTInstrument>();
        private Dictionary<string, OrderRouteInfo> orderRouteInfo = new Dictionary<string, OrderRouteInfo>();

        private Dictionary<string, List<Fill>> hashSymbols = new Dictionary<string, List<Fill>>();
        #endregion

        /// <summary>
        /// Construct an empty TTAPIFunctions object
        /// </summary>
        /// <param name="autoSubscribeInstruments">set to true to automatically subscribe to instruments in fills and orders</param>
        /// <param name="subscribeBookDepth">set to true to subscribe to book depth (false for inside market only)</param>
        /// <param name="subscribeBookDepth">set to true to subscribe to time and sales</param>
        /// <remarks>In XTrader mode, no username or password are required</remarks>
        public TTAPIFunctions(bool autoSubscribeInstruments, bool subscribeMarketDepth, bool subscribeTimeAndSales)
        {
            this.autoSubscribeInstruments = autoSubscribeInstruments;
            this.subscribeMarketDepth = subscribeMarketDepth;
            this.subscribeTimeAndSales = subscribeTimeAndSales;
        }

        /// <summary>
        /// Construct a TTAPIFunctions object using the specified username/password
        /// </summary>
        /// <param name="autoSubscribeInstruments">set to true to automatically subscribe to instruments in fills and orders</param>
        /// <param name="subscribeBookDepth">set to true to subscribe to book depth (false for inside market only)</param>
        /// <param name="subscribeBookDepth">set to true to subscribe to time and sales</param>
        /// <param name="username">TT username to use for this TTAPI functions object</param>
        /// <param name="password">TT password to use for this TTAPI functions object</param>
        /// <remarks>In Universal Login mode, a TT username and password are required</remarks>
        public TTAPIFunctions(bool autoSubscribeInstruments, bool subscribeMarketDepth, bool subscribeTimeAndSales, string username, string password)
        {
            this.autoSubscribeInstruments = autoSubscribeInstruments;
            this.subscribeMarketDepth = subscribeMarketDepth;
            this.subscribeTimeAndSales = subscribeTimeAndSales;
            ttUsername = username;
            ttPassword = password;
        }

        #region TTAPI Initialization and Shutdown
        /// <summary>
        /// Shutdown and clean up the TTAPI
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
                    // Shutdown the Dispatcher
                    if (disp != null)
                    {
                        disp.BeginInvokeShutdown();
                        disp = null;
                    }

                    // Shutdown the TTAPI
                    if (UniversalApiInstance != null)
                    {
                        UniversalApiInstance.Shutdown();
                        UniversalApiInstance = null;
                    }
                    else if (XTraderApiInstance != null)
                    {
                        XTraderApiInstance.Shutdown();
                        XTraderApiInstance = null;
                    }
                }
            }

            disposed = true;
        }

        #region START XTRADER MODE
        /// <summary>
        /// Start TTAPI in XTrader mode
        /// </summary>
        public void StartXTMode()
        {
            // Attach a WorkerDispatcher to the current thread
            disp = Dispatcher.AttachWorkerDispatcher();
            disp.BeginInvoke(new Action(InitXTMode));
            disp.Run();
        }

        /// <summary>
        /// Initialize the TTAPI in XTrader mode
        /// </summary>
        public void InitXTMode()
        {
            // Use "XTrader Mode Login" login mode
            TTAPI.XTraderModeDelegate xtDelegate = new TTAPI.XTraderModeDelegate(ttApiXTModeInitComplete);
            TTAPI.CreateXTraderModeTTAPI(Dispatcher.Current, xtDelegate);
        }

        /// <summary>
        /// Notifies us when initialization is complete (and whether it was successful)
        /// </summary>
        /// <param name="api">API object to use (XTrader mode)</param>
        /// <param name="ex">Exception that occurred (if any)</param>
        public void ttApiXTModeInitComplete(XTraderModeTTAPI api, Exception ex)
        {
            if (ex == null)
            {
                XTraderApiInstance = api;
                XTraderApiInstance.ConnectionStatusUpdate += new EventHandler<ConnectionStatusUpdateEventArgs>(apiInstance_ConnectionStatusUpdate);
                XTraderApiInstance.ConnectToXTrader();
                apiSession = XTraderApiInstance.Session;
                autospreaderManager = XTraderApiInstance.AutospreaderManager;
            }
            else
            {
                initSuccess = false;
                initMessage = "API Initialization Failed: " + ex.Message;
            }
        }

        void apiInstance_ConnectionStatusUpdate(object sender, ConnectionStatusUpdateEventArgs e)
        {
            if (!e.Status.IsSuccess)
            {
                initSuccess = false;
                initMessage = "Login failed: " + e.Status.StatusMessage;
            }

            AppStart();
        }
        #endregion

        #region START UNIVERSAL LOGIN
        /// <summary>
        /// Start TTAPI in Universal Login mode
        /// </summary>
        public void StartUniversal()
        {
            // Attach a WorkerDispatcher to the current thread
            disp = Dispatcher.AttachWorkerDispatcher();
            disp.BeginInvoke(new Action(Init));
            disp.Run();
        }

        /// <summary>
        /// Initialize TTAPI in Universal Login mode
        /// </summary>
        public void Init()
        {
            // Use "Universal Login" login mode
            TTAPI.UniversalLoginModeDelegate ulDelegate = new TTAPI.UniversalLoginModeDelegate(ttApiInitComplete);
            TTAPI.CreateUniversalLoginTTAPI(Dispatcher.Current, ulDelegate);
        }

        /// <summary>
        /// Notifies us when initialization is complete (and whether it was successful)
        /// </summary>
        /// <param name="api">TTAPI object to use (Universal Login mode)</param>
        /// <param name="ex">Exception that occurred during initialization (if any)</param>
        public void ttApiInitComplete(UniversalLoginTTAPI api, Exception ex)
        {
            if (ex == null)
            {
                UniversalApiInstance = api;
                UniversalApiInstance.AuthenticationStatusUpdate += new EventHandler<AuthenticationStatusUpdateEventArgs>(apiInstance_AuthenticationStatusUpdate);
                UniversalApiInstance.Authenticate(ttUsername, ttPassword);
                apiSession = UniversalApiInstance.Session;
                autospreaderManager = UniversalApiInstance.AutospreaderManager;
            }
            else
            {
                initSuccess = false;
                initMessage = "API Initialization Failed: " + ex.Message;
            }
        }

        void apiInstance_AuthenticationStatusUpdate(object sender, AuthenticationStatusUpdateEventArgs e)
        {
            if (!e.Status.IsSuccess)
            {
                initSuccess = false;
                initMessage = "Login failed: " + e.Status.StatusMessage;
            }

            AppStart();
        }
        #endregion
        #endregion

        private void AppStart()
        {
            if (initSuccess)
            {
                while (apiSession == null)
                    Thread.Sleep(100);
                CreateInstrumentThread();
                CreateOrderThread();
                CreateFillThread();
                CreateMarketThread();

                while (EventOrders.Instance.Dispatcher == null)
                    Thread.Sleep(100);
                EventOrders.Instance.CreateOrderSubscription();
                while (EventFills.Instance.Dispatcher == null)
                    Thread.Sleep(100);
                EventFills.Instance.CreateFillSubscription();
            }

            if (OnInitialize != null) OnInitialize(initSuccess, initMessage);
        }

        private void CreateInstrumentThread()
        {
            Thread t = new Thread(() =>
            {
                EventInstruments.Instance.APISession = apiSession;
                EventInstruments.Instance.TTInstruments = ttInstruments;
                EventInstruments.Instance.SentInstrumentRequests = sentInstrumentRequests;
                EventInstruments.Instance.Start();
            });
            t.Start();
        }

        private void CreateOrderThread()
        {
            Thread t = new Thread(() =>
            {
                EventOrders.Instance.APISession = apiSession;
                EventOrders.Instance.TTInstruments = ttInstruments;
                EventOrders.Instance.TTOrders = ttOrders;
                EventOrders.Instance.WorkingTTOrders = workingTTOrders;
                EventOrders.Instance.SentOrders = sentOrders;
                EventOrders.Instance.OrderRouteInfo = orderRouteInfo;
                EventOrders.Instance.AutoSubscribeInstruments = AutoSubscribeInstruments;
                EventOrders.Instance.Start();
            });
            t.Start();
        }

        private void CreateFillThread()
        {
            Thread t = new Thread(() =>
            {
                EventFills.Instance.APISession = apiSession;
                EventFills.Instance.TTInstruments = ttInstruments;
                EventFills.Instance.TTFills = ttFills;
                EventFills.Instance.CurrentTTFills = currentTTFills;
                EventFills.Instance.HashSymbols = hashSymbols;
                EventFills.Instance.AutoSubscribeInstruments = AutoSubscribeInstruments;
                EventFills.Instance.Start();
            });
            t.Start();

        }

        private void CreateMarketThread()
        {
            Thread t = new Thread(() =>
            {
                EventMarkets.Instance.APISession = apiSession;
                EventMarkets.Instance.TTInstruments = ttInstruments;
                //EventMarkets.Instance.TTFills = ttFills;
                //EventMarkets.Instance.CurrentTTFills = currentTTFills;
                //EventMarkets.Instance.HashSymbols = hashSymbols;
                EventMarkets.Instance.AutoSubscribeInstruments = AutoSubscribeInstruments;
                EventMarkets.Instance.Start();
            });
            t.Start();
        }

        /// <summary>
        /// Retrieve an instrument (TTInstrument) given an InstrumentKey
        /// </summary>
        /// <param name="key">Instrument key to look up</param>
        /// <returns>TTInstrument object associated with the specified InstrumentKey</returns>
        public TTInstrument GetInstrument(InstrumentKey key)
        {
            TTInstrument result = null;
            if (ttInstruments.ContainsKey(key))
                result = ttInstruments[key];

            return result;
        }

        /// <summary>
        /// Retrieve a TTInstrument whose name contains the string value passed.
        /// </summary>
        /// <param name="search">text to look for in the instrument name (i.e. 'CL' or 'CL Nov12')</param>
        /// <returns>a TTInstrument whose name contains your text (if one exists)</returns>
        public TTInstrument GetInstrument(string search)
        {
            TTInstrument result = null;
            foreach (TTInstrument tti in ttInstruments.Values)
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
        public List<TTOrder> GetOrders(InstrumentKey key)
        {
            List<TTOrder> results = new List<TTOrder>();

            foreach (TTOrder order in ttOrders.Values)
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
        public List<TTFill> GetFills(InstrumentKey key)
        {
            List<TTFill> results = new List<TTFill>();

            foreach (TTFill fill in ttFills.Values)
            {
                if (fill.InstrumentKey == key)
                    results.Add(fill);
            }
            return results;
        }

        #region METHODS TO SUBSCRIBE TO INSTRUMENTS
        /// <summary>
        /// Subscribe to an instrument using an InstrumentDescriptor
        /// </summary>
        /// <param name="descriptor">InstrumentDescriptor containing the instrument information</param>
        public void SubscribeToInstrument(InstrumentDescriptor descriptor)
        {
            EventInstruments.Instance.SubscribeToInstrument(descriptor);
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
        public void SubscribeToInstrument(InstrumentKey key)
        {
            EventInstruments.Instance.SubscribeToInstrument(key);
  
            /*InstrumentLookupSubscription req = new InstrumentLookupSubscription(apiSession, Dispatcher.Current, key);
            req.Update += new EventHandler<InstrumentLookupSubscriptionEventArgs>(instrumentLookupRequest_Update);
            req.Start();*/
        }

        public TTInstrument CreateInstrument(string instrumentName)
        {
            TTInstrument tti = null;

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

        public TTInstrument CreateInstrument(string marketName, string productTypeName, string productName, string contractName)
        {
            string tag = TTHelper.GetUniqueTag();
            EventInstruments.Instance.CreateInstrument(marketName, productTypeName, productName, contractName, tag);
            /*InstrumentDescriptor descriptor = new InstrumentDescriptor(marketName, productTypeName, productName, contractName);
            string tag = TTHelper.GetUniqueTag();
            SubscribeToInstrument(descriptor, tag);*/

            TTInstrument tti = null;
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
        #endregion

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

        #region SEND ORDERS
        public void SetOrderRouteInfo(string marketName, string orderFeedName, string accountTypeName, string accountName)
        {
            OrderRouteInfo info = new OrderRouteInfo();
            info.MarketName = marketName;
            info.OrderFeedName = orderFeedName;
            info.AccountTypeName = accountTypeName;
            info.AccountName = accountName;

            orderRouteInfo[marketName] = info;
        }

        public TTOrder BuyLimit(TTInstrument tti, int quantity, double price)
        {
            Instrument instr = tti.TTAPI_Instrument;
            return SendOrder(tti, BuySell.Buy, OrderType.Limit, Quantity.FromInt(instr, quantity), Price.FromDouble(instr, price));
        }

        public TTOrder SellLimit(TTInstrument tti, int quantity, double price)
        {
            Instrument instr = tti.TTAPI_Instrument;
            return SendOrder(tti, BuySell.Sell, OrderType.Limit, Quantity.FromInt(instr, quantity), Price.FromDouble(instr, price));
        }

        public TTOrder BuyMarket(TTInstrument tti, int quantity)
        {
            Instrument instr = tti.TTAPI_Instrument;
            return SendOrder(tti, BuySell.Buy, OrderType.Market, Quantity.FromInt(instr, quantity), Price.Invalid);
        }

        public TTOrder SellMarket(TTInstrument tti, int quantity)
        {
            Instrument instr = tti.TTAPI_Instrument;
            return SendOrder(tti, BuySell.Sell, OrderType.Market, Quantity.FromInt(instr, quantity), Price.Invalid);
        }

        public TTOrder SendOrder(TTInstrument instrument, BuySell buySell, OrderType orderType, Quantity quantity, Price price)
        {
            TTOrder tto = null;

            OrderRoute orderRoute = instrument.OrderRoute;

            // If the order route has not been set for this instrument, see if there is
            // an OrderRouteInfo for this market (user sets using SetOrderRouteInfo method)
            if (instrument.OrderRoute == null && orderRouteInfo.ContainsKey(instrument.Market.Name))
            {
                OrderRouteInfo info = orderRouteInfo[instrument.Market.Name];
                string orderFeedName = info.OrderFeedName;
                AccountType accountType = (AccountType)Enum.Parse(typeof(AccountType), info.AccountTypeName);
                string accountName = info.AccountName;
                OrderFeed feedToUse = null;
                foreach (OrderFeed feed in instrument.EnabledOrderFeeds)
                {
                    if (feed.Name.Equals(orderFeedName))
                    {
                        feedToUse = feed;
                        break;
                    }
                }
                orderRoute = new OrderRoute(feedToUse, accountType, accountName);
            }

            OrderProfile prof = new OrderProfile(orderRoute.OrderFeed, instrument.TTAPI_Instrument);
            prof.BuySell = buySell;
            prof.OrderType = orderType;
            prof.OrderQuantity = quantity;
            prof.LimitPrice = price;
            prof.AccountType = orderRoute.AccountType;
            prof.AccountName = orderRoute.AccountName;
            string tag = TTHelper.GetUniqueTag();
            prof.OrderTag = tag;
            instrument.TTAPI_Instrument.Session.SendOrder(prof);

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

            return tto;
        }

        void tto_OnOrderModify(TTModify modify, TTOrder order, Price price, Quantity quantity)
        {
            switch (modify)
            {
                case TTModify.Price:
                    ModifyPrice(order, price);
                    break;
                case TTModify.Quantity:
                    ModifyQuantity(order, quantity);
                    break;
            }
        }

        public void ModifyPrice(TTOrder order, Price price)
        {
            TTOrder tto = null;
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

        public void ModifyQuantity(TTOrder order, Quantity quantity)
        {
            TTOrder tto = null;
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
        #endregion

        /// <summary>
        /// Cancel all working orders in EZAPI
        /// </summary>
        public void CancelAllOrders()
        {
            foreach (TTOrder order in workingTTOrders.Values)
                order.Cancel();
        }

        /// <summary>
        /// Hold or "remove-from-hold" all working orders in EZAPI
        /// </summary>
        /// <param name="hold"></param>
        public void HoldAllOrders(bool hold)
        {
            foreach (TTOrder order in workingTTOrders.Values)
                order.Hold(hold);
        }


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

        string FillToString(Fill fill)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" " + fill.BuySell.ToString() + " ");
            sb.Append(" " + fill.Quantity.ToInt().ToString() + " ");
            sb.Append(" " + fill.InstrumentKey + " ");
            sb.Append("@ " + fill.MatchPrice + " ");
            return (sb.ToString());
        }

        string OrderToString(Order order)
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


    } // class
} // namespace

