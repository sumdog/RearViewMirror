/*
 * RearViewMirror - Sumit Khanna <sumit@penguindreams.org>
 * Copyleft 2007-2012, Some rights reserved
 * http://penguindreams.org/projects/rearviewmirror
 * 
 * Based on work by Andrew Kirillov:
 * http://code.google.com/p/aforge/
 * http://www.codeproject.com/KB/audio-video/Motion_Detection.aspx
 * 
 * 
    This file is part of RearViewMirror.

    RearViewMirror is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    RearViewMirror is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
 *  
 */
using System;
using System.Collections;
namespace RearViewMirror
{

    [Serializable]
    public abstract class AbstractFeedOptions
    {
        public abstract bool UseGlobal { get; set; }

        public abstract double Opacity { get; set; }

        public abstract string Name { get; }

        public abstract bool EnableRecording { get; set; }

        public abstract string RecordFolder { get; set; }

        public abstract bool EnableAlertSound { get; set; }

        public abstract string AlertSoundFile { get; set; }

        protected AbstractFeedOptions() {
        }
    }

    [Serializable]
    public class VideoFeedOptions : AbstractFeedOptions
    {
        private VideoSource vsource;

        public override bool UseGlobal {get; set;}

        public override bool EnableRecording { get; set; }

        public override string RecordFolder { get; set; }

        public override bool EnableAlertSound { get; set; }

        public override string AlertSoundFile { get; set; }

        public override double Opacity
        {
            get { return vsource.ViewerOpacity; }
            set { vsource.ViewerOpacity = value; }
        }

        public override string Name { get { return vsource.Name; } }

        public VideoFeedOptions(VideoSource vsource)
        {
            this.vsource = vsource;

            //defaults
            EnableAlertSound = false;
            EnableRecording = false;
            AlertSoundFile = null;
            RecordFolder = null;
        }
    }

    [Serializable]
    public class GlobalVideoFeedOptions : AbstractFeedOptions
    {
        /// <summary>
        /// List of all video sources passed in from the SystemTray
        /// Note this array is not typed because of the way the SystemTray loads
        /// it from the preferences. Assume each element is of type VideoSource
        /// </summary>
        private ArrayList vsources;

        public GlobalVideoFeedOptions(ArrayList vsources)
        {
            this.vsources = vsources;

            //defaults
            globalOpacity = 1;
            globalEnableAlertSound = false;
            globalEnableRecording = false;
            globalAlertSoundFile = null;
            globalRecordFolder = null;
        }

        public void updateViewers()
        {
            foreach (VideoSource v in vsources)
            { updateViewer(v); }
        }

        public void updateViewer(VideoSource v)
        {
            if (v.Options.UseGlobal)
            {
                v.ViewerOpacity = globalOpacity;
                v.Options.EnableRecording = globalEnableRecording;
                v.Options.RecordFolder = globalRecordFolder;
                v.Options.EnableAlertSound = globalEnableAlertSound;
                v.Options.AlertSoundFile = globalAlertSoundFile;
            }
        }

        #region PrivateGlobals

        private double globalOpacity;

        private bool globalEnableRecording, globalEnableAlertSound;

        private string globalRecordFolder, globalAlertSoundFile;

        #endregion

        #region Global Properties

        public override bool EnableRecording {
            get { return globalEnableRecording; }
            set { globalEnableRecording = value;  } 
        }

        public override string RecordFolder
        {
            get { return globalRecordFolder; }
            set { globalRecordFolder = value; }
        }

        public override bool EnableAlertSound
        {
            get { return globalEnableAlertSound; }
            set { globalEnableAlertSound = value; }
        }

        public override string AlertSoundFile
        {
            get { return globalAlertSoundFile; }
            set { globalAlertSoundFile = value; }
        }

        public override bool UseGlobal
        {
            get { throw new NotImplementedException("Cannot set UseGlobal on Global Options"); }
            set { throw new NotImplementedException("Cannot set UseGlobal on Global Options"); }
        }

        public override string Name { get { return "Global Options"; } }

        public override double Opacity
        {
            get { return globalOpacity; }
            set
            {
                globalOpacity = value;
                updateViewers();
            }
        }

        #endregion

    }
}