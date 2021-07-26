Public Class frmMainEntry
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim f As Form = New frmPrintLabelByDosCmd
        f.ShowDialog()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim f As Form = New frmPrintLabelWinspoolDrv
        f.ShowDialog()
    End Sub
End Class