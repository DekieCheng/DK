using LNMeterInfrastructure.Common;
using System;
using System.ComponentModel;
using System.Data;
using System.Collections.Generic;

namespace Model
{
    public class LNMeterCategory
    {
        /// <summary>
        ///         ''' Entity udtLNMeterCategory
        ///         ''' </summary>
        public LNMeterCategory()
        {
        }
        public LNMeterCategory(int id)
        {
            try
            {
                DataTable dt = LNGlobal.g_DB.GetTable("udtLNMeterCategory  with(nolock)", string.Format("where id = {0} with(nolock) Order By ID", id.ToString()));
                if (dt.Rows.Count > 0)
                {
                    this.ID = (int)dt.Rows[0][0];
                    this.CategoryName = dt.Rows[0][1].ToString();
                    this.CategoryDesc = dt.Rows[0][2].ToString();
                    this.CycleTime = double.Parse(dt.Rows[0][3].ToString());
                    this.UDPToSaveData = dt.Rows[0][4].ToString();
                    this.UDPToSaveRuntimeLog = dt.Rows[0][5].ToString();
                    this.AssemblyName = dt.Rows[0][6].ToString();
                    this.ClassName = dt.Rows[0][7].ToString();
                }
                else
                {
                    throw new Exception(string.Format("Cannot find the record from udtLNMeterCategory by using the ID[{0}] value.", id.ToString()));
                }
            }
            catch (Exception ex)
            { throw ex; }
        }

        public LNMeterCategory(string categoryName)
        {
            try
            {
                DataTable dt = LNGlobal.g_DB.GetTable(" udtLNMeterCategory  with(nolock)  ", string.Format("Where CategoryName ='{0}' Order By ID", categoryName));
                if (dt.Rows.Count > 0)
                {
                    this.ID = (int)dt.Rows[0][0];
                    this.CategoryName = dt.Rows[0][1].ToString();
                    this.CategoryDesc = dt.Rows[0][2].ToString();
                    this.CycleTime = double.Parse(dt.Rows[0][3].ToString());
                    this.UDPToSaveData = dt.Rows[0][4].ToString();
                    this.UDPToSaveRuntimeLog = dt.Rows[0][5].ToString();
                    this.AssemblyName = dt.Rows[0][6].ToString();
                    this.ClassName = dt.Rows[0][7].ToString();
                }
                else
                {
                    throw new Exception(string.Format("Cannot find the record from udtLNMeterCategory by using the CategoryName [{0}] value.", categoryName));
                }
            }
            catch (Exception ex)
            {
                LNLogMgr.WriteLogAsy(ex);
                throw ex;
            }
        }

        private int _id = int.MinValue;
        private string _categoryname = null;
        private string _categorydesc = null;
        private double _cycletime = double.MinValue;
        private string _udptosavedata = null;
        private string _udptosaveruntimelog = null;
        private string _assemblyname = null;
        private string _classname = null;

        private LNMeterCollection _LNSerialPortServerCollection = null;
        private LNMeterCollection _LNCommandCollection = null;

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
        public string CategoryName
        {
            set
            {
                _categoryname = value;
            }
            get
            {
                return _categoryname;
            }
        }
        /// <summary>
        ///         ''' CommandType(NOT NULL)
        ///         ''' </summary>
        public string CategoryDesc
        {
            set
            {
                _categorydesc = value;
            }
            get
            {
                return _categorydesc;
            }
        }
        /// <summary>
        ///         ''' CycleTime(NOT NULL)
        ///         ''' </summary>
        public double CycleTime
        {
            set
            {
                _cycletime = value;
            }
            get
            {
                return _cycletime;
            }
        }

        /// <summary>
        ///         ''' UDPToSaveData(NOT NULL)
        ///         ''' </summary>
        public string UDPToSaveData
        {
            set
            {
                _udptosavedata = value;
            }
            get
            {
                return _udptosavedata;
            }
        }
        /// <summary>
        ///         ''' UDPToSaveErrInfo(NOT NULL)
        ///         ''' </summary>
        public string UDPToSaveRuntimeLog
        {
            set
            {
                _udptosaveruntimelog = value;
            }
            get
            {
                return _udptosaveruntimelog;
            }
        }
        /// <summary>
        ///         ''' Assembly Name(NOT NULL)
        ///         ''' </summary>
        public string AssemblyName
        {
            set
            {
                _assemblyname = value;
            }
            get
            {
                return _assemblyname;
            }
        }
        /// <summary>
        ///         ''' Class Name(NOT NULL)
        ///         ''' </summary>
        public string ClassName
        {
            set
            {
                _classname = value;
            }
            get
            {
                return _classname;
            }
        }

        public LNMeterCollection LNSerialPortServerCollection
        {
            set
            {
                _LNSerialPortServerCollection = value;
            }
            get
            {
                if (_id < 0) return null;
                if (_LNSerialPortServerCollection == null)
                {
                    _LNSerialPortServerCollection = new LNMeterCollection("udtLNSerialPortServer", this.ID);
                }

                return _LNSerialPortServerCollection;
            }
        }

        public LNMeterCollection LNCommandCollection
        {
            set
            {
                _LNCommandCollection = value;
            }
            get
            {
                if (_id < 0) return null;
                if (_LNCommandCollection == null)
                {
                    _LNCommandCollection = new LNMeterCollection("udtLNCommand", this.ID);
                }

                return _LNCommandCollection;
            }
        }
    }
}
