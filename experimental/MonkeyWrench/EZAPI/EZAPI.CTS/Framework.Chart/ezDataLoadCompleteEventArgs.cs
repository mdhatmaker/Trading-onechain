using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZAPI.Framework.Chart
{
    public class ezDataLoadCompleteEventArgs : EventArgs
    {
        public zDataLoadStatus Status
        {
            get { return status; }
        }

        public ezDateRange DateRangeRequested
        {
            get { return dateRangeRequested; }
        }

        public ezDateRange DateRangeProcessed
        {
            get { return dateRangeProcessed; }
        }

        private zDataLoadStatus status;
        private ezDateRange dateRangeRequested;
        private ezDateRange dateRangeProcessed;

        public ezDataLoadCompleteEventArgs(zDataLoadStatus status, ezDateRange requested, ezDateRange processed)
        {
            this.status = status;
            this.dateRangeRequested = requested;
            this.dateRangeProcessed = processed;
        }
    } // class
} // namespace
