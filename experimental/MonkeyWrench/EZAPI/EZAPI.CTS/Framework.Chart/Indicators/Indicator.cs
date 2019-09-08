using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MSChart = System.Windows.Forms.DataVisualization.Charting;

namespace EZAPI.Framework.Chart.Indicators
{
    public enum IndicatorChartArea { MainChart, BottomChart }

    public abstract class Indicator
    {
        public string Name { get { return indicatorName; } }
        public string Description { get { return description; } }
        public string MoreInfoWebPage { get { return moreInfoWebPage; } }
        public IndicatorParameterList Parameters { get { return parameters; } }
        public IndicatorChartArea IndicatorType { get { return indicatorType; } }

        protected MSChart.DataPoint EmptyDP
        {
            get
            {
                MSChart.DataPoint emptyDP = new MSChart.DataPoint();
                emptyDP.IsEmpty = true;
                return emptyDP;
            }
        }

        protected IndicatorParameterList parameters;
        protected string indicatorName;
        protected string description;
        protected string moreInfoWebPage;
        protected IndicatorChartArea indicatorType;

        protected Indicator()
        {
        }

        public Indicator(string indicatorName)
        {
            this.indicatorName = indicatorName;

            parameters = new IndicatorParameterList();

            // Every Indicator will have a "Color" parameter.
            parameters.Add(new IndicatorParameter<Color>("Color", "Color of the indicator.", Color.Blue));

            // Every Indicator will have a "Line Style" parameter.
            parameters.Add(new IndicatorParameter<zChartLineStyle>("Line Style", "Line style of indicator (solid, dash, dot, etc).", zChartLineStyle.Solid));
        }

        public void AddParameter(IIndicatorParameter parameter)
        {
            parameters.Add(parameter);
        }

        public object this[string parameterName]
        {
            get { return parameters[parameterName].Value; }
            set { parameters[parameterName].Value = value; }
        }



        public abstract void Draw(MSChart.Chart chart1, bool enable = true);



        protected void EnableOneBottomSeries(MSChart.Chart chart, string seriesName)
        {
            // Hide all series (in the bottom chart area) except for this one.
            foreach (MSChart.Series series in chart.Series)
            {
                // We use "StartsWith" rather than straight equality so you can have multiple series enabled
                // such as "Forecasting", "Forecasting Upper Error", "Forecasting Lower Error".
                if (series.ChartArea == "ChartAreaBottom" && !series.Name.StartsWith(seriesName))
                    series.Enabled = false;
                else if (series.ChartArea == "ChartAreaBottom" && series.Name.StartsWith(seriesName))
                    series.Enabled = true;
            }
        }

        protected void RescaleAxes(MSChart.Chart chart, string chartAreaName = "ChartAreaBottom")
        {
            if (chartAreaName == "ChartAreaBottom")
            {
                // Update the axes scale for this indicator.
                chart.ChartAreas["ChartAreaBottom"].AxisY.Maximum = Double.NaN;
                chart.ChartAreas["ChartAreaBottom"].AxisY.Minimum = Double.NaN;
                chart.ChartAreas["ChartAreaBottom"].RecalculateAxesScale();
            }
            else
            {
                chart.ChartAreas[chartAreaName].RecalculateAxesScale();
            }
        }

