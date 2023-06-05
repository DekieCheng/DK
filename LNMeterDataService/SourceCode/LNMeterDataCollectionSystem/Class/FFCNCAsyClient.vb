Imports System
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Text

Public Class FFCNCAsyClient

#Region " IDisposable Support "
    Implements IDisposable

    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free unmanaged resources when explicitly called
            End If

            ' TODO: free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

#Region "Variables in class level"

    Private connectDone As New ManualResetEvent(False)
    Private sendDone As New ManualResetEvent(False)
    Private receiveDone As New ManualResetEvent(False)

    Private m_HostServerIP As String = [String].Empty
    Private m_HostServerPort As Integer = 8080
    Private m_CommandType As String = [String].Empty
    Private m_Command As String = [String].Empty
    Private m_ReceivedData As String = [String].Empty

#End Region

#Region "Properties"

    Public Property ReceivedData As String
        Get
            Return m_ReceivedData
        End Get
        Set(ByVal value As String)
            m_ReceivedData = value
        End Set
    End Property

#End Region

#Region "Constructure"

    Public Class StateObject

        Public workSocket As Socket = Nothing
        Public Const BufferSize As Integer = 1024 * 2
        Public buffer As Byte() = New Byte(BufferSize - 1) {}
        Public sb As New StringBuilder()

    End Class

    Sub New(ByVal hostIP As String, ByVal hostPort As Integer, ByVal command As String)
        Try
            m_HostServerIP = hostIP
            m_HostServerPort = hostPort
            m_Command = command
        Catch ex As Exception
            WriteLogAsy(ex)
        End Try
    End Sub

#End Region

#Region "Process Functions"

    Private m_CompletedToConnection As Boolean = False
    Private m_CompletedToReceive As Boolean = False

    Public Function FireFFCNCClient() As Boolean
        Dim flag As Boolean = False
        Dim hostIP As String = Me.m_HostServerIP
        Dim hostPort As Integer = Me.m_HostServerPort
        Dim command As String = Me.m_Command

        Dim ipaddress As System.Net.IPAddress = Nothing
        Dim ipHostInfo As IPHostEntry = Nothing

        Try
            ipaddress = System.Net.IPAddress.Parse(hostIP.Trim)
            ' ipHostInfo = Dns.GetHostEntry(ipaddress)
        Catch ex As SocketException
            WriteLogAsy(ex)
            Return False
        Catch ex As Exception
            WriteLogAsy(ex)
            Return False
        Finally

        End Try

        Dim remoteEP As New IPEndPoint(ipaddress, hostPort)
        Dim client As New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)

        Try
            client.BeginConnect(remoteEP, New AsyncCallback(AddressOf ConnectCallback), client)
            connectDone.WaitOne(5 * 1000)

            If client.Connected Then
                Send(client, command)
                sendDone.WaitOne()

                Receive(client)
                receiveDone.WaitOne()
            End If

            flag = True

        Catch ex As SocketException
            WriteLogAsy(ex)
        Catch ex As Exception
            WriteLogAsy(ex)
        Finally
            Try
                If client IsNot Nothing And (m_CompletedToReceive = True Or m_CompletedToConnection = True) Then
                    client.Close()
                    client = Nothing
                End If
            Catch ex As Exception
                WriteLogAsy(ex)
            End Try
        End Try

        Return flag

    End Function

#Region "Connection Process "

    Private Sub ConnectCallback(ByVal ar As IAsyncResult)
        Dim client As Socket = DirectCast(ar.AsyncState, Socket)
        Try
            client.EndConnect(ar)
            m_CompletedToConnection = True

        Catch ex As ObjectDisposedException
            WriteLogAsy(ex)
        Catch ex As SocketException
            WriteLogAsy(ex)
        Catch ex As Exception
            WriteLogAsy(ex)
        Finally
            connectDone.[Set]()
        End Try
    End Sub

#End Region

#Region "Send Process"

    Private Sub Send(ByVal client As Socket, ByVal data As String)
        Try
            If client.Connected Then
                Dim byteData As Byte() = Encoding.ASCII.GetBytes(data)
                Dim cmd As Byte() = New Byte(7) {}
                cmd(0) = CByte(&H1)
                cmd(1) = CByte(&H3)
                cmd(2) = CByte(&H20)
                cmd(3) = CByte(&H1)
                cmd(4) = CByte(&H0)
                cmd(5) = CByte(&H2)
                cmd(6) = CByte(&H9E)
                cmd(7) = CByte(&HB)

                client.BeginSend(cmd, 0, cmd.Length, 0, New AsyncCallback(AddressOf SendCallback), client)
            End If
        Catch ex As SocketException
            WriteLogAsy(ex)
        Catch ex As Exception
            WriteLogAsy(ex)
        End Try
    End Sub

    Private Sub SendCallback(ByVal ar As IAsyncResult)
        Dim client As Socket = DirectCast(ar.AsyncState, Socket)
        Dim bytesSent As Integer = 0
        Try
            bytesSent = client.EndSend(ar)

        Catch ex As ObjectDisposedException
            WriteLogAsy(ex)
        Catch ex As Exception
            WriteLogAsy(ex)
        Finally
            sendDone.[Set]()
        End Try
    End Sub

