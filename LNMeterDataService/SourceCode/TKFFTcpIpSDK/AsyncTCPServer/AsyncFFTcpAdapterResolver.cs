using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TKFFTcpIpSDK.Infrastructure
{
    //TcpListener achive async TCP Server
    public static class AsyncFFTcpAdapterResolver
    {
        //List of the client
        public static Dictionary<long, TCPClientSession> m_Clients;

        private static bool disposed;

        //Client ID (Increment)
        private static long m_ClientID = 1000;

        //TCP Server TcpListener
        private static TcpListener m_Listener;
        private static Thread _listenerThread;

        //the byte count send or received
        public static long s_AllSendBytes = 0;
        public static long s_AllReceBytes = 0;

        //Constructor
        static AsyncFFTcpAdapterResolver()
        {
            Application.ApplicationExit += new EventHandler(OnApplicationExit);
        }

        private static void OnApplicationExit(object sender, EventArgs e)
        {
            // When the application is exiting, write the application data to the user file and close it.
            try
            {
                Stop();
                ProcessToDispose(true);
            }
            catch (Exception)
            {
                // WriteDataToLocalFiles(ex.Message + ex.StackTrace);
            }
        }

        //Current connected client count
        public static int m_ClientCount { get; set; }

        //TCP Server if it's running
        public static bool IsRunning { get; set; }

        //Listening IP address
        public static IPAddress FFServerAddress { get; set; }

        //Listening port
        public static int FFServerPort { get; set; }

        //Launch TCP Server
        public static void StartFFTcpServerListening(int serverPort)
        {
            string tmpRecData = string.Empty;
            if (!IsRunning)
            {
                _listenerThread = new Thread(() =>
                {
                    Start(10);
                });

                try
                {
                    _listenerThread.SetApartmentState(ApartmentState.STA);
                    _listenerThread.Start();
                }
                catch (Exception ex)
                {
                    TCPHelper.WriteDataToLocalFiles(ex.Message + ex.StackTrace);
                    OnFFServerException(ex);
                }
            }
        }

        private static void GenObjectInstance()
        {
            if (m_Listener == null)
            {
                m_Clients = new Dictionary<long, TCPClientSession>(64);
                m_Listener = new TcpListener(IPAddress.Any, FFServerPort);
                m_Listener.Server.SendBufferSize = NetworkDefine.SocketSendBufSize;
                m_Listener.Server.ReceiveBufferSize = NetworkDefine.SocketReceBufSize;
                m_Listener.Server.NoDelay = NetworkDefine.UseNoDelay;
            }
        }

        public static void Start()
        {
            try
            {
                GenObjectInstance();
                if (!IsRunning)
                {
                    m_Listener.Start();
                    m_Listener.BeginAcceptTcpClient(HandleTcpClientAccepted, m_Listener);
                    IsRunning = true;
                    //Trigger server started notification event
                    OnServerStarted();
                }
            }
            catch (Exception ex)
            {
                TCPHelper.WriteDataToLocalFiles(ex.Message + ex.StackTrace);
                OnFFServerException(ex);
            }
        }

        //Launch TCP Server
        public static void Start(int backlog)
        {
            try
            {
                GenObjectInstance();
                if (!IsRunning)
                {
                    m_Listener.Start(backlog);
                    m_Listener.BeginAcceptTcpClient(HandleTcpClientAccepted, m_Listener);
                    IsRunning = true;
                    //Trigger server started notification event
                    OnServerStarted();
                }
            }
            catch (Exception ex)
            {
                TCPHelper.WriteDataToLocalFiles(ex.Message + ex.StackTrace);
                OnFFServerException(ex);
            }
        }

        //Stop TCP Server
        public static void Stop()
        {
            try
            {
                if (IsRunning)
                {
                    IsRunning = false;
                    m_Listener.Stop();
                    lock (m_Clients)
                    {
                        //Close all client connection
                        CloseAllClient();
                    }
                }
            }
            catch (Exception ex)
            {
                TCPHelper.WriteDataToLocalFiles(ex.Message + ex.StackTrace);
                OnFFServerException(ex);
            }
        }

        //Handle client connection
        private static void HandleTcpClientAccepted(IAsyncResult ar)
        {
            try
            {
                if (IsRunning)
                {
                    var _Client = m_Listener.EndAcceptTcpClient(ar);
                    if (m_ClientCount > NetworkDefine.MaxConnect)
                    {
                        _Client.Close();
                        return;
                    }

                    var _Session = new TCPClientSession(_Client, m_ClientID++);
                    lock (m_Clients)
                    {
                        m_ClientCount++;
                        m_Clients.Add(_Session.ClientID, _Session);
                        OnClientConnected(_Session);
                    }

                    var _Stream = _Session.NetworkStream;
                    //start to read data asynchronously
                    _Stream.BeginRead(_Session.Buffer, 0, _Session.Buffer.Length, HandleDataReceived, _Session);

                    m_Listener.BeginAcceptTcpClient(HandleTcpClientAccepted, ar.AsyncState);
                }
            }
            catch (Exception ex)
            {
                TCPHelper.WriteDataToLocalFiles(ex.Message + ex.StackTrace);
                OnFFServerException(ex);
            }
        }

        //callback function to handle receive data
        private static void HandleDataReceived(IAsyncResult ar)
        {
            if (IsRunning)
            {
                var _Session = (TCPClientSession)ar.AsyncState;
                var _Stream = _Session.NetworkStream;

                try
                {
                    var _RecvSize = _Stream.EndRead(ar);
                    if (_RecvSize == 0) //it's disconnected
                        lock (m_Clients)
                        {
                            m_Clients.Remove(_Session.ClientID);
                            //trigger client disconnected event
                            OnClientDisconnected(_Session);
                            return;
                        }

                    s_AllReceBytes += _RecvSize;
                    //save message and continue to receive message
                    _Session.SaveMsgToBuffer(_RecvSize);
                    //trigger data received event
                    OnDataReceived(_Session);
                    string recData = _Session.ReadReceivedData();
                    TCPHelper.WriteDataToLocalFiles(string.Format("Received Data [{0}]", recData));

                    //if (!IsPause)
                    //{
                    //    OnDataReceived(_Session);
                    //    string recData = _Session.ReadReceivedData();
                    //    TCPHelper.WriteDataToLocalFiles(string.Format("Received Data [{0}]", recData));
                    //}
                    //else
                    //{
                    //    string recData = _Session.ReadReceivedData();
                    //    TCPHelper.WriteDataToLocalFiles(string.Format("Received Data [{0}]", recData));
                    //}

                    _Stream.BeginRead(_Session.Buffer, 0, _Session.Buffer.Length, HandleDataReceived, _Session);
                }
                catch (Exception ex)
                {
                    lock (m_Clients)
                    {
                        m_Clients.Remove(_Session.ClientID);
                        //trigger client disconnected event
                        OnClientDisconnected(_Session);
                        OnFFServerException(ex);
                        TCPHelper.WriteDataToLocalFiles(ex.Message + ex.StackTrace);
                        return;
                    }
                }


            }
        }

        //send data
        public static void Send(IPEndPoint localEP, byte[] data)
        {
            TcpClient tcpClient = new TcpClient(localEP);
            Send(tcpClient, data, data.Length);
        }

        public static void Send(TCPClientSession session, byte[] data)
        {
            Send(session.TcpClient, data, data.Length);
        }

        public static void Send(TCPClientSession session, string data)
        {
            byte[] recData = Encoding.UTF8.GetBytes(data);
            Send(session.TcpClient, recData, recData.Length);
        }

        public static void Send(TCPClientSession session, byte[] data, int pLen)
        {
            Send(session.TcpClient, data, pLen);
        }

        //Send data to specified port asynchronously
        public static void Send(TcpClient client, byte[] data, int pLen)
        {
            if (!IsRunning)
                throw new InvalidProgramException("This TCP Socket server has not been started.");

            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (data == null)
                throw new ArgumentNullException(nameof(data));

            s_AllSendBytes += pLen;
            client.GetStream().BeginWrite(data, 0, pLen, SendDataEnd, client);
        }

        //send data completion callback function
        private static void SendDataEnd(IAsyncResult ar)
        {
            try
            {
                ((TcpClient)ar.AsyncState).GetStream().EndWrite(ar);
            }
            catch (Exception ex)
            {

            }
        }

        //established connection event
        public delegate void ClientConnectedHandler(TCPClientSession session);
        public static event ClientConnectedHandler ClientConnected;
        private static void OnClientConnected(TCPClientSession session)
        {
            ClientConnected?.Invoke(session);
        }

        //disconnection event
        public delegate void ClientDisconnectedHandler(TCPClientSession session);
        public static event ClientDisconnectedHandler ClientDisconnected;
        private static void OnClientDisconnected(TCPClientSession session)
        {
            m_ClientCount--;
            session.EventMsg = "Connection disconnected";
            ClientDisconnected?.Invoke(session);
        }

        //Server launched
        public delegate void ServerStartedHandler(int port);
        public static event ServerStartedHandler ServerStarted;
        private static void OnServerStarted()
        {
            ServerStarted?.Invoke(FFServerPort);
        }

        //Received data event
        public delegate void DataReceivedHandler(TCPClientSession session);
        public static event DataReceivedHandler DataReceived;
        private static void OnDataReceived(TCPClientSession session)
        {
            DataReceived?.Invoke(session);
            TCPHelper.WriteDataToLocalFiles(session.ReceivedMsg);
        }

        //Client is closed event
        public delegate void ClientClosedHandler(TCPClientSession session);
        public static event ClientClosedHandler ClientClosed;
        private static void OnClientClosed(TCPClientSession session)
        {
            m_ClientCount--;
            ClientClosed?.Invoke(session);
        }

        //server exception event
        public delegate void FFServerExceptionHandler(Exception exception);
        public static event FFServerExceptionHandler FFServerException;
        private static void OnFFServerException(Exception exception)
        {
            FFServerException?.Invoke(exception);
        }

        //Close one client session
        public static void Close(TCPClientSession pSession, bool pRemove = true)
        {
            if (pSession != null)
            {
                pSession.Close();
                if (pRemove)
                    m_Clients.Remove(pSession.ClientID);
                OnClientClosed(pSession);
            }
        }

        //close all client session and disconnected with all client
        public static void CloseAllClient()
        {
            foreach (var _Session in m_Clients)
                Close(_Session.Value, false);
            m_ClientCount = 0;
            m_Clients.Clear();
        }

        private static void ProcessToDispose(bool pDisposing)
        {
            if (!disposed)
            {
                if (pDisposing)
                    try
                    {
                        Stop();
                        m_Listener = null;
                        _listenerThread.Abort();
                    }
                    catch (SocketException)
                    {
                    }

                disposed = true;
            }
        }
    }
}