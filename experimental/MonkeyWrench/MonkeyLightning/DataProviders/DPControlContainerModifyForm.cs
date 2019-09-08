using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MonkeyLightning.DataProvider
{
    public partial class DPControlContainerModifyForm : Form
    {
        public Button ButtonOK { get { return btnOK; } }
        public Button ButtonCancel { get { return btnCancel; } }

        public static DPControlContainerModifyForm CreateForm(DPModifyControl modifyControl)
        {
            Size controlSize = modifyControl.Size;
            var dpModifyForm = new DPControlContainerModifyForm();
            dpModifyForm.Width = controlSize.Width + 16;
            dpModifyForm.Height = controlSize.Height + 112;
            Panel panel = dpModifyForm.Controls["panelControl"] as Panel;
            panel.Controls.Add(modifyControl);
            modifyControl.Dock = DockStyle.Fill;
            //dpModifyForm.OKButton.Enabled = false;

            return dpModifyForm;
        }

        public DPControlContainerModifyForm()
        {
            InitializeComponent();

            // Assume DialogResult of Cancel in case User closes the window with the "X".
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DataProviderModifyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Control control in panelControl.Controls)
            {
                if (control is DPModifyControl)
                    (control as DPModifyControl).ParentFormClosing();
            }
        }
    } // class
} // namespace
