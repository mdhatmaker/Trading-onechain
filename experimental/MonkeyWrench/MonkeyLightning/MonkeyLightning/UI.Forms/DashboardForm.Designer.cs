namespace MonkeyLightning.UI.Forms
{
    partial class DashboardForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DashboardForm));
            this.splitContainerTrades = new System.Windows.Forms.SplitContainer();
            this.panelTrades = new System.Windows.Forms.TableLayoutPanel();
            this.btnHideNotifications = new System.Windows.Forms.Button();
            this.lblNotifications = new System.Windows.Forms.Label();
            this.gridNotifications = new System.Windows.Forms.DataGridView();
            this.ColumnIcon = new System.Windows.Forms.DataGridViewImageColumn();
            this.ColumnTradeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTimestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelHeaderCenter = new System.Windows.Forms.Panel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnCharts = new System.Windows.Forms.Button();
            this.btnNewTrade = new System.Windows.Forms.Button();
            this.btnStopAllTrades = new System.Windows.Forms.Button();
            this.btnGoldenRules = new System.Windows.Forms.Button();
            this.picSiren = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTrades)).BeginInit();
            this.splitContainerTrades.Panel1.SuspendLayout();
            this.splitContainerTrades.Panel2.SuspendLayout();
            this.splitContainerTrades.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridNotifications)).BeginInit();
            this.panelHeaderCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSiren)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerTrades
            // 
            this.splitContainerTrades.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerTrades.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.splitContainerTrades.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerTrades.Location = new System.Drawing.Point(1, 75);
            this.splitContainerTrades.Name = "splitContainerTrades";
            this.splitContainerTrades.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerTrades.Panel1
            // 
            this.splitContainerTrades.Panel1.Controls.Add(this.panelTrades);
            // 
            // splitContainerTrades.Panel2
            // 
            this.splitContainerTrades.Panel2.Controls.Add(this.btnHideNotifications);
            this.splitContainerTrades.Panel2.Controls.Add(this.lblNotifications);
            this.splitContainerTrades.Panel2.Controls.Add(this.gridNotifications);
            this.splitContainerTrades.Size = new System.Drawing.Size(1034, 458);
            this.splitContainerTrades.SplitterDistance = 315;
            this.splitContainerTrades.TabIndex = 3;
            // 
            // panelTrades
            // 
            this.panelTrades.AutoScroll = true;
            this.panelTrades.ColumnCount = 1;
            this.panelTrades.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelTrades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTrades.Location = new System.Drawing.Point(0, 0);
            this.panelTrades.Name = "panelTrades";
            this.panelTrades.RowCount = 1;
            this.panelTrades.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelTrades.Size = new System.Drawing.Size(1032, 313);
            this.panelTrades.TabIndex = 5;
            // 
            // btnHideNotifications
            // 
            this.btnHideNotifications.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHideNotifications.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHideNotifications.Image = global::MonkeyLightning.Properties.Resources.arrowDown24x24;
            this.btnHideNotifications.Location = new System.Drawing.Point(1005, 0);
            this.btnHideNotifications.Name = "btnHideNotifications";
            this.btnHideNotifications.Size = new System.Drawing.Size(27, 28);
            this.btnHideNotifications.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btnHideNotifications, "Hide Notifications area");
            this.btnHideNotifications.UseVisualStyleBackColor = true;
            this.btnHideNotifications.Click += new System.EventHandler(this.btnHideNotifications_Click);
            // 
            // lblNotifications
            // 
            this.lblNotifications.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNotifications.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotifications.Location = new System.Drawing.Point(0, 0);
            this.lblNotifications.Name = "lblNotifications";
            this.lblNotifications.Size = new System.Drawing.Size(142, 27);
            this.lblNotifications.TabIndex = 1;
            this.lblNotifications.Text = "Notifications";
            this.lblNotifications.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // gridNotifications
            // 
            this.gridNotifications.AllowUserToAddRows = false;
            this.gridNotifications.AllowUserToDeleteRows = false;
            this.gridNotifications.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            this.gridNotifications.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridNotifications.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridNotifications.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gridNotifications.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridNotifications.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridNotifications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridNotifications.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnIcon,
            this.ColumnTradeName,
            this.ColumnTimestamp,
            this.ColumnDescription});
            this.gridNotifications.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridNotifications.Location = new System.Drawing.Point(0, 28);
            this.gridNotifications.MultiSelect = false;
            this.gridNotifications.Name = "gridNotifications";
            this.gridNotifications.ReadOnly = true;
            this.gridNotifications.RowHeadersVisible = false;
            this.gridNotifications.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gridNotifications.RowTemplate.Height = 24;
            this.gridNotifications.RowTemplate.ReadOnly = true;
            this.gridNotifications.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gridNotifications.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridNotifications.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridNotifications.ShowEditingIcon = false;
            this.gridNotifications.Size = new System.Drawing.Size(1032, 114);
            this.gridNotifications.TabIndex = 0;
            // 
            // ColumnIcon
            // 
            this.ColumnIcon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnIcon.FillWeight = 6F;
            this.ColumnIcon.HeaderText = "Icon";
            this.ColumnIcon.Name = "ColumnIcon";
            this.ColumnIcon.ReadOnly = true;
            // 
            // ColumnTradeName
            // 
            this.ColumnTradeName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnTradeName.FillWeight = 14F;
            this.ColumnTradeName.HeaderText = "Trade Name";
            this.ColumnTradeName.Name = "ColumnTradeName";
            this.ColumnTradeName.ReadOnly = true;
            // 
            // ColumnTimestamp
            // 
            this.ColumnTimestamp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnTimestamp.FillWeight = 15F;
            this.ColumnTimestamp.HeaderText = "Date/Time";
            this.ColumnTimestamp.Name = "ColumnTimestamp";
            this.ColumnTimestamp.ReadOnly = true;
            // 
            // ColumnDescription
            // 
            this.ColumnDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnDescription.FillWeight = 65F;
            this.ColumnDescription.HeaderText = "Description";
            this.ColumnDescription.Name = "ColumnDescription";
            this.ColumnDescription.ReadOnly = true;
            // 
            // panelHeaderCenter
            // 
            this.panelHeaderCenter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelHeaderCenter.Controls.Add(this.picLogo);
            this.panelHeaderCenter.Location = new System.Drawing.Point(275, 3);
            this.panelHeaderCenter.Name = "panelHeaderCenter";
            this.panelHeaderCenter.Size = new System.Drawing.Size(436, 66);
            this.panelHeaderCenter.TabIndex = 4;
            this.panelHeaderCenter.Click += new System.EventHandler(this.panelHeaderCenter_Click);
            // 
            // picLogo
            // 
            this.picLogo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.picLogo.Image = global::MonkeyLightning.Properties.Resources.MonkeyLightning;
            this.picLogo.Location = new System.Drawing.Point(191, 0);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(51, 65);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picLogo.TabIndex = 7;
            this.picLogo.TabStop = false;
            this.picLogo.Visible = false;
            this.picLogo.Click += new System.EventHandler(this.picLogo_Click);
            // 
            // btnCharts
            // 
            this.btnCharts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCharts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCharts.Font = new System.Drawing.Font("Comic Sans MS", 16F);
            this.btnCharts.Image = global::MonkeyLightning.Properties.Resources.penonchartSmallFaded;
            this.btnCharts.Location = new System.Drawing.Point(783, 11);
            this.btnCharts.Name = "btnCharts";
            this.btnCharts.Size = new System.Drawing.Size(118, 57);
            this.btnCharts.TabIndex = 7;
            this.btnCharts.Text = "Charts";
            this.toolTip1.SetToolTip(this.btnCharts, "View charts...");
            this.btnCharts.UseVisualStyleBackColor = true;
            this.btnCharts.Click += new System.EventHandler(this.btnCharts_Click);
            // 
            // btnNewTrade
            // 
            this.btnNewTrade.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewTrade.BackColor = System.Drawing.Color.White;
            this.btnNewTrade.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNewTrade.Font = new System.Drawing.Font("Comic Sans MS", 14F);
            this.btnNewTrade.Image = global::MonkeyLightning.Properties.Resources.newTradeIconFaded;
            this.btnNewTrade.Location = new System.Drawing.Point(907, 11);
            this.btnNewTrade.Name = "btnNewTrade";
            this.btnNewTrade.Size = new System.Drawing.Size(118, 57);
            this.btnNewTrade.TabIndex = 5;
            this.btnNewTrade.Text = "New Trade";
            this.toolTip1.SetToolTip(this.btnNewTrade, "Create a new Trade in TradeBuilder window...");
            this.btnNewTrade.UseVisualStyleBackColor = false;
            this.btnNewTrade.Click += new System.EventHandler(this.btnNewTrade_Click);
            // 
            // btnStopAllTrades
            // 
            this.btnStopAllTrades.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStopAllTrades.Font = new System.Drawing.Font("Comic Sans MS", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStopAllTrades.Image = global::MonkeyLightning.Properties.Resources.emergencyStopFaded;
            this.btnStopAllTrades.Location = new System.Drawing.Point(12, 11);
            this.btnStopAllTrades.Name = "btnStopAllTrades";
            this.btnStopAllTrades.Size = new System.Drawing.Size(118, 58);
            this.btnStopAllTrades.TabIndex = 2;
            this.btnStopAllTrades.Text = "Emergency Stop";
            this.toolTip1.SetToolTip(this.btnStopAllTrades, "Stop all trades immediately");
            this.btnStopAllTrades.UseVisualStyleBackColor = true;
            this.btnStopAllTrades.Click += new System.EventHandler(this.btnStopAllTrades_Click);
            // 
            // btnGoldenRules
            // 
            this.btnGoldenRules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGoldenRules.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGoldenRules.Font = new System.Drawing.Font("Comic Sans MS", 12F);
            this.btnGoldenRules.Image = global::MonkeyLightning.Properties.Resources.medalWashout;
            this.btnGoldenRules.Location = new System.Drawing.Point(659, 11);
            this.btnGoldenRules.Name = "btnGoldenRules";
            this.btnGoldenRules.Size = new System.Drawing.Size(118, 57);
            this.btnGoldenRules.TabIndex = 0;
            this.btnGoldenRules.Text = "Golden Rules";
            this.toolTip1.SetToolTip(this.btnGoldenRules, "View \"Golden Rules\" of trading...");
            this.btnGoldenRules.UseVisualStyleBackColor = true;
            this.btnGoldenRules.Visible = false;
            this.btnGoldenRules.Click += new System.EventHandler(this.btnGoldenRules_Click);
            // 
            // picSiren
            // 
            this.picSiren.Image = global::MonkeyLightning.Properties.Resources.SirenBlink;
            this.picSiren.Location = new System.Drawing.Point(152, 1);
            this.picSiren.Name = "picSiren";
            this.picSiren.Size = new System.Drawing.Size(50, 74);
            this.picSiren.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picSiren.TabIndex = 6;
            this.picSiren.TabStop = false;
            this.picSiren.Visible = false;
            // 
            // DashboardForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1037, 533);
            this.Controls.Add(this.btnCharts);
            this.Controls.Add(this.picSiren);
            this.Controls.Add(this.btnNewTrade);
            this.Controls.Add(this.panelHeaderCenter);
            this.Controls.Add(this.splitContainerTrades);
            this.Controls.Add(this.btnStopAllTrades);
            this.Controls.Add(this.btnGoldenRules);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DashboardForm";
            this.Text = "Monkey Dashboard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DashboardForm_FormClosing);
            this.Load += new System.EventHandler(this.DashboardForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.DashboardForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.DashboardForm_DragEnter);
            this.Resize += new System.EventHandler(this.DashboardForm_Resize);
            this.splitContainerTrades.Panel1.ResumeLayout(false);
            this.splitContainerTrades.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTrades)).EndInit();
            this.splitContainerTrades.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridNotifications)).EndInit();
            this.panelHeaderCenter.ResumeLayout(false);
            this.panelHeaderCenter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSiren)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGoldenRules;
        private System.Windows.Forms.Button btnStopAllTrades;
        private System.Windows.Forms.SplitContainer splitContainerTrades;
        private System.Windows.Forms.DataGridView gridNotifications;
        private System.Windows.Forms.TableLayoutPanel panelTrades;
        private System.Windows.Forms.Label lblNotifications;
        private System.Windows.Forms.Button btnHideNotifications;
        private System.Windows.Forms.DataGridViewImageColumn ColumnIcon;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTradeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTimestamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescription;
        private System.Windows.Forms.Panel panelHeaderCenter;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnNewTrade;
        private System.Windows.Forms.PictureBox picSiren;
        private System.Windows.Forms.Button btnCharts;
    }
}