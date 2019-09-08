using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZAPI.Toolbox.Time
{
    /// <summary>
    /// A SimpleDateTime has Date AND Time information. It uses the year
    /// month and day to specify a Date. Then it uses the same hour, minute,
    /// second to specify the time (just like SimpleTime).
    /// </summary>
    public class SimpleDateTime : IComparable
    {
        public int Year { get { return year; } }
        public int Month { get { return month; } }
        public int Day { get { return day; } }
        public int Hour { get { return hour; } }
        public int Minute { get { return minute; } }
        public int Second { get { return Second; } }

        private int year;
        private int month;
        private int day;
        private int hour;
        private int minute;
        private int second;

        /// <summary>
        /// Create a SimpleDateTime using a year, month, day and 24-hour format for the hour.
        /// </summary>
        public SimpleDateTime(int year, int month, int day, int hour, int minute, int second)
        {
            this.year = year;
            this.month = month;
            this.day = day;
            this.hour = hour;
            this.minute = minute;
            this.second = second;
        }

        public int CompareTo(object obj)
        {
            if (!(obj is SimpleDateTime))
                throw new ArgumentException("object is not a SimpleDateTime");

            SimpleDateTime st2 = obj as SimpleDateTime;

            // Start off assuming a result of equality (zero).
            int result = 0;

            // Work our way through testing year, month, day, hour, minute, second.
            if (this.year > st2.year)
                result = 1;
            else if (this.year < st2.year)
                result = -1;
            else if (this.month > st2.month)
                result = 1;
            else if (this.month < st2.month)
                result = -1;
            else if (this.day > st2.day)
                result = 1;
            else if (this.day < st2.day)
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

        public static bool operator ==(SimpleDateTime st1, SimpleDateTime st2)
        {
            return st1.CompareTo(st2) == 0;
        }

        public static bool operator !=(SimpleDateTime st1, SimpleDateTime st2)
        {
            return st1.CompareTo(st2) != 0;
        }

        public static bool operator >(SimpleDateTime st1, SimpleDateTime st2)
        {
            return st1.CompareTo(st2) > 0;
        }

        public static bool operator <(SimpleDateTime st1, SimpleDateTime st2)
        {
            return st1.CompareTo(st2) < 0;
        }

        public static bool operator >=(SimpleDateTime st1, SimpleDateTime st2)
        {
            return st1.CompareTo(st2) >= 0;
        }

        public static bool operator <=(SimpleDateTime st1, SimpleDateTime st2)
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

            return Equals(obj as SimpleDateTime);
        }

        public bool Equals(SimpleDateTime dTime)
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
            return (this.year.Equals(dTime.year) && this.month.Equals(dTime.month) && this.day.Equals(dTime.day) && this.hour.Equals(dTime.hour) && this.minute.Equals(dTime.minute) && this.second.Equals(dTime.second));
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + year.GetHashCode();
            hash = (hash * 7) + month.GetHashCode();
            hash = (hash * 7) + day.GetHashCode();
            hash = (hash * 7) + hour.GetHashCode();
            hash = (hash * 7) + minute.GetHashCode();
            hash = (hash * 7) + second.GetHashCode();

            return hash;
        }

    } // class
} // namespace
