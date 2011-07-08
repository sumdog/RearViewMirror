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
using System.Collections.Specialized;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Drawing;
using System.Drawing.Imaging;

namespace MJPEGServer
{
    public class VideoSocketHandler
    {
        private StreamWriter write;
        private StreamReader read;
        private Socket socket;
        private Boolean initalized;
        private BinaryWriter bwrite;
        private DateTime connectionTime;
        private StringCollection headers;
        private String name;
        private String protocol;

        public StreamWriter Writer { get { return write; } set { write = value; } }
        public StreamReader Reader { get { return read; } set { read = value; } }
        public Socket Socket { get { return socket; } set { socket = value; } }
        public TimeSpan TimeConnected { get { return DateTime.Now - connectionTime; } }
        public String ClientProtocol { get { return protocol; } }
        public String CameraPath { get { return name; } set { name = value; } }

        public VideoSocketHandler()
        {
            write = null;
            read = null;
            socket = null;
            initalized = false;
            bwrite = null;
            headers = new StringCollection();
            connectionTime = DateTime.Now;
        }

        public void setIOStreams(Socket s)
        {
            NetworkStream ns = new NetworkStream(s);
            write = new StreamWriter(ns);
            read = new StreamReader(ns);
            bwrite = new BinaryWriter(ns);
            socket = s;
            initalized = false;
        }

        /// <summary>
        /// Used within initalize() to check if the HTTP GET request is valid
        /// and pull back the correct camera name.
        /// </summary>
        /// <returns>True if client request header is valid.</returns>
        private bool checkHeader()
        {
            if (headers.Count <= 0)
            {
                return false;
            }
            //split apart the HTTP Get Request. Example:
            //  GET /path HTTP/1.1
            String[] get = headers[0].Split(' ');
            if (get.Length < 3 || get[0] != "GET")
            {
                return false;
            }
            //This is the URL the client is trying to hit
            // which should match the name of the camera
            // (trim off leading/trailing slashes)
            name = get[1].Trim( new char[] { '/','\\' } ); 

            //this is the client's protocol (e.g. HTTP/1.1)
            protocol = get[2];

            return true;
        }

        public bool initalize()
        {
            try
            {
                Log.trace("Pulling Headers for " + socket.RemoteEndPoint.ToString());
                //we're simply going to ignore the header entirely, so whatever they
                //request, we'll just send them back a JPEG image
                bool endhead = false;
                while (!endhead)
                {
                    String line = read.ReadLine();
                    headers.Add(line);
                    Log.trace(line);
                    if (line == null || line.Trim() == "")
                    {
                        endhead = true;

                        if (!checkHeader())
                        {
                            Log.info("Invalid request from " + socket.RemoteEndPoint.ToString());
                            write.WriteLine("HTTP/1.1 400 Bad Response");
                            write.WriteLine("\n<html><body><p>" + 
                                            "This M" + 
                                            "</p></body></html>");
                        }

                        Log.trace("Finished Headers. Sending Response");
                        write.WriteLine("HTTP/1.1 200 OK");
                        write.WriteLine("Content-Type: multipart/x-mixed-replace;boundary=--VID_Boundary");
                        write.WriteLine("Server: PenguinDreams.org/MJPEGServer/1.0");
                        write.WriteLine("");
                        write.Flush();
                    }

                }
                Log.trace("Response Sent. Ready to Send JPEGs");
                initalized = true;
            }
            catch (IOException)
            {
                Log.error("I/O Exception Occured in Socket Initilization");
                initalized = false;
            }

            return initalized;
        }

        public void sendFrame(byte[] b)
        {
            if(!initalized) {
                throw new UninitalizedVideoSocketException("call initalize() on handler first");
            }
            write.WriteLine("--VID_Boundary");
            write.WriteLine("Content-Type: image/jpeg");
            write.WriteLine("Content-Length: " + b.Length);
            write.WriteLine("");
            write.Flush();

            //write the image binary 
            bwrite.Write(b);
            bwrite.Flush();

            write.WriteLine("");
            write.Flush();
        }

        public void close()
        {
            try
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception e)
            {
                Log.warn("Could not close socket. " + e.Message);
            }
        }
    }

    public class UninitalizedVideoSocketException : Exception
    {
        public UninitalizedVideoSocketException(String s) : base(s)
        {
        }
    }
}
