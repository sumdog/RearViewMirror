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

        private VideoCaptureDevice captureDevice;

        private IMotionDetector detector;

        private Camera camera;

        private uint detectorType;

        private Opacity opacityConfig;

        private VideoServer videoServer;


        public SystemTray()
        {
            InitializeComponent();
            this.Resize += SystemTray_Resize;

            //set video server
            videoServer = new VideoServer(80);

            //initalize values
            detector = null;
            detectorType = 0;

            //setup viewing window 
            view = new Viewer();
            view.Hide();
            opacityConfig = new Opacity(view);

            //**load settings**

            captureDevice = Properties.Settings.Default.CaptureDevice;
            enableAlarmToolStripMenuItem.Checked = Properties.Settings.Default.enabled;
            view.Opacity = Properties.Settings.Default.opacity;

            //load detector type
            detectorType = Properties.Settings.Default.detector;
            loadDetectorType();

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

            //if we were running previously, see if we can start back up
            if (Properties.Settings.Default.running && captureDevice != null)
            {
                startDetectorToolStripMenuItem_Click(null, null);
            }

        }

        void camera_NewFrame(object sender, EventArgs e)
        {
            videoServer.sendFrame(camera.LastRawFrame);
        }

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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //save this state before we kill the camera
            Properties.Settings.Default.running = (camera != null);

            //stop the camera
            stopDetectorToolStripMenuItem_Click(null, null);

            //save our settings
            Properties.Settings.Default.viewer_x = view.Location.X;
            Properties.Settings.Default.viewer_y = view.Location.Y;
            Properties.Settings.Default.detector = detectorType;
            Properties.Settings.Default.enabled = enableAlarmToolStripMenuItem.Checked;
            Properties.Settings.Default.CaptureDevice = captureDevice;
            Properties.Settings.Default.opacity = view.Opacity;
            Properties.Settings.Default.Save();

            Application.Exit();
        }

        private void trayContextMenu_Opening(object sender, CancelEventArgs e)
        {
            //disable start/stop links if cam is not setup yet
            if (captureDevice == null)
            {
                startDetectorToolStripMenuItem.Enabled = false;
                stopDetectorToolStripMenuItem.Enabled = false;
            }
            else if(camera == null)
            {
                startDetectorToolStripMenuItem.Enabled = true;
                stopDetectorToolStripMenuItem.Enabled = false;
            }
            else if (camera != null)
            {
                startDetectorToolStripMenuItem.Enabled = false;
                stopDetectorToolStripMenuItem.Enabled = true;
            }

            //stickey vit
            showViewerToolStripMenuItem.Checked = view.Stickey;           

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

        private void startDetectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // enable/disable motion alarm
            if (detector != null)
            {
                detector.MotionLevelCalculation = enableAlarmToolStripMenuItem.Checked;
            }

            camera = new Camera(captureDevice, detector);
            camera.Start();

            camera.Alarm += new EventHandler( cameraAlert );
            camera.NewFrame += new EventHandler(camera_NewFrame);

            // attach camera to camera window
            view.Camera = camera;

        }

        private void cameraAlert(object sender, EventArgs e)
        {
            // keep displaying window for three seconds after we stop
            view.AlarmInterval = 3;
        }

        private void stopDetectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (camera != null)
            {
                // detach camera from camera window
                view.Camera = null;

                camera.SignalToStop();
                camera.WaitForStop();

                camera = null;

                if (detector != null)
                    detector.Reset();
            }

            view.AlarmInterval = 0;
            view.Hide();
        }

        //-----Start Detector Type Subcontext Menu Events-----

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

        //-----End Detector Type Subcontext Menu-----

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

        private void showViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            view.Stickey = !view.Stickey;
        }


    }
}