using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Media;
using EZAPI.Data;
using EZAPI.Messaging;
using EZAPI.Messaging.Speech;
using EZAPI.Toolbox;
using MonkeyLightning.Framework;
using MonkeyLightning.UI.Controls;

namespace MonkeyLightning.UI.Forms
{
    public enum Directions { Up, Down, Left, Right }

    public partial class DashboardForm : Form
    {
        public event EventHandler DashboardClosing;

        public bool IsClosing { get; set; }

        const int MAX_TRADE_ROWS = 100;

        GoldenRulesForm goldenRules = new GoldenRulesForm();
        TradeRowControl[] tradeRows = new TradeRowControl[MAX_TRADE_ROWS];
        int tradeRowCount = 0;
        int saveSplitterDistance;
        bool notificationVisible;
        //Email msgEmail;
        TradeBuilderForm tradeBuilderForm;
        ChartSelectorDialog chartLaunchForm;
        TestSpreadsForm spreadForm;
        bool emergencyStop = false;

        public DashboardForm()
        {
            InitializeComponent();

            IsClosing = false;

            chartLaunchForm = new ChartSelectorDialog();
            spreadForm = new TestSpreadsForm();
            spreadForm.WindowState = FormWindowState.Minimized;
            spreadForm.Show();

            saveSplitterDistance = splitContainerTrades.Height - splitContainerTrades.SplitterDistance;
            splitContainerTrades.IsSplitterFixed = false;
            notificationVisible = true;

            /*string gmailAddress = "monkeylightning70@gmail.com";
            string gmailPassword = "jordan45";
            msgEmail = new Email(gmailAddress, gmailPassword);
            msgEmail.SendMail("mhatmaker@yahoo.com", "body of my text", "first email");*/

            // Put some sample TradeRows in the dashboard.
            /*for (int i = 0; i < 1; i++)
            {
                AddTradeRow(new Trade("Unreal Trade"));
            }*/
        }

        void AddTradeRow(Trade trade)
        {
            int i = tradeRowCount++;

            tradeRows[i] = new TradeRowControl();

            if (i % 2 == 0)
                tradeRows[i].RowColor = Color.LightBlue;
            else
                tradeRows[i].RowColor = Color.LightGray;

            // If we specify a TradeMetrics object for a trade, then the
            // trade will update it automatically.
            trade.Metrics = new TradeMetrics();
            tradeRows[i].Trade = trade;
            
            panelTrades.Controls.Add(tradeRows[i]);

            trade.NotificationArrived += trade_NotificationArrived;
        }

        void trade_NotificationArrived(object sender, NotificationEventArgs e)
        {
            Trade trade = sender as Trade;

            Image image = null;
            if (e.MessageType == NotificationType.ENTER_TRADE)
                image = Properties.Resources.enter_32x32;
            else if (e.MessageType == NotificationType.EXIT_TRADE)
                image = Properties.Resources.exit_32x32;
            else if (e.MessageType == NotificationType.STOP_TRADE)
                image = Properties.Resources.stopsign_24x24;

            AddNotification(image, trade.Name, e.Message);
            //gridNotifications.Rows.Add(image, trade.Name, DateTime.Now, e.Message);
        }

        private void btnGoldenRules_Click(object sender, EventArgs e)
        {
            goldenRules.Show();
        }

        private void DashboardForm_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void DashboardForm_DragDrop(object sender, DragEventArgs e)
        {
            object data = e.Data.GetData(typeof(DateTime));
            if (data == null) { return; }
            Trace.WriteLine("Add dropped item: " + data.ToString());
        }

        private void btnHideNotifications_Click(object sender, EventArgs e)
        {
            if (notificationVisible == true)
                ShowNotifications(false);
            else
                ShowNotifications(true);
        }

        private void ShowNotifications(bool makeNotificationsVisible)
        {
            if (makeNotificationsVisible == true)
            {
                // Show Notifications area.
                splitContainerTrades.SplitterDistance = splitContainerTrades.Height - saveSplitterDistance;
                btnHideNotifications.Image = Properties.Resources.arrowDown;
                toolTip1.SetToolTip(btnHideNotifications, "Hide Notifications area");
                splitContainerTrades.IsSplitterFixed = false;
                notificationVisible = true;
            }
            else
            {
                // Hide Notifications area.
                saveSplitterDistance = splitContainerTrades.Height - splitContainerTrades.SplitterDistance;
                splitContainerTrades.SplitterDistance = splitContainerTrades.Height - btnHideNotifications.Height - 5;
                btnHideNotifications.Image = Properties.Resources.arrowUp;
                toolTip1.SetToolTip(btnHideNotifications, "Show Notifications area");
                splitContainerTrades.IsSplitterFixed = true;
                notificationVisible = false;
            }
        }

