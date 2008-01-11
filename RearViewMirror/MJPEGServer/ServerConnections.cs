/*
 * RearViewMirror - Sumit Khanna 
 * http://penguindreams.org
 * 
 * License: GNU GPLv3 - Free to Distribute so long as any 
 *   modifications are released for free as well
 */
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

            //setup refresh timer (1sec interval)
            refreshTimer = new Timer();
            refreshTimer.Interval = 1000; //one second
            refreshTimer.Tick += new EventHandler(refreshTimer_Tick);

            //setup list view
            lv_clients.Columns.Add("Client");
            lv_clients.Columns.Add("Connection Time");
            lv_clients.View = View.Details;
            lv_clients.Columns[0].Width = 200;
            lv_clients.Columns[1].Width = 100;
            lv_clients.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(lv_clients_ItemSelectionChanged);
            b_disconnect.Enabled = false;


            //keep form from disposing
            this.FormClosing += new FormClosingEventHandler(ServerConnections_FormClosing);
        }

        void lv_clients_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            //only enable disconnect button if hosts are connected
            b_disconnect.Enabled = (lv_clients.SelectedItems.Count > 0);
        }



        void ServerConnections_FormClosing(object sender, FormClosingEventArgs e)
        {
            refreshTimer.Stop();
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true; //prevents window from being Disposed of
            }
            Hide();
        }

        /// <summary>
        /// Queries the VideoServer object to refresh connected client information.
        /// </summary>
        void refreshTimer_Tick(object sender, EventArgs e)
        {

            lock (lv_clients)
            {
                lv_clients.BeginUpdate();

                ConnectionInformation[] conUsers = videoServer.ConnectedUsers;

                //unmark everything first
                foreach (SocketListItem s in lv_clients.Items)
                {
                    s.Marked = false;
                }

                //if it exists, flag it, else add it
                foreach (ConnectionInformation c in conUsers)
                {
                    if (lv_clients.Items.ContainsKey(c.RemoteHost))
                    {

                        SocketListItem s = (SocketListItem)lv_clients.Items.Find(c.RemoteHost, false)[0];
                        /*s.ConnectionInfo.ConnectionTime = c.ConnectionTime;*/
                        s.Marked = true;
                        s.updateTime();
                    }
                    else
                    {
                        lv_clients.Items.Add(new SocketListItem(c));
                    }
                }

                //delete anything that's still unmarked
                foreach (SocketListItem s in lv_clients.Items)
                {
                    if (!s.Marked)
                    {
                        s.Remove();
                    }
                }

                lv_clients.EndUpdate();
            }
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

        private void b_disconnect_Click(object sender, EventArgs e)
        {
            lock (lv_clients)
            {
                foreach (SocketListItem s in lv_clients.SelectedItems)
                {
                    s.ConnectionInfo.disconnect();
                }
            }
        }
    }

    public class SocketListItem : ListViewItem
    {
        private bool marked;
        private ConnectionInformation info;

        /// <summary>
        /// Can be used when syncronizing window with server connection list.
        /// </summary>
        public bool Marked
        {
            get { return marked; }
            set { marked = value; }
        }

        public ConnectionInformation ConnectionInfo
        {
            get { return info; }
            set { info = value; }
        }

        /// <summary>
        /// pulls the latest connection time from the SocketHandler
        /// </summary>
        public void updateTime()
        {
            String hours = info.ConnectionTime.Hours.ToString().PadLeft(2, '0');
            String mins = info.ConnectionTime.Minutes.ToString().PadLeft(2, '0');
            String secs = info.ConnectionTime.Seconds.ToString().PadLeft(2, '0');
            SubItems[1].Text = hours + ":" + mins + ":" + secs;
        }

        public SocketListItem(ConnectionInformation c) : base(c.RemoteHost)
        {
            info = c;
            marked = true;
            SubItems.Add("00:00:00");
            //The name is actually the "Key" usined in the lv_clients.Item collection
            this.Name = c.RemoteHost;
        }

    }
}