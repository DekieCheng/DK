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
    public class RecreationMgntController : BaseController
    {
        RawInputService _service = new RawInputService();

        public ActionResult CheckOutIndex()
        {
            return View();
        }

        public ActionResult GetDropdownList()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "GetDropdownList");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspRecreationRegister", Sqlparam, "bb", ConDB.PPAppContext);

            return JsonFormat(new { EquipmentID = ds.Tables[0] }, "yyyyMMdd");
        }



        public ActionResult CheckOutSaveAdd(string poststr)
        {
            List<string> errMsg = new List<string>();
            try
            {
                string dbstr = Request.Params["X_DB_MATRIX"];
                int code = 0;
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, object> dic = jss.Deserialize<Dictionary<string, object>>(poststr);

                //取到刷卡工号后，转换
                dic["EmployeeID"] = transferEmployeeNO(dic["EmployeeID"].ToString());

                dic["UserID"] = _service.CurrentUser.EmployeeNO;
                dic.Add("Flag", "CheckOutSave");
                string sqlxml = XmlParams.CreateSqlXml(dic);
                string errormsg = _service.RunProcedure("uspRecreationRegister", sqlxml, out code, ConDB.PPAppContext);

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

        public ActionResult CheckOutLoad(string code)
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
            dic.Add("Code", code);
            dic.Add("Flag", "CheckOutLoad");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspRecreationRegister", Sqlparam, "bb", ConDB.PPAppContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }

        public ActionResult CheckInIndex()
        {
            return View();
        }
        public ActionResult CheckInSaveAdd(string poststr)
        {
            List<string> errMsg = new List<string>();
            try
            {
                string dbstr = Request.Params["X_DB_MATRIX"];
                int code = 0;
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, object> dic = jss.Deserialize<Dictionary<string, object>>(poststr);

                //取到刷卡工号后，转换
                dic["EmployeeID"] = transferEmployeeNO(dic["EmployeeID"].ToString());

                dic["UserID"] = _service.CurrentUser.EmployeeNO;
                dic.Add("Flag", "CheckInSave");
                string sqlxml = XmlParams.CreateSqlXml(dic);
                string errormsg = _service.RunProcedure("uspRecreationRegister", sqlxml, out code, ConDB.PPAppContext);

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


        public ActionResult CheckInLoad(string code)
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
            dic.Add("Code", code);
            dic.Add("Flag", "CheckInLoad");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspRecreationRegister", Sqlparam, "bb", ConDB.PPAppContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }

        public ActionResult DashboardIndex()
        {
            return View();
        }

        public ActionResult GetDashboardData()
        {
            IDataParameter[] sqlparameter =
            {

            };
            DataSet ds = _service.RunProcedure("uspRecreationDashboard", sqlparameter, "bb", ConDB.PPAppContext);


            return JsonFormat(new { DashboardData = ds.Tables[0] }, "yyyyMMdd");
        }

        public ActionResult RegisterHistory()
        {
            return View();
        }


        public ActionResult InOutHistoryReportload()
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
            //dic.Add("Code", code);
            dic.Add("Flag", "Load");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspRecreationHistoryDashboard", Sqlparam, "bb", ConDB.PPAppContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }

        public ActionResult Report()
        {
            return View();
        }

        public ActionResult GetReportData()
        {
            IDataParameter[] sqlparameter =
            {

            };
            DataSet ds = _service.RunProcedure("uspRecreationMonthlyReport", sqlparameter, "bb", ConDB.PPAppContext);


            return JsonFormat(new { Report = ds.Tables[0] }, "yyyyMMdd");
        }

    }
}