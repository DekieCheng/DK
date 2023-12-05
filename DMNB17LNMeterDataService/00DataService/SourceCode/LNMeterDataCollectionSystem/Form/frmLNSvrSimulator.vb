Imports Model
Imports LNMeterInfrastructure.BLL
Imports LNMeterInfrastructure.Common
Imports System
Imports System.Timers
Imports LNMeterDataCollectionSystem.ServiceMgr

Public Class frmLNSvrSimulator
    Private _Cycletimer As Timer
    Private _lMeterCategory As LNMeterCategory = Nothing

    Private Sub FireService()
        Try
            AppendLogMsg("Service is starting.", MsgCategoryFlag.MsgNormal)
            _lMeterCategory = New LNMeterCategory(LNGlobal.g_ServiceFilter) '
            'LNSvrMgr.FireCollectDataAction(_lMeterCategory)

            _Cycletimer = New Timer(_lMeterCategory.CycleTime * 1000)
            AddHandler _Cycletimer.Elapsed, New ElapsedEventHandler(AddressOf ProcessToTimerElapsed)
            _Cycletimer.Start()
            AppendLogMsg("Service is started successfully.", MsgCategoryFlag.MsgNormal)
        Catch ex As Exception
            AppendLogMsg(ex.Message & vbCrLf & ex.StackTrace, MsgCategoryFlag.MsgError)
        End Try
    End Sub

    Private Sub ProcessToTimerElapsed(ByVal sender As Object, ByVal e As ElapsedEventArgs)
        Try
            AppendLogMsg(String.Format("Service is running for the filter {0}.", LNGlobal.g_ServiceFilter), MsgCategoryFlag.MsgNormal)
            LNSvrMgr.FireCollectDataAction(_lMeterCategory)
        Catch ex As Exception
            AppendLogMsg(ex.Message & vbCrLf & ex.StackTrace, MsgCategoryFlag.MsgError)
        End Try
    End Sub

    Protected Sub StopService()
        Try
            AppendLogMsg("Stopping the service.", MsgCategoryFlag.MsgNormal)
            _Cycletimer.[Stop]()
            _Cycletimer.Dispose()
            AppendLogMsg("Service is stopped successfully.", MsgCategoryFlag.MsgNormal)
        Catch ex As Exception
            AppendLogMsg(ex.Message & vbCrLf & ex.StackTrace, MsgCategoryFlag.MsgError)
        End Try
    End Sub

    Private Delegate Sub AppendLogMsgDel(ByVal logMsg As String, ByVal MsgOption As MsgCategoryFlag)
    Private Sub AppendLogMsg(ByVal logMsg As String, ByVal MsgOption As MsgCategoryFlag)
        Try
            Static lineNumber As Integer = 0
            If Me.InvokeRequired Then
                Me.rhLog.BeginInvoke(New AppendLogMsgDel(AddressOf AppendLogMsg), logMsg, MsgOption)
                Return
            End If

            logMsg = String.Format("{0} >>> {1}", Now().ToString("yyyy-MM-dd HH:mm:ss.fff"), logMsg)
            lineNumber += 1
            rhLog.SelectionStart = 0
            rhLog.SelectedText = lineNumber.ToString & ". " & logMsg + vbCrLf + rhLog.SelectedText
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

    Private Sub cmdOK_Click(sender As Object, e As EventArgs) Handles cmdOK.Click
        Try
            FireService()

        Catch ex As Exception
            AppendLogMsg(ex.Message & vbCrLf & ex.StackTrace, MsgCategoryFlag.MsgError)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles cmdHostSimulator.Click
        Try
            Dim f As Form = New frmSPSSimulator()
            f.ShowDialog()
        Catch ex As Exception
            AppendLogMsg(ex.Message & vbCrLf & ex.StackTrace, MsgCategoryFlag.MsgError)
        End Try
    End Sub

    Private Sub cmdApply_Click(sender As Object, e As EventArgs) Handles cmdApply.Click
        Try
            Dim lnA As Single = IEEE754Helper.HexToFloat(Me.txtOrgHex.Text)
            AppendLogMsg(String.Format("Hex to Float:Hex[{1}] --> Float[{0}]", lnA.ToString(), Me.txtOrgHex.Text), MsgCategoryFlag.MsgNormal)
        Catch ex As Exception
            AppendLogMsg(ex.Message & vbCrLf & ex.StackTrace, MsgCategoryFlag.MsgError)
        End Try
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim lnCommand As String = LNSvrMgr.ToGenCommand(Me.txtAddr.Text, Me.txtCommand.Text)
            AppendLogMsg(String.Format("Hex Command: [{0}]", lnCommand), MsgCategoryFlag.MsgNormal)
        Catch ex As Exception
            AppendLogMsg(ex.Message & vbCrLf & ex.StackTrace, MsgCategoryFlag.MsgError)
        End Try
    End Sub
End Class
