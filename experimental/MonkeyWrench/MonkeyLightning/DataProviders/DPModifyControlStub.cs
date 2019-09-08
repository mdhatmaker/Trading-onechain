using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MonkeyLightning.DataProvider
{
    public class DPModifyControlStub : UserControl 
    {
        public virtual string ControlName
        {
            get { return null; }
        }

        public virtual void UpdatePropertyValuesFromUI()
        {
            
        }

        public virtual void UpdateUIFromPropertyValues()
        {
            
        }

        public virtual string[] PropertyNames
        {
            get { return null; }
        }

        protected KeyValueCollection controlValues;

        public KeyValueCollection PropertyValues { get; set; }

        protected DPModifyControlStub() : base()
        {
        }

        protected DPModifyControlStub(KeyValueCollection propertyValues) : base()
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
            UpdatePropertyValuesFromUI();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateUIFromPropertyValues();
        }


        public static implicit operator DPModifyControl(DPModifyControlStub stub)
        {
            return null;
        }

    } // class
} // namespace
