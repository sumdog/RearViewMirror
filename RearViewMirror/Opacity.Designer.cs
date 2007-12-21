namespace RearViewMirror
{
    partial class Opacity
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
            this.opacityBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.b_close = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.opacityBar)).BeginInit();
            this.SuspendLayout();
            // 
            // opacityBar
            // 
            this.opacityBar.Location = new System.Drawing.Point(20, 45);
            this.opacityBar.Margin = new System.Windows.Forms.Padding(5);
            this.opacityBar.Name = "opacityBar";
            this.opacityBar.Size = new System.Drawing.Size(195, 42);
            this.opacityBar.TabIndex = 0;
            this.opacityBar.Scroll += new System.EventHandler(this.opacityBar_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Viewer Window Opacity";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // b_close
            // 
            this.b_close.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.b_close.Location = new System.Drawing.Point(24, 88);
            this.b_close.Name = "b_close";
            this.b_close.Size = new System.Drawing.Size(191, 24);
            this.b_close.TabIndex = 2;
            this.b_close.Text = "Close";
            this.b_close.UseVisualStyleBackColor = true;
            this.b_close.Click += new System.EventHandler(this.b_close_Click);
            // 
            // Opacity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(232, 124);
            this.ControlBox = false;
            this.Controls.Add(this.b_close);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.opacityBar);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Opacity";
            this.Text = "Opacity";
            ((System.ComponentModel.ISupportInitialize)(this.opacityBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar opacityBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button b_close;
    }
}