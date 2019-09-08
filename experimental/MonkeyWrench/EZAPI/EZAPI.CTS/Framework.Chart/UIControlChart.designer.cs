namespace EZAPI.Framework.Chart
{
    partial class UIControlChart
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

                // Manually get rid of the chooseBarDataForm because it is set up to Hide instead of Close.
                chooseBarDataForm.IsClosing = true;
                chooseBarDataForm.Close();
                chooseBarDataForm = null;
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panelChart = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMoreIndicatorDescription = new System.Windows.Forms.Button();
            this.txtIndicatorDescription = new System.Windows.Forms.TextBox();
            this.chkBottomChart = new System.Windows.Forms.CheckBox();
            this.chkMainChart = new System.Windows.Forms.CheckBox();
            this.indicatorParams = new EZAPI.Framework.Chart.UIControlIndicatorParameters();
            this.cboIndicator = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnChartMarket = new System.Windows.Forms.Button();
            this.numericPeriod = new System.Windows.Forms.NumericUpDown();
            this.comboChartInterval = new System.Windows.Forms.ComboBox();
            this.btnScaleYAxis = new System.Windows.Forms.Button();
            this.chkVolume = new System.Windows.Forms.CheckBox();
            this.chkGridLines = new System.Windows.Forms.CheckBox();
            this.btnResetZoom = new System.Windows.Forms.Button();
            this.btnZoomMinus = new System.Windows.Forms.Button();
            this.btnZoomPlus = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panelChart.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericPeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelChart
            // 
            this.panelChart.Controls.Add(this.label2);
            this.panelChart.Controls.Add(this.label1);
            this.panelChart.Controls.Add(this.btnMoreIndicatorDescription);
            this.panelChart.Controls.Add(this.txtIndicatorDescription);
            this.panelChart.Controls.Add(this.chkBottomChart);
            this.panelChart.Controls.Add(this.chkMainChart);
            this.panelChart.Controls.Add(this.indicatorParams);
            this.panelChart.Controls.Add(this.cboIndicator);
            this.panelChart.Controls.Add(this.statusStrip1);
            this.panelChart.Controls.Add(this.btnChartMarket);
            this.panelChart.Controls.Add(this.numericPeriod);
            this.panelChart.Controls.Add(this.comboChartInterval);
            this.panelChart.Controls.Add(this.btnScaleYAxis);
            this.panelChart.Controls.Add(this.chkVolume);
            this.panelChart.Controls.Add(this.chkGridLines);
            this.panelChart.Controls.Add(this.btnResetZoom);
            this.panelChart.Controls.Add(this.btnZoomMinus);
            this.panelChart.Controls.Add(this.btnZoomPlus);
            this.panelChart.Controls.Add(this.chart1);
            this.panelChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChart.Location = new System.Drawing.Point(0, 0);
            this.panelChart.Name = "panelChart";
            this.panelChart.Size = new System.Drawing.Size(949, 529);
            this.panelChart.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "zoom";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 265);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "on/off";
            // 
            // btnMoreIndicatorDescription
            // 
            this.btnMoreIndicatorDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoreIndicatorDescription.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMoreIndicatorDescription.Enabled = false;
            this.btnMoreIndicatorDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoreIndicatorDescription.Location = new System.Drawing.Point(899, 486);
            this.btnMoreIndicatorDescription.Name = "btnMoreIndicatorDescription";
            this.btnMoreIndicatorDescription.Size = new System.Drawing.Size(47, 20);
            this.btnMoreIndicatorDescription.TabIndex = 5;
            this.btnMoreIndicatorDescription.Text = "more...";
            this.btnMoreIndicatorDescription.UseVisualStyleBackColor = true;
            this.btnMoreIndicatorDescription.Click += new System.EventHandler(this.btnMoreIndicatorDescription_Click);
            // 
            // txtIndicatorDescription
            // 
            this.txtIndicatorDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIndicatorDescription.BackColor = System.Drawing.SystemColors.Control;
            this.txtIndicatorDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtIndicatorDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(74)))), ((int)(((byte)(74)))));
            this.txtIndicatorDescription.Location = new System.Drawing.Point(3, 472);
            this.txtIndicatorDescription.Multiline = true;
            this.txtIndicatorDescription.Name = "txtIndicatorDescription";
            this.txtIndicatorDescription.ReadOnly = true;
            this.txtIndicatorDescription.Size = new System.Drawing.Size(936, 33);
            this.txtIndicatorDescription.TabIndex = 22;
            // 
            // chkBottomChart
            // 
            this.chkBottomChart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkBottomChart.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkBottomChart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkBottomChart.Location = new System.Drawing.Point(141, 440);
            this.chkBottomChart.Name = "chkBottomChart";
            this.chkBottomChart.Size = new System.Drawing.Size(82, 24);
            this.chkBottomChart.TabIndex = 21;
            this.chkBottomChart.Text = "Bottom Chart";
            this.chkBottomChart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkBottomChart.UseVisualStyleBackColor = true;
            this.chkBottomChart.CheckedChanged += new System.EventHandler(this.chkBottomChart_CheckedChanged);
            // 
            // chkMainChart
            // 
            this.chkMainChart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkMainChart.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkMainChart.Checked = true;
            this.chkMainChart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMainChart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkMainChart.Location = new System.Drawing.Point(39, 440);
            this.chkMainChart.Name = "chkMainChart";
            this.chkMainChart.Size = new System.Drawing.Size(82, 24);
            this.chkMainChart.TabIndex = 20;
            this.chkMainChart.Text = "Main Chart";
            this.chkMainChart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkMainChart.UseVisualStyleBackColor = true;
            this.chkMainChart.CheckedChanged += new System.EventHandler(this.chkMainChart_CheckedChanged);
            // 
            // indicatorParams
            // 
            this.indicatorParams.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.indicatorParams.Location = new System.Drawing.Point(287, 402);
            this.indicatorParams.Name = "indicatorParams";
            this.indicatorParams.Parameters = null;
            this.indicatorParams.Size = new System.Drawing.Size(364, 69);
            this.indicatorParams.TabIndex = 5;
            // 
            // cboIndicator
            // 
            this.cboIndicator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboIndicator.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cboIndicator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIndicator.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboIndicator.FormattingEnabled = true;
            this.cboIndicator.Location = new System.Drawing.Point(3, 404);
            this.cboIndicator.Name = "cboIndicator";
            this.cboIndicator.Size = new System.Drawing.Size(269, 28);
            this.cboIndicator.Sorted = true;
            this.cboIndicator.TabIndex = 19;
            this.cboIndicator.SelectedIndexChanged += new System.EventHandler(this.cboIndicator_SelectedIndexChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status});
            this.statusStrip1.Location = new System.Drawing.Point(0, 507);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(949, 22);
            this.statusStrip1.TabIndex = 18;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // status
            // 
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(934, 17);
            this.status.Spring = true;
            // 
            // btnChartMarket
            // 
            this.btnChartMarket.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChartMarket.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChartMarket.Location = new System.Drawing.Point(687, 421);
            this.btnChartMarket.Name = "btnChartMarket";
            this.btnChartMarket.Size = new System.Drawing.Size(120, 32);
            this.btnChartMarket.TabIndex = 17;
            this.btnChartMarket.Text = "Select market...";
            this.btnChartMarket.UseVisualStyleBackColor = true;
            this.btnChartMarket.Click += new System.EventHandler(this.btnChartMarket_Click);
            // 
            // numericPeriod
            // 
            this.numericPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericPeriod.Cursor = System.Windows.Forms.Cursors.Hand;
            this.numericPeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericPeriod.Location = new System.Drawing.Point(819, 405);
            this.numericPeriod.Name = "numericPeriod";
            this.numericPeriod.Size = new System.Drawing.Size(120, 26);
            this.numericPeriod.TabIndex = 16;
            this.numericPeriod.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // comboChartInterval
            // 
            this.comboChartInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboChartInterval.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboChartInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboChartInterval.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboChartInterval.FormattingEnabled = true;
            this.comboChartInterval.Location = new System.Drawing.Point(818, 438);
            this.comboChartInterval.Name = "comboChartInterval";
            this.comboChartInterval.Size = new System.Drawing.Size(121, 28);
            this.comboChartInterval.Sorted = true;
            this.comboChartInterval.TabIndex = 15;
            // 
            // btnScaleYAxis
            // 
            this.btnScaleYAxis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnScaleYAxis.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnScaleYAxis.Location = new System.Drawing.Point(3, 127);
            this.btnScaleYAxis.Name = "btnScaleYAxis";
            this.btnScaleYAxis.Size = new System.Drawing.Size(36, 23);
            this.btnScaleYAxis.TabIndex = 12;
            this.btnScaleYAxis.Text = "fit Y";
            this.btnScaleYAxis.UseVisualStyleBackColor = true;
            this.btnScaleYAxis.Click += new System.EventHandler(this.btnScaleYAxis_Click);
            // 
            // chkVolume
            // 
            this.chkVolume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkVolume.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkVolume.BackColor = System.Drawing.Color.White;
            this.chkVolume.Checked = true;
            this.chkVolume.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVolume.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkVolume.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkVolume.Location = new System.Drawing.Point(3, 301);
            this.chkVolume.Name = "chkVolume";
            this.chkVolume.Size = new System.Drawing.Size(42, 23);
            this.chkVolume.TabIndex = 11;
            this.chkVolume.Text = "bottom";
            this.chkVolume.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkVolume.UseVisualStyleBackColor = false;
            this.chkVolume.CheckedChanged += new System.EventHandler(this.chkVolume_CheckedChanged);
            // 
            // chkGridLines
            // 
            this.chkGridLines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkGridLines.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkGridLines.BackColor = System.Drawing.Color.White;
            this.chkGridLines.Checked = true;
            this.chkGridLines.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGridLines.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkGridLines.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGridLines.Location = new System.Drawing.Point(3, 281);
            this.chkGridLines.Name = "chkGridLines";
            this.chkGridLines.Size = new System.Drawing.Size(42, 23);
            this.chkGridLines.TabIndex = 10;
            this.chkGridLines.Text = "grid";
            this.chkGridLines.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkGridLines.UseVisualStyleBackColor = false;
            this.chkGridLines.CheckedChanged += new System.EventHandler(this.chkGridLines_CheckedChanged);
            // 
            // btnResetZoom
            // 
            this.btnResetZoom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnResetZoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetZoom.Location = new System.Drawing.Point(3, 153);
            this.btnResetZoom.Name = "btnResetZoom";
            this.btnResetZoom.Size = new System.Drawing.Size(36, 23);
            this.btnResetZoom.TabIndex = 9;
            this.btnResetZoom.Text = "reset";
            this.btnResetZoom.UseVisualStyleBackColor = true;
            this.btnResetZoom.Click += new System.EventHandler(this.btnResetZoom_Click);
            // 
            // btnZoomMinus
            // 
            this.btnZoomMinus.BackgroundImage = global::EZAPI.Properties.Resources.Search;
            this.btnZoomMinus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnZoomMinus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnZoomMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZoomMinus.Location = new System.Drawing.Point(1, 76);
            this.btnZoomMinus.Name = "btnZoomMinus";
            this.btnZoomMinus.Size = new System.Drawing.Size(41, 41);
            this.btnZoomMinus.TabIndex = 8;
            this.btnZoomMinus.Text = "-";
            this.btnZoomMinus.UseVisualStyleBackColor = true;
            this.btnZoomMinus.Click += new System.EventHandler(this.btnZoomMinus_Click);
            // 
            // btnZoomPlus
            // 
            this.btnZoomPlus.BackgroundImage = global::EZAPI.Properties.Resources.Search;
            this.btnZoomPlus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnZoomPlus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnZoomPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZoomPlus.Location = new System.Drawing.Point(1, 29);
            this.btnZoomPlus.Name = "btnZoomPlus";
            this.btnZoomPlus.Size = new System.Drawing.Size(41, 41);
            this.btnZoomPlus.TabIndex = 7;
            this.btnZoomPlus.Text = "+";
            this.btnZoomPlus.UseVisualStyleBackColor = true;
            this.btnZoomPlus.Click += new System.EventHandler(this.btnZoomPlus_Click);
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.AlignWithChartArea = "ChartAreaBottom";
            chartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisX.ScaleView.Zoomable = false;
            chartArea1.AxisX2.ScaleView.Zoomable = false;
            chartArea1.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisY.Maximum = 71000D;
            chartArea1.AxisY.Minimum = 68000D;
            chartArea1.AxisY.ScaleView.Zoomable = false;
            chartArea1.AxisY2.ScaleView.Zoomable = false;
            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorX.LineColor = System.Drawing.Color.Maroon;
            chartArea1.CursorY.LineColor = System.Drawing.Color.Maroon;
            chartArea1.Name = "ChartAreaMain";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 70F;
            chartArea1.Position.Width = 94F;
            chartArea1.Position.X = 3F;
            chartArea1.Position.Y = 3F;
            chartArea1.ShadowOffset = 2;
            chartArea2.AlignWithChartArea = "ChartAreaMain";
            chartArea2.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea2.AxisX.ScaleView.Zoomable = false;
            chartArea2.AxisX.ScrollBar.Enabled = false;
            chartArea2.AxisX2.ScaleView.Zoomable = false;
            chartArea2.AxisY.ScaleView.Zoomable = false;
            chartArea2.AxisY2.ScaleView.Zoomable = false;
            chartArea2.Name = "ChartAreaBottom";
            chartArea2.Position.Auto = false;
            chartArea2.Position.Height = 28F;
            chartArea2.Position.Width = 94F;
            chartArea2.Position.X = 3F;
            chartArea2.Position.Y = 72F;
            chartArea2.ShadowOffset = 2;
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.ChartAreas.Add(chartArea2);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series1.ChartArea = "ChartAreaMain";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.CustomProperties = "PriceDownColor=Red, PriceUpColor=Green";
            series1.EmptyPointStyle.IsVisibleInLegend = false;
            series1.Legend = "Legend1";
            series1.Name = "candlestickSeries";
            series1.ToolTip = "#VALX\\nH: #VALY1\\no: #VALY3 \\nc: #VALY4 *\\nL: #VALY2\\n";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series1.YValuesPerPoint = 4;
            series2.ChartArea = "ChartAreaBottom";
            series2.CustomProperties = "DrawingStyle=Emboss, LabelStyle=Center";
            series2.EmptyPointStyle.IsVisibleInLegend = false;
            series2.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series2.LabelBackColor = System.Drawing.Color.Black;
            series2.LabelForeColor = System.Drawing.Color.White;
            series2.LabelToolTip = "#AXISLABEL";
            series2.Legend = "Legend1";
            series2.Name = "candlestickVolumeSeries";
            series2.ToolTip = "#AXISLABEL  volume: #VAL";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(949, 396);
            this.chart1.TabIndex = 4;
            this.chart1.Text = "chart1";
            this.chart1.SelectionRangeChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.CursorEventArgs>(this.chart1_SelectionRangeChanged);
            this.chart1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseDown);
            this.chart1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseMove);
            this.chart1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseUp);
            // 
            // UIControlChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelChart);
            this.Name = "UIControlChart";
            this.Size = new System.Drawing.Size(949, 529);
            this.panelChart.ResumeLayout(false);
            this.panelChart.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericPeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelChart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnMoreIndicatorDescription;
        private System.Windows.Forms.TextBox txtIndicatorDescription;
        private System.Windows.Forms.CheckBox chkBottomChart;
        private System.Windows.Forms.CheckBox chkMainChart;
        private UIControlIndicatorParameters indicatorParams;
        private System.Windows.Forms.ComboBox cboIndicator;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel status;
        private System.Windows.Forms.Button btnChartMarket;
        private System.Windows.Forms.NumericUpDown numericPeriod;
        private System.Windows.Forms.ComboBox comboChartInterval;
        private System.Windows.Forms.Button btnScaleYAxis;
        private System.Windows.Forms.CheckBox chkVolume;
        private System.Windows.Forms.CheckBox chkGridLines;
        private System.Windows.Forms.Button btnResetZoom;
        private System.Windows.Forms.Button btnZoomMinus;
        private System.Windows.Forms.Button btnZoomPlus;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.ToolTip toolTip1;

    }
}