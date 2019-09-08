using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZAPI.Toolbox.DataStructures
{
    public static class ExtensionMethods
    {
        public static DateTime AddTradeDays(this DateTime startDate, double days)
        {
            return startDate.AddDays(days);
        }

        public static DateTime AddTradeHours(this DateTime startDate, double hours)
        {
            return startDate.AddHours(hours);
        }

    } // class
} // namespace
