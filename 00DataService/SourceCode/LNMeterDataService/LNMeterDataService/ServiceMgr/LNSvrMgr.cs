using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using Microsoft.VisualBasic;
using System.Threading;
using TCPIP;
using Model;
using LNMeterInfrastructure.BLL;
using LNMeterInfrastructure.Common;
using System.Collections;

namespace ServiceMgr
{
    public class LNSvrMgr
    {
        private const string NEWPROTOCAL = "NEWPROTOCAL";

        public static string ToGenCommand(string meterAddr, string orgCommand)
        {
            string[] cmdByte = new string[6];
            string[] splittedCommand = orgCommand.Split(new char[] { ' ' });
            cmdByte[0] = Conversion.Hex(meterAddr).PadLeft(2, '0');
            cmdByte[1] = splittedCommand[0];
            cmdByte[2] = splittedCommand[1];
            cmdByte[3] = splittedCommand[2];
            cmdByte[4] = splittedCommand[3];
            cmdByte[5] = splittedCommand[4];
            string hexCmd = LNGlobal.ToConvertHexString(cmdByte);
            byte[] finalCRC = LNCRC.CRC16(hexCmd);
            string[] destByte = new string[8];
            ToMoveToDestArray(cmdByte, ref destByte);
            destByte[6] = Conversion.Hex(finalCRC[0]).PadLeft(2, '0');
            destByte[7] = Conversion.Hex(finalCRC[1]).PadLeft(2, '0');
            return LNGlobal.ToConvertHexString(destByte);
        }
        private static string[] ToMoveToDestArray(string[] sourceByte, ref string[] destByte)
        {
            for (int i = 0; i <= sourceByte.Length - 1; i++)
                destByte[i] = sourceByte[i];

            return destByte;
        }
        private static Hashtable ProcessToParserReceivedData(LNMeterCategory lNMeterCategory, Hashtable orgReceivedDecData)
        {
            try
            {
                Assembly MyAssy;
                string AssyDllPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase.TrimEnd(new char[] { '\\' }) + "\\" + lNMeterCategory.AssemblyName + ".dll";
                MyAssy = Assembly.LoadFrom(AssyDllPath);
                Type type = MyAssy.GetType(lNMeterCategory.AssemblyName + "." + lNMeterCategory.ClassName);
                object obj = Activator.CreateInstance(type);
                MethodInfo methodInfo = type.GetMethod("ParserReceivedData");
                Hashtable ht = (Hashtable)methodInfo.Invoke(obj, new object[] { orgReceivedDecData });
                return ht;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static void ProcessToSaveData(LNRuntimeArguments lNRuntimeArguments)
        {
            try
            {
                using (LNTranscationMgr _LNTranscationMgr = new LNTranscationMgr())
                {
                    _LNTranscationMgr.ImportDataToDB(lNRuntimeArguments);
                }
            }
            catch (Exception ex)
            {
                LNLogMgr.WriteLogAsy(ex);
            }
        }
        private static void ProcessToSaveRuntimeErrLog(LNRuntimeArguments _LNRuntimeArguments)
        {
            try
            {
                if (_LNRuntimeArguments.RuntimeException == null || string.IsNullOrEmpty(_LNRuntimeArguments.RuntimeErrMsg))
                    return;

                using (LNTranscationMgr _LNTranscationMgr = new LNTranscationMgr())
                {
                    _LNTranscationMgr.SaveRuntimeErrLog(_LNRuntimeArguments);
                }
            }
            catch (Exception ex)
            {
                LNLogMgr.WriteLogAsy(ex);
            }
        }

        private static string GenerateOutputCommand(string HostIP, int HostPort, bool IsNewProtocal, string MeterAddr, string OrgCmd)
        {
            string outputCmd = string.Empty;
            try
            {
                string newcmd = ToAutoIdentifyCommand(IsNewProtocal, OrgCmd);
                if (string.IsNullOrEmpty(newcmd))
                    return string.Empty;
                outputCmd = ToGenCommand(MeterAddr, newcmd);
                LNLogMgr.WriteLogAsy(string.Format("Try to read data from host [{0}:{1}] by the command [{2}].", HostIP, HostPort, outputCmd));
                if (string.IsNullOrEmpty(outputCmd))
                    return string.Empty;
            }
            catch (Exception ex)
            {
                LNLogMgr.WriteLogAsy(string.Format("Exception Error Message:{0} for remote host [{1}:{2}] by command [{3}]",
                    ex.Message, HostIP, HostPort, OrgCmd));
                throw ex;
            }
            return outputCmd;
        }
        private static string GetDatafromRemoteHostbyTCPIP(string HostIP, int HostPort, string outputCmd)
        {
            string receivedDataHex = string.Empty;
            try
            {
                int EmptyDataCounter = 1;
                int AddrUnmatchedCounter = 1;
                int LengthUnmatchedCounter = 1;

            _ReadData:
                string RuntimeErrMsg = string.Empty;
                using (LNTcpClient _AsyncTcpClient = new LNTcpClient(HostIP, HostPort, outputCmd))
                {
                    if ((!_AsyncTcpClient.FireMeterClient()))
                    {
                        RuntimeErrMsg = string.Format("Failed to read data from remote host [{0}:{1}] by the command [{2}]", HostIP, HostPort, outputCmd);
                    }
                    else if (!(_AsyncTcpClient.ReceivedData != null && _AsyncTcpClient.ReceivedData.Length > 0))
                    {
                        RuntimeErrMsg = string.Format("Read empty data from remote host [{0}:{1}] by the command [{2}]", HostIP, HostPort, outputCmd);
                    }
                    else if (_AsyncTcpClient.ReceivedData.Length < 30)
                    {
                        string invalidData = LNGlobal.ToConvertHexString(_AsyncTcpClient.ReceivedData);
                        RuntimeErrMsg = string.Format("Read data [{3}] is not enough from remote host [{0}:{1}] by the command [{2}]", HostIP, HostPort, outputCmd, invalidData);
                    }
                    else
                    {
                        receivedDataHex = LNGlobal.ToConvertHexString(_AsyncTcpClient.ReceivedData);
                    }
                }

                if (!string.IsNullOrEmpty(receivedDataHex))
                {
                    if (!IsMatchedWithAddr(outputCmd, receivedDataHex))
                    {
                        if (AddrUnmatchedCounter <= LNGlobal.g_ReReadCounter)
                        {
                            LNLogMgr.WriteLogAsy(string.Format("Try to re-read [AddrUnmatchedCounter={3}] data from remote host [{0}:{1}] by the command [{2}]", HostIP, HostPort, outputCmd, AddrUnmatchedCounter.ToString()));
                            AddrUnmatchedCounter++;
                            goto _ReadData;
                        }
                        else
                        {
                            receivedDataHex = string.Empty;
                            RuntimeErrMsg = string.Format("Exceeded the re-read counter [AddrUnmatchedCounter={3}], still cannot received the correct data, will skip this circle from remote host [{0}:{1}] by the command [{2}]", HostIP, HostPort, outputCmd, AddrUnmatchedCounter.ToString());
                        }
                    }
                    else if (!IsMatchedDataLength(receivedDataHex))
                    {
                        if (LengthUnmatchedCounter <= LNGlobal.g_ReReadCounter)
                        {
                            LNLogMgr.WriteLogAsy(string.Format("Try to re-read [LengthUnmatchedCounter={3}] data from remote host [{0}:{1}] by the command [{2}]", HostIP, HostPort, outputCmd, AddrUnmatchedCounter.ToString()));
                            LengthUnmatchedCounter++;
                            goto _ReadData;
                        }
                        else
                        {
                            receivedDataHex = string.Empty;
                            RuntimeErrMsg = string.Format("Exceeded the re-read counter [LengthUnmatchedCounter={3}], still cannot received the correct data, will skip this circle from remote host [{0}:{1}] by the command [{2}]", HostIP, HostPort, outputCmd, AddrUnmatchedCounter.ToString());
                        }
                    }
                }
                else
                {
                    if (EmptyDataCounter <= LNGlobal.g_ReReadCounter)
                    {
                        LNLogMgr.WriteLogAsy(string.Format("Try to re-read [EmptyDataCounter={1}] data due to [{0}]", RuntimeErrMsg, EmptyDataCounter.ToString()));
                        EmptyDataCounter++;
                        goto _ReadData;
                    }
                    else
                    {
                        receivedDataHex = string.Empty;
                        RuntimeErrMsg = string.Format("Exceeded the re-read counter [EmptyDataCounter={3}], still cannot received the correct data, will skip this circle from remote host [{0}:{1}] by the command [{2}]", HostIP, HostPort, outputCmd, AddrUnmatchedCounter.ToString());
                    }
                }

                if (!string.IsNullOrEmpty(RuntimeErrMsg))
                {
                    receivedDataHex = string.Empty;
                    LNLogMgr.WriteLogAsy(RuntimeErrMsg);
                }
            }
            catch (Exception ex)
            {
                receivedDataHex = string.Empty;
                LNLogMgr.WriteLogAsy(string.Format("Exception Error Message:{0} for remote host [{1}:{2}]" + Environment.NewLine + " Detail Error Info:[{3}]", ex.Message, HostIP, HostPort, ex.StackTrace));
            }
            return receivedDataHex;
        }

        private static bool IsMatchedWithAddr(string outputCmd, string receivedData)
        {
            string addrr = outputCmd.Split(new char[] { ' ' })[0];
            string addrfromData = receivedData.Split(new char[] { ' ' })[0];

            return addrr.ToUpper().Equals(addrfromData.ToUpper());
        }

        private static bool IsMatchedDataLength(string receivedData)
        {
            string[] tmpArr = receivedData.Split(new char[] { ' ' });
            int dataLength = int.Parse(LNGlobal.Hex2Ten(tmpArr[2]));

            if (dataLength == (tmpArr.Length - 5)) return true;
            else return false;
        }

        public static void FireCollectDataAction(LNMeterCategory currMeterCategory)
        {
            LNRuntimeArguments lNRuntimeArguments = new LNRuntimeArguments();

            try
            {
                lNRuntimeArguments.lnMeterCategory = currMeterCategory;
                LNMeterCollection _lNSerialPortServerCollection = currMeterCategory.LNSerialPortServerCollection;

                foreach (LNSerialPortServer lNSerialPortServer in currMeterCategory.LNSerialPortServerCollection)
                {
                    try
                    {
                        if (currMeterCategory.ID == (int)LNGlobal.MeterDevCategory.Water)
                        {
                            ExecutionforWaterMeter(lNSerialPortServer.GetAllWaterMeter(lNSerialPortServer.ID), currMeterCategory);
                        }
                        else if (currMeterCategory.ID == (int)LNGlobal.MeterDevCategory.Electricity)
                        {
                            ExecutionforElectricityMeter(lNSerialPortServer.GetAllElectricityMeter(lNSerialPortServer.ID), currMeterCategory);
                        }
                        else if (currMeterCategory.ID == (int)LNGlobal.MeterDevCategory.ChilledWaterFlow)
                        {
                            ExecutionforChilledWaterFlowMeter(lNSerialPortServer.GetAllChilledWaterFlowMeter(lNSerialPortServer.ID), currMeterCategory);
                        }
                        else if (currMeterCategory.ID == (int)LNGlobal.MeterDevCategory.CompressedAirFlow)
                        {
                            ExecutionforCompressedAirFlow(lNSerialPortServer.GetCompressedAirFlowMeter(lNSerialPortServer.ID), currMeterCategory);
                        }
                    }
                    catch (Exception ex)
                    {
                        LNLogMgr.WriteLogAsy(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                lNRuntimeArguments.RuntimeException = ex;
                LNLogMgr.WriteLogAsy(ex);
            }
            finally
            {
                ProcessToSaveRuntimeErrLog(lNRuntimeArguments);
            }
        }
        private static void ExecutionforElectricityMeter(LNMeterCollection lNMeterCollection, LNMeterCategory currMeterCategory)
        {
            LNRuntimeArguments lNRuntimeArguments = new LNRuntimeArguments();
            lNRuntimeArguments.lnMeterCategory = currMeterCategory;

            try
            {
                LNLogMgr.WriteLogAsy("ExecutionforElectricityMeter started");
                Stopwatch stopwatch = Stopwatch.StartNew();
                foreach (LNElectricityMeter _LNElectricityMeter in lNMeterCollection)
                {
                    ToReadDatafromHostforElectricityMeter(currMeterCategory, _LNElectricityMeter);
                    Thread.Sleep(LNGlobal.g_LoopIntervalTime);
                }
                stopwatch.Stop();
                LNLogMgr.WriteLogAsy("ExecutionforElectricityMeter time : " + stopwatch.ElapsedMilliseconds + Constants.vbCrLf);
            }
            catch (Exception ex)
            {
                lNRuntimeArguments.RuntimeException = ex;
                LNLogMgr.WriteLogAsy(ex);
            }
            finally
            {
                ProcessToSaveRuntimeErrLog(lNRuntimeArguments);
            }
        }
        private static void ExecutionforWaterMeter(LNMeterCollection lNMeterCollection, LNMeterCategory currMeterCategory)
        {
            LNRuntimeArguments lNRuntimeArguments = new LNRuntimeArguments();
            lNRuntimeArguments.lnMeterCategory = currMeterCategory;

            try
            {
                LNLogMgr.WriteLogAsy("ExecutionforWaterMeter started");
                Stopwatch stopwatch = Stopwatch.StartNew();
                foreach (LNWaterMeter _lNWaterMeter in lNMeterCollection)
                {
                    ToReadDatafromHostforWaterMeter(currMeterCategory, _lNWaterMeter);
                    Thread.Sleep(LNGlobal.g_LoopIntervalTime);
                }
                stopwatch.Stop();
                LNLogMgr.WriteLogAsy("ExecutionforWaterMeter time : " + stopwatch.ElapsedMilliseconds + Constants.vbCrLf);
            }
            catch (Exception ex)
            {
                lNRuntimeArguments.RuntimeException = ex;
                LNLogMgr.WriteLogAsy(ex);
            }
            finally
            {
                ProcessToSaveRuntimeErrLog(lNRuntimeArguments);
            }
        }
        private static void ExecutionforChilledWaterFlowMeter(LNMeterCollection lNMeterCollection, LNMeterCategory currMeterCategory)
        {
            LNRuntimeArguments lNRuntimeArguments = new LNRuntimeArguments();
            lNRuntimeArguments.lnMeterCategory = currMeterCategory;

            try
            {
                LNLogMgr.WriteLogAsy("ExecutionforChilledWaterFlowMeter started");
                Stopwatch stopwatch = Stopwatch.StartNew();
                foreach (LNChilledWaterFlowMeter _lNChilledWaterFlowMeter in lNMeterCollection)
                {
                    ToReadDatafromHostforChilledWaterFlowMeter(currMeterCategory, _lNChilledWaterFlowMeter);
                    Thread.Sleep(LNGlobal.g_LoopIntervalTime);
                }
                stopwatch.Stop();
                LNLogMgr.WriteLogAsy("ExecutionforChilledWaterFlowMeter time : " + stopwatch.ElapsedMilliseconds + Constants.vbCrLf);
            }
            catch (Exception ex)
            {
                lNRuntimeArguments.RuntimeException = ex;
                LNLogMgr.WriteLogAsy(ex);
            }
            finally
            {
                ProcessToSaveRuntimeErrLog(lNRuntimeArguments);
            }
        }
        private static void ExecutionforCompressedAirFlow(LNMeterCollection lNMeterCollection, LNMeterCategory currMeterCategory)
        {
            LNRuntimeArguments lNRuntimeArguments = new LNRuntimeArguments();
            lNRuntimeArguments.lnMeterCategory = currMeterCategory;

            try
            {
                LNLogMgr.WriteLogAsy("ExecutionforCompressedAirFlow started");
                Stopwatch stopwatch = Stopwatch.StartNew();
                foreach (LNCompressedAirFlowMeter _LNCompressedAirFlowMeter in lNMeterCollection)
                {
                    ToReadDatafromHostforCompressedAirFlowMeter(currMeterCategory, _LNCompressedAirFlowMeter);
                    Thread.Sleep(LNGlobal.g_LoopIntervalTime);
                }
                stopwatch.Stop();
                LNLogMgr.WriteLogAsy("ExecutionforCompressedAirFlow time : " + stopwatch.ElapsedMilliseconds + Constants.vbCrLf);
            }
            catch (Exception ex)
            {
                lNRuntimeArguments.RuntimeException = ex;
                LNLogMgr.WriteLogAsy(ex);
            }
            finally
            {
                ProcessToSaveRuntimeErrLog(lNRuntimeArguments);
            }
        }
        private static void ToReadDatafromHostforWaterMeter(LNMeterCategory lNMeterCategory, LNWaterMeter lNWaterMeter)
        {
            LNRuntimeArguments lNRuntimeArguments = new LNRuntimeArguments();
            LNSerialPortServer lNSerialPortServer = lNWaterMeter.LNSerialPortServer;

            try
            {
                lNRuntimeArguments.lnMeterCategory = lNMeterCategory;
                lNRuntimeArguments.lnWaterMeter = lNWaterMeter;
                lNRuntimeArguments.lnSerialPortServer = lNSerialPortServer;
                Hashtable lstReceivedData = new Hashtable();

                LNMeterCollection _lNCommandCollection = lNMeterCategory.LNCommandCollection;
                foreach (LNCommand _lNCommand in _lNCommandCollection)
                {
                    string outputCmd = GenerateOutputCommand(lNSerialPortServer.HostIP, lNSerialPortServer.HostPort, false, lNWaterMeter.MeterAddr, _lNCommand.CommandValue);
                    if (string.IsNullOrEmpty(outputCmd)) { continue; }

                    string ReceivedDataHex = GetDatafromRemoteHostbyTCPIP(lNSerialPortServer.HostIP, lNSerialPortServer.HostPort, outputCmd);
                    if (string.IsNullOrEmpty(ReceivedDataHex))
                    {
                        continue;
                    }
                    else
                    {
                        if ((!lstReceivedData.ContainsKey(_lNCommand.CommandName)))
                            lstReceivedData.Add(_lNCommand.CommandName, ReceivedDataHex);
                    }
                }

                if (lstReceivedData.Count == 0) { return; }
                lNRuntimeArguments.ReceivedData = ProcessToParserReceivedData(lNMeterCategory, lstReceivedData);
                ProcessToSaveData(lNRuntimeArguments);
            }
            catch (Exception ex)
            {
                LNLogMgr.WriteLogAsy(string.Format("Exception Error Message:{0} for remote host [{1}:{2}]",
                    ex.Message, lNSerialPortServer.HostIP, lNSerialPortServer.HostPort));
                lNRuntimeArguments.RuntimeException = ex;
                LNLogMgr.WriteLogAsy(ex);
            }
            finally
            {
                ProcessToSaveRuntimeErrLog(lNRuntimeArguments);
            }
        }
        private static void ToReadDatafromHostforElectricityMeter(LNMeterCategory lNMeterCategory, LNElectricityMeter lNElectricityMeter)
        {
            LNRuntimeArguments lNRuntimeArguments = new LNRuntimeArguments();
            LNSerialPortServer lNSerialPortServer = lNElectricityMeter.LNSerialPortServer;

            try
            {
                lNRuntimeArguments.lnMeterCategory = lNMeterCategory;
                lNRuntimeArguments.lnElectricityMeter = lNElectricityMeter;
                lNRuntimeArguments.lnSerialPortServer = lNSerialPortServer;
                Hashtable lstReceivedData = new Hashtable();
                LNMeterCollection _lNCommandCollection = lNMeterCategory.LNCommandCollection;
                foreach (LNCommand _lNCommand in _lNCommandCollection)
                {
                    string outputCmd = GenerateOutputCommand(lNSerialPortServer.HostIP, lNSerialPortServer.HostPort, lNElectricityMeter.IsNewProtocal, lNElectricityMeter.MeterAddr, _lNCommand.CommandValue);
                    if (string.IsNullOrEmpty(outputCmd)) { continue; }

                    string ReceivedDataHex = GetDatafromRemoteHostbyTCPIP(lNSerialPortServer.HostIP, lNSerialPortServer.HostPort, outputCmd);
                    if (string.IsNullOrEmpty(ReceivedDataHex))
                    {
                        continue;
                    }
                    else
                    {
                        if ((!lstReceivedData.ContainsKey(_lNCommand.CommandName)))
                            lstReceivedData.Add(_lNCommand.CommandName, ReceivedDataHex);

                        if ((!lstReceivedData.ContainsKey(NEWPROTOCAL)))
                            lstReceivedData.Add(NEWPROTOCAL, lNElectricityMeter.IsNewProtocal);
                    }
                }
                if (lstReceivedData.Count == 0) { return; }

                lNRuntimeArguments.ReceivedData = ProcessToParserReceivedData(lNMeterCategory, lstReceivedData);
                ProcessToSaveData(lNRuntimeArguments);

            }
            catch (Exception ex)
            {
                LNLogMgr.WriteLogAsy(string.Format("Exception Error Message:{0} for remote host [{1}:{2}]",
                    ex.Message, lNSerialPortServer.HostIP, lNSerialPortServer.HostPort));
                lNRuntimeArguments.RuntimeException = ex;
                LNLogMgr.WriteLogAsy(ex);
            }
            finally
            {
                ProcessToSaveRuntimeErrLog(lNRuntimeArguments);
            }
        }
        private static void ToReadDatafromHostforCompressedAirFlowMeter(LNMeterCategory lNMeterCategory, LNCompressedAirFlowMeter lNCompressedAirFlowMeter)
        {
            LNRuntimeArguments lNRuntimeArguments = new LNRuntimeArguments();
            LNSerialPortServer lNSerialPortServer = lNCompressedAirFlowMeter.LNSerialPortServer;

            try
            {
                lNRuntimeArguments.lnMeterCategory = lNMeterCategory;
                lNRuntimeArguments.lnCompressedAirFlowMeter = lNCompressedAirFlowMeter;
                lNRuntimeArguments.lnSerialPortServer = lNSerialPortServer;

                Hashtable lstReceivedData = new Hashtable();

                LNMeterCollection _lNCommandCollection = lNMeterCategory.LNCommandCollection;
                foreach (LNCommand _lNCommand in _lNCommandCollection)
                {
                    string outputCmd = GenerateOutputCommand(lNSerialPortServer.HostIP, lNSerialPortServer.HostPort, false, lNCompressedAirFlowMeter.MeterAddr, _lNCommand.CommandValue);
                    if (string.IsNullOrEmpty(outputCmd)) { continue; }

                    string ReceivedDataHex = GetDatafromRemoteHostbyTCPIP(lNSerialPortServer.HostIP, lNSerialPortServer.HostPort, outputCmd);
                    if (string.IsNullOrEmpty(ReceivedDataHex))
                    {
                        continue;
                    }
                    else
                    {
                        if ((!lstReceivedData.ContainsKey(_lNCommand.CommandName)))
                            lstReceivedData.Add(_lNCommand.CommandName, ReceivedDataHex);
                    }
                }

                if (lstReceivedData.Count == 0) { return; }

                lNRuntimeArguments.ReceivedData = ProcessToParserReceivedData(lNMeterCategory, lstReceivedData);
                ProcessToSaveData(lNRuntimeArguments);
            }
            catch (Exception ex)
            {
                LNLogMgr.WriteLogAsy(string.Format("Exception Error Message:{0} for remote host [{1}:{2}]",
                    ex.Message, lNSerialPortServer.HostIP, lNSerialPortServer.HostPort));
                lNRuntimeArguments.RuntimeException = ex;
                LNLogMgr.WriteLogAsy(ex);
            }
            finally
            {
                ProcessToSaveRuntimeErrLog(lNRuntimeArguments);
            }
        }
        private static void ToReadDatafromHostforChilledWaterFlowMeter(LNMeterCategory lNMeterCategory, LNChilledWaterFlowMeter lNChilledWaterFlowMeter)
        {
            LNRuntimeArguments lNRuntimeArguments = new LNRuntimeArguments();
            LNSerialPortServer lNSerialPortServer = lNChilledWaterFlowMeter.LNSerialPortServer;

            try
            {
                lNRuntimeArguments.lnMeterCategory = lNMeterCategory;
                lNRuntimeArguments.lnChilledWaterFlowMeter = lNChilledWaterFlowMeter;
                lNRuntimeArguments.lnSerialPortServer = lNSerialPortServer;
                Hashtable lstReceivedData = new Hashtable();

                LNMeterCollection _lNCommandCollection = lNMeterCategory.LNCommandCollection;

                foreach (LNCommand _lNCommand in _lNCommandCollection)
                {
                    string outputCmd = GenerateOutputCommand(lNSerialPortServer.HostIP, lNSerialPortServer.HostPort, false, lNChilledWaterFlowMeter.MeterAddr, _lNCommand.CommandValue);
                    if (string.IsNullOrEmpty(outputCmd)) { continue; }

                    string ReceivedDataHex = GetDatafromRemoteHostbyTCPIP(lNSerialPortServer.HostIP, lNSerialPortServer.HostPort, outputCmd);
                    if (string.IsNullOrEmpty(ReceivedDataHex))
                    {
                        continue;
                    }
                    else
                    {
                        if ((!lstReceivedData.ContainsKey(_lNCommand.CommandName)))
                            lstReceivedData.Add(_lNCommand.CommandName, ReceivedDataHex);
                    }
                }

                if (lstReceivedData.Count == 0) { return; }
                lNRuntimeArguments.ReceivedData = ProcessToParserReceivedData(lNMeterCategory, lstReceivedData);
                ProcessToSaveData(lNRuntimeArguments);
            }
            catch (Exception ex)
            {
                LNLogMgr.WriteLogAsy(string.Format("Exception Error Message:{0} for remote host [{1}:{2}]",
                    ex.Message, lNSerialPortServer.HostIP, lNSerialPortServer.HostPort));
                lNRuntimeArguments.RuntimeException = ex;
                LNLogMgr.WriteLogAsy(ex);
            }
            finally
            {
                ProcessToSaveRuntimeErrLog(lNRuntimeArguments);
            }
        }
        private static string ToAutoIdentifyCommand(bool IsNewProtocal, string OrgCmd)
        {
            if (OrgCmd.ToUpper().Contains("{NEW}"))
            {
                if (!IsNewProtocal)
                    return string.Empty;
                else
                    return OrgCmd.ToUpper().Replace("{NEW}", "");
            }
            else if (IsNewProtocal)
                return string.Empty;
            else
                return OrgCmd;
        }
    }
}
