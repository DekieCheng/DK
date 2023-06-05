using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using TCPIP;
using Model;
using LNMeterInfrastructure.BLL;
using LNMeterInfrastructure.Common;
using System.Globalization;
using Microsoft.VisualBasic;
using System.Reflection;

namespace ServiceMgr
{
    class LNSvrMgr
    {
        #region Common methods
        private static string ToGenCommand(string meterAddr, string orgCommand)
        {
            string[] cmdByte = new string[5];
            string[] splittedCommand = orgCommand.Split(new char[] { ' ' });
            cmdByte[0] = (Int32.Parse(meterAddr, NumberStyles.HexNumber)).ToString().PadLeft(2, '0');// byte.Parse(meterAddr);
            cmdByte[1] = splittedCommand[0];
            cmdByte[2] = splittedCommand[1];
            cmdByte[3] = splittedCommand[2];
            cmdByte[4] = splittedCommand[3];
            cmdByte[5] = splittedCommand[4];

            string hexCmd = LNGlobal.ToConvertHexString(cmdByte);
            byte[] finalCRC = LNCRC.CRC16(hexCmd);
            string[] destByte = new string[8];
            ToMoveToDestArray(cmdByte, ref destByte);
            destByte[6] = Conversion.Hex(finalCRC[0]);
            destByte[7] = Conversion.Hex(finalCRC[1]);
            return LNGlobal.ToConvertHexString(destByte);
        }
        private static string[] ToMoveToDestArray(string[] sourceByte, ref string[] destByte)
        {
            for (int i = 0; i <= sourceByte.Length - 1; i++)
                destByte[i] = sourceByte[i];
            return destByte;
        }

