using LNMeterInfrastructure.Common;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;

namespace Model
{
    public class LNCompressedAirFlowMeter : LNMeterBase
    {
        /// <summary>
        ///         ''' Entity udtLNCompressedAirFlowMeter
        ///         ''' </summary>
        public LNCompressedAirFlowMeter()
        {
        }

        public LNCompressedAirFlowMeter(int id)
        {
            try
            {
                DataTable dt = LNGlobal.g_DB.GetTable(" udtLNCompressedAirFlowMeter with(nolock) ", string.Format("Where ID = {0} Order By ID", id.ToString()));
                if (dt.Rows.Count > 0)
                {
                    this.ID = (int)dt.Rows[0][0];
                    this.SerialPortServerID = int.Parse(dt.Rows[0][1].ToString());
                    this.MeterAddr = dt.Rows[0][2].ToString();
                    this.MeterName = dt.Rows[0][3].ToString();
                    this.MeterNo = dt.Rows[0][4].ToString();
                    this.AreaLocation = dt.Rows[0][5].ToString();
                    this.AreaFunction = dt.Rows[0][6].ToString();
                }
                else
                {
                    throw new Exception(string.Format("Cannot find the record from udtLNCompressedAirFlowMeter by using the ID[{0}] value.", id.ToString()));
                }
            }
            catch (Exception ex)
            { throw ex; }
        }

        private LNSerialPortServer _serialPortServer = null;

        /// <summary>
        ///         ''' LNSerialPortServer
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
                    _serialPortServer = new LNSerialPortServer(this.SerialPortServerID);
                }
                return _serialPortServer;
            }
        }
    }
}
