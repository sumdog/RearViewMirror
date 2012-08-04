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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RearViewMirror
{
    public partial class OptionsForm : Form
    {

        private VideoSource vsource;

        public OptionsForm(VideoSource vsource)
        {
            InitializeComponent();

            //If not video source, we're dealing with Global Options
            this.vsource = vsource;
            if (vsource == null)
            {
                lCameraName.Text = "Global Options";
                cbGlobalOptions.Visible = false;

            }
            else
            {
                lCameraName.Text = vsource.Name;
            }

            Text = lCameraName.Text;

            //prevents window disposal
            this.FormClosing += new FormClosingEventHandler(OptionsForm_FormClosing);
        }

        void OptionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true; //prevents window from being Disposed of
            }
            Hide();
        }

        #region Events

        private void tbOpacity_Scroll(object sender, EventArgs e)
        {

        }

        private void bBrowseRecordFolder_Click(object sender, EventArgs e)
        {

        }

        private void bBrowseAudioFile_Click(object sender, EventArgs e)
        {

        }

        private void bOK_Click(object sender, EventArgs e)
        {

        }

        private void bCancel_Click(object sender, EventArgs e)
        {

        }

        #endregion
    }
}