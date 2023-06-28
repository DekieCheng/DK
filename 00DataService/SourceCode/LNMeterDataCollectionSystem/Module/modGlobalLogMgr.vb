Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System.IO.File
Imports System.Security.AccessControl

Module modGlobalLogMgr

    Public Sub WriteLog(ByVal strData As String)
        On Error Resume Next
        Dim DestinateFolder As String = AppDomain.CurrentDomain.SetupInformation.ApplicationBase
        Dim strFileName As String
        Dim fsw As System.IO.StreamWriter

        strFileName = DestinateFolder & String.Format("{0:yyyyMMdd}", Now()) & ".log"
        fsw = New System.IO.StreamWriter(File.Open(strFileName, FileMode.Append))
        fsw.WriteLine(Now() & ": " & strData)
        fsw.Flush()
        fsw.Close()
    End Sub

    Public Sub WriteLogAsy(ByVal strData As String)
        Try
            If strData = String.Empty Then
                strData = "Got empty data."
            End If
            Dim DestinateFolder As String = AppDomain.CurrentDomain.SetupInformation.ApplicationBase
            Dim currFolder As String = (DestinateFolder & "LogFiles\" & String.Format("{0:yyyyMMdd}", Now()))
            If Not Directory.Exists(currFolder) Then
                Directory.CreateDirectory(currFolder)
            End If
            Dim strFileName As String = currFolder & "\" & String.Format("{0:yyyyMMddHH}", Now()) & ".log"

            Using fs As New FileStream(strFileName, FileMode.OpenOrCreate, FileSystemRights.AppendData, FileShare.ReadWrite, 4096, FileOptions.Asynchronous)
                Using writer As New StreamWriter(fs)
                    writer.AutoFlush = True
                    writer.WriteLine(strData & vbCrLf)
                End Using
            End Using
        Catch ex As Exception
            'System.Diagnostics.EventLog.WriteEntry(My.Application.Info.AssemblyName, "Message:" & ex.Message & vbCrLf & "StackTrace:" & ex.StackTrace, EventLogEntryType.Error)
        End Try
    End Sub

    Public Sub WriteLogAsy(ByVal oEX As Exception)
        Try
            Dim DestinateFolder As String = AppDomain.CurrentDomain.SetupInformation.ApplicationBase
            Dim currFolder As String = (DestinateFolder & "LogFiles\" & String.Format("{0:yyyyMMdd}", Now()))
            If Not Directory.Exists(currFolder) Then
                Directory.CreateDirectory(currFolder)
            End If
            Dim strFileName As String = currFolder & "\" & String.Format("{0:yyyyMMdd}", Now()) & ".log"

            Dim strData As String = Now.ToString & " >>> " _
               & "App Server: " & g_AppServerName & vbCrLf _
               & "Exception Source: " & oEX.TargetSite.ToString & vbCrLf _
               & "Message: " & oEX.Message & vbCrLf _
               & "StackTrace: " & oEX.StackTrace
            If oEX.InnerException IsNot Nothing Then
                strData = strData & vbCrLf & "Inner Exception:" & oEX.InnerException.Message
            End If

            Using fs As New FileStream(strFileName, FileMode.OpenOrCreate, FileSystemRights.AppendData, FileShare.ReadWrite, 4096, FileOptions.Asynchronous)
                Using writer As New StreamWriter(fs)
                    writer.AutoFlush = True
                    writer.WriteLine(strData & vbCrLf)
                End Using
            End Using
        Catch ex As Exception
            'System.Diagnostics.EventLog.WriteEntry(My.Application.Info.AssemblyName, "Message:" & ex.Message & vbCrLf & "StackTrace:" & ex.StackTrace, EventLogEntryType.Error)
        End Try
    End Sub

End Module
