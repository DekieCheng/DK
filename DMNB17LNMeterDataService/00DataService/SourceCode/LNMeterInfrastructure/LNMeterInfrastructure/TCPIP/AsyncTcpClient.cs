﻿using System;
using System.Text;
using Microsoft.VisualBasic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using LNMeterInfrastructure.Common;
using System.IO;
using System.Runtime.InteropServices;

namespace TCPIP
{
    public class AsyncTcpClient : IDisposable
    {
        private bool disposedValue = false;

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

        private readonly string m_HostServerIP = String.Empty;
        private readonly int m_HostServerPort = 8080;
        private readonly string m_Command = null;
        private byte[] m_ReceivedData = null;

        public byte[] ReceivedData
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
        //public AsyncTcpClient(string hostIP, int hostPort, byte[] command)
        //{

        //}
        public AsyncTcpClient(string hostIP, int hostPort, string command)
        {
            try
            {
                m_HostServerIP = hostIP;
                m_HostServerPort = hostPort;
                m_Command = command;
            }
            catch (Exception ex)
            {
                LNLogMgr.WriteLogAsy(ex);
            }
        }

        private bool m_CompletedToConnection = false;

        public bool FireMeterClient()
        {
            bool flag = false;

            System.Net.IPAddress ipaddress = null;
            try
            {
                ipaddress = System.Net.IPAddress.Parse(m_HostServerIP.Trim());
            }
            catch (SocketException ex)
            {
                LNLogMgr.WriteLogAsy(ex);
                return false;
            }
            catch (Exception ex)
            {
                LNLogMgr.WriteLogAsy(ex);
                return false;
            }

            IPEndPoint remoteEP = new IPEndPoint(ipaddress, m_HostServerPort);
            using (Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);
                    connectDone.WaitOne(5 * 1000);

                    if (client.Connected)
                    {
                        byte[] outCmd = LNGlobal.HexStringToBytes(m_Command.Replace(" ", ""));
                        ProcessToSend(client, outCmd);
                        sendDone.WaitOne(6000);

                        ProcessToReceive(client);
                        receiveDone.WaitOne(3000);
                    }

                    flag = true;
                }
                catch (SocketException ex)
                {
                    LNLogMgr.WriteLogAsy(ex);
                }
                catch (Exception ex)
                {
                    LNLogMgr.WriteLogAsy(ex);
                }
                finally
                {
                    try
                    {
                        if (client != null & (m_CompletedToConnection == true))
                        {
                            client.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        LNLogMgr.WriteLogAsy(ex);
                    }
                }
            }

            return flag;
        }

