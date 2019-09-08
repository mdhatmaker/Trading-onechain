using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZAPI.Toolbox.Debug;

namespace EZAPI.Toolbox.Time
{
    /// <summary>
    /// A single StartTime/EndTime range within a 24-hour period (ANY 24-hour period).
    /// TimeRange is NOT tied to a day of the week or a specific date.
    /// </summary>
    public class TimeRange : ITimeRange
    {
        public SimpleTime BeginTime
        {
            get { return beginTime; }
            set
            {
                // Ensure that the StartTime set by the user is going to be <= the EndTime.
                if (value <= endTime)
                    beginTime = value;
                else
                {
                    // Setting the StartTime would have resulted in a case where the StartTime was
                    // greater than the EndTime, so just make them equal.
                    beginTime = value;
                    endTime = value;
                }
            }
        }
        public SimpleTime EndTime
        {
            get { return endTime; }
            set
            {
                // Ensure that the EndTime set by the user is going to be >= the StartTime.
                if (value >= beginTime)
                    endTime = value;
                else
                {
                    // Setting the EndTime would have resulted in a case where the EndTime was
                    // less than the StartTime, so just make them equal.
                    beginTime = value;
                    endTime = value;
                }
            }
        }

        private SimpleTime beginTime;
        private SimpleTime endTime;

        public TimeRange()
        {
            // Default StartTime to very beginning of day and EndTime to very end of day.
            beginTime = new SimpleTime(0, 0, 0);
            endTime = new SimpleTime(23, 59, 59);
        }

        /// <summary>
        /// This constructor should create a TimeRange from a string representation.
        /// </summary>
        /// <param name="timeRangeToParse">string in format like "7a - 9p"</param>
        /*public TimeRange(string timeRangeToParse)
        {
            // Don't forget that midnight can look different (i.e. "12am" rather than "12a").
            string[] startStopTime = timeRangeToParse.Split('-');
            string startTimeStr = startStopTime[0];
            string stopTimeStr = startStopTime[1];

            beginTime = new SimpleTime(startTimeStr);
            endTime = new SimpleTime(stopTimeStr);
        }*/

        /// <summary>
        /// This static method replaces a constructor. It creates a TimeRange from a
        /// string representation. Using this static method improves on the 
        /// constructor method because now I can return a null if the string passed
        /// to this method is empty ("") or doesn't represent a valid TimeRange.
        /// Passing an empty string (or faulty string to parse) will return a null 
        /// instead of returning a TimeRange object.
        /// </summary>
        /// <param name="timeRangeToParse">string in format like "7a - 9p"</param>
        public static TimeRange FromString(string timeRangeToParse)
        {
            if (timeRangeToParse.Trim() == "")
                return null;

            TimeRange result = new TimeRange();

            try
            {
                // Don't forget that midnight can look different (i.e. "12am" rather than "12a").
                string[] startStopTime = timeRangeToParse.Split('-');
                string startTimeStr = startStopTime[0];
                string stopTimeStr = startStopTime[1];

                result.BeginTime = new SimpleTime(startTimeStr);
                result.EndTime = new SimpleTime(stopTimeStr);
            }
            catch (Exception ex)
            {
                result = null;
                ExceptionHandler.TraceException(ex);
            }

            return result;
        }

        public bool ContainsTime(SimpleTime timeToCheck)
        {
            return beginTime <= timeToCheck && endTime >= timeToCheck;
        }

        public override string ToString()
        {
            return beginTime.ToString() + " - " + endTime.ToString();
        }

    } // class


} // namespace
