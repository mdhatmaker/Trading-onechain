using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MonkeyLightning
{
    public partial class RuleSheetControl : UserControl
    {
        public RuleSheetControl()
        {
            InitializeComponent();
        }

        public void AddRule(TradeRule rule, bool Active = true)
        {
            gridRules.Rows.Add(Active, rule.Name);
        }
    } // class
} // namespace
