using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using EZAPI.Containers;

namespace EZAPI
{
    class EventInstruments
    {
        public event InstrumentFoundHandler OnInstrumentFound;
        public event InsideMarketHandler OnInsideMarketUpdate;
        public event MarketDepthHandler OnMarketDepthUpdate;
        public event TimeAndSalesHandler OnTimeAndSales;
        public event SystemMessageHandler OnSystemMessage;

        public static EventInstruments Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new EventInstruments();
                return _instance;
            }
        }
        public bool SubscribeMarketDepth { get; set; }
        public bool SubscribeTimeAndSales { get; set; }
        public Session APISession { get { return _apiSession; } set { _apiSession = value; } }
        public Dictionary<InstrumentKey, TTInstrument> TTInstruments { get { return _ttInstruments; } set { _ttInstruments = value; } }
        public Dictionary<string, TTInstrument> SentInstrumentRequests { get { return SentInstrumentRequests; } set { _sentInstrumentRequests = value; } }
        public Dispatcher Dispatcher { get { return _dispatcher; } }

        Dictionary<InstrumentKey, TTInstrument> _ttInstruments;
        private Dictionary<string, TTInstrument> _sentInstrumentRequests;
        private Session _apiSession;
        private WorkerDispatcher _dispatcher = null;

        private static EventInstruments _instance;

        private EventInstruments()
        {
        }

        public void Start()
        {
            Console.WriteLine("Starting instrument event thread...");

            // Attach a WorkerDispatcher to the current thread
            _dispatcher = Dispatcher.AttachWorkerDispatcher();
            _dispatcher.Run();
        }

        public void SubscribeToInstrument(InstrumentDescriptor descriptor)
        {
            if (_dispatcher.InvokeRequired())
            {
                _dispatcher.BeginInvoke(() =>
                {
                    SubscribeToInstrument(descriptor);
                });
                return;
            }

            InstrumentLookupSubscription req = new InstrumentLookupSubscription(_apiSession, Dispatcher.Current, descriptor.ProductKey, descriptor.ContractName);
            req.Update += new EventHandler<InstrumentLookupSubscriptionEventArgs>(instrumentLookupRequest_Update);
            req.Start();
        }

        public void SubscribeToInstrument(InstrumentKey key)
        {
            if (_dispatcher.InvokeRequired())
            {
                _dispatcher.BeginInvoke(() =>
                {
                    SubscribeToInstrument(key);
                });
                return;
            }

            InstrumentLookupSubscription req = new InstrumentLookupSubscription(_apiSession, Dispatcher.Current, key);
            req.Update += new EventHandler<InstrumentLookupSubscriptionEventArgs>(instrumentLookupRequest_Update);
            req.Start();
            Console.WriteLine("SUBSCRIBETOINSTRUMENT REQUEST: " + key.ToString());
        }

        public void CreateInstrument(string marketName, string productTypeName, string productName, string contractName, string tag)
        {
            if (_dispatcher.InvokeRequired())
            {
                _dispatcher.BeginInvoke(() =>
                {
                    CreateInstrument(marketName, productTypeName, productName, contractName, tag);
                });
                return;
            }
         
            InstrumentDescriptor descriptor = new InstrumentDescriptor(marketName, productTypeName, productName, contractName);
            InstrumentLookupSubscription req = new InstrumentLookupSubscription(_apiSession, Dispatcher.Current, descriptor.ProductKey, descriptor.ContractName);
            req.Update += new EventHandler<InstrumentLookupSubscriptionEventArgs>(instrumentLookupRequest_Update);
            req.Tag = tag;  // Tag our request with our unique tag string
            req.Start();
            Console.WriteLine("CREATEINSTRUMENT REQUEST: " + descriptor.ToString());
        }

        void instrumentLookupRequest_Update(object sender, InstrumentLookupSubscriptionEventArgs e)
        {
            TTInstrument tti;

            Console.WriteLine("ILR_Update on Thread");
            if (e.Instrument != null && e.Error == null)
            {
                Console.WriteLine("SUBSCRIBED on thread: " + e.Instrument.Name);

                // successfully subscribed to an instrument so request prices
                if (e.Instrument.Key.IsAutospreader)
                    tti = processSpreadFound(e.Instrument);
                else
                {
                    tti = processInstrumentFound(e.Instrument);

                    // If this instrument request was tagged, then put this instrument request
                    // in sentInstrumentRequests with the tag as the key
                    InstrumentLookupSubscription instrumentRequest = sender as InstrumentLookupSubscription;
                    string tag = instrumentRequest.Tag as string;
                    if (tag != null && !tag.Equals(""))
                        _sentInstrumentRequests[tag] = tti;
                }

                if (OnInstrumentFound != null) OnInstrumentFound(tti, true);
            }
            else if (e.IsFinal)
            {
                Console.WriteLine("{0} {1}", e.RequestInfo.IsByName, e.RequestInfo.InstrumentName);
                // Instrument was not found and TTAPI has given up looking for it
                if (e.Instrument == null)
                    tti = null;
                else
                    tti = new TTInstrument(e.Instrument);
                if (OnInstrumentFound != null) OnInstrumentFound(tti, false);
            }
        }

        #region PROCESS NEW INSTRUMENTS AND SPREADS
        TTInstrument processInstrumentFound(Instrument instrument)
        {
            Dispatcher dispatcher = Dispatcher.Current;
            //Dispatcher dispatcher = _dispatcher;

            PriceSubscription priceSub = new PriceSubscription(instrument, dispatcher);
            if (SubscribeMarketDepth == true)
                priceSub.Settings = new PriceSubscriptionSettings(PriceSubscriptionType.MarketDepth);
            else
                priceSub.Settings = new PriceSubscriptionSettings(PriceSubscriptionType.InsideMarket);
            priceSub.FieldsUpdated += new FieldsUpdatedEventHandler(priceSub_FieldsUpdated);
            priceSub.Start();

            InstrumentTradeSubscription its = new InstrumentTradeSubscription(_apiSession, dispatcher, instrument);
            its.EnablePNL = true;
            its.ProfitLossChanged += new EventHandler<ProfitLossChangedEventArgs>(its_ProfitLossChanged);
            its.Start();

            if (SubscribeTimeAndSales == true)
            {
                TimeAndSalesSubscription tsSub = new TimeAndSalesSubscription(instrument, dispatcher);
                tsSub.Update += new EventHandler<TimeAndSalesEventArgs>(tsSub_Update);
                tsSub.Start();
            }

            TTInstrument tti = NewTTInstrument(instrument);
            tti.TradeSubscription = its;
            its.Tag = tti;

            tti.OrderRoute = OrderRoute.GetOrderRoute(tti, instrument.Product.Market.Name);

            return tti;
        }

        void tsSub_Update(object sender, TimeAndSalesEventArgs e)
        {
            if (e.Error == null)
            {
                TTInstrument tti = _ttInstruments[e.Instrument.Key];
                Price ltp;
                Quantity ltq;
                DateTime timestamp;
                TradeDirection direction;
                bool isOTC;
                foreach (TimeAndSalesData tsData in e.Data)
                {
                    timestamp = tsData.TimeStamp;
                    ltp = tsData.TradePrice;
                    ltq = tsData.TradeQuantity;
                    direction = tsData.Direction;
                    isOTC = tsData.IsOverTheCounter;
                    if (OnTimeAndSales != null) OnTimeAndSales(tti, timestamp, ltp, ltq, direction, isOTC);
                }
            }
        }

        TTSpread processSpreadFound(Instrument instrument)
        {
            PriceSubscription priceSub = new PriceSubscription(instrument, Dispatcher.Current);
            priceSub.Settings = new PriceSubscriptionSettings(PriceSubscriptionType.InsideMarket);
            priceSub.FieldsUpdated += new FieldsUpdatedEventHandler(priceSub_FieldsUpdated);
            priceSub.Start();

            ASInstrumentTradeSubscription its = new ASInstrumentTradeSubscription(_apiSession, Dispatcher.Current, instrument as AutospreaderInstrument);
            its.EnablePNL = true;
            its.ProfitLossChanged += new EventHandler<ProfitLossChangedEventArgs>(its_ProfitLossChanged);
            its.Start();

            TTSpread tts = NewTTSpread(instrument);
            tts.TradeSubscription = its;

            tts.OrderRoute = OrderRoute.GetOrderRoute(tts, instrument.Product.Market.Name);

            return tts;
        }
        #endregion

        #region CREATE NEW TTObjects
        private TTInstrument NewTTInstrument(Instrument instrument)
        {
            TTInstrument tti;
            if (_ttInstruments.ContainsKey(instrument.Key))
                tti = _ttInstruments[instrument.Key];
            else
            {
                tti = new TTInstrument(instrument);
                _ttInstruments.Add(instrument.Key, tti);
            }
            // Initialize any TTInstrument fields here:
            tti.TTAPI_Instrument = instrument;
            tti.Name = instrument.Name;

            return tti;
        }

        private TTSpread NewTTSpread(Instrument instrument)
        {
            TTSpread tts;
            if (_ttInstruments.ContainsKey(instrument.Key))
                tts = _ttInstruments[instrument.Key] as TTSpread;
            else
            {
                tts = new TTSpread(instrument);
                _ttInstruments.Add(instrument.Key, tts);
            }
            // Initialize any TTSpread fields here:
            tts.TTAPI_Instrument = instrument;
            tts.Name = instrument.Name;

            return tts;
        }
        #endregion

        void priceSub_FieldsUpdated(object sender, FieldsUpdatedEventArgs e)
        {
            if (e.Error == null)
            {
                // A full snapshot update occurs when the TTAPI loses and restores its connection
                // with a TT Gateway or associated Exchange.
                // If you modify the ProductSubscription.Settings property, the TTAPI automatically
                // stops and restarts the subscription with the new settings. The TTAPI does not deliver
                // updates during the restart process.
                /*
                if (e.UpdateType == UpdateType.Snapshot)
                    ;
                else
                    e.UpdateType == UpdateType.Update;
                */

                InstrumentKey key = e.Fields.Instrument.Key;
                TTInstrument tti = _ttInstruments[key];
                tti.Bid = e.Fields.GetDirectBidPriceField().Value;
                tti.BidQty = e.Fields.GetDirectBidQuantityField().Value;
                tti.Ask = e.Fields.GetDirectAskPriceField().Value;
                tti.AskQty = e.Fields.GetDirectAskQuantityField().Value;
                tti.Last = e.Fields.GetLastTradedPriceField().Value;
                tti.LastQty = e.Fields.GetLastTradedQuantityField().Value;
                tti.Open = e.Fields.GetOpenPriceField().Value;
                tti.High = e.Fields.GetHighPriceField().Value;
                tti.Low = e.Fields.GetLowPriceField().Value;
                tti.Close = e.Fields.GetClosePriceField().Value;
                tti.NetChange = e.Fields.GetNetChangeField().Value;
                tti.NetChangePercent = e.Fields.GetNetChangePercentField().Value;
                tti.Volume = e.Fields.GetTotalTradedQuantityField().Value;

                if (OnInsideMarketUpdate != null) OnInsideMarketUpdate(tti);

                if (SubscribeMarketDepth == true)
                {
                    if (tti.MarketDepth == null)
                    {
                        int maxDepthCount = e.Fields.GetLargestCurrentDepthLevel();

                        tti.MarketDepth = new TTMarketDepth(maxDepthCount);
                    }

                    for (int i = 0; i < tti.MarketDepth.DepthCount; i++)
                    {
                        tti.MarketDepth[i].Bid = e.Fields.GetDirectBidPriceField(i).Value;
                        tti.MarketDepth[i].BidQty = e.Fields.GetDirectBidQuantityField(i).Value;
                        tti.MarketDepth[i].Ask = e.Fields.GetDirectAskPriceField(i).Value;
                        tti.MarketDepth[i].AskQty = e.Fields.GetDirectAskQuantityField(i).Value;
                    }

                    if (OnMarketDepthUpdate != null) OnMarketDepthUpdate(_ttInstruments[key], tti.MarketDepth);
                }
            }
            else
            {
                if (e.Error.IsRecoverableError == false)
                {
                    if (OnSystemMessage != null) OnSystemMessage(SystemMessage.UNRECOVERABLE_PRICE_ERROR);
                }
            }
        }

        void its_ProfitLossChanged(object sender, ProfitLossChangedEventArgs e)
        {
            if (e.TradeSubscription.Tag != null)
            {
                TTInstrument tti = e.TradeSubscription.Tag as TTInstrument;
                tti.Profit = e.TradeSubscription.ProfitLoss.AsPrimaryCurrency;
            }
            //Message("PROFIT: " + string.Format("{0}", e.TradeSubscription.ProfitLoss));
        }


    } // class
} // namespace
