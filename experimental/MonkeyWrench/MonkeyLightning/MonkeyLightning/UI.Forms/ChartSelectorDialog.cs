using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EZAPI;
using EZAPI.Framework;
using EZAPI.Framework.Base;
using EZAPI.Framework.Chart;
using EZAPI.Toolbox;
using EZAPI.Toolbox.DataStructures;

namespace MonkeyLightning.UI.Forms
{
    public partial class ChartSelectorDialog : Form
    {
        public ezContract selectedContract;
        public EZInstrument selectedMarket;
        public EZInstrument selectedChartInstrument;

        private APIMain api;

        public ChartSelectorDialog()
        {
            InitializeComponent();

            api = APIMain.Instance;

            foreach (string intervalName in Enum.GetNames(typeof(zChartInterval)))
            {
                comboChartInterval.Items.Add(intervalName);
            }
            comboChartInterval.SelectedIndex = 0;
        }

        private void RequestChartData(EZInstrument instrument, zChartInterval interval, int period)
        {
            ChartDataForm chartForm;

            // Set EndDate to the current trading date.
            //DateTime dtEndDate = selectedContract.GetTradeDate(DateTime.Now);
            DateTime dtEndDate = DateTime.Now;

            DateTime dtStartDate;

            // Pick a different start date depending on if we are viewing DAYS, HOURS, MINUTES, etc...
            if (interval == zChartInterval.Week)
            {
                dtStartDate = dtEndDate.AddMonths(-16);
            }
            else if (interval == zChartInterval.Day)
            {
                dtStartDate = dtEndDate.AddMonths(-5);
            }
            else if (interval == zChartInterval.Minute)
            {
                dtStartDate = dtEndDate;
                if (period <= 15)   // 15 minute (or less) bars
                {
                    dtStartDate = dtStartDate.AddTradeHours(-4);
                }
                else                // more than 15 minute bars
                {
                    dtStartDate = dtStartDate.AddTradeDays(-2);
                }
            }
            else
            {
                // Anything we haven't covered, we'll load a couple days (not ideal - needs to be improved).
                dtStartDate = dtEndDate;
                dtStartDate = dtStartDate.AddTradeDays(-3);

                /*// This little loop here will ensure that we load the previous trade date as well as today and will account for weekends
                // and holidays.
                while ((selectedContract.GetTradeDate(dtStartDate) == dtEndDate))
                {
                    dtStartDate = dtStartDate.AddDays(-1);
                }*/
            }

            // Create a BarInterval object to tell the API what bar interval we want.
            // So for example, if we wanted a 15 minute bar, we would do:  New ezBarInterval(zChartDataType.Minute, 15)
            ezBarInterval ezbi = new ezBarInterval(interval, period);

            IChartDataProvider provider = new ChartDataProviderCTS(instrument.Name + " : " + ezbi, ezbi, ezSessionTimeRange.Empty);
            chartForm = new ChartDataForm(provider);
            WinForms.SetWaitCursor(true);
            //provider.LoadHistoricalChartData(APIMain.MarketFromInstrument(instrument), dtStartDate, dtEndDate);
            provider.LoadRealTimeChartData(APIMain.MarketFromInstrument(instrument), dtStartDate);

            chartForm.Show();
        }

        private void btnChartMarket_Click(object sender, EventArgs e)
        {
            WinForms.SetWaitCursor(true);

            try
            {
                zChartInterval interval = (zChartInterval)Enum.Parse(typeof(zChartInterval), comboChartInterval.Text);

                EZInstrument instrument = APIMain.Instance.ShowInstrumentDialog();

                if (instrument == null)
                {
                    status.Text = "No instrument selected.";
                }
                else
                {
                    status.Text = "New chart created.";
                    selectedChartInstrument = instrument;
                    RequestChartData(instrument, interval, Convert.ToInt32(numericPeriod.Value));
                }
            }
            finally
            {
                WinForms.SetWaitCursor(false);
                this.Close();
            }
        }

        private void TestChartForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

    } // class
} // namespace
