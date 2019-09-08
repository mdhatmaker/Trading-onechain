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
    public partial class DPDisplayUserTime
#if DESIGNER
        : DPDisplayControlStub
#else
        : DPDisplayControl
#endif
    {
        public override event EventHandler ValueChangedByDisplayUI;

        public override string ControlName { get { return "UserTimeDisplay"; } }

        public DPDisplayUserTime(KeyValueCollection propertyValues) : base(propertyValues)
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (ValueChangedByDisplayUI != null) ValueChangedByDisplayUI(this, e);
        }

        /*public override void UpdateValueHandler(object sender, ValueUpdateEventArgs e)
        {
            Value = e.UpdatedValue;
        }*/

        public override object Value
        {
            get { return dateTimePicker1.Value; }
            set { dateTimePicker1.Value = Convert.ToDateTime(value); }
        }

    } // class
} // namespace
