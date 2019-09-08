namespace MonkeyLightning.DataProvider.Providers.Basic
{
    partial class DPModifyWeeklyTimeRange
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
            this.dpTimeRangeSun = new MonkeyLightning.DataProvider.Providers.Basic.ModifyDailyTimeRangeControl();
            this.dpTimeRangeMon = new MonkeyLightning.DataProvider.Providers.Basic.ModifyDailyTimeRangeControl();
            this.dpTimeRangeTue = new MonkeyLightning.DataProvider.Providers.Basic.ModifyDailyTimeRangeControl();
            this.dpTimeRangeWed = new MonkeyLightning.DataProvider.Providers.Basic.ModifyDailyTimeRangeControl();
            this.dpTimeRangeThu = new MonkeyLightning.DataProvider.Providers.Basic.ModifyDailyTimeRangeControl();
            this.dpTimeRangeFri = new MonkeyLightning.DataProvider.Providers.Basic.ModifyDailyTimeRangeControl();
            this.dpTimeRangeSat = new MonkeyLightning.DataProvider.Providers.Basic.ModifyDailyTimeRangeControl();
            this.SuspendLayout();
            // 
            // dpTimeRangeSun
            // 
            this.dpTimeRangeSun.Checked = false;
            this.dpTimeRangeSun.Day = "SUN";
            this.dpTimeRangeSun.Location = new System.Drawing.Point(0, 0);
            this.dpTimeRangeSun.Name = "dpTimeRangeSun";
            this.dpTimeRangeSun.Size = new System.Drawing.Size(801, 60);
            this.dpTimeRangeSun.TabIndex = 7;
            this.dpTimeRangeSun.Text = "SUN";
            // 
            // dpTimeRangeMon
            // 
            this.dpTimeRangeMon.Checked = true;
            this.dpTimeRangeMon.Day = "MON";
            this.dpTimeRangeMon.Location = new System.Drawing.Point(0, 66);
            this.dpTimeRangeMon.Name = "dpTimeRangeMon";
            this.dpTimeRangeMon.Size = new System.Drawing.Size(801, 60);
            this.dpTimeRangeMon.TabIndex = 8;
            this.dpTimeRangeMon.Text = "MON";
            // 
            // dpTimeRangeTue
            // 
            this.dpTimeRangeTue.Checked = true;
            this.dpTimeRangeTue.Day = "TUE";
            this.dpTimeRangeTue.Location = new System.Drawing.Point(0, 132);
            this.dpTimeRangeTue.Name = "dpTimeRangeTue";
            this.dpTimeRangeTue.Size = new System.Drawing.Size(801, 60);
            this.dpTimeRangeTue.TabIndex = 9;
            this.dpTimeRangeTue.Text = "TUE";
            // 
            // dpTimeRangeWed
            // 
            this.dpTimeRangeWed.Checked = true;
            this.dpTimeRangeWed.Day = "WED";
            this.dpTimeRangeWed.Location = new System.Drawing.Point(0, 198);
            this.dpTimeRangeWed.Name = "dpTimeRangeWed";
            this.dpTimeRangeWed.Size = new System.Drawing.Size(801, 60);
            this.dpTimeRangeWed.TabIndex = 10;
            this.dpTimeRangeWed.Text = "WED";
            // 
            // dpTimeRangeThu
            // 
            this.dpTimeRangeThu.Checked = true;
            this.dpTimeRangeThu.Day = "THU";
            this.dpTimeRangeThu.Location = new System.Drawing.Point(0, 264);
            this.dpTimeRangeThu.Name = "dpTimeRangeThu";
            this.dpTimeRangeThu.Size = new System.Drawing.Size(801, 60);
            this.dpTimeRangeThu.TabIndex = 11;
            this.dpTimeRangeThu.Text = "THU";
            // 
            // dpTimeRangeFri
            // 
            this.dpTimeRangeFri.Checked = true;
            this.dpTimeRangeFri.Day = "FRI";
            this.dpTimeRangeFri.Location = new System.Drawing.Point(0, 330);
            this.dpTimeRangeFri.Name = "dpTimeRangeFri";
            this.dpTimeRangeFri.Size = new System.Drawing.Size(801, 60);
            this.dpTimeRangeFri.TabIndex = 12;
            this.dpTimeRangeFri.Text = "FRI";
            // 
            // dpTimeRangeSat
            // 
            this.dpTimeRangeSat.Checked = false;
            this.dpTimeRangeSat.Day = "SAT";
            this.dpTimeRangeSat.Location = new System.Drawing.Point(0, 396);
            this.dpTimeRangeSat.Name = "dpTimeRangeSat";
            this.dpTimeRangeSat.Size = new System.Drawing.Size(801, 60);
            this.dpTimeRangeSat.TabIndex = 13;
            this.dpTimeRangeSat.Text = "SAT";
            // 
            // DPModifyWeeklyTimeRangeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dpTimeRangeSat);
            this.Controls.Add(this.dpTimeRangeFri);
            this.Controls.Add(this.dpTimeRangeThu);
            this.Controls.Add(this.dpTimeRangeWed);
            this.Controls.Add(this.dpTimeRangeTue);
            this.Controls.Add(this.dpTimeRangeMon);
            this.Controls.Add(this.dpTimeRangeSun);
            this.Name = "DPModifyWeeklyTimeRangeControl";
            this.Size = new System.Drawing.Size(804, 462);
            this.ResumeLayout(false);

        }

        #endregion

        private ModifyDailyTimeRangeControl dpTimeRangeSun;
        private ModifyDailyTimeRangeControl dpTimeRangeMon;
        private ModifyDailyTimeRangeControl dpTimeRangeTue;
        private ModifyDailyTimeRangeControl dpTimeRangeWed;
        private ModifyDailyTimeRangeControl dpTimeRangeThu;
        private ModifyDailyTimeRangeControl dpTimeRangeFri;
        private ModifyDailyTimeRangeControl dpTimeRangeSat;

    }
}
