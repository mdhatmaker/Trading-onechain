using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using EZAPI.Framework.Base;

namespace EZAPI.Framework
{
    public enum SystemMessage { TRADE_SUBSCRIPTION_RESET, ROLLOVER, FILL_LIST_START, FILL_LIST_END, ORDER_BOOK_DOWNLOAD, FILL_BOOK_DOWNLOAD, UNRECOVERABLE_PRICE_ERROR };

    /// <summary>
    /// This class encapsulates the information about a specific instrument (i.e. CME CL Nov12)
    /// </summary>
    /// <remarks>The TTSpread class is derived from the TTInstrument class</remarks>
    /// <seealso cref=">TTSpread"/>
    public class EZInstrument
    {
        /// <summary>
        /// Access the original TTAPI Instrument object
        /// </summary>
        public ezInstrument BaseInstrument { get; set; }
        /// <summary>
        /// Unique instrument key
        /// </summary>
        public ezInstrumentKey Key { get; set; }
        /// <summary>
        /// Text representing the instrument name (i.e. "CME CL Nov12")
        /// </summary>
        public string Name { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Stores the TradeSubscription for this instrument
        /// </summary>
        public ezInstrumentTradeSubscription TradeSubscription { get; set; }
        /// <summary>
        /// An InstrumentDescriptor is used to initially construct an Instrument to subscribe to
        /// </summary>
        public EZInstrumentDescriptor InstrumentDescriptor
        {
            get
            {
                if (_instrumentDescriptor != null)
                    return _instrumentDescriptor;
                else
                {
                    if (DescriptorString == null)
                        return null;

                    _instrumentDescriptor = new EZInstrumentDescriptor(DescriptorString);
                    return _instrumentDescriptor;
                }
            }
            set
            {
                _instrumentDescriptor = value;
            }
        }
        /// <summary>
        /// Bid price of this instrument's inside market (best bid)
        /// </summary>
        public ezPrice Bid { get; set; }
        /// <summary>
        ///  Bid quantity of this instrument's inside market (best bid quantity)
        /// </summary>
        public ezQuantity BidQty { get; set; }
        /// <summary>
        /// Ask price of this instrument's inside market (best offer)
        /// </summary>
        public ezPrice Ask { get; set; }
        /// <summary>
        /// Ask quantity of this instrument's inside market (best offer quantity)
        /// </summary>
        public ezQuantity AskQty { get; set; }
        /// <summary>
        /// Last traded price of this instrument
        /// </summary>
        public ezPrice Last { get; set; }
        /// <summary>
        /// Quantity of last trade in this instrument
        /// </summary>
        public ezQuantity LastQty { get; set; }
        /// <summary>
        /// Total volume traded at the last trade price
        /// </summary>
        public int LastTotalVolume { get; set; }

        public ezPrice MidPrice { get { return ezPrice.FromDouble(this, (Bid.ToDouble() + Ask.ToDouble()) / 2); } }
        public ezPrice WeightedMidPrice { get; set; }

        /// <summary>
        /// Market depth for this instrument
        /// </summary>
        /// <remarks>Each level is represented by a TTDepthLevel</remarks>
        public EZMarketDepth MarketDepth { get; set; }
        /// <summary>
        /// Number of depth levels contained in the MarketDepth for this instrument
        /// </summary>
        /// <remarks>A DepthCount of zero means no market depth is available</remarks>
        public int DepthCount
        {
            get
            {
                if (MarketDepth == null)
                    return 0;
                else
                    return MarketDepth.DepthCount;
            }
        }
        /// <summary>
        /// The OrderRoute contains information on the order feeds, account name, etc. used to
        /// send an order with this instrument
        /// </summary>
        public EZOrderRoute OrderRoute { get; set; }
        /// <summary>
        /// Opening price of this instrument
        /// </summary>
        public ezPrice Open { get; set; }
        /// <summary>
        /// High price of this instrument for the day
        /// </summary>
        public ezPrice High { get; set; }
        /// <summary>
        /// Low price of this instrument for the day
        /// </summary>
        public ezPrice Low { get; set; }
        /// <summary>
        /// Closing price of this instrument
        /// </summary>
        public ezPrice Close { get; set; }
        /// <summary>
        /// The net change in price of this instrument for the day
        /// </summary>
        public ezPrice NetChange { get; set; }
        /// <summary>
        /// The percentage net change in this instrument's price for the day
        /// </summary>
        public double NetChangePercent { get; set; }
        /// <summary>
        /// Current volume (number of contracts traded) in this instrument
        /// </summary>
        public ezQuantity Volume { get; set; }
        /// <summary>
        /// The market (or exchange) on which this instrument trades (i.e. "CME", "CBOT", etc.)
        /// </summary>
        public ezMarket Market { get { return null; /*TTAPI_Instrument.Product.Market;*/ } }
        /// <summary>
        /// Returns true if this instrument is an Autospreader instrument
        /// </summary>
        public bool IsAutospreader { get { return false; /*TTAPI_Instrument.Key.IsAutospreader;*/ } }
        /// <summary>
        /// List of the order feeds that are enabled for this instrument
        /// </summary>
        public ReadOnlyCollection<ezOrderFeed> EnabledOrderFeeds
        {
            get
            {
                List<ezOrderFeed> feeds = new List<ezOrderFeed>();
                
                /*
                // iterate order feeds enabled for this instrument
                foreach (ezOrderFeed oFeed in TTAPI_Instrument.GetValidOrderFeeds())
                {
                    if (oFeed.IsTradingEnabled)
                        feeds.Add(oFeed);
                }
                */

                return new ReadOnlyCollection<ezOrderFeed>(feeds);
            }
        }
        /// <summary>
        /// Current profit or loss taking all the orders for this instrument for the day
        /// </summary>
        public double Profit { get; set; }
        /// <summary>
        /// A compact view of the information used to subscribe to an instrument separated
        /// by the "|" character (i.e. "CME|FUTURE|CL|Nov12")
        /// </summary>
        public string DescriptorString
        {
            get
            {
                /*if (InstrumentDescriptor != null)
                    return InstrumentDescriptor.DescriptorString;
                else
                {*/
                return null;
                /*
                if (TTAPI_Instrument == null)
                    return null;

                string[] items = TTAPI_Instrument.Name.Split(' ');
                string contract = items[items.Length - 1];
                if (!EZHelper.Instance.IsMonthYear(contract))
                    contract = items[items.Length - 2] + " " + items[items.Length - 1];
                return string.Format("{0}|{1}|{2}|{3}", Market.Name, TTAPI_Instrument.Product.Type.Name, TTAPI_Instrument.Product.Name, contract);
                */
                //}
            }
        }
        public int NetPos { get; set; }
        public int NetBuys { get; set; }
        public int NetSells { get; set; }

        private EZInstrumentDescriptor _instrumentDescriptor = null;

        /// <summary>
        /// This parameterless constructor exists solely so we can derive TTSpread from TTInstrument 
        /// </summary>
        protected EZInstrument()
        {
        }

        /// <summary>
        /// Construct a TTInstrument object by passing an instrument key
        /// </summary>
        /// <param name="key">Same InstrumentKey used by the underlying TTAPI</param>
        public EZInstrument(ezInstrumentKey key)
        {
            Key = key;
            InstrumentDescriptor = null;
        }

        /*
        public TTInstrument(string descriptor)
        {
            InstrumentDescriptor = descriptor;
        }*/

        /// <summary>
        /// Construct a TTInstrument object by passing a TTAPI Instrument object
        /// </summary>
        /// <param name="instrument">Same Instrument used by the underlying TTAPI</param>
        public EZInstrument(ezInstrument instrument)
        {
            //TTAPI_Instrument = instrument;
            Key = instrument.Key;
            InstrumentDescriptor = null;
        }

        private void SendOrder(zBuySell buySell, zOrderType orderType, int qty, double price)
        {
            //OrderRoute.GetOrderRoute(this, this.Market.Name);
            /*ezOrderProfile prof = new ezOrderProfile(OrderRoute.OrderFeed, TTAPI_Instrument);
            prof.BuySell = buySell;
            prof.OrderType = orderType;
            prof.OrderQuantity = ezQuantity.FromInt(this, qty);
            prof.LimitPrice = ezPrice.FromDouble(this, price);
            prof.AccountType = OrderRoute.AccountType;
            prof.AccountName = OrderRoute.AccountName;
            TTAPI_Instrument.Session.SendOrder(prof);
            */
            APIMain.SendOrder();
        }

        /// <summary>
        /// Send a limit order to buy this instrument with a specified quantity and price
        /// </summary>
        /// <param name="qty">Number of contracts to buy</param>
        /// <param name="price">Price at which to buy</param>
        public virtual void BuyLimit(int qty, double price)
        {
            SendOrder(zBuySell.Buy, zOrderType.Limit, qty, price);
        }

        /// <summary>
        /// Send a limit order to sell this instrument with a specified quantity and price
        /// </summary>
        /// <param name="qty">Number of contracts to sell</param>
        /// <param name="price">Price at which to sell</param>
        public virtual void SellLimit(int qty, double price)
        {
            SendOrder(zBuySell.Sell, zOrderType.Limit, qty, price);
        }

        /// <summary>
        /// Send a market order to buy this instrument with a specified quantity
        /// </summary>
        /// <param name="qty">Number of contracts to buy</param>
        public void BuyMarket(int qty)
        {
            SendOrder(zBuySell.Buy, zOrderType.Market, qty, -1);
        }

        /// <summary>
        /// Send a market order to sell this instrument with a specified quantity
        /// </summary>
        /// <param name="qty">Number of contracts to sell</param>
        public void SellMarket(int qty)
        {
            SendOrder(zBuySell.Sell, zOrderType.Market, qty, -1);
        }

        public void UpdatePrice(ezFullPriceQuote quote)
        {
            Bid = quote.Bid;
            BidQty = quote.BidQty;
            Ask = quote.Ask;
            AskQty = quote.AskQty;
            Last = quote.Last;
            LastQty = quote.LastQty;
        }

        #region OVERRIDE EQUALS
        public override bool Equals(object obj)
        {
            bool result = false;

            EZInstrument compareObject = obj as EZInstrument;

            if (compareObject.InstrumentDescriptor != null && this.InstrumentDescriptor != null)
            {
                result = compareObject.InstrumentDescriptor.Equals(this.InstrumentDescriptor);
            }
            else
            {
                result = (compareObject.Key == this.Key);
            }

            return result;
        }

        // Two objects that are equal MUST return the same value for GetHashCode. There may be multiple
        // objects with the same GetHashCode value, and this will just cause a "collision" when indexing.
        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }
        #endregion

        public override string ToString()
        {
            return Name;
        }
    } //class
} // namespace
