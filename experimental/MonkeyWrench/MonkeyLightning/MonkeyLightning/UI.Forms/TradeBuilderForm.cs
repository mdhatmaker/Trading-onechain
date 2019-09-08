using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EZAPI;
using EZAPI.Framework;
using EZAPI.Framework.Base;
using EZAPI.Toolbox;
using EZAPI.Toolbox.DataStructures;
using MonkeyLightning.Framework;
using MonkeyLightning.UI.Controls;

namespace MonkeyLightning.UI.Forms
{
    public partial class TradeBuilderForm : Form
    {
        public event EventHandler TradeBuilderClosing;

        public bool IsOpen { get; set; }
        public zBuySell TradeEntrySide { get { return entrySide; } }

        string initialTradeNameText;
        CircularLinkedList<TradeRulePanelControl> tradeStepPanels;
        Node<TradeRulePanelControl> activePanel;
        RuleChooserForm ruleChooserForm;
        EZInstrument tradeInstrument;
        zBuySell entrySide;

        APIMain api;

        public TradeBuilderForm()
        {
            InitializeComponent();

            api = APIMain.Instance;

            this.DialogResult = DialogResult.Abort;

            initialTradeNameText = txtTradeName.Text;
            // Default to a buy-side entry.
            entrySide = zBuySell.Buy;

            tradeStepPanels = new CircularLinkedList<TradeRulePanelControl>();
            tradeStepPanels.AddFirst(panelPreconditions);
            tradeStepPanels.AddLast(panelStop);
            tradeStepPanels.AddLast(panelExit);
            tradeStepPanels.AddLast(panelEntry);

            ChangeActivePanel(tradeStepPanels.Head);

            ruleChooserForm = new RuleChooserForm(this);
            ruleChooserForm.RuleSelect += ruleChooserForm_RuleSelect;
        }

        void ruleChooserForm_RuleSelect(object sender, RuleSelectEventArgs e)
        {
            activePanel.Value.AddRule(e.Rule);
        }

        private void txtTradeName_Enter(object sender, EventArgs e)
        {
            checkTradeName();
        }

        private void txtTradeName_Click(object sender, EventArgs e)
        {
            checkTradeName();
        }

        /// <summary>
        /// If this is the first time the user has selected the field to rename the trade,
        /// then we will select all the text (so he can easily type over it).
        /// </summary>
        private void checkTradeName()
        {
            if (txtTradeName.Text.Equals("(trade name)"))
            {
                txtTradeName.SelectionStart = 0;
                txtTradeName.SelectAll();
            }
        }

        private void btnNextTradeStep_Click(object sender, EventArgs e)
        {
            btnNextTradeStep.Enabled = false;

            Dictionary<Control, Point> controlDestination = new Dictionary<Control, Point>();

            ChangeActivePanel(activePanel.Previous);

            // Go through each panel in our list (order is unimportant).
            foreach (TradeRulePanelControl panel in tradeStepPanels)
            {
                // Find the panel's node in our circular linked list.
                Node<TradeRulePanelControl> node = tradeStepPanels.Find(panel);
                // And then find the NEXT location in the list.
                if (node != null)
                {
                    Point nextLocation = node.Next.Value.Location;
                    controlDestination[panel] = nextLocation;
                }
            }

            Transitions t = new Transitions();
            t.TransitionCompleted += t_TransitionCompleted;
            t.MoveControls(controlDestination);
        }

