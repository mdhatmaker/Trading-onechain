using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MonkeyLightning.Framework;

namespace MonkeyLightning.UI.Controls
{
    public partial class TradeRulePanelControl : UserControl
    {
        public List<TradeRule> Rules { get { return ruleSheet.Rules; } }

        public TradeRulePanelControl()
        {
            InitializeComponent();
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text
        {
            get
            {
                return lblTitle.Text;
            }
            set
            {
                lblTitle.Text = value;
            }
        }

        public TradeRule SelectedRule
        {
            get
            {
                return ruleSheet.SelectedRule;
            }
        }

        public void AddRule(TradeRule rule, bool Active = true)
        {
            ruleSheet.AddRule(rule, Active);
        }

        public void RemoveSelectedRule()
        {
            ruleSheet.RemoveSelectedRule();
        }

    } // class
} // namespace
