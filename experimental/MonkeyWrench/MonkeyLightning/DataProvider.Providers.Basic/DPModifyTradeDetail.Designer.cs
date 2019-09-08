namespace MonkeyLightning.DataProvider.Providers.Basic
{
    partial class DPModifyTradeDetail
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
            this.lblSelected = new System.Windows.Forms.Label();
            this.lblChoicePrice1 = new System.Windows.Forms.Label();
            this.lblChoicePrice3 = new System.Windows.Forms.Label();
            this.lblChoiceTime1 = new System.Windows.Forms.Label();
            this.lblChoicePrice2 = new System.Windows.Forms.Label();
            this.lblChoicePrice4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblSelected
            // 
            this.lblSelected.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelected.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblSelected.Location = new System.Drawing.Point(0, 264);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(418, 27);
            this.lblSelected.TabIndex = 108;
            this.lblSelected.Text = "click data to select";
            this.lblSelected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblChoicePrice1
            // 
            this.lblChoicePrice1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblChoicePrice1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChoicePrice1.ForeColor = System.Drawing.Color.Black;
            this.lblChoicePrice1.Location = new System.Drawing.Point(11, 44);
            this.lblChoicePrice1.Name = "lblChoicePrice1";
            this.lblChoicePrice1.Size = new System.Drawing.Size(393, 23);
            this.lblChoicePrice1.TabIndex = 113;
            this.lblChoicePrice1.Tag = "FillVsLast";
            this.lblChoicePrice1.Text = "Fill price compared to current Last Price";
            this.lblChoicePrice1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblChoicePrice1.Click += new System.EventHandler(this.lblChoice_Click);
            // 
            // lblChoicePrice3
            // 
            this.lblChoicePrice3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblChoicePrice3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChoicePrice3.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblChoicePrice3.Location = new System.Drawing.Point(11, 111);
            this.lblChoicePrice3.Name = "lblChoicePrice3";
            this.lblChoicePrice3.Size = new System.Drawing.Size(393, 23);
            this.lblChoicePrice3.TabIndex = 115;
            this.lblChoicePrice3.Tag = "FillVsBid";
            this.lblChoicePrice3.Text = "Fill price compared to current Bid Price";
            this.lblChoicePrice3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblChoicePrice3.Click += new System.EventHandler(this.lblChoice_Click);
            // 
            // lblChoiceTime1
            // 
            this.lblChoiceTime1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblChoiceTime1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChoiceTime1.ForeColor = System.Drawing.Color.Black;
            this.lblChoiceTime1.Location = new System.Drawing.Point(11, 167);
            this.lblChoiceTime1.Name = "lblChoiceTime1";
            this.lblChoiceTime1.Size = new System.Drawing.Size(393, 23);
            this.lblChoiceTime1.TabIndex = 116;
            this.lblChoiceTime1.Tag = "ElapsedTime";
            this.lblChoiceTime1.Text = "Fill time compared to current Time";
            this.lblChoiceTime1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblChoiceTime1.Click += new System.EventHandler(this.lblChoice_Click);
            // 
            // lblChoicePrice2
            // 
            this.lblChoicePrice2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblChoicePrice2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChoicePrice2.ForeColor = System.Drawing.Color.Maroon;
            this.lblChoicePrice2.Location = new System.Drawing.Point(11, 13);
            this.lblChoicePrice2.Name = "lblChoicePrice2";
            this.lblChoicePrice2.Size = new System.Drawing.Size(393, 23);
            this.lblChoicePrice2.TabIndex = 117;
            this.lblChoicePrice2.Tag = "FillVsAsk";
            this.lblChoicePrice2.Text = "Fill price compared to current Offer Price";
            this.lblChoicePrice2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblChoicePrice2.Click += new System.EventHandler(this.lblChoice_Click);
            // 
            // lblChoicePrice4
            // 
            this.lblChoicePrice4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblChoicePrice4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChoicePrice4.ForeColor = System.Drawing.Color.Black;
            this.lblChoicePrice4.Location = new System.Drawing.Point(11, 77);
            this.lblChoicePrice4.Name = "lblChoicePrice4";
            this.lblChoicePrice4.Size = new System.Drawing.Size(393, 23);
            this.lblChoicePrice4.TabIndex = 118;
            this.lblChoicePrice4.Tag = "FillVsMid";
            this.lblChoicePrice4.Text = "Fill price compared to current bid/ask Mid Price";
            this.lblChoicePrice4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblChoicePrice4.Click += new System.EventHandler(this.lblChoice_Click);
            // 
            // DPModifyTradeDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblChoicePrice4);
            this.Controls.Add(this.lblChoicePrice2);
            this.Controls.Add(this.lblChoiceTime1);
            this.Controls.Add(this.lblChoicePrice3);
            this.Controls.Add(this.lblChoicePrice1);
            this.Controls.Add(this.lblSelected);
            this.Name = "DPModifyTradeDetail";
            this.Size = new System.Drawing.Size(418, 291);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label lblSelected;
        internal System.Windows.Forms.Label lblChoicePrice1;
        internal System.Windows.Forms.Label lblChoicePrice3;
        internal System.Windows.Forms.Label lblChoiceTime1;
        internal System.Windows.Forms.Label lblChoicePrice2;
        internal System.Windows.Forms.Label lblChoicePrice4;
    }
}
