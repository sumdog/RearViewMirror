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

namespace RearViewMirror
{
    /// <summary>
    /// Used to connect to penguindreams.org and check for updates
    /// </summary>
    public class Updater
    {

        public readonly static String VERSION_URL = "http://penguindreams.org/files/RVM.version";

        public readonly static String PAGE_URL = "http://penguindreams.org/files/RVM.version";

        public static Boolean checkForUpdates()
        {
            try
            {
                String version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

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

                Console.WriteLine(sb.ToString());

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

    }
}
