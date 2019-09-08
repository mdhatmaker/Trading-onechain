using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MonkeyLightning.DataProvider.Providers.Basic
{
    public class DataProviderUserDecimal : DataProviderBase
    {
        public override string DisplayFormat { get { return "{0}"; } }
        public override string Name { get { return "USER DECIMAL"; } }
        public override string EnglishDescription { get { return currentValue.ToString(); } }
        public override string Description { get { return "Allows you to enter a decimal number (i.e 3.14, 18.5, 1035.0)."; } }

        //public override event ValueUpdateEventHandler DataUpdate;

        DPDisplayUserDecimal uiControl;

        public DataProviderUserDecimal() : base()
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
                uiControl = new DPDisplayUserDecimal(PropertyValues);
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
            get { return Convert.ToDouble(currentValue); }
        }
    } // class
}
