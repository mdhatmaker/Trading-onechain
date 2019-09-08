using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MonkeyLightning.DataProvider.Providers.Basic
{
    public class DataProviderUserInteger : DataProviderBase
    {
        public override string DisplayFormat { get { return "{0}"; } }
        public override string Name { get { return "USER INTEGER"; } }
        public override string EnglishDescription { get { return currentValue.ToString(); } }
        public override string Description { get { return "Allows you to enter an integer number (1, 25, 5429, 8, 237)."; } }

        //public override event ValueUpdateEventHandler DataUpdate;

        DPDisplayUserInteger uiControl;

        public DataProviderUserInteger() : base()
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
                uiControl = new DPDisplayUserInteger(PropertyValues);
                uiControl.ValueChangedByDisplayUI += uiControl_ValueChanged;
            }

            uiControl.InitializeControl();

            return uiControl;
        }

        public override RuleValueType ValueType
        {
            get { return RuleValueType.INT; }
        }

        public override object DataValue
        {
            get { return Convert.ToInt32(currentValue); }
        }
    } // class
} // namespace
