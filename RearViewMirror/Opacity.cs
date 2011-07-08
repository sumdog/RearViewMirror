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