        private byte[] ToGenByteArray(string command)
        {
            UTF8Encoding uTF8Encoding = new UTF8Encoding();
            byte[] cmd = new byte[8];
            string[] orgCmd = command.Split(new char[] { ' ' });
            for (int i = 0; i < 8; i++)
            {
                cmd[i] = Convert.ToByte(orgCmd[i]);
            }
            return cmd;
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
                LNLogMgr.WriteLogAsy(ex);
            }
            catch (SocketException ex)
            {
                LNLogMgr.WriteLogAsy(ex);
            }
            catch (Exception ex)
            {
                LNLogMgr.WriteLogAsy(ex);
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
                LNLogMgr.WriteLogAsy(ex);
            }
            catch (Exception ex)
            {
                LNLogMgr.WriteLogAsy(ex);
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
            catch (Exception ex)
            {
                LNLogMgr.WriteLogAsy(ex);
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
                    client.ReceiveTimeout = 30 * 1000;
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                }
            }
            catch (Exception ex)
            {
                LNLogMgr.WriteLogAsy(ex);
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket client = state.workSocket;

            try
            {
                int bytesRead = 0;
                SocketError _errorCode;
                try
                {
                    bytesRead = client.EndReceive(ar, out _errorCode);
                }
                catch (Exception ex)
                {
                    //receiveDone.Set();
                    //LNLogMgr.WriteLogAsy(ex);
                    return;
                }

                if (bytesRead > 0)
                {
                    //string data = Encoding.UTF8.GetString(state.buffer, 0, bytesRead);
                    //state.sb.Append(data);
                    //string KWh = state.buffer[3].ToString() + state.buffer[4].ToString() + state.buffer[5].ToString() + state.buffer[6].ToString();
                    //string KWhHex = Conversion.Hex(state.buffer[3].ToString()).PadLeft(2, '0')
                    //    + Conversion.Hex(state.buffer[4].ToString()).PadLeft(2, '0')
                    //    + Conversion.Hex(state.buffer[5].ToString()).PadLeft(2, '0')
                    //    + Conversion.Hex(state.buffer[6].ToString()).PadLeft(2, '0');
                    //int int_KWh = Convert.ToInt32(KWh, 16);
                    //LNLogMgr.WriteLogAsy(string.Format("Received data length:{0}, KWh(int):{1}, KWhHex:{8}, KWh(0x):{2}, Data(string): [{3}], Data(Hex):{4} from host [{5}:{6}] by command [{7}]",
                    //     bytesRead.ToString(), int_KWh.ToString(), KWh, data, recData, m_HostServerIP, m_HostServerPort.ToString(), m_Command, KWhHex));



                    if (bytesRead >= 0)
                    {
                        try
                        {
                            Array.Resize(ref m_ReceivedData, bytesRead);
                            Array.Copy(state.buffer, m_ReceivedData, bytesRead);

                            string recData = LNGlobal.BytesTohexString(state.buffer, bytesRead);
                            LNLogMgr.WriteLogAsy(string.Format("Arrived data length:[{0}], Data(Hex):[{1}] from host [{2}:{3}] by command [{4}]",
                                    bytesRead.ToString(), recData, m_HostServerIP, m_HostServerPort.ToString(), m_Command));

                            client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                        }
                        catch (Exception ex)
                        {
                            LNLogMgr.WriteLogAsy(ex);
                        }
                        finally
                        {
                            //  receiveDone.Set();
                        }
                    }
                    else
                    {

                    }
                }
                else
                    receiveDone.Set();
            }
            catch (Exception ex)
            {
                LNLogMgr.WriteLogAsy(ex);
            }
            finally
            {
                //if (client != null)
                //{
                //    try
                //    {
                //        client.Shutdown(SocketShutdown.Both);
                //        client.Close();
                //    }
                //    catch (Exception ex)
                //    {
                //        //  LNLogMgr.WriteLogAsy(ex);
                //    }
                //}
                receiveDone.Set();
            }
        }

