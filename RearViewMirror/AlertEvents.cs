using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace RearViewMirror
{
    class AlertEvents
    {
        private uint alarmInterval;

        private VideoSource videoSource;

        private Timer timer;

        /// <summary>
        /// sets time remaining for motion alarm in seconds. 
        /// </summary>
        public uint AlarmInterval
        {
            set
            {
                alarmInterval = (uint)(value * (1000 / timer.Interval));
            }
        }

        public AlertEvents(VideoSource v)
        {
            //initalize values
            videoSource = v;
            alarmInterval = 0;
            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 100;
            timer.Start();
        }

        public void sendFrame(Bitmap frame, string Name) {
            if (alarmInterval > 0)
            {
            }
        }

        //call back for timer which is used to display
        //alarm window on motion detection. The timer 
        //interval is 1 sec, and alarmInterval is increased 
        //by 5 sec by alarm callback
        void timer_Tick(object sender, EventArgs e)
        {
            if (alarmInterval > 0)
            {
                alarmInterval--;
            }
        }

    }
}
