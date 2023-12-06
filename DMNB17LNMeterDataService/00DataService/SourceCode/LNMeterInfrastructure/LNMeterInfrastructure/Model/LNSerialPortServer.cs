using LNMeterInfrastructure.Common;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;

namespace Model
{
    public class LNSerialPortServer
    {
        /// <summary>
        ///         ''' Entity udtLNMeterCategory
        ///         ''' </summary>
        public LNSerialPortServer()
        {
        }

        public LNSerialPortServer(int id)
        {
            try
            {
                DataTable dt = LNGlobal.g_DB.GetTable(" udtLNSerialPortServer  with(nolock) ", string.Format("Where ID = {0} And Status = 1 Order By ID", id.ToString()));
                if (dt.Rows.Count > 0)
                {
                    this.ID = (int)dt.Rows[0][0];
                    this.MeterCategoryID = int.Parse(dt.Rows[0][1].ToString());
                    this.HostIP = dt.Rows[0][2].ToString();
                    this.HostPort = int.Parse(dt.Rows[0][3].ToString());
                    this.Location = dt.Rows[0][4].ToString();
                }
                else
                {
                    throw new Exception(string.Format("Cannot find the record from udtLNSerialPortServer by using the ID[{0}] value.", id.ToString()));
                }
            }
            catch (Exception ex)
            { throw ex; }
        }

        private int _id = Int32.MinValue;
        private LNMeterCategory _lnmetercategory = null;
        private int _metercategoryid = -1;
        private string _hostip = null;
        private int _port = 5300;
        private string _location = null;

        /// <summary>
        ///         ''' Primary ID(NOT NULL)
        ///         ''' </summary>
        public Int32 ID
        {
            set
            {
                _id = value;
            }
            get
            {
                return _id;
            }
        }
        /// <summary>
        ///         ''' CommandType(NOT NULL)
        ///         ''' </summary>
        public int MeterCategoryID
        {
            set
            {
                _metercategoryid = value;
            }
            get
            {
                return _metercategoryid;
            }
        }
        /// <summary>
        ///         ''' MeterCategory(NOT NULL)
        ///         ''' </summary>
        public LNMeterCategory MeterCategory
        {
            set
            {
                _lnmetercategory = value;
            }
            get
            {
                if (_metercategoryid < 0) return null;
                if (_lnmetercategory == null)
                {
                    _lnmetercategory = new LNMeterCategory(_metercategoryid);
                }
                return _lnmetercategory;
            }
        }
        /// <summary>
        ///         ''' HostIP(NOT NULL)
        ///         ''' </summary>
        public string HostIP
        {
            set
            {
                _hostip = value;
            }
            get
            {
                return _hostip;
            }
        }
        /// <summary>
        ///         ''' HostPort(NOT NULL)
        ///         ''' </summary>
        public int HostPort
        {
            set
            {
                _port = value;
            }
            get
            {
                return _port;
            }
        }

        /// <summary>
        ///         ''' Location(NOT NULL)
        ///         ''' </summary>
        public string Location
        {
            set
            {
                _location = value;
            }
            get
            {
                return _location;
            }
        }

        public LNMeterCollection GetAllWaterMeter(int lnSerialPortServerID)
        {
            try
            {
                LNMeterCollection lNMeterCollection = new LNMeterCollection("udtLNWaterMeter", lnSerialPortServerID);
                return lNMeterCollection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public LNMeterCollection GetAllElectricityMeter(int lnSerialPortServerID)
        {
            try
            {
                LNMeterCollection lNMeterCollection = new LNMeterCollection("udtLNElectricityMeter", lnSerialPortServerID);
                return lNMeterCollection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public LNMeterCollection GetAllChilledWaterFlowMeter(int lnSerialPortServerID)
        {
            try
            {
                LNMeterCollection lNMeterCollection = new LNMeterCollection("udtLNChilledWaterFlowMeter", lnSerialPortServerID);
                return lNMeterCollection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public LNMeterCollection GetCompressedAirFlowMeter(int lnSerialPortServerID)
        {
            try
            {
                LNMeterCollection lNMeterCollection = new LNMeterCollection("udtLNCompressedAirFlowMeter", lnSerialPortServerID);
                return lNMeterCollection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
