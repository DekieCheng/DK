namespace ReportsApplication
{
    partial class frmRDLCReportSimulator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

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
            this.rptViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.lblUDPName = new System.Windows.Forms.Label();
            this.txtUDPName = new System.Windows.Forms.TextBox();
            this.lblRDLCPath = new System.Windows.Forms.Label();
            this.txtRDLCPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDBConnection = new System.Windows.Forms.TextBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rptViewer
            // 
            this.rptViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rptViewer.Location = new System.Drawing.Point(0, 118);
            this.rptViewer.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.rptViewer.Name = "rptViewer";
            this.rptViewer.ServerReport.BearerToken = null;
            this.rptViewer.Size = new System.Drawing.Size(959, 448);
            this.rptViewer.TabIndex = 0;
            // 
            // lblUDPName
            // 
            this.lblUDPName.AutoSize = true;
            this.lblUDPName.Location = new System.Drawing.Point(6, 88);
            this.lblUDPName.Name = "lblUDPName";
            this.lblUDPName.Size = new System.Drawing.Size(95, 22);
            this.lblUDPName.TabIndex = 1;
            this.lblUDPName.Text = "UDP Name";
            // 
            // txtUDPName
            // 
            this.txtUDPName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUDPName.Location = new System.Drawing.Point(138, 85);
            this.txtUDPName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtUDPName.Name = "txtUDPName";
            this.txtUDPName.Size = new System.Drawing.Size(699, 30);
            this.txtUDPName.TabIndex = 2;
            this.txtUDPName.Text = "udpGetReportDataVS2017";
            // 
            // lblRDLCPath
            // 
            this.lblRDLCPath.AutoSize = true;
            this.lblRDLCPath.Location = new System.Drawing.Point(6, 52);
            this.lblRDLCPath.Name = "lblRDLCPath";
            this.lblRDLCPath.Size = new System.Drawing.Size(109, 22);
            this.lblRDLCPath.TabIndex = 1;
            this.lblRDLCPath.Text = ".rdlc file Path";
            // 
            // txtRDLCPath
            // 
            this.txtRDLCPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRDLCPath.Location = new System.Drawing.Point(138, 49);
            this.txtRDLCPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRDLCPath.Name = "txtRDLCPath";
            this.txtRDLCPath.Size = new System.Drawing.Size(699, 30);
            this.txtRDLCPath.TabIndex = 2;
            this.txtRDLCPath.Text = "D:\\08others\\00SampleCodes\\ReportsApplication2\\ReportsApplication\\ReportsApplicati" +
    "on\\Report1.rdlc";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "DB Connection";
            // 
            // txtDBConnection
            // 
            this.txtDBConnection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDBConnection.Location = new System.Drawing.Point(138, 12);
            this.txtDBConnection.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDBConnection.Name = "txtDBConnection";
            this.txtDBConnection.Size = new System.Drawing.Size(699, 30);
            this.txtDBConnection.TabIndex = 2;
            this.txtDBConnection.Text = "Data Source=NSCM0295\\\\FFMES2K17;Initial Catalog=FF2_9_3;Integrated Security=True";
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.Location = new System.Drawing.Point(842, 84);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(116, 31);
            this.btnQuery.TabIndex = 3;
            this.btnQuery.Text = "Query";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // frmRDLCReportSimulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 566);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.txtDBConnection);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtRDLCPath);
            this.Controls.Add(this.lblRDLCPath);
            this.Controls.Add(this.txtUDPName);
            this.Controls.Add(this.lblUDPName);
            this.Controls.Add(this.rptViewer);
            this.Font = new System.Drawing.Font("Segoe UI Emoji", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "frmRDLCReportSimulator";
            this.Text = "RDLC Report Simulator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rptViewer;
        private System.Windows.Forms.Label lblUDPName;
        private System.Windows.Forms.TextBox txtUDPName;
        private System.Windows.Forms.Label lblRDLCPath;
        private System.Windows.Forms.TextBox txtRDLCPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDBConnection;
        private System.Windows.Forms.Button btnQuery;
    }
}

