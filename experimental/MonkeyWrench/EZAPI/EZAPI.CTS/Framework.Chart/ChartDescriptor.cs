using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZAPI.Framework.Chart.Indicators;

namespace EZAPI.Framework.Chart
{
    public class ChartDescriptor
    {
        public string UniqueID { get; private set; }

        public UIControlChart ChartControl { get; private set; }

        private IChartDataProvider provider;
        private object chartIdentifier;
        private ezBarInterval interval;
        private DateTime startDate;
        private DateTime endDate;
        private IndicatorMap indicators;

        public ChartDescriptor(object chartIdentifier, ezBarInterval interval, DateTime startDate, DateTime endDate, IndicatorMap indicators)        
        {
            this.chartIdentifier = chartIdentifier;
            this.interval = interval;
            this.startDate = startDate;
            this.endDate = endDate;
            this.indicators = indicators;

            this.UniqueID = DateTime.Now.Ticks.ToString();
        }

        public ChartDescriptor(IChartDataProvider provider, IndicatorMap indicators)
            : this(provider.ChartIdentifier, provider.BarInterval, provider.StartDate, provider.EndDate, indicators)
        {

        }

        public void CreateChart()
        {
            //IChartDataProvider provider = new ChartDataProviderCTS(instrument.Name + " : " + interval, interval, ezSessionTimeRange.Empty);
            //chartForm = new ChartDataForm(provider);
            //WinForms.SetWaitCursor(true);
            //provider.LoadHistoricalChartData(APIMain.MarketFromInstrument(instrument), dtStartDate, dtEndDate);
        }

        #region Equals and == overrides
        public override int GetHashCode()
        {
            unchecked
            {
                var result = 0;
                result = (result * 397) ^ Convert.ToInt32(startDate.ToOADate());
                result = (result * 397) ^ Convert.ToInt32(endDate.ToOADate());
                return result;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            ChartDescriptor desc = obj as ChartDescriptor;
            if ((System.Object)desc == null)
                return false;

            return Equals(desc);
        }

        public bool Equals(ChartDescriptor desc)
        {
            bool result = true;

            if ((object)desc == null)
                return false;

            if (chartIdentifier != desc.chartIdentifier)
                result = false;
            else if (interval != desc.interval)
                result = false;
            else if (startDate != desc.startDate)
                result = false;
            else if (endDate != desc.endDate)
                result = false;
            else if (indicators != desc.indicators)
                result = false;

            return result;
        }

        public static bool operator ==(ChartDescriptor desc1, ChartDescriptor desc2)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(desc1, desc2))
                return true;

            // If one is null, but not both, return false.
            if (((object)desc1 == null) || ((object)desc2 == null))
                return false;

            return desc1.Equals(desc2);
        }

        public static bool operator !=(ChartDescriptor desc1, ChartDescriptor desc2)
        {
            return !(desc1 == desc2);
        }
        #endregion


    } // class
} // namespace
