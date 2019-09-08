using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MonkeyLightning.DataProvider.Providers.Basic
{
    public class DataProviderUserTime : DataProviderBase
    {
        public override string DisplayFormat { get { return "{0:h:mm:ss tt}"; } }
        public override string Name { get { return "USER TIME"; } }
        public override string Description { get { return "Allows you to enter a time in hh:mm:ss format."; } }
        public override string EnglishDescription { get { return currentValue.ToString(); } }

        //public override event ValueUpdateEventHandler DataUpdate;

        DPDisplayUserTime uiControl;

        public DataProviderUserTime() : base()
        {
            uiControl = null;
        }

        void uiControl_ValueChanged(object sender, EventArgs e)
        {
            currentValue = uiControl.Value;
            FireValueUpdateEvent();
        }

        public override DPDisplayControl GetDisplayUserInterface()
        {
            if (uiControl == null)
            {
                uiControl = new DPDisplayUserTime(PropertyValues);
                uiControl.ValueChangedByDisplayUI += uiControl_ValueChanged;
            }

            uiControl.InitializeControl();

            return uiControl;
        }

        public override RuleValueType ValueType
        {
            get { return RuleValueType.TIME; }
        }

        public override object DataValue
        {
            get { return Convert.ToDateTime(currentValue); }
        }
    } // class
} // namespace
