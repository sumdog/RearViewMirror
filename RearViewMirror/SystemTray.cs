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

        private Viewer view;

        private IVideoSource captureDevice;

        private IMotionDetector detector;

        private Camera camera;

        private uint detectorType;

        private Opacity opacityConfig;

        private VideoServer videoServer;

        private ServerConnections connectionsWindow;

        private StringCollection recentURLs;

        private const int RECENT_URL_LIMIT = 10;

        public SystemTray()
        {
            InitializeComponent();
            this.Resize += SystemTray_Resize;

            //initalize values
            detector = null;
            detectorType = 0;

            //setup viewing window 
            view = new Viewer();
            view.Hide();
            opacityConfig = new Opacity(view);

            //**load settings**

            //setup capture device from saved settings
            VideoCaptureDevice vc = Properties.Settings.Default.CaptureDevice;
            MJPEGStream mj = Properties.Settings.Default.CaptureStream;
            if (vc != null && mj == null)
            {
                captureDevice = vc;
            }
            else if (vc == null && mj != null)
            {
                captureDevice = mj;
            }
            
            //misc settings
            enableAlarmToolStripMenuItem.Checked = Properties.Settings.Default.enabled;
            view.Opacity = Properties.Settings.Default.opacity;
            showViewerToolStripMenuItem.Checked = Properties.Settings.Default.showViewer;

            //load detector type
            detectorType = Properties.Settings.Default.detector;
            loadDetectorType();

            //previous URLs for MJPEG streams
            recentURLs = Properties.Settings.Default.recentURLs;
            if (recentURLs == null) { recentURLs = new StringCollection(); }

            //set the location of the viewer
            int x = Properties.Settings.Default.viewer_x;
            int y = Properties.Settings.Default.viewer_y;
            view.StartPosition = FormStartPosition.Manual; //without this you can't change Location
            if (x == -1 || y == -1)
            {
                view.moveToTopRight();
            }
            else
            {
                view.Location = new Point(x, y);
            }

            //setup the server
            videoServer = new VideoServer(Properties.Settings.Default.serverPort);
            if (Properties.Settings.Default.serverRunning)
            {  videoServer.startServer(); }
            connectionsWindow = new ServerConnections(videoServer);

            //if we were running previously, see if we can start back up
            if (Properties.Settings.Default.running && captureDevice != null)
            {
                startCapture();
            }

        }



        #region Camera Event Handlers

        void camera_NewFrame(object sender, EventArgs e)
        {
            videoServer.sendFrame(camera.LastRawFrame);
        }

        private void cameraAlert(object sender, EventArgs e)
        {
            // keep displaying window for three seconds after we stop
            view.AlarmInterval = 3;
        }

        #endregion

        #region General Functions 

        /// <summary>
        /// should be called by any function that changes the detectorType
        /// to load the correct Detector and start it.
        /// </summary>
        private void loadDetectorType() {
            switch (detectorType)
            {
                case 0:
                    detector = null;
                    break;
                case 1:
                    detector = new MotionDetector1();
                    break;
                case 2:
                    detector = new MotionDetector2();
                    break;
                case 3:
                    detector = new MotionDetector3();
                    break;
                case 4:
                    detector = new MotionDetector3Optimized();
                    break;
                case 5:
                    detector = new MotionDetector4();
                    break;
            }

            // enable/disable motion alarm
            if (detector != null)
            {
                detector.MotionLevelCalculation = enableAlarmToolStripMenuItem.Checked;
            }

            // set motion detector to camera
            if (camera != null)
            {
                camera.Lock();
                camera.MotionDetector = detector;

                camera.Unlock();
            }
        }

        private void startCapture()
        {
            // enable/disable motion alarm
            if (detector != null)
            {
                detector.MotionLevelCalculation = enableAlarmToolStripMenuItem.Checked;
            }

            camera = new Camera(captureDevice, detector);
            camera.Start();

            camera.Alarm += new EventHandler(cameraAlert);
            camera.NewFrame += new EventHandler(camera_NewFrame);

            // attach camera to camera window
            view.Camera = camera;
        }

        private void stopCapture()
        {
            if (camera != null)
            {
                camera.SignalToStop();
                camera.WaitForStop();
                camera = null;
            }

            // detach camera from camera window
            view.Camera = null;

            if (detector != null)
            {
                detector.Reset();
            }


            view.AlarmInterval = 0;
            view.Hide();
        }

        #endregion

        #region Detector Type Subcontext Menu Events

        private void detectorBasic_Click(object sender, EventArgs e)
        {
            detectorType = 1;
            loadDetectorType();
        }


        private void detectorOutline_Click(object sender, EventArgs e)
        {
            detectorType = 2;
            loadDetectorType();
        }

        private void detectorBlock_Click(object sender, EventArgs e)
        {
            detectorType = 3;
            loadDetectorType();
        }

        private void detectorBetterBlock_Click(object sender, EventArgs e)
        {
            detectorType = 4;
            loadDetectorType();
        }

        private void detectorBox_Click(object sender, EventArgs e)
        {
            detectorType = 5;
            loadDetectorType();
        }

        private void detectorNone_Click(object sender, EventArgs e)
        {
            detectorType = 0;
            loadDetectorType();
        }

        #endregion

        #region General Tray Events

        private void trayContextMenu_Opening(object sender, CancelEventArgs e)
        {
            //disable start/stop links if cam is not setup yet
            if (captureDevice == null)
            {
                startStopDetectorToolStripMenuItem.Enabled = false;
                startStopDetectorToolStripMenuItem.Text = "Start Detector";
            }
            else if (camera == null)
            {
                startStopDetectorToolStripMenuItem.Enabled = true;
                startStopDetectorToolStripMenuItem.Text = "Start Detector";
            }
            else if (camera != null)
            {
                startStopDetectorToolStripMenuItem.Enabled = true;
                startStopDetectorToolStripMenuItem.Text = "Stop Detector";
            }

            //stickey bit
            showViewerToolStripMenuItem.Checked = view.Stickey;

            //server setup
            portToolStripMenuItem.Text = "Port: " + videoServer.Port;
            connectionsToolStripMenuItem.Text = "Connections: " + videoServer.NumberOfConnectedUsers;
            if (videoServer.State == VideoServer.ServerState.STARTED)
            {
                startServerToolStripMenuItem.Enabled = false;
                stopServerToolStripMenuItem.Enabled = true;
                portToolStripMenuItem.Enabled = false;
                connectionsToolStripMenuItem.Enabled = true;
            }
            else
            {
                startServerToolStripMenuItem.Enabled = true;
                stopServerToolStripMenuItem.Enabled = false;
                portToolStripMenuItem.Enabled = true;
                connectionsToolStripMenuItem.Enabled = false;
            }

            //set the correct detector type checkbox
            detectorBasic.Checked = false;
            detectorBetterBlock.Checked = false;
            detectorBlock.Checked = false;
            detectorOutline.Checked = false;
            detectorBox.Checked = false;
            detectorNone.Checked = false;

            switch (detectorType)
            {
                case 0:
                    detectorNone.Checked = true;
                    break;
                case 1:
                    detectorBasic.Checked = true;
                    break;
                case 2:
                    detectorOutline.Checked = true;
                    break;
                case 3:
                    detectorBlock.Checked = true;
                    break;
                case 4:
                    detectorBetterBlock.Checked = true;
                    break;
                case 5:
                    detectorBox.Checked = true;
                    break;
            }

        }

        #endregion

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
            if (e.Button == MouseButtons.Left && e.Clicks == 0)
            {
                view.Stickey = !view.Stickey;
            }
        }



        #region Video Device Selection SubMenu Events

        private void selectDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CaptureDeviceForm form = new CaptureDeviceForm();
            form.StartPosition = FormStartPosition.CenterScreen;

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                // create video source
                captureDevice = new VideoCaptureDevice();
                captureDevice.Source = form.Device;
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
                captureDevice = new MJPEGStream();
                captureDevice.Source = form.URL;
            }
        }

        #endregion

        #region Main TrayIcon Menu Events

        private void startStopDetectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (camera == null)
            {
                startCapture();
            }
            else
            {
                stopCapture();
            }
        }

        private void enableAlarmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            enableAlarmToolStripMenuItem.Checked = !enableAlarmToolStripMenuItem.Checked;
            if (detector != null)
            {
                detector.MotionLevelCalculation = enableAlarmToolStripMenuItem.Checked;
            }
            //if it's open, cut it off
            if (!enableAlarmToolStripMenuItem.Checked)
            {
                view.AlarmInterval = 0;
            }
        }

        private void showViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            view.Stickey = !view.Stickey;
        }

        private void setOpacityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            opacityConfig.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About a = new About();
            a.StartPosition = FormStartPosition.CenterScreen;
            a.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //save this state before we kill the camera
            Properties.Settings.Default.running = (camera != null);

            //stop the camera
            stopCapture();

            //save the video capture device
            if (captureDevice is MJPEGStream)
            {
                Properties.Settings.Default.CaptureDevice = null;
                Properties.Settings.Default.CaptureStream = (MJPEGStream)captureDevice;
            }
            else if (captureDevice is VideoCaptureDevice)
            {
                Properties.Settings.Default.CaptureDevice = (VideoCaptureDevice)captureDevice;
                Properties.Settings.Default.CaptureStream = null;
            }

            //save our settings
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

            Application.Exit();
        }

        #endregion

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


    }
}