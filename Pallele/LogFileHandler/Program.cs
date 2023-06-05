using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParallelvsNormal
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogFileMonitor());
        }

        //static void Main(string[] args)
        //{
        //    var path = Directory.GetCurrentDirectory();
        //    var files = Directory.GetFiles(path + @"\pictures", "*.jpg");
        //    var alteredPath = path + @"\alteredPath";
        //    Directory.CreateDirectory(alteredPath);

        //    ParallelExecution(files, alteredPath);

        //    NormalExecution(files, alteredPath);
        //}

        //private static void ParallelExecution(string[] files, string alteredPath)
        //{
        //    Stopwatch stopwatch = Stopwatch.StartNew();
        //    Parallel.ForEach(files, currentFile =>
        //    {
        //        var file = Path.GetFileName(currentFile);
        //        using (var fileBitmap = new Bitmap(currentFile))
        //        {
        //            fileBitmap.RotateFlip(RotateFlipType.Rotate90FlipX);
        //            fileBitmap.Save(Path.Combine(alteredPath, file));
        //            //Console.WriteLine("Thread {0}", Thread.CurrentThread.ManagedThreadId);
        //        }
        //    });
        //    Console.WriteLine("ParallelExecution " + stopwatch.ElapsedMilliseconds);
        //    stopwatch.Stop();
        //}

        //private static void NormalExecution(string[] files, string alteredPath)
        //{
        //    Stopwatch stopwatch = Stopwatch.StartNew();
        //    foreach (var currentFile in files)
        //    {
        //        var file = Path.GetFileName(currentFile);
        //        using (var fileBitmap = new Bitmap(currentFile))
        //        {
        //            fileBitmap.RotateFlip(RotateFlipType.Rotate90FlipX);
        //            fileBitmap.Save(Path.Combine(alteredPath, file));
        //            //Console.WriteLine("Thread {0}", Thread.CurrentThread.ManagedThreadId);
        //        }
        //    }
        //    Console.WriteLine("NormalExecution " + stopwatch.ElapsedMilliseconds);
        //    stopwatch.Stop();
        //}
    }
}
