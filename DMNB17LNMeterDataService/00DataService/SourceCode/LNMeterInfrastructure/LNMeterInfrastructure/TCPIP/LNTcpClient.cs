using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Timers;
using LNMeterInfrastructure.Common;
using System.Threading;

namespace TCPIP
{
    //通讯接口事件代理
    public delegate void CommRxEventHandler();
    public delegate void CommIdleEventHandler();

    //通讯接口
    public interface ICommunication
    {
        event CommRxEventHandler CommRxEvent;       //收到数据事件
        event CommIdleEventHandler CommIdleEvent;   //通讯空闲事件

        int CommSend(byte[] buffer, int size);      //发送数据到通讯口
        int CommReceive(CommBuffer buffer);         //从通讯口接收数据
        int CommReceive(byte[] buffer, int size);   //从通讯口接收数据
    }

    //TCP客户端实现
    public class LNTcpClient : ICommunication, IDisposable
    {
        private const int MAX_RECONNECT_TIMES = 3;  //断线重连尝试次数
        private const int COMM_IDLE_TIMES = 3;      //通讯空闲触发间隔

        //soket对象及参数
        private Socket _socket;
        private string _host;
        private int _port;
        private bool _reconnect;
        private string _outputcmd;
        private int ConnecteFailedCount { get; set; }
        public int ReconnectStatistics { get; private set; }

        private static uint _keepAliveTime = 5000;      //无数据交互持续时间(ms)
        private static uint _keepAliveInterval = 500;   //发送探测包间隔(ms)

        //定时器,用于触发通讯空闲
        private System.Timers.Timer _timer;
        private int _commIdleCount;

        //实现接口的两个事件
        public event CommRxEventHandler CommRxEvent;
        public event CommIdleEventHandler CommIdleEvent;

        //重连失败事件
        public event EventHandler ReconnectionFailedEvent;

        //数据接收缓存
        private CommBuffer recvBuffer;

        private byte[] m_ReceivedData = new byte[2048];
        private bool disposedValue;

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

        //构造函数
        public LNTcpClient(string host, int port, string outputcmd)
        {
            _host = host;
            _port = port;
            _outputcmd = outputcmd;
            _reconnect = false;
            ConnecteFailedCount = 0;
            recvBuffer = new CommBuffer(20480);
        }

        //连接
        public bool Connect()
        {
            bool flag;
            try
            {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
                    ProtocolType.Tcp);
                _socket.Connect(_host, _port);
                ConnecteFailedCount = MAX_RECONNECT_TIMES;

                //设置KeepAlive
                _socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                byte[] optionValue = new byte[12];
                BitConverter.GetBytes(1).CopyTo(optionValue, 0);
                BitConverter.GetBytes(_keepAliveTime).CopyTo(optionValue, 4);
                BitConverter.GetBytes(_keepAliveInterval).CopyTo(optionValue, 8);
                _socket.IOControl(IOControlCode.KeepAliveValues, optionValue, null);
                flag = true;
            }
            catch (SocketException ex)
            {
                LNLogMgr.WriteLogAsy(string.Format("Connect>>>SocketException: [{0}:{1}] ", ex.SocketErrorCode.ToString(), ex.Message));
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
        }

        //重连
        public bool Reconnect()
        {
            ReconnectStatistics++;
            _reconnect = false;
            Close();
            try
            {
                Connect();
            }
            catch (SocketException e)
            {
                ConnecteFailedCount--;
                if (ConnecteFailedCount > 0)
                {
                    //Console.WriteLine("重试次数剩余{0}",ConnecteFailedCount);
                    _reconnect = true;
                    return true;
                }
                else
                {
                    //重连失败事件
                    if (ReconnectionFailedEvent != null)
                        ReconnectionFailedEvent(this, new EventArgs());
                    return false;
                }
            }
            return true;
        }

        //释放资源
        public void Close()
        {
            _socket.Close();
        }

        //启动空闲事件触发
        public void StartCommIdle()
        {
            //定时器配置
            _timer = new System.Timers.Timer(500);
            _timer.AutoReset = true;
            _timer.Elapsed += TimerElapsed;
            _timer.Start();
            _commIdleCount = 0;
        }

        //停止空闲事件触发
        public void StopCommIdle()
        {
            _timer.Elapsed -= TimerElapsed;
            _timer.Close();
        }

        //发送接收数据事件
        public void SendRecvEvent()
        {
            if (CommRxEvent != null)
                CommRxEvent();
        }

        //发送超时事件
        public void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (_commIdleCount++ >= COMM_IDLE_TIMES)
            {
                if (CommIdleEvent != null)
                    CommIdleEvent();
                _commIdleCount = 0;
            }
        }

