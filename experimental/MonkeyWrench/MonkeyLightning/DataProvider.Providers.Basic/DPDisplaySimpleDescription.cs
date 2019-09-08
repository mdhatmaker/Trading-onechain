//#define DESIGNER
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MonkeyLightning.DataProvider;

namespace MonkeyLightning.DataProvider.Providers.Basic
{
    public partial class DPDisplaySimpleDescription
#if DESIGNER
        : DPDisplayControlStub
#else
        : DPDisplayControl
#endif
    {
        public override event EventHandler ValueChangedByDisplayUI;

        public override string ControlName { get { return "SimpleDescriptionDisplay"; } }

        public DPDisplaySimpleDescription(KeyValueCollection propertyValues) : base(propertyValues)
        {
            InitializeComponent();
        }

        public override void InitializeControl()
        {
            lblDescription.Text = PropertyValues["SimpleDisplay"]; 
        }

        public override object Value
        {
            get { return currentValue; }
            set 
            {                
                try
                {
                    currentValue = value;
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(string.Format("EXCEPTION (DPSimpleDescriptionDisplay): {0}", ex.Message));                    
                }
            }
        }

        public override void ParentFormClosing()
        {
            this.Dispose();
        }

        private void lblDescription_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        
        private void DPDisplaySimpleDescription_Load(object sender, EventArgs e)
        {
            lblDescription.Text = PropertyValues["SimpleDisplay"];
            //if (currentValue != null) lblDescription.Text = currentValue.ToString();
        }
    } // class
} // namespace
