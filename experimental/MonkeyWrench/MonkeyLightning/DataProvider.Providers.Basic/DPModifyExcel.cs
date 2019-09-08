#define DESIGNER
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MonkeyLightning.DataProvider;

namespace MonkeyLightning.DataProvider.Providers.Basic
{
    public partial class DPModifyExcel
#if DESIGNER
        : DPModifyControlStub
#else
        : DPModifyControl
#endif
    {
        public event EventHandler SelectedValueChanged;

        public override string ControlName { get { return "ModifyExcel"; } }

        private string selectedValue;

        public DPModifyExcel(KeyValueCollection propertyValues) : base(propertyValues)
        {
            InitializeComponent();
        }

        private void lblChoice_Click(object sender, EventArgs e)
        {
            PropertyValues["TradeDetailTag"] = selectedValue;
            if (SelectedValueChanged != null) SelectedValueChanged(this, EventArgs.Empty);
        }


        public override void UpdateUIFromPropertyValues()
        {
            //string SelectedValue { get { return selectedValue; } }
            //EZInstrument SelectedInstrument { get { return currentInstrument; } }
        }

        public override void UpdatePropertyValuesFromUI()
        {
            PropertyValues["TradeDetailTag"] = selectedValue;

        }

        private void btnOpenExcelDocument_Click(object sender, EventArgs e)
        {
            OpenExcelDocument();
        }

        private void OpenExcelDocument()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            DialogResult dialogResult = openFile.ShowDialog(this);

            if (dialogResult == DialogResult.OK)
            {
                string excelFile = openFile.FileName;

                var app = new Microsoft.Office.Interop.Excel.Application();
                var workbook = app.Workbooks.Open(excelFile);

            }
        }

    } // class
} // namespace
