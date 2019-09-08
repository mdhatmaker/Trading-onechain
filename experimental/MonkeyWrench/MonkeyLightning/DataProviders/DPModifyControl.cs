using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MonkeyLightning.DataProvider
{
    public abstract class DPModifyControl : UserControl
    {
        public abstract string ControlName { get; }
        public abstract void UpdatePropertyValuesFromUI();
        public abstract void UpdateUIFromPropertyValues();

        public KeyValueCollection PropertyValues { get; set; }

        protected DPModifyControl(KeyValueCollection propertyValues) : base()
        {
            PropertyValues = propertyValues;
            //UpdateUIFromPropertyValues();
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
            UpdatePropertyValuesFromUI();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateUIFromPropertyValues();
        }

        
    } // abstract base class

} // namespace
