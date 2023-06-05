Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading.Tasks
Imports LNMeterInfrastructure.Common
Imports TCPIP

Public Class frmSPSSimulator
    Private m_ServerPort As Integer = 20005
    Private m_SmartAsyncFFTcpClient As AsyncTcpClient = Nothing

    Private Sub cmdOK_Click(sender As Object, e As EventArgs) Handles cmdOK.Click
        Try
            Dim cmdByte As String = ToGetCommand(txtCommand.Text)
            txtCommand.Text = cmdByte
            If Not String.IsNullOrEmpty(cmdByte) Then
                rhLog.AppendText(cmdByte & vbCrLf & vbCrLf)
            End If

            Dim destHost() As String = New String(1) {}
            For i As Integer = 0 To destHost.Length - 1
                destHost(i) = Me.txtHostIP.Text
                ReadFromDestHost(Me.txtHostIP.Text, cmdByte)
            Next

            'ParallelExecution(destHost, cmdByte)

        Catch ex As Exception
            rhLog.AppendText(ex.Message & vbCrLf & ex.StackTrace & vbCrLf & vbCrLf)
        End Try
    End Sub

    Private Sub frmSPSSimulator_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

        Catch ex As Exception
            rhLog.AppendText(ex.Message & vbCrLf & ex.StackTrace & vbCrLf & vbCrLf)
        End Try
    End Sub

    Private Sub ParallelExecution(ByVal destHost() As String, ByVal cmd As String)
        Try
            Dim _Stopwatch As Stopwatch = Stopwatch.StartNew()
            Parallel.ForEach(destHost, Sub(_hostip)
                                           ReadFromDestHost(_hostip, cmd)
                                       End Sub)
            _Stopwatch.Stop()

        Catch ex As Exception
            rhLog.AppendText(ex.Message & vbCrLf & ex.StackTrace & vbCrLf & vbCrLf)
        End Try
    End Sub

    Private Function ReadFromDestHost(_destHost As String, ByVal outputCmd As String) As Action(Of String)
        Try
            outputCmd = outputCmd.Replace(" ", "")

            Using m_SmartAsyncFFTcpClient As New AsyncTcpClient(_destHost, 5300, outputCmd)
                'If (Not m_SmartAsyncFFTcpClient.SendDataToRemoteHost(_destHost, 5300, outputCmd)) Then  
                If (Not m_SmartAsyncFFTcpClient.FireMeterClient()) Then
                    rhLog.AppendText(String.Format("Failed to read data from HostIP [{0}:{1}] by the command [{2}]",
                            _destHost, 5300, outputCmd) & vbCrLf)
                Else
                    Dim ReceivedDataHex As String = ""

                    If (m_SmartAsyncFFTcpClient.ReceivedData IsNot Nothing) Then
                        If m_SmartAsyncFFTcpClient.ReceivedData.Length > 0 Then
                            ReceivedDataHex = LNGlobal.ToConvertHexString(m_SmartAsyncFFTcpClient.ReceivedData)
                            rhLog.AppendText(String.Format("Received data(Hex) is [{0}] from Host[{1}] ", ReceivedDataHex, _destHost))
                        Else
                            rhLog.AppendText(String.Format("Read empty data from HostIP [{0}:{1}] by the command [{2}]",
                                                       _destHost, 5300, outputCmd))
                        End If
                    Else
                        rhLog.AppendText(String.Format("Read empty data from HostIP [{0}:{1}] by the command [{2}]",
                                                       _destHost, 5300, outputCmd))
                    End If
                End If
                rhLog.AppendText(vbCrLf)
            End Using
            Threading.Thread.Sleep(50)
        Catch ex As Exception
        End Try
    End Function

    'Private Sub ReadFromDestHost(ByVal _destHost As String)
    '    Try
    '        rhLog.AppendText("Connect to " & _destHost & vbCrLf & vbCrLf)
    '        _destHost = Nothing
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Private Function ToGetCommand(ByVal orgData As String) As String

        Dim cmd() As String = orgData.Split(" ")

        Dim cmdByte() As String = New String(5) {}

        cmdByte(0) = (Int32.Parse(cmd(0), NumberStyles.HexNumber)).ToString().PadLeft(2, "0")
        cmdByte(1) = (cmd(1))
        cmdByte(2) = (cmd(2))
        cmdByte(3) = (cmd(3))
        cmdByte(4) = (cmd(4))
        cmdByte(5) = (cmd(5))

        'cmdByte(0) = CByte(&H1)
        'cmdByte(1) = CByte(&H3)
        'cmdByte(2) = CByte(&H20)
        'cmdByte(3) = CByte(&H1)
        'cmdByte(4) = CByte(&H0)
        'cmdByte(5) = CByte(&H2)

        Dim retCrc As String = ToConvertHexString(cmdByte)
        Dim outputWithCRC As Byte() = LNCRC.CRC16("01 03 20 01 00 02")
        Dim outputWithCRC1 As Byte() = LNCRC.CRC16(retCrc)

        Dim retByte() As String = New String(7) {}
        ToMoveToDestArray(cmdByte, retByte)
        retByte(6) = Hex(outputWithCRC1(0)).PadLeft(2, "0")
        retByte(7) = Hex(outputWithCRC1(1)).PadLeft(2, "0")
        Return ToConvertHexString(retByte)
    End Function

    Private Function ToConvertHexString(ByVal cmdByte As Byte()) As String
        Dim destStr As String = ""
        For Each bt As Byte In cmdByte
            Dim rcr As String = CStr(bt) ' Hex(bt)
            If rcr.Length = 1 Then rcr = rcr.PadLeft(2, "0")
            destStr = destStr & rcr & Space(1)
        Next
        Return destStr.TrimEnd(" ")
    End Function
    Private Function ToConvertHexString(ByVal cmdByte As String()) As String
        Dim destStr As String = ""
        For Each bt As String In cmdByte
            Dim rcr As String = CStr(bt) ' Hex(bt)
            If rcr.Length = 1 Then rcr = rcr.PadLeft(2, "0")
            destStr = destStr & rcr & Space(1)
        Next
        Return destStr.TrimEnd(" ")
    End Function
    Private Function ToMoveToDestArray(ByVal sourceByte As String(), ByRef destByte As String()) As String()
        For i As Integer = 0 To sourceByte.Length - 1
            destByte(i) = sourceByte(i)
        Next
        Return destByte
    End Function

    Private Delegate Sub AppendLogMsgDel(ByVal logMsg As String, ByVal MsgOption As MsgCategoryFlag)
    Private Sub AppendLogMsg(ByVal logMsg As String, ByVal MsgOption As MsgCategoryFlag)
        Try
            Static lineNumber As Integer = 0
            If Me.InvokeRequired Then
                Me.rhLog.BeginInvoke(New AppendLogMsgDel(AddressOf AppendLogMsg), logMsg, MsgOption)
                Return
            End If

            logMsg = String.Format("{0} >>> {1}", Now().ToString("yyyy-MM-dd HH:mm:ss.fff"), logMsg.Replace(vbCrLf, "<CR><LF>"))
            lineNumber += 1
            rhLog.SelectionStart = 0
            rhLog.SelectedText = lineNumber.ToString & "." & logMsg + vbCrLf + rhLog.SelectedText
            rhLog.Select(0, rhLog.Lines(0).Length)

            Select Case MsgOption
                Case MsgCategoryFlag.MsgError
                    rhLog.SelectionColor = Color.Red
                Case MsgCategoryFlag.MsgNormal
                    rhLog.SelectionColor = Color.Black
                Case MsgCategoryFlag.MsgReminding
                    rhLog.SelectionColor = Color.Black
                Case MsgCategoryFlag.MsgOutput
                    rhLog.SelectionColor = Color.ForestGreen
                Case MsgCategoryFlag.MsgIncoming
                    rhLog.SelectionColor = Color.Blue
                Case Else
                    rhLog.SelectionColor = Color.Black
            End Select
        Catch ex As Exception

        End Try
    End Sub

End Class
