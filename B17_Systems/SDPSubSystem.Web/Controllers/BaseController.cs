using SDPSubSystem.Common.Customized;
using SDPSubSystem.Data;
using SDPSubSystem.Model.AssistModels;
using SDPSubSystem.Model.SysModels;
using SDPSubSystem.Services;
using SDPSubSystem.Services.Oracle;
using SDPSubSystem.Services.SysServices;
using SDPSubSystem.Web.Common;
using SDPSubSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace SDPSubSystem.Web.Controllers
{
    public class BaseController : Controller
    {

        RawInputService _service = new RawInputService();

        
        #region 连接baan数据库，并同步数据
        protected void SyncBaanData_Glue_Jabra()
        {
            //获取PN
            IDataParameter[] sqlparameter =
            {

                };
            DataSet ds = _service.RunProcedure("GlueGetPNInfoForBaan", sqlparameter, "bb", ConDB.PlataContext);

            string sStr = ds.Tables[0].Rows[0][0].ToString();

            //同步data
            BaanService baanservice = new BaanService();
            DataTable dt = baanservice.ExecuteSqlbyParaInAM3(sStr);

            string xml = DtToXml.DtToXmlString(dt);

            int code = 0;
            string errormsg = _service.RunProcedure("GlueSyncBaan", xml, out code, ConDB.PlataContext);

        }

        protected void SyncBaanData_Glue_Dyson()
        {
            //获取PN
            IDataParameter[] sqlparameter =
            {

                };
            DataSet ds = _service.RunProcedure("GlueGetPNInfoForBaan", sqlparameter, "bb", ConDB.DysonContext);

            string sStr = ds.Tables[0].Rows[0][0].ToString();

            //同步data
            BaanService baanservice = new BaanService();
            DataTable dt = baanservice.ExecuteSqlbyParaInAM3(sStr);

            string xml = DtToXml.DtToXmlString(dt);

            int code = 0;
            string errormsg = _service.RunProcedure("GlueSyncBaan", xml, out code, ConDB.DysonContext);

        }

        protected void SyncBaanData_Glue_GHD()
        {
            //获取PN
            IDataParameter[] sqlparameter =
            {

                };
            DataSet ds = _service.RunProcedure("GlueGetPNInfoForBaan", sqlparameter, "bb", ConDB.GHDContext);

            string sStr = ds.Tables[0].Rows[0][0].ToString();

            //同步data
            BaanService baanservice = new BaanService();
            DataTable dt = baanservice.ExecuteSqlbyParaInAM3(sStr);

            string xml = DtToXml.DtToXmlString(dt);

            int code = 0;
            string errormsg = _service.RunProcedure("GlueSyncBaan", xml, out code, ConDB.GHDContext);

        }

        protected void SyncBaanData_Glue_Nuskin()
        {
            //获取PN
            IDataParameter[] sqlparameter =
            {

                };
            DataSet ds = _service.RunProcedure("GlueGetPNInfoForBaan", sqlparameter, "bb", ConDB.NuskinContext);

            string sStr = ds.Tables[0].Rows[0][0].ToString();

            //同步data
            BaanService baanservice = new BaanService();
            DataTable dt = baanservice.ExecuteSqlbyParaInAM3(sStr);

            string xml = DtToXml.DtToXmlString(dt);

            int code = 0;
            string errormsg = _service.RunProcedure("GlueSyncBaan", xml, out code, ConDB.NuskinContext);

        }

        protected void SyncBaanData_Glue_Rebel()
        {
            //获取PN
            IDataParameter[] sqlparameter =
            {

                };
            DataSet ds = _service.RunProcedure("GlueGetPNInfoForBaan", sqlparameter, "bb", ConDB.RebelContext);

            string sStr = ds.Tables[0].Rows[0][0].ToString();

            //同步data
            BaanService baanservice = new BaanService();
            DataTable dt = baanservice.ExecuteSqlbyParaInAM3(sStr);

            string xml = DtToXml.DtToXmlString(dt);

            int code = 0;
            string errormsg = _service.RunProcedure("GlueSyncBaan", xml, out code, ConDB.RebelContext);

        }
        #endregion

        #region Json格式转换
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }

        protected JsonResult JsonFormat(object data, JsonRequestBehavior behavior, string format)
        {
            return new Models.JsonNetResult
            {
                Data = data,
                JsonRequestBehavior = behavior,
                Format = format
            };
        }
        protected JsonResult JsonFormat(object data, string format)
        {
            return new JsonNetResult
            {
                Data = data,
                Format = format
            };
        }

        protected JsonResult JsonFormat(object data)
        {
            return new JsonNetResult
            {
                Data = data
            };
        }
        #endregion

        #region Select List Item
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        protected virtual List<SelectListItem> GetAllConnectStr(string filter)
        {
            Dictionary<string, object> FFDB = new Dictionary<string, object>();
            ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
            //int ii = connections.Count;
            List<SelectListItem> dblist = new List<SelectListItem>();
            int i = 1;
            foreach (ConnectionStringSettings connstr in connections)
            {
                if (connstr.Name.ToString().Contains(filter))
                {
                    dblist.Add(new SelectListItem() { Value = i.ToString(), Text = connstr.Name });
                    i = i + 1;
                }
            }
            dblist.Insert(0, new SelectListItem { Value = "", Text = "===None===", Selected = true });
            return dblist;
        }

        protected List<SelectListItem> DBToSelectList(DataTable dt, int defaultID = 0)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ID"].ToString() == defaultID.ToString())
                {
                    list.Add(new SelectListItem() { Value = dr["ID"].ToString(), Text = dr["Name"].ToString(), Selected = true });
                }
                else
                {
                    list.Add(new SelectListItem() { Value = dr["ID"].ToString(), Text = dr["Name"].ToString() });
                }
            }
            return list;
        }

        protected List<SelectListItem> DBToSelectList(DataTable dt, string defaultID)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ID"].ToString() == defaultID)
                {
                    list.Add(new SelectListItem() { Value = dr["ID"].ToString(), Text = dr["Name"].ToString(), Selected = true });
                }
                else
                {
                    list.Add(new SelectListItem() { Value = dr["ID"].ToString(), Text = dr["Name"].ToString() });
                }
            }
            return list;
        }

        protected List<SelectListItem> DBToSelectListName(DataTable dt, string defaultName)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["Name"].ToString() == defaultName)
                {
                    list.Add(new SelectListItem() { Value = dr["ID"].ToString(), Text = dr["Name"].ToString(), Selected = true });
                }
                else
                {
                    list.Add(new SelectListItem() { Value = dr["ID"].ToString(), Text = dr["Name"].ToString() });
                }
            }
            return list;
        }

        protected List<SelectListItem> DBToSelectList_noID(DataTable dt, int defaultID = 0)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem() { Value = "0", Text = "", Selected = true });

            int count = 1;
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new SelectListItem() { Value = count.ToString(), Text = dr[0].ToString() });

                count++;
            }
            return list;
        }

        protected List<SelectListCustomize> DBToList_noID(DataTable dt)
        {
            List<SelectListCustomize> list = new List<SelectListCustomize>();
            int count = 1;
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new SelectListCustomize() { ID = count.ToString(), Name = dr[0].ToString() });
                count++;
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        protected virtual List<SYS_DBConnection> GetAllConnectStrFromDB()
        {
            var context = new EFSYSService<SYS_DBConnection>();
            List<SYS_DBConnection> dblist = context.Get(d => d.Building == "B17");
            return dblist;
        }
        /// <summary>
        /// 获取矩阵连接
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="option"></param>
        /// <param name="selectedValue"></param>
        /// <returns></returns>
        /// 
        protected virtual List<SelectListItem> GetMatrix(Expression<Func<SYS_DBConnectionMatrixDetail, bool>> predicate, SysEnums.SelectOptions option, string selectedValue = "")
        {
            var obj = new SYS_DBConnectionMatrixDetailService().Get(predicate);

            List<SelectListItem> items = (from o in obj select new SelectListItem { Value = o.ConnCode, Text = o.Alias, Selected = (o.ConnCode == selectedValue) }).OrderBy(d => d.Text).Distinct().ToList();
            return InsertByOption(option, selectedValue, items);
        }
        /// <summary>
        /// 根据数据库连接的Code获取连接池信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetDbConn(string code)
        {
            return SDPSubSystem.Data.CacheData.DataCache.GetDbConn(code);
        }
        /// <summary>
        /// 根据条件获取字典下拉项
        /// </summary>
        /// <param name="option"></param>
        /// <param name="SelectedValue"></param>
        /// <param name="DicType"></param>
        /// <returns></returns>
        protected virtual List<SelectListItem> GetDictionary(Enums.SelectOptions option, string DicType, string constr, string selectedValue = "")
        {
            List<udtDictionary> list = new Services.DYEFService<udtDictionary>(constr).Get(d => d.DicType == DicType);
            List<SelectListItem> result = (from l in list select new SelectListItem { Value = l.DicValue, Text = l.DicText, Selected = (l.DicValue == selectedValue) }).ToList();
            return InsertOptions(result, option, selectedValue == "");
        }
        /// <summary>
        /// 为下拉列表插入首选项
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="option"></param>
        /// <param name="selected"></param>
        /// <returns></returns>
        protected virtual List<SelectListItem> InsertOptions(List<SelectListItem> obj, Enums.SelectOptions option, bool selected = true)
        {
            switch (option)
            {
                case Enums.SelectOptions.None:
                    break;
                case Enums.SelectOptions.InsertNull:
                    obj.Insert(0, new SelectListItem { Value = "", Text = "", Selected = selected });
                    break;
                case Enums.SelectOptions.InsertAll:
                    obj.Insert(0, new SelectListItem { Value = "", Text = "===All===", Selected = selected });
                    break;
                default: break;
            }
            return obj;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="option"></param>
        /// <param name="selectedValue"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        protected List<SelectListItem> InsertByOption(SysEnums.SelectOptions option, string selectedValue, List<SelectListItem> items)
        {
            switch (option)
            {
                case SysEnums.SelectOptions.InsertNull:
                    items.Insert(0, new SelectListItem { Value = "", Text = "", Selected = (selectedValue == "") });
                    break;
                case SysEnums.SelectOptions.InsertAll:
                    items.Insert(0, new SelectListItem { Value = "", Text = "===All===", Selected = (selectedValue == "") });
                    break;
                default: break;
            }
            return items;
            #endregion
        }

        /// <summary>
        /// 转换工号，刷卡机刷卡后转换，兼手输工号
        /// </summary>
        /// <param name="employeeNO"></param>
        /// <returns></returns>
        protected virtual string transferEmployeeNO(string employeeNO)
        {
            string ICCard_16 = Convert.ToInt64(employeeNO).ToString("X8");

            string ICCard_16Cross = "";
            if (ICCard_16.Length > 6)
            { //取反
                ICCard_16Cross = ICCard_16.Substring(6, 2) + ICCard_16.Substring(4, 2) + ICCard_16.Substring(2, 2) + ICCard_16.Substring(0, 2);
            }

            string sql = "select OutID,Name from BASE_CUSTOMERS where  (SCardSNR='{0}' OR OutID='{1}') ";
            sql = string.Format(sql, ICCard_16Cross, employeeNO);
            DataTable dt = _service.QueryBySql(sql, ConDB.DBKQXTContext).Tables[0];

            string result = "";
            if (dt.Rows[0]["OutID"] != null)
            {
                result = dt.Rows[0]["OutID"].ToString();
            }


            return result;
        }


    } 

    
}