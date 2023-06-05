Imports System
Imports System.Text
Imports Microsoft.VisualBasic
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading

Namespace TCPIP
    Public Class AsyncTcpClient
        Implements IDisposable

        Private connectDone As ManualResetEvent = New ManualResetEvent(False)
        Private sendDone As ManualResetEvent = New ManualResetEvent(False)
        Private receiveDone As ManualResetEvent = New ManualResetEvent(False)
        Private ReadOnly m_HostServerIP As String = String.Empty
        Private ReadOnly m_HostServerPort As Integer = 8080
        Private ReadOnly m_Command As Byte() = Nothing
        Private m_ReceivedData As Byte() = Nothing

        Public Property ReceivedData As Byte()
            Get
                Return m_ReceivedData
            End Get
            Set(ByVal value As Byte())
                m_ReceivedData = value
            End Set
        End Property

        Public Class StateObject
            Public workSocket As Socket = Nothing
            Public Const BufferSize As Integer = 1024 * 2
            Public buffer As Byte() = New Byte(2047) {}
            Public sb As StringBuilder = New StringBuilder()
        End Class

        Public Sub New(ByVal hostIP As String, ByVal hostPort As Integer, ByVal command As Byte())
            Try
                m_HostServerIP = hostIP
                m_HostServerPort = hostPort
                m_Command = command
            Catch ex As Exception
                WriteLogAsy(ex)
            End Try
        End Sub

        Private m_CompletedToConnection As Boolean = False
        Private m_CompletedToReceive As Boolean = False
        Private disposedValue1 As Boolean

        Public Function FireMeterClient() As Boolean
            Dim flag As Boolean = False
            Dim ipaddress As System.Net.IPAddress = Nothing

            Try
                ipaddress = System.Net.IPAddress.Parse(m_HostServerIP.Trim())
            Catch ex As SocketException
                WriteLogAsy(ex)
                Return False
            Catch ex As Exception
                WriteLogAsy(ex)
                Return False
            End Try

            Dim remoteEP As IPEndPoint = New IPEndPoint(ipaddress, m_HostServerPort)

            Using client As Socket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)

                Try
                    client.BeginConnect(remoteEP, New AsyncCallback(AddressOf ConnectCallback), client)
                    connectDone.WaitOne(5 * 1000)

                    If client.Connected Then
                        ProcessToSend(client, Me.m_Command)
                        sendDone.WaitOne(200)
                        ProcessToReceive(client)
                        receiveDone.WaitOne(200)
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
                        End If

                    Catch ex As Exception
                        WriteLogAsy(ex)
                    End Try
                End Try
            End Using

            Return flag
        End Function

        Private Sub ConnectCallback(ByVal ar As IAsyncResult)
            Dim client As Socket = CType(ar.AsyncState, Socket)

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

        Private Sub ProcessToSend(ByVal client As Socket, ByVal cmd As Byte())
            Try

                If client.Connected Then
                    client.BeginSend(cmd, 0, cmd.Length, 0, New AsyncCallback(AddressOf SendCallback), client)
                End If

            Catch ex As SocketException
                WriteLogAsy(ex)
            Catch ex As Exception
                WriteLogAsy(ex)
            End Try
        End Sub

        Private Sub SendCallback(ByVal ar As IAsyncResult)
            Dim client As Socket = CType(ar.AsyncState, Socket)
            Dim bytesSent As Integer = 0

            Try
                bytesSent = client.EndSend(ar)
            Catch ex As Exception
                WriteLogAsy(ex)
            Finally
                sendDone.[Set]()
            End Try
        End Sub

        Private Sub ProcessToReceive(ByVal client As Socket)
            Try

                If client.Connected Then
                    Dim state As StateObject = New StateObject()
                    state.workSocket = client
                    client.ReceiveTimeout = 10 * 1000
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, New AsyncCallback(AddressOf ReceiveCallback), state)
                End If

            Catch ex As Exception
                WriteLogAsy(ex)
            End Try
        End Sub

        Private Sub ReceiveCallback(ByVal ar As IAsyncResult)
            Dim state As StateObject = CType(ar.AsyncState, StateObject)
            Dim client As Socket = state.workSocket

            Try
                Dim bytesRead As Integer = 0

                Try
                    bytesRead = client.EndReceive(ar)
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
                    WriteLogAsy("Received bytesRead:" & bytesRead.ToString())
                    Dim KWh As String = state.buffer(3).ToString() & state.buffer(4).ToString() & state.buffer(5).ToString() & state.buffer(6).ToString()
                    WriteLogAsy("Received KWh(0x):" & KWh)
                    Dim int_KWh As Integer = Convert.ToInt32(KWh, 16)
                    WriteLogAsy("Received KWh(int):" & int_KWh.ToString())
                    Dim recData As String = ""

                    For i As Integer = 0 To bytesRead - 1
                        Dim rcr As String = Conversion.Hex(state.buffer(i))
                        If rcr.Length = 1 Then rcr = rcr.PadLeft(2, "0"c)
                        recData = recData & rcr & " "
                    Next

                    WriteLogAsy("Received Data(Hex):" & recData)

                    If True Then

                        Try
                            m_CompletedToReceive = True
                            m_ReceivedData = state.buffer
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

            Catch ex As Exception
                WriteLogAsy(ex)
            Finally

                If client IsNot Nothing And m_CompletedToReceive Then

                    Try
                        client.Shutdown(SocketShutdown.Both)
                        client.Close()
                    Catch ex As Exception
                        WriteLogAsy(ex)
                    End Try
                End If
            End Try
        End Sub

        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue1 Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects)
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override finalizer
                ' TODO: set large fields to null
                disposedValue1 = True
            End If
        End Sub

        ' ' TODO: override finalizer only if 'Dispose(disposing As Boolean)' has code to free unmanaged resources
        ' Protected Overrides Sub Finalize()
        '     ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        '     Dispose(disposing:=False)
        '     MyBase.Finalize()
        ' End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
            Dispose(disposing:=True)
            GC.SuppressFinalize(Me)
        End Sub
    End Class
End Namespace
