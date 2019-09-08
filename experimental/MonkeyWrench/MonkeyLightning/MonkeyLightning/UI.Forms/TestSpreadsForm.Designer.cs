namespace MonkeyLightning.UI.Forms
{
    partial class TestSpreadsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestSpreadsForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtExecQty1 = new System.Windows.Forms.TextBox();
            this.btnMarket1 = new System.Windows.Forms.Button();
            this.txtPriceMult1 = new System.Windows.Forms.TextBox();
            this.lblMarket1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblMarket2 = new System.Windows.Forms.Label();
            this.txtPriceMult2 = new System.Windows.Forms.TextBox();
            this.btnMarket2 = new System.Windows.Forms.Button();
            this.txtExecQty2 = new System.Windows.Forms.TextBox();
            this.btnBuildSpread = new System.Windows.Forms.Button();
            this.lstDisplay = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panelExcel = new System.Windows.Forms.Panel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status});
            this.statusStrip1.Location = new System.Drawing.Point(0, 441);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // status
            // 
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(785, 17);
            this.status.Spring = true;
            this.status.Text = "(status)";
            // 
            // txtExecQty1
            // 
            this.txtExecQty1.Location = new System.Drawing.Point(108, 45);
            this.txtExecQty1.Name = "txtExecQty1";
            this.txtExecQty1.Size = new System.Drawing.Size(75, 20);
            this.txtExecQty1.TabIndex = 1;
            this.txtExecQty1.Text = "3";
            // 
            // btnMarket1
            // 
            this.btnMarket1.Location = new System.Drawing.Point(12, 12);
            this.btnMarket1.Name = "btnMarket1";
            this.btnMarket1.Size = new System.Drawing.Size(75, 23);
            this.btnMarket1.TabIndex = 2;
            this.btnMarket1.Text = "Market...";
            this.btnMarket1.UseVisualStyleBackColor = true;
            this.btnMarket1.Click += new System.EventHandler(this.btnMarket1_Click);
            // 
            // txtPriceMult1
            // 
            this.txtPriceMult1.Location = new System.Drawing.Point(108, 71);
            this.txtPriceMult1.Name = "txtPriceMult1";
            this.txtPriceMult1.Size = new System.Drawing.Size(75, 20);
            this.txtPriceMult1.TabIndex = 3;
            this.txtPriceMult1.Text = "12.6";
            // 
            // lblMarket1
            // 
            this.lblMarket1.Location = new System.Drawing.Point(105, 17);
            this.lblMarket1.Name = "lblMarket1";
            this.lblMarket1.Size = new System.Drawing.Size(213, 18);
            this.lblMarket1.TabIndex = 4;
            this.lblMarket1.Text = "(market 1)";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Execute Qty:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Price Multiplier:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "Price Multiplier:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Execute Qty:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblMarket2
            // 
            this.lblMarket2.Location = new System.Drawing.Point(105, 119);
            this.lblMarket2.Name = "lblMarket2";
            this.lblMarket2.Size = new System.Drawing.Size(213, 18);
            this.lblMarket2.TabIndex = 10;
            this.lblMarket2.Text = "(market 2)";
            // 
            // txtPriceMult2
            // 
            this.txtPriceMult2.Location = new System.Drawing.Point(108, 173);
            this.txtPriceMult2.Name = "txtPriceMult2";
            this.txtPriceMult2.Size = new System.Drawing.Size(75, 20);
            this.txtPriceMult2.TabIndex = 9;
            this.txtPriceMult2.Text = "-400";
            // 
            // btnMarket2
            // 
            this.btnMarket2.Location = new System.Drawing.Point(12, 114);
            this.btnMarket2.Name = "btnMarket2";
            this.btnMarket2.Size = new System.Drawing.Size(75, 23);
            this.btnMarket2.TabIndex = 8;
            this.btnMarket2.Text = "Market...";
            this.btnMarket2.UseVisualStyleBackColor = true;
            this.btnMarket2.Click += new System.EventHandler(this.btnMarket2_Click);
            // 
            // txtExecQty2
            // 
            this.txtExecQty2.Location = new System.Drawing.Point(108, 147);
            this.txtExecQty2.Name = "txtExecQty2";
            this.txtExecQty2.Size = new System.Drawing.Size(75, 20);
            this.txtExecQty2.TabIndex = 7;
            this.txtExecQty2.Text = "-4";
            // 
            // btnBuildSpread
            // 
            this.btnBuildSpread.Location = new System.Drawing.Point(200, 1);
            this.btnBuildSpread.Name = "btnBuildSpread";
            this.btnBuildSpread.Size = new System.Drawing.Size(118, 34);
            this.btnBuildSpread.TabIndex = 13;
            this.btnBuildSpread.Text = "Build Spread";
            this.btnBuildSpread.UseVisualStyleBackColor = true;
            this.btnBuildSpread.Click += new System.EventHandler(this.btnBuildSpread_Click);
            // 
            // lstDisplay
            // 
            this.lstDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstDisplay.FormattingEnabled = true;
            this.lstDisplay.Location = new System.Drawing.Point(12, 212);
            this.lstDisplay.Name = "lstDisplay";
            this.lstDisplay.Size = new System.Drawing.Size(776, 212);
            this.lstDisplay.TabIndex = 14;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(200, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 34);
            this.button1.TabIndex = 15;
            this.button1.Text = "Get CSV file";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(200, 71);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(118, 34);
            this.button2.TabIndex = 16;
            this.button2.Text = "Open Excel doc";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panelExcel
            // 
            this.panelExcel.Location = new System.Drawing.Point(417, 1);
            this.panelExcel.Name = "panelExcel";
            this.panelExcel.Size = new System.Drawing.Size(371, 205);
            this.panelExcel.TabIndex = 19;
            // 
            // TestSpreadsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 463);
            this.Controls.Add(this.panelExcel);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lstDisplay);
            this.Controls.Add(this.btnBuildSpread);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblMarket2);
            this.Controls.Add(this.txtPriceMult2);
            this.Controls.Add(this.btnMarket2);
            this.Controls.Add(this.txtExecQty2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMarket1);
            this.Controls.Add(this.txtPriceMult1);
            this.Controls.Add(this.btnMarket1);
            this.Controls.Add(this.txtExecQty1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "TestSpreadsForm";
            this.Text = "Monkey Spreads";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel status;
        private System.Windows.Forms.TextBox txtExecQty1;
        private System.Windows.Forms.Button btnMarket1;
        private System.Windows.Forms.TextBox txtPriceMult1;
        private System.Windows.Forms.Label lblMarket1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblMarket2;
        private System.Windows.Forms.TextBox txtPriceMult2;
        private System.Windows.Forms.Button btnMarket2;
        private System.Windows.Forms.TextBox txtExecQty2;
        private System.Windows.Forms.Button btnBuildSpread;
        private System.Windows.Forms.ListBox lstDisplay;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panelExcel;
    }
}

