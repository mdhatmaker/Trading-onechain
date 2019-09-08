using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MonkeyLightning.DataProvider.Providers.Basic
{
    public class DataProviderUserTimeSpan : DataProviderBase
    {
        public override string DisplayFormat { get { return "{0}"; } }
        public override string Name { get { return "USER TIME SPAN"; } }
        public override string Description { get { return "Allows you to enter a DURATION of time in hh:mm:ss format."; } }
        public override string EnglishDescription { get { return currentValue.ToString(); } }

        //public override event ValueUpdateEventHandler DataUpdate;

        DPDisplayUserTimeSpan uiControl;

        public DataProviderUserTimeSpan() : base()
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
                uiControl = new DPDisplayUserTimeSpan(PropertyValues);
                uiControl.ValueChangedByDisplayUI += uiControl_ValueChanged;
            }

            uiControl.InitializeControl();

            return uiControl;
        }

        public override RuleValueType ValueType
        {
            get { return RuleValueType.TIME_SPAN; }
        }

        public override object DataValue
        {
            get { return (TimeSpan)currentValue; }
        }

        /// <summary>
        /// Since TimeSpan does not serialize with the XmlSerializer, we do a little
        /// conversion where we will save the Ticks of the TimeSpan (a long value) and
        /// restore by creating a TimeSpan FROM the ticks that we saved.
        /// </summary>
        public override object SerializeCurrentValue
        {
            get
            {
                if (currentValue is TimeSpan)
                {
                    TimeSpan span = (TimeSpan)currentValue;
                    return span.Ticks;
                }
                else
                {
                    return currentValue;
                }
            }
            set
            {
                if (value is long)
                {
                    long ticks = (long)value;
                    currentValue = new TimeSpan(ticks);
                }
                else
                {
                    currentValue = value;
                }
            }
        }


    } // class
} // namespace
