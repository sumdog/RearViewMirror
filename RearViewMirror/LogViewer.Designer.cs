namespace RearViewMirror
{
    partial class LogViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogViewer));
            this.rbLogTxt = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rbLogTxt
            // 
            this.rbLogTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rbLogTxt.Location = new System.Drawing.Point(12, 12);
            this.rbLogTxt.Name = "rbLogTxt";
            this.rbLogTxt.Size = new System.Drawing.Size(625, 317);
            this.rbLogTxt.TabIndex = 0;
            this.rbLogTxt.Text = "";
            // 
            // LogViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 341);
            this.Controls.Add(this.rbLogTxt);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LogViewer";
            this.Text = "LogViewer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rbLogTxt;
    }
}