using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class LNMeterBase
    {
        private int _id = Int32.MinValue;
        private int _serialportserverid = Int32.MinValue;
        private string _meteraddr = null;
        private bool _isnewprotocal = false;
        private string _metername = null;
        private string _meterno = null;
        private string _meterlevel = null;
        private bool _issolarenergy = false;
        private string _arealocation = null;
        private string _areafunction = null;

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
        ///         ''' IsNewProtocal(NOT NULL)
        ///         ''' </summary>
        public bool IsNewProtocal
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
    }
}
