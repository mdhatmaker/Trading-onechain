//#define DESIGNER
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using EZAPI;
using EZAPI.Framework;
using EZAPI.Framework.Base;
using EZAPI.Framework.Chart;
using EZAPI.Framework.Chart.Indicators;
using EZAPI.Toolbox;
using EZAPI.Toolbox.Debug;
using MonkeyLightning.DataProvider;

namespace MonkeyLightning.DataProvider.Providers.Basic
{
    public partial class DPModifyChart
#if DESIGNER
        : DPModifyControlStub
#else
        : DPModifyControl
#endif
    {
        public event EventHandler SelectedValueChanged;

        public override string ControlName { get { return "ModifyChart"; } }

        public DPModifyChart(KeyValueCollection propertyValues) : base(propertyValues)
        {
            InitializeComponent();
            uiControlChart1.BarSelectEnabled = true;
        }

        public override void ParentFormClosing()
        {
            base.ParentFormClosing();
            //api.OnInsideMarketUpdate -= api_OnInsideMarketUpdate;

            // Store this chart in the Chart Pool.
            ChartPool.Instance.Add(uiControlChart1.ChartDescriptor, uiControlChart1);
        }

        public override void UpdateUIFromPropertyValues()
        {
            //string SelectedValue { get { return selectedValue; } }
            //EZInstrument SelectedInstrument { get { return currentInstrument; } }
        }

        public override void UpdatePropertyValuesFromUI()
        {
            PropertyValues["ChartDescriptorID"] = uiControlChart1.ChartDescriptorID;
            PropertyValues["BarIndex"] = uiControlChart1.ReverseBarIndex;   // We want the type of index where zero is the current bar
            PropertyValues["SeriesName"] = uiControlChart1.SelectedSeries;
            PropertyValues["CurrentValue"] = uiControlChart1.CurrentValue;
            PropertyValues["SimpleDisplayDescription"] = uiControlChart1.SelectedSeries + ": ";
        }


    
    } // class
} // namespace
