namespace MonkeyLightning
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
            this.cboMarkets = new System.Windows.Forms.ComboBox();
            this.cboContracts = new System.Windows.Forms.ComboBox();
            this.cboExchanges = new System.Windows.Forms.ComboBox();
            this.lblLastPrice = new System.Windows.Forms.Label();
            this.lblOfferPrice = new System.Windows.Forms.Label();
            this.lblBidPrice = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtOfferVol1 = new System.Windows.Forms.TextBox();
            this.txtLastVolTotal1 = new System.Windows.Forms.TextBox();
            this.txtLast1 = new System.Windows.Forms.TextBox();
            this.txtNet1 = new System.Windows.Forms.TextBox();
            this.lblNet = new System.Windows.Forms.Label();
            this.txtLastVol1 = new System.Windows.Forms.TextBox();
            this.lblSells = new System.Windows.Forms.Label();
            this.txtBuys1 = new System.Windows.Forms.TextBox();
            this.txtSells1 = new System.Windows.Forms.TextBox();
            this.txtMarketDescription1 = new System.Windows.Forms.TextBox();
            this.txtBid1 = new System.Windows.Forms.TextBox();
            this.lblBuys = new System.Windows.Forms.Label();
            this.txtOffer1 = new System.Windows.Forms.TextBox();
            this.txtBidVol1 = new System.Windows.Forms.TextBox();
            this.lblTotalVol = new System.Windows.Forms.Label();
            this.lblLastVol = new System.Windows.Forms.Label();
            this.lblOfferVol = new System.Windows.Forms.Label();
            this.lblBidVol = new System.Windows.Forms.Label();
            this.panelTradeSteps = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panelStop = new System.Windows.Forms.Panel();
            this.lblStop = new System.Windows.Forms.Label();
            this.panelExit = new System.Windows.Forms.Panel();
            this.lblExit = new System.Windows.Forms.Label();
            this.panelEntry = new System.Windows.Forms.Panel();
            this.lblEntry = new System.Windows.Forms.Label();
            this.panelPreconditions = new System.Windows.Forms.Panel();
            this.lblPreconditions = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnChooseMarket = new System.Windows.Forms.Button();
            this.btnChart = new System.Windows.Forms.Button();
            this.btnAddRule = new System.Windows.Forms.Button();
            this.btnEditRule = new System.Windows.Forms.Button();
            this.btnRemoveRule = new System.Windows.Forms.Button();
            this.rulePanelControl1 = new MonkeyLightning.RulePanelControl();
            this.ruleSheetControl3 = new MonkeyLightning.RuleSheetControl();
            this.ruleSheetControl2 = new MonkeyLightning.RuleSheetControl();
            this.ruleSheetControl1 = new MonkeyLightning.RuleSheetControl();
            this.rulesPreconditions = new MonkeyLightning.RuleSheetControl();
            this.panelTradeSteps.SuspendLayout();
            this.panelStop.SuspendLayout();
            this.panelExit.SuspendLayout();
            this.panelEntry.SuspendLayout();
            this.panelPreconditions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTradeName
            // 
            this.txtTradeName.Font = new System.Drawing.Font("888_MANGA", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTradeName.Location = new System.Drawing.Point(12, 10);
            this.txtTradeName.Name = "txtTradeName";
            this.txtTradeName.Size = new System.Drawing.Size(188, 29);
            this.txtTradeName.TabIndex = 13;
            this.txtTradeName.Text = "(trade name)";
            this.txtTradeName.Click += new System.EventHandler(this.txtTradeName_Click);
            this.txtTradeName.Enter += new System.EventHandler(this.txtTradeName_Enter);
            // 
            // cboMarkets
            // 
            this.cboMarkets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMarkets.Location = new System.Drawing.Point(12, 93);
            this.cboMarkets.Name = "cboMarkets";
            this.cboMarkets.Size = new System.Drawing.Size(389, 21);
            this.cboMarkets.TabIndex = 14;
            this.cboMarkets.TabStop = false;
            // 
            // cboContracts
            // 
            this.cboContracts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboContracts.Location = new System.Drawing.Point(12, 69);
            this.cboContracts.Name = "cboContracts";
            this.cboContracts.Size = new System.Drawing.Size(389, 21);
            this.cboContracts.Sorted = true;
            this.cboContracts.TabIndex = 16;
            this.cboContracts.TabStop = false;
            // 
            // cboExchanges
            // 
            this.cboExchanges.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboExchanges.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cboExchanges.Location = new System.Drawing.Point(12, 45);
            this.cboExchanges.Name = "cboExchanges";
            this.cboExchanges.Size = new System.Drawing.Size(389, 21);
            this.cboExchanges.Sorted = true;
            this.cboExchanges.TabIndex = 15;
            this.cboExchanges.TabStop = false;
            // 
            // lblLastPrice
            // 
            this.lblLastPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastPrice.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblLastPrice.Location = new System.Drawing.Point(201, 238);
            this.lblLastPrice.Name = "lblLastPrice";
            this.lblLastPrice.Size = new System.Drawing.Size(60, 20);
            this.lblLastPrice.TabIndex = 65;
            this.lblLastPrice.Text = "Price:";
            this.lblLastPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOfferPrice
            // 
            this.lblOfferPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOfferPrice.ForeColor = System.Drawing.Color.Crimson;
            this.lblOfferPrice.Location = new System.Drawing.Point(109, 238);
            this.lblOfferPrice.Name = "lblOfferPrice";
            this.lblOfferPrice.Size = new System.Drawing.Size(60, 20);
            this.lblOfferPrice.TabIndex = 64;
            this.lblOfferPrice.Text = "Price:";
            this.lblOfferPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBidPrice
            // 
            this.lblBidPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBidPrice.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblBidPrice.Location = new System.Drawing.Point(17, 238);
            this.lblBidPrice.Name = "lblBidPrice";
            this.lblBidPrice.Size = new System.Drawing.Size(60, 20);
            this.lblBidPrice.TabIndex = 63;
            this.lblBidPrice.Text = "Price:";
            this.lblBidPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(12, 186);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(210, 20);
            this.Label1.TabIndex = 62;
            this.Label1.Text = "Market Description:";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtOfferVol1
            // 
            this.txtOfferVol1.BackColor = System.Drawing.Color.MistyRose;
            this.txtOfferVol1.Location = new System.Drawing.Point(173, 258);
            this.txtOfferVol1.Name = "txtOfferVol1";
            this.txtOfferVol1.ReadOnly = true;
            this.txtOfferVol1.Size = new System.Drawing.Size(28, 20);
            this.txtOfferVol1.TabIndex = 58;
            this.txtOfferVol1.TabStop = false;
            this.txtOfferVol1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtLastVolTotal1
            // 
            this.txtLastVolTotal1.BackColor = System.Drawing.Color.Honeydew;
            this.txtLastVolTotal1.Location = new System.Drawing.Point(295, 258);
            this.txtLastVolTotal1.Name = "txtLastVolTotal1";
            this.txtLastVolTotal1.ReadOnly = true;
            this.txtLastVolTotal1.Size = new System.Drawing.Size(60, 20);
            this.txtLastVolTotal1.TabIndex = 61;
            this.txtLastVolTotal1.TabStop = false;
            this.txtLastVolTotal1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtLast1
            // 
            this.txtLast1.BackColor = System.Drawing.Color.Honeydew;
            this.txtLast1.Location = new System.Drawing.Point(203, 258);
            this.txtLast1.Name = "txtLast1";
            this.txtLast1.ReadOnly = true;
            this.txtLast1.Size = new System.Drawing.Size(60, 20);
            this.txtLast1.TabIndex = 59;
            this.txtLast1.TabStop = false;
            this.txtLast1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtNet1
            // 
            this.txtNet1.BackColor = System.Drawing.Color.White;
            this.txtNet1.Location = new System.Drawing.Point(241, 206);
            this.txtNet1.Name = "txtNet1";
            this.txtNet1.ReadOnly = true;
            this.txtNet1.Size = new System.Drawing.Size(38, 20);
            this.txtNet1.TabIndex = 70;
            this.txtNet1.TabStop = false;
            this.txtNet1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblNet
            // 
            this.lblNet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNet.Location = new System.Drawing.Point(239, 186);
            this.lblNet.Name = "lblNet";
            this.lblNet.Size = new System.Drawing.Size(38, 18);
            this.lblNet.TabIndex = 71;
            this.lblNet.Text = "Net:";
            this.lblNet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtLastVol1
            // 
            this.txtLastVol1.BackColor = System.Drawing.Color.Honeydew;
            this.txtLastVol1.Location = new System.Drawing.Point(265, 258);
            this.txtLastVol1.Name = "txtLastVol1";
            this.txtLastVol1.ReadOnly = true;
            this.txtLastVol1.Size = new System.Drawing.Size(28, 20);
            this.txtLastVol1.TabIndex = 60;
            this.txtLastVol1.TabStop = false;
            this.txtLastVol1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblSells
            // 
            this.lblSells.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSells.Location = new System.Drawing.Point(319, 186);
            this.lblSells.Name = "lblSells";
            this.lblSells.Size = new System.Drawing.Size(38, 18);
            this.lblSells.TabIndex = 73;
            this.lblSells.Text = "Sells:";
            this.lblSells.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBuys1
            // 
            this.txtBuys1.BackColor = System.Drawing.Color.White;
            this.txtBuys1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.txtBuys1.Location = new System.Drawing.Point(281, 206);
            this.txtBuys1.Name = "txtBuys1";
            this.txtBuys1.ReadOnly = true;
            this.txtBuys1.Size = new System.Drawing.Size(38, 20);
            this.txtBuys1.TabIndex = 74;
            this.txtBuys1.TabStop = false;
            this.txtBuys1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSells1
            // 
            this.txtSells1.BackColor = System.Drawing.Color.White;
            this.txtSells1.ForeColor = System.Drawing.Color.Crimson;
            this.txtSells1.Location = new System.Drawing.Point(321, 206);
            this.txtSells1.Name = "txtSells1";
            this.txtSells1.ReadOnly = true;
            this.txtSells1.Size = new System.Drawing.Size(38, 20);
            this.txtSells1.TabIndex = 75;
            this.txtSells1.TabStop = false;
            this.txtSells1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMarketDescription1
            // 
            this.txtMarketDescription1.BackColor = System.Drawing.Color.White;
            this.txtMarketDescription1.Location = new System.Drawing.Point(16, 206);
            this.txtMarketDescription1.Name = "txtMarketDescription1";
            this.txtMarketDescription1.ReadOnly = true;
            this.txtMarketDescription1.Size = new System.Drawing.Size(208, 20);
            this.txtMarketDescription1.TabIndex = 54;
            this.txtMarketDescription1.TabStop = false;
            // 
            // txtBid1
            // 
            this.txtBid1.BackColor = System.Drawing.Color.LightCyan;
            this.txtBid1.Location = new System.Drawing.Point(19, 258);
            this.txtBid1.Name = "txtBid1";
            this.txtBid1.ReadOnly = true;
            this.txtBid1.Size = new System.Drawing.Size(60, 20);
            this.txtBid1.TabIndex = 55;
            this.txtBid1.TabStop = false;
            this.txtBid1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblBuys
            // 
            this.lblBuys.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuys.Location = new System.Drawing.Point(279, 186);
            this.lblBuys.Name = "lblBuys";
            this.lblBuys.Size = new System.Drawing.Size(38, 18);
            this.lblBuys.TabIndex = 72;
            this.lblBuys.Text = "Buys:";
            this.lblBuys.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtOffer1
            // 
            this.txtOffer1.BackColor = System.Drawing.Color.MistyRose;
            this.txtOffer1.Location = new System.Drawing.Point(111, 258);
            this.txtOffer1.Name = "txtOffer1";
            this.txtOffer1.ReadOnly = true;
            this.txtOffer1.Size = new System.Drawing.Size(60, 20);
            this.txtOffer1.TabIndex = 56;
            this.txtOffer1.TabStop = false;
            this.txtOffer1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtBidVol1
            // 
            this.txtBidVol1.BackColor = System.Drawing.Color.LightCyan;
            this.txtBidVol1.Location = new System.Drawing.Point(81, 258);
            this.txtBidVol1.Name = "txtBidVol1";
            this.txtBidVol1.ReadOnly = true;
            this.txtBidVol1.Size = new System.Drawing.Size(28, 20);
            this.txtBidVol1.TabIndex = 57;
            this.txtBidVol1.TabStop = false;
            this.txtBidVol1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotalVol
            // 
            this.lblTotalVol.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalVol.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblTotalVol.Location = new System.Drawing.Point(293, 238);
            this.lblTotalVol.Name = "lblTotalVol";
            this.lblTotalVol.Size = new System.Drawing.Size(64, 20);
            this.lblTotalVol.TabIndex = 69;
            this.lblTotalVol.Text = "Total Vol:";
            this.lblTotalVol.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLastVol
            // 
            this.lblLastVol.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastVol.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblLastVol.Location = new System.Drawing.Point(263, 238);
            this.lblLastVol.Name = "lblLastVol";
            this.lblLastVol.Size = new System.Drawing.Size(32, 20);
            this.lblLastVol.TabIndex = 68;
            this.lblLastVol.Text = "Vol:";
            this.lblLastVol.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOfferVol
            // 
            this.lblOfferVol.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOfferVol.ForeColor = System.Drawing.Color.Crimson;
            this.lblOfferVol.Location = new System.Drawing.Point(171, 238);
            this.lblOfferVol.Name = "lblOfferVol";
            this.lblOfferVol.Size = new System.Drawing.Size(32, 20);
            this.lblOfferVol.TabIndex = 67;
            this.lblOfferVol.Text = "Vol:";
            this.lblOfferVol.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBidVol
            // 
            this.lblBidVol.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBidVol.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblBidVol.Location = new System.Drawing.Point(79, 238);
            this.lblBidVol.Name = "lblBidVol";
            this.lblBidVol.Size = new System.Drawing.Size(32, 20);
            this.lblBidVol.TabIndex = 66;
            this.lblBidVol.Text = "Vol:";
            this.lblBidVol.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelTradeSteps
            // 
            this.panelTradeSteps.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelTradeSteps.Controls.Add(this.btnRemoveRule);
            this.panelTradeSteps.Controls.Add(this.btnEditRule);
            this.panelTradeSteps.Controls.Add(this.btnAddRule);
            this.panelTradeSteps.Controls.Add(this.button1);
            this.panelTradeSteps.Controls.Add(this.panelStop);
            this.panelTradeSteps.Controls.Add(this.panelExit);
            this.panelTradeSteps.Controls.Add(this.panelEntry);
            this.panelTradeSteps.Controls.Add(this.panelPreconditions);
            this.panelTradeSteps.Location = new System.Drawing.Point(407, 12);
            this.panelTradeSteps.Name = "panelTradeSteps";
            this.panelTradeSteps.Size = new System.Drawing.Size(618, 313);
            this.panelTradeSteps.TabIndex = 77;
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.FlatAppearance.BorderSize = 3;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::MonkeyLightning.Properties.Resources.GreenRecycleArrowIcons;
            this.button1.Location = new System.Drawing.Point(3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 98);
            this.button1.TabIndex = 16;
            this.button1.Text = "Next TradeStep";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panelStop
            // 
            this.panelStop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStop.Controls.Add(this.ruleSheetControl3);
            this.panelStop.Controls.Add(this.lblStop);
            this.panelStop.Location = new System.Drawing.Point(414, 158);
            this.panelStop.Name = "panelStop";
            this.panelStop.Size = new System.Drawing.Size(200, 149);
            this.panelStop.TabIndex = 15;
            // 
            // lblStop
            // 
            this.lblStop.Font = new System.Drawing.Font("888_MANGA", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStop.Location = new System.Drawing.Point(3, 0);
            this.lblStop.Name = "lblStop";
            this.lblStop.Size = new System.Drawing.Size(194, 23);
            this.lblStop.TabIndex = 1;
            this.lblStop.Text = "Stop";
            this.lblStop.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // panelExit
            // 
            this.panelExit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelExit.Controls.Add(this.ruleSheetControl2);
            this.panelExit.Controls.Add(this.lblExit);
            this.panelExit.Location = new System.Drawing.Point(208, 158);
            this.panelExit.Name = "panelExit";
            this.panelExit.Size = new System.Drawing.Size(200, 149);
            this.panelExit.TabIndex = 14;
            // 
            // lblExit
            // 
            this.lblExit.Font = new System.Drawing.Font("888_MANGA", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExit.Location = new System.Drawing.Point(3, 0);
            this.lblExit.Name = "lblExit";
            this.lblExit.Size = new System.Drawing.Size(194, 23);
            this.lblExit.TabIndex = 1;
            this.lblExit.Text = "Exit";
            this.lblExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // panelEntry
            // 
            this.panelEntry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelEntry.Controls.Add(this.ruleSheetControl1);
            this.panelEntry.Controls.Add(this.lblEntry);
            this.panelEntry.Location = new System.Drawing.Point(2, 158);
            this.panelEntry.Name = "panelEntry";
            this.panelEntry.Size = new System.Drawing.Size(200, 149);
            this.panelEntry.TabIndex = 13;
            // 
            // lblEntry
            // 
            this.lblEntry.Font = new System.Drawing.Font("888_MANGA", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntry.Location = new System.Drawing.Point(3, 0);
            this.lblEntry.Name = "lblEntry";
            this.lblEntry.Size = new System.Drawing.Size(194, 23);
            this.lblEntry.TabIndex = 1;
            this.lblEntry.Text = "Entry";
            this.lblEntry.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // panelPreconditions
            // 
            this.panelPreconditions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPreconditions.Controls.Add(this.rulesPreconditions);
            this.panelPreconditions.Controls.Add(this.lblPreconditions);
            this.panelPreconditions.Location = new System.Drawing.Point(208, 3);
            this.panelPreconditions.Name = "panelPreconditions";
            this.panelPreconditions.Size = new System.Drawing.Size(200, 149);
            this.panelPreconditions.TabIndex = 12;
            // 
            // lblPreconditions
            // 
            this.lblPreconditions.Font = new System.Drawing.Font("888_MANGA", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreconditions.Location = new System.Drawing.Point(3, 0);
            this.lblPreconditions.Name = "lblPreconditions";
            this.lblPreconditions.Size = new System.Drawing.Size(194, 23);
            this.lblPreconditions.TabIndex = 1;
            this.lblPreconditions.Text = "Preconditions";
            this.lblPreconditions.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(81, 459);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(161, 23);
            this.progressBar1.TabIndex = 78;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(248, 442);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(71, 50);
            this.pictureBox1.TabIndex = 79;
            this.pictureBox1.TabStop = false;
            // 
            // btnChooseMarket
            // 
            this.btnChooseMarket.Image = global::MonkeyLightning.Properties.Resources.chartSymbol1;
            this.btnChooseMarket.Location = new System.Drawing.Point(147, 120);
            this.btnChooseMarket.Name = "btnChooseMarket";
            this.btnChooseMarket.Size = new System.Drawing.Size(116, 39);
            this.btnChooseMarket.TabIndex = 76;
            this.btnChooseMarket.UseVisualStyleBackColor = true;
            // 
            // btnChart
            // 
            this.btnChart.Image = global::MonkeyLightning.Properties.Resources.chartSymbol1;
            this.btnChart.Location = new System.Drawing.Point(12, 324);
            this.btnChart.Name = "btnChart";
            this.btnChart.Size = new System.Drawing.Size(53, 31);
            this.btnChart.TabIndex = 12;
            this.btnChart.UseVisualStyleBackColor = true;
            // 
            // btnAddRule
            // 
            this.btnAddRule.Location = new System.Drawing.Point(478, 6);
            this.btnAddRule.Name = "btnAddRule";
            this.btnAddRule.Size = new System.Drawing.Size(87, 23);
            this.btnAddRule.TabIndex = 80;
            this.btnAddRule.Text = "Add Rule";
            this.btnAddRule.UseVisualStyleBackColor = true;
            this.btnAddRule.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnEditRule
            // 
            this.btnEditRule.Location = new System.Drawing.Point(478, 35);
            this.btnEditRule.Name = "btnEditRule";
            this.btnEditRule.Size = new System.Drawing.Size(87, 23);
            this.btnEditRule.TabIndex = 81;
            this.btnEditRule.Text = "Edit Rule";
            this.btnEditRule.UseVisualStyleBackColor = true;
            // 
            // btnRemoveRule
            // 
            this.btnRemoveRule.Location = new System.Drawing.Point(478, 64);
            this.btnRemoveRule.Name = "btnRemoveRule";
            this.btnRemoveRule.Size = new System.Drawing.Size(87, 23);
            this.btnRemoveRule.TabIndex = 82;
            this.btnRemoveRule.Text = "Remove Rule";
            this.btnRemoveRule.UseVisualStyleBackColor = true;
            // 
            // rulePanelControl1
            // 
            this.rulePanelControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rulePanelControl1.Location = new System.Drawing.Point(447, 341);
            this.rulePanelControl1.Name = "rulePanelControl1";
            this.rulePanelControl1.Size = new System.Drawing.Size(219, 160);
            this.rulePanelControl1.TabIndex = 80;
            this.rulePanelControl1.Text = "My Title";
            // 
            // ruleSheetControl3
            // 
            this.ruleSheetControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ruleSheetControl3.Location = new System.Drawing.Point(2, 27);
            this.ruleSheetControl3.Name = "ruleSheetControl3";
            this.ruleSheetControl3.Size = new System.Drawing.Size(194, 117);
            this.ruleSheetControl3.TabIndex = 3;
            // 
            // ruleSheetControl2
            // 
            this.ruleSheetControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ruleSheetControl2.Location = new System.Drawing.Point(2, 27);
            this.ruleSheetControl2.Name = "ruleSheetControl2";
            this.ruleSheetControl2.Size = new System.Drawing.Size(194, 117);
            this.ruleSheetControl2.TabIndex = 3;
            // 
            // ruleSheetControl1
            // 
            this.ruleSheetControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ruleSheetControl1.Location = new System.Drawing.Point(2, 27);
            this.ruleSheetControl1.Name = "ruleSheetControl1";
            this.ruleSheetControl1.Size = new System.Drawing.Size(194, 117);
            this.ruleSheetControl1.TabIndex = 3;
            // 
            // rulesPreconditions
            // 
            this.rulesPreconditions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rulesPreconditions.Location = new System.Drawing.Point(2, 27);
            this.rulesPreconditions.Name = "rulesPreconditions";
            this.rulesPreconditions.Size = new System.Drawing.Size(194, 117);
            this.rulesPreconditions.TabIndex = 2;
            // 
            // TradeBuilderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1037, 513);
            this.Controls.Add(this.rulePanelControl1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.panelTradeSteps);
            this.Controls.Add(this.btnChooseMarket);
            this.Controls.Add(this.lblLastPrice);
            this.Controls.Add(this.lblOfferPrice);
            this.Controls.Add(this.lblBidPrice);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.txtOfferVol1);
            this.Controls.Add(this.txtLastVolTotal1);
            this.Controls.Add(this.txtLast1);
            this.Controls.Add(this.txtNet1);
            this.Controls.Add(this.lblNet);
            this.Controls.Add(this.txtLastVol1);
            this.Controls.Add(this.lblSells);
            this.Controls.Add(this.txtBuys1);
            this.Controls.Add(this.txtSells1);
            this.Controls.Add(this.txtMarketDescription1);
            this.Controls.Add(this.txtBid1);
            this.Controls.Add(this.lblBuys);
            this.Controls.Add(this.txtOffer1);
            this.Controls.Add(this.txtBidVol1);
            this.Controls.Add(this.lblTotalVol);
            this.Controls.Add(this.lblLastVol);
            this.Controls.Add(this.lblOfferVol);
            this.Controls.Add(this.lblBidVol);
            this.Controls.Add(this.cboMarkets);
            this.Controls.Add(this.cboContracts);
            this.Controls.Add(this.cboExchanges);
            this.Controls.Add(this.txtTradeName);
            this.Controls.Add(this.btnChart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TradeBuilderForm";
            this.Text = "Trade Builder";
            this.panelTradeSteps.ResumeLayout(false);
            this.panelTradeSteps.PerformLayout();
            this.panelStop.ResumeLayout(false);
            this.panelExit.ResumeLayout(false);
            this.panelEntry.ResumeLayout(false);
            this.panelPreconditions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnChart;
        private System.Windows.Forms.TextBox txtTradeName;
        internal System.Windows.Forms.ComboBox cboMarkets;
        internal System.Windows.Forms.ComboBox cboContracts;
        internal System.Windows.Forms.ComboBox cboExchanges;
        internal System.Windows.Forms.Label lblLastPrice;
        internal System.Windows.Forms.Label lblOfferPrice;
        internal System.Windows.Forms.Label lblBidPrice;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox txtOfferVol1;
        internal System.Windows.Forms.TextBox txtLastVolTotal1;
        internal System.Windows.Forms.TextBox txtLast1;
        internal System.Windows.Forms.TextBox txtNet1;
        internal System.Windows.Forms.Label lblNet;
        internal System.Windows.Forms.TextBox txtLastVol1;
        internal System.Windows.Forms.Label lblSells;
        internal System.Windows.Forms.TextBox txtBuys1;
        internal System.Windows.Forms.TextBox txtSells1;
        internal System.Windows.Forms.TextBox txtMarketDescription1;
        internal System.Windows.Forms.TextBox txtBid1;
        internal System.Windows.Forms.Label lblBuys;
        internal System.Windows.Forms.TextBox txtOffer1;
        internal System.Windows.Forms.TextBox txtBidVol1;
        internal System.Windows.Forms.Label lblTotalVol;
        internal System.Windows.Forms.Label lblLastVol;
        internal System.Windows.Forms.Label lblOfferVol;
        internal System.Windows.Forms.Label lblBidVol;
        private System.Windows.Forms.Button btnChooseMarket;
        private System.Windows.Forms.Panel panelTradeSteps;
        private System.Windows.Forms.Panel panelStop;
        private System.Windows.Forms.Label lblStop;
        private System.Windows.Forms.Panel panelExit;
        private System.Windows.Forms.Label lblExit;
        private System.Windows.Forms.Panel panelEntry;
        private System.Windows.Forms.Label lblEntry;
        private System.Windows.Forms.Panel panelPreconditions;
        private System.Windows.Forms.Label lblPreconditions;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private RuleSheetControl rulesPreconditions;
        private System.Windows.Forms.Button btnAddRule;
        private RuleSheetControl ruleSheetControl3;
        private RuleSheetControl ruleSheetControl2;
        private RuleSheetControl ruleSheetControl1;
        private System.Windows.Forms.Button btnRemoveRule;
        private System.Windows.Forms.Button btnEditRule;
        private RulePanelControl rulePanelControl1;

    }
}