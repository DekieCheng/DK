Imports System.IO
Imports System.Drawing.Printing
Imports System.Runtime.InteropServices

Public Class frmPrintLabelWinspoolDrv

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.OpenFileDialog1.FileName = ""
        Me.txtZPLFile.Text = ""
        Me.txtZPLFileContent.Text = ""
        Me.txtPrinterName.Text = "Microsoft XPS Document Writer"
    End Sub

    Public Sub PrintRaw(ByVal fileName As String, ByVal PrinterName As String)
        Dim content As String = LoadFileContent(fileName)
        Me.txtZPLFileContent.Text = content
        RawPrinterHelper.SendStringToPrinter(PrinterName, content)
    End Sub

    Private Sub txtPrint_Click(sender As Object, e As EventArgs) Handles txtPrint.Click
        PrintRaw(Me.txtZPLFile.Text, Me.txtPrinterName.Text)
    End Sub

    Private Sub ProcessToOpenZplFile()
        Me.OpenFileDialog1.FileName = ""
        Me.OpenFileDialog1.Filter = "All File|*.*|Zpl File|*.zpl|Txt File|*.txt"

        If Me.OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim fileName As String = Me.OpenFileDialog1.FileName
            Me.txtZPLFile.Text = Me.OpenFileDialog1.FileName
            Me.txtZPLFileContent.Text = LoadFileContent(Me.txtZPLFile.Text)
        End If
    End Sub

    Private Function LoadFileContent(ByVal fileName As String) As String
        Dim objFileToPrint As System.IO.StreamReader
        objFileToPrint = New System.IO.StreamReader(fileName)
        Dim fileContent As String = ""
        Try
            fileContent = objFileToPrint.ReadToEnd()
        Catch ex As Exception

        Finally
            objFileToPrint.Close()
        End Try
        Return fileContent
    End Function

    Private Sub cmdOpenZplFile_Click(sender As Object, e As EventArgs) Handles cmdOpenZplFile.Click
        ProcessToOpenZplFile()
    End Sub


End Class
