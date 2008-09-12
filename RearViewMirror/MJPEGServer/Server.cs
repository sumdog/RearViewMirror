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
using System.Net.Sockets;
using System.Net;
using System.Collections;
using System.Drawing;
using System.IO;

namespace MJPEGServer
{
    /// <summary>
    /// Used as a way to pass connection information to user interfaces
    /// and abstract the VideoSocketHandler
    /// </summary>
    public class ConnectionInformation
    {

        private VideoSocketHandler sockHandle;

        /// <summary>
        /// Indicates how long the client has been connected to the server
        /// </summary>
        public TimeSpan ConnectionTime
        {
            get { return sockHandle.TimeConnected; }
        }

        /// <summary>
        /// Client's Hostname and sending Port
        /// </summary>
        public String RemoteHost { get { return sockHandle.Socket.RemoteEndPoint.ToString(); } }

        /// <summary>
        /// Disconnects the client
        /// </summary>
        public void disconnect() { sockHandle.close(); }

        public ConnectionInformation(VideoSocketHandler h)
        {
            sockHandle = h;
        }
    }

    /// <summary>
    /// MJPEG Server Object designed to send video over HTTP
    /// </summary>
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
        public ConnectionInformation[] ConnectedUsers {
            get
            {
                List<ConnectionInformation> retval;
                lock (socketList)
                {
                    retval = new List<ConnectionInformation>(socketList.Count);
                    for (int i = 0; i < socketList.Count; i++)
                    {
                        if (socketList[i].Socket.Connected)
                        {
                            retval.Add(new ConnectionInformation(socketList[i]));
                        }
                    }
                }
                return retval.ToArray();
            }
        }

        /// <summary>
        /// Creates a bound instance of the MJPEG HTTP Server
        /// </summary>
        /// <param name="port">port for server to accept requests on</param>
        public VideoServer(int port)
        {
            this.port = port;
            socketList = new List<VideoSocketHandler>();
            serverListener = new TcpListener(IPAddress.Any, port);
            state = ServerState.STOPPED;
        }


        /// <summary>
        /// Begins listening and accepting server connections.
        /// </summary>
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

        /// <summary>
        /// Stops accepting connections and disconnects all connected clients.
        /// </summary>
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

        /// <summary>
        /// Sends one frame of JPEG video to all connected clients
        /// </summary>
        /// <param name="b">Frame to Send</param>
        public void sendFrame(Bitmap b, String name)
        {
            lock (socketList)
            {
                foreach (VideoSocketHandler v in socketList)
                {
                    try
                    {
                        //send to clients connecting to a specific camera path/name
                        if (v.CameraPath == name)
                        {
                            v.sendFrame(b);
                        }
                    }
                    catch (IOException)
                    {
                        //the socket must be removed from the list first
                        //or else we'll keep catching the same exceptions here
                        //on manual disconnects from the ServerConnections.cs
                        socketList.Remove(v);
                        v.close();
                    }
                }
            }
        }
    }

    /// <summary>
    /// Thrown when trying to stop a server that isn't running
    /// or when trying to start a server that is all ready running
    /// </summary>
    public class InvalidServerStateException : Exception
    {
        public InvalidServerStateException(String s) : base(s) { }
    }
}
