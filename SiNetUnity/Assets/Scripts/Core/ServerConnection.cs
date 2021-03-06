﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using System.Net.Sockets;
using System.Net;
using UnityEngine;

namespace SiNet {
    public class ServerConnection
    {
        const int MAX_DATA_SIZE = 2048;

        private bool _isConnected;

        public bool isConnected { get { return _isConnected; } }

        private ReceiveBuffer receiveBuffer;

        private Socket socket;
        private IPAddress ip;
        private IPEndPoint ipEnd;

        private Thread connectThread;

        private ServerConnection() { }

        public ServerConnection(string hostIP, int port, ReceiveBuffer receiveBuffer)
        {
            this.receiveBuffer = receiveBuffer;
            this.ip = IPAddress.Parse(hostIP);
            this.ipEnd = new IPEndPoint(ip, port);

            connectThread = new Thread(new ThreadStart(SocketReceiveLoop));
            connectThread.Start();
        }

        public void Abort() {
            if (connectThread != null)
            {
                connectThread.Interrupt();
                connectThread.Abort();
            }

            if (socket != null)
                socket.Close();
        }

		public void Send(Message message) {
            var sendData = message.ToBytes();
			socket.Send(sendData, sendData.Length, SocketFlags.None);
		}

        private void Connect() {
            if (socket != null)
                socket.Close();

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                socket.Connect(ipEnd);
                _isConnected = true;
            }
            catch
            {
                _isConnected = false;
                return;
            }
        }

        private void SocketReceiveLoop() {
            Connect();

            while (true) {
                try
                {
                    // receive data from server
                    var recvData = new byte[MAX_DATA_SIZE];
                    var recvLen = socket.Receive(recvData);

                    if (recvLen == 0)
                    {
                        // lost connection
                        _isConnected = false;
                        Connect();
                        continue;
                    }
                    else
                    {
                        // write receive data to buffer
                        var recvStr = Encoding.ASCII.GetString(recvData, 0, recvLen);
                        foreach (var msg in PDUParser.ParsePDU(recvStr)) {
                            receiveBuffer.Write(msg);
                        }
                    }
                }
                catch {
                    _isConnected = false;
                    Connect();
                    continue;
                }       
            }
        }        
    }
}
