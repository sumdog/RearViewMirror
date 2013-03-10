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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using MJPEGServer;
using System;
using System.Collections.Generic;

namespace RearViewMirror
{

    public partial class OptionsForm : Form
    {

        private AbstractFeedOptions options;

        private double initialOpacity;
        private bool initialUseGlobal;

        public OptionsForm(AbstractFeedOptions options)
        {
            InitializeComponent();

            this.options = options;

            Log.debug(String.Format("Current Options {0}",options));

            //prevent manual input
            cbDetectorType.DropDownStyle = ComboBoxStyle.DropDownList;

            lCameraName.Text = options.Name;
            tbOpacity.Minimum = 0;
            tbOpacity.Maximum = 100;

            //undos
            motionUndoRecord = options.EnableRecording;
            motionUndoAlertsound = options.EnableAlertSound;

            //pull values from options
            cbAlwaysShow.Checked = options.EnableAlwaysShow;
            cbRecord.Checked = options.EnableRecording;
            tbRecordFolder.Text = options.RecordFolder;
            cbAlertSound.Checked = options.EnableAlertSound;
            tbAudioFile.Text = options.AlertSoundFile;
            cbEnableMotionAlarm.Checked = options.EnableMotionAlert;
            flopFileSelectionBoxes();
            flopAlerts();

            tbOpacity.Value = (int)(options.Opacity * 100);

            //Global Check
            if (options is GlobalVideoFeedOptions)
            {
                cbGlobalOptions.Visible = false;
            }
            else
            {
                cbGlobalOptions.Checked = options.UseGlobal;
                initialUseGlobal = options.UseGlobal; //for cancel
            }

            //for cancel
            initialOpacity = options.Opacity;

            this.MaximumSize = this.Size; //prevent resizin'
            this.MinimumSize = this.Size;
            this.MaximizeBox = false;


            //motion detector types
            cbDetectorType.DataSource = Enum.GetValues(typeof(VideoSource.DetectorType));
            cbDetectorType.FormattingEnabled = true;
            cbDetectorType.Format += delegate(object sender, ListControlConvertEventArgs e)
            {
                if ( ((VideoSource.DetectorType)e.Value) == VideoSource.DetectorType.FastBlock)
                {
                    e.Value = "Block (Optimized)";
                }
                else
                {
                    e.Value = e.Value;
                }
            };
            cbDetectorType.SelectedItem = options.DetectorType;


            //prevents window disposal
            this.FormClosing += new FormClosingEventHandler(OptionsForm_FormClosing);

            //Log
            cbCodec.Items.AddRange(CodecOption.getAvailableCodecs());
            cbCodec.SelectedItem = options.Codec;
            Log.debug("Selected Codec: " + options.Codec);
        }

        private void cbEnableMotionAlarm_CheckedChanged(object sender, EventArgs e)
        {
            cbDetectorType.Enabled = cbEnableMotionAlarm.Checked;
            flopAlerts(true);
        }

        #region Events

        private bool OKClicked = false; 

