using System.Windows.Forms;

partial class frmLogFileMonitor
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
        UninstallEvents();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.richTextBox1 = new System.Windows.Forms.RichTextBox();
        this.btnStart = new System.Windows.Forms.Button();
        this.lblRemaining = new System.Windows.Forms.Label();
        this.lblTotalQty = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // richTextBox1
        // 
        this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
        | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.richTextBox1.Location = new System.Drawing.Point(6, 5);
        this.richTextBox1.Name = "richTextBox1";
        this.richTextBox1.Size = new System.Drawing.Size(1071, 552);
        this.richTextBox1.TabIndex = 0;
        this.richTextBox1.Text = "";
        // 
        // btnStart
        // 
        this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.btnStart.Location = new System.Drawing.Point(904, 562);
        this.btnStart.Name = "btnStart";
        this.btnStart.Size = new System.Drawing.Size(168, 93);
        this.btnStart.TabIndex = 1;
        this.btnStart.Text = "Start";
        this.btnStart.UseVisualStyleBackColor = true;
        this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
        // 
        // lblRemaining
        // 
        this.lblRemaining.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.lblRemaining.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblRemaining.ForeColor = System.Drawing.Color.Red;
        this.lblRemaining.Location = new System.Drawing.Point(235, 612);
        this.lblRemaining.Name = "lblRemaining";
        this.lblRemaining.Size = new System.Drawing.Size(332, 38);
        this.lblRemaining.TabIndex = 3;
        this.lblRemaining.Text = "10000";
        // 
        // lblTotalQty
        // 
        this.lblTotalQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.lblTotalQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblTotalQty.ForeColor = System.Drawing.Color.Blue;
        this.lblTotalQty.Location = new System.Drawing.Point(235, 560);
        this.lblTotalQty.Name = "lblTotalQty";
        this.lblTotalQty.Size = new System.Drawing.Size(332, 38);
        this.lblTotalQty.TabIndex = 3;
        this.lblTotalQty.Text = "10000";
        // 
        // label2
        // 
        this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.label2.AutoSize = true;
        this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label2.Location = new System.Drawing.Point(-1, 560);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(150, 38);
        this.label2.TabIndex = 3;
        this.label2.Text = "Total Qty:";
        // 
        // label3
        // 
        this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.label3.AutoSize = true;
        this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label3.Location = new System.Drawing.Point(-1, 615);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(172, 38);
        this.label3.TabIndex = 3;
        this.label3.Text = "Remaining:";
        // 
        // frmLogFileMonitor
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1084, 665);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.lblTotalQty);
        this.Controls.Add(this.lblRemaining);
        this.Controls.Add(this.btnStart);
        this.Controls.Add(this.richTextBox1);
        this.Name = "frmLogFileMonitor";
        this.Text = "frmLogFileMonitor";
        this.Load += new System.EventHandler(this.Form1_Load);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.RichTextBox richTextBox1;
    private System.Windows.Forms.Button btnStart;
    private Label lblRemaining;
    private Label lblTotalQty;
    private Label label2;
    private Label label3;
}