namespace MonkeyLightning.UI.Forms
{
    partial class RuleChooserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RuleChooserForm));
            this.btnNewRule = new System.Windows.Forms.Button();
            this.btnChoose = new System.Windows.Forms.Button();
            this.ruleSheet = new MonkeyLightning.UI.Controls.TradeRuleSheetControl();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnNewRule
            // 
            this.btnNewRule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNewRule.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNewRule.Image = global::MonkeyLightning.Properties.Resources.Add;
            this.btnNewRule.Location = new System.Drawing.Point(12, 505);
            this.btnNewRule.Name = "btnNewRule";
            this.btnNewRule.Size = new System.Drawing.Size(86, 58);
            this.btnNewRule.TabIndex = 1;
            this.btnNewRule.Text = "New Rule";
            this.btnNewRule.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnNewRule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNewRule.UseVisualStyleBackColor = true;
            this.btnNewRule.Click += new System.EventHandler(this.btnNewRule_Click);
            // 
            // btnChoose
            // 
            this.btnChoose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChoose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChoose.Image = global::MonkeyLightning.Properties.Resources.Checkmark;
            this.btnChoose.Location = new System.Drawing.Point(191, 505);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(84, 58);
            this.btnChoose.TabIndex = 2;
            this.btnChoose.Text = "Choose";
            this.btnChoose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnChoose.UseVisualStyleBackColor = true;
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // ruleSheet
            // 
            this.ruleSheet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ruleSheet.Location = new System.Drawing.Point(12, 43);
            this.ruleSheet.Name = "ruleSheet";
            this.ruleSheet.ShowActiveColumn = false;
            this.ruleSheet.Size = new System.Drawing.Size(263, 421);
            this.ruleSheet.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Navy;
            this.lblStatus.Location = new System.Drawing.Point(12, 467);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(263, 26);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHeader
            // 
            this.lblHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHeader.ForeColor = System.Drawing.Color.DimGray;
            this.lblHeader.Location = new System.Drawing.Point(4, 6);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(277, 26);
            this.lblHeader.TabIndex = 4;
            this.lblHeader.Text = "Select a rule and click Choose to use an existing rule in a Trade Step. Or click " +
    "New Rule to create one.";
            // 
            // RuleChooserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 566);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnChoose);
            this.Controls.Add(this.btnNewRule);
            this.Controls.Add(this.ruleSheet);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RuleChooserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Rule Chooser";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RuleChooserForm_FormClosing);
            this.Load += new System.EventHandler(this.RuleChooserForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private MonkeyLightning.UI.Controls.TradeRuleSheetControl ruleSheet;
        private System.Windows.Forms.Button btnNewRule;
        private System.Windows.Forms.Button btnChoose;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblHeader;
    }
}