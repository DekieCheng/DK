using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TKFFTcpIpSDK.Infrastructure

{
    public class TCPClientSession
    {
        public long ClientID { get; private set; }

        public int SendCount = 0;
        public long SendSize = 0;
        public int ReceCount = 0;
        public long ReceSize = 0;
        public ListViewItem m_ListItem;

        public TCPClientSession(TcpClient pTcpClient, long pClientID)
        {
            if (pTcpClient == null)
                throw new ArgumentNullException(nameof(pTcpClient));

            TcpClient = pTcpClient;
            ClientID = pClientID;
            Buffer = new byte[NetworkDefine.SocketReceBufSize];
            MsgBuffer = NetByteBuffer.Allocate(NetworkDefine.SocketReceBufSize + 1024);
        }

        public TcpClient TcpClient { get; }

        public byte[] Buffer { get; private set; }

        public NetByteBuffer MsgBuffer { get; private set; }

        public NetworkStream NetworkStream => TcpClient.GetStream();

        public string EventMsg { get; set; }

        public string ReceivedMsg { get; set; }

        public void Close()
        {
            TcpClient.Close();
            Buffer = null;
        }

        public string ReadReceivedData()
        {
            string msgData = "";
            lock (MsgBuffer)
            {
                var MsgLen = MsgBuffer.ReadableBytes();
                if (MsgLen <= 0)
                    return "";
                byte[] _MsgData = new byte[MsgLen];
                MsgBuffer.ReadBytes(_MsgData, 0, MsgLen);
                MsgBuffer.Clear();
                msgData = Encoding.UTF8.GetString(_MsgData);
                ReceivedMsg = msgData;
            }
            return msgData;
        }

        public void SaveMsgToBuffer(int pMsgLen)
        {
            ReceCount++;
            ReceSize += pMsgLen;

            lock (MsgBuffer)
            {
                MsgBuffer.WriteBytes(Buffer, pMsgLen);
            }
        }

        public void UpdateSendInfo(int pMsgLen)
        {
            SendCount++;
            SendSize += pMsgLen;
        }
    }
}
