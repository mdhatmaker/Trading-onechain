using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using EZAPI;
using EZAPI.Framework;
using EZAPI.Framework.Base;
using EZAPI.Toolbox;
using EZAPI.Toolbox.Serialization;
using MonkeyLightning.DataProvider;

namespace MonkeyLightning.UI.Forms
{
    public partial class LightningLoaderForm : Form
    {
        private APIMain api;

        private Dictionary<ezInstrumentKey, EZInstrument> ttInstruments = new Dictionary<ezInstrumentKey, EZInstrument>();
        private DashboardForm dashboardForm;
        private SplashForm splashForm;

        public LightningLoaderForm()
        {
            InitializeComponent();

            /*IDataProvider dp = new DataProviderTest();

            XmlSaveDataProvider save = dp.SaveData;

            string filename = @"G:\DATA\PROJECTS\MonkeyLightning\EZTrader\bin\Debug\test";

            bool success;
            success = Xml.Serialize(save, typeof(XmlSaveDataProvider), filename + ".xml");
            success = Binary.Serialize(save, filename + ".bin");
            success = DataContract.Serialize(save, typeof(XmlSaveDataProvider), filename + ".dat");

            XmlSaveDataProvider restore;
            restore = (XmlSaveDataProvider)Xml.Deserialize(typeof(XmlSaveDataProvider), filename + ".xml");
            restore = (XmlSaveDataProvider)Binary.Deserialize(filename + ".bin");
            restore = (XmlSaveDataProvider)DataContract.Deserialize(typeof(XmlSaveDataProvider), filename + ".dat");

            return;*/

            
            dashboardForm = new DashboardForm();
            dashboardForm.DashboardClosing += dashboardForm_DashboardClosing;
            dashboardForm.Show();

            WinForms.SetFormRelativePosition(this, dashboardForm);

            splashForm = new SplashForm();
            splashForm.Show();

            WinForms.SetFormRelativePosition(splashForm, dashboardForm);

            dashboardForm.UseWaitCursor = true;
            splashForm.UseWaitCursor = true;

            Application.DoEvents();

            // Finally register Form events.
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Closed += new System.EventHandler(this.frmMain_Closed);
        }

        void dashboardForm_DashboardClosing(object sender, EventArgs e)
        {
            CloseApplication();
        }

        #region Startup and shutdown code

        // Initialise the api when the application starts.
        private void frmMain_Load(object sender, System.EventArgs e)
        {
            string loginUsername = txtUsername.Text;
            string loginPassword = txtPassword.Text;

            api = APIMain.Instance;
            /*api.OnInitialize += new InitializeHandler(api_OnInitialize);
            api.OnInstrumentFound += new InstrumentFoundHandler(api_OnInstrumentFound);
            api.OnInsideMarketUpdate += new InsideMarketHandler(api_OnInsideMarketUpdate);
            api.OnFill += api_OnFill;
            api.OnOrder += api_OnOrder;*/

            btnConnect.Enabled = false;
            splashForm.Status = "attempting to login...";

            api.LoginFailure += api_LoginFailure;
            api.LoginSuccess += api_LoginSuccess;
            //api.StartUserInteraction();
            api.StartAPIUnattended(txtUsername.Text.Trim(), txtPassword.Text.Trim(), "CTS");
        }

        // Shutdown the api when the application exits.
        private void frmMain_Closed(object sender, System.EventArgs e)
        {

        }
        #endregion

        void api_LoginSuccess(EventArgsEx ex)
        {
            Trace.WriteLine("login success");
            this.Invoke((MethodInvoker)delegate
            {
                splashForm.Status = "Login: SUCCESS!";
                Thread.Sleep(1000);                
                this.Visible = false;
                dashboardForm.ShowLogo(true);
                dashboardForm.UseWaitCursor = false;
                splashForm.UseWaitCursor = false;
                splashForm.Close();
                //btnSubscribe.Enabled = true; // runs on UI thread
            });

        }

