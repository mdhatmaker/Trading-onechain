using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GuiTools;
using Tools;
using static Tools.G;
using ZeroSumAPI;

namespace ZeroSumAPIManager
{
    public partial class ApiTestForm : Form
    {
        private TengineTT m_api;
        private TradingEngine m_te;

        private string m_pathname;
        //private StrategyA m_strategy;

        public ApiTestForm()
        {
            InitializeComponent();

            if (CheckTTAPIArchitecture() == true)
            {
                m_api = new TengineTT();
                m_te = m_api as TradingEngine;
                //m_te.SetTradingEngineCallbacks(this);

                //PopulateTradingEngineDropdown();

                G.COutput += G_COutput;
                //writer = new RichTextBoxConsole(rtbStrategyOutput);
                //Console.SetOut(writer);
            }
        }

        private void G_COutput(MessageArgs e)
        {
            if (rtbStrategyOutput.InvokeRequired)
                rtbStrategyOutput.Invoke(new Action<MessageArgs>(G_COutput), new object[] { e });
            else
            {
                rtbStrategyOutput.AppendText(e.Text + "\n");
                rtbStrategyOutput.ScrollToBottom();
            }
        }

        /*private void PopulateTradingEngineDropdown()
        {
            cboTradingEngines.Items.Add("TT");
            cboTradingEngines.Items.Add("CTS T4");
            cboTradingEngines.Items.Add("Kraken");
            cboTradingEngines.Items.Add("Bittrex");
            cboTradingEngines.Items.Add("XCrypto");
            cboTradingEngines.SelectedIndex = 0;
        }*/

        public bool CheckTTAPIArchitecture()
        {
            // Check that the compiler settings are compatible with the version of TT API installed
            TTAPIArchitectureCheck archCheck = new TTAPIArchitectureCheck();
            if (archCheck.validate())
            {
                cout("TTAPI Architecture check passed.");
                return true;
            }
            else
            {
                ErrorMessage("Architecture check failed.  {0}", archCheck.ErrorString);
                return false;
            }
        }

        private void StrategyForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (!m_te.IsShutdown)
            {
                e.Cancel = true;
                m_te.Shutdown();
            }
            else
            {
                base.OnFormClosing(e);
            }
        }

    }
}
