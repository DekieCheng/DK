
namespace TKFFTcpIpSDK.Infrastructure
{
    class NetworkDefine
    {
        public static int MaxConnect = 1024;

        public static int SocketSendBufSize = 1024 * 8; // 8k

        public static int SocketReceBufSize = 1024 * 8; // 8k

        public static bool UseNoDelay = true;
    }
}
