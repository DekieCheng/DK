<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintLabelSimulator
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
        Me.txtZPLFile = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtZPLFileContent = New System.Windows.Forms.TextBox()
        Me.txtPrinterName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmdOpenZplFile = New System.Windows.Forms.Button()
        Me.txtPrint = New System.Windows.Forms.Button()
        Me.rbAnsi = New System.Windows.Forms.RadioButton()
        Me.rbUni = New System.Windows.Forms.RadioButton()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SuspendLayout()
        '
        'txtZPLFile
        '
        Me.txtZPLFile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtZPLFile.Location = New System.Drawing.Point(124, 3)
        Me.txtZPLFile.Name = "txtZPLFile"
        Me.txtZPLFile.Size = New System.Drawing.Size(559, 22)
        Me.txtZPLFile.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "ZPL File File"
        '
        'txtZPLFileContent
        '
        Me.txtZPLFileContent.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtZPLFileContent.Location = New System.Drawing.Point(8, 64)
        Me.txtZPLFileContent.Multiline = True
        Me.txtZPLFileContent.Name = "txtZPLFileContent"
        Me.txtZPLFileContent.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtZPLFileContent.Size = New System.Drawing.Size(674, 358)
        Me.txtZPLFileContent.TabIndex = 0
        '
        'txtPrinterName
        '
        Me.txtPrinterName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPrinterName.Location = New System.Drawing.Point(124, 36)
        Me.txtPrinterName.Name = "txtPrinterName"
        Me.txtPrinterName.Size = New System.Drawing.Size(559, 22)
        Me.txtPrinterName.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(91, 17)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Printer Name"
        '
        'cmdOpenZplFile
        '
        Me.cmdOpenZplFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOpenZplFile.Location = New System.Drawing.Point(689, 2)
        Me.cmdOpenZplFile.Name = "cmdOpenZplFile"
        Me.cmdOpenZplFile.Size = New System.Drawing.Size(67, 26)
        Me.cmdOpenZplFile.TabIndex = 2
        Me.cmdOpenZplFile.Text = "..."
        Me.cmdOpenZplFile.UseVisualStyleBackColor = True
        '
        'txtPrint
        '
        Me.txtPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPrint.Location = New System.Drawing.Point(689, 36)
        Me.txtPrint.Name = "txtPrint"
        Me.txtPrint.Size = New System.Drawing.Size(67, 26)
        Me.txtPrint.TabIndex = 2
        Me.txtPrint.Text = "Print"
        Me.txtPrint.UseVisualStyleBackColor = True
        '
        'rbAnsi
        '
        Me.rbAnsi.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rbAnsi.AutoSize = True
        Me.rbAnsi.Checked = True
        Me.rbAnsi.Location = New System.Drawing.Point(690, 96)
        Me.rbAnsi.Name = "rbAnsi"
        Me.rbAnsi.Size = New System.Drawing.Size(56, 21)
        Me.rbAnsi.TabIndex = 3
        Me.rbAnsi.TabStop = True
        Me.rbAnsi.Text = "Ansi"
        Me.rbAnsi.UseVisualStyleBackColor = True
        '
        'rbUni
        '
        Me.rbUni.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rbUni.AutoSize = True
        Me.rbUni.Location = New System.Drawing.Point(690, 123)
        Me.rbUni.Name = "rbUni"
        Me.rbUni.Size = New System.Drawing.Size(50, 21)
        Me.rbUni.TabIndex = 4
        Me.rbUni.Text = "Uni"
        Me.rbUni.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(761, 434)
        Me.Controls.Add(Me.rbUni)
        Me.Controls.Add(Me.rbAnsi)
        Me.Controls.Add(Me.txtPrint)
        Me.Controls.Add(Me.cmdOpenZplFile)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtPrinterName)
        Me.Controls.Add(Me.txtZPLFileContent)
        Me.Controls.Add(Me.txtZPLFile)
        Me.Name = "Form1"
        Me.Text = "Print Label Simulator"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtZPLFile As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtZPLFileContent As TextBox
    Friend WithEvents txtPrinterName As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents cmdOpenZplFile As Button
    Friend WithEvents txtPrint As Button
    Friend WithEvents rbAnsi As RadioButton
    Friend WithEvents rbUni As RadioButton
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
End Class