        private static string ToGetFinalResultByInstance(string DLLName, string DLLClassName, string orgData)
        {
            string result = "";
            try
            {
                Assembly MyAssy;
                MyAssy = Assembly.LoadFrom(Environment.CurrentDirectory.TrimEnd(new char[] { '\\' }) + @"\\" + DLLName + @".dll");
                Type type = MyAssy.GetType(DLLClassName);
                object obj = Activator.CreateInstance(type);

                MethodInfo methodInfo = type.GetMethod("ParserReceivedData");
                MethodInfo genericMethodInfo = methodInfo.MakeGenericMethod(typeof(string));

                result = (string)genericMethodInfo.Invoke(obj, new object[] { orgData });
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private static string ProcessToParserReceivedData(LNMeterCategory lNMeterCategory, string orgReceivedDecData)
        {
            try
            {
                string retData = ToGetFinalResultByInstance(lNMeterCategory.AssemblyName, lNMeterCategory.ClassName, orgReceivedDecData);
                return retData;
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
                using (var _LNTranscationMgr = new LNTranscationMgr())
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
                if (_LNRuntimeArguments.RuntimeException == null || string.IsNullOrEmpty(_LNRuntimeArguments.RuntimeErrMsg)) return;

                using (var _LNTranscationMgr = new LNTranscationMgr())
                {
                    _LNTranscationMgr.SaveRuntimeErrLog(_LNRuntimeArguments);
                }
            }
            catch (Exception ex)
            {
                LNLogMgr.WriteLogAsy(ex);
            }
        }
        #endregion

        #region Trigger the read data process
        public static void FireCollectDataAction(LNMeterCategory currMeterCategory)
        {
            LNRuntimeArguments lNRuntimeArguments = new LNRuntimeArguments();

            try
            {
                lNRuntimeArguments.lnMeterCategory = currMeterCategory;
                LNMeterCollection _lNMeterCollection = currMeterCategory.LNSerialPortServerCollection;

                foreach (LNSerialPortServer lNSerialPortServer in _lNMeterCollection)
                {
                    try
                    {
                        LNMeterCollection _LNElectricityMeterCollection = lNSerialPortServer.LNElectricityMeterCollection;
                        LNMeterCollection _LNWaterMeterCollection = lNSerialPortServer.LNWaterMeterCollection;
                        if (_LNElectricityMeterCollection.Count > 0)
                        {
                            //handle the electricity meter
                            ParallelExecutionforElectricityMeter(_LNElectricityMeterCollection, currMeterCategory);
                        }
                        else if (_LNWaterMeterCollection.Count > 0)
                        {
                            //handle the water meter
                            ParallelExecutionforWaterMeter(_LNWaterMeterCollection, currMeterCategory);
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

        private static void ParallelExecutionforElectricityMeter(LNMeterCollection lNMeterCollection, LNMeterCategory currMeterCategory)
        {
            LNRuntimeArguments lNRuntimeArguments = new LNRuntimeArguments();
            lNRuntimeArguments.lnMeterCategory = currMeterCategory;

            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                Parallel.ForEach((IEnumerable<LNElectricityMeter>)lNMeterCollection, currentMeter =>
              {
                  ToReadDatafromHost(currMeterCategory, currentMeter);
                  Thread.Sleep(10);
              });
                LNLogMgr.WriteLogAsy("ParallelExecution time for ParallelExecutionforElectricityMeter: " + stopwatch.ElapsedMilliseconds);
                stopwatch.Stop();
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

        private static void ParallelExecutionforWaterMeter(LNMeterCollection lNMeterCollection, LNMeterCategory currMeterCategory)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Parallel.ForEach((IEnumerable<LNWaterMeter>)lNMeterCollection, currentMeter =>
           {
               ToReadDatafromHost(currMeterCategory, currentMeter);
               Thread.Sleep(10);
           });
            LNLogMgr.WriteLogAsy("ParallelExecution for ParallelExecutionforWaterMeter: " + stopwatch.ElapsedMilliseconds);
            stopwatch.Stop();
        }

        private static void ToReadDatafromHost(LNMeterCategory lNMeterCategory, LNElectricityMeter lNElectricityMeter)
        {
            LNRuntimeArguments lNRuntimeArguments = new LNRuntimeArguments();
            LNSerialPortServer lNSerialPortServer = lNElectricityMeter.LNSerialPortServer;

            lNRuntimeArguments.lnMeterCategory = lNMeterCategory;
            lNRuntimeArguments.lnElectricityMeter = lNElectricityMeter;
            lNRuntimeArguments.lnSerialPortServer = lNSerialPortServer;
            try
            {
                string outputCmd = ToGenCommand(lNElectricityMeter.MeterAddr, lNMeterCategory.Command);

                using (var _AsyncTcpClient = new AsyncTcpClient(lNSerialPortServer.HostIP, lNSerialPortServer.HostPort, outputCmd.Replace(" ", string.Empty)))
                {
                    //connect to host and read data
                    if (!_AsyncTcpClient.FireMeterClient())
                    {
                        lNRuntimeArguments.RuntimeErrMsg = string.Format("Failed to read data from HostIP [{0}:{1}] by the command [{2}]",
                            lNSerialPortServer.HostIP, lNSerialPortServer.HostPort, outputCmd);
                        return;
                    }
                    else
                    {
                        string ReceivedDataHex = "";
                        if (_AsyncTcpClient.ReceivedData != null && _AsyncTcpClient.ReceivedData.Length > 0)
                        {
                            ReceivedDataHex = LNGlobal.ToConvertHexString(_AsyncTcpClient.ReceivedData);
                            LNLogMgr.WriteLogAsy(string.Format("Received data (Hex) is [{0}]", ReceivedDataHex));
                        }
                        else
                        {
                            lNRuntimeArguments.RuntimeErrMsg = string.Format("Read empty data from HostIP [{0}:{1}] by the command [{2}]",
                                lNSerialPortServer.HostIP, lNSerialPortServer.HostPort, outputCmd);
                            return;
                        }

                        //paser the received data through external DLL
                        string decReceivedData = ProcessToParserReceivedData(lNMeterCategory, ReceivedDataHex);

                        //Save the parserd data into DB tables.
                        ProcessToSaveData(lNRuntimeArguments);
                    }
                };
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

        private static void ToReadDatafromHost(LNMeterCategory lNMeterCategory, LNWaterMeter lNWaterMeter)
        {
            try
            {
                LNSerialPortServer lNSerialPortServer = lNWaterMeter.LNSerialPortServer;
                using (var _AsyncTcpClient = new AsyncTcpClient(lNSerialPortServer.HostIP, lNSerialPortServer.HostPort, ToGenCommand(lNWaterMeter.MeterAddr, lNMeterCategory.Command)))
                {
                    ////connect to host and read data
                    //_AsyncTcpClient.FireMeterClient();
                    //if (_AsyncTcpClient.ReceivedData != null)
                    //{
                    //    if (_AsyncTcpClient.ReceivedData.Length > 0)
                    //    {
                    //        string recData = "";
                    //        for (int i = 0; i <= _AsyncTcpClient.ReceivedData.Length - 1; i++)
                    //        {
                    //            string rcr = Conversion.Hex(_AsyncTcpClient.ReceivedData[i]);
                    //            if (rcr.Length == 1)
                    //                rcr = rcr.PadLeft(2, '0');
                    //            recData = recData + rcr + " ";
                    //        }

                    //        LNLogMgr.WriteLogAsy(string.Format("Received original data (Hex) is [{0}]", recData));
                    //    }
                    //}

                    ////paser the received data through external DLL
                    //byte[] finalData = ProcessToParserReceivedData(lNMeterCategory, _AsyncTcpClient.ReceivedData);

                    ////Save the parserd data into DB tables.
                    //ProcessToSaveData(lNMeterCategory, finalData);
                };
            }
            catch (Exception ex)
            { }
        }

        #endregion
    }
}
