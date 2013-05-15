/*
 * RearViewMirror - Sumit Khanna <sumit@penguindreams.org>
 * Copyleft 2007-2011, Some rights reserved
 * http://penguindreams.org/projects/rearviewmirror
 * 
 * Based on work by Andrew Kirillov:
 * http://code.google.com/p/aforge/
 * http://www.codeproject.com/KB/audio-video/Motion_Detection.aspx
 * 
 * 
    This file is part of RearViewMirror.

    Foobar is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Foobar is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
 *  
 */
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using AForge.Video;
using AForge.Video.DirectShow;
using MJPEGServer;
using motion;
using System.Xml.Serialization;

namespace RearViewMirror
{

    [Serializable]
    public class VideoSource
    {

        public enum DetectorType { None ,Basic,Outline,Block,FastBlock,Box };

        public enum CameraState { Stopped, Started };

        private Viewer view;

        private IVideoSource captureDevice;

        private IMotionDetector detector;

        private Camera camera;

        private DetectorType detectorType;

        private CameraState savedState;

        private AlertEvents alertEvents;

        private VideoFeedOptions options;

        private bool enableAlert;

        /// <summary>
        /// Default constructor for serialization. Not useful to call directly. 
        /// </summary>
        public VideoSource() : this("default",null)
        {
            //default constructor for serialization

        }

        public VideoSource(String name, IVideoSource source)
        {

            //establish source
            captureDevice = source;

            //setup system tray menu
            InitalizeToolstrip();
            miMain.Text = name;

            //initalize default detector
            detectorType = DetectorType.FastBlock;
            loadDetectorType();

            //setup viewing window 
            view = new Viewer();
            view.Text = name;

            //you have to set this to be able to move the viewer programatically
            view.StartPosition = FormStartPosition.Manual;
            view.moveToTopRight();
            view.Hide();

            view.moveToTopRight();

            //defaults
            enableAlert = true;

        }

        /// <summary>
        /// Indicates the application wants all view windows visible.
        /// Sets an indicator that all views should be visible. If set to
        /// false and an individual viewer is still set to be "stickey" it
        /// will still be visible. This is not a property to avoid 
        /// serialization issues
        /// </summary>
        /// <param name="b">set to current global "Show All" status</param>
        public void setViewerGlobalStickey(bool b)
        { view.ShowAll = b; }



        #region Devices

        /// <summary>
        /// Property representing a CameraDevice. If set, this will overwrite the StreamSource. 
        /// If the StreamSource is set, this will return null.
        /// </summary>
        [XmlIgnore]
        public VideoCaptureDevice CameraDevice
        {
            get
            { return (captureDevice is VideoCaptureDevice) ? (VideoCaptureDevice)captureDevice : null; }
            set { captureDevice = value; }

        }

        /// <summary>
        /// Property representing a StreamSource. If set, this will overwrite the CameraDevice
        /// If the CameraDevice is set, this will return null.
        /// </summary>
        [XmlIgnore]
        public MJPEGStream StreamSource
        {
            get { return (captureDevice is MJPEGStream) ? (MJPEGStream)captureDevice : null; }
            set { captureDevice = value; }
        }

        /// <summary>
        /// Newer versions of AForge do not have Serializable capture devices. 
        /// This property is so that saved devices can be recreated corectly on startup.
        /// </summary>
        public string SerializeddDeviceString
        {
            get
            { 
                return (captureDevice != null) ? captureDevice.Source : null;  
            }
            set
            {
                if(value.StartsWith("http")) {
                    //MJPEG Stream
                    captureDevice = new MJPEGStream(value);
                }
                else
                {
                    captureDevice = new VideoCaptureDevice(value);
                }
            }
        }



        #endregion

        #region Properties (Used for Serialization)


        /// <summary>
        /// Options for video feed and viewer
        /// </summary>
        public VideoFeedOptions Options { 
            get {
                if (options != null)
                {
                    return options;
                }
                else
                {
                    VideoFeedOptions o = new VideoFeedOptions();
                    o.VideoSource = this;
                    alertEvents = new AlertEvents(o);
                    options = o;
                    return o;
                }
            }
            set { 
                options = value;
                options.VideoSource = this;
                if (alertEvents == null)
                {
                    alertEvents = new AlertEvents(options);
                }
            } 
        }



        /// <summary>
        /// If set, window to automatically pops-up if motion detector is specified
        /// </summary>
        public bool EnableAlert
        {
            get { return enableAlert; }
            set { 
                enableAlert = value;

                if (detector != null)
                {
                    detector.MotionLevelCalculation = enableAlert;
                }
                if (!enableAlert)
                {
                    view.AlarmInterval = 0;
                    alertEvents.AlarmInterval = 0;
                }
            }
        }

        /// <summary>
        /// If set, window will always display event without an alert.
        /// Can be overridden by Global Stickey
        /// </summary>
        public bool Sticky
        {
            get { return view.Stickey; }
            set { view.Stickey = value; }
        }

        /// <summary>
        /// Viewer window's transparency
        /// </summary>
        public double ViewerOpacity
        {
            get { return view.Opacity; }
            set { view.Opacity = value; }
        }

        /// <summary>
        /// Motion detector type
        /// </summary>
        public DetectorType Detector
        {
            get { return detectorType; }
            set
            {
                detectorType = value;
                loadDetectorType();
            }
        }

