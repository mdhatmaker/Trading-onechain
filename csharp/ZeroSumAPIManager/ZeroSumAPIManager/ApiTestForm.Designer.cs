namespace ZeroSumAPIManager
{
    partial class ApiTestForm
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
            this.rtbStrategyOutput = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtbStrategyOutput
            // 
            this.rtbStrategyOutput.BackColor = System.Drawing.Color.Black;
            this.rtbStrategyOutput.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbStrategyOutput.ForeColor = System.Drawing.Color.White;
            this.rtbStrategyOutput.Location = new System.Drawing.Point(12, 12);
            this.rtbStrategyOutput.Name = "rtbStrategyOutput";
            this.rtbStrategyOutput.Size = new System.Drawing.Size(796, 605);
            this.rtbStrategyOutput.TabIndex = 0;
            this.rtbStrategyOutput.Text = "";
            // 
            // ApiTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 707);
            this.Controls.Add(this.rtbStrategyOutput);
            this.Name = "ApiTestForm";
            this.Text = "ApiTestForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbStrategyOutput;
    }
}