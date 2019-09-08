using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MonkeyLightning.UI.Forms
{
    public partial class GoldenRulesForm : Form
    {
        public GoldenRulesForm()
        {
            InitializeComponent();
        }

        private void listViewGoldenRules_MouseDown(object sender, MouseEventArgs e)
        {
            
            listViewGoldenRules.DoDragDrop(this.listViewGoldenRules.SelectedItems, DragDropEffects.Move);
        }

        private void listViewGoldenRules_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void GoldenRulesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    } // class
} // namespace
