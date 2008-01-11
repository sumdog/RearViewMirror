namespace MJPEGServer
{
    partial class ServerConnections
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
            this.b_Close = new System.Windows.Forms.Button();
            this.b_disconnect = new System.Windows.Forms.Button();
            this.lv_clients = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // b_Close
            // 
            this.b_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.b_Close.Location = new System.Drawing.Point(12, 136);
            this.b_Close.Name = "b_Close";
            this.b_Close.Size = new System.Drawing.Size(144, 22);
            this.b_Close.TabIndex = 1;
            this.b_Close.Text = "Close";
            this.b_Close.UseVisualStyleBackColor = true;
            this.b_Close.Click += new System.EventHandler(this.b_Close_Click);
            // 
            // b_disconnect
            // 
            this.b_disconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.b_disconnect.Location = new System.Drawing.Point(177, 136);
            this.b_disconnect.Name = "b_disconnect";
            this.b_disconnect.Size = new System.Drawing.Size(144, 22);
            this.b_disconnect.TabIndex = 2;
            this.b_disconnect.Text = "Disconnect";
            this.b_disconnect.UseVisualStyleBackColor = true;
            this.b_disconnect.Click += new System.EventHandler(this.b_disconnect_Click);
            // 
            // lv_clients
            // 
            this.lv_clients.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lv_clients.Location = new System.Drawing.Point(13, 13);
            this.lv_clients.Name = "lv_clients";
            this.lv_clients.Size = new System.Drawing.Size(308, 117);
            this.lv_clients.TabIndex = 3;
            this.lv_clients.UseCompatibleStateImageBehavior = false;
            // 
            // ServerConnections
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 167);
            this.Controls.Add(this.lv_clients);
            this.Controls.Add(this.b_disconnect);
            this.Controls.Add(this.b_Close);
            this.Name = "ServerConnections";
            this.ShowIcon = false;
            this.Text = "Clients Connected";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button b_Close;
        private System.Windows.Forms.Button b_disconnect;
        private System.Windows.Forms.ListView lv_clients;
    }
}