using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EZAPI.Framework.Base;
using EZAPI.Toolbox.Time;

namespace MonkeyLightning.DataProvider.Providers.Basic
{
    public class DataProviderTimeRange : DataProviderBase
    {
        public override string DisplayFormat { get { return "{0}"; } }
        public override string Name { get { return "DAILY TIME RANGE"; } }
        public override string Description { get { return "Allows you to choose a daily time range (useful for establishing times during the day in which certain trades will be active)."; } }
        public override string EnglishDescription { get { return "TODO: daily time range description."; } }
        public override string[] PropertyNames { get { return new string[] { "SimpleDisplay", "ranges" }; } }

        DPDisplaySimpleDescription uiControl;
        DPModifyTimeRange uiModifyControl;

        public DataProviderTimeRange()
            : base()
        {
            uiControl = null;
            uiModifyControl = null;
        }

        void uiControl_ValueChanged(object sender, EventArgs e)
        {
            currentValue = uiControl.Value;
            FireValueUpdateEvent();
        }

        public override DPDisplayControl GetDisplayUserInterface()
        {
            if (uiControl == null)
                uiControl = new DPDisplaySimpleDescription(PropertyValues);

            uiControl.InitializeControl();

            return uiControl;
        }

        public override DPModifyControl GetModifyUserInterface()
        {
            if (uiModifyControl == null)
                uiModifyControl = new DPModifyTimeRange(PropertyValues);

            uiModifyControl.InitializeControl();

            return uiModifyControl;
        }

        public override void UpdateProviderFromPropertyValues()
        {
            prop["SimpleDisplay"] = prop["ranges"];
            string ranges = prop["ranges"];

            currentValue = new TimeRanges(ranges);
            FireValueUpdateEvent();
        }
      
        public override RuleValueType ValueType
        {
            get { return RuleValueType.TIME_RANGE; }
        }

        public override object DataValue
        {
            get { return (TimeRanges)currentValue; }
        }
    } // class
} // namespace
