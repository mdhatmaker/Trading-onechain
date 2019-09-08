using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using EZAPI.Toolbox.Serialization;

namespace EZAPI.Toolbox.Time
{
    public class WeeklyTimeRange : ITimeRange
    {
        public Dictionary<zDayOfWeek, DailyTimeRange> TimeRanges { get { return dailyTimeRanges; } }

        private Dictionary<zDayOfWeek, DailyTimeRange> dailyTimeRanges;
        
        public WeeklyTimeRange()
        {
            dailyTimeRanges = new Dictionary<zDayOfWeek, DailyTimeRange>();
        }

        public void AddTimeRange(zDayOfWeek weekDay, TimeRange tr)
        {
            // Don't add a null TimeRange.
            if (tr == null)
                return;

            // If there is not already a DailyTimeRange for this WeekDay, then
            // create a new DailyTimeRange for the WeekDay.
            if (!dailyTimeRanges.ContainsKey(weekDay) || dailyTimeRanges[weekDay] == null)
            {
                dailyTimeRanges[weekDay] = new DailyTimeRange(weekDay, tr);
            }
            else
            {
                // ADD TO our existing DailyTimeRange for this WeekDay.
                DailyTimeRange existingDTR = dailyTimeRanges[weekDay];
                existingDTR.AddTimeRange(tr);
            }
        }

        /*
        // The WeekDay (Monday, Tuesday, etc) is contained WITHIN the DailyTimeRange object.
        public void AddDailyTimeRange(DailyTimeRange dtr)
        {
            // Don't add a null TimeRange.
            if (dtr == null)
                return;

            // If there is not already a DailyTimeRange for the specified WeekDay, then
            // set our DailyTimeRange for the WeekDay to the specified DailyTimeRange.
            if (dailyTimeRanges[dtr.WeekDay] == null)
            {
                // We are going to use a CLONE of the DailyTimeRange so we can modify it within
                // our WeeklyTimeRange (such as adding TimeRanges to it) without affecting the
                // original DailyTimeRange object.
                dailyTimeRanges[dtr.WeekDay] = dtr.Clone() as DailyTimeRange;
            }
            else
            {
                // ADD TO our existing DailyTimeRange for this WeekDay.
                DailyTimeRange existingDTR = dailyTimeRanges[dtr.WeekDay];

                foreach (TimeRange tr in dtr.TimeRanges)
                {
                    existingDTR.AddTimeRange(tr);
                }
            }
        }*/

        public bool ContainsTime(SimpleTime st)
        {
            bool result = false;

            foreach (DailyTimeRange dtr in dailyTimeRanges.Values)
            {
                if (dtr.ContainsTime(st))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        public override string ToString()
        {
            return "(weekly time range)";
        }


    } // class
} // namespace
