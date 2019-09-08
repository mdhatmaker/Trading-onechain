using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EZAPI.Toolbox.Time;

namespace MonkeyLightning.DataProvider.Providers.Basic
{
    public class DataProviderWeeklyTimeRange : DataProviderBase
    {
        public override string DisplayFormat { get { return "{0}"; } }
        public override string Name { get { return "WEEKLY TIME RANGE"; } }
        public override string Description { get { return "Allows you to daily time ranges for an entire week (useful for establishing days/times during which certain trades will be active)."; } }
        public override string EnglishDescription { get { return "TODO: weekly time range description."; } }
        public override string[] PropertyNames { get { return new string[] { "SimpleDisplay", "sun", "mon", "tue", "wed", "thu", "fri", "sat"  }; } }

        DPDisplaySimpleDescription uiControl;
        DPModifyWeeklyTimeRange uiModifyControl;

        public DataProviderWeeklyTimeRange()
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
            {
                uiControl = new DPDisplaySimpleDescription(PropertyValues);
                uiControl.ValueChangedByDisplayUI += uiControl_ValueChanged;
            }

            uiControl.InitializeControl();

            return uiControl;
        }

        public override DPModifyControl GetModifyUserInterface()
        {
            if (uiModifyControl == null)
                uiModifyControl = new DPModifyWeeklyTimeRange(PropertyValues);

            uiModifyControl.InitializeControl();

            return uiModifyControl;
        }

        public override void UpdateProviderFromPropertyValues()
        {
            //prop["SimpleDisplay"] = "(weekly time ranges)";
            
            string sun = prop["Sun"];
            string mon = prop["Mon"];
            string tue = prop["Tue"];
            string wed = prop["Wed"];
            string thu = prop["Thu"];
            string fri = prop["Fri"];
            string sat = prop["Sat"];

            WeeklyTimeRange wtr = new WeeklyTimeRange();

            wtr.AddTimeRange(zDayOfWeek.Sun, TimeRange.FromString(sun));
            wtr.AddTimeRange(zDayOfWeek.Mon, TimeRange.FromString(mon));
            wtr.AddTimeRange(zDayOfWeek.Tue, TimeRange.FromString(tue));
            wtr.AddTimeRange(zDayOfWeek.Wed, TimeRange.FromString(wed));
            wtr.AddTimeRange(zDayOfWeek.Thu, TimeRange.FromString(thu));
            wtr.AddTimeRange(zDayOfWeek.Fri, TimeRange.FromString(fri));
            wtr.AddTimeRange(zDayOfWeek.Sat, TimeRange.FromString(sat));
            

            currentValue = wtr;
            FireValueUpdateEvent();
        }


        public override RuleValueType ValueType
        {
            get { return RuleValueType.TIME_RANGE; }
        }

        public override object DataValue
        {
            get { return (WeeklyTimeRange)currentValue; }
        }


        public override XmlSaveDataProvider SaveData
        {
            get
            {
                XmlSaveDataProvider save = new XmlSaveDataProvider();
                save.Name = Name;
                save.CurrentValue = SerializeCurrentValue.ToString();
                save.KeyValuePairs = prop.GetSerializableDictionary();
                return save;
            }
            set
            {
                prop.SetSerializableDictionary(value.KeyValuePairs);
                SerializeCurrentValue = value.CurrentValue;
                UpdateProviderFromPropertyValues();
            }
        }


    } // class
} // namespace
