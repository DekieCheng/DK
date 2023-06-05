using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using TKFFTcpIpSDK.TaskSettings;

namespace TKFFTcpIpSDK
{
    public class TCPHelper
    {
        private static FFTcpIpHandlerSettingMgr _FFTcpIpHandlerSettingMgr;
        public static FFTcpIpHandlerSettingMgr FFTcpIpSettingMgr
        {
            get { return _FFTcpIpHandlerSettingMgr; }
            set { _FFTcpIpHandlerSettingMgr = value; }
        }

        private static bool _writedatatolocalfile;
        public static bool WriteDataToLocalFile
        {
            get { return _writedatatolocalfile; }
            set { _writedatatolocalfile = value; }
        }

        public static void WriteDataToLocalFiles(string strData)
        {
            //  if (!FFTcpIpSettingMgr.WriteDataLocalFile) { return; }

            try
            {
                string currPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                string folder = currPath + @"TKFFTcpIpSDKLog\";
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                string fileName = folder + string.Format("{0:yyyyMMdd}", DateTime.Now) + ".log";

                using (var fs = new FileStream(fileName, FileMode.OpenOrCreate, FileSystemRights.AppendData, FileShare.ReadWrite, 4096, FileOptions.Asynchronous))
                {
                    using (var wr = new StreamWriter(fs))
                    {
                        wr.AutoFlush = true;
                        wr.WriteLine(string.Format("{0:yyyy-MM-dd HH:mm:ss.ffff} >>> {1}", DateTime.Now, $"{strData}\r"));
                    }
                }
            }
            catch (Exception)
            { }
        }

        public static string TcpParserReceivedData(byte[] sourceDatabyte)
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
    }
}
