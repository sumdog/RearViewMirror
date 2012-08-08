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
                    rbLogTxt.SelectionColor = Color.LightCyan;
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


            //Taken from http://stackoverflow.com/questions/2196097/elegant-log-window-in-winforms-c-sharp (m_eiman)
            if (rbLogTxt.Lines.Length > MaxBufferSize)
            {
                rbLogTxt.Select(0, rbLogTxt.Text.IndexOf('\n') + 1);
                rbLogTxt.SelectedRtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1053\\uc1 }";
                rbLogTxt.SelectionStart = rbLogTxt.Text.Length;
            }

            rbLogTxt.ScrollToCaret();


        }
    }
}