        void api_LoginFailure(EventArgsEx ex)
        {
            Trace.WriteLine("login failure");
            this.Invoke((MethodInvoker)delegate
            {
                //btnConnect.Enabled = true;
                splashForm.Status = "Login: failed.";
                MessageBox.Show(ex.Value.ToString(), "Login Failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                splashForm.Status = "Exiting Monkey Lightning...";
            });
            
            CloseApplication();
        }

        void CloseApplication()
        {
            this.Invoke((MethodInvoker)delegate
            {
                api.StopAPI();
                if (dashboardForm != null && dashboardForm.IsClosing == false)
                {
                    dashboardForm.IsClosing = true;
                    dashboardForm.Close();
                }
                this.Close();
            });
            Application.Exit();
        }

        void api_OnFill(FillOriginator originator, FillAction action, EZInstrument ttInstrument, EZFill fill, EZFill newFill)
        {
            //cross thread - so you don't get the cross theading exception
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    api_OnFill(originator, action, ttInstrument, fill, newFill);
                });
                return;
            }
        }

        void api_OnOrder(EZOrderStatus status, EZInstrument ttInstrument, EZOrder order, EZOrder newOrder)
        {
            //cross thread - so you don't get the cross theading exception
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    api_OnOrder(status, ttInstrument, order, newOrder);
                });
                return;
            }
        }

        void api_OnInstrumentFound(EZInstrument ttInstrument, bool success)
        {
            //cross thread - so you don't get the cross theading exception
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    api_OnInstrumentFound(ttInstrument, success);
                });
                return;
            }

            if (success)
            {
                //TTInstrument ttInstrument = new TTInstrument(instrument.Key);
                if (!ttInstruments.ContainsKey(ttInstrument.Key))
                    ttInstruments[ttInstrument.Key] = ttInstrument;

                Message("Subscribed to " + ttInstrument.Name);
                //btnOrder.Enabled = true;
            }
            else
            {
                string errorMsg = "";

                /*if (e.Error != null)
                    errorMsg = e.Error.Message;*/

                // Instrument not found and TTAPI has given up looking for it
                MessageBox.Show(this, "Could not find instrument:\n\n" + ttInstrument.Name + "\n\n" + errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        void api_OnInitialize(bool success, string message)
        {
            if (success == true)
            {
                Console.WriteLine("TTAPI initialized successfully.");
            }
            else
            {
                MessageBox.Show(message, "TTAPI Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        void api_OnInsideMarketUpdate(EZInstrument instrument)
        {
            //cross thread - so you don't get the cross threading exception
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    api_OnInsideMarketUpdate(instrument);
                });
                return;
            }
            Message(string.Format("{0:h:mm:ss}: bid {1} @ {2}  ask {3} @ {4}  last {5} @ {6} [{7}]", DateTime.Now, instrument.BidQty, instrument.Bid, instrument.AskQty, instrument.Ask, instrument.LastQty, instrument.Last, instrument.Volume));
        }

        private void Message(string msg)
        {
            Console.WriteLine(msg);
            //lstMessage.Items.Insert(0, msg);
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            /*ezPrice bid = ++currentInstrument.Bid;
            ezPrice ask = --currentInstrument.Ask;

            api.BuyLimit(currentInstrument, 7, ask);
            api.SellLimit(currentInstrument, 3, bid);

            Console.WriteLine("ORDER PRICES: {0}  {1}", bid, ask);/*
        }

        private void btnSubscribe_Click(object sender, EventArgs e)
        {
            /*currentInstrument = api.ShowInstrumentDialog();

            if (currentInstrument != null)
                btnOrder.Enabled = true;*/
        }

        private void btnMarketData_Click(object sender, EventArgs e)
        {
            /*var marketDataForm = new MarketDataForm();
            marketDataForm.API = api;
            marketDataForm.Show();*/
        }


        private void cmdChartRequest_Click(object sender, System.EventArgs e)
        {
            /*
            chartDataForm.moSelectedExchange = moExchange;
            chartDataForm.moSelectedContract = moContract;
            chartDataForm.moMarket = moPickerMarket;

            // Configure the chart control.
            chartDataForm.ConfigureGridControl();

            chartDataForm.Show();

            // Request chart data.
            ChartInterval interval = ChartInterval.Day;
            if (radHourBars.Checked)
                interval = ChartInterval.Hour;
            chartDataForm.RequestChartData(interval);
            */
        }



    } // class
} // namespace
