namespace MonkeyLightning.UI.Forms
{
    partial class RuleBuilderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RuleBuilderForm));
            this.txtRuleName = new System.Windows.Forms.TextBox();
            this.cboCombineConditions = new System.Windows.Forms.ComboBox();
            this.cboRuleType = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnDeleteCondition = new System.Windows.Forms.Button();
            this.btnEditCondition = new System.Windows.Forms.Button();
            this.btnCreateCondition = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.conditionsControl = new MonkeyLightning.UI.Controls.TradeRuleConditionsControl();
            this.SuspendLayout();
            // 
            // txtRuleName
            // 
            this.txtRuleName.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRuleName.Location = new System.Drawing.Point(12, 10);
            this.txtRuleName.Name = "txtRuleName";
            this.txtRuleName.Size = new System.Drawing.Size(214, 26);
            this.txtRuleName.TabIndex = 4;
            this.txtRuleName.Text = "(rule name)";
            this.txtRuleName.Click += new System.EventHandler(this.txtRuleName_Click);
            this.txtRuleName.Enter += new System.EventHandler(this.txtRuleName_Enter);
            // 
            // cboCombineConditions
            // 
            this.cboCombineConditions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCombineConditions.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cboCombineConditions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCombineConditions.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCombineConditions.FormattingEnabled = true;
            this.cboCombineConditions.Items.AddRange(new object[] {
            "ALL condtions are met",
            "ANY conditions are met"});
            this.cboCombineConditions.Location = new System.Drawing.Point(246, 26);
            this.cboCombineConditions.Name = "cboCombineConditions";
            this.cboCombineConditions.Size = new System.Drawing.Size(185, 23);
            this.cboCombineConditions.TabIndex = 5;
            // 
            // cboRuleType
            // 
            this.cboRuleType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboRuleType.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cboRuleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRuleType.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboRuleType.FormattingEnabled = true;
            this.cboRuleType.Items.AddRange(new object[] {
            "VALUE",
            "TRIGGER"});
            this.cboRuleType.Location = new System.Drawing.Point(246, 82);
            this.cboRuleType.Name = "cboRuleType";
            this.cboRuleType.Size = new System.Drawing.Size(185, 23);
            this.cboRuleType.TabIndex = 6;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Image = global::MonkeyLightning.Properties.Resources.Error_Symbol;
            this.btnCancel.Location = new System.Drawing.Point(356, 294);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 51);
            this.btnCancel.TabIndex = 8;
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
            this.btnOK.Location = new System.Drawing.Point(275, 294);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 51);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnDeleteCondition
            // 
            this.btnDeleteCondition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteCondition.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteCondition.Image = global::MonkeyLightning.Properties.Resources.Error;
            this.btnDeleteCondition.Location = new System.Drawing.Point(12, 294);
            this.btnDeleteCondition.Name = "btnDeleteCondition";
            this.btnDeleteCondition.Size = new System.Drawing.Size(99, 51);
            this.btnDeleteCondition.TabIndex = 3;
            this.btnDeleteCondition.Text = "Delete Condition";
            this.btnDeleteCondition.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDeleteCondition.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteCondition.UseVisualStyleBackColor = true;
            this.btnDeleteCondition.Click += new System.EventHandler(this.btnDeleteCondition_Click);
            // 
            // btnEditCondition
            // 
            this.btnEditCondition.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditCondition.Image = global::MonkeyLightning.Properties.Resources.Denided;
            this.btnEditCondition.Location = new System.Drawing.Point(117, 54);
            this.btnEditCondition.Name = "btnEditCondition";
            this.btnEditCondition.Size = new System.Drawing.Size(99, 51);
            this.btnEditCondition.TabIndex = 2;
            this.btnEditCondition.Text = "Edit Condition";
            this.btnEditCondition.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEditCondition.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEditCondition.UseVisualStyleBackColor = true;
            this.btnEditCondition.Click += new System.EventHandler(this.btnEditCondition_Click);
            // 
            // btnCreateCondition
            // 
            this.btnCreateCondition.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreateCondition.Image = global::MonkeyLightning.Properties.Resources.Add;
            this.btnCreateCondition.Location = new System.Drawing.Point(12, 54);
            this.btnCreateCondition.Name = "btnCreateCondition";
            this.btnCreateCondition.Size = new System.Drawing.Size(99, 51);
            this.btnCreateCondition.TabIndex = 1;
            this.btnCreateCondition.Text = "Create Condition";
            this.btnCreateCondition.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCreateCondition.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCreateCondition.UseVisualStyleBackColor = true;
            this.btnCreateCondition.Click += new System.EventHandler(this.btnAddCondition_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(282, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "This rule is TRUE if...";
            // 
            // conditionsControl
            // 
            this.conditionsControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.conditionsControl.Location = new System.Drawing.Point(12, 116);
            this.conditionsControl.Name = "conditionsControl";
            this.conditionsControl.Size = new System.Drawing.Size(419, 172);
            this.conditionsControl.TabIndex = 0;
            // 
            // RuleBuilderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 350);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cboRuleType);
            this.Controls.Add(this.cboCombineConditions);
            this.Controls.Add(this.txtRuleName);
            this.Controls.Add(this.btnDeleteCondition);
            this.Controls.Add(this.btnEditCondition);
            this.Controls.Add(this.btnCreateCondition);
            this.Controls.Add(this.conditionsControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RuleBuilderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Rule Builder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RuleBuilderForm_FormClosing);
            this.Load += new System.EventHandler(this.RuleBuilderForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MonkeyLightning.UI.Controls.TradeRuleConditionsControl conditionsControl;
        private System.Windows.Forms.Button btnCreateCondition;
        private System.Windows.Forms.Button btnEditCondition;
        private System.Windows.Forms.Button btnDeleteCondition;
        private System.Windows.Forms.TextBox txtRuleName;
        private System.Windows.Forms.ComboBox cboCombineConditions;
        private System.Windows.Forms.ComboBox cboRuleType;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
    }
}