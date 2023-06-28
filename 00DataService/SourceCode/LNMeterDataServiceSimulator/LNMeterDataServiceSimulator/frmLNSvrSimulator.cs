using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using Microsoft.VisualBasic;
using Model;
using LNMeterInfrastructure.BLL;
using LNMeterInfrastructure.Common;
using System.Timers;
using ServiceMgr;
using System.Drawing;
using System.Windows.Forms;

public partial class frmLNSvrSimulator : Form
{
    private System.Timers.Timer _Cycletimer;
    private LNMeterCategory _lMeterCategory = null;

    public frmLNSvrSimulator()
    {
        InitializeComponent();
    }

    private void FireService()
    {
        try
        {
            AppendLogMsg("Service is starting.", (int)Variables.MsgCategoryFlag.MsgNormal);
            _lMeterCategory = new LNMeterCategory(LNGlobal.g_ServiceFilter);
            if (chkTimerControl.Checked)
            {
                _Cycletimer = new System.Timers.Timer(1);
                _Cycletimer.Elapsed += new ElapsedEventHandler(ProcessToTimerElapsed);
                _Cycletimer.Start();
            }
            else
            {
                LNSvrMgr.FireCollectDataAction(_lMeterCategory);
            }

            AppendLogMsg("Service is started successfully.", (int)Variables.MsgCategoryFlag.MsgNormal);
        }
        catch (Exception ex)
        {
            AppendLogMsg(ex.Message + Constants.vbCrLf + ex.StackTrace, (int)Variables.MsgCategoryFlag.MsgError);
        }
    }

    private void ProcessToTimerElapsed(object sender, ElapsedEventArgs e)
    {
        try
        {
            _Cycletimer.Interval = _lMeterCategory.CycleTime * 1000;
            //LNLogMgr.WriteLogAsy(string.Format("Service is running for the filter {0} with interval {1} seconds.", LNGlobal.g_ServiceFilter, _lMeterCategory.CycleTime.ToString()));
            AppendLogMsg(string.Format("Service is running for the filter {0} with interval {1} seconds.", LNGlobal.g_ServiceFilter, _lMeterCategory.CycleTime.ToString()), (int)Variables.MsgCategoryFlag.MsgNormal);
            LNSvrMgr.FireCollectDataAction(_lMeterCategory);
        }
        catch (Exception ex)
        {
            AppendLogMsg(ex.Message + Constants.vbCrLf + ex.StackTrace, (int)Variables.MsgCategoryFlag.MsgError);
        }
    }

    protected void StopService()
    {
        try
        {
            AppendLogMsg("Stopping the service.", (int)Variables.MsgCategoryFlag.MsgNormal);
            _Cycletimer.Stop();
            _Cycletimer.Dispose();
            AppendLogMsg("Service is stopped successfully.", (int)Variables.MsgCategoryFlag.MsgNormal);
        }
        catch (Exception ex)
        {
            AppendLogMsg(ex.Message + Constants.vbCrLf + ex.StackTrace, (int)Variables.MsgCategoryFlag.MsgError);
        }
    }

    private delegate void AppendLogMsgDel(string logMsg, Variables.MsgCategoryFlag MsgOption);
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

    private void cmdStartService_Click(object sender, EventArgs e)
    {
        try
        {
            FireService();
        }
        catch (Exception ex)
        {
            AppendLogMsg(ex.Message + Constants.vbCrLf + ex.StackTrace, (int)Variables.MsgCategoryFlag.MsgError);
        }
    }

    private void Button1_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            AppendLogMsg(ex.Message + Constants.vbCrLf + ex.StackTrace, (int)Variables.MsgCategoryFlag.MsgError);
        }
    }

    private void cmdApply_Click(object sender, EventArgs e)
    {
        try
        {
            float lnA = IEEE754Helper.HexToFloat(this.txtOrgHex.Text.Replace(" ", ""));
            AppendLogMsg(string.Format("Hex to Float:Hex[{1}] --> Float[{0}]", lnA.ToString(), this.txtOrgHex.Text), (int)Variables.MsgCategoryFlag.MsgNormal);
        }
        catch (Exception ex)
        {
            AppendLogMsg(ex.Message + Constants.vbCrLf + ex.StackTrace, (int)Variables.MsgCategoryFlag.MsgError);
        }
    }

    private void Button1_Click_1(object sender, EventArgs e)
    {
        try
        {
            string lnCommand = LNSvrMgr.ToGenCommand(this.txtAddr.Text, this.txtCommand.Text);
            AppendLogMsg(string.Format("Hex Command: [{0}]", lnCommand), (int)Variables.MsgCategoryFlag.MsgNormal);
        }
        catch (Exception ex)
        {
            AppendLogMsg(ex.Message + Constants.vbCrLf + ex.StackTrace, (int)Variables.MsgCategoryFlag.MsgError);
        }
    }
}
