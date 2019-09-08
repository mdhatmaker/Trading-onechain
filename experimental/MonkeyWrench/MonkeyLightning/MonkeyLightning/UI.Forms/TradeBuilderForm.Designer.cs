namespace MonkeyLightning.UI.Forms
{
    partial class TradeBuilderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TradeBuilderForm));
            this.txtTradeName = new System.Windows.Forms.TextBox();
            this.panelTradeSteps = new System.Windows.Forms.Panel();
            this.panelStop = new MonkeyLightning.UI.Controls.TradeRulePanelControl();
            this.panelExit = new MonkeyLightning.UI.Controls.TradeRulePanelControl();
            this.panelEntry = new MonkeyLightning.UI.Controls.TradeRulePanelControl();
            this.panelPreconditions = new MonkeyLightning.UI.Controls.TradeRulePanelControl();
            this.btnRemoveRule = new System.Windows.Forms.Button();
            this.btnEditRule = new System.Windows.Forms.Button();
            this.btnRules = new System.Windows.Forms.Button();
            this.btnNextTradeStep = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnSelectMarket = new System.Windows.Forms.Button();
            this.txtMarket = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkBuy = new System.Windows.Forms.CheckBox();
            this.chkSell = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboExecutionEngine = new System.Windows.Forms.ComboBox();
            this.panelTradeSteps.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtTradeName
            // 
            this.txtTradeName.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTradeName.Location = new System.Drawing.Point(12, 10);
            this.txtTradeName.Name = "txtTradeName";
            this.txtTradeName.Size = new System.Drawing.Size(229, 26);
            this.txtTradeName.TabIndex = 13;
            this.txtTradeName.Text = "(trade name)";
            this.txtTradeName.Click += new System.EventHandler(this.txtTradeName_Click);
            this.txtTradeName.Enter += new System.EventHandler(this.txtTradeName_Enter);
            // 
            // panelTradeSteps
            // 
            this.panelTradeSteps.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelTradeSteps.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelTradeSteps.Controls.Add(this.panelStop);
            this.panelTradeSteps.Controls.Add(this.panelExit);
            this.panelTradeSteps.Controls.Add(this.panelEntry);
            this.panelTradeSteps.Controls.Add(this.panelPreconditions);
            this.panelTradeSteps.Controls.Add(this.btnRemoveRule);
            this.panelTradeSteps.Controls.Add(this.btnEditRule);
            this.panelTradeSteps.Controls.Add(this.btnRules);
            this.panelTradeSteps.Controls.Add(this.btnNextTradeStep);
            this.panelTradeSteps.Location = new System.Drawing.Point(12, 71);
            this.panelTradeSteps.Name = "panelTradeSteps";
            this.panelTradeSteps.Size = new System.Drawing.Size(618, 313);
            this.panelTradeSteps.TabIndex = 77;
            // 
            // panelStop
            // 
            this.panelStop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStop.Location = new System.Drawing.Point(3, 158);
            this.panelStop.Name = "panelStop";
            this.panelStop.Size = new System.Drawing.Size(200, 149);
            this.panelStop.TabIndex = 86;
            this.panelStop.Text = "Stop";
            // 
            // panelExit
            // 
            this.panelExit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelExit.Location = new System.Drawing.Point(208, 158);
            this.panelExit.Name = "panelExit";
            this.panelExit.Size = new System.Drawing.Size(200, 149);
            this.panelExit.TabIndex = 85;
            this.panelExit.Text = "Exit";
            // 
            // panelEntry
            // 
            this.panelEntry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelEntry.Location = new System.Drawing.Point(413, 158);
            this.panelEntry.Name = "panelEntry";
            this.panelEntry.Size = new System.Drawing.Size(200, 149);
            this.panelEntry.TabIndex = 84;
            this.panelEntry.Text = "Entry";
            // 
            // panelPreconditions
            // 
            this.panelPreconditions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPreconditions.Location = new System.Drawing.Point(208, 3);
            this.panelPreconditions.Name = "panelPreconditions";
            this.panelPreconditions.Size = new System.Drawing.Size(200, 149);
            this.panelPreconditions.TabIndex = 83;
            this.panelPreconditions.Text = "Preconditions";
            // 
            // btnRemoveRule
            // 
            this.btnRemoveRule.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRemoveRule.Enabled = false;
            this.btnRemoveRule.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveRule.Image = global::MonkeyLightning.Properties.Resources.Error1;
            this.btnRemoveRule.Location = new System.Drawing.Point(499, 108);
            this.btnRemoveRule.Name = "btnRemoveRule";
            this.btnRemoveRule.Size = new System.Drawing.Size(112, 44);
            this.btnRemoveRule.TabIndex = 82;
            this.btnRemoveRule.Text = "Remove Rule";
            this.btnRemoveRule.UseVisualStyleBackColor = true;
            this.btnRemoveRule.Click += new System.EventHandler(this.btnRemoveRule_Click);
            // 
            // btnEditRule
            // 
            this.btnEditRule.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditRule.Enabled = false;
            this.btnEditRule.Font = new System.Drawing.Font("Comic Sans MS", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditRule.Image = global::MonkeyLightning.Properties.Resources.Denided1;
            this.btnEditRule.Location = new System.Drawing.Point(499, 58);
            this.btnEditRule.Name = "btnEditRule";
            this.btnEditRule.Size = new System.Drawing.Size(112, 44);
            this.btnEditRule.TabIndex = 81;
            this.btnEditRule.Text = "Edit Rule";
            this.btnEditRule.UseVisualStyleBackColor = true;
            this.btnEditRule.Click += new System.EventHandler(this.btnEditRule_Click);
            // 
            // btnRules
            // 
            this.btnRules.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRules.Font = new System.Drawing.Font("Comic Sans MS", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRules.Image = global::MonkeyLightning.Properties.Resources.Blueprint;
            this.btnRules.Location = new System.Drawing.Point(499, 6);
            this.btnRules.Name = "btnRules";
            this.btnRules.Size = new System.Drawing.Size(112, 49);
            this.btnRules.TabIndex = 80;
            this.btnRules.Text = "Rules";
            this.btnRules.UseVisualStyleBackColor = true;
            this.btnRules.Click += new System.EventHandler(this.btnAddRule_Click);
            // 
            // btnNextTradeStep
            // 
            this.btnNextTradeStep.AutoSize = true;
            this.btnNextTradeStep.BackColor = System.Drawing.Color.White;
            this.btnNextTradeStep.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNextTradeStep.FlatAppearance.BorderSize = 3;
            this.btnNextTradeStep.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNextTradeStep.Image = global::MonkeyLightning.Properties.Resources.GreenRecycleArrowIcons;
            this.btnNextTradeStep.Location = new System.Drawing.Point(3, 4);
            this.btnNextTradeStep.Name = "btnNextTradeStep";
            this.btnNextTradeStep.Size = new System.Drawing.Size(110, 106);
            this.btnNextTradeStep.TabIndex = 16;
            this.btnNextTradeStep.Text = "Trade Step";
            this.btnNextTradeStep.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnNextTradeStep.UseVisualStyleBackColor = false;
            this.btnNextTradeStep.Click += new System.EventHandler(this.btnNextTradeStep_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Image = global::MonkeyLightning.Properties.Resources.Error_Symbol;
            this.btnCancel.Location = new System.Drawing.Point(548, 395);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 51);
            this.btnCancel.TabIndex = 79;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.Image = global::MonkeyLightning.Properties.Resources.Checkmark;
            this.btnOK.Location = new System.Drawing.Point(467, 395);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 51);
            this.btnOK.TabIndex = 78;
            this.btnOK.Text = "OK";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnSelectMarket
            // 
            this.btnSelectMarket.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectMarket.Location = new System.Drawing.Point(555, 8);
            this.btnSelectMarket.Name = "btnSelectMarket";
            this.btnSelectMarket.Size = new System.Drawing.Size(75, 26);
            this.btnSelectMarket.TabIndex = 80;
            this.btnSelectMarket.Text = "Select...";
            this.btnSelectMarket.UseVisualStyleBackColor = true;
            this.btnSelectMarket.Click += new System.EventHandler(this.btnSelectMarket_Click);
            // 
            // txtMarket
            // 
            this.txtMarket.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMarket.Location = new System.Drawing.Point(271, 10);
            this.txtMarket.Name = "txtMarket";
            this.txtMarket.ReadOnly = true;
            this.txtMarket.Size = new System.Drawing.Size(271, 23);
            this.txtMarket.TabIndex = 81;
            this.txtMarket.Text = "(no market selected)";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 19);
            this.label1.TabIndex = 82;
            this.label1.Text = "Entry side:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkBuy
            // 
            this.chkBuy.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkBuy.BackColor = System.Drawing.Color.Blue;
            this.chkBuy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkBuy.ForeColor = System.Drawing.Color.White;
            this.chkBuy.Location = new System.Drawing.Point(90, 39);
            this.chkBuy.Name = "chkBuy";
            this.chkBuy.Size = new System.Drawing.Size(55, 24);
            this.chkBuy.TabIndex = 83;
            this.chkBuy.Text = "Buy";
            this.chkBuy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkBuy.UseVisualStyleBackColor = false;
            this.chkBuy.CheckedChanged += new System.EventHandler(this.chkBuy_CheckedChanged);
            this.chkBuy.Click += new System.EventHandler(this.chkBuy_Click);
            // 
            // chkSell
            // 
            this.chkSell.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkSell.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkSell.Location = new System.Drawing.Point(148, 39);
            this.chkSell.Name = "chkSell";
            this.chkSell.Size = new System.Drawing.Size(55, 24);
            this.chkSell.TabIndex = 84;
            this.chkSell.Text = "Sell";
            this.chkSell.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkSell.UseVisualStyleBackColor = true;
            this.chkSell.Click += new System.EventHandler(this.chkSell_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(261, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 19);
            this.label2.TabIndex = 85;
            this.label2.Text = "Execution engine:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboExecutionEngine
            // 
            this.cboExecutionEngine.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cboExecutionEngine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboExecutionEngine.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboExecutionEngine.FormattingEnabled = true;
            this.cboExecutionEngine.Items.AddRange(new object[] {
            "Aggressive",
            "Moderate",
            "Passive"});
            this.cboExecutionEngine.Location = new System.Drawing.Point(388, 38);
            this.cboExecutionEngine.Name = "cboExecutionEngine";
            this.cboExecutionEngine.Size = new System.Drawing.Size(154, 24);
            this.cboExecutionEngine.TabIndex = 86;
            // 
            // TradeBuilderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 458);
            this.Controls.Add(this.cboExecutionEngine);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkSell);
            this.Controls.Add(this.chkBuy);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMarket);
            this.Controls.Add(this.btnSelectMarket);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.panelTradeSteps);
            this.Controls.Add(this.txtTradeName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TradeBuilderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Trade Builder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TradeBuilderForm_FormClosing);
            this.Load += new System.EventHandler(this.TradeBuilderForm_Load);
            this.panelTradeSteps.ResumeLayout(false);
            this.panelTradeSteps.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTradeName;
        private System.Windows.Forms.Panel panelTradeSteps;
        private System.Windows.Forms.Button btnNextTradeStep;
        private System.Windows.Forms.Button btnRules;
        private System.Windows.Forms.Button btnRemoveRule;
        private System.Windows.Forms.Button btnEditRule;
        private MonkeyLightning.UI.Controls.TradeRulePanelControl panelEntry;
        private MonkeyLightning.UI.Controls.TradeRulePanelControl panelPreconditions;
        private MonkeyLightning.UI.Controls.TradeRulePanelControl panelExit;
        private MonkeyLightning.UI.Controls.TradeRulePanelControl panelStop;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnSelectMarket;
        private System.Windows.Forms.TextBox txtMarket;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkBuy;
        private System.Windows.Forms.CheckBox chkSell;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboExecutionEngine;

    }
}