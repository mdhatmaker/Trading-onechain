using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZAPI.Toolbox.DataStructures
{
    public class EconomicNumber
    {
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public zTimeZone TimeZone { get; set; }
        public zCurrency Currency { get; set; }
        public string Event { get; set; }
        public zImportance Importance { get; set; }
        public string Actual { get; set; }
        public string Forecast { get; set; }
        public string Previous { get; set; }

        public override string ToString()
        {
            return string.Format("Econ #: {0:ddd MMM d} {1:h:mm} {2} | {3} | {4} | {5} | {6} {7} {8}", Date, Time, TimeZone, Currency, Event, Importance, Actual, Forecast, Previous);
        }

    } // class
} // namespace
