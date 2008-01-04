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
using System.Windows.Forms;
using MJPEGServer;

namespace RearViewMirror
{
    static class Program
    {

        /// <summary>
        /// This is where the magic happens.
        /// </summary>
        [STAThread]
        static void Main()
        {
            VideoServer v = new VideoServer(222);
            SystemTray s = new SystemTray();
            Application.Run(s);
        }
    }
}