using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using LNMeterInfrastructure.Common;
using Microsoft.VisualBasic;

namespace Model
{

    [Serializable()]
    public class LNMeterCollection : IEnumerable
    {
        //   protected Collection m_LNMeterCollection = new Collection();

        protected List<object> m_LNMeterCollection = new List<object>();

        public System.Collections.IEnumerator GetEnumerator()
        {
            return m_LNMeterCollection.GetEnumerator();
        }

        internal LNMeterCollection()
        {
        }

        internal LNMeterCollection(string tableName, int id)
        {
            DataTable dt;

            try
            {
                if (tableName.Equals("udtLNSerialPortServer"))
                {
                    GenSerialPortServerCollection(id);
                }
                else if (tableName.Equals("udtLNElectricityMeter"))
                {
                    GenElectricityMeterCollection(id);
                }
                else if (tableName.Equals("udtLNWaterMeter"))
                {
                    GenWaterMeterCollection(id);
                }
                else if (tableName.Equals("udtLNChilledWaterFlowMeter"))
                {
                    GenChilledWaterFlowMeterCollection(id);
                }
                else if (tableName.Equals("udtLNCompressedAirFlowMeter"))
                {
                    GenCompressedAirFlowMeterCollection(id);
                }
                else if (tableName.Equals("udtLNCommand"))
                {
                    GenLNCommandCollection(id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GenSerialPortServerCollection(int id)
        {
            try
            {
                DataTable dt = LNGlobal.g_DB.GetTable(" udtLNSerialPortServer with(nolock) ", string.Format("Where MeterCategoryID = {0} And Status = 1 Order By ID", id.ToString()));
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        LNSerialPortServer lNSerialPortServer = new LNSerialPortServer();
                        lNSerialPortServer.ID = int.Parse(dr[0].ToString());
                        lNSerialPortServer.MeterCategoryID = int.Parse(dr[1].ToString());
                        lNSerialPortServer.HostIP = dr[2].ToString();
                        lNSerialPortServer.HostPort = int.Parse(dr[3].ToString());
                        lNSerialPortServer.Location = dr[4].ToString();
                        m_LNMeterCollection.Add(lNSerialPortServer);
                    }
                }
                else
                {
                    //   throw new Exception(string.Format("Cannot find the records from udtLNSerialPortServer by using the MeterCategoryID[{0}] value.", id.ToString()));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GenElectricityMeterCollection(int id)
        {
            try
            {
                DataTable dt = LNGlobal.g_DB.GetTable(" udtLNElectricityMeter with(nolock) ", string.Format("Where SerialPortServerID = {0} Order By ID", id.ToString()));
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        LNElectricityMeter _LNElectricityMeter = new LNElectricityMeter
                        {
                            ID = int.Parse(dr[0].ToString()),
                            SerialPortServerID = int.Parse(dr[1].ToString()),
                            MeterAddr = dr[2].ToString(),
                            IsNewProtocal = bool.Parse(dr[3].ToString()),
                            MeterName = dr[4].ToString(),
                            MeterNo = dr[5].ToString(),
                            MeterLevel = dr[6].ToString(),
                            IsSolarEnergy = bool.Parse(dr[7].ToString()),
                            AreaLocation = dr[8].ToString(),
                            AreaFunction = dr[9].ToString()
                        };

                        m_LNMeterCollection.Add(_LNElectricityMeter);
                    }
                }
                else
                {
                    //   throw new Exception(string.Format("Cannot find the records from udtLNElectricityMeter by using the SerialPortServerID[{0}] value.", id.ToString()));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GenWaterMeterCollection(int id)
        {
            try
            {
                DataTable dt = LNGlobal.g_DB.GetTable("udtLNWaterMeter with(nolock)", string.Format("Where SerialPortServerID = {0} Order By ID", id.ToString()));
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        LNWaterMeter _lNWaterMeter = new LNWaterMeter
                        {
                            ID = int.Parse(dr[0].ToString()),
                            SerialPortServerID = int.Parse(dr[1].ToString()),
                            MeterAddr = dr[2].ToString(),
                            MeterName = dr[3].ToString(),
                            MeterNo = dr[4].ToString(),
                            AreaLocation = dr[5].ToString(),
                            AreaFunction = dr[6].ToString()
                        };

                        m_LNMeterCollection.Add(_lNWaterMeter);
                    }
                }
                else
                {
                    //throw new Exception(string.Format("Cannot find the records from udtLNWaterMeter by using the SerialPortServerID[{0}] value.", id.ToString()));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GenChilledWaterFlowMeterCollection(int id)
        {
            try
            {
                DataTable dt = LNGlobal.g_DB.GetTable("udtLNChilledWaterFlowMeter with(nolock)", string.Format("Where SerialPortServerID = {0} Order By ID", id.ToString()));
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        LNChilledWaterFlowMeter _lNWaterMeter = new LNChilledWaterFlowMeter
                        {
                            ID = int.Parse(dr[0].ToString()),
                            SerialPortServerID = int.Parse(dr[1].ToString()),
                            MeterAddr = dr[2].ToString(),
                            MeterName = dr[3].ToString(),
                            MeterNo = dr[4].ToString(),
                            AreaLocation = dr[5].ToString(),
                            AreaFunction = dr[6].ToString()
                        };

                        m_LNMeterCollection.Add(_lNWaterMeter);
                    }
                }
                else
                {
                    //  throw new Exception(string.Format("Cannot find the records from udtLNWaterMeter by using the SerialPortServerID[{0}] value.", id.ToString()));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GenCompressedAirFlowMeterCollection(int id)
        {
            try
            {
                DataTable dt = LNGlobal.g_DB.GetTable("udtLNCompressedAirFlowMeter with(nolock)", string.Format("Where SerialPortServerID = {0} Order By ID", id.ToString()));
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        LNCompressedAirFlowMeter _lNWaterMeter = new LNCompressedAirFlowMeter
                        {
                            ID = int.Parse(dr[0].ToString()),
                            SerialPortServerID = int.Parse(dr[1].ToString()),
                            MeterAddr = dr[2].ToString(),
                            MeterName = dr[3].ToString(),
                            MeterNo = dr[4].ToString(),
                            AreaLocation = dr[5].ToString(),
                            AreaFunction = dr[6].ToString()
                        };

                        m_LNMeterCollection.Add(_lNWaterMeter);
                    }
                }
                else
                {
                    //  throw new Exception(string.Format("Cannot find the records from udtLNCompressedAirFlowMeter by using the SerialPortServerID[{0}] value.", id.ToString()));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GenLNCommandCollection(int id)
        {
            try
            {
                DataTable dt = LNGlobal.g_DB.GetTable(" udtLNCommand with(nolock) ", string.Format("Where MeterCategoryID = {0} And Status = 1 Order By ID", id.ToString()));
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        LNCommand _lNWaterMeter = new LNCommand
                        {
                            ID = int.Parse(dr[0].ToString()),
                            MeterCategoryID = int.Parse(dr[1].ToString()),
                            CommandName = dr[2].ToString(),
                            CommandValue = dr[3].ToString()
                        };

                        m_LNMeterCollection.Add(_lNWaterMeter);
                    }
                }
                else
                {
                    //  throw new Exception(string.Format("Cannot find the records from udtLNCompressedAirFlowMeter by using the SerialPortServerID[{0}] value.", id.ToString()));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Count
        {
            get
            {
                return m_LNMeterCollection.Count;
            }
        }
    }
}
