<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLNSvrSimulator
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
        Me.rhLog = New System.Windows.Forms.RichTextBox()
        Me.txtMeterCategory = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmdHostSimulator = New System.Windows.Forms.Button()
        Me.cmdApply = New System.Windows.Forms.Button()
        Me.txtOrgHex = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtAddr = New System.Windows.Forms.TextBox()
        Me.txtCommand = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'rhLog
        '
        Me.rhLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rhLog.Location = New System.Drawing.Point(5, 55)
        Me.rhLog.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.rhLog.Name = "rhLog"
        Me.rhLog.Size = New System.Drawing.Size(567, 352)
        Me.rhLog.TabIndex = 12
        Me.rhLog.Text = ""
        '
        'txtMeterCategory
        '
        Me.txtMeterCategory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMeterCategory.Location = New System.Drawing.Point(142, 17)
        Me.txtMeterCategory.Margin = New System.Windows.Forms.Padding(5)
        Me.txtMeterCategory.Name = "txtMeterCategory"
        Me.txtMeterCategory.Size = New System.Drawing.Size(167, 23)
        Me.txtMeterCategory.TabIndex = 5
        Me.txtMeterCategory.Text = "Electricity"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(21, 20)
        Me.Label2.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(111, 16)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Meter Category"
        '
        'cmdOK
        '
        Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOK.Location = New System.Drawing.Point(319, 13)
        Me.cmdOK.Margin = New System.Windows.Forms.Padding(5)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(117, 32)
        Me.cmdOK.TabIndex = 11
        Me.cmdOK.Text = "Start Service"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdHostSimulator
        '
        Me.cmdHostSimulator.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdHostSimulator.Location = New System.Drawing.Point(446, 13)
        Me.cmdHostSimulator.Margin = New System.Windows.Forms.Padding(5)
        Me.cmdHostSimulator.Name = "cmdHostSimulator"
        Me.cmdHostSimulator.Size = New System.Drawing.Size(126, 32)
        Me.cmdHostSimulator.TabIndex = 13
        Me.cmdHostSimulator.Text = "Host Simulator"
        Me.cmdHostSimulator.UseVisualStyleBackColor = True
        '
        'cmdApply
        '
        Me.cmdApply.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdApply.Location = New System.Drawing.Point(431, 414)
        Me.cmdApply.Name = "cmdApply"
        Me.cmdApply.Size = New System.Drawing.Size(133, 33)
        Me.cmdApply.TabIndex = 14
        Me.cmdApply.Text = "Apply"
        Me.cmdApply.UseVisualStyleBackColor = True
        '
        'txtOrgHex
        '
        Me.txtOrgHex.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOrgHex.Location = New System.Drawing.Point(78, 419)
        Me.txtOrgHex.Name = "txtOrgHex"
        Me.txtOrgHex.Size = New System.Drawing.Size(332, 23)
        Me.txtOrgHex.TabIndex = 15
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 422)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 16)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Hex Data"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(5, 461)
        Me.Label3.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 16)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Hex Data"
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(431, 453)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(133, 33)
        Me.Button1.TabIndex = 14
        Me.Button1.Text = "Apply"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtAddr
        '
        Me.txtAddr.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtAddr.Location = New System.Drawing.Point(78, 458)
        Me.txtAddr.Name = "txtAddr"
        Me.txtAddr.Size = New System.Drawing.Size(54, 23)
        Me.txtAddr.TabIndex = 15
        Me.txtAddr.Text = "1"
        '
        'txtCommand
        '
        Me.txtCommand.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCommand.Location = New System.Drawing.Point(142, 458)
        Me.txtCommand.Name = "txtCommand"
        Me.txtCommand.Size = New System.Drawing.Size(268, 23)
        Me.txtCommand.TabIndex = 15
        Me.txtCommand.Text = "03 20 37 00 02"
        '
        'frmLNSvrSimulator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(576, 493)
        Me.Controls.Add(Me.txtCommand)
        Me.Controls.Add(Me.txtAddr)
        Me.Controls.Add(Me.txtOrgHex)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.cmdApply)
        Me.Controls.Add(Me.cmdHostSimulator)
        Me.Controls.Add(Me.rhLog)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtMeterCategory)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmdOK)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmLNSvrSimulator"
        Me.Text = "LN Meter Service Simulator"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents rhLog As RichTextBox
    Friend WithEvents txtMeterCategory As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cmdOK As Button
    Friend WithEvents cmdHostSimulator As Button
    Friend WithEvents cmdApply As Button
    Friend WithEvents txtOrgHex As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents txtAddr As TextBox
    Friend WithEvents txtCommand As TextBox
End Class