        //发送数据接收实现,断线重连
        public int CommSend(byte[] buffer, int size)
        {
            int sendSize = 0;
            try
            {
                sendSize = _socket.Send(buffer, size, SocketFlags.None);
                _commIdleCount = 0;
            }
            catch (SocketException e)
            {
                ReconnectStatistics++;
                _reconnect = true;
            }
            return sendSize;
        }

        //接收数据接口实现
        public int CommReceive(byte[] buffer, int size)
        {
            return recvBuffer.Read(buffer, size);
        }

        //接收数据接口实现
        public int CommReceive(CommBuffer buffer)
        {
            return recvBuffer.CopyTo(buffer);
        }

        //接收数据线程,使用阻塞方式接收数据
        public void ReadFormSocket()
        {
            int recvNum;
            byte[] recv = new byte[2048];
            _socket.ReceiveTimeout = 1000;
            while (true)
            {
                try
                {
                    recvNum = _socket.Receive(recv, SocketFlags.None);
                }
                catch (SocketException ex)
                {
                    recvNum = 0;
                    LNLogMgr.WriteLogAsy(string.Format("SocketException: [{0}:{1}] ", ex.SocketErrorCode.ToString(), ex.Message));
                    break;
                }

                //网络断开Receive返回0
                if (recvNum == 0 || _reconnect == true)
                {
                    //重连次数用尽,退出
                    if (Reconnect() == false)
                        break;
                }
                else
                {
                    recvBuffer.Write(recv, recvNum);
                    // SendRecvEvent();
                    break;
                }
                _commIdleCount = 0;
            }
        }
        private void LNReconnect()
        {
            int reconnectcount = 1;
        reconnect:
            if (Reconnect() == false && reconnectcount <= 3)
            {
                LNLogMgr.WriteLogAsy(string.Format("Re-connect to the remote host [{0}:{1}] for the {2} times", _host, _port, reconnectcount.ToString()));
                reconnectcount++;
                goto reconnect;
            }
        }

