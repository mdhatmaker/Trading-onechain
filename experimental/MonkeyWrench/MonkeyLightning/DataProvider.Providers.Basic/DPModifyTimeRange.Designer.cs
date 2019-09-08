namespace MonkeyLightning.DataProvider.Providers.Basic
{
    partial class DPModifyTimeRange
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
            this.rangeSelectorControl1 = new CustomRangeSelectorControl.RangeSelectorControl();
            this.btnInclusive = new System.Windows.Forms.Button();
            this.lblTimeRange1 = new System.Windows.Forms.Label();
            this.lblTimeRange2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rangeSelectorControl1
            // 
            this.rangeSelectorControl1.BackColor = System.Drawing.SystemColors.Control;
            this.rangeSelectorControl1.DelimiterForRange = ",";
            this.rangeSelectorControl1.DisabledBarColor = System.Drawing.Color.Silver;
            this.rangeSelectorControl1.DisabledRangeLabelColor = System.Drawing.Color.Silver;
            this.rangeSelectorControl1.GapFromLeftMargin = ((uint)(20u));
            this.rangeSelectorControl1.GapFromRightMargin = ((uint)(20u));
            this.rangeSelectorControl1.HeightOfThumb = 20F;
            this.rangeSelectorControl1.InFocusBarColor = System.Drawing.Color.Lime;
            this.rangeSelectorControl1.InFocusRangeLabelColor = System.Drawing.Color.Green;
            this.rangeSelectorControl1.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.rangeSelectorControl1.LeftThumbImagePath = null;
            this.rangeSelectorControl1.Location = new System.Drawing.Point(71, 3);
            this.rangeSelectorControl1.MiddleBarWidth = ((uint)(3u));
            this.rangeSelectorControl1.Name = "rangeSelectorControl1";
            this.rangeSelectorControl1.OutputStringFontColor = System.Drawing.SystemColors.Control;
            this.rangeSelectorControl1.Range1 = "7a";
            this.rangeSelectorControl1.Range2 = "3p";
            this.rangeSelectorControl1.RangeString = "";
            this.rangeSelectorControl1.RangeValues = "12am,1a,2a,3a,4a,5a,6a,7a,8a,9a,10a,11a,12pm,1p,2p,3p,4p,5p,6p,7p,8p,9p,10p,11p,1" +
    "2am";
            this.rangeSelectorControl1.ReverseThumbDirection = false;
            this.rangeSelectorControl1.RightThumbImagePath = null;
            this.rangeSelectorControl1.Size = new System.Drawing.Size(684, 54);
            this.rangeSelectorControl1.TabIndex = 1;
            this.rangeSelectorControl1.ThumbColor = System.Drawing.Color.Blue;
            this.rangeSelectorControl1.WidthOfThumb = 10F;
            this.rangeSelectorControl1.XMLFileName = null;
            // 
            // btnInclusive
            // 
            this.btnInclusive.Location = new System.Drawing.Point(2, 34);
            this.btnInclusive.Name = "btnInclusive";
            this.btnInclusive.Size = new System.Drawing.Size(69, 23);
            this.btnInclusive.TabIndex = 4;
            this.btnInclusive.Text = "Inclusive";
            this.btnInclusive.UseVisualStyleBackColor = true;
            this.btnInclusive.Click += new System.EventHandler(this.btnInclusive_Click);
            // 
            // lblTimeRange1
            // 
            this.lblTimeRange1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeRange1.Location = new System.Drawing.Point(6, 0);
            this.lblTimeRange1.Name = "lblTimeRange1";
            this.lblTimeRange1.Size = new System.Drawing.Size(55, 13);
            this.lblTimeRange1.TabIndex = 5;
            // 
            // lblTimeRange2
            // 
            this.lblTimeRange2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeRange2.Location = new System.Drawing.Point(6, 18);
            this.lblTimeRange2.Name = "lblTimeRange2";
            this.lblTimeRange2.Size = new System.Drawing.Size(55, 13);
            this.lblTimeRange2.TabIndex = 6;
            // 
            // DPModifyDailyTimeRange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTimeRange2);
            this.Controls.Add(this.lblTimeRange1);
            this.Controls.Add(this.btnInclusive);
            this.Controls.Add(this.rangeSelectorControl1);
            this.Name = "DPModifyDailyTimeRange";
            this.Size = new System.Drawing.Size(757, 60);
            this.Load += new System.EventHandler(this.DPModifyDailyTimeRangeControl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CustomRangeSelectorControl.RangeSelectorControl rangeSelectorControl1;
        private System.Windows.Forms.Button btnInclusive;
        private System.Windows.Forms.Label lblTimeRange1;
        private System.Windows.Forms.Label lblTimeRange2;
    }
}
