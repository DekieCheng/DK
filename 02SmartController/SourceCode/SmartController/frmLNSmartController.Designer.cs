namespace AutoItLee
{
    partial class frmLNSmartController
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
            this.btnTriggerCoolDown = new System.Windows.Forms.Button();
            this.rhLog = new System.Windows.Forms.RichTextBox();
            this.chkManually = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnTriggerCoolDown
            // 
            this.btnTriggerCoolDown.Location = new System.Drawing.Point(381, 333);
            this.btnTriggerCoolDown.Margin = new System.Windows.Forms.Padding(4);
            this.btnTriggerCoolDown.Name = "btnTriggerCoolDown";
            this.btnTriggerCoolDown.Size = new System.Drawing.Size(159, 38);
            this.btnTriggerCoolDown.TabIndex = 4;
            this.btnTriggerCoolDown.Text = "Trigger Cool Down";
            this.btnTriggerCoolDown.UseVisualStyleBackColor = true;
            this.btnTriggerCoolDown.Click += new System.EventHandler(this.btnTriggerCoolDown_Click);
            // 
            // rhLog
            // 
            this.rhLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rhLog.Location = new System.Drawing.Point(5, 5);
            this.rhLog.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rhLog.Name = "rhLog";
            this.rhLog.Size = new System.Drawing.Size(535, 321);
            this.rhLog.TabIndex = 22;
            this.rhLog.Text = "";
            // 
            // chkManually
            // 
            this.chkManually.AutoSize = true;
            this.chkManually.Location = new System.Drawing.Point(13, 340);
            this.chkManually.Name = "chkManually";
            this.chkManually.Size = new System.Drawing.Size(84, 20);
            this.chkManually.TabIndex = 23;
            this.chkManually.Text = "Manually";
            this.chkManually.UseVisualStyleBackColor = true;
            // 
            // frmLNSmartController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 378);
            this.Controls.Add(this.chkManually);
            this.Controls.Add(this.rhLog);
            this.Controls.Add(this.btnTriggerCoolDown);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmLNSmartController";
            this.Text = "LNSmart Controller";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnTriggerCoolDown;
        private System.Windows.Forms.RichTextBox rhLog;
        private System.Windows.Forms.CheckBox chkManually;
    }
}