        /// <summary>
        /// Name given to video source to display in menu
        /// </summary>
        public String Name
        {
            get { return miMain.Text; }
            //TODO: There has got to be a better way to do this; this won't always work
            set { miMain.Text = value; if (view != null) { view.Text = value; } }
        }


        /// <summary>
        /// Viewer's Window Position
        /// </summary>
        public Point Location
        {
            get { return view.Location; }
            set { view.Location = value; }
        }

        /// <summary>
        /// Indicates if the Camera is currently running
        /// </summary>
        public CameraState CurrentState
        {
            get { return (camera == null) ? CameraState.Stopped : CameraState.Started; }
        }

        /// <summary>
        /// Should be set before applicate closes to save
        /// the running state of the Camera on Serizilation
        /// </summary>
        public CameraState SaveState 
        {
            get { return savedState; }
            set { savedState = value; }
        }


        #endregion

        #region ContextMenu

        private ToolStripMenuItem miMain;
        private ToolStripMenuItem miDeviceStatus;
        private ToolStripMenuItem miOptions;
        private ToolStripMenuItem miRemoveDevice;

        private void InitalizeToolstrip()
        {
            miMain = new ToolStripMenuItem();

            miDeviceStatus = new ToolStripMenuItem("-", null, miStatusMenuItem_Click);
            miMain.DropDown.Items.Add(miDeviceStatus);
            miRemoveDevice = new ToolStripMenuItem(Messages.RemoveDevice, null, miRemoveDeviceMenuItem_Click);
            miMain.DropDown.Items.Add(miRemoveDevice);
            miOptions = new ToolStripMenuItem(Messages.Options, null, miOptionsMenuItem_Click);
            miMain.DropDown.Items.Add(miOptions);           
            
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

        /// <summary>
        /// should be called by any function that changes the detectorType
        /// to load the correct Detector and start it.
        /// </summary>
        private void loadDetectorType()
        {
            switch (detectorType)
            {
                case DetectorType.None:
                    detector = null;
                    break;
                case DetectorType.Basic:
                    detector = new MotionDetector1();
                    break;
                case DetectorType.Outline:
                    detector = new MotionDetector2();
                    break;
                case DetectorType.Block:
                    detector = new MotionDetector3();
                    break;
                case DetectorType.FastBlock:
                    detector = new MotionDetector3Optimized();
                    break;
                case DetectorType.Box:
                    detector = new MotionDetector4();
                    break;
            }

            // enable/disable motion alarm
            if (detector != null)
            {
                detector.MotionLevelCalculation = EnableAlert;
            }

            // set motion detector to camera
            if (camera != null)
            {
                camera.Lock();
                camera.MotionDetector = detector;

                camera.Unlock();
            }
        }

        #endregion

        #region Camera Event Handlers

        private void camera_NewFrame(object sender, EventArgs e)
        {
            if (VideoServer.Instance != null && camera != null)
            {
                VideoServer.Instance.sendFrame(camera.LastRawFrame, Name);
            }
            if (alertEvents != null && camera != null)
            {
                alertEvents.sendFrame(camera.LastFrame, Name);
            }
        }

        private void cameraAlert(object sender, EventArgs e)
        {
            // keep displaying window for three seconds after we stop
            //TODO: make into an option with a default of 3
            view.AlarmInterval = 3;

            if (alertEvents != null)
            {
                alertEvents.AlarmInterval = 3;
            }
        }

        #endregion

        #region Camera Start/Stop Functions

        public void startCamera()
        {
            // enable/disable motion alarm
            if (detector != null)
            {
                detector.MotionLevelCalculation = EnableAlert;
            }

            camera = new Camera(captureDevice, detector);
            camera.Start();

            camera.Alarm += new EventHandler(cameraAlert);
            camera.NewFrame += new EventHandler(camera_NewFrame);

            // attach camera to camera window
            view.Camera = camera;

            //show if we're suposte to
            if (view.ShowAll || view.Stickey)
            {
                view.Show();
            }
        }

        public void stopCamera()
        {
            if (camera != null)
            {
                camera.SignalToStop();
                //This is where we freze at on exit
                //  --gotta be a fix for this
                //camera.WaitForStop();
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

        #region Tray Menu Events

        /// <summary>
        /// This must be called by system try to update the context menu for this 
        /// video source before it is displayed. 
        /// </summary>
        public void updateContextMenu()
        {
            //disable start/stop links if cam is not setup yet
            if (captureDevice == null)
            {
                miDeviceStatus.Enabled = false;
                miDeviceStatus.Text = Messages.MenuCameraStart;
            }
            else if (camera == null)
            {
                miDeviceStatus.Enabled = true;
                miDeviceStatus.Text = Messages.MenuCameraStop;
            }
            else if (camera != null)
            {
                miDeviceStatus.Enabled = true;
                miDeviceStatus.Text = Messages.MenuCameraStop;
            }

        }

        private void miStatusMenuItem_Click(object sender, EventArgs e)
        {
            if (camera == null)
            {
                startCamera();
            }
            else
            {
                stopCamera();
            }
        }

        public void miOptionsMenuItem_Click(object sender, EventArgs e)
        {
            new OptionsForm(Options).ShowDialog();
        }


        private void miRemoveDeviceMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show(String.Format(Messages.RemoveCameraQuestion,this.Name), Messages.MenuRemoveCamera, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                RemoveSelected(this);
            }
        }


        #endregion

        #region Event Callback for Remove Device

        public delegate void RemoveEventHandler(object source);

        public event RemoveEventHandler RemoveSelected;

        #endregion

    }
}
