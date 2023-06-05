using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public static class LogFileManagers
{
    public delegate void OnFileAmountHandler(int fileAmount);
    public static event OnFileAmountHandler OnFileAmount;

    public delegate void OnFileAmountChangedHandler(int fileAmount);
    public static event OnFileAmountChangedHandler OnFileAmountChanged;

    public delegate void StatusReportHandler(string msg);
    public static event StatusReportHandler OnStatusReport;

    public delegate void FileCompletedHandler(string FullfileName);
    public static event FileCompletedHandler OnFileCompleted;

    public delegate void ErrorHandler(Exception ex);
    public static event ErrorHandler OnInternalError;

    private static string GoodFilePath = Directory.GetCurrentDirectory() + @"\GoodFiles\";
    private static string ErrorFilePath = Directory.GetCurrentDirectory() + @"\ExceptionFiles\";
    private static string CSVPath = Directory.GetCurrentDirectory() + @"\CSVFiles\";


    public static void ParserLogFiles()
    {
        try
        {
            if (!Directory.Exists(GoodFilePath))
            {
                Directory.CreateDirectory(GoodFilePath);
            }
            if (!Directory.Exists(ErrorFilePath))
            {
                Directory.CreateDirectory(ErrorFilePath);
            }
            if (!Directory.Exists(CSVPath))
            {
                Directory.CreateDirectory(CSVPath);
            }

            var logFilePath = Directory.GetCurrentDirectory() + @"\LogFiles";
            if (!Directory.Exists(logFilePath))
            {
                Directory.CreateDirectory(logFilePath);
            }

            OnStatusReport.Invoke("Prepare Dummy log files");
            GenerateDummyLogFiles.GenerateLogFiles(logFilePath, 200);
            var files = Directory.GetFiles(logFilePath, "*.log");
            OnFileAmount.Invoke(files.Length);
            OnStatusReport.Invoke("Total [" + files.Length.ToString() + "] files are ready");
            Thread.Sleep(2000);
            ParallelExecution(files);
        }
        catch (Exception ex)
        {
            OnInternalError.Invoke(ex);
        }
    }

    private static void ParallelExecution(string[] files)
    {
        try
        {
            Task.Run(() =>
                    {
                        Stopwatch stopwatch = Stopwatch.StartNew();
                        Parallel.ForEach(files, new ParallelOptions
                        {
                            MaxDegreeOfParallelism = Environment.ProcessorCount
                        }, currentFile =>
                        {
                            var ExtractedResult = FileContentParser.ExtractFileContent(ReadFileContent(currentFile));
                            StoreResultToCSVFile(ExtractedResult);
                            MoveFileToErrorFolder(currentFile);
                            OnFileCompleted.Invoke(string.Format("The file [{0}] is done", currentFile));
                            OnFileAmountChanged.Invoke(1);
                        });
                        OnFileCompleted.Invoke("Parallel Execution Time: " + stopwatch.ElapsedMilliseconds);
                        stopwatch.Stop();
                    });
        }
        catch (Exception e)
        {
            WriteDataToLocalFiles(e.Message);
            OnInternalError.Invoke(e);
        }

    }

    private static string ReadFileContent(string sourceFile)
    {
        string fileContent = string.Empty;
        try
        {
            using (StreamReader sr = new StreamReader(sourceFile, Encoding.Unicode))
            {
                fileContent = sr.ReadToEnd();
            }
        }
        catch (Exception e)
        {
            WriteDataToLocalFiles(e.Message);
            OnInternalError.Invoke(e);
        }
        return fileContent;
    }

    private static void MoveFileToErrorFolder(string sourceFile)
    {
        try
        {
            string fileName = "_Err_" + Path.GetFileName(sourceFile);
            File.Move(sourceFile, ErrorFilePath + fileName);
        }
        catch (Exception e)
        {
            WriteDataToLocalFiles(e.Message);
            OnInternalError.Invoke(e);
        }
    }
    private static void MoveFileToGoodFolder(string sourceFile)
    {
        try
        {
            File.Move(sourceFile, GoodFilePath);
        }
        catch (Exception e)
        {
            WriteDataToLocalFiles(e.Message);
            OnInternalError.Invoke(e);
        }
    }

    public static void WriteDataToLocalFiles(string strData)
    {
        try
        {
            var path = Directory.GetCurrentDirectory();
            string folder = path + @"\ApplicationLogs\";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            string fileName = folder + string.Format("{0:yyyyMMddhh}", DateTime.Now) + ".log";

            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate, FileSystemRights.AppendData, FileShare.ReadWrite, 4096, FileOptions.Asynchronous))
            {
                using (var wr = new StreamWriter(fs))
                {
                    wr.AutoFlush = true;
                    wr.WriteLine(string.Format("{0:yyyy-MM-dd HH:mm:ss.ffff}", DateTime.Now));
                    wr.WriteLine($"{strData}\r");
                }
            }
        }
        catch (Exception ex)
        {
            OnInternalError.Invoke(ex);
        }
    }

    public static void StoreResultToCSVFile(string strData)
    {
        try
        {
            string fileName = CSVPath + string.Format("{0:yyyyMMddhh}", DateTime.Now) + ".csv";

            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate, FileSystemRights.AppendData, FileShare.ReadWrite, 4096, FileOptions.Asynchronous))
            {
                using (var wr = new StreamWriter(fs))
                {
                    wr.AutoFlush = true;
                    wr.WriteLine($"{strData}\r");
                }
            }
        }
        catch (Exception ex)
        {
            WriteDataToLocalFiles(ex.Message);
            OnInternalError.Invoke(ex);
        }
    }

    public static class GenerateDummyLogFiles
    {
        public static void GenerateLogFiles(string destPath, int Qty)
        {
            try
            {
                //Task.Run(() =>
                //{
                Parallel.For(0, Qty, x =>
            {
                string destFile = destPath + "\\" + string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now) + "_" + x.ToString() + ".log";
                using (FileStream fs = File.Create(destFile))
                {
                    AddText(fs, "This is some text");
                    AddText(fs, "This is some more text,");
                    AddText(fs, "\r\nand this is on a new line");
                    AddText(fs, "\r\n\r\nThe following is a subset of characters:\r\n");

                    for (int i = 1; i < 120; i++)
                    {
                        AddText(fs, Convert.ToChar(i).ToString());
                    }
                    // OnStatusReport.Invoke("File [" + destFile + "] is created.");
                    //OnFileAmountChanged.Invoke(1);
                }
            });
                //});
            }
            catch (Exception ex)
            {
                WriteDataToLocalFiles(ex.Message);
                OnInternalError.Invoke(ex);
            }
        }
    }

    private static void AddText(FileStream fs, string value)
    {
        byte[] info = new UTF8Encoding(true).GetBytes(value);
        fs.Write(info, 0, info.Length);
    }
}

public static class FileContentParser
{
    public static string ExtractFileContent(string fileContent)
    {
        return "a,b,c,d";
    }
}
