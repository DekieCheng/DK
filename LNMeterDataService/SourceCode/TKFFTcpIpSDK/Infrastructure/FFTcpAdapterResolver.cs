using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace TKFFTcpIpSDK.Infrastructure
{
    public delegate void RemoteHostExceptionHandler(object ex);
    public delegate void HeartBeatExceptionHandler(object ex);
    public delegate void FFServerExceptionHandler(object ex);
    public delegate void DataArrivedHandler(FFDataArrivedArgs fDataArrivedObj);
    public delegate void DataArrivedfromServerHandler(string incomingData);

    public static class FFTcpAdapterResolver
    {
        private static Thread _listenerThread;
        private static TcpListener _tcpListener;
        private static bool _listening;

        private static IPAddress _outgoingIPAddress;
        private static int _outgoingPort;
        private static bool _writedatatolocalfile;
        private static int _heartbeatIntervalTime = 30;
        private static string _heartbeatcallValues = "";
        private static string _udpToGetheartbeatcall = "";
        private static string _udpToGetResponseValue = "";

        private static Timer _ffCheckTimer;
        private static bool _FFServerIsStopped = false;
        private static bool _heartbeatStopped = false;
        private static Hashtable _objectStore = null;
        private static bool _isrunning = false;
        public static event RemoteHostExceptionHandler OnRemoteHostException;
        public static event HeartBeatExceptionHandler OnHeartBeatException;
        public static event FFServerExceptionHandler OnFFServerException;
        public static event DataArrivedHandler OnDataArrived;
        public static event DataArrivedfromServerHandler OnDataArrivedfromServer;

        public static IPAddress OutgoingIPAddress
        {
            get { return _outgoingIPAddress; }
            set { _outgoingIPAddress = value; }
        }
        public static int OutgoingPort
        {
            get { return _outgoingPort; }
            set { _outgoingPort = value; }
        }
        public static bool WriteDataToLocalFile
        {
            get { return _writedatatolocalfile; }
            set { _writedatatolocalfile = value; }
        }
        public static int HeatbeatIntervalTime
        {
            get { return _heartbeatIntervalTime; }
            set { _heartbeatIntervalTime = value; }
        }
        public static string HeartBeatCallValues
        {
            get { return _heartbeatcallValues; }
            set { _heartbeatcallValues = value; }
        }
        public static bool FFServerIsStopped
        {
            get { return _FFServerIsStopped; }
            set { _FFServerIsStopped = value; }
        }
        public static string UDPToGetHeartBeatCallValues
        {
            get { return _udpToGetheartbeatcall; }
            set { _udpToGetheartbeatcall = value; }
        }
        public static string UDPToGetReponseValues
        {
            get { return _udpToGetResponseValue; }
            set { _udpToGetResponseValue = value; }
        }

        static FFTcpAdapterResolver()
        {
            Application.ApplicationExit += new EventHandler(OnApplicationExit);
            _listening = false;
            _FFServerIsStopped = false;
        }

        private static void OnApplicationExit(object sender, EventArgs e)
        {
            // When the application is exiting, write the application data to the user file and close it.
            try
            {
                if (!FFServerIsStopped)
                {
                    StopFFTcpServer();
                }
            }
            catch (Exception ex)
            {
                TCPHelper.WriteDataToLocalFiles(ex.Message + ex.StackTrace);
            }
        }

        public static void StartFFTcpServerListening(int serverPort)
        {
            string tmpRecData = string.Empty;
            _FFServerIsStopped = false;

            if (!_listening)
            {
                _listening = true;
                _listenerThread = new Thread(() =>
                {
                    _tcpListener = new TcpListener(IPAddress.Any, serverPort);
                    _tcpListener.Start();

                    while (_listening)
                    {
                        if (_FFServerIsStopped) { continue; }
                        try
                        {
                            using (System.Net.Sockets.TcpClient client = _tcpListener.AcceptTcpClient())
                            {
                                using (NetworkStream stream = client.GetStream())
                                {
                                    using (var srReader = new StreamReader(stream, Encoding.UTF8))
                                    {
                                        char[] recBuffer = new char[1024 * 10];
                                        srReader.Read(recBuffer, 0, 1024 * 10);
                                        tmpRecData = new string(recBuffer).Trim('\0');

                                        if (!string.IsNullOrEmpty(tmpRecData))
                                        {
                                            try
                                            {
                                                OnDataArrived?.Invoke(new FFDataArrivedArgs
                                                {
                                                    ArrivedData = tmpRecData,
                                                    SourceClient = client.Client,
                                                    MyRemoteEndPoint = client.Client.RemoteEndPoint,
                                                    MyLocalEndPoint = client.Client.LocalEndPoint,
                                                }); ;
                                            }
                                            catch (Exception ex)
                                            {
                                                TCPHelper.WriteDataToLocalFiles(ex.Message + ex.StackTrace);
                                                OnFFServerException?.Invoke(ex);
                                            }
                                            TCPHelper.WriteDataToLocalFiles(string.Format("Received data [{0}] from client[{1}].", tmpRecData, client.Client.RemoteEndPoint.ToString()));
                                        }

                                        //Send response to the client
                                        try
                                        {
                                            byte[] respBuffer = Encoding.UTF8.GetBytes(SetResponseACKValues(tmpRecData));
                                            stream.Write(respBuffer, 0, respBuffer.Length);
                                        }
                                        catch (Exception ex)
                                        {
                                            TCPHelper.WriteDataToLocalFiles(ex.Message + ex.StackTrace);
                                            OnFFServerException?.Invoke(ex);
                                        }
                                    }
                                }
                            }
                        }
                        catch (SocketException sex)
                        {
                            TCPHelper.WriteDataToLocalFiles(sex.Message + sex.StackTrace + sex.SocketErrorCode.ToString());
                            //OnFFServerException?.Invoke(sex);
                        }
                        catch (ThreadAbortException tae)
                        {
                            TCPHelper.WriteDataToLocalFiles(tae.Message + tae.StackTrace);
                        }
                        catch (Exception ex)
                        {
                            TCPHelper.WriteDataToLocalFiles(ex.Message + ex.StackTrace);
                            OnFFServerException?.Invoke(ex);
                        }
                    }
                });

                try
                {
                    _listenerThread.Start();
                }
                catch (Exception ex)
                {
                    TCPHelper.WriteDataToLocalFiles(ex.Message + ex.StackTrace);
                    OnFFServerException?.Invoke(ex);
                }
            }
        }

        public static void StopFFTcpServer()
        {
            try
            {
                _FFServerIsStopped = true;
                _listening = false;
                if (_tcpListener != null)
                {
                    _tcpListener.Stop();
                    _tcpListener = null;
                }

                if (_listenerThread != null)
                {
                    _listenerThread.Abort();
                    _listenerThread = null;
                }
            }
            catch (Exception ex)
            {
                TCPHelper.WriteDataToLocalFiles(ex.Message + Environment.NewLine + ex.StackTrace);
                OnFFServerException?.Invoke(ex);
            }
        }

        public static void StartHostHeartbeat()
        {
            try
            {
                _heartbeatStopped = false;
                if (_heartbeatIntervalTime > 0)
                {
                    if (_ffCheckTimer == null)
                    {
                        _ffCheckTimer = new Timer(new TimerCallback(StartUpHostHeartbeat), null, _heartbeatIntervalTime * 400, _heartbeatIntervalTime * 1000);
                    }
                    else
                    {
                        _ffCheckTimer.Change(_heartbeatIntervalTime * 400, _heartbeatIntervalTime * 1000);
                    }
                }
            }
            catch (Exception ex)
            {
                TCPHelper.WriteDataToLocalFiles(ex.Message + ex.StackTrace);
            }
        }

        public static void StopHostHeartbeat()
        {
            try
            {
                _heartbeatStopped = true;
                if (_ffCheckTimer != null)
                {
                    _ffCheckTimer.Change(-1, Timeout.Infinite);
                }
            }
            catch (Exception ex)
            {
                TCPHelper.WriteDataToLocalFiles(ex.Message + ex.StackTrace);
            }
        }

        public static bool SendDataToRemoteHost(string outgoingData)
        {
            bool flag = false;
            try
            {
                // flag = SendDataToRemoteHost(_outgoingIPAddress.ToString(), _outgoingPort, outgoingData);
            }
            catch (Exception ex)
            {
                TCPHelper.WriteDataToLocalFiles(ex.Message + ex.StackTrace);
                OnRemoteHostException?.Invoke(ex);
                throw ex;
            }
            return flag;
        }

        public static bool SendDataToRemoteHost(Socket client, string outgoingData)
        {
            bool flag = false;
            try
            {
                if (client != null)
                {
                    string clientip = ((IPEndPoint)client.RemoteEndPoint).Address.ToString();
                    int clientport = ((IPEndPoint)client.RemoteEndPoint).Port;
                    //    flag = SendDataToRemoteHost(clientip, clientport, outgoingData);
                }
            }
            catch (Exception ex)
            {
                TCPHelper.WriteDataToLocalFiles(ex.Message + ex.StackTrace);
                if (OnRemoteHostException != null)
                {
                    OnRemoteHostException?.Invoke(ex);
                }
                throw ex;
            }
            return flag;
        }
        public static bool SendDataToRemoteHost(Socket client, byte[] destCmd)
        {
            bool flag = false;
            try
            {
                if (client != null)
                {
                    string clientip = ((IPEndPoint)client.RemoteEndPoint).Address.ToString();
                    int clientport = ((IPEndPoint)client.RemoteEndPoint).Port;
                    //    flag = SendDataToRemoteHost(clientip, clientport, outgoingData);
                }
            }
            catch (Exception ex)
            {
                TCPHelper.WriteDataToLocalFiles(ex.Message + ex.StackTrace);
                if (OnRemoteHostException != null)
                {
                    OnRemoteHostException?.Invoke(ex);
                }
                throw ex;
            }
            return flag;
        }

        public static bool SendDataToRemoteHost(string outgoingIP, int outgoingPort, byte[] destCmd)
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
                        client.ReceiveTimeout = 1000 * 30;
                        client.SendTimeout = 1000 * 30;
                        using (var stream = client.GetStream())
                        {
                            //Send data to remote host
                            try
                            {
                                //byte[] senderbuffer = new byte[outgoingData.Length];
                                //senderbuffer = Encoding.UTF8.GetBytes(outgoingData);
                                //stream.Write(senderbuffer, 0, senderbuffer.Length);
                                stream.Write(destCmd, 0, destCmd.Length);
                                // stream.Write(cmd, 0, cmd.Length);
                            }
                            catch (Exception ex)
                            { throw ex; }

                            //Receive response from remote host
                            try
                            {
                                while (true)
                                {
                                    if (!stream.CanRead) { continue; }

                                    byte[] recbuffer = new byte[1024 * 5];
                                    int readByte = stream.Read(recbuffer, 0, recbuffer.Length);
                                    string msgfromSvr = TcpParserReceivedData(recbuffer);

                                    if (!string.IsNullOrEmpty(msgfromSvr))
                                    {
                                        string recData = "";
                                        for (int idx = 0; idx < readByte; idx++)
                                        {
                                            string rcr = recbuffer[idx].ToString("x");
                                            if (rcr.Length == 1) { rcr = rcr.PadLeft(2, '0'); }
                                            recData = recData + rcr + " ";
                                        }
                                        TCPHelper.WriteDataToLocalFiles(string.Format("Get Response Message from Server [{0}{1}]:{2}", outgoingIP, outgoingPort, recData));

                                        //WriteLogAsy("Received bytesRead:" & bytesRead.ToString)
                                        //Dim recData As String = ""
                                        //For i As Integer = 0 To bytesRead -1
                                        //    Dim rcr As String = Hex(state.buffer(i))
                                        //    If rcr.Length = 1 Then rcr = rcr.PadLeft(2, "0")
                                        //    recData = recData & rcr & Space(1)
                                        //Next
                                        //WriteLogAsy("Received Data(Hex):" & recData)

                                        TCPHelper.WriteDataToLocalFiles(string.Format("Get Response Message from Server [{0}{1}]:{2}", outgoingIP, outgoingPort, msgfromSvr));
                                        OnDataArrivedfromServer?.Invoke(msgfromSvr);
                                        break;
                                    }
                                    else
                                    {
                                        TCPHelper.WriteDataToLocalFiles(string.Format("Disconnected from Server [{0}{1}]", outgoingIP, outgoingPort));
                                        break;
                                    }
                                    // break;
                                }
                            }
                            catch (Exception ex)
                            { throw ex; }
                        }
                    }
                    flag = true;
                }
                catch (IOException iex)
                {
                    TCPHelper.WriteDataToLocalFiles(iex.Message + Environment.NewLine + iex.StackTrace + Environment.NewLine);
                    OnRemoteHostException?.Invoke(iex);
                    throw iex;
                }
                catch (SocketException sex)
                {
                    TCPHelper.WriteDataToLocalFiles(sex.Message + Environment.NewLine + sex.StackTrace + Environment.NewLine + ":" + sex.SocketErrorCode.ToString());
                    OnRemoteHostException?.Invoke(sex);
                    throw sex;
                }
                catch (Exception ex)
                {
                    TCPHelper.WriteDataToLocalFiles(ex.Message + Environment.NewLine + ex.StackTrace);
                    OnRemoteHostException?.Invoke(ex);
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

        public static void AddCustomData(string key, object data)
        {
            if (_objectStore == null) { _objectStore = new Hashtable(); }

            if (_objectStore.ContainsKey(key))
            {
                _objectStore[key] = data;
            }
            else
            {
                _objectStore.Add(key, data);
            }
        }

        public static object GetCustomData(string key)
        {
            if (!_objectStore.ContainsKey(key)) { return null; }

            return _objectStore[key];
        }

        public static void RemoveCustomData(string key)
        {
            if (_objectStore != null)
            {
                if (_objectStore.ContainsKey(key)) { _objectStore.Remove(key); }
            }
        }

        public static void RemoveCustomData(bool removeAllCustomData)
        {
            _objectStore.Clear();
        }

        private static void StartUpHostHeartbeat(object sender)
        {
            try
            {
                if (_heartbeatStopped) { return; }
                if (_outgoingIPAddress == null)
                {
                    TCPHelper.WriteDataToLocalFiles("Out-going IP Address is mandatory.");
                    return;
                }

                try
                {
                    _heartbeatcallValues = SetHeartBeatCallValues();
                    if (string.IsNullOrEmpty(_heartbeatcallValues)) { return; }
                }
                catch
                { return; }

                using (var client = new System.Net.Sockets.TcpClient(System.Net.Sockets.AddressFamily.InterNetwork))
                {
                    try
                    {
                        client.ReceiveTimeout = 500;
                        //keep-alive
                        client.Client.IOControl(IOControlCode.KeepAliveValues, GetKeepAliveData(), null);
                        client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);

                        client.Connect(_outgoingIPAddress, _outgoingPort);

                        if (client.Connected)
                        {
                            TCPHelper.WriteDataToLocalFiles(string.Format("Connected to remote server [{0}:{1}]successfully.",
                                    _outgoingIPAddress.ToString(), _outgoingPort.ToString()));

                            TCPHelper.WriteDataToLocalFiles(_heartbeatcallValues);

                            using (var stream = client.GetStream())
                            {
                                byte[] buffer = Encoding.UTF8.GetBytes(_heartbeatcallValues);
                                stream.Write(buffer, 0, buffer.Length);

                                byte[] recbuffer = new byte[256];
                                string receivedRESP = "";
                                while (true)
                                {
                                    stream.Read(recbuffer, 0, recbuffer.Length);
                                    receivedRESP = TcpParserReceivedData(recbuffer);
                                    if (!string.IsNullOrEmpty(receivedRESP)) { break; }
                                    else break;
                                }
                                TCPHelper.WriteDataToLocalFiles(receivedRESP);
                                client.Client.Disconnect(false);
                            }
                            client.Close();
                        }
                    }
                    catch (IOException iex)
                    {
                        TCPHelper.WriteDataToLocalFiles(iex.Message + Environment.NewLine + iex.StackTrace);
                        OnHeartBeatException?.Invoke(iex);
                    }
                    catch (SocketException sex)
                    {
                        TCPHelper.WriteDataToLocalFiles(sex.Message + Environment.NewLine + sex.StackTrace + Environment.NewLine + sex.SocketErrorCode.ToString());
                        OnHeartBeatException?.Invoke(sex);
                    }
                    catch (Exception ex)
                    {
                        TCPHelper.WriteDataToLocalFiles(ex.Message + Environment.NewLine + ex.StackTrace);
                        OnHeartBeatException?.Invoke(ex);
                    }
                    finally
                    {
                        if (client != null)
                        {
                            client.Close();
                        }
                    }
                }
            }
            catch (SocketException sex)
            {
                TCPHelper.WriteDataToLocalFiles(sex.Message + Environment.NewLine + sex.StackTrace + Environment.NewLine + sex.SocketErrorCode.ToString());
                OnHeartBeatException?.Invoke(sex);
            }
            catch (Exception ex)
            {
                TCPHelper.WriteDataToLocalFiles(ex.Message + Environment.NewLine + ex.StackTrace);
                OnHeartBeatException?.Invoke(ex);
            }
        }

        private static string SetHeartBeatCallValues()
        {
            string heartbeatcallValues;
            try
            {
                heartbeatcallValues = UDPMgr.GetHeartBeartCallStringByUDP(UDPToGetHeartBeatCallValues, null, null, null, null, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return heartbeatcallValues;
        }

        private static string SetResponseACKValues(string receivedValues)
        {
            string ResponseValue;
            try
            {
                FlexFlow.XMLDetailBuilder xMLDetailBuilder = new FlexFlow.XMLDetailBuilder();
                xMLDetailBuilder.Add("Received", receivedValues);
                ResponseValue = UDPMgr.GetResponseACKByUDP(UDPToGetReponseValues, null, null, null, null, xMLDetailBuilder);
                if (string.IsNullOrEmpty(ResponseValue)) { ResponseValue = "Thanks from FlexFlow Server"; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ResponseValue;
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

        private static string TcpParserReceivedData(byte[] sourceDatabyte)
        {
            List<byte> newBytes = new List<byte>();
            foreach (byte b in sourceDatabyte)
            {
                if (b == 0x00) { continue; }
                newBytes.Add(b);
            }
            if (newBytes.Count == 0) { return string.Empty; }
            return Encoding.UTF8.GetString(newBytes.ToArray());
        }

        //public static void WriteDataToLocalFiles(string strData)
        //{
        //    if (!WriteDataToLocalFile) { return; }

        //    try
        //    {
        //        string folder = @"C:\temp\TKFFTcpIpSDKLog\";
        //        if (!Directory.Exists(folder))
        //        {
        //            Directory.CreateDirectory(folder);
        //        }
        //        string fileName = folder + string.Format("{0:yyyyMMdd}", DateTime.Now) + ".log";

        //        using (var fs = new FileStream(fileName, FileMode.OpenOrCreate, FileSystemRights.AppendData, FileShare.ReadWrite, 4096, FileOptions.Asynchronous))
        //        {
        //            using (var wr = new StreamWriter(fs))
        //            {
        //                wr.AutoFlush = true;
        //                wr.WriteLine(string.Format("{0:yyyy-MM-dd HH:mm:ss.ffff}", DateTime.Now));
        //                wr.WriteLine($"{strData}\r");
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    { }
        //}
    }
    public class FFDataArrivedArgs : System.EventArgs
    {
        string _ArrivedData = "";
        public string ArrivedData
        {
            get { return _ArrivedData; }
            set { _ArrivedData = value; }
        }

        Socket _sourceClient = null;
        public Socket SourceClient
        {
            get
            {
                return _sourceClient;
            }
            set
            {
                _sourceClient = value;
            }
        }

        public EndPoint MyLocalEndPoint { get; set; }
        public EndPoint MyRemoteEndPoint { get; set; }
    }
}