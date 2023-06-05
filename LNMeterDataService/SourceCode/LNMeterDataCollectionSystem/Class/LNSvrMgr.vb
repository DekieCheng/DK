Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Threading
Imports System.Threading.Tasks
Imports TCPIP
Imports Model
Imports LNMeterInfrastructure.BLL
Imports LNMeterInfrastructure.Common
Imports System.Globalization
Imports System.Reflection

Namespace ServiceMgr
    Public Class LNSvrMgr
        Private Shared Function ToGenCommand(ByVal meterAddr As String, ByVal orgCommand As String) As String
            Dim cmdByte As String() = New String(5) {}
            Dim splittedCommand As String() = orgCommand.Split(New Char() {" "c})
            cmdByte(0) = (Int32.Parse(meterAddr, NumberStyles.HexNumber)).ToString().PadLeft(2, "0") '(meterAddr).PadLeft(2, "0"c)
            cmdByte(1) = splittedCommand(0)
            cmdByte(2) = splittedCommand(1)
            cmdByte(3) = splittedCommand(2)
            cmdByte(4) = splittedCommand(3)
            cmdByte(5) = splittedCommand(4)
            Dim hexCmd As String = LNGlobal.ToConvertHexString(cmdByte)
            Dim finalCRC As Byte() = LNCRC.CRC16(hexCmd)
            Dim destByte As String() = New String(7) {}
            ToMoveToDestArray(cmdByte, destByte)
            destByte(6) = Conversion.Hex(finalCRC(0))
            destByte(7) = Conversion.Hex(finalCRC(1))
            Return LNGlobal.ToConvertHexString(destByte)
        End Function

        Private Shared Function ToMoveToDestArray(ByVal sourceByte As String(), ByRef destByte As String()) As String()
            For i As Integer = 0 To sourceByte.Length - 1
                destByte(i) = sourceByte(i)
            Next

            Return destByte
        End Function

        Private Shared Function ProcessToParserReceivedData(ByVal lNMeterCategory As LNMeterCategory, ByVal orgReceivedDecData As String) As Hashtable
            Try
                Dim MyAssy As Assembly
                MyAssy = Assembly.LoadFrom(Environment.CurrentDirectory.TrimEnd(New Char() {"\"c}) & "\\" & lNMeterCategory.AssemblyName & ".dll")
                Dim type As Type = MyAssy.[GetType](lNMeterCategory.AssemblyName & "." & lNMeterCategory.ClassName)
                Dim obj As Object = Activator.CreateInstance(type)
                Dim methodInfo As MethodInfo = type.GetMethod("ParserReceivedData")
                Dim ht As Hashtable = methodInfo.Invoke(obj, New Object() {orgReceivedDecData})
                Return ht
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Private Shared Sub ProcessToSaveData(ByVal lNRuntimeArguments As LNRuntimeArguments)
            Try
                Using _LNTranscationMgr As LNTranscationMgr = New LNTranscationMgr()
                    _LNTranscationMgr.ImportDataToDB(lNRuntimeArguments)
                End Using
            Catch ex As Exception
                LNLogMgr.WriteLogAsy(ex)
            End Try
        End Sub

        Private Shared Sub ProcessToSaveRuntimeErrLog(ByVal _LNRuntimeArguments As LNRuntimeArguments)
            Try
                If _LNRuntimeArguments.RuntimeException Is Nothing OrElse String.IsNullOrEmpty(_LNRuntimeArguments.RuntimeErrMsg) Then Return

                Using _LNTranscationMgr As LNTranscationMgr = New LNTranscationMgr()
                    _LNTranscationMgr.SaveRuntimeErrLog(_LNRuntimeArguments)
                End Using

            Catch ex As Exception
                LNLogMgr.WriteLogAsy(ex)
            End Try
        End Sub

        Public Shared Sub FireCollectDataAction(ByVal currMeterCategory As LNMeterCategory)
            Dim lNRuntimeArguments As LNRuntimeArguments = New LNRuntimeArguments()

            Try
                lNRuntimeArguments.lnMeterCategory = currMeterCategory
                Dim _lNMeterCollection As LNMeterCollection = currMeterCategory.LNSerialPortServerCollection

                For Each lNSerialPortServer As LNSerialPortServer In _lNMeterCollection

                    Try
                        Dim _LNElectricityMeterCollection As LNMeterCollection = lNSerialPortServer.LNElectricityMeterCollection
                        Dim _LNWaterMeterCollection As LNMeterCollection = lNSerialPortServer.LNWaterMeterCollection

                        If _LNElectricityMeterCollection.Count > 0 Then
                            ParallelExecutionforElectricityMeter(_LNElectricityMeterCollection, currMeterCategory)

                        ElseIf _LNWaterMeterCollection.Count > 0 Then
                            ParallelExecutionforWaterMeter(_LNWaterMeterCollection, currMeterCategory)

                        End If

                    Catch ex As Exception
                        LNLogMgr.WriteLogAsy(ex)
                    End Try
                Next

            Catch ex As Exception
                lNRuntimeArguments.RuntimeException = ex
                LNLogMgr.WriteLogAsy(ex)
            Finally
                ProcessToSaveRuntimeErrLog(lNRuntimeArguments)
            End Try
        End Sub

        Private Shared Sub ParallelExecutionforElectricityMeter(ByVal lNMeterCollection As LNMeterCollection, ByVal currMeterCategory As LNMeterCategory)
            Dim lNRuntimeArguments As LNRuntimeArguments = New LNRuntimeArguments()
            lNRuntimeArguments.lnMeterCategory = currMeterCategory

            Try
                Dim stopwatch As Stopwatch = Stopwatch.StartNew()
                'Parallel.ForEach(CType(lNMeterCollection, IEnumerable(Of LNElectricityMeter)), Sub(currentMeter)
                '                                                                                   ToReadDatafromHost(currMeterCategory, currentMeter)
                '                                                                                   Thread.Sleep(10)
                '                                                                               End Sub)
                '  Dim electrictiyList As List(Of LNElectricityMeter) = lNMeterCollection.GetEnumerator()
                For Each _LNElectricityMeter As LNElectricityMeter In lNMeterCollection
                    ToReadDatafromHost(currMeterCategory, _LNElectricityMeter)
                    Thread.Sleep(500)
                Next
                'ForEach(a As LNElectricityMeter  In lNMeterCollection)


                'Parallel.ForEach(Of LNElectricityMeter)(lNMeterCollection, Sub(currentMeter)
                '                                                               ToReadDatafromHost(currMeterCategory, currentMeter)
                '                                                               Thread.Sleep(10)
                '                                                           End Sub)

                LNLogMgr.WriteLogAsy("ParallelExecution time for ParallelExecutionforElectricityMeter: " & stopwatch.ElapsedMilliseconds)
                stopwatch.[Stop]()
            Catch ex As Exception
                lNRuntimeArguments.RuntimeException = ex
                LNLogMgr.WriteLogAsy(ex)
            Finally
                ProcessToSaveRuntimeErrLog(lNRuntimeArguments)
            End Try
        End Sub

        Private Shared Sub ParallelExecutionforWaterMeter(ByVal lNMeterCollection As LNMeterCollection, ByVal currMeterCategory As LNMeterCategory)
            Dim stopwatch As Stopwatch = Stopwatch.StartNew()
            Parallel.ForEach(CType(lNMeterCollection, IEnumerable(Of LNWaterMeter)), Sub(currentMeter)
                                                                                         ToReadDatafromHost(currMeterCategory, currentMeter)
                                                                                         Thread.Sleep(10)
                                                                                     End Sub)
            LNLogMgr.WriteLogAsy("ParallelExecution for ParallelExecutionforWaterMeter: " & stopwatch.ElapsedMilliseconds)
            stopwatch.[Stop]()
        End Sub

        Private Shared Sub ToReadDatafromHost(ByVal lNMeterCategory As LNMeterCategory, ByVal lNElectricityMeter As LNElectricityMeter)
            Dim lNRuntimeArguments As LNRuntimeArguments = New LNRuntimeArguments()
            Dim lNSerialPortServer As LNSerialPortServer = lNElectricityMeter.LNSerialPortServer
            lNRuntimeArguments.lnMeterCategory = lNMeterCategory
            lNRuntimeArguments.lnElectricityMeter = lNElectricityMeter
            lNRuntimeArguments.lnSerialPortServer = lNSerialPortServer
            Try
                Dim outputCmd As String = ToGenCommand(lNElectricityMeter.MeterAddr, lNMeterCategory.Command)
                Using _AsyncTcpClient = New AsyncTcpClient(lNSerialPortServer.HostIP, lNSerialPortServer.HostPort, outputCmd.Replace(" ", ""))
                    If Not _AsyncTcpClient.FireMeterClient() Then
                        lNRuntimeArguments.RuntimeErrMsg = String.Format("Failed to read data from remote host [{0}:{1}] by the command [{2}]",
                                                                         lNSerialPortServer.HostIP, lNSerialPortServer.HostPort, outputCmd)
                        Return
                    End If

                    If Not (_AsyncTcpClient.ReceivedData IsNot Nothing AndAlso _AsyncTcpClient.ReceivedData.Length > 0) Then
                        lNRuntimeArguments.RuntimeErrMsg = String.Format("Read empty data from remote host [{0}:{1}] by the command [{2}]",
                                                                             lNSerialPortServer.HostIP, lNSerialPortServer.HostPort, outputCmd)
                        Return
                    End If

                    Dim ReceivedDataHex As String = LNGlobal.ToConvertHexString(_AsyncTcpClient.ReceivedData)
                    LNLogMgr.WriteLogAsy(String.Format("Arrived data(Hex) [{0}] from remote host [{1}:{2}] by the command [{3}].",
                                                           ReceivedDataHex, lNSerialPortServer.HostIP, lNSerialPortServer.HostPort, outputCmd))

                    lNRuntimeArguments.ReceivedData = ProcessToParserReceivedData(lNMeterCategory, ReceivedDataHex)
                    ProcessToSaveData(lNRuntimeArguments)
                End Using

            Catch ex As Exception
                lNRuntimeArguments.RuntimeException = ex
                LNLogMgr.WriteLogAsy(ex)
            Finally
                ProcessToSaveRuntimeErrLog(lNRuntimeArguments)
            End Try
        End Sub

        Private Shared Sub ToReadDatafromHost(ByVal lNMeterCategory As LNMeterCategory, ByVal lNWaterMeter As LNWaterMeter)
            Try
                Dim lNSerialPortServer As LNSerialPortServer = lNWaterMeter.LNSerialPortServer

                Using _AsyncTcpClient = New AsyncTcpClient(lNSerialPortServer.HostIP, lNSerialPortServer.HostPort, ToGenCommand(lNWaterMeter.MeterAddr, lNMeterCategory.Command))
                End Using

            Catch ex As Exception
            End Try
        End Sub
    End Class
End Namespace
