using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EZAPI.Toolbox;
using MonkeyLightning.Framework;
using MonkeyLightning.Framework.IO;

namespace MonkeyLightning.UI.Forms
{
    public delegate void RuleSelectHandler(object sender, RuleSelectEventArgs e);

    public partial class RuleChooserForm : Form
    {
        public event RuleSelectHandler RuleSelect;

        private Form parentForm;

        public RuleChooserForm(Form parentForm)
        {
            InitializeComponent();

            ruleSheet.StatusMessageUpdate += ruleSheet_StatusMessageUpdate;
            ruleSheet.ErrorMessageUpdate += ruleSheet_ErrorMessageUpdate;

            this.parentForm = parentForm;
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        void ruleSheet_ErrorMessageUpdate(string msg)
        {
            lblHeader.Text = msg;
        }

        void ruleSheet_StatusMessageUpdate(string msg)
        {
            lblStatus.Text = msg;
        }

        private void btnNewRule_Click(object sender, EventArgs e)
        {
            this.Opacity = .3;
            parentForm.Opacity = .3;
            using (var form = new RuleBuilderForm())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    TradeRule tradeRule = form.Rule;
                    ruleSheet.AddRule(tradeRule);
                    MonkeyIO.SaveRule(tradeRule);
                }
            }
            this.Opacity = 1.0;
            parentForm.Opacity = 1.0;
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            if (RuleSelect != null && ruleSheet.SelectedRule != null)
                RuleSelect(this, new RuleSelectEventArgs(ruleSheet.SelectedRule));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void RuleChooserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void RuleChooserForm_Load(object sender, EventArgs e)
        {
            List<TradeRule> rules = MonkeyIO.LoadAllRules();
            if (rules == null || rules.Count == 0)
                return;

            foreach (TradeRule rule in rules)
            {
                if (rule != null)
                    ruleSheet.AddRule(rule);
            }
        }

    } // class

    public class RuleSelectEventArgs
    {
        public TradeRule Rule { get { return tradeRule; } }

        private TradeRule tradeRule;

        public RuleSelectEventArgs(TradeRule rule)
        {
            this.tradeRule = rule;
        }
    }
} // namespace