        public bool FireMeterClient()
        {
            bool flag = false;

            //connect
            try
            {
                if (!Connect())
                {
                    LNReconnect();
                    LNLogMgr.WriteLogAsy(string.Format("Failed to connect to the remote host [{0}:{1}]", _host, _port));

                    return flag;
                }
            }
            catch (SocketException ex)
            {
                LNLogMgr.WriteLogAsy(string.Format("FireMeterClient>>>SocketException: [{0}:{1}] ", ex.SocketErrorCode.ToString(), ex.Message));
                LNReconnect();
                return false;
            }
            catch (Exception ex)
            {
                LNLogMgr.WriteLogAsy(string.Format("FireMeterClient>>>Exception: [{0}:{1}] ", ex.Message, ex.StackTrace));
                LNReconnect();
                return false;
            }

            //Send data
            try
            {
                byte[] byteCmand = LNGlobal.HexStringToBytes(_outputcmd.Replace(" ", ""));
                if (CommSend(byteCmand, byteCmand.Length) <= 0)
                {
                    LNLogMgr.WriteLogAsy(string.Format("Failed to send command [{0}] to the remote host [{1}:{2}]", _commIdleCount, _host, _port));
                    return flag;
                }
            }
            catch (SocketException ex)
            {
                LNLogMgr.WriteLogAsy(string.Format("FireMeterClient>>>CommSend>>>SocketException: [{0}:{1}] ", ex.SocketErrorCode.ToString(), ex.Message));
                return false;
            }
            catch (Exception ex)
            {
                LNLogMgr.WriteLogAsy(ex);
                return false;
            }

            //read data
            try
            {
                ReadFormSocket();
            }
            catch (SocketException ex)
            {
                LNLogMgr.WriteLogAsy(string.Format("FireMeterClient>>>ReadFormSocket>>>SocketException: [{0}:{1}] ", ex.SocketErrorCode.ToString(), ex.Message));
                return false;
            }
            catch (Exception ex)
            {
                LNLogMgr.WriteLogAsy(ex);
                return false;
            }

            //Receive data
            try
            {
                int iCount = 0;
                Array.Resize(ref m_ReceivedData, 0);
                while (true)
                {
                    Thread.Sleep(200);
                    CommBuffer commBuffer = new CommBuffer(1024);// recData = new byte[1024];
                    int bytesRead = CommReceive(commBuffer);
                    if (bytesRead > 0)
                    {
                        Array.Resize(ref m_ReceivedData, bytesRead);
                        Array.Copy(commBuffer.pBuf, m_ReceivedData, bytesRead);

                        string recData = LNGlobal.ToConvertHexString(m_ReceivedData);
                        LNLogMgr.WriteLogAsy(string.Format("Arrived data length:[{0}], Data(Hex):[{1}] from host [{2}:{3}] by command [{4}]",
                                bytesRead.ToString(), recData, _host, _port.ToString(), _outputcmd));
                        break;
                    }
                    if (iCount >= 2) { break; }
                    iCount++;
                }
                flag = true;
            }
            catch (SocketException ex)
            {
                LNLogMgr.WriteLogAsy(string.Format("FireMeterClient>>>CommReceive>>>SocketException: [{0}:{1}] ", ex.SocketErrorCode.ToString(), ex.Message));
                return false;
            }
            catch (Exception ex)
            {
                LNLogMgr.WriteLogAsy(ex);
                return false;
            }
            return flag;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~LNTcpClient()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

    //通讯缓冲结构类
    public class CommBuffer
    {
        public uint capacity;              //缓冲区大小
        public int readPtr;                //读指针
        public int writePtr;               //写指针
        public byte[] pBuf;                //缓冲区

        //构造函数
        public CommBuffer(uint capacity)
        {
            this.capacity = capacity;
            this.readPtr = 0;
            this.writePtr = 0;
            this.pBuf = new byte[capacity];
        }

        //从缓冲区中读取数据到byte数组
        public int Read(byte[] buff, int size)
        {
            int readSize;
            for (readSize = 0; readSize < size; readSize++)
            {
                if (IsEmpty())
                    break;
                buff[readSize] = pBuf[readPtr++];
                if (readPtr >= capacity)
                    readPtr = 0;
            }
            return readSize;
        }

        //将byte数组写入缓冲区
        public int Write(byte[] buff, int size)
        {
            int writeSize, wp;
            for (writeSize = 0; writeSize < size; writeSize++)
            {
                wp = writePtr + 1;
                if (wp >= capacity)
                    wp = 0;
                if (wp == readPtr)
                    break;
                pBuf[writePtr] = buff[writeSize];
                writePtr = wp;

            }
            return writeSize;
        }

        //拷贝buffer缓冲区数据到本缓冲区内,会引起拷贝源有效数据为空
        public int Copy(CommBuffer buffer, int len)
        {
            if (len == 0)
                return 0;
            byte[] data = new byte[len];
            len = buffer.Read(data, len);
            return this.Write(data, len);
        }

        //拷贝本缓冲区中的数据到buffer中,会引起拷贝源有效数据为空
        public int CopyTo(CommBuffer buffer)
        {
            int dataLen = this.DataLength();
            if (dataLen == 0)
                return 0;
            byte[] data = new byte[dataLen];
            dataLen = this.Read(data, dataLen);
            return buffer.Write(data, dataLen);
        }

        //索引器实现
        public byte this[int index]
        {
            get
            {
                if (index >= DataLength())
                    return 0;
                int rp = readPtr + index;
                if (rp >= capacity)
                    rp = rp - (int)capacity;
                return pBuf[rp];
            }
        }

        //忽略len长度的数据
        public void Skip(int len)
        {
            while (len-- > 0)
            {
                if (readPtr == writePtr)
                    break;
                readPtr++;
                if (readPtr >= capacity)
                    readPtr = 0;
            }
        }

        //判断是否有数据
        public bool IsEmpty()
        {
            return writePtr == readPtr;
        }

        //获取有效数据长度
        public int DataLength()
        {
            int len = writePtr - readPtr;
            if (len < 0)
                len = len + (int)capacity;
            return len;
        }

        //清空缓冲区
        public void Clear()
        {
            writePtr = readPtr = 0;
        }

        //缓冲区整理,将数据移动到0位置,方便后续数据的处理
        public void Neaten()
        {
            uint i, j;

            if (readPtr == 0)
            {
                return; //读指针已经为0
            }


            if (readPtr >= writePtr)
            {
                readPtr = writePtr = 0;
                return;
            }


            if (writePtr >= capacity)
            {
                readPtr = 0;
                writePtr = 0;
                return;
            }

            i = 0;
            j = (uint)readPtr;
            while (j < writePtr)
            {
                pBuf[i++] = pBuf[j++];
            }

            readPtr = 0;
            writePtr = (int)i;
        }
        //显示缓存数据
        public override string ToString()
        {
            int dataLen = this.DataLength();
            string str = string.Format("数据长度{0}: ", this.DataLength());
            for (int i = 0; i < dataLen; i++)
            {
                str += string.Format("{0:X} ", this[i]);
            }
            return str;
        }
    }
}