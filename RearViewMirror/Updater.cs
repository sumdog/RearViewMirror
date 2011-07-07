/**
 * RearViewMirror - Sumit Khanna 
 * http://penguindreams.org
 * 
 * License: GNU GPLv3 - Free to Distribute so long as any 
 *   modifications are released for free as well
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
