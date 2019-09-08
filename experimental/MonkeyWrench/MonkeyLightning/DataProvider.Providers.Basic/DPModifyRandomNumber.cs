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
    public partial class DPModifyRandomNumber
#if DESIGNER
        : DPModifyControlStub
#else
        : DPModifyControl
#endif
    {
        public override string ControlName { get { return "ModifyRandomNumber"; } }

        public DPModifyRandomNumber(KeyValueCollection propertyValues) : base(propertyValues)
        {
            InitializeComponent();
        }

        public override void UpdateUIFromPropertyValues()
        {
            numericMinValue.Value = PropertyValues["Min"];
            numericMaxValue.Value = PropertyValues["Max"];
        }

        public override void UpdatePropertyValuesFromUI()
        {
            PropertyValues["Min"] = numericMinValue.Value;
            PropertyValues["Max"] = numericMaxValue.Value;
        }


    } // class
} // namespace
