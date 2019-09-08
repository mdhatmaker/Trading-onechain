using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CryptoTools.Net;

namespace CryptoRestApis.Models
{


    public abstract class AccountBalance
    {
        public abstract string Asset { get; }
        public abstract decimal Free { get; }
        public abstract decimal Locked { get; }
        public decimal Total { get { return Free + Locked; } }

        public override string ToString() { return string.Format("{0,-4}    free:{1,13}    locked:{2,13}    total:{3,13}", Asset, Free, Locked, Total); }
    } // end of class AccountBalance

    public struct Ticker
    {
        public decimal Bid { get; set; }
        public decimal BidSize { get; set; }
        public decimal Ask { get; set; }
        public decimal AskSize { get; set; }
        public long Time { get; set; }

        public Ticker(decimal bid, decimal bidSize, decimal ask, decimal askSize, long time)
        {
            Bid = bid;
            BidSize = bidSize;
            Ask = ask;
            AskSize = askSize;
            Time = time;
        }

        public Ticker(JObject jo)
        {
            Time = jo["E"].Value<long>();
            Bid = jo["b"].Value<decimal>();
            BidSize = jo["B"].Value<decimal>();
            Ask = jo["a"].Value<decimal>();
            AskSize = jo["A"].Value<decimal>();
        }
    } // end of struct Ticker

    public struct OrderBookTicker : NullableObject
    {
        public string symbol { get; set; }
        public decimal bidPrice { get; set; }
        public decimal bidQty { get; set; }
        public decimal askPrice { get; set; }
        public decimal askQty { get; set; }

        public decimal MidPrice { get { return (bidPrice + askPrice) / 2.0M; } }
        public decimal BidAskSpread { get { return askPrice - bidPrice; } }

        public bool IsNull { get { return symbol == null; } }
    }

} // end of namespace
