<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintLabelByDosCmd
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCMD = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtOutput = New System.Windows.Forms.TextBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.cmdOpenFile = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(105, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Source ZPL file"
        '
        'txtCMD
        '
        Me.txtCMD.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCMD.Location = New System.Drawing.Point(12, 33)
        Me.txtCMD.Name = "txtCMD"
        Me.txtCMD.Size = New System.Drawing.Size(672, 22)
        Me.txtCMD.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(112, 17)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Output Message"
        '
        'txtOutput
        '
        Me.txtOutput.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOutput.Location = New System.Drawing.Point(12, 102)
        Me.txtOutput.Multiline = True
        Me.txtOutput.Name = "txtOutput"
        Me.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtOutput.Size = New System.Drawing.Size(672, 509)
        Me.txtOutput.TabIndex = 1
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'cmdOpenFile
        '
        Me.cmdOpenFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOpenFile.Location = New System.Drawing.Point(700, 31)
        Me.cmdOpenFile.Name = "cmdOpenFile"
        Me.cmdOpenFile.Size = New System.Drawing.Size(75, 23)
        Me.cmdOpenFile.TabIndex = 2
        Me.cmdOpenFile.Text = "..."
        Me.cmdOpenFile.UseVisualStyleBackColor = True
        '
        'cmdPrint
        '
        Me.cmdPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdPrint.Location = New System.Drawing.Point(700, 85)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.Size = New System.Drawing.Size(75, 29)
        Me.cmdPrint.TabIndex = 2
        Me.cmdPrint.Text = "Execute"
        Me.cmdPrint.UseVisualStyleBackColor = True
        '
        'frmPrintLabelByDosCmd
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(795, 623)
        Me.Controls.Add(Me.cmdPrint)
        Me.Controls.Add(Me.cmdOpenFile)
        Me.Controls.Add(Me.txtOutput)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCMD)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmPrintLabelByDosCmd"
        Me.Text = "Print Label By Dos Cmd"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txtCMD As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtOutput As TextBox
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents cmdOpenFile As Button
    Friend WithEvents cmdPrint As Button
End Class