        private void btnStopAllTrades_Click(object sender, EventArgs e)
        {
            if (emergencyStop == false)
            {
                picSiren.Visible = true;
                btnStopAllTrades.Text = "Restart Trades";
                toolTip1.SetToolTip(btnStopAllTrades, "Exit Emergency Stop mode...restart all active trades");
                emergencyStop = true;

                ActivateEmergencyStop();
            }
            else
            {
                picSiren.Visible = false;
                btnStopAllTrades.Text = "Emergency Stop";
                toolTip1.SetToolTip(btnStopAllTrades, "Stop all trades immediately");
                emergencyStop = false;

                DeactivateEmergencyStop();
            }
            //Synthesizer synth = new Synthesizer();
            //synth.SynthesizerTest();

            //AddNotification(Properties.Resources.chart__3_, "HOCL Crack", null, "An event occurred with the hocl crack trade.");
        }

        private void AddNotification(Image icon, string tradeName, string description, string dateTime = null)
        {
            // If dateTime is not provided, use the current date/time.
            if (dateTime == null)
                dateTime = DateTime.Now.ToString("MM/dd/yyyy      hh:mm:ss tt");

            // Since a message arrived, show the Notifications area if it is hidden.
            if (notificationVisible == false)
                ShowNotifications(true);

            this.Invoke((MethodInvoker)delegate
            {
                gridNotifications.Rows.Add(icon, tradeName, dateTime, description);
            });

            // Scroll to bottom of grid to show new message.
            ScrollGridToBottom();
        }

        void ScrollGridToBottom()
        {
            try
            {
                this.Invoke((MethodInvoker)delegate
                {
                    gridNotifications.FirstDisplayedScrollingRowIndex = gridNotifications.RowCount - 1;
                });
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("EXCEPTION (ScrollGridToBottom): " + ex.Message);
            }
        }

        private void picLogo_Click(object sender, EventArgs e)
        {
            // Play a random chimp sound when the Monkey Lightning icon is clicked.
            Random rnd = new Random();
            Stream stream;
            if (rnd.NextDouble() > .5)
                stream = Properties.Resources.Chimpanzee;
            else
                stream = Properties.Resources.Monkeys;

            SoundPlayer snd = new SoundPlayer(stream);
            snd.Play();
        }

        private void btnNewTrade_Click(object sender, EventArgs e)
        {
            if (tradeBuilderForm == null || tradeBuilderForm.IsOpen == false)
            {
                tradeBuilderForm = new TradeBuilderForm();
                tradeBuilderForm.Show();
                tradeBuilderForm.TradeBuilderClosing += tradeBuilderForm_TradeBuilderClosing;
                tradeBuilderForm.IsOpen = true;
            }
            else
            {
                tradeBuilderForm.Activate();
                tradeBuilderForm.WindowState = FormWindowState.Normal;
            }
        }

        void tradeBuilderForm_TradeBuilderClosing(object sender, EventArgs e)
        {
            if (tradeBuilderForm.DialogResult == DialogResult.OK)
            {
                Trade trade = tradeBuilderForm.BuildTrade();
                AddTradeRow(trade);
            }
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            Win32Helper.FadeInForm(this, 1000);            
        }

        private void btnCharts_Click(object sender, EventArgs e)
        {
            WinForms.SetFormRelativePosition(chartLaunchForm, this);
            chartLaunchForm.WindowState = FormWindowState.Normal;
            chartLaunchForm.BringToFront();
            chartLaunchForm.Show();
            //WinForms.SetWindowState("MonkeyChart.vshost", Win32.WindowState.SW_NORMAL);
        }

        public void ShowLogo(bool logoVisible)
        {
            picLogo.Visible = logoVisible;
        }

        private void panelHeaderCenter_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("CLICK!");
            
            //btnCharts.Image = WinForms.Lighter(btnCharts.Image, 90, Color.Black);
        }

        private void DashboardForm_Resize(object sender, EventArgs e)
        {
            if (notificationVisible == false)
                splitContainerTrades.SplitterDistance = splitContainerTrades.Height - btnHideNotifications.Height - 5;
        }

        private void DashboardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.IsClosing == false)
            {
#if DEBUG
                if (true)
#else
                DialogResult dialogResult = MessageBox.Show(this, "Are you sure you want to quit all trades and exit Monkey Lightning?", "Exit Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
#endif                
                {
                    this.IsClosing = true;
                    for (int i = 0; i < tradeRowCount; i++)
                    {
                        TradeRowControl trc = tradeRows[i];
                        trc.Trade.Abort();
                    }

                    if (DashboardClosing != null) DashboardClosing(this, EventArgs.Empty);
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }


        void ActivateEmergencyStop()
        {
            for (int i = 0; i < tradeRowCount; i++)
            {
                tradeRows[i].AutoRestart = false;
                tradeRows[i].Active = false;
                tradeRows[i].Trade.Abort();
                tradeRows[i].Enabled = false;
            }
            AddNotification(Properties.Resources.exclamation26x26, "", "EMERGENCY STOP activated.");
        }

        void DeactivateEmergencyStop()
        {
            for (int i = 0; i < tradeRowCount; i++)
            {
                tradeRows[i].Trade.Reset();
                tradeRows[i].Enabled = true;
            }
            AddNotification(Properties.Resources.exclamation26x26, "", "Emergency stop deactivated.");
        }

    } // class
} // namespace
