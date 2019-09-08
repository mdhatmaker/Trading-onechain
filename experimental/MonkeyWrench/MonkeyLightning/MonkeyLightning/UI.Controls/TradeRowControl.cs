using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EZAPI.Data;
using MonkeyLightning.Framework;
using MonkeyLightning.UI.Forms;

namespace MonkeyLightning.UI.Controls
{
    public partial class TradeRowControl : UserControl
    {
        public Trade Trade {
            get { return trade; }
            set
            {
                trade = value;
                lblTradeName.Text = trade.Name;
                DisplayTradeState();

                panelPreconditions.TradeStep = trade.Steps[TradeStepType.PRECONDITIONS];
                panelEntry.TradeStep = trade.Steps[TradeStepType.ENTRY];
                panelExit.TradeStep = trade.Steps[TradeStepType.EXIT];
                panelStop.TradeStep = trade.Steps[TradeStepType.STOP];

                // We'll make the STOP panel RED if it completes (GREEN is the default).
                panelStop.ColorComplete = Color.Red;

                trade.StateChanged += trade_StateChanged;
                trade.ThrottleCountdownTimer += trade_ThrottleTimer;
                trade.AutoRestartSignal += trade_AutoRestartSignal;

                if (trade.Metrics != null) trade.Metrics.TradeMetricsUpdated += Metrics_TradeMetricsUpdated;
            }
        }

        public TradeMetrics TradeMetrics { get; set; }

        public bool Active
        {
            get { return Trade.Active; }
            set
            {
                Trade.Active = value;
                chkActive.Checked = Trade.Active;
            }
        }

        public bool AutoRestart
        {
            get { return Trade.AutoRestart; }
            set
            {
                Trade.AutoRestart = value;
                chkAutoRestart.Checked = Trade.AutoRestart;
                panelThrottle.Visible = value;
                Trade.Throttle = Convert.ToDouble(txtThrottle.Text);
            }
        }


        private Trade trade;

        void DisplayTradeState()
        {
            try
            {
                this.Invoke((MethodInvoker)delegate
                {
                    lblTradeStatus.Text = trade.State.ToString();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

        public TradeRowControl()
        {
            InitializeComponent();
        }

        void RestartTrade()
        {
            this.Invoke((MethodInvoker)delegate
            {
                btnStartTrade.Visible = false;
            });

            Trade.Reset();
            Trade.Start();
        }

        private void btnStartTrade_Click(object sender, EventArgs e)
        {
            //chkActive.Checked = true;
            RestartTrade();
        }

        void Metrics_TradeMetricsUpdated(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                lblTradesEntered.Text = Trade.Metrics.EntryCount.ToString();
                lblTradesExited.Text = Trade.Metrics.ExitCount.ToString();
                lblTradesStopped.Text = Trade.Metrics.StopCount.ToString();
                lblWinPercent.Text = Trade.Metrics.WinPercent.ToString("0.0") + "%";
                lblProfitLoss.Text = Trade.Metrics.ProfitLoss.ToString("$0.00");
                double hours = Trade.Metrics.RunTime.TotalHours;
                int averageTradesPerHour = (int)Math.Round((double)Trade.Metrics.EntryCount / hours);
                lblTradesPerHour.Text = averageTradesPerHour.ToString();
            });
        }

        void trade_AutoRestartSignal(object sender, EventArgs e)
        {
            RestartTrade();
        }

        void trade_ThrottleTimer(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                lblTradeStatus.Text = "Countdown: " + trade.ThrottleCountdown.ToString();
            });
            //txtThrottle.Text = trade.ThrottleCountdown.ToString();
        }

        void trade_StateChanged(object sender, EventArgs e)
        {
            DisplayTradeState();
            if (Trade.State == TradeState.EXITED || Trade.State == TradeState.STOPPED)
            {
                if (trade.AutoRestart == true)
                {
                    // No need to show the START TRADE button if AutoRestart is on.
                }
                else
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        btnStartTrade.Visible = true;
                    });
                }
            }
            else if (Trade.State == TradeState.ABORTED)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    btnStartTrade.Visible = true;
                    btnStartTrade.Enabled = false;
                });
            }
            else if (Trade.State == TradeState.NOT_STARTED)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    btnStartTrade.Enabled = true;
                });
            }

        }

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            Trade.Active = chkActive.Checked;
        }

        private void chkAutoRestart_CheckedChanged(object sender, EventArgs e)
        {
            Trade.Throttle = Convert.ToDouble(txtThrottle.Text);
            Trade.AutoRestart = chkAutoRestart.Checked;
            panelThrottle.Visible = chkAutoRestart.Checked;
            if (chkAutoRestart.Checked == true)
            {
                //chkAutoRestart.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
                //chkAutoRestart.BackColor = Color.White;
            }
            else
            {
                //chkAutoRestart.ForeColor = Color.FromKnownColor(KnownColor.ControlDarkDark);
                //chkAutoRestart.BackColor = Color.FromKnownColor(KnownColor.Control);
            }
        }

        private void txtThrottle_TextChanged(object sender, EventArgs e)
        {
            double updatedThrottle;
            if (double.TryParse(txtThrottle.Text.Trim(), out updatedThrottle))
            {
                Trade.Throttle = updatedThrottle;
            }
        }

        private void btnTradeDescription_Click(object sender, EventArgs e)
        {
            EnglishDescriptionForm descriptionForm = new EnglishDescriptionForm(this.Trade);
            descriptionForm.Show();
        }

        private void btnPerformance_Click(object sender, EventArgs e)
        {
            if (panelTradeOptions.Visible == true)
            {
                panelTradeOptions.Visible = false;
                panelTradePerformance.Visible = true;
                btnPerformance.Image = Properties.Resources.ButtonBack;
                toolTip1.SetToolTip(btnPerformance, "Back to view trade options");
            }
            else
            {
                panelTradeOptions.Visible = true;
                panelTradePerformance.Visible = false;
                btnPerformance.Image = Properties.Resources.Dashboard;
                toolTip1.SetToolTip(btnPerformance, "View trade performance");
            }
        }


    } // class
} // namespace
