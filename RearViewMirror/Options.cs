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
using System.Xml.Serialization;
using MJPEGServer;
using AForge.Video.DirectShow;
namespace RearViewMirror
{


    /// <summary>
    /// Provides an array of Codecs suitable for use in Combo boxes.
    /// </summary>
    [Serializable]
    public class CodecOption
    {

        public string Moniker { get; set; }
        public string Name { get; set; }
        public string Code { 
            get { 
                String[] parts = Moniker.Split('\\');
                return (parts.Length >= 2) ? parts[1] : parts[0];
            } 
        }
        public override string ToString() { return Name; }
        public override bool Equals(object obj)
        {
            if (obj is CodecOption)
            {
                return ((CodecOption)obj).Name == Name;
            }
            return false;
        }  

        public static CodecOption[] getAvailableCodecs()
        {
            FilterInfoCollection codecs = new FilterInfoCollection(FilterCategory.VideoCompressorCategory);
            CodecOption[] retval = new CodecOption[codecs.Count+1];

            retval[0] = new CodecOption() { Name = "Raw\\Uncompressed", Moniker = "DIB " };

            Log.info("Installed Codecs");
            for(int i=0; i < codecs.Count; i++){
                Log.info(String.Format("Name: {0}\n\tMoniker:{1}", codecs[i].Name, codecs[i].MonikerString));
                retval[i+1] = new CodecOption() { Name = codecs[i].Name, Moniker = codecs[i].MonikerString };
            }
            return retval;
        }
    }

    public abstract class AbstractFeedOptions
    {
        public abstract bool UseGlobal { get; set; }

        public abstract double Opacity { get; set; }

        public abstract string Name { get; }

        public abstract bool EnableRecording { get; set; }

        public abstract string RecordFolder { get; set; }

        public abstract bool EnableAlertSound { get; set; }

        public abstract string AlertSoundFile { get; set; }

        public abstract CodecOption Codec { get; set; }

        public abstract bool EnableMotionAlert { get; set; }
        public abstract bool EnableAlwaysShow { get; set; }
        public abstract VideoSource.DetectorType DetectorType { get; set; }

        protected AbstractFeedOptions() {
        }

        public override string ToString()
        {
            return String.Format("Settings for {0}: [UseGlobal={1}, Opacity={2}, EnableRecording={3}, RecordFolder={4}, EnableAlertSound={5}, AlertSoundFile={6}]",
                Name,UseGlobal,Opacity,EnableRecording,RecordFolder,EnableAlertSound,AlertSoundFile);
        }
    }

    [Serializable]
    public class VideoFeedOptions : AbstractFeedOptions
    {

        public override bool UseGlobal {get; set;}

        public override bool EnableRecording { get; set; }

        public override string RecordFolder { get; set; }

        public override bool EnableAlertSound { get; set; }

        public override string AlertSoundFile { get; set; }

        [XmlIgnore]
        public override bool EnableMotionAlert { 
            get {return VideoSource.EnableAlert; }
            set {
                if (VideoSource != null)
                {
                    VideoSource.EnableAlert = value;
                }
            }
        }

        private CodecOption codec;
        public override CodecOption Codec { 
            get { return codec; } 
            set { codec = value; } 
        }

        public override double Opacity
        {
            get { 
                return VideoSource.ViewerOpacity; 
            }
            set {
                if (VideoSource != null)
                {
                    VideoSource.ViewerOpacity = value;
                }
            }
        }

        [XmlIgnore]
        public override string Name { get { return VideoSource.Name; } }

        [XmlIgnore]
        public VideoSource VideoSource { set; private get; }

        
        public override bool EnableAlwaysShow { 
            get { return VideoSource.Sticky; }
            set {
                if (VideoSource != null)
                {
                    VideoSource.Sticky = value;
                }
            } 
        }



        [XmlIgnore]        public override VideoSource.DetectorType DetectorType {
            get { return VideoSource.Detector; }
            set {
                if (VideoSource != null)
                {
                    VideoSource.Detector = value;
                }            }        }

        public VideoFeedOptions()
        {

            //defaults
            EnableAlertSound = false;
            EnableRecording = false;
            AlertSoundFile = null;
            RecordFolder = null;
            UseGlobal = true;
            Codec = null;
            EnableMotionAlert = true;
            EnableAlwaysShow = false;
            DetectorType = RearViewMirror.VideoSource.DetectorType.FastBlock;
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
        [XmlIgnore] public ArrayList VideoSources { set; private get; }

        public GlobalVideoFeedOptions()
        {

            //defaults
            globalOpacity = 1;
            globalEnableAlertSound = false;
            globalEnableRecording = false;
            globalAlertSoundFile = null;
            globalRecordFolder = null;
            globalCodec = null;
            globalEnableMotionAlert = true;
            globalEnableAlwaysShow = false;
            globalDetectorType = RearViewMirror.VideoSource.DetectorType.FastBlock;
        }

        public void updateViewers()
        {
            if (VideoSources == null)
            {
                Log.warn("Video Source List for Global Options is null");
            }
            else
            {
                foreach (VideoSource v in VideoSources)
                { updateViewer(v); }
            }
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
                v.Options.Codec = globalCodec;
                v.Options.EnableAlwaysShow = globalEnableAlwaysShow;
                v.Options.EnableMotionAlert = globalEnableMotionAlert;
                v.Options.DetectorType = globalDetectorType;
            }
        }

        #region PrivateGlobals

        private double globalOpacity;

        private bool globalEnableRecording, globalEnableAlertSound, globalEnableMotionAlert, globalEnableAlwaysShow;

        private VideoSource.DetectorType globalDetectorType;

        private string globalRecordFolder, globalAlertSoundFile;

        private CodecOption globalCodec;

        #endregion

        #region Global Properties

        public override CodecOption Codec
        {
            get { return globalCodec; }
            set { globalCodec = value; }
        }

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
            get { return false; }
            set {  }
        }

        [XmlIgnore]
        public override string Name { get { return "Global Options"; } }

        public override bool EnableAlwaysShow { 
            get { return globalEnableAlwaysShow; }
            set { globalEnableAlwaysShow = value; } 
        }

        public override bool EnableMotionAlert
        {
            get { return globalEnableMotionAlert; }
            set { globalEnableMotionAlert = value; }
        }

        public override VideoSource.DetectorType DetectorType
        {
            get { return globalDetectorType; }
            set { globalDetectorType =  value; }
        }

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