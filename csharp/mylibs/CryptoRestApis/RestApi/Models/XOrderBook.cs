using System;
using System.Collections.Generic;

namespace CryptoRestApis.RestApi.Models
{
    public class XOrderBook : XModel
    {
        public SortedDictionary<decimal, XOrderBookEntry> Bids { get; private set; }
        public SortedDictionary<decimal, XOrderBookEntry> Asks { get; private set; }

        public XOrderBook()
        {
        }
    } // end of class XOrderBook

} // end of namespace
