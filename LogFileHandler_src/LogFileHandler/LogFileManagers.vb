Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Security.AccessControl
Imports System.Text
Imports System.Threading
Imports System.Threading.Tasks

Public Class LogFileManagers
    Public Event OnFileAmount(ByVal fileAmount As Integer)
    Public Event OnFileAmountChanged(ByVal fileAmount As Integer)
    Public Event OnStatusReport(ByVal msg As String)
    Public Event OnFileCompleted(ByVal FullfileName As String)
    Public Event OnInternalError(ByVal ex As Exception)

    Private Shared GoodFilePath As String = Directory.GetCurrentDirectory() & "\GoodFiles\"
    Private Shared ErrorFilePath As String = Directory.GetCurrentDirectory() & "\ExceptionFiles\"
    Private Shared CSVPath As String = Directory.GetCurrentDirectory() & "\CSVFiles\"
    Private Shared logFilePath = Directory.GetCurrentDirectory() & "\LogFiles"
    Public Sub StartParserLogFiles()
        Try
            If Not Directory.Exists(GoodFilePath) Then
                Directory.CreateDirectory(GoodFilePath)
            End If
            If Not Directory.Exists(ErrorFilePath) Then
                Directory.CreateDirectory(ErrorFilePath)
            End If
            If Not Directory.Exists(CSVPath) Then
                Directory.CreateDirectory(CSVPath)
            End If
            If Not Directory.Exists(logFilePath) Then
                Directory.CreateDirectory(logFilePath)
            End If

            RaiseEvent OnStatusReport("Prepare Dummy log files")
            GenerateLogFiles()
            Dim files = Directory.GetFiles(logFilePath, "*.log")
            RaiseEvent OnFileAmount(files.Length)
            RaiseEvent OnStatusReport("Total [" & files.Length.ToString() & "] files are ready")

            ParallelExecution(files)
        Catch ex As Exception
            RaiseEvent OnInternalError(ex)
        End Try
    End Sub

    Private Sub ParallelExecution(ByVal files As String())
        Try
            Task.Run(Sub()
                         Dim stopwatch As Stopwatch = Stopwatch.StartNew()
                         Parallel.ForEach(files, New ParallelOptions With {
                             .MaxDegreeOfParallelism = Environment.ProcessorCount
                         }, Sub(currentFile)
                                Dim ExtractedResult = ExtractFileContent(ReadFileContent(currentFile))
                                StoreResultToCSVFile(ExtractedResult)
                                MoveFileToErrorFolder(currentFile)
                                RaiseEvent OnFileCompleted(String.Format("The file [{0}] is done", currentFile))
                                RaiseEvent OnFileAmountChanged(1)
                            End Sub)
                         RaiseEvent OnFileCompleted("Parallel Execution Time: " & stopwatch.ElapsedMilliseconds)
                         stopwatch.[Stop]()
                     End Sub)
        Catch e As Exception
            WriteDataToLocalFiles(e.Message)
            RaiseEvent OnInternalError(e)
        End Try
    End Sub

    Private Function ReadFileContent(ByVal sourceFile As String) As String
        Dim fileContent As String = String.Empty
        Try
            Using sr As StreamReader = New StreamReader(sourceFile, Encoding.Unicode)
                fileContent = sr.ReadToEnd()
            End Using

        Catch e As Exception
            WriteDataToLocalFiles(e.Message)
            RaiseEvent OnInternalError(e)
        End Try

        Return fileContent
    End Function

    Private Sub MoveFileToErrorFolder(ByVal sourceFile As String)
        Try
            Dim fileName As String = "_Err_" & Path.GetFileName(sourceFile)
            File.Move(sourceFile, ErrorFilePath & fileName)
        Catch e As Exception
            WriteDataToLocalFiles(e.Message)
            RaiseEvent OnInternalError(e)
        End Try
    End Sub

    Private Sub MoveFileToGoodFolder(ByVal sourceFile As String)
        Try
            File.Move(sourceFile, GoodFilePath)
        Catch e As Exception
            WriteDataToLocalFiles(e.Message)
            RaiseEvent OnInternalError(e)
        End Try
    End Sub

    Public Sub WriteDataToLocalFiles(ByVal strData As String)
        Try
            Dim path = Directory.GetCurrentDirectory()
            Dim folder As String = path & "\ApplicationLogs\"

            If Not Directory.Exists(folder) Then
                Directory.CreateDirectory(folder)
            End If

            Dim fileName As String = folder & String.Format("{0:yyyyMMddhh}", DateTime.Now) & ".log"

            Using fs = New FileStream(fileName, FileMode.OpenOrCreate, FileSystemRights.AppendData, FileShare.ReadWrite, 4096, FileOptions.Asynchronous)

                Using wr = New StreamWriter(fs)
                    wr.AutoFlush = True
                    wr.WriteLine(String.Format("{0:yyyy-MM-dd HH:mm:ss.ffff}", DateTime.Now))
                    wr.WriteLine(strData)
                End Using
            End Using

        Catch ex As Exception
            RaiseEvent OnInternalError(ex)
        End Try
    End Sub

    Private Sub StoreResultToCSVFile(ByVal strData As String)
        Try
            Dim fileName As String = CSVPath & String.Format("{0:yyyyMMddhh}", DateTime.Now) & ".csv"

            Using fs = New FileStream(fileName, FileMode.OpenOrCreate, FileSystemRights.AppendData, FileShare.ReadWrite, 4096, FileOptions.Asynchronous)

                Using wr = New StreamWriter(fs)
                    wr.AutoFlush = True
                    wr.WriteLine(strData)
                End Using
            End Using

        Catch ex As Exception
            WriteDataToLocalFiles(ex.Message)
            RaiseEvent OnInternalError(ex)
        End Try
    End Sub

    Public Sub GenerateLogFiles()
        Try
            Dim Qty As Integer = 10
            Parallel.[For](0, Qty, Sub(x)
                                       Dim destFile As String = logFilePath & "\" & String.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now) & "_" & x.ToString() & ".log"
                                       Using fs As FileStream = File.Create(destFile)
                                           AddText(fs, "This is some text")
                                           AddText(fs, "This is some more text,")
                                           AddText(fs, vbCrLf & "and this is on a new line")
                                           AddText(fs, vbCrLf & vbCrLf & "The following is a subset of characters:" & vbCrLf)

                                           For i As Integer = 1 To 120 - 1
                                               AddText(fs, Convert.ToChar(i).ToString())
                                           Next
                                       End Using
                                   End Sub)
        Catch ex As Exception
            WriteDataToLocalFiles(ex.Message)
            RaiseEvent OnInternalError(ex)
        End Try
    End Sub

    Private Shared Sub AddText(ByVal fs As FileStream, ByVal value As String)
        Dim info As Byte() = New UTF8Encoding(True).GetBytes(value)
        fs.Write(info, 0, info.Length)
    End Sub

    Public Shared Function ExtractFileContent(ByVal fileContent As String) As String
        Return "a,b,c,d"
    End Function
End Class

