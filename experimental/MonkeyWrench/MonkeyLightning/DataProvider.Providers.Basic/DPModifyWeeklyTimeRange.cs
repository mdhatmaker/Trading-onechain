//#define DESIGNER
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MonkeyLightning.DataProvider;

namespace MonkeyLightning.DataProvider.Providers.Basic
{
    public partial class DPModifyWeeklyTimeRange
#if DESIGNER
        : DPModifyControlStub
#else
        : DPModifyControl
#endif
    {
        public override string ControlName { get { return "ModifyWeeklyTimeRange"; } }

        public DPModifyWeeklyTimeRange(KeyValueCollection propertyValues) : base(propertyValues)
        {
            InitializeComponent();
        }

        public override void UpdateUIFromPropertyValues()
        {
            //numericMinValue.Value = Convert.ToDecimal(keyValuePairs["Min"]);
            //numericMaxValue.Value = Convert.ToDecimal(keyValuePairs["Max"]);
        }

        public override void UpdatePropertyValuesFromUI()
        {
            PropertyValues["Sun"] = dpTimeRangeSun.Ranges;
            PropertyValues["Mon"] = dpTimeRangeMon.Ranges;
            PropertyValues["Tue"] = dpTimeRangeTue.Ranges;
            PropertyValues["Wed"] = dpTimeRangeWed.Ranges;
            PropertyValues["Thu"] = dpTimeRangeThu.Ranges;
            PropertyValues["Fri"] = dpTimeRangeFri.Ranges;
            PropertyValues["Sat"] = dpTimeRangeSat.Ranges;

            PropertyValues["SimpleDisplay"] = "(weekly time range)";
        }

    } // class
} // namespace
