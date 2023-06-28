using System;
using System.Data.SqlClient;
using Model;
using LNMeterInfrastructure.Common;
using System.Collections;

namespace LNMeterInfrastructure.DAL
{
    public class LNTranscationDAL : LNDBWrapper, IDisposable
    {
        private bool disposedValue;

        public bool ImportDataToDB(LNRuntimeArguments lNRuntimeArguments)
        {
            bool flag;
            try
            {
                LNParameterCollection lnPars = new LNParameterCollection();
                if (lNRuntimeArguments.lnMeterCategory != null)
                {
                    lnPars.Add(new LNParameter("@MeterCategoryID", lNRuntimeArguments.lnMeterCategory.ID));
                }

                if (lNRuntimeArguments.lnSerialPortServer != null)
                {
                    lnPars.Add(new LNParameter("@SerialPortServerID", lNRuntimeArguments.lnSerialPortServer.ID));
                }

                if (lNRuntimeArguments.lnElectricityMeter != null)
                {
                    lnPars.Add(new LNParameter("@MeterID", lNRuntimeArguments.lnElectricityMeter.ID));
                }
                else if (lNRuntimeArguments.lnWaterMeter != null)
                {
                    lnPars.Add(new LNParameter("@MeterID", lNRuntimeArguments.lnWaterMeter.ID));
                }
                else if (lNRuntimeArguments.lnChilledWaterFlowMeter != null)
                {
                    lnPars.Add(new LNParameter("@MeterID", lNRuntimeArguments.lnChilledWaterFlowMeter.ID));
                }
                else if (lNRuntimeArguments.lnCompressedAirFlowMeter != null)
                {
                    lnPars.Add(new LNParameter("@MeterID", lNRuntimeArguments.lnCompressedAirFlowMeter.ID));
                }

                if (lNRuntimeArguments.ReceivedData != null)
                {
                    lnPars.Add(new LNParameter("@xmlRecData", ToGenRecDataXML(lNRuntimeArguments.ReceivedData)));
                }

                int retVal = 0;
                this.ExecuteSPWithNoResults(lNRuntimeArguments.lnMeterCategory.UDPToSaveData, lnPars, ref retVal, true);
                flag = true;
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
        }

        private string ToGenRecDataXML(Hashtable recData)
        {

            try
            {
                LNXMLDetailBuilder _lNXMLDetailBuilder = new LNXMLDetailBuilder(true);
                foreach (DictionaryEntry de in recData)
                {
                    _lNXMLDetailBuilder.Add(de.Key.ToString(), de.Value.ToString());
                }
                return _lNXMLDetailBuilder.ToXMLString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool SaveRuntimeErrLog(LNRuntimeArguments lNRuntimeArguments)
        {
            bool flag = false;
            string ErrMsg = "";
            try
            {
                LNParameterCollection lnPars = new LNParameterCollection();
                if (lNRuntimeArguments.lnMeterCategory != null)
                {
                    lnPars.Add(new LNParameter("@MeterCategoryID", lNRuntimeArguments.lnMeterCategory.ID));
                    ErrMsg += string.Format("[MeterCategory:]", lNRuntimeArguments.lnMeterCategory.CategoryName);
                }
                if (lNRuntimeArguments.lnSerialPortServer != null)
                {
                    lnPars.Add(new LNParameter("@SerialPortServerID", lNRuntimeArguments.lnSerialPortServer.ID));
                    ErrMsg += string.Format("[SerialPortServer:{0}{1}]", lNRuntimeArguments.lnSerialPortServer.HostIP, lNRuntimeArguments.lnSerialPortServer.HostPort.ToString());
                }
                if (lNRuntimeArguments.lnElectricityMeter != null)
                {
                    lnPars.Add(new LNParameter("@MeterID", lNRuntimeArguments.lnElectricityMeter.ID));
                    ErrMsg += string.Format("[ElectricityMeter MeterAddr:{0}]", lNRuntimeArguments.lnElectricityMeter.MeterAddr);
                }
                else if (lNRuntimeArguments.lnWaterMeter != null)
                {
                    lnPars.Add(new LNParameter("@MeterID", lNRuntimeArguments.lnWaterMeter.ID));
                    ErrMsg += string.Format("[WaterMeter MeterAddr:{0}]", lNRuntimeArguments.lnWaterMeter.MeterAddr);
                }
                ErrMsg += ToGenExceptionErrMsg(lNRuntimeArguments.RuntimeErrMsg, lNRuntimeArguments.RuntimeException);

                lnPars.Add(new LNParameter("@ErrorMessage", ErrMsg));

                int retVal = 0;
                this.ExecuteSPWithNoResults(lNRuntimeArguments.lnMeterCategory.UDPToSaveRuntimeLog, lnPars, ref retVal, true);
                flag = true;
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
        }

        private string ToGenExceptionErrMsg(string RecErrMsg, Exception recEx)
        {
            string ErrMsg = "";
            if (!string.IsNullOrEmpty(RecErrMsg))
            {
                ErrMsg += string.Format("[Error Message: {0}]", RecErrMsg);
            }
            if (recEx != null)
            {
                ErrMsg = Environment.NewLine + ErrMsg
               + "App Server: " + LNGlobal.g_AppServerName + Environment.NewLine
               + "Exception Source: " + recEx.TargetSite.ToString() + Environment.NewLine
               + "Message: " + recEx.Message + Environment.NewLine
               + "StackTrace: " + recEx.StackTrace;
                if (recEx.InnerException != null)
                {
                    ErrMsg = Environment.NewLine + ErrMsg
                        + "InnerException Message: " + recEx.InnerException.Message + Environment.NewLine
                        + "InnerException StackTrace: " + recEx.InnerException.StackTrace;
                }
            }
            return ErrMsg;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~LNTranscationDAL()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
