namespace MonkeyLightning.DataProvider.Providers.Basic
{
    partial class DPModifyChart
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
            this.uiControlChart1 = new EZAPI.Framework.Chart.UIControlChart();
            this.SuspendLayout();
            // 
            // uiControlChart1
            // 
            this.uiControlChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiControlChart1.Location = new System.Drawing.Point(0, 0);
            this.uiControlChart1.Name = "uiControlChart1";
            this.uiControlChart1.Size = new System.Drawing.Size(949, 590);
            this.uiControlChart1.TabIndex = 0;
            // 
            // DPModifyChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.uiControlChart1);
            this.Name = "DPModifyChart";
            this.Size = new System.Drawing.Size(949, 590);
            this.ResumeLayout(false);

        }

        #endregion

        private EZAPI.Framework.Chart.UIControlChart uiControlChart1;

    }
}