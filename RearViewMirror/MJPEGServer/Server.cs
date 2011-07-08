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
using System.Net.Sockets;
using System.Net;
using System.Collections;
using System.Drawing;
using System.IO;
using RearViewMirror.MJPEGServer;
using System.Collections.Concurrent;

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
        private ConcurrentDictionary<int,VideoSocketHandler> socketList;

        private int port;

        /// <summary>
        /// Standard TCP Backlog Queue
        /// </summary>
        private static readonly int BACKLOG = 30;

        public enum ServerState { STOPPED, STARTED };

        private ServerState state;

        public ServerState State { get { return state; } }

        private FrameQueue frameQueue;

        /// <summary>
        /// Port for Server to Listen To
        /// </summary>
        public int Port { get { return port; } set { port = value; } }

        /// <summary>
        /// Property indicating number of clients connected to server
        /// </summary>
        public int NumberOfConnectedUsers { get {  return socketList.Count; }   }

        /// <summary>
        /// Property containing a list of IP addresses connected to server
        /// </summary>
        public ConnectionInformation[] ConnectedUsers {
            get
            {
                List<ConnectionInformation> retval;

                retval = new List<ConnectionInformation>(socketList.Count);
                foreach (KeyValuePair<int,VideoSocketHandler> pair in socketList)
                {
                    if (pair.Value.Socket.Connected)
                    {
                        retval.Add(new ConnectionInformation(pair.Value));
                    }
                }
                return retval.ToArray();
            }
        }


        /// <summary>
        /// Gets an instance to the Server Singleton
        /// </summary>
        public static readonly VideoServer Instance = new VideoServer();


        /// <summary>
        /// Protected Singleton constructor for an instance of the MJPEG HTTP Server
        /// </summary>
        public VideoServer()
        {
            socketList = new ConcurrentDictionary<int,VideoSocketHandler>();                    
            state = ServerState.STOPPED;
            frameQueue = new FrameQueue(socketList);
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
                    serverListener = new TcpListener(IPAddress.Any, port);
                    serverListener.Start(BACKLOG);
                    serverListener.BeginAcceptSocket(new AsyncCallback(socketAcceptCallback), new VideoSocketHandler());
                    frameQueue.startQueue();
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
                frameQueue.stopQueue();
                serverListener = null;
                state = ServerState.STOPPED;

                //kill all sockets by closing them first and then
                //clearning the socket queue
                foreach (KeyValuePair<int, VideoSocketHandler> set in socketList)
                {
                    VideoSocketHandler closeme;
                    if (socketList.TryRemove(set.Key, out closeme))
                    {
                        closeme.close();
                    }
                }
                socketList.Clear();
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
                    socketList.TryAdd( ((IPEndPoint)(clientSocket.RemoteEndPoint)).Port,handle);
                    //socketList.Add(1,handle);
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
            catch (NullReferenceException)
            {
                Log.warn("Socket callback threw NullPointer. This *might* be normal if the server was stopped");
            }
        }

        /// <summary>
        /// Sends one frame of JPEG video to all connected clients
        /// </summary>
        /// <param name="b">Frame to Send</param>
        public void sendFrame(Bitmap b, String name)
        {
            frameQueue.addFrameToQueue(b, name);
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
