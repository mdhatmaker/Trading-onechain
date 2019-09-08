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
    public partial class DPDisplaySimpleValue
#if DESIGNER
        : DPDisplayControlStub
#else
        : DPDisplayControl
#endif
    {
        public override event EventHandler ValueChangedByDisplayUI;

        public override string ControlName { get { return "SimpleValueDisplay"; } }

        private string displayFormat = null;
        private string descriptionText = null;

        public DPDisplaySimpleValue(KeyValueCollection propertyValues) : base(propertyValues)
        {
            InitializeComponent();

            displayFormat = propertyValues["SimpleDisplayFormat"];
            descriptionText = propertyValues["SimpleDisplayDescription"];
        }

        public override object Value
        {
            get { return lblNumber.Text.Trim(); }
            set 
            {
                try
                {
                    if (this.IsHandleCreated)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            descriptionText = PropertyValues["SimpleDisplayDescription"] ?? "";
                            if (displayFormat == null)
                                lblNumber.Text = descriptionText + value.ToString();
                            else
                                lblNumber.Text = descriptionText + String.Format(displayFormat, value);
                        });
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(string.Format("EXCEPTION (DPSimpleDisplayControl): {0}", ex.Message));                    
                }
            }
        }

        public override void ParentFormClosing()
        {
            Console.WriteLine("Parent Form Closing...");
            this.Dispose();
        }

        private void lblNumber_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        
    } // class
} // namespace
