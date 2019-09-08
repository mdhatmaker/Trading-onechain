using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingTechnologies.TTAPI;
using TradingTechnologies.TTAPI.Tradebook;
using TradingTechnologies.TTAPI.Autospreader;

namespace EZAPI.Containers
{
    /// <summary>
    /// This class is used to create TTSpread objects which are derived from the TTInstrument object
    /// </summary>
    public class TTSpread : TTInstrument
    {
        /// <summary>
        /// The TTAPI AutoSpreaderInstrument object
        /// </summary>
        public AutospreaderInstrument SpreadInstrument { get { return TTAPI_Instrument as AutospreaderInstrument; } }
        /// <summary>
        /// Name of the ASE server on which to execute orders for this spread (i.e. "ASE-B")
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// Construct a TTSpread object from a TTAPI Instrument object
        /// </summary>
        /// <param name="instrument">TTAPI instrument which represents this spread instrument</param>
        public TTSpread(Instrument instrument) : base(instrument)
        {
            ServerName = null;
        }

        private OrderFeed GetOrderFeed()
        {
            OrderFeed feed = null;
            foreach (OrderFeed f in SpreadInstrument.GetValidOrderFeeds())
                if (f.Name.Equals(ServerName) && f.IsTradingEnabled)
                    feed = f;
            return feed;
        }

        private void SendLimitOrder(BuySell buySell, int qty, double price)
        {
            OrderFeed feed = GetOrderFeed();
            LaunchReturnCode lrc = SpreadInstrument.LaunchToOrderFeed(feed);
            if (lrc == LaunchReturnCode.Success)
            {
                AutospreaderSyntheticOrderProfile prof = new AutospreaderSyntheticOrderProfile(feed, SpreadInstrument);
                prof.BuySell = buySell;
                prof.OrderQuantity = Quantity.FromInt(SpreadInstrument, qty);
                prof.OrderType = OrderType.Limit;
                prof.LimitPrice = Price.FromDouble(SpreadInstrument, price);

                if (!SpreadInstrument.Session.SendOrder(prof))
                {
                    Console.WriteLine("send order failed: {0}", prof.RoutingStatus.Message);
                }
            }
        }

        /// <summary>
        /// Version of the BuyLimit method that overrides the one in TTInstrument in order to
        /// correctly submit a spread order to buy with a specified quantity and price
        /// </summary>
        /// <param name="qty">Number of spreads to buy as an int</param>
        /// <param name="price">Price at which to buy the spreads as a double</param>
        public override void BuyLimit(int qty, double price)
        {
            SendLimitOrder(BuySell.Buy, qty, price);
        }

        /// <summary>
        /// Version of the SellLimit method that overrides the one in TTInstrument in order to
        /// correctly submit a spread order to sell with a specified quantity and price
        /// </summary>
        /// <param name="qty">Number of spreads to sell as an int</param>
        /// <param name="price">Price at which to sell the spreads as a double</param>
        public override void SellLimit(int qty, double price)
        {
            SendLimitOrder(BuySell.Sell, qty, price);
        }

    } // class
} // namespace
