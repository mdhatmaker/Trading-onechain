using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZAPI.Framework.Base;
using EZAPI.Framework.Spread;

namespace EZAPI.Framework
{
    /// <summary>
    /// This class is used to create TTSpread objects which are derived from the TTInstrument object
    /// </summary>
    public class EZSpreadInstrument : EZInstrument
    {
        public List<ezSpreadLeg> Legs { get { return spreadLegs.Values.ToList<ezSpreadLeg>(); } }

        public new ezPrice MidPrice { get; set; }

        private Dictionary<EZInstrument, ezSpreadLeg> spreadLegs = new Dictionary<EZInstrument, ezSpreadLeg>();
        private ezSpreadDefinition spreadDefinition;
        
        /// <summary>
        /// The TTAPI AutoSpreaderInstrument object
        /// </summary>
        public ezAutospreaderInstrument SpreadInstrument { get { return BaseInstrument as ezAutospreaderInstrument; } }
        /// <summary>
        /// Name of the ASE server on which to execute orders for this spread (i.e. "ASE-B")
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// Construct a TTSpread object from a TTAPI Instrument object
        /// </summary>
        /// <param name="instrument">TTAPI instrument which represents this spread instrument</param>
        public EZSpreadInstrument(ezInstrument instrument) : base(instrument)
        {
            ServerName = null;
        }

        public EZSpreadInstrument(ezSpreadDefinition spreadDefinition)
            : base()
        {
            this.spreadDefinition = spreadDefinition;
            
            foreach (ezSpreadLeg leg in spreadDefinition.Legs)
            {
                spreadLegs[leg.Instrument] = leg;
            }
        }

        public void UpdatePrice()
        {
            double bid = 0.0;
            double ask = 0.0;
            double last = 0.0;
            double mid = 0.0;
            foreach (ezSpreadLeg leg in Legs)
            {
                if (leg.BuySell == zBuySell.Buy)
                    bid += leg.Instrument.Bid * leg.PriceMultiplier;
                else
                    bid += leg.Instrument.Ask * leg.PriceMultiplier;

                if (leg.BuySell == zBuySell.Buy)
                    ask += leg.Instrument.Ask * leg.PriceMultiplier;
                else
                    ask += leg.Instrument.Bid * leg.PriceMultiplier;

                last += leg.Instrument.Last * leg.PriceMultiplier;

                mid += leg.Instrument.MidPrice * leg.PriceMultiplier;
            }
            Bid = ezPrice.FromDouble(this, bid);
            Ask = ezPrice.FromDouble(this, ask);
            Last = ezPrice.FromDouble(this, last);
            MidPrice = ezPrice.FromDouble(this, mid);
        }

        private ezOrderFeed GetOrderFeed()
        {
            ezOrderFeed feed = null;
            foreach (ezOrderFeed f in SpreadInstrument.GetValidOrderFeeds())
                if (f.Name.Equals(ServerName) && f.IsTradingEnabled)
                    feed = f;
            return feed;
        }

        private void SendLimitOrder(zBuySell buySell, int qty, double price)
        {
            /*
            ezOrderFeed feed = GetOrderFeed();
            zLaunchReturnCode lrc = SpreadInstrument.LaunchToOrderFeed(feed);
            if (lrc == zLaunchReturnCode.Success)
            {
                ezAutospreaderSyntheticOrderProfile prof = new ezAutospreaderSyntheticOrderProfile(feed, SpreadInstrument);
                prof.BuySell = buySell;
                prof.OrderQuantity = ezQuantity.FromInt(SpreadInstrument, qty);
                prof.OrderType = zOrderType.Limit;
                prof.LimitPrice = ezPrice.FromDouble(SpreadInstrument, price);

                if (!SpreadInstrument.Session.SendOrder(prof))
                {
                    Console.WriteLine("send order failed: {0}", prof.RoutingStatus.Message);
                }
            }
            */
        }

        /// <summary>
        /// Version of the BuyLimit method that overrides the one in TTInstrument in order to
        /// correctly submit a spread order to buy with a specified quantity and price
        /// </summary>
        /// <param name="qty">Number of spreads to buy as an int</param>
        /// <param name="price">Price at which to buy the spreads as a double</param>
        public override void BuyLimit(int qty, double price)
        {
            SendLimitOrder(zBuySell.Buy, qty, price);
        }

        /// <summary>
        /// Version of the SellLimit method that overrides the one in TTInstrument in order to
        /// correctly submit a spread order to sell with a specified quantity and price
        /// </summary>
        /// <param name="qty">Number of spreads to sell as an int</param>
        /// <param name="price">Price at which to sell the spreads as a double</param>
        public override void SellLimit(int qty, double price)
        {
            SendLimitOrder(zBuySell.Sell, qty, price);
        }

    } // class
} // namespace
