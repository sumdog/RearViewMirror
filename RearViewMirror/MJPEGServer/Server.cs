using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Collections;
using System.Drawing;
using System.IO;

namespace MJPEGServer
{


    public class VideoServer
    {
        private TcpListener serverListener;

        private List<VideoSocketHandler> socketList;

        private static readonly int BACKLOG = 30;

        public enum ServerState { STOPPED, STARTED };

        private ServerState state;

        public ServerState State { get { return state; } }

        public VideoServer(int port)
        {
            socketList = new List<VideoSocketHandler>();
            serverListener = new TcpListener(IPAddress.Any, 80);
            state = ServerState.STOPPED;
        }

        public void startServer()
        {
            if (state == ServerState.STOPPED)
            {
                try
                {
                    serverListener.Start(BACKLOG);
                    serverListener.BeginAcceptSocket(new AsyncCallback(socketAcceptCallback), new VideoSocketHandler());
                    state = ServerState.STARTED;
                }
                catch (SocketException e)
                {
                    throw new InvalidServerStateException("Could not start server: " + e.Message);
                }
            }
            else
            {
                throw new InvalidServerStateException("Server has all ready started");
            }
        }

        public void stopServer()
        {
            if (state == ServerState.STARTED)
            {
                throw new InvalidServerStateException("Server has not been started");
            }
            try
            {
                serverListener.Stop();
                state = ServerState.STOPPED;
            }
            catch (SocketException e)
            {
                //Can we safely Ignore this?
            }
            
        }

        private void socketAcceptCallback(IAsyncResult r)
        {
            VideoSocketHandler handle = (VideoSocketHandler) r.AsyncState;
            Socket clientSocket = serverListener.EndAcceptSocket(r);
            serverListener.BeginAcceptSocket(new AsyncCallback(socketAcceptCallback), new VideoSocketHandler());

            handle.setIOStreams(clientSocket);
            if (handle.initalize())
            {
                socketList.Add(handle);
            }
            else
            {
                handle.close();
            }
        }

        public void sendFrame(Bitmap b)
        {
            foreach (VideoSocketHandler v in socketList)
            {
                try
                {
                    v.sendFrame(b);
                }
                catch (IOException o)
                {
                    v.close();
                    socketList.Remove(v);
                }
            }
        }
    }

    public class InvalidServerStateException : Exception
    {
        public InvalidServerStateException(String s) : base(s) { }
    }
}
