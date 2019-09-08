namespace EZAPI
{
    partial class APIMarketSelectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(APIMarketSelectForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageMarkets = new System.Windows.Forms.TabPage();
            this.marketListView = new System.Windows.Forms.ListView();
            this.largeImageList = new System.Windows.Forms.ImageList(this.components);
            this.smallImageList = new System.Windows.Forms.ImageList(this.components);
            this.tabPageSpreads = new System.Windows.Forms.TabPage();
            this.tabPageRecent = new System.Windows.Forms.TabPage();
            this.recentListView = new System.Windows.Forms.ListView();
            this.tabPageFavorites = new System.Windows.Forms.TabPage();
            this.favoritesListView = new System.Windows.Forms.ListView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonBack = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonHome = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButtonViewType = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemAddToFavorites = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.itemCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageMarkets.SuspendLayout();
            this.tabPageRecent.SuspendLayout();
            this.tabPageFavorites.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status});
            this.statusStrip1.Location = new System.Drawing.Point(0, 469);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(663, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // status
            // 
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(648, 17);
            this.status.Spring = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageMarkets);
            this.tabControl1.Controls.Add(this.tabPageSpreads);
            this.tabControl1.Controls.Add(this.tabPageRecent);
            this.tabControl1.Controls.Add(this.tabPageFavorites);
            this.tabControl1.Location = new System.Drawing.Point(0, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(663, 438);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPageMarkets
            // 
            this.tabPageMarkets.Controls.Add(this.marketListView);
            this.tabPageMarkets.Location = new System.Drawing.Point(4, 22);
            this.tabPageMarkets.Name = "tabPageMarkets";
            this.tabPageMarkets.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMarkets.Size = new System.Drawing.Size(655, 412);
            this.tabPageMarkets.TabIndex = 0;
            this.tabPageMarkets.Text = "Markets";
            this.tabPageMarkets.UseVisualStyleBackColor = true;
            // 
            // marketListView
            // 
            this.marketListView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.marketListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.marketListView.LargeImageList = this.largeImageList;
            this.marketListView.Location = new System.Drawing.Point(3, 3);
            this.marketListView.MultiSelect = false;
            this.marketListView.Name = "marketListView";
            this.marketListView.Size = new System.Drawing.Size(649, 406);
            this.marketListView.SmallImageList = this.smallImageList;
            this.marketListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.marketListView.TabIndex = 2;
            this.marketListView.UseCompatibleStateImageBehavior = false;
            this.marketListView.View = System.Windows.Forms.View.List;
            this.marketListView.ItemActivate += new System.EventHandler(this.marketListView_ItemActivate);
            this.marketListView.SelectedIndexChanged += new System.EventHandler(this.marketListView_SelectedIndexChanged);
            this.marketListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.marketListView_MouseClick);
            // 
            // largeImageList
            // 
            this.largeImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("largeImageList.ImageStream")));
            this.largeImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.largeImageList.Images.SetKeyName(0, "exchange");
            this.largeImageList.Images.SetKeyName(1, "contract");
            this.largeImageList.Images.SetKeyName(2, "market");
            // 
            // smallImageList
            // 
            this.smallImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("smallImageList.ImageStream")));
            this.smallImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.smallImageList.Images.SetKeyName(0, "exchange");
            this.smallImageList.Images.SetKeyName(1, "contract");
            this.smallImageList.Images.SetKeyName(2, "market");
            // 
            // tabPageSpreads
            // 
            this.tabPageSpreads.Location = new System.Drawing.Point(4, 22);
            this.tabPageSpreads.Name = "tabPageSpreads";
            this.tabPageSpreads.Size = new System.Drawing.Size(655, 412);
            this.tabPageSpreads.TabIndex = 3;
            this.tabPageSpreads.Text = "Spreads";
            this.tabPageSpreads.UseVisualStyleBackColor = true;
            // 
            // tabPageRecent
            // 
            this.tabPageRecent.Controls.Add(this.recentListView);
            this.tabPageRecent.Location = new System.Drawing.Point(4, 22);
            this.tabPageRecent.Name = "tabPageRecent";
            this.tabPageRecent.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRecent.Size = new System.Drawing.Size(655, 412);
            this.tabPageRecent.TabIndex = 1;
            this.tabPageRecent.Text = "Recent";
            this.tabPageRecent.UseVisualStyleBackColor = true;
            // 
            // recentListView
            // 
            this.recentListView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.recentListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.recentListView.LargeImageList = this.largeImageList;
            this.recentListView.Location = new System.Drawing.Point(3, 3);
            this.recentListView.MultiSelect = false;
            this.recentListView.Name = "recentListView";
            this.recentListView.Size = new System.Drawing.Size(649, 406);
            this.recentListView.SmallImageList = this.smallImageList;
            this.recentListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.recentListView.TabIndex = 3;
            this.recentListView.UseCompatibleStateImageBehavior = false;
            this.recentListView.View = System.Windows.Forms.View.List;
            this.recentListView.ItemActivate += new System.EventHandler(this.recentListView_ItemActivate);
            // 
            // tabPageFavorites
            // 
            this.tabPageFavorites.Controls.Add(this.favoritesListView);
            this.tabPageFavorites.Location = new System.Drawing.Point(4, 22);
            this.tabPageFavorites.Name = "tabPageFavorites";
            this.tabPageFavorites.Size = new System.Drawing.Size(655, 412);
            this.tabPageFavorites.TabIndex = 2;
            this.tabPageFavorites.Text = "Favorites";
            this.tabPageFavorites.UseVisualStyleBackColor = true;
            // 
            // favoritesListView
            // 
            this.favoritesListView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.favoritesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.favoritesListView.LargeImageList = this.largeImageList;
            this.favoritesListView.Location = new System.Drawing.Point(0, 0);
            this.favoritesListView.MultiSelect = false;
            this.favoritesListView.Name = "favoritesListView";
            this.favoritesListView.Size = new System.Drawing.Size(655, 412);
            this.favoritesListView.SmallImageList = this.smallImageList;
            this.favoritesListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.favoritesListView.TabIndex = 4;
            this.favoritesListView.UseCompatibleStateImageBehavior = false;
            this.favoritesListView.View = System.Windows.Forms.View.List;
            this.favoritesListView.ItemActivate += new System.EventHandler(this.favoritesListView_ItemActivate);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonBack,
            this.toolStripButtonHome,
            this.toolStripSeparator1,
            this.toolStripDropDownButtonViewType,
            this.toolStripTextBox1,
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(663, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonBack
            // 
            this.toolStripButtonBack.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonBack.Image")));
            this.toolStripButtonBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonBack.Name = "toolStripButtonBack";
            this.toolStripButtonBack.Size = new System.Drawing.Size(52, 22);
            this.toolStripButtonBack.Text = "Back";
            this.toolStripButtonBack.Click += new System.EventHandler(this.toolStripButtonBack_Click);
            // 
            // toolStripButtonHome
            // 
            this.toolStripButtonHome.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonHome.Image")));
            this.toolStripButtonHome.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonHome.Name = "toolStripButtonHome";
            this.toolStripButtonHome.Size = new System.Drawing.Size(60, 22);
            this.toolStripButtonHome.Text = "Home";
            this.toolStripButtonHome.Click += new System.EventHandler(this.toolStripButtonHome_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDropDownButtonViewType
            // 
            this.toolStripDropDownButtonViewType.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5});
            this.toolStripDropDownButtonViewType.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonViewType.Image")));
            this.toolStripDropDownButtonViewType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonViewType.Name = "toolStripDropDownButtonViewType";
            this.toolStripDropDownButtonViewType.Size = new System.Drawing.Size(61, 22);
            this.toolStripDropDownButtonViewType.Text = "View";
            this.toolStripDropDownButtonViewType.Click += new System.EventHandler(this.toolStripDropDownButtonViewType_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.CheckOnClick = true;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(129, 22);
            this.toolStripMenuItem1.Text = "Large Icon";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.CheckOnClick = true;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(129, 22);
            this.toolStripMenuItem2.Text = "Small Icon";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.CheckOnClick = true;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(129, 22);
            this.toolStripMenuItem3.Text = "Tile";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.CheckOnClick = true;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(129, 22);
            this.toolStripMenuItem4.Text = "List";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.CheckOnClick = true;
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(129, 22);
            this.toolStripMenuItem5.Text = "Details";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(150, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(45, 22);
            this.toolStripLabel1.Text = "Search:";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemAddToFavorites,
            this.toolStripMenuItem6,
            this.itemCancel});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(159, 54);
            // 
            // itemAddToFavorites
            // 
            this.itemAddToFavorites.Name = "itemAddToFavorites";
            this.itemAddToFavorites.Size = new System.Drawing.Size(158, 22);
            this.itemAddToFavorites.Text = "Add to favorites";
            this.itemAddToFavorites.Click += new System.EventHandler(this.itemAddToFavorites_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(155, 6);
            // 
            // itemCancel
            // 
            this.itemCancel.Name = "itemCancel";
            this.itemCancel.Size = new System.Drawing.Size(158, 22);
            this.itemCancel.Text = "Cancel";
            // 
            // APIMarketSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 491);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "APIMarketSelectForm";
            this.Text = "Market Selection";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.APIMarketSelectForm_FormClosing);
            this.Load += new System.EventHandler(this.APIMarketSelectForm_Load);
            this.VisibleChanged += new System.EventHandler(this.APIMarketSelectForm_VisibleChanged);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageMarkets.ResumeLayout(false);
            this.tabPageRecent.ResumeLayout(false);
            this.tabPageFavorites.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel status;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageMarkets;
        private System.Windows.Forms.ListView marketListView;
        private System.Windows.Forms.TabPage tabPageSpreads;
        private System.Windows.Forms.TabPage tabPageRecent;
        private System.Windows.Forms.TabPage tabPageFavorites;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonBack;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonViewType;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton toolStripButtonHome;
        private System.Windows.Forms.ImageList largeImageList;
        private System.Windows.Forms.ImageList smallImageList;
        private System.Windows.Forms.ListView recentListView;
        private System.Windows.Forms.ListView favoritesListView;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem itemAddToFavorites;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem itemCancel;
    }
}