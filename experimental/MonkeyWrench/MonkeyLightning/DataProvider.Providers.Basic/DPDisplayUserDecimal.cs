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
    public partial class DPDisplayUserDecimal
#if DESIGNER
        : DPDisplayControlStub
#else
        : DPDisplayControl
#endif
    {
        public override event EventHandler ValueChangedByDisplayUI;

        public override string ControlName { get { return "UserDecimalDisplay"; } }

        public DPDisplayUserDecimal(KeyValueCollection propertyValues) : base(propertyValues)
        {
            InitializeComponent();
        }

        private void textNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.'
                && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;

            // only allow one minus sign
            if (e.KeyChar == '-' && (sender as TextBox).Text.IndexOf('-') > -1)
                e.Handled = true;
        }

        private void textNumber_TextChanged(object sender, EventArgs e)
        {
            if (ValueChangedByDisplayUI != null) ValueChangedByDisplayUI(this, e);
        }

        /*public override void UpdateValueHandler(object sender, ValueUpdateEventArgs e)
        {
            Value = e.UpdatedValue;
        }*/

        public override object Value
        {
            get { return textNumber.Text; }
            set { textNumber.Text = value.ToString(); }
        }
    } // class
} // namespace
