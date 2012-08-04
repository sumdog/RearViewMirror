using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MJPEGServer;

namespace RearViewMirror
{
    public partial class LogViewer : Form
    {
        public const int MaxBufferSize = 500;

        public LogViewer()
        {
            InitializeComponent();
            rbLogTxt.BackColor = Color.Black;
            Log.LogEvent += new Log.LogEventHandler(ReceiveLogMessage);
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

        private void ReceiveLogMessage(string logMessage, Log.LogLevel level)
        {
            switch (level)
            {
                case Log.LogLevel.TRACE:
                    rbLogTxt.SelectionColor = Color.Blue;
                    break;
                case Log.LogLevel.DEBUG:
                    rbLogTxt.SelectionColor = Color.Yellow;
                    break;
                case Log.LogLevel.INFO:
                    rbLogTxt.SelectionColor = Color.White;
                    break;
                case Log.LogLevel.WARN:
                    rbLogTxt.SelectionColor = Color.Orange;
                    break;
                case Log.LogLevel.ERROR:
                    rbLogTxt.SelectionColor = Color.Tomato;
                    break;
                case Log.LogLevel.FATAL:
                    rbLogTxt.SelectionColor = Color.Red;
                    break;

            }

            rbLogTxt.SelectedText += logMessage + '\n';

            int oversize = rbLogTxt.TextLength;
            if (oversize > MaxBufferSize)
            {
                rbLogTxt.Text = rbLogTxt.Text.Substring(MaxBufferSize - oversize, MaxBufferSize);
            }
        }
    }
}
