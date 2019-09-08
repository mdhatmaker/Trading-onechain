namespace EZAPI.Framework.Chart
{
    partial class UIFormChooseBarData
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
            this.components = new System.ComponentModel.Container();
            this.lblSelectedBar = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.listSeriesData = new System.Windows.Forms.ListView();
            this.columnSeriesName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnSeriesValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblIndicatorName = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblSelectedBar
            // 
            this.lblSelectedBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelectedBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedBar.Location = new System.Drawing.Point(93, 7);
            this.lblSelectedBar.Name = "lblSelectedBar";
            this.lblSelectedBar.Size = new System.Drawing.Size(162, 19);
            this.lblSelectedBar.TabIndex = 2;
            this.lblSelectedBar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnFirst
            // 
            this.btnFirst.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFirst.Image = global::EZAPI.Properties.Resources.Button_First;
            this.btnFirst.Location = new System.Drawing.Point(2, 3);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(43, 42);
            this.btnFirst.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnFirst, "Select first bar of chart data (oldest bar)");
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnLast
            // 
            this.btnLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLast.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLast.Image = global::EZAPI.Properties.Resources.Button_Last;
            this.btnLast.Location = new System.Drawing.Point(303, 3);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(43, 42);
            this.btnLast.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnLast, "Select last bar of chart data (current bar)");
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNext.Image = global::EZAPI.Properties.Resources.Button_Next;
            this.btnNext.Location = new System.Drawing.Point(259, 3);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(43, 42);
            this.btnNext.TabIndex = 1;
            this.toolTip1.SetToolTip(this.btnNext, "Select next bar of chart data");
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrevious.Image = global::EZAPI.Properties.Resources.Button_Back;
            this.btnPrevious.Location = new System.Drawing.Point(46, 3);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(43, 42);
            this.btnPrevious.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnPrevious, "Select previous bar of chart data");
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // listSeriesData
            // 
            this.listSeriesData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listSeriesData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnSeriesName,
            this.columnSeriesValue});
            this.listSeriesData.FullRowSelect = true;
            this.listSeriesData.Location = new System.Drawing.Point(12, 51);
            this.listSeriesData.MultiSelect = false;
            this.listSeriesData.Name = "listSeriesData";
            this.listSeriesData.ShowGroups = false;
            this.listSeriesData.Size = new System.Drawing.Size(324, 156);
            this.listSeriesData.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listSeriesData.TabIndex = 5;
            this.listSeriesData.UseCompatibleStateImageBehavior = false;
            this.listSeriesData.View = System.Windows.Forms.View.Details;
            this.listSeriesData.SelectedIndexChanged += new System.EventHandler(this.listSeriesData_SelectedIndexChanged);
            // 
            // columnSeriesName
            // 
            this.columnSeriesName.Text = "Indicator Name";
            this.columnSeriesName.Width = 185;
            // 
            // columnSeriesValue
            // 
            this.columnSeriesValue.Text = "Value";
            this.columnSeriesValue.Width = 100;
            // 
            // lblIndicatorName
            // 
            this.lblIndicatorName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIndicatorName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIndicatorName.Location = new System.Drawing.Point(93, 29);
            this.lblIndicatorName.Name = "lblIndicatorName";
            this.lblIndicatorName.Size = new System.Drawing.Size(162, 19);
            this.lblIndicatorName.TabIndex = 8;
            this.lblIndicatorName.Text = "(select an indicator)";
            this.lblIndicatorName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Image = global::EZAPI.Properties.Resources.Error_Symbol;
            this.button1.Location = new System.Drawing.Point(255, 213);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 51);
            this.button1.TabIndex = 17;
            this.button1.Text = "Cancel";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::EZAPI.Properties.Resources.Checkmark;
            this.btnOK.Location = new System.Drawing.Point(171, 213);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 51);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "OK";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // UIFormChooseBarData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 271);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblIndicatorName);
            this.Controls.Add(this.listSeriesData);
            this.Controls.Add(this.btnFirst);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.lblSelectedBar);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "UIFormChooseBarData";
            this.Text = "Choose Bar Data";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UIFormChooseBarData_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblSelectedBar;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ListView listSeriesData;
        private System.Windows.Forms.ColumnHeader columnSeriesName;
        private System.Windows.Forms.ColumnHeader columnSeriesValue;
        private System.Windows.Forms.Label lblIndicatorName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnOK;
    }
}