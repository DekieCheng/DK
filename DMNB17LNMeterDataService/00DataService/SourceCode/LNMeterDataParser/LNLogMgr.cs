using System;
using System.IO;
using Microsoft.VisualBasic;
using System.Security.AccessControl;

namespace LNMeterDataParser
{
    public static class LNLogMgr
    {
        public static void WriteLogAsy(string strData)
        {
            try
            {
                string DestinateFolder = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                string currFolder = (DestinateFolder + @"LogFiles\DataParser\" + string.Format("{0:yyyyMMdd}", DateTime.Now));
                if (!Directory.Exists(currFolder))
                    Directory.CreateDirectory(currFolder);
                string strFileName = currFolder + @"\" + string.Format("{0:yyyyMMddHH}", DateTime.Now) + ".log";

                using (FileStream fs = new FileStream(strFileName, FileMode.OpenOrCreate, FileSystemRights.AppendData, FileShare.ReadWrite, 4096, FileOptions.Asynchronous))
                {
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        writer.AutoFlush = true;
                        writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ": " + strData);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static void WriteLogAsy(Exception oEX)
        {
            try
            {
                string DestinateFolder = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                string currFolder = (DestinateFolder + @"LogFiles\DataParser\" + string.Format("{0:yyyyMMdd}", DateTime.Now));
                if (!Directory.Exists(currFolder))
                    Directory.CreateDirectory(currFolder);
                string strFileName = currFolder + @"\" + string.Format("{0:yyyyMMddHH}", DateTime.Now) + ".log";
                //string _Msg = "Error Message: " + oEX.Message + Environment.NewLine
                //            + "Exception Source: " + oEX.TargetSite.ToString() + Environment.NewLine;

                string _Msg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " >>> "
                   + "App Server: " + Environment.MachineName + Environment.NewLine
                   + "Exception Source: " + oEX.TargetSite.ToString() + Environment.NewLine
                   + "Message: " + oEX.Message + Environment.NewLine
                   + "Source: " + oEX.Source + Environment.NewLine
                   + "StackTrace: " + oEX.StackTrace;

                if (oEX.InnerException != null)
                {
                    _Msg = _Msg + Environment.NewLine + "Inner Exception:" + oEX.InnerException.Message;
                }

                using (FileStream fs = new FileStream(strFileName, FileMode.OpenOrCreate, FileSystemRights.AppendData, FileShare.ReadWrite, 4096, FileOptions.Asynchronous))
                {
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        writer.AutoFlush = true;
                        writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ": " + _Msg);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}