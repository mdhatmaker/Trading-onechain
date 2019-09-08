namespace EZAPI
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslblInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblFillCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblProfit = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.fillGrid = new System.Windows.Forms.DataGridView();
            this.TimeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BuySellColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QuantityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PriceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filteredGrid = new System.Windows.Forms.DataGridView();
            this.TimeColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BuySellColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QuantityColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PriceColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnMessaging = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsitemTextMessageAlert = new System.Windows.Forms.ToolStripMenuItem();
            this.tsitemiPhoneAlert = new System.Windows.Forms.ToolStripMenuItem();
            this.tsitemAndroidAlert = new System.Windows.Forms.ToolStripMenuItem();
            this.tsitemTextMessageBackup = new System.Windows.Forms.ToolStripMenuItem();
            this.tsitemSendTestMessage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsitemMessagingSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnSounds = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsitemEnableSounds = new System.Windows.Forms.ToolStripMenuItem();
            this.tsitemSoundSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnHedger = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsitemEnableHedging = new System.Windows.Forms.ToolStripMenuItem();
            this.tsitemHedgerSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnSettings = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsitemAutoScroll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsitemGeneralSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnHelp = new System.Windows.Forms.ToolStripButton();
            this.tsbtnInfo = new System.Windows.Forms.ToolStripButton();
            this.tscomboFFT = new System.Windows.Forms.ToolStripComboBox();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fillGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filteredGrid)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslblInfo,
            this.tslblFillCount,
            this.tslblProfit});
            this.statusStrip1.Location = new System.Drawing.Point(0, 321);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(785, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tslblInfo
            // 
            this.tslblInfo.Name = "tslblInfo";
            this.tslblInfo.Size = new System.Drawing.Size(562, 17);
            this.tslblInfo.Spring = true;
            this.tslblInfo.Text = "Go to Settings->General Settings to enter your TT username/password. (App restart" +
                " required.)";
            this.tslblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tslblFillCount
            // 
            this.tslblFillCount.AutoSize = false;
            this.tslblFillCount.Name = "tslblFillCount";
            this.tslblFillCount.Size = new System.Drawing.Size(88, 17);
            this.tslblFillCount.Text = "Fill count: 0";
            // 
            // tslblProfit
            // 
            this.tslblProfit.AutoSize = false;
            this.tslblProfit.Name = "tslblProfit";
            this.tslblProfit.Size = new System.Drawing.Size(89, 17);
            this.tslblProfit.Text = "Profit: $0.00";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(9, 89);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.fillGrid);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.filteredGrid);
            this.splitContainer1.Size = new System.Drawing.Size(764, 229);
            this.splitContainer1.SplitterDistance = 380;
            this.splitContainer1.TabIndex = 5;
            // 
            // fillGrid
            // 
            this.fillGrid.AllowUserToAddRows = false;
            this.fillGrid.AllowUserToDeleteRows = false;
            this.fillGrid.AllowUserToResizeRows = false;
            this.fillGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.fillGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fillGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TimeColumn,
            this.IDColumn,
            this.BuySellColumn,
            this.QuantityColumn,
            this.ProductColumn,
            this.PriceColumn});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.fillGrid.DefaultCellStyle = dataGridViewCellStyle9;
            this.fillGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fillGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.fillGrid.Location = new System.Drawing.Point(0, 0);
            this.fillGrid.MultiSelect = false;
            this.fillGrid.Name = "fillGrid";
            this.fillGrid.ReadOnly = true;
            this.fillGrid.RowHeadersVisible = false;
            this.fillGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.fillGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.fillGrid.ShowCellErrors = false;
            this.fillGrid.Size = new System.Drawing.Size(380, 229);
            this.fillGrid.TabIndex = 0;
            this.fillGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.fillGrid_CellClick);
            // 
            // TimeColumn
            // 
            this.TimeColumn.HeaderText = "Time";
            this.TimeColumn.Name = "TimeColumn";
            this.TimeColumn.ReadOnly = true;
            // 
            // IDColumn
            // 
            this.IDColumn.HeaderText = "IDs";
            this.IDColumn.Name = "IDColumn";
            this.IDColumn.ReadOnly = true;
            // 
            // BuySellColumn
            // 
            this.BuySellColumn.FillWeight = 50F;
            this.BuySellColumn.HeaderText = "Side";
            this.BuySellColumn.Name = "BuySellColumn";
            this.BuySellColumn.ReadOnly = true;
            // 
            // QuantityColumn
            // 
            this.QuantityColumn.FillWeight = 50F;
            this.QuantityColumn.HeaderText = "Qty";
            this.QuantityColumn.Name = "QuantityColumn";
            this.QuantityColumn.ReadOnly = true;
            // 
            // ProductColumn
            // 
            this.ProductColumn.FillWeight = 150F;
            this.ProductColumn.HeaderText = "Product";
            this.ProductColumn.Name = "ProductColumn";
            this.ProductColumn.ReadOnly = true;
            // 
            // PriceColumn
            // 
            this.PriceColumn.FillWeight = 74F;
            this.PriceColumn.HeaderText = "Price";
            this.PriceColumn.Name = "PriceColumn";
            this.PriceColumn.ReadOnly = true;
            // 
            // filteredGrid
            // 
            this.filteredGrid.AllowUserToAddRows = false;
            this.filteredGrid.AllowUserToDeleteRows = false;
            this.filteredGrid.AllowUserToResizeRows = false;
            this.filteredGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.filteredGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.filteredGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TimeColumn2,
            this.IDColumn2,
            this.BuySellColumn2,
            this.QuantityColumn2,
            this.ProductColumn2,
            this.PriceColumn2});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.filteredGrid.DefaultCellStyle = dataGridViewCellStyle10;
            this.filteredGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filteredGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.filteredGrid.Location = new System.Drawing.Point(0, 0);
            this.filteredGrid.Name = "filteredGrid";
            this.filteredGrid.ReadOnly = true;
            this.filteredGrid.RowHeadersVisible = false;
            this.filteredGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.filteredGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.filteredGrid.ShowCellErrors = false;
            this.filteredGrid.Size = new System.Drawing.Size(380, 229);
            this.filteredGrid.TabIndex = 1;
            // 
            // TimeColumn2
            // 
            this.TimeColumn2.HeaderText = "Time";
            this.TimeColumn2.Name = "TimeColumn2";
            this.TimeColumn2.ReadOnly = true;
            // 
            // IDColumn2
            // 
            this.IDColumn2.HeaderText = "IDs";
            this.IDColumn2.Name = "IDColumn2";
            this.IDColumn2.ReadOnly = true;
            // 
            // BuySellColumn2
            // 
            this.BuySellColumn2.FillWeight = 50F;
            this.BuySellColumn2.HeaderText = "Side";
            this.BuySellColumn2.Name = "BuySellColumn2";
            this.BuySellColumn2.ReadOnly = true;
            // 
            // QuantityColumn2
            // 
            this.QuantityColumn2.FillWeight = 50F;
            this.QuantityColumn2.HeaderText = "Qty";
            this.QuantityColumn2.Name = "QuantityColumn2";
            this.QuantityColumn2.ReadOnly = true;
            // 
            // ProductColumn2
            // 
            this.ProductColumn2.FillWeight = 150F;
            this.ProductColumn2.HeaderText = "Product";
            this.ProductColumn2.Name = "ProductColumn2";
            this.ProductColumn2.ReadOnly = true;
            // 
            // PriceColumn2
            // 
            this.PriceColumn2.FillWeight = 74F;
            this.PriceColumn2.HeaderText = "Price";
            this.PriceColumn2.Name = "PriceColumn2";
            this.PriceColumn2.ReadOnly = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnMessaging,
            this.tsbtnSounds,
            this.tsbtnHedger,
            this.tsbtnSettings,
            this.toolStripSeparator1,
            this.tsbtnHelp,
            this.tsbtnInfo,
            this.tscomboFFT});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(785, 86);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtnMessaging
            // 
            this.tsbtnMessaging.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsitemTextMessageAlert,
            this.tsitemiPhoneAlert,
            this.tsitemAndroidAlert,
            this.tsitemTextMessageBackup,
            this.tsitemSendTestMessage,
            this.tsitemMessagingSettings});
            this.tsbtnMessaging.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnMessaging.Image")));
            this.tsbtnMessaging.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnMessaging.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnMessaging.Name = "tsbtnMessaging";
            this.tsbtnMessaging.Size = new System.Drawing.Size(77, 83);
            this.tsbtnMessaging.Text = "Messaging";
            this.tsbtnMessaging.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsitemTextMessageAlert
            // 
            this.tsitemTextMessageAlert.CheckOnClick = true;
            this.tsitemTextMessageAlert.Name = "tsitemTextMessageAlert";
            this.tsitemTextMessageAlert.Size = new System.Drawing.Size(187, 22);
            this.tsitemTextMessageAlert.Text = "Text Message Alert";
            this.tsitemTextMessageAlert.Click += new System.EventHandler(this.tsitemTextMessageAlert_Click);
            // 
            // tsitemiPhoneAlert
            // 
            this.tsitemiPhoneAlert.CheckOnClick = true;
            this.tsitemiPhoneAlert.Name = "tsitemiPhoneAlert";
            this.tsitemiPhoneAlert.Size = new System.Drawing.Size(187, 22);
            this.tsitemiPhoneAlert.Text = "iPhone Prowl Alert";
            this.tsitemiPhoneAlert.Click += new System.EventHandler(this.tsitemiPhoneAlert_Click);
            // 
            // tsitemAndroidAlert
            // 
            this.tsitemAndroidAlert.CheckOnClick = true;
            this.tsitemAndroidAlert.Name = "tsitemAndroidAlert";
            this.tsitemAndroidAlert.Size = new System.Drawing.Size(187, 22);
            this.tsitemAndroidAlert.Text = "Android NMA Alert";
            this.tsitemAndroidAlert.Click += new System.EventHandler(this.tsitemAndroidAlert_Click);
            // 
            // tsitemTextMessageBackup
            // 
            this.tsitemTextMessageBackup.CheckOnClick = true;
            this.tsitemTextMessageBackup.Enabled = false;
            this.tsitemTextMessageBackup.Name = "tsitemTextMessageBackup";
            this.tsitemTextMessageBackup.Size = new System.Drawing.Size(187, 22);
            this.tsitemTextMessageBackup.Text = "Text Message Backup";
            // 
            // tsitemSendTestMessage
            // 
            this.tsitemSendTestMessage.Name = "tsitemSendTestMessage";
            this.tsitemSendTestMessage.Size = new System.Drawing.Size(187, 22);
            this.tsitemSendTestMessage.Text = "Send Test Message";
            this.tsitemSendTestMessage.Click += new System.EventHandler(this.tsitemSendTestMessage_Click);
            // 
            // tsitemMessagingSettings
            // 
            this.tsitemMessagingSettings.Name = "tsitemMessagingSettings";
            this.tsitemMessagingSettings.Size = new System.Drawing.Size(187, 22);
            this.tsitemMessagingSettings.Text = "Messaging Settings...";
            this.tsitemMessagingSettings.Click += new System.EventHandler(this.tsitemMessagingSettings_Click);
            // 
            // tsbtnSounds
            // 
            this.tsbtnSounds.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsitemEnableSounds,
            this.tsitemSoundSettings});
            this.tsbtnSounds.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSounds.Image")));
            this.tsbtnSounds.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnSounds.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSounds.Name = "tsbtnSounds";
            this.tsbtnSounds.Size = new System.Drawing.Size(77, 83);
            this.tsbtnSounds.Text = "Sounds";
            this.tsbtnSounds.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsitemEnableSounds
            // 
            this.tsitemEnableSounds.CheckOnClick = true;
            this.tsitemEnableSounds.Name = "tsitemEnableSounds";
            this.tsitemEnableSounds.Size = new System.Drawing.Size(153, 22);
            this.tsitemEnableSounds.Text = "Enable Sounds";
            this.tsitemEnableSounds.Click += new System.EventHandler(this.tsitemEnableSounds_Click);
            // 
            // tsitemSoundSettings
            // 
            this.tsitemSoundSettings.Name = "tsitemSoundSettings";
            this.tsitemSoundSettings.Size = new System.Drawing.Size(162, 22);
            this.tsitemSoundSettings.Text = "Sound Settings...";
            this.tsitemSoundSettings.Click += new System.EventHandler(this.soundSettingsToolStripMenuItem_Click);
            // 
            // tsbtnHedger
            // 
            this.tsbtnHedger.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsitemEnableHedging,
            this.tsitemHedgerSettings});
            this.tsbtnHedger.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnHedger.Image")));
            this.tsbtnHedger.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnHedger.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnHedger.Name = "tsbtnHedger";
            this.tsbtnHedger.Size = new System.Drawing.Size(77, 83);
            this.tsbtnHedger.Text = "Hedger";
            this.tsbtnHedger.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsitemEnableHedging
            // 
            this.tsitemEnableHedging.CheckOnClick = true;
            this.tsitemEnableHedging.Name = "tsitemEnableHedging";
            this.tsitemEnableHedging.Size = new System.Drawing.Size(167, 22);
            this.tsitemEnableHedging.Text = "Enable Hedging";
            // 
            // tsitemHedgerSettings
            // 
            this.tsitemHedgerSettings.Name = "tsitemHedgerSettings";
            this.tsitemHedgerSettings.Size = new System.Drawing.Size(167, 22);
            this.tsitemHedgerSettings.Text = "Hedger Settings...";
            this.tsitemHedgerSettings.Click += new System.EventHandler(this.tsitemHedgerSettings_Click);
            // 
            // tsbtnSettings
            // 
            this.tsbtnSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsitemAutoScroll,
            this.tsitemGeneralSettings});
            this.tsbtnSettings.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSettings.Image")));
            this.tsbtnSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSettings.Name = "tsbtnSettings";
            this.tsbtnSettings.Size = new System.Drawing.Size(77, 83);
            this.tsbtnSettings.Text = "Settings";
            this.tsbtnSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnSettings.Click += new System.EventHandler(this.tsitemAutoScroll_Click);
            // 
            // tsitemAutoScroll
            // 
            this.tsitemAutoScroll.Checked = true;
            this.tsitemAutoScroll.CheckOnClick = true;
            this.tsitemAutoScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsitemAutoScroll.Name = "tsitemAutoScroll";
            this.tsitemAutoScroll.Size = new System.Drawing.Size(168, 22);
            this.tsitemAutoScroll.Text = "Autoscroll";
            // 
            // tsitemGeneralSettings
            // 
            this.tsitemGeneralSettings.Name = "tsitemGeneralSettings";
            this.tsitemGeneralSettings.Size = new System.Drawing.Size(168, 22);
            this.tsitemGeneralSettings.Text = "General Settings...";
            this.tsitemGeneralSettings.Click += new System.EventHandler(this.generalSettingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 86);
            // 
            // tsbtnHelp
            // 
            this.tsbtnHelp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnHelp.Image")));
            this.tsbtnHelp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnHelp.Name = "tsbtnHelp";
            this.tsbtnHelp.Size = new System.Drawing.Size(68, 83);
            this.tsbtnHelp.Text = "Help";
            this.tsbtnHelp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnHelp.Click += new System.EventHandler(this.tsbtnHelp_Click);
            // 
            // tsbtnInfo
            // 
            this.tsbtnInfo.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnInfo.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnInfo.Image")));
            this.tsbtnInfo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnInfo.Name = "tsbtnInfo";
            this.tsbtnInfo.Size = new System.Drawing.Size(68, 83);
            this.tsbtnInfo.Text = "Info";
            this.tsbtnInfo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnInfo.Click += new System.EventHandler(this.tsbtnInfo_Click);
            // 
            // tscomboFFT
            // 
            this.tscomboFFT.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tscomboFFT.BackColor = System.Drawing.SystemColors.HotTrack;
            this.tscomboFFT.Name = "tscomboFFT";
            this.tscomboFFT.Size = new System.Drawing.Size(121, 86);
            this.tscomboFFT.SelectedIndexChanged += new System.EventHandler(this.tscomboFFT_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 343);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Fills Deluxe";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fillGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filteredGrid)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tslblInfo;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView fillGrid;
        private System.Windows.Forms.ToolStripStatusLabel tslblFillCount;
        private System.Windows.Forms.ToolStripStatusLabel tslblProfit;
        private System.Windows.Forms.DataGridView filteredGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuySellColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn QuantityColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PriceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuySellColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn QuantityColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn PriceColumn2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton tsbtnMessaging;
        private System.Windows.Forms.ToolStripMenuItem tsitemTextMessageAlert;
        private System.Windows.Forms.ToolStripMenuItem tsitemiPhoneAlert;
        private System.Windows.Forms.ToolStripMenuItem tsitemAndroidAlert;
        private System.Windows.Forms.ToolStripMenuItem tsitemTextMessageBackup;
        private System.Windows.Forms.ToolStripMenuItem tsitemSendTestMessage;
        private System.Windows.Forms.ToolStripMenuItem tsitemMessagingSettings;
        private System.Windows.Forms.ToolStripDropDownButton tsbtnSounds;
        private System.Windows.Forms.ToolStripMenuItem tsitemEnableSounds;
        private System.Windows.Forms.ToolStripMenuItem tsitemSoundSettings;
        private System.Windows.Forms.ToolStripDropDownButton tsbtnHedger;
        private System.Windows.Forms.ToolStripMenuItem tsitemEnableHedging;
        private System.Windows.Forms.ToolStripMenuItem tsitemHedgerSettings;
        private System.Windows.Forms.ToolStripDropDownButton tsbtnSettings;
        private System.Windows.Forms.ToolStripMenuItem tsitemAutoScroll;
        private System.Windows.Forms.ToolStripMenuItem tsitemGeneralSettings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbtnHelp;
        private System.Windows.Forms.ToolStripButton tsbtnInfo;
        private System.Windows.Forms.ToolStripComboBox tscomboFFT;
    }
}

