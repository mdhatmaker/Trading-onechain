using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EZAPI.Framework.Chart
{
    public partial class UIFormChooseBarData : Form
    {
        public event Action<int> SelectedBarChanged;

        public DialogResult UserSelection { get; private set; }
        public string SelectedSeries { get { return selectedSeries; } }

        public int BarIndex
        {
            get { return barIndex; }
            set
            {
                if (barIndex != value)
                {
                    barIndex = value;
                    UpdateBarDataDisplay();
                    if (SelectedBarChanged != null) SelectedBarChanged(barIndex);
                }
            }
        }

        public int ReverseBarIndex { get { return chartDataProvider.ChartData.TradeBars.Count - barIndex - 1; } }

        public bool IsClosing { get; set; }

        private IChartDataProvider chartDataProvider;
        private int barIndex;
        private string selectedSeries;

        public UIFormChooseBarData(IChartDataProvider provider)
        {
            this.UserSelection = DialogResult.Abort;
            this.selectedSeries = null;
            this.chartDataProvider = provider;
            
            // Set our initial bar index to the last (current) bar.
            this.barIndex = provider.ChartData.TradeBars.Count - 1;
            UpdateBarDataDisplay();

            InitializeComponent();
        }

        void UpdateBarDataDisplay()
        {
            // We are just updating UI features, so if the form is not yet created, then return.
            if (!this.IsHandleCreated)
                return;

            if (ReverseBarIndex == 0)
                lblSelectedBar.Text = "Current Bar";
            else if (ReverseBarIndex == 1)
                lblSelectedBar.Text = "Previous Bar";
            else
                lblSelectedBar.Text = string.Format("Current Bar - {0}", ReverseBarIndex);

            // Enable the Previous and Next buttons, then disable them if we are
            // at the boundary of available bar data.
            btnNext.Enabled = true;
            btnPrevious.Enabled = true;
            btnFirst.Enabled = true;
            btnLast.Enabled = true;

            if (ReverseBarIndex == 0)
            {
                btnNext.Enabled = false;
                btnLast.Enabled = false;
            }
            // No "else if" here for the VERY unlikely scenario that there is only one bar (it is the first AND last bar).
            if (barIndex == 0)
            {
                btnPrevious.Enabled = false;
                btnFirst.Enabled = false;
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            --BarIndex;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            ++BarIndex;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            BarIndex = 0;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            BarIndex = chartDataProvider.ChartData.TradeBars.Count - 1;
        }

        public void SetSeriesData(Dictionary<string, string> seriesData)
        {
            listSeriesData.Items.Clear();
            foreach (string key in seriesData.Keys)
            {
                ListViewItem item = new ListViewItem();
                item.Text = key;
                item.SubItems.Add(seriesData[key]);
                listSeriesData.Items.Add(item);
                Console.WriteLine("{0} : {1}", key, seriesData[key]);
            }
        }

        private void UIFormChooseBarData_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsClosing == true)
                return;

            this.Hide();
            e.Cancel = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            UserSelection = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listSeriesData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listSeriesData.SelectedItems.Count > 0)
            {
                selectedSeries = listSeriesData.SelectedItems[0].Text;
                lblIndicatorName.Text = selectedSeries;
            }
        }

    } // class
} // namespace
