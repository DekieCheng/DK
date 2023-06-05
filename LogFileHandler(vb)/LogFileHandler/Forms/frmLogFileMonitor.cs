using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LogFileManagers;

public partial class frmLogFileMonitor : Form
{
    public frmLogFileMonitor()
    {

        InitializeComponent();
    }

    private void ProcessToParserLogFiles()
    {
        try
        {
            LogFileManagers.WriteDataToLocalFiles("Start log file process");
            DisplayMessage("Start log file process");
            LogFileManagers.ParserLogFiles();
        }
        catch (Exception ex)
        {

        }
    }

    private void FileCompletedHandler(string sMessage)
    {
        try
        {
            DisplayMessage(sMessage);
        }
        catch
        {
        }
    }

    public delegate void DisplayMessageInvoke(string Msg);
    public delegate void DisplayMessageExceptionInvoke(Exception ex);
    private void DisplayMessage(string Msg)
    {
        try
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new DisplayMessageInvoke(DisplayMessage), Msg);
            }
            else
            {
                ShowMsg(string.Format("{0:yyyy-MM-dd HH:mm:ss.ffff}", DateTime.Now) + " " + Msg + Environment.NewLine);
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void DisplayMessage(Exception ex)
    {
        try
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new DisplayMessageExceptionInvoke(DisplayMessage), ex);
            }
            else
            {
                ShowMsg(string.Format("{0:yyyy-MM-dd HH:mm:ss.ffff}", DateTime.Now) + " "
                + ex.Message + Environment.NewLine
                + ex.StackTrace + Environment.NewLine);
            }
        }
        catch (Exception exx)
        {
        }
    }

    private void ShowMsg(string Msg)
    {
        try
        {
            this.richTextBox1.AppendText(Msg);
        }
        catch (Exception exx)
        {
        }
    }

    int TotalFileQty = 0;
    public delegate void ProcessToSetTotalQtyInvoke(int Qty);
    private void ProcessToSetTotalQty(int Qty)
    {
        try
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ProcessToSetTotalQtyInvoke(SetTotalQty), Qty);
            }
            else
            {
                SetTotalQty(Qty);
            }
        }
        catch (Exception exx)
        {
        }
    }
    private void SetTotalQty(int TotalQty)
    {
        try
        {
            this.lblTotalQty.Text = TotalQty.ToString();
            TotalFileQty = TotalQty;
        }
        catch (Exception ex)
        {
        }
    }
    public delegate void ProcessToSetRemainingQtyInvoke(int Qty);
    private void ProcessToSetRemainingQty(int Qty)
    {
        try
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ProcessToSetRemainingQtyInvoke(SetRemainingQty), Qty);
            }
            else
            {
                SetRemainingQty(Qty);
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void SetRemainingQty(int RemainingQty)
    {
        try
        {
            TotalFileQty = TotalFileQty - RemainingQty;
            this.lblRemaining.Text = TotalFileQty.ToString();
        }
        catch (Exception exx)
        {
        }
    }


    private bool INITLogFileManager()
    {
        bool flag = false;
        try
        {
            LogFileManagers.OnFileCompleted += new FileCompletedHandler(FileCompletedHandler);
            LogFileManagers.OnStatusReport += new StatusReportHandler(DisplayMessage);
            LogFileManagers.OnInternalError += new ErrorHandler(DisplayMessage);
            LogFileManagers.OnFileAmount += new OnFileAmountHandler(ProcessToSetTotalQty);
            LogFileManagers.OnFileAmountChanged += new OnFileAmountChangedHandler(ProcessToSetRemainingQty);

            flag = true;
        }
        catch (Exception ex)
        {
            flag = false;
        }
        return flag;
    }

    private void UninstallEvents()
    {
        LogFileManagers.OnFileCompleted -= new FileCompletedHandler(FileCompletedHandler);
        LogFileManagers.OnStatusReport -= new StatusReportHandler(DisplayMessage);
        LogFileManagers.OnInternalError += new ErrorHandler(DisplayMessage);
        LogFileManagers.OnFileAmount -= new OnFileAmountHandler(ProcessToSetTotalQty);
        LogFileManagers.OnFileAmountChanged -= new OnFileAmountChangedHandler(ProcessToSetRemainingQty);
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        try
        {
            this.Text = Directory.GetCurrentDirectory();
            this.lblTotalQty.Text = "0";
            this.lblRemaining.Text = "0";
            INITLogFileManager();
        }
        catch (Exception ex)
        { ShowMsg(ex.Message); }
    }

    private void btnStart_Click(object sender, EventArgs e)
    {
        try
        {
            ProcessToParserLogFiles();
        }
        catch (Exception ex)
        {
            ShowMsg(ex.Message);
        }
    }
}

