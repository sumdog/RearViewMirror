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
using RearViewMirror;
using motion;

namespace RearViewMirror
{
    public partial class Viewer : Form
    {

        private Boolean stickey;

        private Boolean globalStickey;

        private Timer timer;

        private uint alarmInterval;

        /// <summary>
        /// sets time remaining for alarm window in seconds. 
        /// </summary>
        public uint AlarmInterval
        {
            set 
            { 
                alarmInterval = (uint)(value * (1000 / timer.Interval));
            }
        }

        public Boolean ShowAll
        {
            get { return globalStickey; }
            set
            {
                changeViewState(ref globalStickey,value);
            }
        }

        /// <summary>
        /// property for window sticky bit. When set, causes window to display or stay open.
        /// </summary>
        public Boolean Stickey
        {
            get
            { return stickey; }
            set
            {
                changeViewState(ref stickey, value);
            }
        }

        private void changeViewState(ref bool s, bool newVal) {
            s = newVal;
            if (s && !Visible && Camera != null) { Show(); }
            else if(!s && Visible) 
            {
                this.Hide();//alarm interval and both stickys are taken care of in Hide();
            }
        }

        public Viewer()
        {
            InitializeComponent();

            //initalize values
            alarmInterval = 0;
            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 100;
            timer.Start();
        }

        //call back for timer which is used to display
        //alarm window on motion detection. The timer 
        //interval is 1 sec, and alarmInterval is increased 
        //by 5 sec by alarm callback
        void timer_Tick(object sender, EventArgs e)
        {
            if (alarmInterval > 0)
            {
                if (!Visible)
                {
                    this.Show();
                }
                alarmInterval--;
            }
            else
            {
                this.Hide();
            }
        }

        /// <summary>
        /// Places the viewer window in the top right corner of the screen.
        /// </summary>
        public void moveToTopRight()
        {
            Size s = SystemInformation.PrimaryMonitorSize;
            Location = new Point(s.Width-Width,0);
        }

        /// <summary>
        /// prevents the viewer from stealing focus
        /// </summary>
        protected override bool ShowWithoutActivation
        {
            get
            {
                return true;
            }
        }
        
        /// <summary>
        /// property to access Camera within viewer
        /// </summary>
        public Camera Camera
        {
            get
            {
                return cameraWindow.Camera;
            }
            set
            {
                cameraWindow.Camera = value;
                
            }
        }


        /// <summary>
        /// override the base Hide function to handle the sticky bit
        /// </summary>
        new public void Hide()
        {
            if (Camera == null || 
                (alarmInterval == 0 && !stickey && !globalStickey) )
            {
                base.Hide();
            }
        }

    }
}