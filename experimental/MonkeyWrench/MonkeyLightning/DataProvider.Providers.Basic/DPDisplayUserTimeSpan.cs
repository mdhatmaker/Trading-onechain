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
    public partial class DPDisplayUserTimeSpan
#if DESIGNER
        : DPDisplayControlStub
#else
        : DPDisplayControl
#endif
    {
        public override event EventHandler ValueChangedByDisplayUI;

        public override string ControlName { get { return "UserTimeSpanDisplay"; } }

        public DPDisplayUserTimeSpan(KeyValueCollection propertyValues) : base(propertyValues)
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (ValueChangedByDisplayUI != null) ValueChangedByDisplayUI(this, e);

            lblHours.Text = string.Format("{0} hrs", dateTimePicker1.Value.Hour);
            lblMinutes.Text = string.Format("{0} mins", dateTimePicker1.Value.Minute);
            lblSeconds.Text = string.Format("{0} secs", dateTimePicker1.Value.Second);
        }

        /*public override void UpdateValueHandler(object sender, ValueUpdateEventArgs e)
        {
            Value = e.UpdatedValue;
        }*/

        public override object Value
        {
            get
            {
                DateTime dt = dateTimePicker1.Value;
                TimeSpan ts = new TimeSpan(dt.Hour, dt.Minute, dt.Second);
                return ts;
            }
            set { dateTimePicker1.Value = Convert.ToDateTime(value); }
        }



    } // class
} // namespace
