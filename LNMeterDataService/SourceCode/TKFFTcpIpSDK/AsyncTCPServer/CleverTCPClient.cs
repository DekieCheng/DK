using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TKFFTcpIpSDK.Infrastructure
{
    //Refer to the solution: https://blog.csdn.net/aa989111337/article/details/127092479?spm=1001.2101.3001.6650.3&utm_medium=distribute.pc_relevant.none-task-blog-2%7Edefault%7EYuanLiJiHua%7EPosition-3-127092479-blog-116230920.pc_relevant_3mothn_strategy_recovery&depth_1-utm_source=distribute.pc_relevant.none-task-blog-2%7Edefault%7EYuanLiJiHua%7EPosition-3-127092479-blog-116230920.pc_relevant_3mothn_strategy_recovery

    public class CleverTCPClient
    {
        private static bool IsConnet = false;
        private static TcpClient _tcpClient;

        static CleverTCPClient()
        {
            //  Application.ApplicationExit += new EventHandler(OnApplicationExit);
            _tcpClient = new TcpClient(AddressFamily.InterNetwork);
            Thread thread = new Thread(() =>
            {
                Connet();
            });
            thread.IsBackground = true; //Set to a background thread and automatically releases it when the program exits
            thread.Start();
        }

        public static void Connet()
        {
            try
            {
                ConnectDestHost(TCPHelper.FFTcpIpSettingMgr.OutgoingIpAddress.ToString(), TCPHelper.FFTcpIpSettingMgr.OutgoingPort);
                // Connet(TCPHelper.FFTcpIpSettingMgr.OutgoingIpAddress.ToString(), TCPHelper.FFTcpIpSettingMgr.OutgoingPort);
            }
            catch (Exception ex)
            {
                TCPHelper.WriteDataToLocalFiles(ex.Message + ex.StackTrace);
            }
        }
        public static void Connet(EndPoint endPoint)
        {
            try
            {
                char[] spliter = new char[] { ':' };

                string ipstring = endPoint.ToString().Split(spliter)[0];
                string port = endPoint.ToString().Split(spliter)[1];

                ConnectDestHost(ipstring, int.Parse(port));
                // Connet(TCPHelper.FFTcpIpSettingMgr.OutgoingIpAddress.ToString(), TCPHelper.FFTcpIpSettingMgr.OutgoingPort);
            }
            catch (Exception ex)
            {
                TCPHelper.WriteDataToLocalFiles(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// will connect to the specified target server till it connected successfully
        /// </summary>
        /// <param name="destHostIP">the target ip address </param>
        /// <param name="Port">the target port number</param>
        public static void Connet(string destHostIP, int Port)
        {
            try
            {
                ////create a new TCPClient object
                ////wrapper the method into thread
                //Thread thread = new Thread(() =>
                //{
                //    ConnectDestHost(destHostIP, Port);
                //});
                //thread.IsBackground = true; //Set to a background thread and automatically releases it when the program exits
                //thread.Start();
            }
            catch (Exception ex)
            {
                TCPHelper.WriteDataToLocalFiles(ex.Message + ex.StackTrace);
            }

        }

        private static void ConnectDestHost(string destHostIP, int Port)
        {
            try
            {
                while (!IsConnet)//While...loop
                {
                    try
                    {
                        if (_tcpClient == null)
                        {
                            _tcpClient = new TcpClient(AddressFamily.InterNetwork);//overwrite the previous instance with latest resource.
                        }

                        _tcpClient.Connect(IPAddress.Parse(destHostIP), Port);//try to connect, otherwise go to catch
                        IsConnet = true;
                        OnConnectedToDestServer(_tcpClient, string.Format("Connected to target server [{0}:{1}] successfully", destHostIP, Port));
                        Thread.Sleep(300);
                        break;
                    }
                    catch
                    {
                        IsConnet = false;
                        _tcpClient.Close();
                        _tcpClient = new TcpClient(AddressFamily.InterNetwork);//overwrite the previous instance with latest resource.
                        Thread.Sleep(200);
                    }
                }

                /*Here is different is to put the receiving thread, on the connection after the break out, execute.
                Because you need to take parameters, so to use a special ParameterizedThreadStart,
                Then you start the thread.*/
                Thread thread2 = new Thread(new ParameterizedThreadStart(ClientReceiveData));//the method of receiving thread
                thread2.IsBackground = true;
                thread2.Start(_tcpClient);
            }
            catch (Exception ex)
            {
                TCPHelper.WriteDataToLocalFiles(ex.Message + ex.StackTrace);
                OnConnectedToDestServer(_tcpClient, string.Format("Failed to connect to target server [{0}:{1}] because of {2}", destHostIP, Port, ex.Message));
            }
        }

        public static void Send(string data, int pLen)
        {
            if (!IsConnet)
            {
                Connet();
            }

            if (_tcpClient == null)
                throw new ArgumentNullException(nameof(_tcpClient));

            if (data == null)
                throw new ArgumentNullException(nameof(data));
            try
            {
                SendDataToDestHost(data, pLen);
            }
            catch
            {
                if (!IsConnet)
                {
                    Connet();
                }
                SendDataToDestHost(data, pLen);
            }
        }

        private static void SendDataToDestHost(string data, int pLen)
        {
            try
            {
                byte[] sendData = Encoding.ASCII.GetBytes(data);
                _tcpClient.GetStream().BeginWrite(sendData, 0, pLen, SendDataEnd, _tcpClient);
            }
            catch (Exception ex)
            {
                TCPHelper.WriteDataToLocalFiles(ex.Message + ex.StackTrace);
            }
        }

        //send data completion callback function
        private static void SendDataEnd(IAsyncResult ar)
        {
            ((TcpClient)ar.AsyncState).GetStream().EndWrite(ar);
        }

        public static void ClientReceiveData(object tcpClient)
        {
            var ProxSocket = _tcpClient.Client;//处理上一步传过来的Socket函数
            byte[] data = new byte[1024 * 1024];
            while (IsConnet)
            {
                Thread.Sleep(100);
                int len = 0;
                try
                {
                    len = ProxSocket.Receive(data, 0, data.Length, SocketFlags.None);
                }
                catch (Exception)
                {
                    ProxSocket.Shutdown(SocketShutdown.Both);
                    ProxSocket.Close();
                    IsConnet = false;
                    Connet();
                    return;
                }

                if (len <= 0)
                {
                    try
                    {
                        if (ProxSocket != null)
                        {
                            ProxSocket.Shutdown(SocketShutdown.Both);
                            ProxSocket.Close();
                            //OnDestServerShutdown(_tcpClient, string.Format("The Connection is disconnected from the dest server [{0}:{1}].",
                            //    TCPHelper.FFTcpIpSettingMgr.OutgoingIpAddress.ToString(),
                            //    TCPHelper.FFTcpIpSettingMgr.OutgoingPort));
                        }
                    }
                    catch (Exception ex)
                    {
                        TCPHelper.WriteDataToLocalFiles(ex.Message + ex.StackTrace);
                    }

                    IsConnet = false;
                    Connet();
                    return;
                }
                TCPHelper.WriteDataToLocalFiles(string.Format("received response [{0}] from server.", Encoding.Default.GetString(data, 0, len)));
            }
        }

        //Server launched
        public delegate void ConnectedToDestServerHandler(TcpClient activeClient, string statusMsg);
        public static event ConnectedToDestServerHandler ConnectedToDestServer;
        private static void OnConnectedToDestServer(TcpClient activeClient, string statusMsg)
        {
            ConnectedToDestServer?.Invoke(activeClient, statusMsg);
            string hintMsg = "It's from FlexFlow Client.";
            Send(hintMsg, hintMsg.Length);
        }

        //Server launched
        public delegate void DestServerShutdownHandler(TcpClient activeClient, string statusMsg);
        public static event DestServerShutdownHandler DestServerShutdown;
        private static void OnDestServerShutdown(TcpClient activeClient, string statusMsg)
        {
            DestServerShutdown?.Invoke(activeClient, statusMsg);
        }
    }
}
