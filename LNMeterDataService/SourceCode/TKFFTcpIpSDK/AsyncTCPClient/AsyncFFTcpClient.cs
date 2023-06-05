using System;
using System.Text;
using Microsoft.VisualBasic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TKFFTcpIpSDK.Infrastructure
{
    public class AsyncFFTcpClient : IDisposable
    {
        private bool disposedValue = false;        // To detect redundant calls

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                }
            }
            this.disposedValue = true;
        }

        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private ManualResetEvent connectDone = new ManualResetEvent(false);
        private ManualResetEvent sendDone = new ManualResetEvent(false);
        private ManualResetEvent receiveDone = new ManualResetEvent(false);

        private string m_HostServerIP = String.Empty;
        private int m_HostServerPort = 8080;
        private string m_CommandType = String.Empty;
        private string m_Command = String.Empty;
        private string m_ReceivedData = String.Empty;

        public string ReceivedData
        {
            get
            {
                return m_ReceivedData;
            }
            set
            {
                m_ReceivedData = value;
            }
        }

        public class StateObject
        {
            public Socket workSocket = null;
            public const int BufferSize = 1024 * 2;
            public byte[] buffer = new byte[2048];
            public StringBuilder sb = new StringBuilder();
        }

        public AsyncFFTcpClient(string hostIP, int hostPort, string command)
        {
            try
            {
                m_HostServerIP = hostIP;
                m_HostServerPort = hostPort;
                m_Command = command;
            }
            catch (Exception ex)
            {
                modGlobalLogMgr.WriteLogAsy(ex);
            }
        }

        private bool m_CompletedToConnection = false;
        private bool m_CompletedToReceive = false;

        public bool FireMeterClient(byte[] outputCmd)
        {
            bool flag = false;
            string hostIP = this.m_HostServerIP;
            int hostPort = this.m_HostServerPort;
            string command = this.m_Command;

            System.Net.IPAddress ipaddress = null;
            try
            {
                ipaddress = System.Net.IPAddress.Parse(hostIP.Trim());
            }
            catch (SocketException ex)
            {
                modGlobalLogMgr.WriteLogAsy(ex);
                return false;
            }
            catch (Exception ex)
            {
                modGlobalLogMgr.WriteLogAsy(ex);
                return false;
            }
            finally
            {
            }

            IPEndPoint remoteEP = new IPEndPoint(ipaddress, hostPort);
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);
                connectDone.WaitOne(5 * 1000);

                if (client.Connected)
                {
                    ProcessToSend(client, outputCmd);
                    sendDone.WaitOne(200);

                    ProcessToReceive(client);
                    receiveDone.WaitOne(200);
                }

                flag = true;
            }
            catch (SocketException ex)
            {
                modGlobalLogMgr.WriteLogAsy(ex);
            }
            catch (Exception ex)
            {
                modGlobalLogMgr.WriteLogAsy(ex);
            }
            finally
            {
                try
                {
                    if (client != null & (m_CompletedToReceive == true | m_CompletedToConnection == true))
                    {
                        client.Close();
                        client = null;
                    }
                }
                catch (Exception ex)
                {
                    modGlobalLogMgr.WriteLogAsy(ex);
                }
            }

            return flag;
        }


        private void ConnectCallback(IAsyncResult ar)
        {
            Socket client = (Socket)ar.AsyncState;
            try
            {
                client.EndConnect(ar);
                m_CompletedToConnection = true;
            }
            catch (ObjectDisposedException ex)
            {
                modGlobalLogMgr.WriteLogAsy(ex);
            }
            catch (SocketException ex)
            {
                modGlobalLogMgr.WriteLogAsy(ex);
            }
            catch (Exception ex)
            {
                modGlobalLogMgr.WriteLogAsy(ex);
            }
            finally
            {
                connectDone.Set();
            }
        }

        private void ProcessToSend(Socket client, byte[] cmd)
        {
            try
            {
                if (client.Connected)
                {
                    client.BeginSend(cmd, 0, cmd.Length, 0, new AsyncCallback(SendCallback), client);
                }
            }
            catch (SocketException ex)
            {
                modGlobalLogMgr.WriteLogAsy(ex);
            }
            catch (Exception ex)
            {
                modGlobalLogMgr.WriteLogAsy(ex);
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            Socket client = (Socket)ar.AsyncState;
            int bytesSent = 0;
            try
            {
                bytesSent = client.EndSend(ar);
            }
            catch (ObjectDisposedException ex)
            {
                modGlobalLogMgr.WriteLogAsy(ex);
            }
            catch (Exception ex)
            {
                modGlobalLogMgr.WriteLogAsy(ex);
            }
            finally
            {
                sendDone.Set();
            }
        }


        private void ProcessToReceive(Socket client)
        {
            try
            {
                if (client.Connected)
                {
                    StateObject state = new StateObject();
                    state.workSocket = client;
                    client.ReceiveTimeout = 10 * 1000;
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                }
            }
            catch (SocketException ex)
            {
                modGlobalLogMgr.WriteLogAsy(ex);
            }
            catch (Exception ex)
            {
                modGlobalLogMgr.WriteLogAsy(ex);
            }
            finally
            {
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket client = state.workSocket;

            try
            {
                int bytesRead = 0;

                try
                {
                    bytesRead = client.EndReceive(ar);
                }
                catch (ObjectDisposedException ex)
                {
                    receiveDone.Set();
                    m_CompletedToReceive = true;
                    modGlobalLogMgr.WriteLogAsy(ex);
                    return;
                }
                catch (SocketException ex)
                {
                    receiveDone.Set();
                    modGlobalLogMgr.WriteLogAsy(ex);
                    m_CompletedToReceive = true;
                    return;
                }
                catch (Exception ex)
                {
                    receiveDone.Set();
                    modGlobalLogMgr.WriteLogAsy(ex);
                    m_CompletedToReceive = true;
                    return;
                }

                if (bytesRead > 0)
                {
                    string data = Encoding.UTF8.GetString(state.buffer, 0, bytesRead);
                    modGlobalLogMgr.WriteLogAsy("Received Data:" + data);
                    state.sb.Append(data);
                    modGlobalLogMgr.WriteLogAsy("Received bytesRead:" + bytesRead.ToString());

                    string KWh = state.buffer[3].ToString() + state.buffer[4].ToString() + state.buffer[5].ToString() + state.buffer[6].ToString();
                    modGlobalLogMgr.WriteLogAsy("Received KWh(0x):" + KWh);

                    int int_KWh = Convert.ToInt32(KWh, 16);
                    modGlobalLogMgr.WriteLogAsy("Received KWh(int):" + int_KWh.ToString());

                    string recData = "";
                    for (int i = 0; i <= bytesRead - 1; i++)
                    {
                        string rcr = Conversion.Hex(state.buffer[i]);
                        if (rcr.Length == 1)
                            rcr = rcr.PadLeft(2, '0');
                        recData = recData + rcr + " ";
                    }
                    modGlobalLogMgr.WriteLogAsy("Received Data(Hex):" + recData);

                    if (true)
                    {
                        m_CompletedToReceive = true;
                        m_ReceivedData = state.sb.ToString();
                        this.ReceivedData = m_ReceivedData;

                        try
                        {
                            // SaveReceivedDataIntoDB()
                        }
                        catch (Exception ex)
                        {
                            modGlobalLogMgr.WriteLogAsy(ex);
                        }
                        finally
                        {
                            receiveDone.Set();
                        }
                    }
                    else
                    {
                        client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                    }
                }
                else
                    receiveDone.Set();
            }
            catch (SocketException ex)
            {
                modGlobalLogMgr.WriteLogAsy(ex);
            }
            catch (Exception ex)
            {
                modGlobalLogMgr.WriteLogAsy(ex);
            }
            finally
            {
                if (client != null & m_CompletedToReceive)
                {
                    try
                    {
                        client.Shutdown(SocketShutdown.Both);
                    }
                    catch (ObjectDisposedException ex)
                    {
                    }

                    try
                    {
                        client.Close();
                    }
                    catch (ObjectDisposedException ex)
                    {
                        modGlobalLogMgr.WriteLogAsy(ex);
                    }
                    catch (SocketException ex)
                    {
                        modGlobalLogMgr.WriteLogAsy(ex);
                    }
                    catch (Exception ex)
                    {
                        modGlobalLogMgr.WriteLogAsy(ex);
                    }
                }
            }
        }

        static void Connect(String hostIP, int hostPort, byte[] outMsg, ref byte[] recMsg)
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer
                // connected to the same address as specified by the server, port
                // combination.

                // Prefer a using declaration to ensure the instance is Disposed later.
                using (TcpClient client = new TcpClient(hostIP, hostPort))
                {
                    // Translate the passed message into ASCII and store it as a Byte array.
                    // Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                    // Get a client stream for reading and writing.
                    NetworkStream stream = client.GetStream();

                    // Send the message to the connected TcpServer.
                    stream.Write(outMsg, 0, outMsg.Length);

                    // Receive the server response.

                    // Buffer to store the response bytes.
                    Byte[] data = new Byte[256];

                    // String to store the response ASCII representation.
                    String responseData = String.Empty;

                    // Read the first batch of the TcpServer response bytes.
                    Int32 bytes = stream.Read(data, 0, data.Length);
                    recMsg = data;

                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Console.WriteLine("Received: {0}", responseData);

                    // Explicit close is not necessary since TcpClient.Dispose() will be
                    // called automatically.
                    // stream.Close();
                    // client.Close();
                }
            }
            catch (ArgumentNullException e)
            {
                modGlobalLogMgr.WriteLogAsy(e);
            }
            catch (SocketException e)
            {
                modGlobalLogMgr.WriteLogAsy(e);
            }
        }
    }
}
