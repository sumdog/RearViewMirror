/*
 * RearViewMirror - Sumit Khanna 
 * http://penguindreams.org
 * 
 * License: GNU GPLv3 - Free to Distribute so long as any 
 *   modifications are released for free as well
 * 
 * Based on work by Andrew Kirillov found at the following address:
 * http://www.codeproject.com/KB/audio-video/Motion_Detection.aspx
 * 
 */
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using MJPEGServer;
using motion;


namespace RearViewMirror
{

    public partial class SystemTray : Form
    {

        private StringCollection recentURLs;

        private const int RECENT_URL_LIMIT = 10;

        private List<VideoSource> sources;

        public SystemTray()
        {
            InitializeComponent();
            this.Resize += SystemTray_Resize;

            sources = new List<VideoSource>();

            //previous URLs for MJPEG streams
            recentURLs = Properties.Settings.Default.recentURLs;
            if (recentURLs == null) { recentURLs = new StringCollection(); }

        }
            

        private void SystemTray_Resize(object sender, System.EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                Hide();
        }

        /// <summary>
        /// Single Mouse click on the tray icon makes the viewer sticky
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            /*if (e.Button == MouseButtons.Left && e.Clicks == 0)
            {
                view.Stickey = !view.Stickey;
            }*/
        }



        #region Video Device Selection SubMenu Events

        private void cameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CaptureDeviceForm form = new CaptureDeviceForm();
            form.StartPosition = FormStartPosition.CenterScreen;

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                // create video source
                VideoCaptureDevice c = new VideoCaptureDevice();
                c.Source = form.Device;

                string strResponse = Microsoft.VisualBasic.Interaction.InputBox(
                    "What would you like to name this camera?", "RearViewMirror : Camera Name", "", 100, 100);

                if (strResponse != null && !strResponse.Trim().Equals(""))
                {
                    VideoSource r = new VideoSource(strResponse, c);
                    sources.Add(r);
                    sourcesToolStripMenuItem.DropDown.Items.Add(r.ContextMenu);
                }
            }
        }

        private void mJPEGStreamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            URLForm form = new URLForm();
            form.Description = "Enter URL of an updating JPEG from a web camera";

            //Load recent URLs
            String[] urls = new String[recentURLs.Count];
            recentURLs.CopyTo(urls, 0);
            form.URLs = urls;

            form.StartPosition = FormStartPosition.CenterScreen;
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                //update recent URLs
                if (recentURLs.Count == RECENT_URL_LIMIT)
                {
                    recentURLs.RemoveAt(RECENT_URL_LIMIT - 1);
                }
                recentURLs.Add(form.URL);

                //open the stream
                MJPEGStream s = new MJPEGStream();
                s.Source = form.URL;
                sources.Add(new VideoSource("test", s));
            }
        }

        #endregion

        #region Main TrayIcon Menu Events


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About a = new About();
            a.StartPosition = FormStartPosition.CenterScreen;
            a.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //save this state before we kill the camera
            //Properties.Settings.Default.running = (camera != null);

            //stop the camera(s)
            foreach (VideoSource v in sources)
            {
                v.stopCamera();
            }

            //save the video capture device
            /*if (captureDevice is MJPEGStream)
            {
                Properties.Settings.Default.CaptureDevice = null;
                Properties.Settings.Default.CaptureStream = (MJPEGStream)captureDevice;
            }
            else if (captureDevice is VideoCaptureDevice)
            {
                Properties.Settings.Default.CaptureDevice = (VideoCaptureDevice)captureDevice;
                Properties.Settings.Default.CaptureStream = null;
            }*/

            //save our settings
            /*
            Properties.Settings.Default.viewer_x = view.Location.X;
            Properties.Settings.Default.viewer_y = view.Location.Y;
            Properties.Settings.Default.detector = detectorType;
            Properties.Settings.Default.enabled = enableAlarmToolStripMenuItem.Checked;
            Properties.Settings.Default.showViewer = showViewerToolStripMenuItem.Checked;
            Properties.Settings.Default.opacity = view.Opacity;
            Properties.Settings.Default.serverPort = videoServer.Port;
            Properties.Settings.Default.serverRunning = videoServer.State == VideoServer.ServerState.STARTED;
            Properties.Settings.Default.recentURLs = recentURLs;
            Properties.Settings.Default.Save();
            */
            Application.Exit();
        }

        #endregion


        /*
        #region Server SubMenu Events

        private void portToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string boxPort = videoServer.Port.ToString();
            bool invalid = true;

            while (invalid)
            {
                //TODO: Remove this and replace with a real C# Form / remove VB reference
                string strResponse = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter Server Port", "RearViewMirror : Server Port", boxPort, 100, 100);

                //empty string means cancel was clicked
                if (strResponse.Equals("")) { invalid = false; continue;  }

                try
                {
                    videoServer = new VideoServer(Convert.ToInt32(strResponse));
                    Properties.Settings.Default.serverPort = videoServer.Port;
                    invalid = false;
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid Port Number", "Error: Invalid Port", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void startServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                videoServer.startServer();
            }
            catch (InvalidServerStateException se)
            {
                MessageBox.Show(se.Message, "Could not Start Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void stopServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            videoServer.stopServer();
        }

        private void connectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connectionsWindow.Show();
        }

        #endregion
*/

    }
}