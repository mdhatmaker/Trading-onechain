using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZAPI.Framework.Base
{
    public class ezFullPriceQuote
    {
        public ezPrice Bid { get; set; }
        public ezQuantity BidQty { get; set; }
        public ezPrice Ask { get; set; }
        public ezQuantity AskQty { get; set; }
        public ezPrice Last { get; set; }
        public ezQuantity LastQty { get; set; }

    } // class
} // namespace
