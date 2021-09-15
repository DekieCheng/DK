Imports System
Imports System.Drawing.Drawing2D
Imports System.Windows
Imports System.Windows.Forms

Class DoubleBufferListView

    Inherits ListView

    Friend WithEvents ListView1 As System.Windows.Forms.ListView

    Public Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint _
        Or ControlStyles.UseTextForAccessibility _
        Or ControlStyles.DoubleBuffer _
        Or ControlStyles.ResizeRedraw _
        Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.Selectable, True)
        UpdateStyles()
    End Sub

    Private Sub InitializeComponent()
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.SuspendLayout()
        '
        'ListView1
        '
        Me.ListView1.Location = New System.Drawing.Point(0, 0)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(121, 97)
        Me.ListView1.TabIndex = 0
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ResumeLayout(False)

    End Sub
End Class