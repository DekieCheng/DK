using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using AutoItLee;
using Microsoft.VisualBasic;

namespace AutoItLee
{
    public partial class frmLNSmartController : Form
    {
        private readonly AutoItLeeHelper x;

        public frmLNSmartController()
        {
            InitializeComponent();
            x = new AutoItLeeHelper();
        }


        private void BtnMouseClick_Click(object sender, EventArgs e)
        {
            //mouse move and click the specific window coordinates
            x.MouseClick(CommonEnum.MouseButton.LEFT, 35, 21, 2, -1);
        }

        private void BtnControlFocus_Click(object sender, EventArgs e)
        {
            //in this example can't focus application if windowstate is minimize
            Process.Start("Notepad");
            x.ControlFocus(@"[Title:Untitled - Notepad; Class:Notepad]", "", "Edit1");
            x.WinWaitActive(@"[Title:Untitled - Notepad; Class:Notepad]");
            x.Send("This is some text.");
            x.WinClose("*Untitled - Notepad");

            x.WinWaitActive("Notepad", "Save");

            SendKeys.SendWait("!n");
        }

        private void BtnWaitActiveControl_Click(object sender, EventArgs e)
        {
            //Run notepad
            Process.Start("Notepad");

            x.WinWaitActive("Untitled - Notepad");

            x.Send("This is some text.");


            x.WinClose("*Untitled - Notepad");

            x.WinWaitActive("Notepad", "Save");

            SendKeys.SendWait("!n");


            #region Another example using WinWaitActive & WinClose
            ////Run notepad
            //Process.Start("Notepad");

            ////Wait 10 seconds for the Notepad window to appear.
            //x.WinWaitActive("[CLASS:Notepad]", "", 10);

            ////Wait for 2 seconds to display the Notepad window.
            //Thread.Sleep(2000);

            ////Close the Notepad window using the classname of Notepad.
            //x.WinClose("[CLASS:Notepad]");
            #endregion
        }

        private void BtnActivateWindow_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "Calc.exe";
            process.Start();

            x.WinActivate("Calculator");
            Thread.Sleep(3000);
            x.WinClose("Calculator");
        }

        static int lineNumber = 0;
        private void AppendLogMsg(string logMsg, int MsgOption)
        {
            try
            {
                logMsg = string.Format("{0} >>> {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), logMsg);
                lineNumber += 1;
                rhLog.SelectionStart = 0;
                rhLog.SelectedText = lineNumber.ToString() + ". " + logMsg + Constants.vbCrLf + rhLog.SelectedText;
                rhLog.Select(0, rhLog.Lines[0].Length);
                switch (MsgOption)
                {
                    case 0://MsgError
                        rhLog.SelectionColor = Color.Red;
                        break;
                    case 1://MsgReminding
                        rhLog.SelectionColor = Color.Black;
                        break;
                    case 2://MsgNormal
                        rhLog.SelectionColor = Color.Black;
                        break;
                    case 3://MsgIncoming
                        rhLog.SelectionColor = Color.Blue;
                        break;
                    case 4://MsgOutput
                        rhLog.SelectionColor = Color.ForestGreen;
                        break;
                    default:
                        {
                            rhLog.SelectionColor = Color.Black;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
            }
        }


        private void btnTriggerCoolDown_Click(object sender, EventArgs e)
        {

            try
            {
                string Title = "JT回流焊系统";
                x.WinActivate(Title);
                AppendLogMsg("激活 " + Title, 1);
                x.WinSetState(Title, "", 3);
                Thread.Sleep(200);
                int poinX = x.ControlGetPosX(Title, "", "[Class:WindowsForms10.Window.8.app.0.141b42a_r7_ad1]") - 320;// 0;// 155;
                int poinY = x.ControlGetPosY(Title, "", "[Class:WindowsForms10.Window.8.app.0.141b42a_r7_ad1]") - 90;// 0;// 65;
                AppendLogMsg("打开登陆界面", 1);
                x.MouseClick(CommonEnum.MouseButton.LEFT, poinX, poinY, 1, -1);

                Title = "登陆";
                x.WinWaitActive(Title);
                Thread.Sleep(1500);
                AppendLogMsg("输入 User Name", 1);
                x.ControlSetText(Title, "", "[NAME:txtUserName]", "user");
                Thread.Sleep(500);
                AppendLogMsg("输入 Password", 1);
                x.ControlSetText(Title, "", "[NAME:txtPwd]", "123");
                Thread.Sleep(2000);
                x.ControlClick(Title, "", "[NAME:btnLogin]");

                Thread.Sleep(1000);
                AppendLogMsg("打开冷却", 1);

                x.MouseClick(CommonEnum.MouseButton.LEFT, poinX - 180, poinY, 1, -1);

                Title = "JTR Series";
                x.WinWaitActive(Title);
                Thread.Sleep(2000);
                x.ControlClick(Title, "", "[NAME:btnOk]");

                x.WinWaitActive(Title);
                Thread.Sleep(3000);
                x.ControlClick(Title, "", "[NAME:btnOk]");
                AppendLogMsg("冷却操作完成", 1);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
