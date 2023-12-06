using System;
using System.Collections.Generic;
using System.Data;
using LNMeterInfrastructure.DAL;
using Model;
using LNMeterInfrastructure.Common;

namespace LNMeterInfrastructure.BLL
{
    public class LNMeterMgr : LNDBWrapper
    {
        public List<LNMeterCategory> GetAllLNMeterCategory()
        {
            List<LNMeterCategory> _lNMeterCategoryCollection = null;
            try
            {
                _lNMeterCategoryCollection = _GetAllLNMeterCategory("");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _lNMeterCategoryCollection;
        }

        public List<LNMeterCategory> GetAllLNMeterCategory(string specifiedFilter)
        {
            List<LNMeterCategory> _lNMeterCategoryCollection = null;
            try
            {
                _lNMeterCategoryCollection = _GetAllLNMeterCategory(specifiedFilter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _lNMeterCategoryCollection;
        }

        private List<LNMeterCategory> _GetAllLNMeterCategory(string filter)
        {
            char[] separaterChar = new char[] { ';' };
            string whereClause = "";
            List<LNMeterCategory> _lNMeterCategoryCollection = null;

            try
            {
                if (!string.IsNullOrEmpty(filter))
                {
                    foreach (string currFilter in filter.Split(separaterChar))
                    {
                        whereClause = whereClause + "'" + currFilter + "',";
                    }
                    if (whereClause.Length > 0)
                    {
                        char[] trimChar = new char[] { ',' };
                        whereClause = " Where CategoryName in (" + whereClause.TrimEnd(trimChar) + ") ";
                    }
                }

                DataTable dt = GetTable(" udtLNMeterCategory with(nolock) ", string.IsNullOrEmpty(whereClause) ? "" : whereClause, " * ");
                if (dt.Rows.Count > 0)
                {
                    _lNMeterCategoryCollection = new List<LNMeterCategory>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        _lNMeterCategoryCollection.Add(GenMeterCategoryInstance(dr));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _lNMeterCategoryCollection;
        }

        internal static LNMeterCategory GenMeterCategoryInstance(DataRow dr)
        {
            LNMeterCategory objTemp = new LNMeterCategory
            {
                ID = int.Parse(dr[0].ToString()),
                CategoryName = dr[1].ToString(),
                CategoryDesc = dr[2].ToString(),
                CycleTime = double.Parse(dr[3].ToString()),
                UDPToSaveData = dr[4].ToString(),
                UDPToSaveRuntimeLog = dr[5].ToString(),
                AssemblyName = dr[6].ToString(),
                ClassName = dr[7].ToString()
            };
            return objTemp;
        }
    }
}
