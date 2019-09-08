using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace EZAPI
{
    public class TTHelper
    {
        public static TTHelper Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TTHelper();
                return _instance;
            }
        }

        private static TTHelper _instance;
        private DateTimeFormatInfo _dtfi;

        private TTHelper()
        {
            // Get the en-US culture.
            CultureInfo ci = new CultureInfo("en-US");
            // Get the DateTimeFormatInfo for the en-US culture.
            _dtfi = ci.DateTimeFormat;
        }

        public List<string> GetMonthAbbreviations()
        {
            List<string> result = new List<string>();
            foreach (string name in _dtfi.AbbreviatedMonthGenitiveNames)
            {
                result.Add(name);
            }
            return result;
        }

        public bool IsMonthYear(string mmmYY)
        {
            if (mmmYY.Length != 5)
                return false;

            string month = mmmYY.Substring(0, 3);
            string year = mmmYY.Substring(3, 2);
            if (GetMonthAbbreviations().Contains(month) && char.IsDigit(year[0]) && char.IsDigit(year[1]))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Create a unique string based on the current time in ticks so we can "tag" our
        /// objects (i.e. sent orders using the Order.Tag field).
        /// </summary>
        /// <returns>A string representing a unique "tag" for this order</returns>
        /// <remarks>TTAPI seems to truncate the order tags at 15 characters so we account for that</remarks>
        public static string GetUniqueTag()
        {
            string timeStr = DateTime.Now.Ticks.ToString();
            // Apparently order tag can only be a maximum of 15 chars
            if (timeStr.Length > 15)
                timeStr = timeStr.Substring(timeStr.Length - 15);
            return timeStr;
        }
    } // class
} // namespace
