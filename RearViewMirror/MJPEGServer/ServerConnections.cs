using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;



namespace MJPEGServer
{
    public partial class ServerConnections : Form
    {

        private VideoServer videoServer;

        private Timer refreshTimer;

        public ServerConnections(VideoServer v)
        {
            InitializeComponent();
            videoServer = v;
            refreshTimer = new Timer();
            refreshTimer.Interval = 1000; //one second
            refreshTimer.Tick += new EventHandler(refreshTimer_Tick);


            this.FormClosing += new FormClosingEventHandler(ServerConnections_FormClosing);
        }

        void ServerConnections_FormClosing(object sender, FormClosingEventArgs e)
        {
            refreshTimer.Stop();
            e.Cancel = true; //prevents window from being Disposed of
            Hide();
        }

        /// <summary>
        /// Queries the VideoServer object to refresh connected client information.
        /// </summary>
        void refreshTimer_Tick(object sender, EventArgs e)
        {
            System.Windows.Forms.ListBox.ObjectCollection c = lb_clients.Items;
            //lb_clients.Items.Clear();
            /*foreach (String u in videoServer.ConnectedUsers)
            {
                lb_clients.Items.Add(u);
            }*/
        }

        new public void Show()
        {
            refreshTimer.Start();
            base.Show();
        }

        new public void Hide()
        {
            refreshTimer.Stop();
            base.Hide();
        }

        private void b_Close_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}