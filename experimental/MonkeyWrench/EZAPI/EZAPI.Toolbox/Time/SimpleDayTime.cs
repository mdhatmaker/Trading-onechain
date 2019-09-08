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
    public class SimpleDayTime : IComparable
    {
        public zDayOfWeek WeekDay { get { return WeekDay; } }
        public int Hour { get { return hour; } }
        public int Minute { get { return minute; } }
        public int Second { get { return Second; } }

        private zDayOfWeek weekDay;
        private int hour;
        private int minute;
        private int second;

        /// <summary>
        /// Create a SimpleDayTime using a day of the week and 24-hour format for the hour.
        /// </summary>
        public SimpleDayTime(zDayOfWeek weekDay, int hour, int minute, int second)
        {
            this.weekDay = weekDay;
            this.hour = hour;
            this.minute = minute;
            this.second = second;
        }

        public int CompareTo(object obj)
        {
            if (!(obj is SimpleDayTime))
                throw new ArgumentException("object is not a SimpleDayTime");

            SimpleDayTime st2 = obj as SimpleDayTime;

            // Start off assuming a result of equality (zero).
            int result = 0;

            // Work our way through testing weekday, hour, minute, second.
            if (this.weekDay > st2.weekDay)
                result = 1;
            else if (this.weekDay < st2.weekDay)
                result = -1;
            else if (this.hour > st2.hour)
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

        public static bool operator ==(SimpleDayTime st1, SimpleDayTime st2)
        {
            return st1.CompareTo(st2) == 0;
        }

        public static bool operator !=(SimpleDayTime st1, SimpleDayTime st2)
        {
            return st1.CompareTo(st2) != 0;
        }

        public static bool operator >(SimpleDayTime st1, SimpleDayTime st2)
        {
            return st1.CompareTo(st2) > 0;
        }

        public static bool operator <(SimpleDayTime st1, SimpleDayTime st2)
        {
            return st1.CompareTo(st2) < 0;
        }

        public static bool operator >=(SimpleDayTime st1, SimpleDayTime st2)
        {
            return st1.CompareTo(st2) >= 0;
        }

        public static bool operator <=(SimpleDayTime st1, SimpleDayTime st2)
        {
            return st1.CompareTo(st2) <= 0;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            // Check for null.
            if (obj == null)
                return false;

            // Check for equivalent data types.
            if (this.GetType() != obj.GetType())
                return false;

            return Equals(obj as SimpleDayTime);
        }

        public bool Equals(SimpleDayTime dTime)
        {
            // Check for null.
            if (dTime == null)
                return false;

            // Check for ReferenceEquals if this is a reference type.
            if (ReferenceEquals(this, dTime))
                return true;

            // Possibly check for equivalent hash codes.
            if (this.GetHashCode() != dTime.GetHashCode())
                return false;

            // Check base.Equals if base overrides Equals().
            System.Diagnostics.Debug.Assert(base.GetType() != typeof(object));

            if (!base.Equals(dTime))
                return false;

            // Compare identifying fields for equality.
            return (this.weekDay.Equals(dTime.weekDay) && this.hour.Equals(dTime.hour) && this.minute.Equals(dTime.minute) && this.second.Equals(dTime.second));
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + weekDay.GetHashCode();
            hash = (hash * 7) + hour.GetHashCode();
            hash = (hash * 7) + minute.GetHashCode();
            hash = (hash * 7) + second.GetHashCode();

            return hash;
        }

    } // class
} // namespace
