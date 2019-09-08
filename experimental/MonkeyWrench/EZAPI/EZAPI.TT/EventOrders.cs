using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using EZAPI.Containers;
using TradingTechnologies.TTAPI;
using TradingTechnologies.TTAPI.Tradebook;

namespace EZAPI
{
    class EventOrders
    {
        public event OrderHandler OnOrder;
        public event SystemMessageHandler OnSystemMessage;

        public static EventOrders Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new EventOrders();
                return _instance;
            }
        }
        public bool AutoSubscribeInstruments { get; set; }
        public Session APISession { get { return _apiSession; } set { _apiSession = value; } }
        public Dictionary<InstrumentKey, TTInstrument> TTInstruments { set { _ttInstruments = value; } }
        public Dictionary<string, TTOrder> TTOrders { set { _ttOrders = value; } }
        public Dictionary<string, TTOrder> WorkingTTOrders { set { _workingTTOrders = value; } }
        public Dictionary<string, TTOrder> SentOrders { set { _sentOrders = value; } }
        public Dictionary<string, OrderRouteInfo> OrderRouteInfo { set { _orderRouteInfo = value; } }
        public Dispatcher Dispatcher { get { return _dispatcher; } }

        private Dictionary<InstrumentKey, TTInstrument> _ttInstruments = new Dictionary<InstrumentKey, TTInstrument>();
        private Dictionary<string, TTOrder> _ttOrders = new Dictionary<string, TTOrder>();
        private Dictionary<string, TTOrder> _workingTTOrders = new Dictionary<string, TTOrder>();
        private Dictionary<string, TTOrder> _sentOrders = new Dictionary<string, TTOrder>();
        private Dictionary<string, OrderRouteInfo> _orderRouteInfo = new Dictionary<string, OrderRouteInfo>();
        private Session _apiSession;
        private WorkerDispatcher _dispatcher = null;

        private TradeSubscription _ts;

        private static EventOrders _instance;

        private EventOrders()
        {
        }

        public void Start()
        {
            Console.WriteLine("Starting order event thread...");

            // Attach a WorkerDispatcher to the current thread
            _dispatcher = Dispatcher.AttachWorkerDispatcher();
            _dispatcher.Run();
        }
        
        public void CreateOrderSubscription()
        {

            if (_dispatcher.InvokeRequired())
            {
                _dispatcher.BeginInvoke(() =>
                {
                    CreateOrderSubscription();
                });
                return;
            }

            Dispatcher dispatcher = Dispatcher.Current;

            Console.WriteLine("Creating Order Subscription...");

            _ts = new TradeSubscription(_apiSession, dispatcher);
            _ts.OrderBookDownload += new EventHandler<OrderBookDownloadEventArgs>(ts_OrderBookDownload);
            _ts.OrderAdded += new EventHandler<OrderAddedEventArgs>(ts_OrderAdded);
            _ts.OrderUpdated += new EventHandler<OrderUpdatedEventArgs>(ts_OrderUpdated);
            _ts.OrderDeleted += new EventHandler<OrderDeletedEventArgs>(ts_OrderDeleted);
            _ts.OrderFilled += new EventHandler<OrderFilledEventArgs>(ts_OrderFilled);
            _ts.OrderStatusUnknown += new EventHandler<OrderStatusUnknownEventArgs>(ts_OrderStatusUnknown);
            _ts.OrderRejected += new EventHandler<OrderRejectedEventArgs>(ts_OrderRejected);
            //ts.EnablePNL = true;
            //ts.ProfitLossChanged += new EventHandler<ProfitLossChangedEventArgs>(ts_ProfitLossChanged);
            _ts.Start();

            Console.WriteLine("Order Subscription created.");
        }

        #region TRADE SUBSCRIPTION ORDERS
        void ts_OrderRejected(object sender, OrderRejectedEventArgs e)
        {
            processOrder(TTOrderStatus.Rejected, e.Order);
        }

        void ts_OrderStatusUnknown(object sender, OrderStatusUnknownEventArgs e)
        {
            processOrder(TTOrderStatus.Unknown, e.Order);
        }

        void ts_OrderFilled(object sender, OrderFilledEventArgs e)
        {
            processOrder(TTOrderStatus.Filled, e.OldOrder);
        }

        void ts_OrderDeleted(object sender, OrderDeletedEventArgs e)
        {
            processOrder(TTOrderStatus.Deleted, e.OldOrder);
        }

        void ts_OrderUpdated(object sender, OrderUpdatedEventArgs e)
        {
            processOrder(TTOrderStatus.Updated, e.OldOrder, e.NewOrder);
        }

        void ts_OrderAdded(object sender, OrderAddedEventArgs e)
        {
            processOrder(TTOrderStatus.Added, e.Order);
        }

        void ts_OrderBookDownload(object sender, OrderBookDownloadEventArgs e)
        {
            //processOrder(TTOrderStatus.Added, e.Orders);
        }
        #endregion

        #region PROCESS ORDERS
        void processOrder(TTOrderStatus status, ReadOnlyCollection<Order> orders)
        {
            foreach (Order order in orders)
            {
                processOrder(status, order);
                Thread.Sleep(5);
            }

            processSystemMessage(SystemMessage.ORDER_BOOK_DOWNLOAD);
        }

        void processOrder(TTOrderStatus status, Order order, params Order[] orders)
        {
            TTOrder oldOrder = null;

            // If this order's instrument is not yet in our dictionary, then add the instrument
            // to our list and subscribe to the instrument updates if AutoSubscribeInstruments
            // is true.
            if (!_ttInstruments.ContainsKey(order.InstrumentKey))
            {
                if (AutoSubscribeInstruments)
                {
                    //TODO: figure out how to subscribe to instruments here
                    //SubscribeToInstrument(order.InstrumentKey);
                    /*InstrumentLookupSubscription ils = new InstrumentLookupSubscription(apiSession, Dispatcher.Current, order.InstrumentKey);
                    ils.Update += new EventHandler<InstrumentLookupSubscriptionEventArgs>(ils_Update);
                    ils.Start();*/
                }

                TTInstrument tti = new TTInstrument(order.InstrumentKey);
                _ttInstruments.Add(order.InstrumentKey, tti);
            }

            TTOrder tto;
            if (_workingTTOrders.ContainsKey(order.SiteOrderKey))
                tto = _workingTTOrders[order.SiteOrderKey];
            else
                tto = NewTTOrder(order);
            tto.Status = status;

            string orderKey = tto.Key;
            // Maintain our working orders
            switch (status)
            {
                case TTOrderStatus.Added:
                    _workingTTOrders[orderKey] = tto;
                    break;
                case TTOrderStatus.Deleted:
                case TTOrderStatus.Filled:
                case TTOrderStatus.Rejected:
                    _workingTTOrders.Remove(orderKey);
                    break;
                case TTOrderStatus.Updated:
                    oldOrder = tto.Clone();
                    tto.TTAPI_Order = orders[0];
                    //newOrder = NewTTOrder(orders[0]);
                    string newOrderKey = orders[0].SiteOrderKey;
                    _workingTTOrders.Remove(orderKey);
                    _workingTTOrders.Add(newOrderKey, tto);
                    System.Diagnostics.Debug.Assert(orderKey.Equals(newOrderKey));
                    break;
            }

            // When we send an order using the SendOrder method, the method creates a
            // unique OrderTag and then blocks waiting for that order to be "created".
            // It waits for it to appear in the sentOrders dictionary with the OrderTag
            // that was generated as the key. This is the only way I could deduce to
            // be able to return a TTOrder object to the caller of SendOrder since all
            // the order sending methods appear to be asynchronous.
            if (!order.OrderTag.Equals(""))
                _sentOrders[order.OrderTag] = tto;

            //Console.WriteLine("Working orders size: {0}", workingTTOrders.Count);
            if (OnOrder != null) OnOrder(status, _ttInstruments[order.InstrumentKey], tto, oldOrder);
        }
        #endregion

        #region CREATE NEW TTObjects
        private TTOrder NewTTOrder(Order order)
        {
            TTOrder tto;
            string key = order.SiteOrderKey;
            if (_ttOrders.ContainsKey(key))
                tto = _ttOrders[key];
            else
            {
                tto = new TTOrder(order);
                tto.OnOrderModify += new TTOrderModifyHandler(tto_OnOrderModify);
                //ttOrders.Add(key, tto);
                //Console.WriteLine("ttOrders size: {0}", ttOrders.Count);
            }
            // Initialize any TTOrder fields here:
            tto.Instrument = _ttInstruments[order.InstrumentKey];
            tto.Key = key;
            return tto;
        }
        #endregion

        #region SEND ORDERS
        public void SetOrderRouteInfo(string marketName, string orderFeedName, string accountTypeName, string accountName)
        {
            OrderRouteInfo info = new OrderRouteInfo();
            info.MarketName = marketName;
            info.OrderFeedName = orderFeedName;
            info.AccountTypeName = accountTypeName;
            info.AccountName = accountName;

            _orderRouteInfo[marketName] = info;
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
            if (instrument.OrderRoute == null && _orderRouteInfo.ContainsKey(instrument.Market.Name))
            {
                OrderRouteInfo info = _orderRouteInfo[instrument.Market.Name];
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
                if (_sentOrders.ContainsKey(tag))
                {
                    tto = _sentOrders[tag];
                    _sentOrders.Remove(tag);
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
                if (_sentOrders.ContainsKey(tag))
                {
                    tto = _sentOrders[tag];
                    _sentOrders.Remove(tag);
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
                if (_sentOrders.ContainsKey(tag))
                {
                    tto = _sentOrders[tag];
                    _sentOrders.Remove(tag);
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
            foreach (TTOrder order in _workingTTOrders.Values)
                order.Cancel();
        }

        /// <summary>
        /// Hold or "remove-from-hold" all working orders in EZAPI
        /// </summary>
        /// <param name="hold"></param>
        public void HoldAllOrders(bool hold)
        {
            foreach (TTOrder order in _workingTTOrders.Values)
                order.Hold(hold);
        }

        #region PROCESS SYSTEM MESSAGES
        void processSystemMessage(SystemMessage systemMessage)
        {
            if (OnSystemMessage != null) OnSystemMessage(systemMessage);
        }
        #endregion


    } // class
} // namespace
