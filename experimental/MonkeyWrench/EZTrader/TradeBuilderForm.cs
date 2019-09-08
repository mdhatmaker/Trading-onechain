using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TT.Hatmaker;

namespace MonkeyLightning
{
    public partial class TradeBuilderForm : Form
    {
        string initialTradeNameText;
        CircularLinkedList<Panel> tradeStepPanels;
        Node<Panel> activePanel;

        public TradeBuilderForm()
        {
            InitializeComponent();

            initialTradeNameText = txtTradeName.Text;

            tradeStepPanels = new CircularLinkedList<Panel>();
            tradeStepPanels.AddFirst(panelPreconditions);
            tradeStepPanels.AddLast(panelEntry);
            tradeStepPanels.AddLast(panelExit);
            tradeStepPanels.AddLast(panelStop);

            activePanel = tradeStepPanels.Head;
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

        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<Control, Point> controlDestination = new Dictionary<Control, Point>();
            
            // Go through each panel in our list (order is unimportant).
            foreach (Panel panel in tradeStepPanels)
            {
                // Find the panel's node in our circular linked list.
                Node<Panel> node = tradeStepPanels.Find(panel);
                // And then find the NEXT location in the list.
                if (node != null)
                {
                    Point nextLocation = node.Next.Value.Location;
                    controlDestination[panel] = nextLocation;
                }
            }

            Transitions t = new Transitions();
            t.MoveControls(controlDestination);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TradeRule rule = new TradeRule("my first rule");
            rulesPreconditions.AddRule(rule);
        }
    } // class
} // namespace

