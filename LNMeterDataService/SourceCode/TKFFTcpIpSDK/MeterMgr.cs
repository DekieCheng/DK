using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace TKFFTcpIpSDK.MeterMgr
{

    public static class MeterCommunicationMgr
    {
        private static byte[] tcpResult = new byte[1024];
        private static Socket socketSend = null;
        static MeterCommunicationMgr()
        {
            Application.ApplicationExit += new EventHandler(OnApplicationExit);
        }

        private static void OnApplicationExit(object sender, EventArgs e)
        {
            // When the application is exiting, write the application data to the user file and close it.
            try
            {

            }
            catch (Exception)
            {
                // WriteDataToLocalFiles(ex.Message + ex.StackTrace);
            }
        }

        public static int InitAndSendData(string hostIP, int hostPort, string SendData)
        {
            var ip = hostIP;
            var port = hostPort;

            Log(LogType.Open, "TCP Servise IP." + ip);
            Log(LogType.Open, "TCP Servise Port." + port);

            try
            {
                socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socketSend.Connect(new IPEndPoint(IPAddress.Parse(ip), port));

                while (true)
                {
                    var r = send((SendData).ToBytes(), 500);
                    if (r != 0) { Log(LogType.Recv, "rs err r." + r); Thread.Sleep(1000); continue; }
                    Log(LogType.Recv, BitConverter.ToString(tcpResult, 0, tcpResult.Length));
                    Thread.Sleep(1000);
                }

            }
            catch (Exception e)
            {
                Log(LogType.Error, e.Message);
            }

            return 0;

        }

        static long send_expire = 0;
        static int send(byte[] buf, int ti = 3000)
        {
            tcpResult = new byte[1024];
            Log(LogType.Send, BitConverter.ToString(buf));

            try
            {
                socketSend.Send(buf);

                send_expire = DateTime.Now.Ticks + ti * 10000;
                int len = 7;//最短数据长度位置

                for (; DateTime.Now.Ticks < send_expire; Thread.Sleep(20))
                {

                    if (socketSend.Receive(tcpResult, tcpResult.Length, 0) == 0) continue;

                    tcpResult = tcpResult.Remove();

                    if (tcpResult.Length >= len) break;
                }

                if (tcpResult == null || tcpResult.Length <= 0) { return 4; }

                return 0;

            }
            catch (Exception ex)
            {
                Log(LogType.Error, ex.Message);
                return 4;
            }


        }

        private static void Log(LogType t, string msg) { Console.Write($"{DateTime.Now:yyyy-MM-dd HH:mm:ss:fff} [{t}] { msg}{Environment.NewLine}"); }
        private enum LogType
        {
            Open,
            Send,
            Recv,
            Error,
            Info,
        }


    }

    public static class Expand
    {

        public static byte[] Remove(this byte[] b, byte cut = 0x00)
        {
            var list = new List<byte>();
            list.AddRange(b);
            for (var i = list.Count - 1; i >= 0; i--)
            {
                if (list[i] == cut) { list.RemoveAt(i); } else { break; }
            }
            var lastbyte = new byte[list.Count];
            for (var i = 0; i < list.Count; i++)
            {
                lastbyte[i] = list[i];
            }
            return lastbyte;
        }

        public static string Replace(this string s, string p, string r)
        {
            return Regex.Replace(s, p, r);
        }

        public static byte[] ToBytes(this string s)
        {
            s = Replace(s, @"[^\dA-Fa-f]+", "");
            if (s.Length % 2 > 0) s = "0" + s;
            var max = s.Length / 2;
            var buf = new byte[max];
            for (var i = 0; i < max; i++) buf[i] = Convert.ToByte(s.Substring(i * 2, 2), 16);
            return buf;
        }

    }

}
