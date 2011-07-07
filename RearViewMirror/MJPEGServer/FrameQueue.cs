/*
 * RearViewMirror - Sumit Khanna 
 * http://penguindreams.org
 * 
 * License: GNU GPLv3 - Free to Distribute so long as any 
 *   modifications are released for free as well
 */
using System;
using System.Collections.Generic;
using System.Text;
using MJPEGServer;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Collections.Concurrent;
using System.Drawing.Imaging;

namespace RearViewMirror.MJPEGServer
{
    public class FrameQueue
    {

        public const int STOP_QUEUE_TIMEOUT = 2000;

        public const int QUEUE_RATE = 10;

        private ConcurrentDictionary<int,VideoSocketHandler> socketList;

        private Thread queueThread;

        private bool keepRunning;

        private ConcurrentDictionary<string, ConcurrentQueue<byte[]>> queues;

        private byte[] decodeFrame(Bitmap b)
        {
            MemoryStream mem = new MemoryStream();
            b.Save(mem, ImageFormat.Jpeg);
            mem.Position = 0;
            return mem.GetBuffer();
        }

        public void addFrameToQueue(Bitmap b, String name)
        {
            if (keepRunning)
            {
                if (!queues.ContainsKey(name))
                {
                    Log.info("Queue created for " + name);
                    queues[name] = new ConcurrentQueue<byte[]>();
                }
                queues[name].Enqueue(decodeFrame(b));
            }
        }

        private void queueThreadFunction()
        {
            Log.debug("Starting Frame Queue Thread");
            while(keepRunning)
            {
                byte[] sendB;
                foreach (KeyValuePair<string,ConcurrentQueue<byte[]>> q in queues)
                {
                    while (!q.Value.IsEmpty)
                    {
                        if (q.Value.TryDequeue(out sendB))
                        {
                            sendFrame(q.Key, sendB);
                        }
                    }
                }
                Thread.Sleep(QUEUE_RATE);
            }
            Log.debug("Frame Queue Thread Exiting");
        }

        private void sendFrame(string name, byte[] frame)
        {

            foreach (KeyValuePair<int,VideoSocketHandler> pair in socketList)
            {
                try
                {
                    //send to clients connecting to a specific camera path/name
                    if (pair.Value.CameraPath == name)
                    {
                        pair.Value.sendFrame(frame);
                    }
                }
                catch (IOException)
                {
                    //the socket must be removed from the list first
                    //or else we'll keep catching the same exceptions here
                    //on manual disconnects from the ServerConnections.cs
                    VideoSocketHandler removed;
                    if (socketList.TryRemove(pair.Key, out removed))
                    {
                        removed.close();
                    }
                }
            }
        }

        

        public FrameQueue(ConcurrentDictionary<int,VideoSocketHandler> socketList)
        {
            this.socketList = socketList;
            keepRunning = false;
            queueThread = new Thread(this.queueThreadFunction);
            queues = new ConcurrentDictionary<string, ConcurrentQueue<byte[]>>();
        }

        public void startQueue()
        {
            Log.info("Request to start Frame Queue");
            keepRunning = true;
            queueThread.Start();
        }

        public void stopQueue()
        {
            Log.info("Request to stop Frame Queue");
            keepRunning = false;
            if (!queueThread.Join(STOP_QUEUE_TIMEOUT))
            {
                Log.warn("Frame Queue did not stop. Giving it an abortion.");
                queueThread.Abort();
            }
            queueThread = new Thread(this.queueThreadFunction); 
        }


    }
}
