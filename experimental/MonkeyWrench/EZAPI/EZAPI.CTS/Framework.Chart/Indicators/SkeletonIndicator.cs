using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MSChart = System.Windows.Forms.DataVisualization.Charting;
using EZAPI.Toolbox.DataStructures;

namespace EZAPI.Framework.Chart.Indicators
{
    public class IndicatorName : Indicator
    {
        public IndicatorName(string name = "INDICATOR NAME", string description = "", string moreInfoWebPage = "") : base(name)
        {
            // If we are passed a blank description or web page, then use our default values.
            // If 'null' is passed to the constructor for either of those values, they stay null (in case the user of this class doesn't want them displayed).
            if (description == "")
                description = "DESCRIPTION";
            if (moreInfoWebPage == "")
                moreInfoWebPage = "MOREINFOWEBPAGE";

            // ADD PARAMETERS HERE

            this["Color"] = Color.Navy;
            this["Line Style"] = zChartLineStyle.Solid;
        }

        /// <summary>
        /// Override the Draw method to display the indicator.
        /// </summary>
        public override void Draw(MSChart.Chart chart1, bool enable = true)
        {
        }

    } // class
} // namespace
