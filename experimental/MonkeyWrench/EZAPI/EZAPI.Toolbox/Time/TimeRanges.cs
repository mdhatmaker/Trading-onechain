using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZAPI.Toolbox.Time
{
    /// <summary>
    /// TimeRanges is a collection of TimeRange objects. Again, these ranges represent ANY
    /// 24-hour period. They are not tied to a day of the week or a specific date.
    /// </summary>
    public class TimeRanges : ITimeRange, IEnumerable<TimeRange>
    {
        private List<TimeRange> ranges;

        public TimeRanges()
        {
            ranges = new List<TimeRange>();
        }

        public TimeRanges(string timeRangesToParse) : this()
        {
            // If there is only one time range represented by the string:
            if (!timeRangesToParse.Contains(','))
                Add(TimeRange.FromString(timeRangesToParse));
            else
            {
                string[] split = timeRangesToParse.Split(',');
                foreach (string range in split)
                {
                    Add(TimeRange.FromString(range));
                }
            }
        }

        public void Add(TimeRange range)
        {
            if (range != null)
                ranges.Add(range);
        }

        public bool ContainsTime(SimpleTime timeToCheck)
        {
            bool result = false;

            foreach (TimeRange range in ranges)
            {
                if (range.ContainsTime(timeToCheck))
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

            foreach (TimeRange range in ranges)
            {
                if (sb.Length != 0) sb.Append(',');
                sb.Append(range.ToString());
            }

            return sb.ToString();
        }

        public IEnumerator<TimeRange> GetEnumerator()
        {
            return ranges.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ranges.GetEnumerator();
        }
    } // class (TimeRanges)
} // namespace
