namespace RearViewMirror
{
    partial class SystemTray
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SystemTray));
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sourcesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newSourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mjpegToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.portToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cameraURLsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.donateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trayContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // trayIcon
            // 
            this.trayIcon.ContextMenuStrip = this.trayContextMenu;
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "RearViewMirror";
            this.trayIcon.Visible = true;
            this.trayIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.trayIcon_MouseClick);
            // 
            // trayContextMenu
            // 
            this.trayContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sourcesToolStripMenuItem,
            this.newSourceToolStripMenuItem,
            this.videoServerToolStripMenuItem,
            this.showAllToolStripMenuItem,
            this.toolStripSeparator2,
            this.helpToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.trayContextMenu.Name = "trayContextMenu";
            this.trayContextMenu.Size = new System.Drawing.Size(153, 164);
            this.trayContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.trayContextMenu_Opening);
            // 
            // sourcesToolStripMenuItem
            // 
            this.sourcesToolStripMenuItem.Name = "sourcesToolStripMenuItem";
            this.sourcesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.sourcesToolStripMenuItem.Text = "Video Sources";
            // 
            // newSourceToolStripMenuItem
            // 
            this.newSourceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cameraToolStripMenuItem,
            this.mjpegToolStripMenuItem});
            this.newSourceToolStripMenuItem.Name = "newSourceToolStripMenuItem";
            this.newSourceToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newSourceToolStripMenuItem.Text = "New Source";
            // 
            // cameraToolStripMenuItem
            // 
            this.cameraToolStripMenuItem.Name = "cameraToolStripMenuItem";
            this.cameraToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cameraToolStripMenuItem.Text = "Camera";
            this.cameraToolStripMenuItem.Click += new System.EventHandler(this.cameraToolStripMenuItem_Click);
            // 
            // mjpegToolStripMenuItem
            // 
            this.mjpegToolStripMenuItem.Name = "mjpegToolStripMenuItem";
            this.mjpegToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mjpegToolStripMenuItem.Text = "MJPEG Stream";
            this.mjpegToolStripMenuItem.Click += new System.EventHandler(this.mjpegToolStripMenuItem_Click);
            // 
            // videoServerToolStripMenuItem
            // 
            this.videoServerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleServerToolStripMenuItem,
            this.portToolStripMenuItem,
            this.connectionsToolStripMenuItem,
            this.cameraURLsToolStripMenuItem});
            this.videoServerToolStripMenuItem.Name = "videoServerToolStripMenuItem";
            this.videoServerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.videoServerToolStripMenuItem.Text = "Video Server";
            // 
            // toggleServerToolStripMenuItem
            // 
            this.toggleServerToolStripMenuItem.Name = "toggleServerToolStripMenuItem";
            this.toggleServerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.toggleServerToolStripMenuItem.Text = "--";
            this.toggleServerToolStripMenuItem.Click += new System.EventHandler(this.toggleServerToolStripMenuItem_Click);
            // 
            // portToolStripMenuItem
            // 
            this.portToolStripMenuItem.Name = "portToolStripMenuItem";
            this.portToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.portToolStripMenuItem.Text = "Port:";
            this.portToolStripMenuItem.Click += new System.EventHandler(this.portToolStripMenuItem_Click);
            // 
            // connectionsToolStripMenuItem
            // 
            this.connectionsToolStripMenuItem.Name = "connectionsToolStripMenuItem";
            this.connectionsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.connectionsToolStripMenuItem.Text = "Connections:";
            this.connectionsToolStripMenuItem.Click += new System.EventHandler(this.connectionsToolStripMenuItem_Click);
            // 
            // cameraURLsToolStripMenuItem
            // 
            this.cameraURLsToolStripMenuItem.Name = "cameraURLsToolStripMenuItem";
            this.cameraURLsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cameraURLsToolStripMenuItem.Text = "Camera URLs";
            this.cameraURLsToolStripMenuItem.Click += new System.EventHandler(this.cameraURLsToolStripMenuItem_Click);
            // 
            // showAllToolStripMenuItem
            // 
            this.showAllToolStripMenuItem.Name = "showAllToolStripMenuItem";
            this.showAllToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.showAllToolStripMenuItem.Text = "Show All";
            this.showAllToolStripMenuItem.Click += new System.EventHandler(this.showAllToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1,
            this.donateToolStripMenuItem,
            this.logViewerToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // donateToolStripMenuItem
            // 
            this.donateToolStripMenuItem.Name = "donateToolStripMenuItem";
            this.donateToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.donateToolStripMenuItem.Text = "Donate";
            this.donateToolStripMenuItem.Click += new System.EventHandler(this.donateToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // logViewerToolStripMenuItem
            // 
            this.logViewerToolStripMenuItem.Name = "logViewerToolStripMenuItem";
            this.logViewerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.logViewerToolStripMenuItem.Text = "Log Viewer";
            this.logViewerToolStripMenuItem.Click += new System.EventHandler(this.logViewerToolStripMenuItem_Click);
            // 
            // SystemTray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Name = "SystemTray";
            this.ShowInTaskbar = false;
            this.Text = "SystemTray";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.trayContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip trayContextMenu;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newSourceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cameraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mjpegToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sourcesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem videoServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem portToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cameraURLsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem donateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem logViewerToolStripMenuItem;
    }
}