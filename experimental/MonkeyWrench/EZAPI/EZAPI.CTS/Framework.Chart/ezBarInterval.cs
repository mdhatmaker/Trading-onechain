using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZAPI.Framework.Chart
{
    public class ezBarInterval
    {
        public zChartInterval Interval { get { return interval; } }
        public int Period { get { return period; } }

        zChartInterval interval;
        int period;

        public ezBarInterval(zChartInterval interval, int period)
        {
            this.interval = interval;
            this.period = period;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", period, interval);
        }

        #region Equals and == overrides
        public override int GetHashCode()
        {
            unchecked
            {
                var result = 0;
                result = (result * 397) ^ period;
                return result;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            ezBarInterval ezb = obj as ezBarInterval;
            if ((System.Object)ezb == null)
                return false;

            return Equals(ezb);
        }

        public bool Equals(ezBarInterval ezb)
        {
            bool result = true;

            if ((object)ezb == null)
                return false;

            if (interval != ezb.interval)
                result = false;
            else if (period != ezb.period)
                result = false;

            return result;
        }

        public static bool operator ==(ezBarInterval ezb1, ezBarInterval ezb2)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(ezb1, ezb2))
                return true;

            // If one is null, but not both, return false.
            if (((object)ezb1 == null) || ((object)ezb2 == null))
                return false;

            return ezb1.Equals(ezb2);
        }

        public static bool operator !=(ezBarInterval ezb1, ezBarInterval ezb2)
        {
            return !(ezb1 == ezb2);
        }
        #endregion

    } // class
} // namespace
