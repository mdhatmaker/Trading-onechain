using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using TradingTechnologies.TTAPI;
using TradingTechnologies.TTAPI.Tradebook;

namespace EZAPI.Containers
{
    /// <summary>
    /// Current status of this order to indicate if it was last added, updated, deleted, etc.
    /// </summary>
    public enum TTOrderStatus { Added, Updated, Deleted, Filled, Rejected, Unknown, None };
    public enum TTModify { Price, Quantity };

    /*public class OrderKey
    {
        private string _key;

        public OrderKey(string key)
        {
            _key = key;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            OrderKey key = obj as OrderKey;
            if ((System.Object)key == null)
                return false;

            return _key.Equals(key._key);
        }

        public bool Equals(OrderKey key)
        {
            if (key == null)
                return false;

            return _key.Equals(key._key);
        }

        public override int GetHashCode()
        {
            int result = 1;
            for (int i = 0; i < _key.Length; i++)
            {
                if (char.IsDigit(_key[i]))
                    result = result * ((int)_key[i] + 1);
            }
            return result;
        }
    }*/

    /// <summary>
    /// This delegate is used to notify the EZAPI of any changes that need to be made to the order
    /// </summary>
    /// <param name="modify">Type of modification (price, quantity)</param>
    /// <param name="order">TTOrder object to modify</param>
    /// <param name="price">New price (Price.Invalid if changing quantity)</param>
    /// <param name="quantity">New quantity (Quantity.Invalid if changing price)</param>
    public delegate void TTOrderModifyHandler(TTModify modify, TTOrder order, Price price, Quantity quantity);

    /// <summary>
    /// A TTOrder object contains all the information necessary to maintain, modify, etc. an
    /// order within the API
    /// </summary>
    public class TTOrder
    {
        /// <summary>
        /// Unique key identifying this order
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// Instrument key of the instrument in this order
        /// </summary>
        public InstrumentKey InstrumentKey { get; set; }
        /// <summary>
        /// Instrument to buy/sell with this order
        /// </summary>
        public TTInstrument Instrument { get; set; }
        /// <summary>
        /// Order side (buy/sell)
        /// </summary>
        public BuySell BuySell { get; set; }
        /// <summary>
        /// Status of this order (Added, Updated, Deleted, etc.)
        /// </summary>
        /// <seealso cref=">TTOrderStatus"/>
        public TTOrderStatus Status { get; set; }
        /// <summary>
        /// The OrderRoute contains information on the order feeds, account name, etc. used to
        /// send an order with this instrument
        /// </summary>
        public OrderRoute OrderRoute { get; set; }
        /// <summary>
        /// The underlying TTAPI Order object
        /// </summary>
        public Order TTAPI_Order { get { return _order; } set { _order = value; } }
        /// <summary>
        /// Type of order as Limit, Market, etc.
        /// </summary>
        /// <remarks>Uses the standard TTAPI order types</remarks>
        public OrderType OrderType { get; set; }
        /// <summary>
        /// The market (or exchange) on which this instrument trades (i.e. "CME", "CBOT", etc.)
        /// </summary>
        public Market Market { get { return Instrument.TTAPI_Instrument.Product.Market; } }
        /// <summary>
        /// User-defined identifying string for this order
        /// </summary>
        public string Tag { get { return TTAPI_Order.OrderTag; } }
        /// <summary>
        /// Number of contracts to buy/sell
        /// </summary>
        /// <remarks>Setting the Quantity property will do the necessary work to modify the order</remarks>
        public Quantity Quantity
        {
            get { return _quantity; }
            set
            {
                if (OnOrderModify != null) OnOrderModify(TTModify.Quantity, this, Price.Invalid, value);
            }
        }
        /// <summary>
        /// Price at which to buy/sell
        /// </summary>
        /// <remarks>Setting the Price property will do the necessary work to modify the order</remarks>
        public Price Price
        {
            get { return _price; }
            set
            {
                if (OnOrderModify != null) OnOrderModify(TTModify.Price, this, value, Quantity.Invalid);
            }
        }
        /// <summary>
        /// Number of contracts to buy/sell as an integer
        /// </summary>
        /// <remarks>Setting the SimpleQuantity property will do the necessary work to modify the order</remarks>
        public int SimpleQuantity
        {
            get { return _quantity.ToInt(); }
            set
            {
                if (OnOrderModify != null) OnOrderModify(TTModify.Quantity, this, Price.Invalid, Quantity.FromInt(TTAPI_Order, value));
            }
        }
        /// <summary>
        /// Price at which to buy/sell as a double
        /// </summary>
        /// <remarks>Setting the SimplePrice property will do the necessary work to modify the order</remarks>
        public double SimplePrice
        {
            get { return _price.ToDouble(); }
            set
            {
                if (OnOrderModify != null) OnOrderModify(TTModify.Price, this, Price.FromDouble(TTAPI_Order, value), Quantity.Invalid);
            }
        }

        /// <summary>
        /// This event is fired when this order needs to be modified (EZAPI should handle this event)
        /// </summary>
        public event TTOrderModifyHandler OnOrderModify;

        private Order _order;
        private Quantity _quantity;
        private Price _price;

