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
    public partial class TradeRowControl : UserControl
    {
        public string TradeName
        {
            get { return tradeName; }
            set
            {
                tradeName = value;
                lblTradeName.Text = tradeName;
            }
        }
        public Color RowColor
        {
            get { return this.BackColor; }
            set
            {
                this.BackColor = value;
            }
        }

        private string tradeName;

        public TradeRowControl()
        {
            InitializeComponent();
        }
    }
}
