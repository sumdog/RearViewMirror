using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using AForge.Video;
using AForge.Video.DirectShow;
using MJPEGServer;
using motion;

namespace RearViewMirror
{
    [Serializable]
    class CameraVideoSource : VideoSourceBase
    {

        #region Video Server Variables

        //Local cameras support having a video server

        private VideoServer videoServer;

        private ServerConnections connectionsWindow;

        private StringCollection recentURLs;

        private const int RECENT_URL_LIMIT = 10;

        #endregion

        public CameraVideoSource(String name, VideoCaptureDevice d) : base(name, d)
        {
            
        }

        #region Camera Event Handlers

        protected override void camera_NewFrame(object sender, EventArgs e) 
        {            
            if (videoServer != null)
            {
                videoServer.sendFrame(camera.LastRawFrame);
            }
            
            
            base.camera_NewFrame(sender, e);
        }

        #endregion


        public override void updateContextMenu()
        {
            base.updateContextMenu();
            //server setup
            /*
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
            }*/
        }
    }

    [Serializable]
    class MJPEGStreamVideoSource : VideoSourceBase
    {
        public MJPEGStreamVideoSource(String name, MJPEGStream s) : base(name,s)
        {
        }
    
    }


    [Serializable]
    abstract class VideoSourceBase
    {

        protected Viewer view;

        protected IVideoSource captureDevice;

        protected IMotionDetector detector;

        protected Camera camera;

        protected uint detectorType;

        protected Opacity opacityConfig;

        protected String sourceName;

        #region ContextMenu

        protected ToolStripMenuItem miMain; //+
        protected ToolStripMenuItem miDeviceStatus;
        protected ToolStripMenuItem miEnableAlert; //+
        protected ToolStripMenuItem miShowViewer;
        protected ToolStripMenuItem miOpacity;
        protected ToolStripMenuItem miDetectorType;
        protected ToolStripMenuItem miDetectorTypeNone;
        protected ToolStripMenuItem miDetectorTypeBasic;
        protected ToolStripMenuItem miDetectorTypeOutline;
        protected ToolStripMenuItem miDetectorTypeBlock;
        protected ToolStripMenuItem miDetectorTypeFastBlock;
        protected ToolStripMenuItem miDetectorTypeBox;

        private void InitalizeToolstrip()
        {
            miMain = new ToolStripMenuItem(sourceName);
            
            miDeviceStatus = new ToolStripMenuItem("Start Camera", null);
            miMain.DropDown.Items.Add(miDeviceStatus);
            miMain.DropDown.Items.Add(new ToolStripSeparator());

            miEnableAlert = new ToolStripMenuItem("Enable Alert", null);
            miMain.DropDown.Items.Add(miEnableAlert);
            miShowViewer = new ToolStripMenuItem("Show Viewer", null);
            miMain.DropDown.Items.Add(miShowViewer);
            miOpacity = new ToolStripMenuItem("Set Opacity", null);
            miMain.DropDown.Items.Add(miOpacity);
            miDetectorType = new ToolStripMenuItem("Detector Type", null);
            miMain.DropDown.Items.Add(miDetectorType);

            miDetectorTypeNone = new ToolStripMenuItem("None", null, detectorNone_Click);
            miDetectorType.DropDown.Items.Add(miDetectorTypeNone);
            miDetectorTypeBasic = new ToolStripMenuItem("Basic",null,detectorBasic_Click);
            miDetectorType.DropDown.Items.Add(miDetectorTypeBasic);
            miDetectorTypeOutline = new ToolStripMenuItem("Outline",null,detectorOutline_Click);
            miDetectorType.DropDown.Items.Add(miDetectorTypeOutline);
            miDetectorTypeBlock = new ToolStripMenuItem("Block",null,detectorBlock_Click);
            miDetectorType.DropDown.Items.Add(miDetectorTypeBlock);
            miDetectorTypeFastBlock = new ToolStripMenuItem("Block (Optimized)",null,detectorBetterBlock_Click);
            miDetectorType.DropDown.Items.Add(miDetectorTypeFastBlock);
            miDetectorTypeBox = new ToolStripMenuItem("Box",null,detectorBox_Click);
            miDetectorType.DropDown.Items.Add(miDetectorTypeBox);

            
        }

        public ToolStripMenuItem ContextMenu
        {
            get
            {
                return miMain;
            }
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

        public VideoSourceBase(String name,IVideoSource source)
        {
            //establish source
            captureDevice = source;
            sourceName = name;

            //initalize values
            detector = null;
            detectorType = 0;

            //setup viewing window 
            view = new Viewer();
            view.Hide();
            opacityConfig = new Opacity(view);
        }

        #region Camera Event Handlers

        virtual protected void camera_NewFrame(object sender, EventArgs e)
        {

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
        private void loadDetectorType()
        {
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
                detector.MotionLevelCalculation = miEnableAlert.Checked;
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
                detector.MotionLevelCalculation = miEnableAlert.Checked;
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

        #region General Tray Events

        public virtual void updateContextMenu()
        {
            //disable start/stop links if cam is not setup yet
            if (captureDevice == null)
            {
                miDeviceStatus.Enabled = false;
                miDeviceStatus.Text = "Start Camera";
            }
            else if (camera == null)
            {
                miDeviceStatus.Enabled = true;
                miDeviceStatus.Text = "Start Camera";
            }
            else if (camera != null)
            {
                miDeviceStatus.Enabled = true;
                miDeviceStatus.Text = "Stop Camera";
            }

            //stickey bit
            miShowViewer.Checked = view.Stickey;

            //set the correct detector type checkbox
            miDetectorTypeBasic.Checked = false;
            miDetectorTypeFastBlock.Checked = false;
            miDetectorTypeBlock.Checked = false;
            miDetectorTypeOutline.Checked = false;
            miDetectorTypeBox.Checked = false;
            miDetectorTypeNone.Checked = false;

            switch (detectorType)
            {
                case 0:
                    miDetectorTypeNone.Checked = true;
                    break;
                case 1:
                    miDetectorTypeBasic.Checked = true;
                    break;
                case 2:
                    miDetectorTypeOutline.Checked = true;
                    break;
                case 3:
                    miDetectorTypeBlock.Checked = true;
                    break;
                case 4:
                    miDetectorTypeFastBlock.Checked = true;
                    break;
                case 5:
                    miDetectorTypeBox.Checked = true;
                    break;
            }

        }

        #endregion
    }
}
