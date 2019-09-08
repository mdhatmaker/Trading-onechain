namespace MonkeyLightning.UI.Forms
{
    partial class RuleConditionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RuleConditionForm));
            this.comboValue2 = new System.Windows.Forms.ComboBox();
            this.lblTrueFalse = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelValue2UI = new System.Windows.Forms.Panel();
            this.textValue2 = new System.Windows.Forms.TextBox();
            this.numericValue2 = new System.Windows.Forms.NumericUpDown();
            this.panelValue1UI = new System.Windows.Forms.Panel();
            this.textValue1 = new System.Windows.Forms.TextBox();
            this.numericValue1 = new System.Windows.Forms.NumericUpDown();
            this.comboValue1 = new System.Windows.Forms.ComboBox();
            this.comboCompare = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelValue2UI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericValue2)).BeginInit();
            this.panelValue1UI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericValue1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboValue2
            // 
            this.comboValue2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboValue2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboValue2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboValue2.FormattingEnabled = true;
            this.comboValue2.Items.AddRange(new object[] {
            "Chart Data",
            "Market Data",
            "Number",
            "Text"});
            this.comboValue2.Location = new System.Drawing.Point(327, 3);
            this.comboValue2.Name = "comboValue2";
            this.comboValue2.Size = new System.Drawing.Size(157, 21);
            this.comboValue2.Sorted = true;
            this.comboValue2.TabIndex = 1;
            this.comboValue2.SelectedIndexChanged += new System.EventHandler(this.comboValue2_SelectedIndexChanged);
            // 
            // lblTrueFalse
            // 
            this.lblTrueFalse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTrueFalse.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrueFalse.ForeColor = System.Drawing.Color.White;
            this.lblTrueFalse.Location = new System.Drawing.Point(172, 63);
            this.lblTrueFalse.Margin = new System.Windows.Forms.Padding(10);
            this.lblTrueFalse.Name = "lblTrueFalse";
            this.lblTrueFalse.Size = new System.Drawing.Size(142, 34);
            this.lblTrueFalse.TabIndex = 9;
            this.lblTrueFalse.Text = "True";
            this.lblTrueFalse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTrueFalse.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.panelValue2UI, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelValue1UI, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.comboValue1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblTrueFalse, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.comboCompare, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboValue2, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(487, 107);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // panelValue2UI
            // 
            this.panelValue2UI.Controls.Add(this.textValue2);
            this.panelValue2UI.Controls.Add(this.numericValue2);
            this.panelValue2UI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelValue2UI.Location = new System.Drawing.Point(327, 56);
            this.panelValue2UI.Name = "panelValue2UI";
            this.panelValue2UI.Size = new System.Drawing.Size(157, 48);
            this.panelValue2UI.TabIndex = 12;
            // 
            // textValue2
            // 
            this.textValue2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textValue2.Location = new System.Drawing.Point(14, 5);
            this.textValue2.Name = "textValue2";
            this.textValue2.Size = new System.Drawing.Size(119, 20);
            this.textValue2.TabIndex = 9;
            this.textValue2.Visible = false;
            this.textValue2.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // numericValue2
            // 
            this.numericValue2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericValue2.Location = new System.Drawing.Point(14, 5);
            this.numericValue2.Name = "numericValue2";
            this.numericValue2.Size = new System.Drawing.Size(132, 20);
            this.numericValue2.TabIndex = 8;
            this.numericValue2.Visible = false;
            this.numericValue2.ValueChanged += new System.EventHandler(this.value_Changed);
            // 
            // panelValue1UI
            // 
            this.panelValue1UI.Controls.Add(this.textValue1);
            this.panelValue1UI.Controls.Add(this.numericValue1);
            this.panelValue1UI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelValue1UI.Location = new System.Drawing.Point(3, 56);
            this.panelValue1UI.Name = "panelValue1UI";
            this.panelValue1UI.Size = new System.Drawing.Size(156, 48);
            this.panelValue1UI.TabIndex = 13;
            // 
            // textValue1
            // 
            this.textValue1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textValue1.Location = new System.Drawing.Point(14, 5);
            this.textValue1.Name = "textValue1";
            this.textValue1.Size = new System.Drawing.Size(119, 20);
            this.textValue1.TabIndex = 15;
            this.textValue1.Visible = false;
            this.textValue1.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // numericValue1
            // 
            this.numericValue1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericValue1.Location = new System.Drawing.Point(14, 5);
            this.numericValue1.Name = "numericValue1";
            this.numericValue1.Size = new System.Drawing.Size(132, 20);
            this.numericValue1.TabIndex = 14;
            this.numericValue1.Visible = false;
            this.numericValue1.ValueChanged += new System.EventHandler(this.value_Changed);
            // 
            // comboValue1
            // 
            this.comboValue1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboValue1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboValue1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboValue1.FormattingEnabled = true;
            this.comboValue1.Items.AddRange(new object[] {
            "ChartData",
            "MarketData",
            "Number",
            "Text"});
            this.comboValue1.Location = new System.Drawing.Point(3, 3);
            this.comboValue1.Name = "comboValue1";
            this.comboValue1.Size = new System.Drawing.Size(156, 21);
            this.comboValue1.Sorted = true;
            this.comboValue1.TabIndex = 1;
            this.comboValue1.SelectedIndexChanged += new System.EventHandler(this.comboValue1_SelectedIndexChanged);
            // 
            // comboCompare
            // 
            this.comboCompare.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboCompare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboCompare.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCompare.FormattingEnabled = true;
            this.comboCompare.Items.AddRange(new object[] {
            "greater than ( > )",
            "less than ( < )",
            "equal to ( = )"});
            this.comboCompare.Location = new System.Drawing.Point(165, 3);
            this.comboCompare.Name = "comboCompare";
            this.comboCompare.Size = new System.Drawing.Size(156, 21);
            this.comboCompare.TabIndex = 2;
            this.comboCompare.SelectedIndexChanged += new System.EventHandler(this.comboCompare_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Image = global::MonkeyLightning.Properties.Resources.Error_Symbol;
            this.btnCancel.Location = new System.Drawing.Point(424, 174);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 51);
            this.btnCancel.TabIndex = 13;
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
            this.btnOK.Enabled = false;
            this.btnOK.Image = global::MonkeyLightning.Properties.Resources.Checkmark;
            this.btnOK.Location = new System.Drawing.Point(343, 174);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 51);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "OK";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.SystemColors.Control;
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDescription.Location = new System.Drawing.Point(12, 125);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(430, 43);
            this.txtDescription.TabIndex = 14;
            // 
            // RuleConditionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 232);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.txtDescription);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RuleConditionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Condition Builder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RuleConditionForm_FormClosing);
            this.Load += new System.EventHandler(this.RuleConditionForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelValue2UI.ResumeLayout(false);
            this.panelValue2UI.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericValue2)).EndInit();
            this.panelValue1UI.ResumeLayout(false);
            this.panelValue1UI.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericValue1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboValue2;
        private System.Windows.Forms.Label lblTrueFalse;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelValue2UI;
        private System.Windows.Forms.TextBox textValue2;
        private System.Windows.Forms.NumericUpDown numericValue2;
        private System.Windows.Forms.ComboBox comboValue1;
        private System.Windows.Forms.ComboBox comboCompare;
        private System.Windows.Forms.Panel panelValue1UI;
        private System.Windows.Forms.TextBox textValue1;
        private System.Windows.Forms.NumericUpDown numericValue1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtDescription;
    }
}