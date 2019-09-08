using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TradingTechnologies.TTAPI;
using TradingTechnologies.TTAPI.WinFormsHelpers;
using EZAPI;
using EZAPI.Containers;

namespace TTScript
{
    public partial class EZAPITestForm : Form
    {
        private TTAPIFunctions api;
        private Thread _apiThread;

        private Dictionary<InstrumentKey, TTInstrument> ttInstruments = new Dictionary<InstrumentKey, TTInstrument>();

        public EZAPITestForm()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string ttUsername = txtUsername.Text;
            string ttPassword = txtPassword.Text;

            api = new TTAPIFunctions(ttUsername, ttPassword);
            api.OnInitialize += new InitializeHandler(api_OnInitialize);
            api.OnInstrumentFound += new InstrumentFoundHandler(api_OnInstrumentFound);
            api.OnInsideMarketUpdate += new InsideMarketHandler(api_OnInsideMarketUpdate);
            api.OnFill += new FillHandler(api_OnFill);
            api.OnOrder += new OrderHandler(api_OnOrder);

            _apiThread = new Thread(api.Start);
            _apiThread.Name = "TTAPI Thread";
            _apiThread.Start();

            btnConnect.Enabled = false;
        }

        void api_OnInstrumentFound(TTInstrument ttInstrument, bool success)
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
                btnPrices.Enabled = true;
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

        void api_OnOrder(Instrument instrument, TTOrder order)
        {
            //cross thread - so you don't get the cross theading exception
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    api_OnOrder(instrument, order);
                });
                return;
            }
        }

        void api_OnFill(Instrument instrument, TTFill fill)
        {
            //cross thread - so you don't get the cross theading exception
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    api_OnFill(instrument, fill);
                });
                return;
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

        void api_OnInsideMarketUpdate(TTInstrument instrument)
        {
            //cross thread - so you don't get the cross theading exception
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    api_OnInsideMarketUpdate(instrument);
                });
                return;
            }

            //InstrumentKey key = instrument.Key;
            //TTInstrument tti = ttInstruments[key];
        }

        private void TTScriptForm_DragDrop(object sender, DragEventArgs e)
        {
            //SubscribeToInstrument("CME", "FUTURE", "CL", "Nov12"); 
            
            if (e.Data.GetInstrumentKeys().Count > 0)
            {
                foreach (InstrumentKey ik in e.Data.GetInstrumentKeys())
                {
                    api.SubscribeToInstrument(ik);
                }
            }
        }

        private void TTScriptForm_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetInstrumentKeys().Count > 0)
                e.Effect = DragDropEffects.Copy;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }

        private void TTScriptForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _apiThread.Abort();
        }

        private void Message(string msg)
        {
            lstMessage.Items.Add(msg);
        }

        private void btnPrices_Click(object sender, EventArgs e)
        {
            foreach (InstrumentKey key in ttInstruments.Keys)
            {
                TTInstrument tti = ttInstruments[key];
                Message(string.Format("{0}:{1} {2}:{3}", tti.BidQty, tti.Bid, tti.Ask, tti.AskQty));
            }
        }

        private void btnSubscribe_Click(object sender, EventArgs e)
        {
            api.SubscribeToInstrument("CME", "FUTURE", "HG", "Dec12");
        }

    } // class
} // namespace
