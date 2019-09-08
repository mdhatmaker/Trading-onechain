using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZAPI.Toolbox.Time
{
    /// <summary>
    /// A SimpleTime is just an hour, minute and second. No Day or Date information.
    /// Essentially, a SimpleTime is a hh:mm:ss designation within a 24-hour period.
    /// It can be ANY 24-hour period. You must use other classes if you want additional
    /// information such as "it has to be a Monday" (SimpleDayTime) or "this 24-hour
    /// period is on Oct 13th, 2015" (SimpleDateTime).
    /// </summary>
    public class SimpleTime : IComparable 
    {
        public int Hour { get { return hour; } }
        public int Minute { get { return minute; } }
        public int Second { get { return Second; } }

        private int hour;
        private int minute;
        private int second;

        /// <summary>
        /// Create a SimpleTime using 24-hour format for the hour.
        /// </summary>
        public SimpleTime(int hour, int minute, int second)
        {
            this.hour = hour;
            this.minute = minute;
            this.second = second;
        }

        /// <summary>
        /// Create a SimpleTime from a string in a format such as "7a" or "9p".
        /// </summary>
        /// <param name="timeToParse"></param>
        public SimpleTime(string timeToParse)
        {
            string str = timeToParse.ToUpper().Trim();

            if (str.Length < 2)
                throw new FormatException("Error parsing a string for the SimpleTime constructor.");
            else
            {
                string hourStr;
                // Start out with the outlier case of "12m" (for midnight).
                if (str == "12M")
                {
                    // We are going to make "12 midnight" equal to 23:59:59 (the very end of the 24-hour day).
                    this.hour = 23;
                    this.minute = 59;
                    this.second = 59;
                }
                else if (str.EndsWith("A"))
                {
                    hourStr = str.Substring(0, str.Length - 1);
                    this.hour = Convert.ToInt32(hourStr);
                    // One more check to handle "12am" (which should be zero in our 24-hour time format)...
                    if (this.hour == 12)
                        this.hour = 0;
                }
                else if (str.EndsWith("AM"))
                {
                    hourStr = str.Substring(0, str.Length - 2);
                    this.hour = Convert.ToInt32(hourStr);
                    // One more check to handle "12am" (which should be zero in our 24-hour time format)...
                    if (this.hour == 12)
                        this.hour = 0;
                }
                else if (str.EndsWith("P"))
                {
                    hourStr = str.Substring(0, str.Length - 1);
                    this.hour = Convert.ToInt32(hourStr) + 12;  // 24-hour format for hour, so add 12 for PM
                }
                else if (str.EndsWith("PM"))
                {
                    hourStr = str.Substring(0, str.Length - 2);
                    this.hour = Convert.ToInt32(hourStr) + 12;  // 24-hour format for hour, so add 12 for PM
                }
                else
                    throw new FormatException("Error parsing a string for the SimpleTime constructor.");

                this.minute = 0;
                this.second = 0;
            
            }

        }

        public int CompareTo(object obj)
        {
            if (!(obj is SimpleTime))
                throw new ArgumentException("object is not a SimpleTime");

            SimpleTime st2 = obj as SimpleTime;

            // Start off assuming a result of equality (zero).
            int result = 0;

            // Work our way through testing hour, minute, second.
            if (this.hour > st2.hour)
                result = 1;
            else if (this.hour < st2.hour)
                result = -1;
            else if (this.minute > st2.minute)
                result = 1;
            else if (this.minute < st2.minute)
                result = -1;
            else if (this.second > st2.second)
                result = 1;
            else if (this.second < st2.second)
                result = -1;

            return result;
        }

        public static bool operator ==(SimpleTime st1, SimpleTime st2)
        {
            return (st1.hour == st2.hour && st1.minute == st2.minute && st1.second == st2.second);
        }

        public static bool operator !=(SimpleTime st1, SimpleTime st2)
        {
            return (st1.hour != st2.hour || st1.minute != st2.minute || st1.second != st2.second);
        }

        public static bool operator >(SimpleTime st1, SimpleTime st2)
        {
            return st1.CompareTo(st2) > 0;
        }

        public static bool operator <(SimpleTime st1, SimpleTime st2)
        {
            return st1.CompareTo(st2) < 0;
        }

        public static bool operator >=(SimpleTime st1, SimpleTime st2)
        {
            return st1.CompareTo(st2) >= 0;
        }

        public static bool operator <=(SimpleTime st1, SimpleTime st2)
        {
            return st1.CompareTo(st2) <= 0;
        }

        public override bool Equals(object obj)
        {
            // Check for null.
            if (obj == null)
                return false;

            // Check for equivalent data types.
            if (this.GetType() != obj.GetType())
                return false;

            return Equals(obj as SimpleTime);
        }

        public bool Equals(SimpleTime time)
        {
            // Check for null.
            if (time == null)
                return false;

            // Check for ReferenceEquals if this is a reference type.
            if (ReferenceEquals(this, time))
                return true;

            // Possibly check for equivalent hash codes.
            if (this.GetHashCode() != time.GetHashCode())
                return false;

            // Check base.Equals if base overrides Equals().
            System.Diagnostics.Debug.Assert(base.GetType() != typeof(object));

            if (!base.Equals(time))
                return false;

            // Compare identifying fields for equality.
            return (this.hour.Equals(time.hour) && this.minute.Equals(time.minute) && this.second.Equals(time.second));
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + hour.GetHashCode();
            hash = (hash * 7) + minute.GetHashCode();
            hash = (hash * 7) + second.GetHashCode();

            return hash;
        }

        public override string ToString()
        {
            int adjustedHour = hour;
            string ampm = "a";
            if (hour > 12)
            {
                adjustedHour = hour - 12;
                ampm = "p";
            }
            return string.Format("{0:0}:{1:00}:{2:00} {3}", adjustedHour, minute, second, ampm);
        }

        public static SimpleTime FromDateTime(DateTime dt)
        {
            return new SimpleTime(dt.Hour, dt.Minute, dt.Second);
        }

    } // class (SimpleTime)
} // namespace
