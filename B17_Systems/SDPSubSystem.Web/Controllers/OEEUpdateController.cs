using SDPSubSystem.Common.Opt;
using SDPSubSystem.Data;
using SDPSubSystem.Model.SysModels;
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
using System.Web.Script.Serialization;
using SDPSubSystem.Services;
using SDPSubSystem.Model.ChangeOver;
using System.IO;
using SDPSubSystem.Common.Customized;

namespace SDPSubSystem.Web.Controllers
{
    public class OEEUpdateController : BaseController
    {
        RawInputService _service = new RawInputService();


        public ActionResult Index()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "GetDropdownList");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("udpOEEUpdateManagement", Sqlparam, "bb", ConDB.DWHDBContext);
            ViewData["Line"] = DBToSelectList(ds.Tables[0]);
            return View();
        }

        public ActionResult loadData(string planDate,string LineID)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("PlanDate", planDate);
            dic.Add("Line", LineID);
            dic.Add("Flag", "Get");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;
            DataSet ds = _service.RunProcedure("udpOEEUpdateManagement", Sqlparam, "bb", ConDB.DWHDBContext);


            return JsonFormat(new { Data = ds.Tables[1] }, "yyyyMMdd");
        }

        
        public ActionResult Edit(int ID)
        {
            string dbstr = Request.Params["X_DB_MATRIX"];
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "GetEdit");
            dic.Add("ID", ID);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("udpOEEUpdateManagement", Sqlparam, "bb", ConDB.DWHDBContext);
            ViewData["ID"] = ds.Tables[0].Rows[0]["ID"];
            ViewData["PlanDate"] = ds.Tables[0].Rows[0]["PlanDate"];
            ViewData["Line"] = ds.Tables[0].Rows[0]["Line"];
            ViewData["WO#"] = ds.Tables[0].Rows[0]["WO#"];
            ViewData["P_N"] = ds.Tables[0].Rows[0]["P_N"];
            ViewData["Sort_ID"] = ds.Tables[0].Rows[0]["Sort_ID"];
            ViewData["Plan_Qty"] = ds.Tables[0].Rows[0]["Plan_Qty"];
            ViewData["Act_Qty"] = ds.Tables[0].Rows[0]["Act_Qty"];
            ViewData["Status"] = ds.Tables[0].Rows[0]["Status"];
            ViewData["PSID"] = ds.Tables[0].Rows[0]["PSID"];

            return View();
        }
        public ActionResult SaveEdit(string poststr)
        {
            List<string> errMsg = new List<string>();
            try
            {
                string dbstr = Request.Params["X_DB_MATRIX"];
                int code = 0;
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, object> dic = jss.Deserialize<Dictionary<string, object>>(poststr);
                dic["UserName"] = _service.CurrentUser.Account;
                dic.Add("Flag", "EditSave");
                ///dic.Add("IDS", ids);
                string sqlxml = XmlParams.CreateSqlXml(dic);
                //DataTable dt = _service.GetSelectItems(XmlParams.CreateSqlXml(dic), Request.Params["X_DB_MATRIX"]);
                string errormsg = _service.RunProcedure("udpOEEUpdateManagement", sqlxml, out code, ConDB.DWHDBContext);

                if (code == 0)
                {
                    ///return Json(new JsonData(JsonData.FLAG_SUCCESS, null, "OK"));
                    return Json(new JsonData(JsonData.FLAG_SUCCESS, null, "Save OK"));
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
        



    }
}