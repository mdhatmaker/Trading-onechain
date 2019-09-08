using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace EZAPI.Framework.Base
{
    public class ezInstrumentKey
    {

        string gateway;
        string productType;
        string productName;
        string seriesKey;

        /// <summary>
        /// NOTE: For CTS, you are passing this constructor (ExchangeID, ContractID, MarketID, ?)
        /// </summary>
        /// <param name="gateway"></param>
        /// <param name="productType"></param>
        /// <param name="productName"></param>
        /// <param name="seriesKey"></param>
        public ezInstrumentKey(string gateway, string productType, string productName, string seriesKey)
        {
            this.gateway = gateway;
            this.productType = productType;
            this.productName = productName;
            this.seriesKey = seriesKey;
        }

        public bool IsAutospreader
        {
            get { return false; }
        }

        public ReadOnlyCollection<ezOrderFeed> GetValidOrderFeeds()
        {
            var list = new List<ezOrderFeed>();
            return new ReadOnlyCollection<ezOrderFeed>(list);
        }

    } // class
} // namespace
