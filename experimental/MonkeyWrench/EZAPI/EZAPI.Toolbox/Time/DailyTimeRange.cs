using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZAPI.Toolbox.Time
{

    public class DailyTimeRange : ITimeRange, ICloneable 
    {
        static readonly string timeFormat = "h:mm:ss tt";

        public zDayOfWeek WeekDay { get; set; }
        public TimeRanges TimeRanges { get { return timeRanges; } }

        private TimeRanges timeRanges;

        /*public DailyTimeRange()
        {
            timeRanges = new TimeRanges();
        }*/

        /*public DailyTimeRange(string rangesToParse) : this()
        {
            if (rangesToParse == null)
                return;

            // Split up the ranges in the string (comma-separated). If there is no
            // comma in the string then the string represents a single range.
            string[] individualRanges;
            if (!rangesToParse.Contains(','))
                individualRanges = new string[] { rangesToParse };
            else
                individualRanges = rangesToParse.Split(',');

            // Now process each range individually. Should be in the format "7a - 8p".
            foreach (string range in individualRanges)
            {

            }
        }*/

        // Create a DailyTimeRange with ONE OR MORE TimeRange objects passed to the constructor.
        public DailyTimeRange(zDayOfWeek weekDay, TimeRange range, params TimeRange[] ranges)
        {
            timeRanges = new TimeRanges();

            WeekDay = weekDay;
            timeRanges.Add(range);
            foreach (TimeRange tr in ranges)
            {
                timeRanges.Add(tr);
            }
        }

        // Add a time range to the ranges represented by this DailyTimeRange.
        public void AddTimeRange(TimeRange tr)
        {
            timeRanges.Add(tr);
        }

        // Add ALL of the time ranges from an existing DailyTimeRange.
        public void AddTimeRange(DailyTimeRange dtr)
        {
            foreach (TimeRange tr in dtr.timeRanges)
            {
                AddTimeRange(tr);
            }
        }

        public bool ContainsTime(SimpleTime st)
        {
            bool result = false;

            foreach (TimeRange tr in timeRanges)
            {
                if (tr.ContainsTime(st))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(WeekDay.ToString() + ":");

            foreach (var tr in timeRanges)
            {
                // Add a comma if this is not the first time range we are listing.
                if (sb.Length > 0) sb.Append(",");

                sb.Append(tr.BeginTime.ToString() + "-" + tr.EndTime.ToString());
            }

            return sb.ToString();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    } // class



} // namespace
