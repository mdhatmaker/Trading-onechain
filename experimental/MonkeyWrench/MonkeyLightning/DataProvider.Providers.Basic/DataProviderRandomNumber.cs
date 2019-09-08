using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MonkeyLightning.DataProvider.Providers.Basic
{
    [Serializable]
    public class DataProviderRandomNumber : DataProviderBase
    {
        public override string Name { get { return "RANDOM"; } }
        public override string DisplayFormat { get { return "{0}"; } }
        public override string Description { get { return "Provides constantly updating random number."; } }
        public override string EnglishDescription { get { return string.Format("a random number between {0} and {1}", Min, Max); } }
        public override string[] PropertyNames { get { return new string[] { "min", "max" }; } }

        //public override event ValueUpdateEventHandler DataUpdate;

        public int Min {
            get { return prop["min"]; }
            set { prop["min"] = value; }
        }
        public int Max {
            get { return prop["max"]; }
            set { prop["max"] = value; }
        }

        public int DelayMilliseconds { get { return delayMilliseconds; } }

        int delayMilliseconds;

        Timer timer;

        DPDisplaySimpleValue uiControl;
        DPModifyRandomNumber uiModifyControl;
        
        Random random;

        public DataProviderRandomNumber() : base()
        {
            uiControl = null;
            uiModifyControl = null;
            DataUpdate += DataProviderRandom_DataUpdate;

            random = new Random(new System.DateTime().Millisecond);

            this.Min = 1;
            this.Max = 10;
            this.delayMilliseconds = 2000;

            StartTimer();
        }

        void DataProviderRandom_DataUpdate(object sender, ValueUpdateEventArgs e)
        {
            if (uiControl != null) uiControl.Value = e.UpdatedValue;
        }

        private void DisconnectDisplayUIEvents()
        {
            DataUpdate -= DataProviderRandom_DataUpdate;
        }

        public override void ClosingDisplayUI()
        {
            DisconnectDisplayUIEvents();
        }

        void StartTimer()
        {
            timer = new Timer();
            timer.Tick += timer_Tick;
            timer.Interval = delayMilliseconds;
            timer.Enabled = true;

            timer_Tick(this, EventArgs.Empty);
            
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            currentValue = random.Next(Min, Max);
            FireValueUpdateEvent();
            //uiControl.Value = currentValue;
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
            //{
                uiModifyControl = new DPModifyRandomNumber(PropertyValues);
                //uiModifyControl.PropertyValues["Min"] = Min;
                //uiModifyControl.PropertyValues["Max"] = Max;
            //}

            uiModifyControl.InitializeControl();

            return uiModifyControl;
        }

        public override void UpdateProviderFromPropertyValues()
        {
            Min = prop["Min"];
            Max = prop["Max"];
        }

        public override RuleValueType ValueType
        {
            get { return RuleValueType.INT; }
        }

        public override object DataValue
        {
            get { return currentValue; }
        }
    } // class
} // namespace
