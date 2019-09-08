using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EZAPI;
using EZAPI.Framework;
using EZAPI.Framework.Base;
using EZAPI.Framework.Spread;
using EZAPI.Toolbox.DataStructures;
using EZAPI.Toolbox.Finance;
using XL = Microsoft.Office.Interop.Excel;

namespace MonkeyLightning.UI.Forms
{
    public partial class TestSpreadsForm : Form
    {
        private EZInstrument instrument1;
        private EZInstrument instrument2;
        private EZSpreadInstrument spread;

        private APIMain api;

        public TestSpreadsForm()
        {
            InitializeComponent();

            api = APIMain.Instance;

            api.OnInsideMarketUpdate += api_OnInsideMarketUpdate;        
        }

        private void btnMarket1_Click(object sender, EventArgs e)
        {
            EZInstrument instrument = api.ShowInstrumentDialog();
            if (instrument != null)
            {
                lblMarket1.Text = instrument.Description;
                instrument1 = instrument;
            }
        }

        private void btnMarket2_Click(object sender, EventArgs e)
        {
            EZInstrument instrument = api.ShowInstrumentDialog();
            if (instrument != null)
            {
                lblMarket2.Text = instrument.Description;
                instrument2 = instrument;
            }
        }

        private void btnBuildSpread_Click(object sender, EventArgs e)
        {
            ezSpreadDefinition def = new ezSpreadDefinition();
            ezSpreadLeg leg1 = new ezSpreadLeg(instrument1, zBuySell.Buy, Convert.ToInt32(txtExecQty1.Text), Convert.ToDouble(txtPriceMult1.Text));
            ezSpreadLeg leg2 = new ezSpreadLeg(instrument2, zBuySell.Sell, Convert.ToInt32(txtExecQty2.Text), Convert.ToDouble(txtPriceMult2.Text));
            def.AddSpreadLeg(leg1);
            def.AddSpreadLeg(leg2);
            spread = new EZSpreadInstrument(def);
            api.SubscribeToSpread(spread);
            status.Text = "Spread created.";
        }

        void api_OnInsideMarketUpdate(EZInstrument ezi)
        {
            if (ezi == spread)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    lstDisplay.Items.Insert(0, string.Format("bid: {0:0}   ask: {1:0}   last: {2:0}   mid: {3:0}", spread.Bid.ToDouble(), spread.Ask.ToDouble(), spread.Last.ToDouble(), spread.MidPrice.ToDouble()));
                });
                Console.WriteLine("bid: {0:0}    ask: {1:0}    last: {2:0}    mid: {3:0}", spread.Bid, spread.Ask, spread.Last, spread.MidPrice);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<EconomicNumber> econNumbers = EconomicNumbers.DownloadDailyFXCalendar(null);
        }

        private void OpenExcelDocument()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            DialogResult dialogResult = openFile.ShowDialog(this);

            if (dialogResult == DialogResult.OK)
            {
                string excelFile = openFile.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenExcelDocument();
        }


/*
        //private DSOFramer.FramerControl axFramer;
        private AxDSOFramer.AxFramerControl axFramer;


        private void OpenExcelDocument()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            DialogResult dialogResult = openFile.ShowDialog(this);

            if (dialogResult == DialogResult.OK)
            {
                string excelFile = openFile.FileName;

                KillExcelProcesses();

                try
                {
                    CreateAndAddFramer();

                    string file = excelFile;   // @"c:\temp\date_6490.xls";

                    // Create a new Excel Application. For the purpose of the test lets ensure it's visible too.
                    XL.Application excelApp = new XL.Application() { Visible = true, DisplayAlerts = false };

                    // Set the Excel security to Forcefully disable all macros.
                    //excelApp.AutomationSecurity = Microsoft.Office.Core.MsoAutomationSecurity.msoAutomationSecurityForceDisable;

                    // Open the file. This is because it needs to be open before trying with the DsoFramer
                    // control. It also demonstrates that excel opens fine without prompting for any 
                    // macro support.
                    XL.Workbook selectedBook = excelApp.Workbooks.Open(file);

                    // Open the file in the framer. This will now prompt to enable macros.
                    axFramer.Open(file, true, null, null, null);
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.ToString());
                    KillExcelProcesses();
                }

                //browser.Navigate(excelFile, false);

                //var app = new Microsoft.Office.Interop.Excel.Application();
                //var workbook = app.Workbooks.Open(excelFile);


            }
        }


        /// <summary>
        /// Creates a DsoFramer and adds to the form.
        /// </summary>
        private void CreateAndAddFramer()
        {
            // Create an axFramer control
            this.axFramer = new AxDSOFramer.AxFramerControl();

            // Initialize the axFamer
            ((System.ComponentModel.ISupportInitialize)(this.axFramer)).BeginInit();

            // Update the name of the framer (bug in framer)
            this.axFramer.Name = "framer_" + Guid.NewGuid().ToString();
            this.axFramer.ResetText();
            
            // Dock the framer and add to the form
            this.axFramer.Dock = System.Windows.Forms.DockStyle.Fill;            
            this.Controls.Add(axFramer);
            this.panelExcel.Controls.Add(axFramer);
            ((System.ComponentModel.ISupportInitialize)(this.axFramer)).EndInit();
        }

        /// <summary>
        /// Kills all excel processes.
        /// </summary>
        private static void KillExcelProcesses()
        {
            // Kill all instances of Excel
            foreach (Process p in Process.GetProcessesByName("excel"))
            {
                p.Kill();
            }
        }
*/

    } // class
} // namespace
