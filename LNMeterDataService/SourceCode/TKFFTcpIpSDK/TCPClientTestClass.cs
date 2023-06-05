using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TKFFTcpIpSDK
{

    public class TCPClientTestClass
    {
        Socket socket;
        NetworkStream tcpStream;
        private IAsyncResult asyncResultRead;//接收数据的异步对象
        private IAsyncResult asyncResultWrite;//发送数据的异步对象

        private byte[] buffers = new byte[0x1000];
        public void Connect(byte[] bytes, int offset, int length)
        {
            Task.Factory.StartNew(() =>
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IAsyncResult conAsync = socket.BeginConnect("10.200.7.213", 5300, null, socket);//开始连接

                bool success = conAsync.AsyncWaitHandle.WaitOne(6000, true);//堵塞 直到收到反馈

                if (success && socket.Connected)//成功连接
                {
                    socket.Send(bytes);
                    //socket.EndConnect(conAsync);//关闭异步对象

                    tcpStream = new NetworkStream(socket);//创建收发数据流

                    socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);//无延迟发送

                    Receive();//开始接收

                }
                else
                {
                    //超时或者连接失败
                }

            });
        }

        public void Receive()
        {
            try
            {
                //异步从流中读取数据
                asyncResultRead = tcpStream.BeginRead(buffers, 0, buffers.Length, EndReceive, socket);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        private void EndReceive(IAsyncResult asyncReceive)
        {
            try
            {
                int len = tcpStream.EndRead(asyncReceive);
                if (len > 0)
                {
                    //读取数据内容
                    ReadData(buffers, 0, len);
                    //再次开启接收
                    Receive();
                }
                else
                {

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void ReadData(byte[] bytes, int offset, int length)
        {
            //在此处理接收到的数据
            String recData = Encoding.UTF8.GetString(bytes);
            string s = recData;
        }

        //发送数据
        public void Send(byte[] bytes, int offset, int length)
        {
            try
            {
                asyncResultWrite = tcpStream.BeginWrite(bytes, offset, length, EndSend, socket);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void EndSend(IAsyncResult ar)
        {
            try
            {
                tcpStream.EndWrite(ar);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        //断开连接
        public void Disconnect()
        {
            try
            {
                //关闭两个异步对象
                if (!asyncResultRead.IsCompleted)
                {
                    asyncResultRead.AsyncWaitHandle.Close();
                }

                if (!asyncResultWrite.IsCompleted)
                {
                    asyncResultWrite.AsyncWaitHandle.Close();
                }

                tcpStream.Close();
                tcpStream.Dispose();//释放流

                socket.Shutdown(SocketShutdown.Both);//关闭发送和接收
                socket.Close();
                socket.Dispose();


            }
            catch (Exception e)
            {

            }
        }


    }
}
