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
    public partial class DPModifyTimeRange
#if DESIGNER
        : DPModifyControlStub
#else
        : DPModifyControl
#endif
    {
        public override string ControlName { get { return "ModifyDailyTimeRange"; } }
        
        private bool inclusive;
        private bool enabled;
        private CustomRangeSelectorControl.NotifyClient objNotifyClient;

        public DPModifyTimeRange(KeyValueCollection propertyValues) : base(propertyValues)
        {
            InitializeComponent();

            inclusive = true;
            enabled = true;
            UpdateTimeRanges();

            rangeSelectorControl1.RangeUpdated += rangeSelectorControl1_RangeUpdated;
        }

        void rangeSelectorControl1_RangeUpdated(object sender, EventArgs e)
        {
            UpdateTimeRanges();
        }

        private void btnInclusive_Click(object sender, EventArgs e)
        {
            SetInclusive(!inclusive);

            UpdateTimeRanges();
        }

        private void UpdateTimeRanges()
        {
            string range1, range2;
            rangeSelectorControl1.QueryRange(out range1, out range2);

            if (inclusive == true)
            {
                lblTimeRange1.Text = range1 + " - " + range2;
                lblTimeRange2.Text = "";
            }
            else
            {
                lblTimeRange1.Text = "12am - " + range1;
                lblTimeRange2.Text = range2 + " - 12am";
            }
        }

        private void DPModifyDailyTimeRangeControl_Load(object sender, EventArgs e)
        {
            objNotifyClient = new CustomRangeSelectorControl.NotifyClient();            
            rangeSelectorControl1.RegisterForChangeEvent(ref objNotifyClient);
        }

        public override void UpdateUIFromPropertyValues()
        {
            //Day = PropertyValues["WeekDay"];

            /*SetRanges(PropertyValues["range1"], PropertyValues["range2"]);*/

            // TODO: interpret time ranges and other properties and adjust sliders accordingly            
            SetInclusive(inclusive);
        }

        public override void UpdatePropertyValuesFromUI()
        {
            if (lblTimeRange2.Text == "")
                PropertyValues["Ranges"] = lblTimeRange1.Text;
            else
                PropertyValues["Ranges"] = lblTimeRange1.Text + "," + lblTimeRange2.Text;

            PropertyValues["SimpleDisplay"] = PropertyValues["Ranges"];
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public string Range1
        {
            get
            {
                return rangeSelectorControl1.Range1;
            }
            set
            {
                rangeSelectorControl1.Range1 = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public string Range2
        {
            get
            {
                return rangeSelectorControl1.Range2;
            }
            set
            {
                rangeSelectorControl1.Range2 = value;
            }
        }

        void SetInclusive(bool b)
        {
            inclusive = b;

            if (inclusive == false)
            {
                btnInclusive.Text = "Exclusive";
                rangeSelectorControl1.ReverseThumbDirection = true;
                rangeSelectorControl1.InFocusBarColor = Color.Silver;
                rangeSelectorControl1.InFocusRangeLabelColor = Color.Silver;
                rangeSelectorControl1.DisabledBarColor = Color.Lime;
                rangeSelectorControl1.DisabledRangeLabelColor = Color.Green;
            }
            else
            {
                btnInclusive.Text = "Inclusive";
                rangeSelectorControl1.ReverseThumbDirection = false;
                rangeSelectorControl1.InFocusBarColor = Color.Lime;
                rangeSelectorControl1.InFocusRangeLabelColor = Color.Green;
                rangeSelectorControl1.DisabledBarColor = Color.Silver;
                rangeSelectorControl1.DisabledRangeLabelColor = Color.Silver;
            }
        }

        void SetRanges(string r1, string r2)
        {
            lblTimeRange1.Text = r1;
            lblTimeRange2.Text = r2;


        }

    } // class
} // namespace
