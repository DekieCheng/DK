using System;
using System.Windows.Forms;
using System.Drawing;

partial class frmLNSvrSimulator
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        Control.CheckForIllegalCrossThreadCalls = false;
        this.txtCommand = new System.Windows.Forms.TextBox();
        this.txtAddr = new System.Windows.Forms.TextBox();
        this.txtOrgHex = new System.Windows.Forms.TextBox();
        this.cmdGenCommand = new System.Windows.Forms.Button();
        this.cmdApply = new System.Windows.Forms.Button();
        this.rhLog = new System.Windows.Forms.RichTextBox();
        this.Label3 = new System.Windows.Forms.Label();
        this.txtMeterCategory = new System.Windows.Forms.TextBox();
        this.Label1 = new System.Windows.Forms.Label();
        this.Label2 = new System.Windows.Forms.Label();
        this.cmdOK = new System.Windows.Forms.Button();
        this.chkTimerControl = new System.Windows.Forms.CheckBox();
        this.SuspendLayout();
        // 
        // txtCommand
        // 
        this.txtCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.txtCommand.Location = new System.Drawing.Point(153, 431);
        this.txtCommand.Name = "txtCommand";
        this.txtCommand.Size = new System.Drawing.Size(257, 23);
        this.txtCommand.TabIndex = 25;
        this.txtCommand.Text = "03 20 37 00 02";
        // 
        // txtAddr
        // 
        this.txtAddr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.txtAddr.Location = new System.Drawing.Point(89, 431);
        this.txtAddr.Name = "txtAddr";
        this.txtAddr.Size = new System.Drawing.Size(54, 23);
        this.txtAddr.TabIndex = 26;
        this.txtAddr.Text = "1";
        // 
        // txtOrgHex
        // 
        this.txtOrgHex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.txtOrgHex.Location = new System.Drawing.Point(89, 392);
        this.txtOrgHex.Name = "txtOrgHex";
        this.txtOrgHex.Size = new System.Drawing.Size(321, 23);
        this.txtOrgHex.TabIndex = 27;
        // 
        // cmdGenCommand
        // 
        this.cmdGenCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.cmdGenCommand.Location = new System.Drawing.Point(416, 426);
        this.cmdGenCommand.Name = "cmdGenCommand";
        this.cmdGenCommand.Size = new System.Drawing.Size(111, 33);
        this.cmdGenCommand.TabIndex = 23;
        this.cmdGenCommand.Text = "Get Command";
        this.cmdGenCommand.UseVisualStyleBackColor = true;
        this.cmdGenCommand.Click += new System.EventHandler(this.Button1_Click_1);
        // 
        // cmdApply
        // 
        this.cmdApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.cmdApply.Location = new System.Drawing.Point(416, 387);
        this.cmdApply.Name = "cmdApply";
        this.cmdApply.Size = new System.Drawing.Size(111, 33);
        this.cmdApply.TabIndex = 24;
        this.cmdApply.Text = "Apply";
        this.cmdApply.UseVisualStyleBackColor = true;
        this.cmdApply.Click += new System.EventHandler(this.cmdApply_Click);
        // 
        // rhLog
        // 
        this.rhLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
        | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.rhLog.Location = new System.Drawing.Point(4, 76);
        this.rhLog.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
        this.rhLog.Name = "rhLog";
        this.rhLog.Size = new System.Drawing.Size(523, 306);
        this.rhLog.TabIndex = 21;
        this.rhLog.Text = "";
        // 
        // Label3
        // 
        this.Label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.Label3.AutoSize = true;
        this.Label3.Location = new System.Drawing.Point(16, 434);
        this.Label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
        this.Label3.Name = "Label3";
        this.Label3.Size = new System.Drawing.Size(68, 16);
        this.Label3.TabIndex = 17;
        this.Label3.Text = "Hex Data";
        // 
        // txtMeterCategory
        // 
        this.txtMeterCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.txtMeterCategory.Location = new System.Drawing.Point(145, 11);
        this.txtMeterCategory.Margin = new System.Windows.Forms.Padding(5);
        this.txtMeterCategory.Name = "txtMeterCategory";
        this.txtMeterCategory.Size = new System.Drawing.Size(255, 23);
        this.txtMeterCategory.TabIndex = 16;
        this.txtMeterCategory.Text = "Electricity";
        // 
        // Label1
        // 
        this.Label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.Label1.AutoSize = true;
        this.Label1.Location = new System.Drawing.Point(16, 395);
        this.Label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
        this.Label1.Name = "Label1";
        this.Label1.Size = new System.Drawing.Size(68, 16);
        this.Label1.TabIndex = 18;
        this.Label1.Text = "Hex Data";
        // 
        // Label2
        // 
        this.Label2.AutoSize = true;
        this.Label2.Location = new System.Drawing.Point(24, 13);
        this.Label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
        this.Label2.Name = "Label2";
        this.Label2.Size = new System.Drawing.Size(111, 16);
        this.Label2.TabIndex = 19;
        this.Label2.Text = "Meter Category";
        // 
        // cmdOK
        // 
        this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.cmdOK.Location = new System.Drawing.Point(410, 6);
        this.cmdOK.Margin = new System.Windows.Forms.Padding(5);
        this.cmdOK.Name = "cmdOK";
        this.cmdOK.Size = new System.Drawing.Size(117, 32);
        this.cmdOK.TabIndex = 20;
        this.cmdOK.Text = "Start Service";
        this.cmdOK.UseVisualStyleBackColor = true;
        this.cmdOK.Click += new System.EventHandler(this.cmdStartService_Click);
        // 
        // chkTimerControl
        // 
        this.chkTimerControl.AutoSize = true;
        this.chkTimerControl.Location = new System.Drawing.Point(145, 43);
        this.chkTimerControl.Name = "chkTimerControl";
        this.chkTimerControl.Size = new System.Drawing.Size(163, 20);
        this.chkTimerControl.TabIndex = 28;
        this.chkTimerControl.Text = "Enable Timer Control";
        this.chkTimerControl.UseVisualStyleBackColor = true;
        // 
        // frmLNSvrSimulator
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(531, 466);
        this.Controls.Add(this.chkTimerControl);
        this.Controls.Add(this.txtCommand);
        this.Controls.Add(this.txtAddr);
        this.Controls.Add(this.txtOrgHex);
        this.Controls.Add(this.cmdGenCommand);
        this.Controls.Add(this.cmdApply);
        this.Controls.Add(this.rhLog);
        this.Controls.Add(this.Label3);
        this.Controls.Add(this.txtMeterCategory);
        this.Controls.Add(this.Label1);
        this.Controls.Add(this.Label2);
        this.Controls.Add(this.cmdOK);
        this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Margin = new System.Windows.Forms.Padding(4);
        this.Name = "frmLNSvrSimulator";
        this.Text = "frmLNSvrSimulator";
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtCommand;
    private System.Windows.Forms.TextBox txtAddr;
    private System.Windows.Forms.TextBox txtOrgHex;
    private System.Windows.Forms.Button cmdGenCommand;
    private System.Windows.Forms.Button cmdApply;
    private System.Windows.Forms.RichTextBox rhLog;
    private System.Windows.Forms.Label Label3;
    private System.Windows.Forms.TextBox txtMeterCategory;
    private System.Windows.Forms.Label Label1;
    private System.Windows.Forms.Label Label2;
    private System.Windows.Forms.Button cmdOK;
    private CheckBox chkTimerControl;
}

