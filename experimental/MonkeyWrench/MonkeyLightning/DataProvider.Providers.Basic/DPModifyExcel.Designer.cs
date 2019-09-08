namespace MonkeyLightning.DataProvider.Providers.Basic
{
    partial class DPModifyExcel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DPModifyExcel));
            this.lblSelected = new System.Windows.Forms.Label();
            this.panelExcel = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnOpenExcelDocument = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSelected
            // 
            this.lblSelected.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelected.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblSelected.Location = new System.Drawing.Point(0, 266);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(372, 27);
            this.lblSelected.TabIndex = 108;
            this.lblSelected.Text = "select Excel cell to use";
            this.lblSelected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelExcel
            // 
            this.panelExcel.Location = new System.Drawing.Point(4, 42);
            this.panelExcel.Name = "panelExcel";
            this.panelExcel.Size = new System.Drawing.Size(365, 221);
            this.panelExcel.TabIndex = 109;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpenExcelDocument});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(372, 39);
            this.toolStrip1.TabIndex = 110;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnOpenExcelDocument
            // 
            this.btnOpenExcelDocument.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenExcelDocument.Image")));
            this.btnOpenExcelDocument.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenExcelDocument.Name = "btnOpenExcelDocument";
            this.btnOpenExcelDocument.Size = new System.Drawing.Size(168, 36);
            this.btnOpenExcelDocument.Text = "Open Excel document...";
            this.btnOpenExcelDocument.Click += new System.EventHandler(this.btnOpenExcelDocument_Click);
            // 
            // DPModifyExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panelExcel);
            this.Controls.Add(this.lblSelected);
            this.Name = "DPModifyExcel";
            this.Size = new System.Drawing.Size(372, 293);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lblSelected;
        private System.Windows.Forms.Panel panelExcel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnOpenExcelDocument;
    }
}
