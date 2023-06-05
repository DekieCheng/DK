using LNMeterInfrastructure.Common;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;

namespace Model
{
    public class LNElectricityMeter
    {
        /// <summary>
        ///         ''' Entity udtLNElectricityMeter
        ///         ''' </summary>
        public LNElectricityMeter()
        {
        }

        public LNElectricityMeter(int id)
        {
            try
            {
                DataTable dt = LNGlobal.g_DB.GetTable(" udtLNElectricityMeter with(nolock) ", string.Format("Where ID = {0} Order By ID", id.ToString()));
                if (dt.Rows.Count > 0)
                {
                    this.ID = (int)dt.Rows[0][0];
                    this.SerialPortServerID = int.Parse(dt.Rows[0][1].ToString());
                    this.MeterAddr = dt.Rows[0][2].ToString();
                    this.MeterName = dt.Rows[0][3].ToString();
                    this.MeterNo = dt.Rows[0][4].ToString();
                    this.MeterLevel = dt.Rows[0][5].ToString();
                    this.IsSolarEnergy = bool.Parse(dt.Rows[0][6].ToString());
                    this.AreaLocation = dt.Rows[0][7].ToString();
                    this.AreaFunction = dt.Rows[0][8].ToString();
                }
                else
                {
                    throw new Exception(string.Format("Cannot find the record from udtLNElectricityMeter by using the ID[{0}] value.", id.ToString()));
                }
            }
            catch (Exception ex)
            { throw ex; }
        }

        private int _id = Int32.MinValue;
        private int _serialportserverid = Int32.MinValue;
        private string _meteraddr = null;
        private string _metername = null;
        private string _meterno = null;
        private string _meterlevel = null;
        private bool _issolarenergy = false;
        private string _arealocation = null;
        private string _areafunction = null;

        private LNSerialPortServer _serialPortServer = null;

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
        ///         ''' SerialPortServerID(NOT NULL)
        ///         ''' </summary>
        public int SerialPortServerID
        {
            set
            {
                _serialportserverid = value;
            }
            get
            {
                return _serialportserverid;
            }
        }
        /// <summary>
        ///         ''' MeterAddr(NOT NULL)
        ///         ''' </summary>
        public string MeterAddr
        {
            set
            {
                _meteraddr = value;
            }
            get
            {
                return _meteraddr;
            }
        }
        /// <summary>
        ///         ''' MeterName(NOT NULL)
        ///         ''' </summary>
        public string MeterName
        {
            set
            {
                _metername = value;
            }
            get
            {
                return _metername;
            }
        }
        /// <summary>
        ///         ''' MeterNo(NOT NULL)
        ///         ''' </summary>
        public string MeterNo
        {
            set
            {
                _meterno = value;
            }
            get
            {
                return _meterno;
            }
        }
        /// <summary>
        ///         ''' MeterLevel(NOT NULL)
        ///         ''' </summary>
        public string MeterLevel
        {
            set
            {
                _meterlevel = value;
            }
            get
            {
                return _meterlevel;
            }
        }
        /// <summary>
        ///         ''' IsSolarEnergy(NOT NULL)
        ///         ''' </summary>
        public bool IsSolarEnergy
        {
            set
            {
                _issolarenergy = value;
            }
            get
            {
                return _issolarenergy;
            }
        }
        /// <summary>
        ///         ''' AreaLocation(NOT NULL)
        ///         ''' </summary>
        public string AreaLocation
        {
            set
            {
                _arealocation = value;
            }
            get
            {
                return _arealocation;
            }
        }
        /// <summary>
        ///         ''' AreaFunction(NOT NULL)
        ///         ''' </summary>
        public string AreaFunction
        {
            set
            {
                _areafunction = value;
            }
            get
            {
                return _areafunction;
            }
        }

        /// <summary>
        ///         ''' MeterCategory(NOT NULL)
        ///         ''' </summary>
        public LNSerialPortServer LNSerialPortServer
        {
            set
            {
                _serialPortServer = value;
            }
            get
            {
                if (_serialportserverid < 0) return null;
                if (_serialPortServer == null)
                {
                    _serialPortServer = new LNSerialPortServer(_serialportserverid);
                }
                return _serialPortServer;
            }
        }
    }
}
