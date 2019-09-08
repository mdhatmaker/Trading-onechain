using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MonkeyLightning.DataProvider;

namespace MonkeyLightning.DataProvider.Providers.Basic
{
    public class DataProviderCurrentTime : DataProviderBase
    {
        public override string Name { get { return "CURRENT TIME"; } }
        public override string DisplayFormat { get { return "{0:h:mm:ss tt}"; } }
        public override string Description { get { return "Provides the current time."; } }
        public override string EnglishDescription { get { return "the current time"; } }
        public override string[] PropertyNames { get { return new string[] { "SimpleDisplayFormat" }; } }

        //public override event ValueUpdateEventHandler DataUpdate;

        DPDisplaySimpleValue uiControl;

        Timer timer;

        const int TimerInterval = 500;

        public DataProviderCurrentTime() : base()
        {
            uiControl = null;
            DataUpdate += DataProviderCurrentTime_DataUpdate;

            PropertyValues["SimpleDisplayFormat"] = DisplayFormat;

            StartTimer();
        }

        void DataProviderCurrentTime_DataUpdate(object sender, ValueUpdateEventArgs e)
        {
            if (uiControl != null) uiControl.Value = e.UpdatedValue;
        }

        void StartTimer()
        {
            timer = new Timer();
            timer.Tick += timer_Tick;
            timer.Interval = TimerInterval;
            timer.Enabled = true;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            currentValue = DateTime.Now;
            FireValueUpdateEvent();
            //if (DataUpdate != null) DataUpdate(this, new ValueUpdateEventArgs(currentValue));
            //uiControl.Value = String.Format("{0:hh:mm:ss}", currentValue);
        }

        public override DPDisplayControl GetDisplayUserInterface()
        {
            if (uiControl == null)
                uiControl = new DPDisplaySimpleValue(PropertyValues);

            uiControl.InitializeControl();

            return uiControl;
        }

        public override RuleValueType ValueType
        {
            get { return RuleValueType.TIME; }
        }

        public override object DataValue
        {
            get { return currentValue; }
        }
    } // class
} // namespace
