Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports LogFileManagers

Partial Public Class frmLogFileMonitor
    Inherits Form

    Private TotalFileQty As Integer = 0
    Public Event OnStatusReport(Msg As String)
    Public Event OnInternalError(ex As Exception)
    Public Event OnFileAmount(qty As Integer)
    Public Event OnFileAmountChanged(qty As Integer)
    Private WithEvents _LogFileManagers As LogFileManagers = New LogFileManagers()

    Public Delegate Sub ProcessToSetTotalQtyInvoke(ByVal Qty As Integer)

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub ProcessToParserLogFiles()
        Try
            _LogFileManagers.WriteDataToLocalFiles("Start log file process")
            DisplayMessage("Start log file process")
            _LogFileManagers.StartParserLogFiles()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FileCompletedHandler(ByVal sMessage As String) Handles _LogFileManagers.OnFileCompleted
        Try
            DisplayMessage(sMessage)
        Catch
        End Try
    End Sub

    Public Delegate Sub DisplayMessageInvoke(ByVal Msg As String)
    Public Delegate Sub DisplayMessageExceptionInvoke(ByVal ex As Exception)

    Private Sub DisplayMessage(ByVal Msg As String)
        Try
            If Me.InvokeRequired Then
                Me.BeginInvoke(New DisplayMessageInvoke(AddressOf DisplayMessage), Msg)
            Else
                ShowMsg(String.Format("{0:yyyy-MM-dd HH:mm:ss.ffff}", DateTime.Now) & " " & Msg & Environment.NewLine)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub DisplayMessage(ByVal ex As Exception)
        Try
            If Me.InvokeRequired Then
                Me.BeginInvoke(New DisplayMessageExceptionInvoke(AddressOf DisplayMessage), ex)
            Else
                ShowMsg(String.Format("{0:yyyy-MM-dd HH:mm:ss.ffff}", DateTime.Now) & " " & ex.Message & Environment.NewLine & ex.StackTrace & Environment.NewLine)
            End If

        Catch exx As Exception
        End Try
    End Sub

    Private Sub ShowMsg(ByVal Msg As String)
        Try
            Me.richTextBox1.AppendText(Msg)
        Catch exx As Exception
        End Try
    End Sub

    Private Sub ProcessToSetTotalQty(ByVal Qty As Integer) Handles _LogFileManagers.OnFileAmount
        Try
            If Me.InvokeRequired Then
                Me.BeginInvoke(New ProcessToSetTotalQtyInvoke(AddressOf SetTotalQty), Qty)
            Else
                SetTotalQty(Qty)
            End If

        Catch exx As Exception
        End Try
    End Sub

    Private Sub SetTotalQty(ByVal TotalQty As Integer)
        Try
            Me.lblTotalQty.Text = TotalQty.ToString()
            TotalFileQty = TotalQty
        Catch ex As Exception
        End Try
    End Sub

    Public Delegate Sub ProcessToSetRemainingQtyInvoke(ByVal Qty As Integer)

    Private Sub ProcessToSetRemainingQty(ByVal Qty As Integer) Handles _LogFileManagers.OnFileAmountChanged
        Try
            If Me.InvokeRequired Then
                Me.BeginInvoke(New ProcessToSetRemainingQtyInvoke(AddressOf SetRemainingQty), Qty)
            Else
                SetRemainingQty(Qty)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SetRemainingQty(ByVal RemainingQty As Integer)
        Try
            TotalFileQty = TotalFileQty - RemainingQty
            Me.lblRemaining.Text = TotalFileQty.ToString()
        Catch exx As Exception
        End Try
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Try
            Me.Text = Directory.GetCurrentDirectory()
            Me.lblTotalQty.Text = "0"
            Me.lblRemaining.Text = "0"
        Catch ex As Exception
            ShowMsg(ex.Message)
        End Try
    End Sub

    Private Sub btnStart_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnStart.Click
        Try
            ProcessToParserLogFiles()
        Catch ex As Exception
            ShowMsg(ex.Message)
        End Try
    End Sub

End Class