        public bool SendDataToRemoteHost(string outgoingIP, int outgoingPort, string cmd)
        {
            bool flag = false;
            using (var client = new System.Net.Sockets.TcpClient(System.Net.Sockets.AddressFamily.InterNetwork))
            {
                //keep-alive
                client.Client.IOControl(IOControlCode.KeepAliveValues, GetKeepAliveData(), null);
                client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);

                try
                {
                    client.Connect(outgoingIP, outgoingPort);
                    if (client.Connected)
                    {
                        client.ReceiveTimeout = 1000 * 120;
                        client.SendTimeout = 1000 * 120;
                        using (var stream = client.GetStream())
                        {
                            //Send data to remote host
                            try
                            {
                                byte[] destCmd = LNGlobal.HexStringToBytes(cmd);
                                //byte[] senderbuffer = new byte[outgoingData.Length];
                                //senderbuffer = Encoding.UTF8.GetBytes(outgoingData);
                                //stream.Write(senderbuffer, 0, senderbuffer.Length);
                                stream.Write(destCmd, 0, destCmd.Length);
                                // stream.Write(cmd, 0, cmd.Length);
                            }
                            catch (Exception ex)
                            {
                                LNLogMgr.WriteLogAsy(ex);
                                throw ex;
                            }

                            //Receive response from remote host
                            try
                            {
                                while (true)
                                {
                                    if (!stream.CanRead) { continue; }

                                    byte[] recbuffer = new byte[512];
                                    int readByte = stream.Read(recbuffer, 0, recbuffer.Length);
                                    string msgfromSvr = LNGlobal.BytesTohexString(recbuffer);// TcpParserReceivedData(recbuffer);

                                    if (!string.IsNullOrEmpty(msgfromSvr))
                                    {
                                        //Array.Resize(ref m_ReceivedData, readByte);
                                        //Array.Copy(recbuffer, m_ReceivedData, readByte);
                                        string recData = LNGlobal.BytesTohexString(recbuffer, readByte);

                                        //string recData = "";
                                        //for (int idx = 0; idx < readByte; idx++)
                                        //{'
                                        //    string rcr = recbuffer[idx].ToString("x");
                                        //    if (rcr.Length == 1) { rcr = rcr.PadLeft(2, '0'); }
                                        //    recData = recData + rcr + " ";
                                        //}
                                        LNLogMgr.WriteLogAsy(string.Format("Received Data [{2}] from Server [{0}:{1}] by the command [{3}]", outgoingIP, outgoingPort, recData, cmd));
                                        //OnDataArrivedfromServer?.Invoke(msgfromSvr);
                                        break;
                                    }
                                    else
                                    {
                                        LNLogMgr.WriteLogAsy(string.Format("Disconnected from Server [{0}:{1}] by the command [{2}] ", outgoingIP, outgoingPort, cmd));
                                        break;
                                    }
                                    // break;
                                }
                            }
                            catch (IOException iex)
                            {
                                LNLogMgr.WriteLogAsy(iex);
                                throw iex;
                            }
                            catch (SocketException sex)
                            {
                                LNLogMgr.WriteLogAsy(sex);
                                throw sex;
                            }
                            catch (Exception ex)
                            {
                                LNLogMgr.WriteLogAsy(ex);
                                throw ex;
                            }
                        }
                    }
                    flag = true;
                }
                catch (IOException ex)
                {
                    LNLogMgr.WriteLogAsy(ex);
                    throw ex;
                }
                catch (SocketException ex)
                {
                    LNLogMgr.WriteLogAsy(ex);
                    throw ex;
                }
                catch (Exception ex)
                {
                    LNLogMgr.WriteLogAsy(ex);
                    throw ex;
                }
                finally
                {
                    if (client != null)
                    {
                        client.Close();
                    }
                }
                return flag;
            }
        }
        private static byte[] GetKeepAliveData()
        {
            uint dummy = 0;
            byte[] inOptionValues = new byte[Marshal.SizeOf(dummy) * 3];
            BitConverter.GetBytes((uint)1).CopyTo(inOptionValues, 0);
            BitConverter.GetBytes((uint)3000).CopyTo(inOptionValues, Marshal.SizeOf(dummy));
            BitConverter.GetBytes((uint)500).CopyTo(inOptionValues, Marshal.SizeOf(dummy) * 2);
            return inOptionValues;
        }

        public bool StartClient()
        {
            bool flag = false;
            // Data buffer for incoming data.  
            byte[] bytes = new byte[1024];

            // Connect to a remote device.  
            try
            {
                System.Net.IPAddress ipaddress = null;
                try
                {
                    ipaddress = System.Net.IPAddress.Parse(m_HostServerIP.Trim());
                }
                catch (SocketException ex)
                {
                    LNLogMgr.WriteLogAsy(ex);
                    return false;
                }
                catch (Exception ex)
                {
                    LNLogMgr.WriteLogAsy(ex);
                    return false;
                }

                IPEndPoint remoteEP = new IPEndPoint(ipaddress, m_HostServerPort);
                Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    sender.Connect(remoteEP);

                    int bytesSent = sender.Send(LNGlobal.HexStringToBytes(m_Command));

                    try
                    {
                        sender.ReceiveTimeout = 30 * 1000;
                        int bytesRec = sender.Receive(bytes);
                        Array.Resize(ref m_ReceivedData, bytesRec);
                        Array.Copy(bytes, m_ReceivedData, bytesRec);

                        string recData = LNGlobal.BytesTohexString(m_ReceivedData, bytesRec);
                        LNLogMgr.WriteLogAsy(string.Format("Arrived data length:[{0}], Data(Hex):[{1}] from host [{2}:{3}] by command [{4}]",
                                bytesRec.ToString(), recData, m_HostServerIP, m_HostServerPort.ToString(), m_Command));
                    }
                    catch (Exception ex)
                    {
                        LNLogMgr.WriteLogAsy(ex);
                    }
                    finally
                    {
                        // receiveDone.Set();
                    }

                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                    flag = true;
                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return flag;
        } // end of StartClient
    }
}
