using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace FlexFlowSmartDesktopCCharp
{
    public partial class ThirdExeInterface : Form
    {
        [DllImport("User32.dll", EntryPoint = "SetParent")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("User32.dll", EntryPoint = "ShowWindow")]
        private static extern int ShowWindow(IntPtr hwnd, int nCmdShow);
        [DllImport("User32.dll", SetLastError = true)]
        private static extern int MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);

        IntPtr intPtr = new IntPtr();

        public ThirdExeInterface()
        {
            InitializeComponent();
        }

        private void Running()
        {
           
        }


        public bool CheckProcessExists(string szProcessName)
        {
            try
            {
                Process[] processes = Process.GetProcessesByName(szProcessName);
                foreach (Process p in processes)
                {
                    if (System.IO.Path.Combine(Application.StartupPath, szProcessName) == p.MainModule.FileName)
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return false;
        }

        //Kill prcoess
        private void KillProcess(string szProcessName)
        {
            Process[] processes = Process.GetProcessesByName(szProcessName);
            foreach (Process p in processes)
            {
                if (System.IO.Path.Combine(Application.StartupPath, szProcessName) == p.MainModule.FileName)
                {
                    p.Kill();
                    p.Close();
                }
            }
        }

        //Running execution program
        public void RunThirdExe(string szProcessName)
        {
            try
            {
                if (CheckProcessExists(szProcessName))
                {
                    KillProcess(szProcessName);
                }

                string fexePath = szProcessName;
                Process p = new Process();
                p.StartInfo.FileName = fexePath;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                p.Start();
                
                while (p.MainWindowHandle.ToInt32() == 0)
                {
                    System.Threading.Thread.Sleep(100);
                }

                SetParent(p.MainWindowHandle, this.Handle);
                intPtr = p.MainWindowHandle;
                MoveWindow(p.MainWindowHandle, 0, 30, this.Width - 18, this.Height - 50, true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Source +" " + e.Message);
            }
        }

        private void ThirdExeInterface_Load(object sender, EventArgs e)
        {
            this.txtExtPath.Text = Application.StartupPath;
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            this.RunThirdExe(this.txtExtPath.Text);
        }
    }
}
