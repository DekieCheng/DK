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
        Me.rhLog.Size = New System.Drawing.Size(567, 433)
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
        'frmLNSvrSimulator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(576, 493)
        Me.Controls.Add(Me.cmdHostSimulator)
        Me.Controls.Add(Me.rhLog)
        Me.Controls.Add(Me.txtMeterCategory)
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
End Class
