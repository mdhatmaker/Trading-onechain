using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MonkeyLightning.DataProvider
{
    public abstract class DPDisplayControl : UserControl
    {
        public abstract event EventHandler ValueChangedByDisplayUI;

        public abstract object Value { get; set; }
        public abstract string ControlName { get; }
        public KeyValueCollection PropertyValues { get; set; }

        protected object currentValue;

        protected DPDisplayControl(KeyValueCollection propertyValues) : base()
        {
            PropertyValues = propertyValues;
        }

        public virtual void InitializeControl()
        {
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (this.ParentForm != null)
                this.ParentForm.FormClosing += ParentForm_FormClosing;
        }

        void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ParentFormClosing();
        }

        public virtual void ParentFormClosing()
        {

        }

    } // abstract base class
} // namespace
