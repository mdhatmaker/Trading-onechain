namespace MonkeyLightning.UI.Controls
{
    partial class TradeRuleSheetControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelRules = new System.Windows.Forms.Panel();
            this.gridRules = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemRename = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.itemCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.panelRules.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRules)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelRules
            // 
            this.panelRules.Controls.Add(this.gridRules);
            this.panelRules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRules.Location = new System.Drawing.Point(0, 0);
            this.panelRules.Name = "panelRules";
            this.panelRules.Size = new System.Drawing.Size(210, 168);
            this.panelRules.TabIndex = 0;
            // 
            // gridRules
            // 
            this.gridRules.AllowUserToAddRows = false;
            this.gridRules.AllowUserToDeleteRows = false;
            this.gridRules.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.gridRules.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridRules.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridRules.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridRules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridRules.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridRules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridRules.Location = new System.Drawing.Point(0, 0);
            this.gridRules.MultiSelect = false;
            this.gridRules.Name = "gridRules";
            this.gridRules.RowHeadersVisible = false;
            this.gridRules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridRules.Size = new System.Drawing.Size(210, 168);
            this.gridRules.TabIndex = 0;
            this.gridRules.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridRules_CellEndEdit);
            this.gridRules.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridRules_CellMouseDown);
            this.gridRules.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.gridRules_RowPostPaint);
            this.gridRules.SelectionChanged += new System.EventHandler(this.gridRules_SelectionChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemRename,
            this.toolStripMenuItem1,
            this.itemCancel});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(141, 54);
            // 
            // itemRename
            // 
            this.itemRename.Name = "itemRename";
            this.itemRename.Size = new System.Drawing.Size(140, 22);
            this.itemRename.Text = "Rename rule";
            this.itemRename.Click += new System.EventHandler(this.itemRename_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(137, 6);
            // 
            // itemCancel
            // 
            this.itemCancel.Name = "itemCancel";
            this.itemCancel.Size = new System.Drawing.Size(140, 22);
            this.itemCancel.Text = "Cancel";
            this.itemCancel.Click += new System.EventHandler(this.itemCancel_Click);
            // 
            // TradeRuleSheetControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelRules);
            this.Name = "TradeRuleSheetControl";
            this.Size = new System.Drawing.Size(210, 168);
            this.panelRules.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridRules)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelRules;
        private System.Windows.Forms.DataGridView gridRules;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem itemRename;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem itemCancel;
    }
}
