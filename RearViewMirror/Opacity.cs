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

namespace RearViewMirror
{
    public partial class Opacity : Form
    {
        private Form opWin;

        public Opacity(Form opWin)
        {
            InitializeComponent();
            this.opWin = opWin;
            opacityBar.Minimum = 0;
            opacityBar.Maximum = 100;
            opacityBar.Value = (int) (opWin.Opacity * 100);
            this.MaximumSize = this.Size; //prevent resizing
        }

        private void opacityBar_Scroll(object sender, EventArgs e)
        {
            opWin.Opacity = ((double)opacityBar.Value) / 100;
        }

        private void b_close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

    }
}