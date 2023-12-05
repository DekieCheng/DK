using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualBasic;
using System.Configuration;
using System.Timers;
using System.IO;
using AutoIt;


namespace LNSmartController
{
    public partial class frmLNSmartController : Form
    {

        public frmLNSmartController()
        {
            InitializeComponent();

            InitTimer();
        }

        private void btnTriggerCoolDown_Click(object sender, EventArgs e)
        {
            try
            {
                AppendLogMsg("开始检测是否满足自动Cool Down 条件.", 1);
                Thread.Sleep(1300);
                AppendLogMsg("在EPPS中检查生产排产计划.", 1);
                Thread.Sleep(1300);
                string PlanPOInfo = LNSmartController.SmartControllerMgr.GetPOInfofromEPPS();
                if (string.IsNullOrEmpty(PlanPOInfo))
                {
                    AppendLogMsg("当前生产中，不符合Cool down", 1);
                    return;
                }
                else
                {
                    AppendLogMsg("最新排产信息如下：", 1);
                    AppendLogMsg(Environment.NewLine + PlanPOInfo, 1);
                }

                AppendLogMsg("结合EPPS的排产计划和flexflow中炉前和炉后生产工单信息, 检查是否满足Cool down 条件.", 1);
                string coolDownMsg = string.Empty;
                if (!LNSmartController.SmartControllerMgr.FFExecuteCoolDown(PlanPOInfo, ref coolDownMsg))
                {
                    AppendLogMsg("结合EPPS的排产计划和flexflow中炉前和炉后生产工单信息, 无满足Cool down 条件.", 1);
                    return;
                }
                else
                {
                    AppendLogMsg(coolDownMsg, 1);
                    //ProcessToExecuteCoolDown();
                }
            }
            catch (Exception ex)
            {
                AppendLogMsg(ex.Message + ex.StackTrace, 0);
            }
        }

        private void InitTimer()
        {
            System.Timers.Timer _TriggerTimer = new System.Timers.Timer();
            _TriggerTimer.Interval = GetIntervalTime() * 1000.00;
            _TriggerTimer.Elapsed += new ElapsedEventHandler(ProcessToTimerElapsed);
            _TriggerTimer.Start();
        }

        private void ProcessToTimerElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (chkManually.Checked) return;

                btnTriggerCoolDown_Click(null, null);
            }
            catch (Exception ex)
            {
                AppendLogMsg(ex.Message + ex.StackTrace, 0);
            }
        }

        private void ProcessToExecuteCoolDown()
        {
            try
            {
                AppendLogMsg("开始执行冷却操作", 1);

                const string mouse_left = "LEFT";
                string Title = "JT回流焊系统";
                AutoItX.WinActivate(Title);
                AppendLogMsg("激活 " + Title, 1);
                AutoItX.WinSetState(Title, "", 3);
                Thread.Sleep(200);
                Rectangle rectangle = AutoItX.ControlGetPos(Title, "", "[Class:WindowsForms10.Window.8.app.0.141b42a_r7_ad1]");

                int poinX = rectangle.X - 320;
                int poinY = rectangle.Y - 90;

                AppendLogMsg("打开登陆界面", 1);
                AutoItX.MouseClick(mouse_left, poinX, poinY, 1, -1);

                Title = "登陆";
                AutoItX.WinActivate(Title);
                Thread.Sleep(1000);
                AppendLogMsg("输入 User Name", 1);
                AutoItX.ControlSetText(Title, "", "[NAME:txtUserName]", GetUserName());

                Thread.Sleep(500);
                AppendLogMsg("输入 Password", 1);
                AutoItX.ControlSetText(Title, "", "[NAME:txtPwd]", GetPWD());

                Thread.Sleep(1000);
                AutoItX.ControlClick(Title, "", "[NAME:btnLogin]");

                Thread.Sleep(1000);
                AppendLogMsg("执行冷却操作", 1);

                //AutoItX.MouseClick(mouse_left, poinX - 180, poinY, 1, -1);

                Title = "JTR Series";
                //AutoItX.WinWaitActive(Title);
                //Thread.Sleep(1000);
                //AutoItX.ControlClick(Title, "", "[NAME:btnOk]");

                //AutoItX.WinWaitActive(Title);
                //Thread.Sleep(1000);
                //AutoItX.ControlClick(Title, "", "[NAME:btnOk]");
                AppendLogMsg("冷却操作完成", 1);
            }
            catch (Exception ex)
            {
                AppendLogMsg(ex.Message + ex.StackTrace, 0);
            }
        }
        private string GetUserName()
        {
            return ConfigurationManager.AppSettings.Get("UserName");
        }
        private string GetPWD()
        {
            return ConfigurationManager.AppSettings.Get("Password");
        }
        private int GetIntervalTime()
        {
            return int.Parse(ConfigurationManager.AppSettings.Get("IntervalTime"));
        }
        private string GetSourceau3File()
        {
            return ConfigurationManager.AppSettings.Get("Sourceau3File");
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

        private void BtnMouseClick_Click(object sender, EventArgs e)
        {
            //mouse move and click the specific window coordinates
            AutoItX.MouseClick("LEFT", 35, 21, 2, -1);
        }

        private void frmLNSmartController_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.btnTriggerCoolDown.Enabled = this.chkManually.Checked;
        }

        private void chkManually_CheckedChanged(object sender, EventArgs e)
        {
            this.btnTriggerCoolDown.Enabled = this.chkManually.Checked;
        }
    }
}
