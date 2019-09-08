using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZAPI.Framework.Base
{
    public class ezOrderProfile
    {
        public zBuySell BuySell { get; set; }
        public zOrderType OrderType { get; set; }
        public ezQuantity OrderQuantity { get; set; }
        public ezPrice LimitPrice { get; set; }
        public zAccountType AccountType { get; set; }
        public string AccountName { get; set; }
        public string FFT2 { get; set; }
        public string FFT3 { get; set; }
        public string GiveUp { get; set; }
        public bool IsAutomated { get; set; }
        public ezQuantity MinimumQuantity { get; set; }
        public zOrderModifiers Modifiers { get; set; }
        public zOpenClose OpenClose { get; set; }
        public string OrderTag { get; set; }
        public bool PriceCheck { get; set; }
        public ezOrderRestriction OrderRestriction { get; set; }
        public ezPrice StopPrice { get; set; }
        public ezQuantity StopTriggerQuantity { get; set; }
        public string SubUserId { get; set; }
        public ezTimeInForce TimeInForce { get; set; }
        public string UserName { get; set; }
        public string UserTag { get; set; }
        public string Destination { get; set; }
        public ezOrderRestriction Restriction { get; set; }
        public ezOrderRoutingStatus RoutingStatus { get; set; }

        public ezOrderProfile(ezOrderFeed feed, ezInstrument instrument)
        {

        }

    } // class
} // namespace
