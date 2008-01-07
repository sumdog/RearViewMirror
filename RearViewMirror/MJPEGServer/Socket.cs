using System;
using System.Collections.Generic;
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

        public StreamWriter Writer { get { return write; } set { write = value; } }
        public StreamReader Reader { get { return read; } set { read = value; } }
        public Socket Socket { get { return socket; } set { socket = value; } }

        public VideoSocketHandler()
        {
            write = null;
            read = null;
            socket = null;
            initalized = false;
            bwrite = null;
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
                    Log.trace(line);
                    if (line == null || line.Trim() == "")
                    {
                        endhead = true;
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
            catch (IOException e)
            {
                Log.error("I/O Exception Occured in Socket Initilization");
                initalized = false;
            }

            return initalized;
        }

        public void sendFrame(Bitmap b)
        {
            if(!initalized) {
                throw new UninitalizedVideoSocketException("call initalize() on handler first");
            }
            MemoryStream mem = new MemoryStream();
            b.Save(mem, ImageFormat.Jpeg);
            mem.Position = 0;
            write.WriteLine("--VID_Boundary");
            write.WriteLine("Content-Type: image/jpeg");
            write.WriteLine("Content-Length: " + mem.Length);
            write.WriteLine("");
            write.Flush();

            //write the image binary 
            bwrite.Write(mem.GetBuffer());
            bwrite.Flush();

            write.WriteLine("");
            write.Flush();
        }

        public void close()
        {
            socket.Close();
        }
    }

    public class UninitalizedVideoSocketException : Exception
    {
        public UninitalizedVideoSocketException(String s) : base(s)
        {
        }
    }
}
