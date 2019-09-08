using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZAPI.Framework.Base
{
    public class ezFill
    {
        public zFillType FillType
        {
            get { return zFillType.Full; }
        }

        public string SpreadId
        {
            get { return "spreadId"; }
        }

        public DateTime TransactionDateTime
        {
            get { return DateTime.Now; }
        }

        public zBuySell BuySell
        {
            get { return zBuySell.Unknown; }
        }

        public ezQuantity Quantity
        {
            get { return ezQuantity.Invalid; }
        }

        public ezInstrumentKey InstrumentKey
        {
            get { return null; }
        }

        public ezPrice MatchPrice
        {
            get { return ezPrice.Invalid; }
        }

        public string FFT2
        {
            get { return "FFT2"; }
        }

        public string FFT3
        {
            get { return "FFT3"; }
        }

        public bool IsStartOfDay
        {
            get { return false; }
        }

        public zOpenClose OpenClose
        {
            get { return zOpenClose.Open; }
        }

        public bool IsAutospreaderLegFill
        {
            get { return false; }
        }

        public bool IsAutospreaderSyntheticFill
        {
            get { return false; }
        }

        public bool IsHedge
        {
            get { return false; }
        }

        public bool IsQuoting
        {
            get { return false; }
        }

        public bool IsExchangeSpreadLegFill
        {
            get { return false; }
        }

        public bool IsSseFill
        {
            get { return false; }
        }

        public bool IsSseChildFill
        {
            get { return false; }
        }



    } // class
} // namespace
