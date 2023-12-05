using SDPSubSystem.Common.Opt;
using SDPSubSystem.Data;
using SDPSubSystem.Model.SysModels;
using SDPSubSystem.Model.Vmodels;
using SDPSubSystem.Services.kanban;
using SDPSubSystem.Services.SysServices;
using SDPSubSystem.Web.Filter;
using SDPSubSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SDPSubSystem.Web.vmodels;
using System.Web.Script.Serialization;
using SDPSubSystem.Services;
using SDPSubSystem.Model.ChangeOver;
using SDPSubSystem.Model.SMTBackEndAutoKitting;
using System.IO;
using SDPSubSystem.Common.Customized;

namespace SDPSubSystem.Web.Controllers
{
    public class EPPSController : BaseController
    {
        RawInputService _service = new RawInputService();
        
        public ActionResult DashboardIndex()
        {
            return View();
        }


        public ActionResult Dashboardload()
        {
            int start = Convert.ToInt32(Request.Params["start"]);//起始数
            int rows = Convert.ToInt32(Request.Params["length"]);//长度
            int draw = Convert.ToInt32(Request.Params["draw"]);//画的页数
            string dbstr = Request.Params["X_DB_MATRIX"];
            //int total;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            //dic.Add("PageSize", rows);
            //int PageIndex = start / rows + 1;
            //dic.Add("PageIndex", start / rows + 1);
            IDataParameter[] Sqlparam =
            {
                
            };

            DataSet ds = _service.RunProcedure("udpGetCOAlarm", Sqlparam, "bb", ConDB.DWHDBContext);

            //total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            //var result = xs.ConvertTo<vShippingNotice>(ds.Tables[1]);
            var result = ds.Tables[0];
            return JsonFormat(new DataTablesResultInfo(result, draw, 99999999, 99999999));
        }

        public ActionResult Operate(string PSID)
        {
            List<string> errMsg = new List<string>();
            try
            {
                string dbstr = Request.Params["X_DB_MATRIX"];
                int code = 0;
                Dictionary<string, object> dic = new Dictionary<string, object>();

                dic["Operator"] = _service.CurrentUser.Account;

                dic.Add("PSID", PSID);
                //dic.Add("IDS", ids);
                string sqlxml = XmlParams.CreateSqlXml(dic);
                string errormsg = _service.RunProcedure("uspManageLatestLineState", sqlxml, out code, ConDB.DWHDBContext);

                if (code == 0)
                {
                    ///return Json(new JsonData(JsonData.FLAG_SUCCESS, null, "OK"));
                    return Json(new JsonData(JsonData.FLAG_SUCCESS, null, "Add OK"));
                }
                else
                {
                    errMsg.Add(errormsg);
                    return Json(new JsonData(JsonData.FLAG_ERROR, null, errMsg));
                }
            }
            catch (Exception ex)
            {
                errMsg.Add(ex.Message);
            }
            return Json(new JsonData(JsonData.FLAG_ERROR, null, errMsg));
        }

        public ActionResult WOAgingDashboardByLine()
        {
            string linename = Request.QueryString["Line"].ToString();
            ViewData["Line"] = linename;
            return View();
        }

        public ActionResult GetSMTWOAgingDatabyLine(string Line)
        {
            IDataParameter[] sqlparameter1 =
            {
                 new SqlParameter("@Line", Line)
            };
            DataSet ds1 = _service.RunProcedure("udpRGetCurrWOAgings", sqlparameter1, "bb", ConDB.DWHDBContext);
            return JsonFormat(new { aging = ds1.Tables[0] }, "yyyyMMdd");
        }

        public ActionResult ePPSTriggerDashboardByLine()
        {
            string linename = Request.QueryString["Line"].ToString();
            ViewData["Line"] = linename;
            return View();
        }

        public ActionResult GetePPSTriggerDatabyLine(string Line)
        {
            int start = Convert.ToInt32(Request.Params["start"]);//起始数
            int rows = Convert.ToInt32(Request.Params["length"]);//长度
            int draw = Convert.ToInt32(Request.Params["draw"]);//画的页数
            string dbstr = Request.Params["X_DB_MATRIX"];
            int total;
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("PageSize", rows);
            //int PageIndex = start / rows + 1;
            dic.Add("PageIndex", start / rows + 1);
            dic.Add("Line", Line);
            dic.Add("Flag", "Get");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("udp_GetePPSTriggerStatus", Sqlparam, "bb", ConDB.DWHDBContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }

        public ActionResult PullingListIndex()
        {
            IDataParameter[] Sqlparam =
            {
                
            };

            DataSet ds = _service.RunProcedure("udpRGetDBList", Sqlparam, "bb", ConDB.DWHDBContext);
            ViewData["DB"] = DBToSelectList_noID(ds.Tables[0]);

            
            IDataParameter[] Sqlparam1 =
            {
                
            };

            DataSet ds1 = _service.RunProcedure("udpRGetLineList", Sqlparam, "bb", ConDB.DWHDBContext);
            ViewData["Line"] = DBToSelectList_noID(ds1.Tables[0]);
            return View();
        }

        public ActionResult GetDropDownList(string db,string line)
        {
            IDataParameter[] sqlparameter =
            {
               new SqlParameter("@DB",db),
               new SqlParameter("@Line",line)
            };

            DataSet ds = _service.RunProcedure("udpRGetPLList", sqlparameter, "bb", ConDB.DWHDBContext);

            return JsonFormat(new { PLName = DBToList_noID(ds.Tables[0]) }, "yyyyMMdd") ;
        }


        public ActionResult PullingListload(string PLName)
        {
            int start = Convert.ToInt32(Request.Params["start"]);//起始数
            int rows = Convert.ToInt32(Request.Params["length"]);//长度
            int draw = Convert.ToInt32(Request.Params["draw"]);//画的页数
            //string dbstr = Request.Params["X_DB_MATRIX"];
            int total;
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("PageSize", rows);
            //int PageIndex = start / rows + 1;
            dic.Add("PageIndex", start / rows + 1);
            dic.Add("PLName", PLName);
            //dic.Add("Flag", "Get");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("udpRGetRPLPLDetail", Sqlparam, "bb", ConDB.DWHDBContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            //var result = xs.ConvertTo<vShippingNotice>(ds.Tables[1]);
            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }

    }
}