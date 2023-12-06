using LNMeterInfrastructure.Common;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;

namespace Model
{
    public class LNElectricityMeter : LNMeterBase
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
                    this.IsNewProtocal = bool.Parse(dt.Rows[0][3].ToString());
                    this.MeterName = dt.Rows[0][4].ToString();
                    this.MeterNo = dt.Rows[0][5].ToString();
                    this.MeterLevel = dt.Rows[0][6].ToString();
                    this.IsSolarEnergy = bool.Parse(dt.Rows[0][7].ToString());
                    this.AreaLocation = dt.Rows[0][8].ToString();
                    this.AreaFunction = dt.Rows[0][9].ToString();
                }
                else
                {
                    throw new Exception(string.Format("Cannot find the record from udtLNElectricityMeter by using the ID[{0}] value.", id.ToString()));
                }
            }
            catch (Exception ex)
            { throw ex; }
        }

        private bool _isnewprotocal = false;
        private string _meterlevel = null;
        private bool _issolarenergy = false;

        private LNSerialPortServer _serialPortServer = null;

        /// <summary>
        ///         ''' IsNewProtocal(NOT NULL)
        ///         ''' </summary>
        public new bool IsNewProtocal
        {
            set
            {
                _isnewprotocal = value;
            }
            get
            {
                return _isnewprotocal;
            }
        }

        /// <summary>
        ///         ''' MeterLevel(NOT NULL)
        ///         ''' </summary>
        public new string MeterLevel
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
        public new bool IsSolarEnergy
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
                if (this.SerialPortServerID < 0) return null;
                if (_serialPortServer == null)
                {
                    _serialPortServer = new LNSerialPortServer(SerialPortServerID);
                }
                return _serialPortServer;
            }
        }
    }
}
