#define DEEPDEBUG
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EZAPI;
using EZAPI.Framework;
using EZAPI.Framework.Base;
using EZAPI.Framework.Chart;
using EZAPI.Toolbox.Debug;

namespace MonkeyLightning.DataProvider.Providers.Basic
{
    public class DataProviderChart : DataProviderBase
    {
        public override string Name { get { return "CHART DATA"; } }
        public override string DisplayFormat { get { return "{0}"; } }
        public override string Description { get { return "Choose values from bars on the chart (open, high, low, close) and from chart indicators (bollinger bands, moving average, etc)."; } }
        public override string EnglishDescription { get { return "TODO : Chart data description"; } }
        public override string[] PropertyNames
        {
            get
            {
                return new string[] {
            "CurrentValue",   // current value of the provider
            "SimpleDisplayFormat",  // display format to use when printing this data provider's value to the DPDisplay
            "SimpleDisplayDescription",     // (optional) description of the simple data to output
            "ChartDescriptorID",    // unique descriptor for the chart
            "BarIndex",         // index of the bar on the chart (0 = current bar)
            "SeriesName"   // name of the series selected by the user
        };
            }
        }

        //public override event ValueUpdateEventHandler DataUpdate;

        DPDisplaySimpleValue uiControl;
        DPModifyChart uiModifyControl;
        APIMain api;
        string chartDescriptorID;
        int barIndex;
        string selectedSeriesName;

        public DataProviderChart()
            : base()
        {
            uiControl = null;
            uiModifyControl = null;
            DataUpdate += DataProviderMarketData_DataUpdate;

            api = APIMain.Instance;
        }

        void DataProviderMarketData_DataUpdate(object sender, ValueUpdateEventArgs e)
        {
            if (uiControl != null) uiControl.Value = e.UpdatedValue;
        }

        private void DisconnectDisplayUIEvents()
        {
            DataUpdate -= DataProviderMarketData_DataUpdate;
        }

        public override void ClosingDisplayUI()
        {
            DisconnectDisplayUIEvents();
        }

        public override DPDisplayControl GetDisplayUserInterface()
        {
            if (uiControl == null)
                uiControl = new DPDisplaySimpleValue(PropertyValues);

            uiControl.InitializeControl();

            return uiControl;
        }

        public override DPModifyControl GetModifyUserInterface()
        {
            if (uiModifyControl == null)
                uiModifyControl = new DPModifyChart(PropertyValues);

            uiModifyControl.InitializeControl();

            return uiModifyControl;
        }

        public override void UpdateProviderFromPropertyValues()
        {
            chartDescriptorID = prop["ChartDescriptorID"];
            barIndex = prop["BarIndex"];
            selectedSeriesName = prop["SeriesName"];
            //descriptionText = prop["SimpleDisplayDescription"];
            UpdateDataValue(prop["CurrentValue"]);
        }

        public override RuleValueType ValueType
        {
            get { return RuleValueType.DOUBLE; }
        }

        private void UpdateDataValue(object updatedValue)
        {
            currentValue = updatedValue;
            FireValueUpdateEvent();
            //uiControl.Value = updatedValue;
        }

        public override object DataValue
        {
            get { return currentValue; }
        }
    } // class
} // namespace