        void OptionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!OKClicked) {
                //Canceled
                options.Opacity = initialOpacity;
                if (!(options is GlobalVideoFeedOptions))
                {
                    options.UseGlobal = initialUseGlobal;
                }
            }
            Dispose();
        }


        private void tbOpacity_Scroll(object sender, EventArgs e)
        {
            options.Opacity = ((double)tbOpacity.Value) / 100.0;
        }

        private void bBrowseRecordFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browser = new FolderBrowserDialog();
            browser.SelectedPath = tbRecordFolder.Text;
            if (browser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbRecordFolder.Text = browser.SelectedPath;
            }
        }

        private void bBrowseAudioFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog browser = new OpenFileDialog();
            browser.FileName = tbAudioFile.Text;
            browser.Filter = "Wave Audio|*.wav";
            if (browser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbAudioFile.Text = browser.FileName;
            }
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            OKClicked = true;

            if (!(options is GlobalVideoFeedOptions))
            {
                options.UseGlobal = cbGlobalOptions.Checked;
            }

            //Save stuff
            options.EnableRecording = cbRecord.Checked;
            options.RecordFolder = tbRecordFolder.Text ;
            options.EnableAlertSound = cbAlertSound.Checked;
            options.AlertSoundFile= tbAudioFile.Text;
            options.Codec = (CodecOption) cbCodec.SelectedItem;
            options.EnableAlwaysShow = cbAlwaysShow.Checked;
            options.EnableMotionAlert = cbEnableMotionAlarm.Checked;
            options.DetectorType = (VideoSource.DetectorType) cbDetectorType.SelectedItem;

            //Validation
            LinkedList<string> errors = new LinkedList<string>();
            if (cbAlertSound.Checked && tbAudioFile.Text.Trim() == "")
            { errors.AddLast("No sound file selected"); }
            if (cbRecord.Checked)
            {
                if (tbRecordFolder.Text.Trim() == "")
                { errors.AddLast("No record folder set"); }
                if (cbCodec.SelectedItem == null)
                { errors.AddLast("No video codec selected"); }
            }
            if (errors.Count > 0)
            {
                MessageBox.Show(String.Join("\n", errors), "Errors", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else
            {
                if (options is GlobalVideoFeedOptions)
                {
                    ((GlobalVideoFeedOptions)options).updateViewers();               
                }
                Close();
            }
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cbGlobalOptions_CheckedChanged(object sender, EventArgs e)
        {            
            flopGlobals();
            flopFileSelectionBoxes();
            flopAlerts();
        }

        private void cbAlertSound_CheckedChanged(object sender, EventArgs e)
        {
            flopFileSelectionBoxes();
        }

        private void cbRecord_CheckedChanged(object sender, EventArgs e)
        {
            flopFileSelectionBoxes();
        }

        #endregion

        #region SubEvents   

        private void flopFileSelectionBoxes()
        {

            bBrowseAudioFile.Enabled = cbAlertSound.Checked;
            tbAudioFile.Enabled = cbAlertSound.Checked;

            bBrowseRecordFolder.Enabled = cbRecord.Checked;
            tbRecordFolder.Enabled = cbRecord.Checked;
            cbCodec.Enabled = cbRecord.Checked;

            cbDetectorType.Enabled = cbEnableMotionAlarm.Checked;
        }


        private bool motionUndoRecord, motionUndoAlertsound;
        private void flopAlerts(bool changed = false)
        {
            Control[] controls = {  cbAlertSound, cbRecord };
            foreach (Control c in controls) { c.Enabled = cbEnableMotionAlarm.Checked; }

            if (!cbEnableMotionAlarm.Checked && cbAlertSound.Checked)
            {
                motionUndoAlertsound = cbAlertSound.Checked;
                cbAlertSound.Checked = false;
            }

            if (!cbEnableMotionAlarm.Checked && cbRecord.Checked)
            {
                motionUndoRecord = cbRecord.Checked;
                cbRecord.Checked = false;
            }

            if (changed && cbEnableMotionAlarm.Checked)
            {
                cbRecord.Checked = motionUndoRecord;
                cbAlertSound.Checked = motionUndoAlertsound;
            }
        }

        private bool undoAlertSoundEnabled, undoRecordEnabled, undoAlwaysShow, undoEnableMotion;
        private string undoRecordFolder, undoAudioFile;
        private int undoOpacity;
        private object undoCodec;
        private VideoSource.DetectorType undoDetectorType;

        private void flopGlobals()
        {
            Control[] controls = { tbOpacity, cbRecord, cbAlertSound, bBrowseAudioFile, bBrowseRecordFolder, 
                                     tbAudioFile, tbRecordFolder, cbCodec, cbDetectorType, cbEnableMotionAlarm, cbAlwaysShow };

            if (cbGlobalOptions.Checked)
            {
                //save undo
                undoAlertSoundEnabled = cbAlertSound.Checked;
                undoRecordEnabled = cbRecord.Checked;
                undoRecordFolder = tbRecordFolder.Text;
                undoAudioFile = tbAudioFile.Text;
                undoOpacity = tbOpacity.Value;
                undoCodec = cbCodec.SelectedItem;
                undoAlwaysShow = cbAlwaysShow.Checked;
                undoEnableMotion = cbEnableMotionAlarm.Checked;
                undoDetectorType = options.DetectorType;


                //pull in all globals
                GlobalVideoFeedOptions globals = Program.globalSettings;
                cbAlertSound.Checked = globals.EnableAlertSound;
                cbRecord.Checked = globals.EnableRecording;
                tbRecordFolder.Text = globals.RecordFolder;
                tbAudioFile.Text = globals.AlertSoundFile;
                cbCodec.SelectedItem = globals.Codec;

                tbOpacity.Value = (int)(globals.Opacity * 100);
                options.Opacity = ((double)tbOpacity.Value) / 100.0;

                cbAlwaysShow.Checked = globals.EnableAlwaysShow;
                cbEnableMotionAlarm.Checked = globals.EnableMotionAlert;
                cbDetectorType.SelectedItem = globals.DetectorType;

                foreach (Control c in controls) { c.Enabled = false; }
                
            }
            else
            {

                //undo changes                
                cbAlertSound.Checked = undoAlertSoundEnabled;
                cbRecord.Checked = undoRecordEnabled;
                tbRecordFolder.Text = undoRecordFolder;
                tbAudioFile.Text = undoAudioFile;
                tbOpacity.Value = undoOpacity;
                options.Opacity = ((double)tbOpacity.Value) / 100.0;
                cbCodec.SelectedItem = undoCodec;
                cbAlwaysShow.Checked = undoAlwaysShow;
                cbEnableMotionAlarm.Checked = undoEnableMotion;
                cbDetectorType.SelectedItem = undoDetectorType;

                foreach (Control c in controls) { c.Enabled = true; }
            }
        }

        #endregion




    }

    
}