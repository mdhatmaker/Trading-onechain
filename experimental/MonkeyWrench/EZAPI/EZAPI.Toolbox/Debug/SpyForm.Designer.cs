namespace EZAPI.Toolbox.Debug
{
    partial class SpyForm
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
            this.listOutput = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listOutput
            // 
            this.listOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listOutput.FormattingEnabled = true;
            this.listOutput.Location = new System.Drawing.Point(0, 0);
            this.listOutput.Name = "listOutput";
            this.listOutput.Size = new System.Drawing.Size(604, 321);
            this.listOutput.TabIndex = 0;
            // 
            // SpyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 321);
            this.Controls.Add(this.listOutput);
            this.Name = "SpyForm";
            this.Text = "SpyForm";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listOutput;
    }
}