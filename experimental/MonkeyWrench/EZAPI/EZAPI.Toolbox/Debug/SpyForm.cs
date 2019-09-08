using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EZAPI.Toolbox.Debug
{
    public partial class SpyForm : Form
    {
        public static bool bClosing { get; set; }

        private const int MAX_LIST_ITEMS = 200;

        public SpyForm()
        {
            InitializeComponent();

            this.BringToFront();
        }

        public void Print(string msg)
        {
            if (!this.IsHandleCreated)
                return;

            this.Invoke((MethodInvoker)delegate
            {
                this.Text = msg;
                listOutput.Items.Insert(0, msg);
                if (listOutput.Items.Count > MAX_LIST_ITEMS)
                    listOutput.Items.RemoveAt(MAX_LIST_ITEMS);
            });
        }

    } // class
} // namespace
