using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using EZAPI.Messaging;
using EZAPI.Messaging.Speech;
using TT.Hatmaker;

namespace MonkeyLightning
{
    public enum Directions { Up, Down, Left, Right }

    public partial class DashboardForm : Form
    {
        GoldenRulesForm goldenRules = new GoldenRulesForm();
        TradeRowControl[] tradeRows = new TradeRowControl[10];
        int saveSplitterDistance;
        Directions hideNotificationArrow = Directions.Down;
        //Email msgEmail;

        public DashboardForm()
        {
            InitializeComponent();

            saveSplitterDistance = splitContainerTrades.SplitterDistance;

            /*string gmailAddress = "monkeylightning70@gmail.com";
            string gmailPassword = "jordan45";
            msgEmail = new Email(gmailAddress, gmailPassword);
            msgEmail.SendMail("mhatmaker@yahoo.com", "body of my text", "first email");*/

            // Put some sample TradeRows in the dashboard.
            for (int i = 0; i < 8; i++)
            {
                tradeRows[i] = new TradeRowControl();
                tradeRows[i].TradeName = "giddy up";
                
                if (i % 2 == 0)
                    tradeRows[i].RowColor = Color.LightBlue;
                else
                    tradeRows[i].RowColor = Color.LightGray;

                panelTrades.Controls.Add(tradeRows[i]);
            }
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
            if (hideNotificationArrow == Directions.Down)
            {
                saveSplitterDistance = splitContainerTrades.SplitterDistance;
                int height = splitContainerTrades.Size.Height;
                splitContainerTrades.SplitterDistance = height - btnHideNotifications.Height - 5;
                //btnShowNotifications.Enabled = true;
                btnHideNotifications.Image = Properties.Resources.arrowUp;
                toolTip1.SetToolTip(btnHideNotifications, "Show Notifications area");
                hideNotificationArrow = Directions.Up;
            }
            else
            {
                splitContainerTrades.SplitterDistance = saveSplitterDistance;
                btnHideNotifications.Image = Properties.Resources.arrowDown;
                toolTip1.SetToolTip(btnHideNotifications, "Hide Notifications area");
                hideNotificationArrow = Directions.Down;
            }
        }

        private void btnStopAllTrades_Click(object sender, EventArgs e)
        {
            picSiren.Visible = true;
            
            //Synthesizer synth = new Synthesizer();
            //synth.SynthesizerTest();

            //AddNotification(Properties.Resources.chart__3_, "HOCL Crack", null, "An event occurred with the hocl crack trade.");
        }

        private void AddNotification(Image icon, string tradeName, string dateTime, string description)
        {
            if (dateTime == null)
                dateTime = DateTime.Now.ToString("MM/dd/yyyy      hh:mm:ss tt");
            gridNotifications.Rows.Add(icon, tradeName, dateTime, description);
            // Scroll to bottom of grid to show new message.
            gridNotifications.FirstDisplayedScrollingRowIndex = gridNotifications.RowCount - 1;
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
            TradeBuilderForm tradeBuilderForm = new TradeBuilderForm();
            tradeBuilderForm.Show();
        }

    } // class
} // namespace
