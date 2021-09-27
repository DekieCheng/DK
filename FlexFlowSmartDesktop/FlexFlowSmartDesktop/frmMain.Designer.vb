<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.lvFFClientList = New System.Windows.Forms.ListView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.SuspendLayout()
        '
        'lvFFClientList
        '
        Me.lvFFClientList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvFFClientList.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.lvFFClientList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lvFFClientList.HideSelection = False
        Me.lvFFClientList.Location = New System.Drawing.Point(1, 39)
        Me.lvFFClientList.Name = "lvFFClientList"
        Me.lvFFClientList.Size = New System.Drawing.Size(581, 282)
        Me.lvFFClientList.SmallImageList = Me.ImageList1
        Me.lvFFClientList.StateImageList = Me.ImageList1
        Me.lvFFClientList.TabIndex = 0
        Me.lvFFClientList.UseCompatibleStateImageBehavior = False
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "FF.jpg")
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.ClientSize = New System.Drawing.Size(582, 321)
        Me.Controls.Add(Me.lvFFClientList)
        Me.Name = "frmMain"
        Me.Text = "FlexFlow Smart Desktop"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lvFFClientList As ListView
    Friend WithEvents ImageList1 As ImageList
End Class
