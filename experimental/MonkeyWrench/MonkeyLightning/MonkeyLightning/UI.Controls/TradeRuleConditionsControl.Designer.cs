namespace MonkeyLightning.UI.Controls
{
    partial class TradeRuleConditionsControl
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
            this.gridConditions = new System.Windows.Forms.DataGridView();
            this.panelRules.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridConditions)).BeginInit();
            this.SuspendLayout();
            // 
            // panelRules
            // 
            this.panelRules.Controls.Add(this.gridConditions);
            this.panelRules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRules.Location = new System.Drawing.Point(0, 0);
            this.panelRules.Name = "panelRules";
            this.panelRules.Size = new System.Drawing.Size(210, 168);
            this.panelRules.TabIndex = 0;
            // 
            // gridConditions
            // 
            this.gridConditions.AllowUserToAddRows = false;
            this.gridConditions.AllowUserToDeleteRows = false;
            this.gridConditions.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.gridConditions.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridConditions.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridConditions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridConditions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridConditions.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridConditions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridConditions.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridConditions.Location = new System.Drawing.Point(0, 0);
            this.gridConditions.MultiSelect = false;
            this.gridConditions.Name = "gridConditions";
            this.gridConditions.RowHeadersVisible = false;
            this.gridConditions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridConditions.Size = new System.Drawing.Size(210, 168);
            this.gridConditions.TabIndex = 0;
            this.gridConditions.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gridConditions_CellFormatting);
            this.gridConditions.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.gridConditions_RowPostPaint);
            this.gridConditions.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.gridConditions_RowPrePaint);
            this.gridConditions.SelectionChanged += new System.EventHandler(this.gridConditions_SelectionChanged);
            // 
            // TradeRuleConditionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelRules);
            this.Name = "TradeRuleConditionsControl";
            this.Size = new System.Drawing.Size(210, 168);
            this.panelRules.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridConditions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelRules;
        private System.Windows.Forms.DataGridView gridConditions;
    }
}
