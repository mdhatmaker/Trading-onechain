namespace MonkeyLightning
{
    partial class RuleSheetControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelRules = new System.Windows.Forms.Panel();
            this.gridRules = new System.Windows.Forms.DataGridView();
            this.ActivateColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.RuleNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelRules.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRules)).BeginInit();
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
            this.gridRules.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ActivateColumn,
            this.RuleNameColumn});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridRules.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridRules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridRules.Location = new System.Drawing.Point(0, 0);
            this.gridRules.Name = "gridRules";
            this.gridRules.RowHeadersVisible = false;
            this.gridRules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridRules.Size = new System.Drawing.Size(210, 168);
            this.gridRules.TabIndex = 0;
            // 
            // ActivateColumn
            // 
            this.ActivateColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ActivateColumn.FillWeight = 25.38071F;
            this.ActivateColumn.HeaderText = "active";
            this.ActivateColumn.MinimumWidth = 25;
            this.ActivateColumn.Name = "ActivateColumn";
            this.ActivateColumn.Width = 49;
            // 
            // RuleNameColumn
            // 
            this.RuleNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.RuleNameColumn.FillWeight = 74.61929F;
            this.RuleNameColumn.HeaderText = "Rule";
            this.RuleNameColumn.Name = "RuleNameColumn";
            this.RuleNameColumn.ReadOnly = true;
            // 
            // RuleSheetControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelRules);
            this.Name = "RuleSheetControl";
            this.Size = new System.Drawing.Size(210, 168);
            this.panelRules.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridRules)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelRules;
        private System.Windows.Forms.DataGridView gridRules;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ActivateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RuleNameColumn;
    }
}
