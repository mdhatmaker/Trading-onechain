namespace MonkeyLightning.UI.Controls
{
    partial class TradeRowControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblTradeName = new System.Windows.Forms.Label();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnPerformance = new System.Windows.Forms.Button();
            this.btnTradeDescription = new System.Windows.Forms.Button();
            this.btnChart = new System.Windows.Forms.Button();
            this.txtThrottle = new System.Windows.Forms.TextBox();
            this.chkAutoRestart = new System.Windows.Forms.CheckBox();
            this.panelTradeOptions = new System.Windows.Forms.Panel();
            this.panelThrottle = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTradeStatus = new System.Windows.Forms.Label();
            this.btnStartTrade = new System.Windows.Forms.Button();
            this.panelTradePerformance = new System.Windows.Forms.Panel();
            this.lblProfitLoss = new System.Windows.Forms.Label();
            this.lblTradesPerHour = new System.Windows.Forms.Label();
            this.lblWinPercent = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblTradesEntered = new System.Windows.Forms.Label();
            this.lblTradesExited = new System.Windows.Forms.Label();
            this.lblTradesStopped = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panelStop = new MonkeyLightning.UI.Controls.TradeRowStepControl();
            this.panelExit = new MonkeyLightning.UI.Controls.TradeRowStepControl();
            this.panelEntry = new MonkeyLightning.UI.Controls.TradeRowStepControl();
            this.panelPreconditions = new MonkeyLightning.UI.Controls.TradeRowStepControl();
            this.panelTradeOptions.SuspendLayout();
            this.panelThrottle.SuspendLayout();
            this.panelTradePerformance.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTradeName
            // 
            this.lblTradeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTradeName.Location = new System.Drawing.Point(19, 0);
            this.lblTradeName.Name = "lblTradeName";
            this.lblTradeName.Size = new System.Drawing.Size(173, 23);
            this.lblTradeName.TabIndex = 0;
            this.lblTradeName.Text = "Trade Name";
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Checked = true;
            this.chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActive.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkActive.Location = new System.Drawing.Point(3, 5);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(15, 14);
            this.chkActive.TabIndex = 5;
            this.toolTip1.SetToolTip(this.chkActive, "Enable or disable trade");
            this.chkActive.UseVisualStyleBackColor = true;
            this.chkActive.CheckedChanged += new System.EventHandler(this.chkActive_CheckedChanged);
            // 
            // btnPerformance
            // 
            this.btnPerformance.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPerformance.Image = global::MonkeyLightning.Properties.Resources.Dashboard;
            this.btnPerformance.Location = new System.Drawing.Point(150, 107);
            this.btnPerformance.Name = "btnPerformance";
            this.btnPerformance.Size = new System.Drawing.Size(42, 42);
            this.btnPerformance.TabIndex = 19;
            this.toolTip1.SetToolTip(this.btnPerformance, "View trade performance");
            this.btnPerformance.UseVisualStyleBackColor = true;
            this.btnPerformance.Click += new System.EventHandler(this.btnPerformance_Click);
            // 
            // btnTradeDescription
            // 
            this.btnTradeDescription.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTradeDescription.Image = global::MonkeyLightning.Properties.Resources.Blocknotes_Blue;
            this.btnTradeDescription.Location = new System.Drawing.Point(150, 23);
            this.btnTradeDescription.Name = "btnTradeDescription";
            this.btnTradeDescription.Size = new System.Drawing.Size(42, 42);
            this.btnTradeDescription.TabIndex = 18;
            this.toolTip1.SetToolTip(this.btnTradeDescription, "View or modify trade description...");
            this.btnTradeDescription.UseVisualStyleBackColor = true;
            this.btnTradeDescription.Click += new System.EventHandler(this.btnTradeDescription_Click);
            // 
            // btnChart
            // 
            this.btnChart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChart.Image = global::MonkeyLightning.Properties.Resources.chartSymbol1;
            this.btnChart.Location = new System.Drawing.Point(150, 65);
            this.btnChart.Name = "btnChart";
            this.btnChart.Size = new System.Drawing.Size(42, 42);
            this.btnChart.TabIndex = 6;
            this.toolTip1.SetToolTip(this.btnChart, "View charts for this trade...");
            this.btnChart.UseVisualStyleBackColor = true;
            // 
            // txtThrottle
            // 
            this.txtThrottle.BackColor = System.Drawing.SystemColors.Info;
            this.txtThrottle.Location = new System.Drawing.Point(46, 2);
            this.txtThrottle.Name = "txtThrottle";
            this.txtThrottle.Size = new System.Drawing.Size(24, 20);
            this.txtThrottle.TabIndex = 17;
            this.txtThrottle.Text = "5";
            this.toolTip1.SetToolTip(this.txtThrottle, "Number of seconds between end of trade and auto-restart");
            this.txtThrottle.TextChanged += new System.EventHandler(this.txtThrottle_TextChanged);
            // 
            // chkAutoRestart
            // 
            this.chkAutoRestart.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkAutoRestart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkAutoRestart.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkAutoRestart.Image = global::MonkeyLightning.Properties.Resources.ButtonRestart;
            this.chkAutoRestart.Location = new System.Drawing.Point(2, 12);
            this.chkAutoRestart.Name = "chkAutoRestart";
            this.chkAutoRestart.Size = new System.Drawing.Size(42, 40);
            this.chkAutoRestart.TabIndex = 20;
            this.toolTip1.SetToolTip(this.chkAutoRestart, "Turn on/off auto-restart of completed trades");
            this.chkAutoRestart.UseVisualStyleBackColor = true;
            this.chkAutoRestart.CheckedChanged += new System.EventHandler(this.chkAutoRestart_CheckedChanged);
            // 
            // panelTradeOptions
            // 
            this.panelTradeOptions.Controls.Add(this.panelThrottle);
            this.panelTradeOptions.Controls.Add(this.chkAutoRestart);
            this.panelTradeOptions.Controls.Add(this.lblTradeStatus);
            this.panelTradeOptions.Controls.Add(this.btnStartTrade);
            this.panelTradeOptions.Location = new System.Drawing.Point(3, 22);
            this.panelTradeOptions.Name = "panelTradeOptions";
            this.panelTradeOptions.Size = new System.Drawing.Size(147, 127);
            this.panelTradeOptions.TabIndex = 20;
            // 
            // panelThrottle
            // 
            this.panelThrottle.Controls.Add(this.label2);
            this.panelThrottle.Controls.Add(this.label1);
            this.panelThrottle.Controls.Add(this.txtThrottle);
            this.panelThrottle.Location = new System.Drawing.Point(44, 20);
            this.panelThrottle.Name = "panelThrottle";
            this.panelThrottle.Size = new System.Drawing.Size(99, 23);
            this.panelThrottle.TabIndex = 21;
            this.panelThrottle.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(70, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "sec";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(0, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Throttle:";
            // 
            // lblTradeStatus
            // 
            this.lblTradeStatus.Location = new System.Drawing.Point(3, 105);
            this.lblTradeStatus.Name = "lblTradeStatus";
            this.lblTradeStatus.Size = new System.Drawing.Size(141, 18);
            this.lblTradeStatus.TabIndex = 19;
            this.lblTradeStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStartTrade
            // 
            this.btnStartTrade.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnStartTrade.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartTrade.Location = new System.Drawing.Point(23, 68);
            this.btnStartTrade.Name = "btnStartTrade";
            this.btnStartTrade.Size = new System.Drawing.Size(91, 34);
            this.btnStartTrade.TabIndex = 18;
            this.btnStartTrade.Text = "Start Trade";
            this.btnStartTrade.UseVisualStyleBackColor = false;
            this.btnStartTrade.Click += new System.EventHandler(this.btnStartTrade_Click);
            // 
            // panelTradePerformance
            // 
            this.panelTradePerformance.Controls.Add(this.lblProfitLoss);
            this.panelTradePerformance.Controls.Add(this.lblTradesPerHour);
            this.panelTradePerformance.Controls.Add(this.lblWinPercent);
            this.panelTradePerformance.Controls.Add(this.label10);
            this.panelTradePerformance.Controls.Add(this.label9);
            this.panelTradePerformance.Controls.Add(this.lblTradesEntered);
            this.panelTradePerformance.Controls.Add(this.lblTradesExited);
            this.panelTradePerformance.Controls.Add(this.lblTradesStopped);
            this.panelTradePerformance.Controls.Add(this.label6);
            this.panelTradePerformance.Controls.Add(this.label5);
            this.panelTradePerformance.Controls.Add(this.label3);
            this.panelTradePerformance.Location = new System.Drawing.Point(3, 22);
            this.panelTradePerformance.Name = "panelTradePerformance";
            this.panelTradePerformance.Size = new System.Drawing.Size(147, 127);
            this.panelTradePerformance.TabIndex = 22;
            this.panelTradePerformance.Visible = false;
            // 
            // lblProfitLoss
            // 
            this.lblProfitLoss.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProfitLoss.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProfitLoss.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblProfitLoss.Location = new System.Drawing.Point(6, 97);
            this.lblProfitLoss.Name = "lblProfitLoss";
            this.lblProfitLoss.Size = new System.Drawing.Size(136, 28);
            this.lblProfitLoss.TabIndex = 30;
            this.lblProfitLoss.Text = "$0.00";
            this.lblProfitLoss.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTradesPerHour
            // 
            this.lblTradesPerHour.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblTradesPerHour.Location = new System.Drawing.Point(97, 77);
            this.lblTradesPerHour.Name = "lblTradesPerHour";
            this.lblTradesPerHour.Size = new System.Drawing.Size(48, 18);
            this.lblTradesPerHour.TabIndex = 29;
            this.lblTradesPerHour.Text = "0";
            this.lblTradesPerHour.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblWinPercent
            // 
            this.lblWinPercent.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblWinPercent.Location = new System.Drawing.Point(97, 59);
            this.lblWinPercent.Name = "lblWinPercent";
            this.lblWinPercent.Size = new System.Drawing.Size(48, 18);
            this.lblWinPercent.TabIndex = 28;
            this.lblWinPercent.Text = "0";
            this.lblWinPercent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label10.Location = new System.Drawing.Point(3, 59);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(90, 18);
            this.label10.TabIndex = 27;
            this.label10.Text = "Win percent :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label9.Location = new System.Drawing.Point(3, 77);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 18);
            this.label9.TabIndex = 26;
            this.label9.Text = "Trades / hour:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTradesEntered
            // 
            this.lblTradesEntered.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblTradesEntered.Location = new System.Drawing.Point(97, 3);
            this.lblTradesEntered.Name = "lblTradesEntered";
            this.lblTradesEntered.Size = new System.Drawing.Size(48, 18);
            this.lblTradesEntered.TabIndex = 25;
            this.lblTradesEntered.Text = "0";
            this.lblTradesEntered.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTradesExited
            // 
            this.lblTradesExited.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblTradesExited.Location = new System.Drawing.Point(97, 21);
            this.lblTradesExited.Name = "lblTradesExited";
            this.lblTradesExited.Size = new System.Drawing.Size(48, 18);
            this.lblTradesExited.TabIndex = 24;
            this.lblTradesExited.Text = "0";
            this.lblTradesExited.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTradesStopped
            // 
            this.lblTradesStopped.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblTradesStopped.Location = new System.Drawing.Point(97, 39);
            this.lblTradesStopped.Name = "lblTradesStopped";
            this.lblTradesStopped.Size = new System.Drawing.Size(48, 18);
            this.lblTradesStopped.TabIndex = 23;
            this.lblTradesStopped.Text = "0";
            this.lblTradesStopped.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label6.Location = new System.Drawing.Point(3, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 18);
            this.label6.TabIndex = 22;
            this.label6.Text = "...stopped:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label5.Location = new System.Drawing.Point(3, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 18);
            this.label5.TabIndex = 21;
            this.label5.Text = "...exited:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 18);
            this.label3.TabIndex = 19;
            this.label3.Text = "Trades entered:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelStop
            // 
            this.panelStop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStop.ColorAbort = System.Drawing.Color.Purple;
            this.panelStop.ColorActive = System.Drawing.Color.Yellow;
            this.panelStop.ColorComplete = System.Drawing.Color.Green;
            this.panelStop.ColorInactive = System.Drawing.SystemColors.Control;
            this.panelStop.Location = new System.Drawing.Point(810, 0);
            this.panelStop.Name = "panelStop";
            this.panelStop.Size = new System.Drawing.Size(200, 149);
            this.panelStop.TabIndex = 10;
            this.panelStop.Text = "Stop";
            this.panelStop.TradeStep = null;
            // 
            // panelExit
            // 
            this.panelExit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelExit.ColorAbort = System.Drawing.Color.Purple;
            this.panelExit.ColorActive = System.Drawing.Color.Yellow;
            this.panelExit.ColorComplete = System.Drawing.Color.Green;
            this.panelExit.ColorInactive = System.Drawing.SystemColors.Control;
            this.panelExit.Location = new System.Drawing.Point(606, 0);
            this.panelExit.Name = "panelExit";
            this.panelExit.Size = new System.Drawing.Size(200, 149);
            this.panelExit.TabIndex = 9;
            this.panelExit.Text = "Exit";
            this.panelExit.TradeStep = null;
            // 
            // panelEntry
            // 
            this.panelEntry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelEntry.ColorAbort = System.Drawing.Color.Purple;
            this.panelEntry.ColorActive = System.Drawing.Color.Yellow;
            this.panelEntry.ColorComplete = System.Drawing.Color.Green;
            this.panelEntry.ColorInactive = System.Drawing.SystemColors.Control;
            this.panelEntry.Location = new System.Drawing.Point(402, 0);
            this.panelEntry.Name = "panelEntry";
            this.panelEntry.Size = new System.Drawing.Size(200, 149);
            this.panelEntry.TabIndex = 8;
            this.panelEntry.Text = "Entry";
            this.panelEntry.TradeStep = null;
            // 
            // panelPreconditions
            // 
            this.panelPreconditions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPreconditions.ColorAbort = System.Drawing.Color.Purple;
            this.panelPreconditions.ColorActive = System.Drawing.Color.Yellow;
            this.panelPreconditions.ColorComplete = System.Drawing.Color.Green;
            this.panelPreconditions.ColorInactive = System.Drawing.SystemColors.Control;
            this.panelPreconditions.Location = new System.Drawing.Point(198, 0);
            this.panelPreconditions.Name = "panelPreconditions";
            this.panelPreconditions.Size = new System.Drawing.Size(200, 149);
            this.panelPreconditions.TabIndex = 7;
            this.panelPreconditions.Text = "Preconditions";
            this.panelPreconditions.TradeStep = null;
            // 
            // TradeRowControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelTradePerformance);
            this.Controls.Add(this.panelTradeOptions);
            this.Controls.Add(this.btnPerformance);
            this.Controls.Add(this.btnTradeDescription);
            this.Controls.Add(this.panelStop);
            this.Controls.Add(this.panelExit);
            this.Controls.Add(this.panelEntry);
            this.Controls.Add(this.panelPreconditions);
            this.Controls.Add(this.btnChart);
            this.Controls.Add(this.chkActive);
            this.Controls.Add(this.lblTradeName);
            this.Name = "TradeRowControl";
            this.Size = new System.Drawing.Size(1013, 149);
            this.panelTradeOptions.ResumeLayout(false);
            this.panelThrottle.ResumeLayout(false);
            this.panelThrottle.PerformLayout();
            this.panelTradePerformance.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTradeName;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Button btnChart;
        private TradeRowStepControl panelPreconditions;
        private TradeRowStepControl panelEntry;
        private TradeRowStepControl panelExit;
        private TradeRowStepControl panelStop;
        private System.Windows.Forms.Button btnTradeDescription;
        private System.Windows.Forms.Button btnPerformance;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panelTradeOptions;
        private System.Windows.Forms.Panel panelThrottle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtThrottle;
        private System.Windows.Forms.CheckBox chkAutoRestart;
        private System.Windows.Forms.Label lblTradeStatus;
        private System.Windows.Forms.Button btnStartTrade;
        private System.Windows.Forms.Panel panelTradePerformance;
        private System.Windows.Forms.Label lblProfitLoss;
        private System.Windows.Forms.Label lblTradesPerHour;
        private System.Windows.Forms.Label lblWinPercent;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblTradesEntered;
        private System.Windows.Forms.Label lblTradesExited;
        private System.Windows.Forms.Label lblTradesStopped;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
    }
}
