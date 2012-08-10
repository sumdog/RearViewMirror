using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Media;
using MJPEGServer;
using AForge.Video.VFW;
using System.IO;


namespace RearViewMirror
{
    class AlertEvents
    {
        private uint alarmInterval;

        private VideoFeedOptions options;

        private Timer timer;

        private bool recording, audioPlayed;

        private AVIWriter videoWriter;

        private int frameDropThreshold;

        public const int MAX_FRAME_DROP = 10;

        /// <summary>
        /// sets time remaining for motion alarm in seconds. 
        /// </summary>
        public uint AlarmInterval
        {
            set
            {
                recording = true;
                alarmInterval = (uint)(value * (1000 / timer.Interval));
            }
        }

        public AlertEvents(VideoFeedOptions o)
        {
            //initalize values
            options = o;
            alarmInterval = 0;
            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 100;
            timer.Start();

            //events n stuff
            recording = false;
            audioPlayed = false;
            frameDropThreshold = 0;

            Log.debug(String.Format("Alert Events Initialized for {0}",o.Name));
        }

        public void sendFrame(Bitmap frame, string Name) {
            if (alarmInterval > 0)
            {
                if (!audioPlayed && options.EnableAlertSound)
                {
                    try
                    {
                        Log.debug(String.Format("Playing audio file {0} for camera {1}",options.AlertSoundFile,options.Name));
                        audioPlayed = true;
                        SoundPlayer simpleSound = new SoundPlayer(options.AlertSoundFile);
                        simpleSound.Play();
                    }
                    catch (Exception e)
                    {
                        Log.error(String.Format("Unable to play audio file for {0}. Error: {1}",options.Name, e.Message));
                    }
                }
                if (recording && options.EnableRecording && frameDropThreshold <= MAX_FRAME_DROP)
                {
                    try
                    {
                        if (videoWriter == null)
                        {
                            DateTime now = DateTime.Now;
                            string file = String.Format("{0}-{1:D4}.{2:D2}.{3:D2}-{4:D2}.{5:D2}.{6:D2}.avi", options.Name, now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
                            string fullpath = Path.Combine(options.RecordFolder, file);

                            Log.debug(String.Format("Creating new Video Writer {0} with codec {1}",
                                fullpath, options.Codec));

                            videoWriter = new AVIWriter(options.Codec.Code);
                            videoWriter.Open(fullpath, frame.Width, frame.Height);
                        }

                        videoWriter.AddFrame(frame);
                    }
                    catch (Exception io)
                    {
                        frameDropThreshold++;
                        videoWriter = null;
                        Log.warn(String.Format("Error writing video frame for {0}. Record Folder: {1}. Message: {2}", options.Name, options.RecordFolder, io.Message));
                        if(frameDropThreshold == MAX_FRAME_DROP) {
                            Log.error(String.Format("Maximum ({0}) frames failed for {1}. Dropping all frames until next motion alert",MAX_FRAME_DROP,options.Name));
                        }
                    }
                }
            }
        }

        //call back for timer which is used to display
        //alarm window on motion detection. The timer 
        //interval is 1 sec, and alarmInterval is increased 
        //by n sec by alarm callback
        void timer_Tick(object sender, EventArgs e)
        {
            if (alarmInterval > 0)
            {
                alarmInterval--;
            }
            if (alarmInterval == 0)
            {
                audioPlayed = false;
                frameDropThreshold = 0;

                if (recording)
                {
                    if (videoWriter != null)
                    {
                        try
                        {
                            Log.info(String.Format("Closing idle writer for camera {0}",options.Name));
                            videoWriter.Close();
                            videoWriter = null;
                        }
                        catch (Exception io)
                        {
                            Log.warn(String.Format("Error closing recorded video file for {0}. Record Folder: {1}. Message: {2}",options.Name,options.RecordFolder,io.Message));
                        }
                    }
                    recording = false;
                }
            }
        }

    }
}
