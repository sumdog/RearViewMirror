using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Collections;
using System.IO;

namespace MJPEGServer
{
    public class VideoServer
    {
        private TcpListener serverListener;

        private List<Socket> socketList;

        public VideoServer(int port)
        {
            socketList = new List<Socket>();
            serverListener = new TcpListener(port);
            serverListener.BeginAcceptSocket(new AsyncCallback(socketAcceptCallback),null);
        }

        private void socketAcceptCallback(IAsyncResult r)
        {
            Socket clientSocket = serverListener.EndAcceptSocket(r);
            //serverListener.BeginAcceptSocket(new AsyncCallback(socketAcceptCallback), null);
            NetworkStream ns = new NetworkStream(clientSocket);
            StreamWriter write = new StreamWriter(ns);
            StreamReader read = new StreamReader(ns);
            while (!read.EndOfStream)
            {
                Console.WriteLine(read.ReadLine());
            }
        }

        //private void 
    }
}
