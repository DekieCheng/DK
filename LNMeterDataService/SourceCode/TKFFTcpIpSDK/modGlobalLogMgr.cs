using System;
using System.IO;
using Microsoft.VisualBasic;
using System.Security.AccessControl;

static class modGlobalLogMgr
{
    static string g_AppServerName = Environment.MachineName;

    public static void WriteLog(string strData)
    {
        string DestinateFolder = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        string strFileName;
        System.IO.StreamWriter fsw;

        strFileName = DestinateFolder + string.Format("{0:yyyyMMdd}", DateTime.Now) + ".log";
        fsw = new System.IO.StreamWriter(File.Open(strFileName, FileMode.Append));
        fsw.WriteLine(DateTime.Now + ": " + strData);
        fsw.Flush();
        fsw.Close();
    }

    public static void WriteLogAsy(string strData)
    {
        try
        {
            if (strData == string.Empty)
                strData = "Got empty data.";
            string DestinateFolder = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string currFolder = (DestinateFolder + @"LogFiles\" + string.Format("{0:yyyyMMdd}", DateTime.Now));
            if (!Directory.Exists(currFolder))
                Directory.CreateDirectory(currFolder);
            string strFileName = currFolder + @"\" + string.Format("{0:yyyyMMddHH}", DateTime.Now) + ".log";

            using (FileStream fs = new FileStream(strFileName, FileMode.OpenOrCreate, FileSystemRights.AppendData, FileShare.ReadWrite, 4096, FileOptions.Asynchronous))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.AutoFlush = true;
                    writer.WriteLine(strData + Environment.NewLine);
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
            string currFolder = (DestinateFolder + @"LogFiles\" + string.Format("{0:yyyyMMdd}", DateTime.Now));
            if (!Directory.Exists(currFolder))
                Directory.CreateDirectory(currFolder);
            string strFileName = currFolder + @"\" + string.Format("{0:yyyyMMdd}", DateTime.Now) + ".log";

            string strData = DateTime.Now.ToString() + " >>> "
               + "App Server: " + g_AppServerName + Environment.NewLine
               + "Exception Source: " + oEX.TargetSite.ToString() + Environment.NewLine
               + "Message: " + oEX.Message + Environment.NewLine
               + "StackTrace: " + oEX.StackTrace;
            if (oEX.InnerException != null)
                strData = strData + Environment.NewLine + "Inner Exception:" + oEX.InnerException.Message;

            using (FileStream fs = new FileStream(strFileName, FileMode.OpenOrCreate, FileSystemRights.AppendData, FileShare.ReadWrite, 4096, FileOptions.Asynchronous))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.AutoFlush = true;
                    writer.WriteLine(strData + Environment.NewLine);
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
}
