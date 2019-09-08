namespace MonkeyLightning.DataProvider.Providers.Basic
{
    partial class TestTimeRangeForm
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
            this.dpTimeRange1 = new MonkeyLightning.DataProvider.Providers.Basic.ModifyDailyTimeRangeControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOpenExcel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dpTimeRange1
            // 
            this.dpTimeRange1.Checked = true;
            this.dpTimeRange1.Day = "THU";
            this.dpTimeRange1.Location = new System.Drawing.Point(3, 12);
            this.dpTimeRange1.Name = "dpTimeRange1";
            this.dpTimeRange1.Range1 = "7a";
            this.dpTimeRange1.Range2 = "3p";
            this.dpTimeRange1.Size = new System.Drawing.Size(801, 60);
            this.dpTimeRange1.TabIndex = 0;
            this.dpTimeRange1.Text = "THU";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(40, 102);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(294, 144);
            this.panel1.TabIndex = 1;
            // 
            // btnOpenExcel
            // 
            this.btnOpenExcel.Location = new System.Drawing.Point(349, 102);
            this.btnOpenExcel.Name = "btnOpenExcel";
            this.btnOpenExcel.Size = new System.Drawing.Size(124, 35);
            this.btnOpenExcel.TabIndex = 2;
            this.btnOpenExcel.Text = "Open Excel document";
            this.btnOpenExcel.UseVisualStyleBackColor = true;
            this.btnOpenExcel.Click += new System.EventHandler(this.btnOpenExcel_Click);
            // 
            // TestTimeRangeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 258);
            this.Controls.Add(this.btnOpenExcel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dpTimeRange1);
            this.Name = "TestTimeRangeForm";
            this.Text = "TestTimeRangeForm";
            this.ResumeLayout(false);

        }

        #endregion

        private MonkeyLightning.DataProvider.Providers.Basic.ModifyDailyTimeRangeControl dpTimeRange1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOpenExcel;
    }
}