        /// <summary>
        /// Construct a TTOrder object from a TTAPI Order object
        /// </summary>
        /// <param name="order">TTAPI Order object</param>
        public TTOrder(Order order)
        {
            _order = order;
            
            InstrumentKey = order.InstrumentKey;
            BuySell = order.BuySell;
            _quantity = order.OrderQuantity;
            _price = order.LimitPrice;
            Status = TTOrderStatus.None;
        }

        /// <summary>
        /// Cancel this order deleting it from the market
        /// </summary>
        public void Cancel()
        {
            _order.Delete();            
        }

        /// <summary>
        /// Mark this order as held/"un-held" to pull it from the market or re-enter it into the market
        /// </summary>
        /// <param name="hold">true to mark the order as held; false to "un-hold" the order</param>
        public void Hold(bool hold)
        {
            if (hold == true)
                _order.Hold();
            else
                _order.RemoveFromHold();
        }

        /// <summary>
        /// Create an OrderProfile from this existing order
        /// </summary>
        /// <returns>A TTAPI OrderProfile object</returns>
        /// <remarks>There is a problem with using the TTAPI method to create a copy of an order</remarks>
        public OrderProfile CloneProfile()
        {
            ReadOnlyCollection<OrderFeed> feeds = Instrument.TTAPI_Instrument.GetValidOrderFeeds();
            OrderFeed feed = feeds[1];
            OrderRoute.GetOrderRoute(Instrument, Market.Name);
            OrderProfile profile = new OrderProfile(feed, Instrument.TTAPI_Instrument);
            profile.AccountName = TTAPI_Order.AccountName;
            profile.AccountType = TTAPI_Order.AccountType;
            profile.BuySell = TTAPI_Order.BuySell;
            profile.Destination = TTAPI_Order.Destination;
            profile.FFT2 = TTAPI_Order.FFT2;
            profile.FFT3 = TTAPI_Order.FFT3;
            profile.GiveUp = TTAPI_Order.GiveUp;
            profile.IsAutomated = TTAPI_Order.IsAutomated;
            profile.LimitPrice = TTAPI_Order.LimitPrice;
            profile.MinimumQuantity = TTAPI_Order.MinimumQuantity;
            profile.Modifiers = TTAPI_Order.Modifiers;
            profile.OpenClose = TTAPI_Order.OpenClose;
            profile.OrderQuantity = TTAPI_Order.OrderQuantity;
            profile.OrderTag = TTAPI_Order.OrderTag;
            profile.OrderType = TTAPI_Order.OrderType;
            profile.PriceCheck = TTAPI_Order.PriceCheck;
            profile.Restriction = TTAPI_Order.Restriction;
            profile.StopPrice = TTAPI_Order.StopPrice;
            profile.StopTriggerQuantity = TTAPI_Order.StopTriggerQuantity;
            profile.SubUserId = TTAPI_Order.SubUserId;
            profile.TimeInForce = TTAPI_Order.TimeInForce;
            profile.UserName = TTAPI_Order.UserName;
            profile.UserTag = TTAPI_Order.UserTag;

            return profile;
        }

        /// <summary>
        /// Create a copy of an existing TTOrder object
        /// </summary>
        /// <returns>TTOrder object that is a clone of the existing object</returns>
        public TTOrder Clone()
        {
            TTOrder tto = new TTOrder(TTAPI_Order);
            return tto;
        }

        /// <summary>
        /// Easier-to-read string representation of this Order object
        /// </summary>
        /// <returns>A string containing a subset of information about this order</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{" + _order.BuySell.ToString() + "}");
            sb.Append("{" + _order.OrderQuantity.ToString() + "}");
            sb.Append("{" + _order.InstrumentKey + "}");
            sb.Append("{" + _order.LimitPrice + "}");
            if (_order.IsSynthetic)
                sb.Append(" SYNTHETIC ");
            if (_order.IsParent)
                sb.Append("  PARENT  ");
            if (_order.IsChild)
                sb.Append("  CHILD  ");
            return (sb.ToString());
        }
    
    } // class

    /*
    OrderProfile CreateOrderCopy(Order order, Instrument instrument)
    {
        ReadOnlyCollection<OrderFeed> feeds = instrument.GetValidOrderFeeds();
        OrderFeed feed = feeds[1];
        OrderProfile profile = new OrderProfile(feed, instrument);
        profile.AccountName = order.AccountName;
        profile.AccountType = order.AccountType;
        profile.BuySell = order.BuySell;
        profile.Destination = order.Destination;
        profile.FFT2 = order.FFT2;
        profile.FFT3 = order.FFT3;
        profile.GiveUp = order.GiveUp;
        profile.IsAutomated = order.IsAutomated;
        profile.LimitPrice = order.LimitPrice;
        profile.MinimumQuantity = order.MinimumQuantity;
        profile.Modifiers = order.Modifiers;
        profile.OpenClose = order.OpenClose;
        profile.OrderQuantity = order.OrderQuantity;
        profile.OrderTag = order.OrderTag;
        profile.OrderType = order.OrderType;
        profile.PriceCheck = order.PriceCheck;
        profile.Restriction = order.Restriction;
        profile.StopPrice = order.StopPrice;
        profile.StopTriggerQuantity = order.StopTriggerQuantity;
        profile.SubUserId = order.SubUserId;
        profile.TimeInForce = order.TimeInForce;
        profile.UserName = order.UserName;
        profile.UserTag = order.UserTag;

        return profile;
    }
    */

} // namespace
