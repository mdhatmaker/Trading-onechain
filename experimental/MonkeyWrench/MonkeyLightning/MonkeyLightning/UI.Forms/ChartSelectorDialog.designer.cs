namespace MonkeyLightning.UI.Forms
{
    partial class ChartSelectorDialog
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
            this.comboChartInterval = new System.Windows.Forms.ComboBox();
            this.numericPeriod = new System.Windows.Forms.NumericUpDown();
            this.btnChartMarket = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.numericPeriod)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboChartInterval
            // 
            this.comboChartInterval.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboChartInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboChartInterval.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboChartInterval.FormattingEnabled = true;
            this.comboChartInterval.Location = new System.Drawing.Point(12, 21);
            this.comboChartInterval.Name = "comboChartInterval";
            this.comboChartInterval.Size = new System.Drawing.Size(121, 28);
            this.comboChartInterval.Sorted = true;
            this.comboChartInterval.TabIndex = 0;
            // 
            // numericPeriod
            // 
            this.numericPeriod.Cursor = System.Windows.Forms.Cursors.Hand;
            this.numericPeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericPeriod.Location = new System.Drawing.Point(151, 21);
            this.numericPeriod.Name = "numericPeriod";
            this.numericPeriod.Size = new System.Drawing.Size(120, 26);
            this.numericPeriod.TabIndex = 2;
            this.numericPeriod.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnChartMarket
            // 
            this.btnChartMarket.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChartMarket.Location = new System.Drawing.Point(151, 65);
            this.btnChartMarket.Name = "btnChartMarket";
            this.btnChartMarket.Size = new System.Drawing.Size(120, 28);
            this.btnChartMarket.TabIndex = 4;
            this.btnChartMarket.Text = "Select market...";
            this.btnChartMarket.UseVisualStyleBackColor = true;
            this.btnChartMarket.Click += new System.EventHandler(this.btnChartMarket_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status});
            this.statusStrip1.Location = new System.Drawing.Point(0, 106);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(283, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // status
            // 
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(268, 17);
            this.status.Spring = true;
            this.status.Text = "Select chart to view";
            // 
            // ChartSelectorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 128);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnChartMarket);
            this.Controls.Add(this.numericPeriod);
            this.Controls.Add(this.comboChartInterval);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChartSelectorDialog";
            this.Text = "Monkey Charts";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TestChartForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numericPeriod)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboChartInterval;
        private System.Windows.Forms.NumericUpDown numericPeriod;
        private System.Windows.Forms.Button btnChartMarket;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel status;
    }
}

