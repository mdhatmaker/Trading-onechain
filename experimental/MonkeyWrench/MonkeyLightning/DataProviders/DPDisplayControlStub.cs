using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MonkeyLightning.DataProvider
{
    public class DPDisplayControlStub : UserControl
    {
        public virtual event EventHandler ValueChangedByDisplayUI;

        public virtual object Value { get; set; }
        public virtual string ControlName { get { return "DPDisplayControlStub"; } }
        public KeyValueCollection PropertyValues { get; set; }

        protected object currentValue;

        protected DPDisplayControlStub(KeyValueCollection propertyValues) : base()
        {
            PropertyValues = propertyValues;
        }

        protected DPDisplayControlStub() : base()
        {
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

        // Implicit conversion to DPDisplayControl (when we are using the stub in designer mode).
        public static implicit operator DPDisplayControl(DPDisplayControlStub stub)
        {
            return null;
        }

    } // class
} // namespace