        /// <summary>
        /// The chart data always needs to be "aligned," which is to say that the number of data points must
        /// match. This method takes the "parent" (main series) and tweaks the "child" (subordinate series such
        /// as an indicator that we have created). We will "pad" the child series using empty data points.
        /// </summary>
        /// <param name="parent">main chart series - contains the target count for datapoints</param>
        /// <param name="child">subordinate chart series - we will tweak it to match the "parent" series datapoint count</param>
        /// <param name="pad">side on which to pad with empty data points - PadLeft is the default</param>
        protected void AlignChartData(MSChart.Series parent, MSChart.Series child, PadSide pad = PadSide.PadLeft)
        {
            int pointsToAddCount = parent.Points.Count - child.Points.Count;

            switch (pad)
            {
                case PadSide.PadLeft:
                    // In some instances, we may need to REMOVE points from the child to get the series aligned.
                    if (pointsToAddCount >= 0)
                    {
                        // Add points to the LEFT side of the child series.
                        for (int i = 0; i < pointsToAddCount; i++)
                        {
                            // Create an empty data point.
                            MSChart.DataPoint dpEmpty = new MSChart.DataPoint();
                            dpEmpty.IsEmpty = true;

                            child.Points.Insert(0, dpEmpty);
                        }
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                    break;
                case PadSide.PadRight:
                    throw new NotImplementedException();
                    break;
            }

            // Go through the chart data points and make sure the X values are aligned.
            for (int i = 0; i < parent.Points.Count; i++)
            {
                child.Points[i].XValue = parent.Points[i].XValue;
            }
        }

        #region Equals and == overrides
        public override int GetHashCode()
        {
            unchecked
            {
                var result = 0;
                result = (result * 397) ^ parameters.Count;
                return result;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Indicator ind = obj as Indicator;
            if ((System.Object)ind == null)
                return false;

            return Equals(ind);
        }

        public bool Equals(Indicator ind)
        {
            bool result = true;

            if ((object)ind == null)
                return false;

            if (Name != ind.Name)
                result = false;
            else if (Description != ind.Description)
                result = false;
            else if (MoreInfoWebPage != ind.MoreInfoWebPage)
                result = false;
            else if (parameters != ind.parameters)
                result = false;

            return result;
        }

        public static bool operator ==(Indicator ind1, Indicator ind2)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(ind1, ind2))
                return true;

            // If one is null, but not both, return false.
            if (((object)ind1 == null) || ((object)ind2 == null))
                return false;

            return ind1.Equals(ind2);
        }

        public static bool operator !=(Indicator ind1, Indicator ind2)
        {
            return !(ind1 == ind2);
        }
        #endregion

    } // class

    public class IndicatorMap
    {
        public Dictionary<string, Indicator>.KeyCollection Keys { get { return indicators.Keys; } }
        public Dictionary<string, Indicator>.ValueCollection Values { get { return indicators.Values; } }

        private Dictionary<string, Indicator> indicators;

        public IndicatorMap()
        {
            indicators = new Dictionary<string, Indicator>();
        }

        public void Clear()
        {
            indicators.Clear();
        }

        public void Add(Indicator indicator)
        {
            if (indicators.ContainsKey(indicator.Name))
                throw new ArgumentException(string.Format("This IndicatorMap already contains an indicator named '{0}'.", indicator.Name), "Indicator.Name");

            indicators[indicator.Name] = indicator;
        }

        public Indicator this[string key]
        {
            get { return indicators[key]; }
        }

        #region Equals and == overrides
        public override int GetHashCode()
        {
            unchecked
            {
                var result = 0;
                result = (result * 397) ^ indicators.Count;
                return result;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            IndicatorMap imap = obj as IndicatorMap;
            if ((System.Object)imap == null)
                return false;

            return Equals(imap);
        }

        public bool Equals(IndicatorMap imap)
        {
            bool result = true;

            if ((object)imap == null)
                return false;

            if (indicators.Count != imap.indicators.Count)
                result = false;
            else
            {
                foreach (string key in indicators.Keys)
                {
                    if (!imap.indicators.ContainsKey(key) || indicators[key] != imap.indicators[key])
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        public static bool operator ==(IndicatorMap imap1, IndicatorMap imap2)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(imap1, imap2))
                return true;

            // If one is null, but not both, return false.
            if (((object)imap1 == null) || ((object)imap2 == null))
                return false;

            return imap1.Equals(imap2);
        }

        public static bool operator !=(IndicatorMap imap1, IndicatorMap imap2)
        {
            return !(imap1 == imap2);
        }
        #endregion


    }

} // namespace
