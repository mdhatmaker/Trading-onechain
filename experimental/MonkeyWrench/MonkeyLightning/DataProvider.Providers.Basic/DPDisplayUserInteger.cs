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
    public partial class DPDisplayUserInteger
#if DESIGNER
        : DPDisplayControlStub
#else
        : DPDisplayControl
#endif
    {
        public override event EventHandler ValueChangedByDisplayUI;

        public override string ControlName { get { return "UserIntegerDisplay"; } }

        public DPDisplayUserInteger(KeyValueCollection propertyValues) : base(propertyValues)
        {
            InitializeComponent();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (ValueChangedByDisplayUI != null) ValueChangedByDisplayUI(this, e);
        }

        /*public override void UpdateValueHandler(object sender, ValueUpdateEventArgs e)
        {
            Value = e.UpdatedValue;
        }*/

        public override object Value
        {
            get { return numericUpDown1.Value; }
            set { numericUpDown1.Value = Convert.ToDecimal(value); }
        }
    } // class
} // namespace