#End Region

#Region "Receive Process"

    Private Sub Receive(ByVal client As Socket)
        Try
            If client.Connected Then
                Dim state As New StateObject()
                state.workSocket = client
                client.ReceiveTimeout = 10 * 1000
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, New AsyncCallback(AddressOf ReceiveCallback), state)
            End If
        Catch ex As SocketException
            WriteLogAsy(ex)
        Catch ex As Exception
            WriteLogAsy(ex)
        Finally

        End Try
    End Sub

    Private Sub ReceiveCallback(ByVal ar As IAsyncResult)
        Dim state As StateObject = DirectCast(ar.AsyncState, StateObject)
        Dim client As Socket = state.workSocket

        Try
            Dim bytesRead As Integer = 0

            Try
                bytesRead = client.EndReceive(ar)

            Catch ex As ObjectDisposedException
                receiveDone.[Set]()
                m_CompletedToReceive = True
                WriteLogAsy(ex)
                Return
            Catch ex As SocketException
                receiveDone.[Set]()
                WriteLogAsy(ex)
                m_CompletedToReceive = True
                Return
            Catch ex As Exception
                receiveDone.[Set]()
                WriteLogAsy(ex)
                m_CompletedToReceive = True
                Return
            End Try

            If bytesRead > 0 Then
                Dim data As String = Encoding.UTF8.GetString(state.buffer, 0, bytesRead)
                WriteLogAsy("Received Data:" & data)
                state.sb.Append(data)

                WriteLogAsy("Received bytesRead:" & bytesRead.ToString)
                Dim recData As String = ""
                For i As Integer = 0 To bytesRead - 1
                    Dim rcr As String = Hex(state.buffer(i))
                    If rcr.Length = 1 Then rcr = rcr.PadLeft(2, "0")
                    recData = recData & rcr & Space(1)
                Next
                WriteLogAsy("Received Data(Hex):" & recData)

                'For Each a As Byte In state.buffer
                '    Dim rcr As String = Hex(a)
                '    If rcr.Length = 1 Then rcr = rcr.PadLeft(2, "0")
                '    recData = recData & rcr & Space(1)
                'Next

                If FindEndFlag(data) Then
                    m_CompletedToReceive = True
                    m_ReceivedData = state.sb.ToString()
                    Me.ReceivedData = m_ReceivedData

                    Try
                        '  SaveReceivedDataIntoDB()
                    Catch ex As Exception
                        WriteLogAsy(ex)
                    Finally
                        receiveDone.[Set]()
                    End Try
                Else
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, New AsyncCallback(AddressOf ReceiveCallback), state)
                End If
            Else
                receiveDone.[Set]()
            End If

        Catch ex As SocketException
            WriteLogAsy(ex)
        Catch ex As Exception
            WriteLogAsy(ex)
        Finally
            If client IsNot Nothing And m_CompletedToReceive Then
                Try
                    client.Shutdown(SocketShutdown.Both)
                Catch ex As ObjectDisposedException

                End Try

                Try
                    client.Close()
                Catch ex As ObjectDisposedException
                    WriteLogAsy(ex)
                Catch ex As SocketException
                    WriteLogAsy(ex)
                Catch ex As Exception
                    WriteLogAsy(ex)
                End Try
            End If
        End Try
    End Sub

    Private Function FindEndFlag(ByRef receivedData As String) As Boolean
        Return True

        'If receivedData = "" Then Return True

        'Dim recData() As String = receivedData.Split(vbCrLf)

        ''Here gets the last row from received block data, 
        ''because the endFlag is located the last char of the last row
        'Dim lastRowData As String = recData(UBound(recData))
        'If lastRowData = "" Then Return False

        'Dim predefinedEndFlag() As String = ";".ToCharArray()  ' m_oCNCRuntimeParameter.EndFlag.Split(";")

        'For Each endFlag As String In predefinedEndFlag
        '    For Each lastChr As Char In lastRowData.ToCharArray
        '        If lastChr = endFlag Then Return True
        '    Next
        'Next

        'Return False

    End Function

#End Region

#End Region

#Region "Store the received data into DB"

    Private Sub SaveReceivedDataIntoDB()
        If Me.ReceivedData = "" Then Return
        ' Dim oFFCNCTranscationMgr As FFCNCTranscationMgr = New FFCNCTranscationMgr

        Try
            'oFFCNCTranscationMgr.FFCNCImportData(Me.m_oCNCMachine, Me.ReceivedData, Me.m_oCNCRuntimeParameter)

            'Dim oReceivedData As New FFCNCReceivedData
            'oReceivedData.CurrCNCMachine = Me.m_oCNCMachine
            'oReceivedData.RuntimeParameter = Me.m_oCNCRuntimeParameter
            'oReceivedData.ReceivedData = Me.ReceivedData


        Catch ex As SqlClient.SqlException
            WriteLogAsy(ex)
        Catch ex As SocketException
            WriteLogAsy(ex)
        Catch ex As Exception
            WriteLogAsy(ex)
        Finally
            'oFFCNCTranscationMgr = Nothing
        End Try
    End Sub

#End Region

End Class
