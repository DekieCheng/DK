
namespace FFObjectViewer
{
    partial class frmObjetViewer
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
            this.btnGetInfo = new System.Windows.Forms.Button();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdBrow = new System.Windows.Forms.Button();
            this.rtInfo = new System.Windows.Forms.RichTextBox();
            this.chkPara = new System.Windows.Forms.CheckBox();
            this.chkPro = new System.Windows.Forms.CheckBox();
            this.txtDBScript = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdDBScript = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGetInfo
            // 
            this.btnGetInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetInfo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetInfo.Location = new System.Drawing.Point(535, 507);
            this.btnGetInfo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnGetInfo.Name = "btnGetInfo";
            this.btnGetInfo.Size = new System.Drawing.Size(98, 62);
            this.btnGetInfo.TabIndex = 1;
            this.btnGetInfo.Text = "Get Info";
            this.btnGetInfo.UseVisualStyleBackColor = true;
            this.btnGetInfo.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // txtFile
            // 
            this.txtFile.AllowDrop = true;
            this.txtFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFile.Location = new System.Drawing.Point(7, 358);
            this.txtFile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtFile.Multiline = true;
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(457, 95);
            this.txtFile.TabIndex = 2;
            this.txtFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtFile_DragDrop);
            this.txtFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtFile_DragEnter);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 341);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(218, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "File path (project, DLL or EXE etc)";
            // 
            // cmdBrow
            // 
            this.cmdBrow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBrow.Location = new System.Drawing.Point(464, 390);
            this.cmdBrow.Name = "cmdBrow";
            this.cmdBrow.Size = new System.Drawing.Size(29, 63);
            this.cmdBrow.TabIndex = 5;
            this.cmdBrow.Text = "...";
            this.cmdBrow.UseVisualStyleBackColor = true;
            this.cmdBrow.Click += new System.EventHandler(this.cmdBrow_Click);
            // 
            // rtInfo
            // 
            this.rtInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtInfo.Location = new System.Drawing.Point(4, 4);
            this.rtInfo.Name = "rtInfo";
            this.rtInfo.Size = new System.Drawing.Size(632, 334);
            this.rtInfo.TabIndex = 6;
            this.rtInfo.Text = "";
            // 
            // chkPara
            // 
            this.chkPara.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkPara.AutoSize = true;
            this.chkPara.Location = new System.Drawing.Point(519, 407);
            this.chkPara.Name = "chkPara";
            this.chkPara.Size = new System.Drawing.Size(125, 18);
            this.chkPara.TabIndex = 7;
            this.chkPara.Text = "Get Parameters";
            this.chkPara.UseVisualStyleBackColor = true;
            // 
            // chkPro
            // 
            this.chkPro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkPro.AutoSize = true;
            this.chkPro.Location = new System.Drawing.Point(519, 435);
            this.chkPro.Name = "chkPro";
            this.chkPro.Size = new System.Drawing.Size(117, 18);
            this.chkPro.TabIndex = 7;
            this.chkPro.Text = "Get Properties";
            this.chkPro.UseVisualStyleBackColor = true;
            // 
            // txtDBScript
            // 
            this.txtDBScript.AllowDrop = true;
            this.txtDBScript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDBScript.Location = new System.Drawing.Point(4, 474);
            this.txtDBScript.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtDBScript.Multiline = true;
            this.txtDBScript.Name = "txtDBScript";
            this.txtDBScript.Size = new System.Drawing.Size(457, 95);
            this.txtDBScript.TabIndex = 2;
            this.txtDBScript.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtDBScript_DragDrop);
            this.txtDBScript.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtDBScript_DragEnter);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 457);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "DB Script path";
            // 
            // cmdDBScript
            // 
            this.cmdDBScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDBScript.Location = new System.Drawing.Point(461, 507);
            this.cmdDBScript.Name = "cmdDBScript";
            this.cmdDBScript.Size = new System.Drawing.Size(29, 63);
            this.cmdDBScript.TabIndex = 5;
            this.cmdDBScript.Text = "...";
            this.cmdDBScript.UseVisualStyleBackColor = true;
            this.cmdDBScript.Click += new System.EventHandler(this.cmdDBScript_Click);
            // 
            // frmObjetViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 576);
            this.Controls.Add(this.chkPro);
            this.Controls.Add(this.chkPara);
            this.Controls.Add(this.rtInfo);
            this.Controls.Add(this.cmdDBScript);
            this.Controls.Add(this.cmdBrow);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDBScript);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.btnGetInfo);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmObjetViewer";
            this.Text = "Object Viewer";
            this.Load += new System.EventHandler(this.frmObjetViewer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnGetInfo;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdBrow;
        private System.Windows.Forms.RichTextBox rtInfo;
        private System.Windows.Forms.CheckBox chkPara;
        private System.Windows.Forms.CheckBox chkPro;
        private System.Windows.Forms.TextBox txtDBScript;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdDBScript;
    }
}

