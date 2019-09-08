namespace MonkeyLightning.UI.Forms
{
    partial class GoldenRulesForm
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
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("1. Always lower your trade size when you\'re trading poorly.", 0);
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("2. Never turn a winner into a loser.", 0);
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("3. Your biggest loser can\'t exceed your biggest winner.", 0);
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("4. If your trade is not going anywhere in a given timeframe, it\'s time to exit.");
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem("5. Never take a big loss.");
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem("6. Learn to scale out your winners.");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GoldenRulesForm));
            this.listViewGoldenRules = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageListLarge = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // listViewGoldenRules
            // 
            this.listViewGoldenRules.AllowDrop = true;
            this.listViewGoldenRules.BackColor = System.Drawing.Color.Gold;
            this.listViewGoldenRules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewGoldenRules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewGoldenRules.Font = new System.Drawing.Font("888_MANGA", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewGoldenRules.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewGoldenRules.HoverSelection = true;
            this.listViewGoldenRules.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11,
            listViewItem12});
            this.listViewGoldenRules.LargeImageList = this.imageListLarge;
            this.listViewGoldenRules.Location = new System.Drawing.Point(0, 0);
            this.listViewGoldenRules.MultiSelect = false;
            this.listViewGoldenRules.Name = "listViewGoldenRules";
            this.listViewGoldenRules.ShowGroups = false;
            this.listViewGoldenRules.Size = new System.Drawing.Size(669, 194);
            this.listViewGoldenRules.TabIndex = 0;
            this.listViewGoldenRules.UseCompatibleStateImageBehavior = false;
            this.listViewGoldenRules.View = System.Windows.Forms.View.Details;
            this.listViewGoldenRules.DragOver += new System.Windows.Forms.DragEventHandler(this.listViewGoldenRules_DragOver);
            this.listViewGoldenRules.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewGoldenRules_MouseDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Rules";
            this.columnHeader1.Width = 660;
            // 
            // imageListLarge
            // 
            this.imageListLarge.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListLarge.ImageStream")));
            this.imageListLarge.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListLarge.Images.SetKeyName(0, "gold medal award.jpg");
            // 
            // GoldenRulesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 194);
            this.Controls.Add(this.listViewGoldenRules);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "GoldenRulesForm";
            this.Text = "Golden Rules of Trading";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GoldenRulesForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewGoldenRules;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ImageList imageListLarge;
    }
}