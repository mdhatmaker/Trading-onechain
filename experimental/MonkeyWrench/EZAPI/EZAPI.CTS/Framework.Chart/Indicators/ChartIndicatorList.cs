using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZAPI.Framework.Chart.Indicators
{
    public abstract class ChartIndicatorList
    {
        public event Action<double> IndicatorValueChanged;

        public IndicatorMap Indicators { get; private set; }

        protected ChartIndicatorList()
        {
            Indicators = new IndicatorMap();
        }

        public Indicator this[string indicatorName]
        {
            get
            {
                return Indicators[indicatorName];
            }
        }

    } // class
} // namespace
