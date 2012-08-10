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

        public enum DetectorType { None,Basic,Outline,Block,FastBlock,Box };

        public enum CameraState { Stopped, Started };

        private Viewer view;

        private IVideoSource captureDevice;

        private IMotionDetector detector;

        private Camera camera;

        private DetectorType detectorType;

        private CameraState savedState;

        private AlertEvents alertEvents;

        private VideoFeedOptions options;

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
            miEnableAlert.Checked = true;

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

        #region Properties (Used for Serialization)

        /// <summary>
        /// Property representing a CameraDevice. If set, this will overwrite the StreamSource. 
        /// If the StreamSource is set, this will return null.
        /// </summary>
        public VideoCaptureDevice CameraDevice
        {
            get
            {
                return (captureDevice is VideoCaptureDevice) ? (VideoCaptureDevice)captureDevice : null;
            }
            set { captureDevice = value; }

        }

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
        /// Property representing a StreamSource. If set, this will overwrite the CameraDevice
        /// If the CameraDevice is set, this will return null.
        /// </summary>
        public MJPEGStream StreamSource
        {
            get { return (captureDevice is MJPEGStream) ? (MJPEGStream)captureDevice : null; }
            set { captureDevice = value; }
        }

        /// <summary>
        /// If set, window to automatically pops-up if motion detector is specified
        /// </summary>
        public bool EnableAlert
        {
            get { return miEnableAlert.Checked; }
            set { miEnableAlert.Checked = value; }
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
        private ToolStripMenuItem miEnableAlert;
        private ToolStripMenuItem miShowViewer;
        private ToolStripMenuItem miOptions;
        private ToolStripMenuItem miDetectorType;
        private ToolStripMenuItem miDetectorTypeNone;
        private ToolStripMenuItem miDetectorTypeBasic;
        private ToolStripMenuItem miDetectorTypeOutline;
        private ToolStripMenuItem miDetectorTypeBlock;
        private ToolStripMenuItem miDetectorTypeFastBlock;
        private ToolStripMenuItem miDetectorTypeBox;
        private ToolStripMenuItem miRemoveDevice;

        private void InitalizeToolstrip()
        {
            miMain = new ToolStripMenuItem();

            miDeviceStatus = new ToolStripMenuItem("-", null, miStatusMenuItem_Click);
            miMain.DropDown.Items.Add(miDeviceStatus);
            miRemoveDevice = new ToolStripMenuItem("Remove Device", null, miRemoveDeviceMenuItem_Click);
            miMain.DropDown.Items.Add(miRemoveDevice);
            miOptions = new ToolStripMenuItem("Options", null, miOptionsMenuItem_Click);
            miMain.DropDown.Items.Add(miOptions);
            miMain.DropDown.Items.Add(new ToolStripSeparator());            

            miEnableAlert = new ToolStripMenuItem("Enable Alert", null,miEnableAlertMenuItem_Click);
            miMain.DropDown.Items.Add(miEnableAlert);
            miShowViewer = new ToolStripMenuItem("Show Viewer", null,miShowViewerMenuItem_Click);
            miMain.DropDown.Items.Add(miShowViewer);
            miDetectorType = new ToolStripMenuItem("Detector Type");
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

        private void detectorBasic_Click(object sender, EventArgs e)
        {
            detectorType = DetectorType.Basic;
            loadDetectorType();
        }


        private void detectorOutline_Click(object sender, EventArgs e)
        {
            detectorType = DetectorType.Outline;
            loadDetectorType();
        }

        private void detectorBlock_Click(object sender, EventArgs e)
        {
            detectorType = DetectorType.Block;
            loadDetectorType();
        }

        private void detectorBetterBlock_Click(object sender, EventArgs e)
        {
            detectorType = DetectorType.FastBlock;
            loadDetectorType();
        }

        private void detectorBox_Click(object sender, EventArgs e)
        {
            detectorType = DetectorType.Box;
            loadDetectorType();
        }

        private void detectorNone_Click(object sender, EventArgs e)
        {
            detectorType = DetectorType.None;
            loadDetectorType();
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
                detector.MotionLevelCalculation = miEnableAlert.Checked;
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
                case DetectorType.None:
                    miDetectorTypeNone.Checked = true;
                    break;
                case DetectorType.Basic:
                    miDetectorTypeBasic.Checked = true;
                    break;
                case DetectorType.Outline:
                    miDetectorTypeOutline.Checked = true;
                    break;
                case DetectorType.Block:
                    miDetectorTypeBlock.Checked = true;
                    break;
                case DetectorType.FastBlock:
                    miDetectorTypeFastBlock.Checked = true;
                    break;
                case DetectorType.Box:
                    miDetectorTypeBox.Checked = true;
                    break;
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

        private void miEnableAlertMenuItem_Click(object sender, EventArgs e)
        {
            miEnableAlert.Checked = !miEnableAlert.Checked;
            if (detector != null)
            {
                detector.MotionLevelCalculation = miEnableAlert.Checked;
            }
            //if it's open, cut it off
            if (!miEnableAlert.Checked)
            {
                view.AlarmInterval = 0;
            }
        }

        private void miShowViewerMenuItem_Click(object sender, EventArgs e)
        {
            view.Stickey = !view.Stickey;
        }


        private void miRemoveDeviceMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Are you sure you want to remove the Camera " + this.Name + "?", "Remove Camera", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
