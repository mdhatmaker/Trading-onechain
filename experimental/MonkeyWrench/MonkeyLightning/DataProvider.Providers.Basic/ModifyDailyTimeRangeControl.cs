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
    public partial class ModifyDailyTimeRangeControl : UserControl
    {
        public string Day
        {
            get { return lblDay.Text; }
            set { lblDay.Text = value; }
        }

        public string Ranges
        {
            get
            {
                if (enabled == false)
                    return "";
                else if (lblTimeRange2.Text.Trim() == "")
                    return lblTimeRange1.Text.Trim();
                else
                    return lblTimeRange1.Text.Trim() + "," + lblTimeRange2.Text.Trim();
            }
        }

        private bool inclusive;
        private bool enabled;
        private CustomRangeSelectorControl.NotifyClient objNotifyClient;

        public ModifyDailyTimeRangeControl()
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

        /*public void UpdateUIFromControlValues()
        {
            Day = this["day"];
            inclusive = this["inclusive"];
            enabled = this["enabled"];
            SetRanges(this["range1"], this["range2"]);

            // TODO: interpret time ranges and other properties and adjust sliders accordingly            
            SetInclusive(inclusive);
            Checked = enabled;
        }*/

        /*public void UpdateControlValuesFromUI()
        {
            this["day"] = Day;
            this["inclusive"] = inclusive.ToString();
            this["enabled"] = enabled.ToString();
            this["range1"] = lblTimeRange1.Text;
            this["range2"] = lblTimeRange2.Text;
        }*/

        private void chkEnable_CheckedChanged(object sender, EventArgs e)
        {
            enabled = chkEnable.Checked;
            foreach (Control control in this.Controls)
                if (control.Name != "chkEnable") control.Enabled = enabled;
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text
        {
            get
            {
                return lblDay.Text;
            }
            set
            {
                lblDay.Text = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public bool Checked
        {
            get
            {
                return chkEnable.Checked;
            }
            set
            {
                chkEnable.Checked = value;
            }
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
