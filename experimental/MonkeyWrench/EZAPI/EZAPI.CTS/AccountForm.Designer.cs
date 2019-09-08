namespace EZAPI
{
    partial class AccountForm
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
            this.gridAccounts = new System.Windows.Forms.DataGridView();
            this.MarketColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.OrderFeedColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountTypeColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridAccounts)).BeginInit();
            this.SuspendLayout();
            // 
            // gridAccounts
            // 
            this.gridAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridAccounts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MarketColumn,
            this.OrderFeedColumn,
            this.AccountNameColumn,
            this.AccountTypeColumn});
            this.gridAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridAccounts.Location = new System.Drawing.Point(0, 0);
            this.gridAccounts.Name = "gridAccounts";
            this.gridAccounts.Size = new System.Drawing.Size(679, 239);
            this.gridAccounts.TabIndex = 0;
            // 
            // MarketColumn
            // 
            this.MarketColumn.HeaderText = "Market";
            this.MarketColumn.Items.AddRange(new object[] {
            "<Default>",
            "CBOT",
            "CME",
            "ICE",
            "LME"});
            this.MarketColumn.Name = "MarketColumn";
            this.MarketColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MarketColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // OrderFeedColumn
            // 
            this.OrderFeedColumn.HeaderText = "Order Feed";
            this.OrderFeedColumn.Name = "OrderFeedColumn";
            // 
            // AccountNameColumn
            // 
            this.AccountNameColumn.HeaderText = "Account Name";
            this.AccountNameColumn.Name = "AccountNameColumn";
            // 
            // AccountTypeColumn
            // 
            this.AccountTypeColumn.HeaderText = "Account Type";
            this.AccountTypeColumn.Items.AddRange(new object[] {
            "Agent1",
            "Agent2",
            "Agent3",
            "Agent4",
            "Agent5",
            "Agent6",
            "Agent7",
            "Agent8",
            "Agent9",
            "MarketMaker1",
            "MarketMaker2",
            "MarketMaker3",
            "None",
            "Principal1",
            "Principal2",
            "Principal3",
            "Unallocated1",
            "Unallocated2"});
            this.AccountTypeColumn.Name = "AccountTypeColumn";
            // 
            // AccountForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 239);
            this.Controls.Add(this.gridAccounts);
            this.Name = "AccountForm";
            this.Text = "Accounts";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AccountForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.gridAccounts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridAccounts;
        private System.Windows.Forms.DataGridViewComboBoxColumn MarketColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderFeedColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountNameColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn AccountTypeColumn;
    }
}