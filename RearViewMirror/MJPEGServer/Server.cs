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

        /// <summary>
        /// List of connected sockets in queue 
        /// </summary>
        private List<VideoSocketHandler> socketList;

        private int port;

        private static readonly int BACKLOG = 30;

        public enum ServerState { STOPPED, STARTED };

        private ServerState state;

        public ServerState State { get { return state; } }

        public int Port { get { return port; }  }

        /// <summary>
        /// Property indicating number of clients connected to server
        /// </summary>
        public int NumberOfConnectedUsers { get { return socketList.Count; } }

        /// <summary>
        /// Property containing a list of IP addresses connected to server
        /// </summary>
        public List<String> ConnectedUsers {
            get
            {
                List<String> retval = new List<String>();
                lock (socketList)
                {
                    for (int i = 0; i < socketList.Count; i++)
                    {
                        retval.Add( socketList[i].Socket.RemoteEndPoint.ToString() );
                    }
                }
                return retval;
            }
        }

        public VideoServer(int port)
        {
            this.port = port;
            socketList = new List<VideoSocketHandler>();
            serverListener = new TcpListener(IPAddress.Any, port);
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
                    throw new InvalidServerStateException("Could not start server. " + e.Message);
                }
            }
            else
            {
                throw new InvalidServerStateException("Server has all ready started");
            }
        }

        public void stopServer()
        {
            if (state == ServerState.STOPPED)
            {
                throw new InvalidServerStateException("Server has not been started");
            }
            try
            {
                serverListener.Stop();
                state = ServerState.STOPPED;

                //kill all sockets by closing them first and then
                //clearning the socket queue
                lock (socketList)
                {
                    foreach (VideoSocketHandler v in socketList) { v.close(); }
                    socketList.Clear();
                }
            }
            catch (SocketException)
            {
                Log.warn("SocketException occured in stopServer(). Can we safely ignore this?");
            }
            
        }

        private void socketAcceptCallback(IAsyncResult r)
        {
            try
            {
                VideoSocketHandler handle = (VideoSocketHandler)r.AsyncState;
                Socket clientSocket = serverListener.EndAcceptSocket(r);
                serverListener.BeginAcceptSocket(new AsyncCallback(socketAcceptCallback), new VideoSocketHandler());

                handle.setIOStreams(clientSocket);
                if (handle.initalize())
                {
                    lock (socketList)
                    {
                        socketList.Add(handle);
                    }
                }
                else
                {
                    handle.close();
                }
            }
            catch (ObjectDisposedException)
            {
                Log.info("Socket callback got a ObjectDisposed. This is normal if the server was stopped");
            }
        }

        public void sendFrame(Bitmap b)
        {
            lock (socketList)
            {
                foreach (VideoSocketHandler v in socketList)
                {
                    try
                    {
                        v.sendFrame(b);
                    }
                    catch (IOException)
                    {
                        v.close();
                        socketList.Remove(v);
                    }
                }
            }
        }
    }

    public class InvalidServerStateException : Exception
    {
        public InvalidServerStateException(String s) : base(s) { }
    }
}
