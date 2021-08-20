using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;

namespace EPPlusSimulatorv5
{
    class Program
    {

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmEpplusSimulator());
        }

        static void XMain(string[] args)
        {
            var fileInfo = new System.IO.FileInfo("c:\\Temp\\EPPlus_PerfTest.xlsx");
            if (File.Exists(fileInfo.FullName)) File.Delete(fileInfo.FullName);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            Console.WriteLine("Creating package");

            var sw = new Stopwatch();
            sw.Start();
            // since we are in a sync context, wait for the async task to finish...
            WriteToDisk(sw, fileInfo).Wait();
            sw.Stop();
            Console.WriteLine(" Total seconds writing data: " + sw.Elapsed.TotalSeconds);
            Console.WriteLine("Loading package...");
            sw.Reset();
            sw.Start();
            LoadFromDisk(fileInfo).Wait();
            sw.Stop();
            Console.WriteLine(" Total seconds loading data: " + sw.Elapsed.TotalSeconds);
        }

        private static async Task LoadFromDisk(FileInfo fileInfo)
        {
            var package = new ExcelPackage();
            await package.LoadAsync(fileInfo);
            Console.WriteLine("Cell GR100000: " + package.Workbook.Worksheets[0].Cells["GR100000"].Value);
        }

        private static async Task WriteToDisk(Stopwatch sw, FileInfo fileInfo)
        {
            var package = new ExcelPackage(fileInfo);
            var sheet = package.Workbook.Worksheets.Add("Test");

            var range = sheet.Cells[1, 1, 100000, 200];
            var toCol = range.End.Column;
            var toRow = range.End.Row;
            for (var row = 1; row <= toRow; row++)
            {
                for (var col = 1; col <= toCol; col++)
                {
                    if (col % 2 == 0)
                    {
                        range[row, col].Value = row + col;
                    }
                    else
                    {
                        range[row, col].Value = "abc123abc123abc123abc123abc123abc123";
                    }
                }
            }
            Console.WriteLine(" Data written to cell store: " + sw.Elapsed.TotalSeconds);

            await package.SaveAsync();
        }
    }
}
