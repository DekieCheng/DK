Public Class frmPrintLabelByDosCmd

    Private Sub CmdExcute()
        Me.txtOutput.Text += "cmd /k " & Me.txtCMD.Text & vbCrLf

        'Shell("cmd /k " & Me.txtCMD.Text)


        'Return

        'Console.WriteLine(ConsoleOutput.ExcuteCmd("NET TIME \\192.168.0.223"))
        Me.txtOutput.Text += ConsoleOutput.ExcuteCmd(Me.txtCMD.Text) & vbCrLf
        Me.txtOutput.Text += "Print out successfully." & vbCrLf
        '        Dim sw As System.IO.StreamWriter
        '        Dim sr As System.IO.StreamReader
        '        Dim err As System.IO.StreamReader
        '        Dim p As System.Diagnostics.Process = New System.Diagnostics.Process
        '        Dim psI As New System.Diagnostics.ProcessStartInfo(System.Environment.GetEnvironmentVariable("ComSpec"))
        '        psI.UseShellExecute = False
        '        psI.RedirectStandardInput = True
        '        psI.RedirectStandardOutput = True
        '        psI.RedirectStandardError = True
        '        psI.CreateNoWindow = True
        '        p.StartInfo = psI
        '        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        '        'p.Start()

        '#Disable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance
        '        p.Start("cmd.exe /c  " & Me.txtCMD.Text)
        '#Enable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance

        '        sw = p.StandardInput
        '        sr = p.StandardOutput
        '        err = p.StandardError
        '        sw.AutoFlush = True
        '        If Me.txtCMD.Text <> "" Then
        '            sw.WriteLine(Me.txtCMD.Text)
        '        Else
        '            sw.WriteLine("Dir ")
        '        End If
        '        sw.Close()

        '        Me.txtOutput.Text = sr.ReadToEnd()
        '        Me.txtOutput.Text += err.ReadToEnd()
        '        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ProcessToOpenZplFile()
        Me.OpenFileDialog1.FileName = ""
        Me.OpenFileDialog1.Filter = "All File|*.*|Zpl File|*.zpl|Txt File|*.txt"

        If Me.OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim fileName As String = Me.OpenFileDialog1.FileName
            Me.txtCMD.Text = "copy " & Me.OpenFileDialog1.FileName & " com1"

            Me.txtOutput.Text += "Source file: " & fileName & vbCrLf
            Me.txtOutput.Text += "Command: " & Me.txtCMD.Text & vbCrLf

        End If
    End Sub

    Private Sub frmPrintLabelByDosCmd_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.OpenFileDialog1.FileName = ""
        Me.txtCMD.Text = ""
        Me.txtOutput.Text = ""
        Me.txtOutput.Text += "Ready for printing..." & vbCrLf
    End Sub

    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        If Me.txtCMD.Text = String.Empty Then
            Me.cmdOpenFile.PerformClick()
            If Me.txtCMD.Text = String.Empty Then Return
        End If

        CmdExcute()

    End Sub

    Private Sub cmdOpenFile_Click(sender As Object, e As EventArgs) Handles cmdOpenFile.Click
        Try
            ProcessToOpenZplFile()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class

Public Class ConsoleOutput

    Private Sub New()
    End Sub

    Private Shared gWorkingDirectory As String = Environment.GetFolderPath(Environment.SpecialFolder.Personal)

    Public Shared Property WorkingDirectory() As String
        Get
            Return gWorkingDirectory
        End Get
        Set(ByVal Value As String)
            gWorkingDirectory = Value
        End Set
    End Property

    Public Shared Function ExcuteCmd(ByVal command As String) As String
        Dim mResult As String = ""

        Dim tmpProcess As New Process
        With tmpProcess
            With .StartInfo
                .CreateNoWindow = True
                .FileName = .EnvironmentVariables.Item("ComSpec")
                .RedirectStandardOutput = True
                .UseShellExecute = False
                .Arguments = String.Format("/C {0}", command)
                .WorkingDirectory = gWorkingDirectory
            End With
            Try
                .Start()
                .WaitForExit(5000)
                mResult = .StandardOutput.ReadToEnd
            Catch e As System.ComponentModel.Win32Exception
                mResult = e.ToString
            End Try
        End With

        Return mResult
    End Function

End Class