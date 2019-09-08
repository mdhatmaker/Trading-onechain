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
    public partial class RuleBuilderForm : Form
    {
        public TradeRule Rule {
            get
            {
                string ruleName = txtRuleName.Text.Trim();
                BooleanRuleCombination booleanCombination = BooleanRuleCombination.AND;
                if (cboCombineConditions.SelectedIndex == 1)
                    booleanCombination = BooleanRuleCombination.OR;
                TradeRuleType ruleType = (TradeRuleType) Enum.Parse(typeof(TradeRuleType), cboRuleType.Text, true);
                TradeRule tradeRule = new TradeRule(ruleName, booleanCombination, ruleType);
                foreach (RuleCondition condition in conditionsControl.Conditions)
                {
                    tradeRule.AddRuleCondition(condition);
                }
                return tradeRule;
            }
        }

        public RuleBuilderForm()
        {
            InitializeComponent();

            cboCombineConditions.SelectedIndex = 0;
            cboRuleType.SelectedIndex = 0;

            conditionsControl.SelectionChanged += conditionsControl_SelectionChanged;

            CheckEnableEditDelete();
        }

        void conditionsControl_SelectionChanged(object sender, EventArgs e)
        {
            CheckEnableEditDelete();
        }

        void CheckEnableEditDelete()
        {
            if (conditionsControl.SelectedCondition != null)
            {
                btnEditCondition.Enabled = true;
                btnDeleteCondition.Enabled = true;
            }
            else
            {
                btnEditCondition.Enabled = false;
                btnDeleteCondition.Enabled = false;
            }
        }

        private void btnAddCondition_Click(object sender, EventArgs e)
        {
            using (var form = new RuleConditionForm())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    RuleCondition condition = form.Condition;
                    conditionsControl.AddCondition(condition);
                    //MonkeyIO.SaveCondition(condition);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            CloseForm();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            CloseForm();

        }

        private void txtRuleName_Enter(object sender, EventArgs e)
        {
            checkRuleName();
        }

        private void txtRuleName_Click(object sender, EventArgs e)
        {
            checkRuleName();
        }

        /// <summary>
        /// If this is the first time the user has selected the field to rename the rule,
        /// then we will select all the text (so he can easily type over it).
        /// </summary>
        private void checkRuleName()
        {
            if (txtRuleName.Text.Equals("(rule name)"))
            {
                txtRuleName.SelectionStart = 0;
                txtRuleName.SelectAll();
            }
        }

        private void btnEditCondition_Click(object sender, EventArgs e)
        {
            RuleCondition selectedCondition = conditionsControl.SelectedCondition;

            if (selectedCondition == null) return;

            using (var form = new RuleConditionForm(selectedCondition))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    RuleCondition updatedCondition = form.Condition;
                    conditionsControl.UpdateCondition(selectedCondition, updatedCondition);
                }
            }
        }

        private void btnDeleteCondition_Click(object sender, EventArgs e)
        {
            RuleCondition selectedCondition = conditionsControl.SelectedCondition;

            if (selectedCondition == null) return;

            conditionsControl.DeleteCondition(selectedCondition);
        }

        private void RuleBuilderForm_Load(object sender, EventArgs e)
        {
            //Win32Helper.FadeInForm(this, 250);            
        }

        private void RuleBuilderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void CloseForm()
        {
            conditionsControl.ParentFormClosing();
            this.Close();
        }

    } // class
} // namespace
