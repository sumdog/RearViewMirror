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
using System.Text;
using System.Reflection;
using System.Net;
using System.IO;
using MJPEGServer;

namespace RearViewMirror
{
    /// <summary>
    /// Used to connect to penguindreams.org and check for updates
    /// </summary>
    public class Updater
    {

        public const String VERSION_URL = "http://penguindreams.org/files/progs/RVM.version";

        private static bool newerVersion(String client, String server)
        {
            return new Version(server) > new Version(client);
        }

        /// <summary>
        /// Checks for software updates
        /// </summary>
        /// <returns>string containing URL for updates or null if verison is current or error occurs</returns>
        public static string checkForUpdates()
        {
            try
            {
                Log.info("Checking for Updates");
                
                String version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                Log.debug("Current Version " + version);

                //Make webserver request
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(VERSION_URL);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream webStream = response.GetResponseStream();

                //Prep for buffering response
                String t = null;
                StringBuilder sb = new StringBuilder();
                int count = 0;
                byte[] buf = new byte[8192];
                
                //process response stream
                do
                {
                    count = webStream.Read(buf, 0, buf.Length);
                    if (count != 0)
                    {
                        t = Encoding.ASCII.GetString(buf, 0, count);
                        sb.Append(t);
                    }
                }
                while (count > 0);

                string serverResponse = sb.ToString();
                Log.debug("Server Response " + sb.ToString());

                string[] parts = serverResponse.Split(';');

                bool newVersion = Updater.newerVersion(version,parts[0]);

                Log.info("Newer Version " + newVersion);

                return (newVersion) ? parts[1] : null;
            }
            catch (Exception e)
            {
                Log.error("Error checking for updates " + e.Message);
                return null;
            }
        }

    }
}
