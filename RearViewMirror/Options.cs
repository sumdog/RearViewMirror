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
    }
}