using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MonkeyLightning.DataProvider.Providers.Basic
{
    public partial class TestTimeRangeForm : Form
    {
        public TestTimeRangeForm()
        {
            InitializeComponent();

            dpTimeRange1.Day = "MON";
            
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

        private void btnOpenExcel_Click(object sender, EventArgs e)
        {
            OpenExcelDocument();
        }

    } // class
} // namespace
