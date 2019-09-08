using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZAPI.Framework.Base;

namespace EZAPI.Framework
{
    /// <summary>
    /// A FillAction is used to describe the action of the Fill event messages received by EZAPI
    /// </summary>
    public enum FillAction { ADD = 0, AMEND = 1, DELETE = 2, LIST_START = 3, LIST_END = 4, BOOK_DOWNLOAD = 5 };
    /// <summary>
    /// A FillOriginator specifies from whom the Fill event messages received by EZAPI originate
    /// </summary>
    public enum FillOriginator { TRADER = 0, ADMIN = 1, BOOK = 2 };

    /*public class FillKey
    {
        private string _key;

        public FillKey(string key)
        {
            _key = key;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            FillKey key = obj as FillKey;
            if ((System.Object)key == null)
                return false;

            return _key.Equals(key._key);
        }

        public bool Equals(FillKey key)
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
    /// This class encapsulates the information for a fill in EZAPI
    /// </summary>
    public class EZFill : EZBaseObject 
    {
        /// <summary>
        /// A unique key to identify this fill
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// The unique key that identifies the instrument in this fill
        /// </summary>
        public ezInstrumentKey InstrumentKey { get; set; }
        /// <summary>
        /// The instrument bought/sold in this fill
        /// </summary>
        public EZInstrument Instrument { get; set; }
        /// <summary>
        /// The time this fill occurred
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// A shortened version of the fill time (hh:mm:ss AM/PM)
        /// </summary>
        public string ShortTime { get; set; }
        /// <summary>
        /// A string containing abbreviations for the various identifiers associated with
        /// this fill such as "S" (start-of-day), "A" (admin), "ASL" (autospreader leg), etc.
        /// </summary>
        public string Identifiers { get; set; }
        /// <summary>
        /// The side of this fill (buy/sell)
        /// </summary>
        public zBuySell BuySell { get; set; }
        /// <summary>
        /// The number of contracts bought/sold in this fill
        /// </summary>
        public ezQuantity Quantity { get; set; }
        public string InstrumentName { get; set; }
        public ezPrice Price { get; set; }
        public ezFill TTAPI_Fill { get { return _fill; } }
        public string FFT2 { get; set; }
        public string FFT3 { get; set; }
        public zFillType FillType { get; set; }
        public string SpreadId { get { return _fill.SpreadId; } }
        public FillOriginator Originator { get; set; }
        public FillAction Action { get; set; }
        public EZFill ReplacementForFill { get; set; }
        
        private ezFill _fill;

        public EZFill(ezFill fill)
        {
            TimeZoneInfo cst = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(fill.TransactionDateTime, cst);

            DateTime time = localTime;
            string timeStr = time.ToString("H:mm:ss.fff");
            string shortTimeStr = time.ToString("hh:mm:ss tt");

            Time = timeStr;
            ShortTime = shortTimeStr;
            Identifiers = GetFillIdenifiers(fill);
            BuySell = fill.BuySell;
            Quantity = fill.Quantity;
            InstrumentName = ParseInstrumentKey(fill.InstrumentKey.ToString());
            Price = fill.MatchPrice;
            FFT2 = fill.FFT2;
            FFT3 = fill.FFT3;

            _fill = fill;
        }

        public EZFill(DateTime fillTime, EZInstrument instrument, zBuySell buySell, ezQuantity quantity, ezPrice price, zFillType fillType)
        {
            TimeZoneInfo cst = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(fillTime, cst);

            DateTime time = localTime;
            string timeStr = time.ToString("H:mm:ss.fff");
            string shortTimeStr = time.ToString("hh:mm:ss tt");

            Time = timeStr;
            ShortTime = shortTimeStr;
            Identifiers = ""; // GetFillIdenifiers(fill);
            Instrument = instrument;
            BuySell = buySell;
            InstrumentName = ""; // ParseInstrumentKey(fill.InstrumentKey.ToString());
            Quantity = quantity;
            Price = price;
            FFT2 = ""; // fill.FFT2;
            FFT3 = ""; // fill.FFT3;
            FillType = fillType;
            //_fill = fill;
            Originator = FillOriginator.TRADER;
            Action = FillAction.ADD;
            ReplacementForFill = null;
        }

        string GetFillIdenifiers(ezFill fill)
        {
            StringBuilder sb = new StringBuilder();

            // Start of Day
            if (fill.IsStartOfDay)  // S
                sb.Append("S ");

            // Open/Close
            if (fill.OpenClose == zOpenClose.Open)  // O
                sb.Append("O ");
            else if (fill.OpenClose == zOpenClose.Close)  // C
                sb.Append("C ");
            else if (fill.OpenClose == zOpenClose.Manual)  // M
                sb.Append("M ");
            else if (fill.OpenClose == zOpenClose.StartOfDay)  // (nothing - we already handle start of day)
            { /* do nothing */ }
            else
            {
                sb.Append("? ");
                Console.WriteLine("OpenClose: " + fill.OpenClose.ToString());
            }

            // Autospreader
            if (fill.IsAutospreaderLegFill)  //ASL
                sb.Append("ASL ");
            if (fill.IsAutospreaderSyntheticFill)  //ASS
                sb.Append("ASS ");
            if (fill.IsHedge)  //H
                sb.Append("H ");
            if (fill.IsQuoting)  //Q
                sb.Append("Q ");

            // ETS
            if (fill.IsExchangeSpreadLegFill)  //ETS
                sb.Append("ETS ");

            // SSE
            if (fill.IsSseFill)  //SSE
                sb.Append("SSE ");
            if (fill.IsSseChildFill)  //SSC
                sb.Append("SSC ");

            return sb.ToString();
        }

        string ParseInstrumentKey(string key)
        {
            string result = key;
            int i = key.IndexOf("(FUTURE)");

            if (i >= 0)
            {
                int i2 = key.IndexOf(" ");
                result = key.Substring(i2, (i - i2)).Trim();
            }

            string st = key.Substring(i + 8).Trim();
            if (key.StartsWith("CME"))
            {
                if (st.Length > 5)
                {
                    char chMonth = st[4];
                    char chYear = st[5];

                    int iMonth = (int)chMonth - 64;
                    int iYear = (int)chYear - 65;   // we subtract 1 extra here because 'A' represents the year 2000
                    if (iMonth >= 1 && iMonth <= 12 && iYear >= 0 && iYear <= 99)
                    {
                        DateTime dt = new DateTime(iYear, iMonth, 1);
                        string expiry = dt.ToString("MMMyy");
                        result += "  " + expiry;
                    }
                }
            }

            return result;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" " + Time + " ");
            sb.Append(" " + Identifiers + " ");
            sb.Append(" " + BuySell.ToString() + " ");
            sb.Append(" " + Quantity.ToInt().ToString() + " ");
            sb.Append(" " + InstrumentName + " ");
            sb.Append("@ " + Price + " ");
            return (sb.ToString());
        }
    } // class
} // namespace