        void t_TransitionCompleted(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                btnNextTradeStep.Enabled = true;
            });
        }

        private void ChangeActivePanel(Node<TradeRulePanelControl> setActivePanel)
        {
            activePanel = setActivePanel;

            CheckSelectedRule();
        }

        private void CheckSelectedRule()
        {
            if (activePanel.Value.SelectedRule != null)
            {
                btnEditRule.Enabled = true;
                btnRemoveRule.Enabled = true;
            }
            else
            {
                btnEditRule.Enabled = false;
                btnRemoveRule.Enabled = false;
            }
        }

        private void btnAddRule_Click(object sender, EventArgs e)
        {
            EZAPI.Toolbox.WinForms.SetFormRelativePosition(ruleChooserForm, this, FormRelativePosition.RIGHT);
            /*ruleChooserForm.Left = this.Left + this.Width + 40;
            ruleChooserForm.Top = this.Top - 40;*/
            ruleChooserForm.Show();
            ruleChooserForm.WindowState = FormWindowState.Normal; // in case the user had minimized the Rule Chooser
            ruleChooserForm.BringToFront();

            CheckSelectedRule();
        }

        private void btnRemoveRule_Click(object sender, EventArgs e)
        {
            activePanel.Value.RemoveSelectedRule();
            CheckSelectedRule();
        }

        private void btnEditRule_Click(object sender, EventArgs e)
        {
            (new RuleBuilderForm()).Show();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            if (TradeBuilderClosing != null) TradeBuilderClosing(this, EventArgs.Empty);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            if (TradeBuilderClosing != null) TradeBuilderClosing(this, EventArgs.Empty);
            this.Close();
        }

        private void TradeBuilderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsOpen = false;
            ruleChooserForm.Close();
        }

        private void TradeBuilderForm_Load(object sender, EventArgs e)
        {
            //Win32Helper.FadeInForm(this);            
        }

        public Trade BuildTrade()
        {
            Trade trade = null;

            trade = new Trade(txtTradeName.Text.Trim(), tradeInstrument);

            trade.BuySell = entrySide;

            // TODO Tie in the Execution Engine (chosen from the Trade Builder Form)

            trade.Steps[TradeStepType.PRECONDITIONS].AddRules(panelPreconditions.Rules);
            trade.Steps[TradeStepType.ENTRY].AddRules(panelEntry.Rules);
            trade.Steps[TradeStepType.EXIT].AddRules(panelExit.Rules);
            trade.Steps[TradeStepType.STOP].AddRules(panelStop.Rules);
                        
            return trade;            
        }

        /// <summary>
        /// I'm setting the WaitCursor here (especially for the first time the
        /// market dialog loads, it can be a little slow). The "finally" should
        /// ensure that we are never left hanging with the WaitCursor. But I
        /// will also attempt to set the cursor back to default within the
        /// ShowInstrumentDialog method (and/or the showing of the form).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectMarket_Click(object sender, EventArgs e)
        {
            txtMarket.Text = "Loading Market Chooser...";
            Application.DoEvents();

            Cursor.Current = Cursors.WaitCursor;
            Cursor.Show();

            try
            {
                // Store the existing trade instrument in case we want to
                // change it back (if the user cancels the market selection
                // dialog, for instance).
                EZInstrument storeTradeInstrument = tradeInstrument;

                tradeInstrument = api.ShowInstrumentDialog();

                if (tradeInstrument != null)
                    txtMarket.Text = tradeInstrument.Name;
                else
                {
                    // User cancelled the market selection dialog, so let's
                    // try to restore the previously selected trade instrument
                    // (which may have been null - so handle that also).
                    tradeInstrument = storeTradeInstrument;
                    if (tradeInstrument == null)
                        txtMarket.Text = "(no market selected)";
                    else
                        txtMarket.Text = tradeInstrument.Name;
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                Cursor.Show();
            }
        }

        private void chkBuy_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chkBuy_Click(object sender, EventArgs e)
        {
            if (chkBuy.Checked == true)
            {
                entrySide = zBuySell.Buy;
                chkSell.Checked = false;
            }
            else
            {
                entrySide = zBuySell.Sell;
                chkSell.Checked = true;
            }
            FormatBuySellButtons();
        }

        private void chkSell_Click(object sender, EventArgs e)
        {
            if (chkSell.Checked == true)
            {
                entrySide = zBuySell.Sell;
                chkBuy.Checked = false;
            }
            else
            {
                entrySide = zBuySell.Buy;
                chkBuy.Checked = true;
            }
            FormatBuySellButtons();
        }

        void FormatBuySellButtons()
        {
            // Format the BUY button (color BLUE if it is active).
            if (chkBuy.Checked == true)
            {
                chkBuy.BackColor = Color.Blue;
                chkBuy.ForeColor = Color.White;
            }
            else
            {
                chkBuy.BackColor = Color.FromKnownColor(KnownColor.Control);
                chkBuy.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
            }

            // Format the SELL button (color RED if it is active).
            if (chkSell.Checked == true)
            {
                chkSell.BackColor = Color.Red;
                chkSell.ForeColor = Color.White;
            }
            else
            {
                chkSell.BackColor = Color.FromKnownColor(KnownColor.Control);
                chkSell.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
            }

        }

    } // class
} // namespace

