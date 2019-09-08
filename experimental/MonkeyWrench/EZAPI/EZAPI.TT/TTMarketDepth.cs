using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingTechnologies.TTAPI;
using TradingTechnologies.TTAPI.Tradebook;

namespace EZAPI.Containers
{
    /// <summary>
    /// This class maintains market depth (used for each instrument when market depth is subscribed)
    /// </summary>
    public class TTMarketDepth
    {
        /// <summary>
        /// The maximum market depth level count
        /// </summary>
        public int DepthCount { get { return _depthCount; } }
        /// <summary>
        /// Index into the market depth levels (use like marketDepth[2] to return level 2 market depth)
        /// </summary>
        /// <param name="index">market depth level to return</param>
        /// <returns>MarketDepthLevel object containing market depth for the specified level</returns>
        public TTMarketDepthLevel this[int index]
        {
            get
            {
                return _marketDepth[index];
            }
            set
            {
                if (_marketDepth[index] == null)
                    _marketDepth[index] = new TTMarketDepthLevel();
                _marketDepth[index].Bid = value.Bid;
                _marketDepth[index].BidQty = value.BidQty;
                _marketDepth[index].Ask = value.Ask;
                _marketDepth[index].AskQty = value.AskQty;
            }
        }

        int _depthCount;
        TTMarketDepthLevel[] _marketDepth;

        /// <summary>
        /// Construct a market depth object with the specified instrument key and maximum depth
        /// </summary>
        /// <param name="key">InstrumentKey for the TTInstrument associated with this depth</param>
        /// <param name="maxDepthCount">The maximum number of depth levels allowed for the associated instrument</param>
        public TTMarketDepth(int maxDepthCount)
        {
            _depthCount = maxDepthCount;
            _marketDepth = new TTMarketDepthLevel[maxDepthCount];
            for (int i = 0; i < maxDepthCount; i++)
                _marketDepth[i] = new TTMarketDepthLevel();
        }

        /// <summary>
        /// Get the market depth for the specified level (works just like the indexer)
        /// </summary>
        /// <param name="level">market depth level to return</param>
        /// <returns>market depth at the specified level</returns>
        public TTMarketDepthLevel GetDepth(int level)
        {
            return _marketDepth[level];
        }

        /// <summary>
        /// Set the market depth at a specified level
        /// </summary>
        /// <param name="level">market depth level to change</param>
        /// <param name="depthLevel">new MarketDepthLevel object for the specified level</param>
        public void SetDepth(int level, TTMarketDepthLevel depthLevel)
        {
            SetDepth(level, depthLevel.Bid, depthLevel.BidQty, depthLevel.Ask, depthLevel.AskQty);
        }

        /// <summary>
        /// Set the market depth at a specified level by specifying bid/ask price/quantity
        /// </summary>
        /// <param name="level">market depth level to change</param>
        /// <param name="bid">bid price</param>
        /// <param name="bidQty">bid quantity</param>
        /// <param name="ask">ask price</param>
        /// <param name="askQty">ask quantity</param>
        public void SetDepth(int level, Price bid, Quantity bidQty, Price ask, Quantity askQty)
        {
            if (_marketDepth[level] == null)
                _marketDepth[level] = new TTMarketDepthLevel();
            _marketDepth[level].Bid = bid;
            _marketDepth[level].BidQty = bidQty;
            _marketDepth[level].Ask = ask;
            _marketDepth[level].AskQty = askQty;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < _depthCount; i++)
            {
                sb.Append(string.Format("{0}) {1}\n", i, _marketDepth[i]));
            }
            return sb.ToString();
        }
    } // class

    /// <summary>
    /// This class holds one level of market depth
    /// </summary>
    public class TTMarketDepthLevel
    {
        /// <summary>
        /// Bid price
        /// </summary>
        public Price Bid { get; set; }
        /// <summary>
        /// Bid quantity
        /// </summary>
        public Quantity BidQty { get; set; }
        /// <summary>
        /// Ask price
        /// </summary>
        public Price Ask { get; set; }
        /// <summary>
        /// Ask quantity
        /// </summary>
        public Quantity AskQty { get; set; }

        /// <summary>
        /// Construct an empty market depth level
        /// </summary>
        public TTMarketDepthLevel()
        {
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}  {2}:{3}", BidQty, Bid, Ask, AskQty);
        }
    }
} // namespace
