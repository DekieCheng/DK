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
    public class AssetFlowController : BaseController
    {
        RawInputService _service = new RawInputService();
        
        public ActionResult PrintLabelIndex()
        {
            return View();
        }


        public ActionResult Print(string SN)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("SN", SN);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            _service.RunProcedure("udpmanageAMSLabel", Sqlparam, "bb", ConDB.AMSDBContext);
            String Zpl = Sqlparam[1].Value.ToString();

            return JsonFormat(new { zpl = Zpl });
        }

        public ActionResult CheckInIndex()
        {
            return View();
        }

        public ActionResult getDetailDropForProject()
        {
            string dbstr = Request.Params["X_DB_MATRIX"];
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "GetDetailDropForProject");
            //dic.Add("ID", ID);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspAssetFlowProjectMaintenance", Sqlparam, "bb", ConDB.AMSDBContext);

            //ViewData["PartID"] = DBToSelectList(ds.Tables[2]);
            //ViewData["OutputStationType"] = DBToSelectList(ds.Tables[3]);

            return JsonFormat(new { Project = ds.Tables[0]}, "yyyyMMdd");
        }

        public ActionResult SaveCheckInFlow(string poststr)
        {
            List<string> errMsg = new List<string>();
            try
            {
                string dbstr = Request.Params["X_DB_MATRIX"];
                int code = 0;
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, object> dic = jss.Deserialize<Dictionary<string, object>>(poststr);
                string sqlxml = XmlParams.CreateSqlXml(dic);
                string errormsg = _service.RunProcedure("udpmanageAMSInventoryFlow", sqlxml, out code, ConDB.AMSDBContext);

                if (code == 0)
                {
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

        public ActionResult CheckOutIndex()
        {
            return View();
        }

        public ActionResult InOutHistoryReportIndex()
        {
            return View();
        }

        public ActionResult InOutHistoryReportload(string dateFrom, string dateTo, string WorkID, string EquipmentID,string type)
        {
            int start = Convert.ToInt32(Request.Params["start"]);//起始数
            int rows = Convert.ToInt32(Request.Params["length"]);//长度
            int draw = Convert.ToInt32(Request.Params["draw"]);//画的页数
            string dbstr = Request.Params["X_DB_MATRIX"];
            int total;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("PageSize", rows);
            //int PageIndex = start / rows + 1;
            dic.Add("PageIndex", start / rows + 1);
            dic.Add("dateFrom", dateFrom);
            dic.Add("dateTo", dateTo);
            dic.Add("WorkID", WorkID);
            dic.Add("EquipmentID", EquipmentID);
            dic.Add("type", type);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspManageInOutHistoryReport", Sqlparam, "bb", ConDB.AMSDBContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            //var result = xs.ConvertTo<vShippingNotice>(ds.Tables[1]);
            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }

        public ActionResult StoredReportIndex()
        {
            return View();
        }

        public ActionResult StoredReportload()
        {
            int start = Convert.ToInt32(Request.Params["start"]);//起始数
            int rows = Convert.ToInt32(Request.Params["length"]);//长度
            int draw = Convert.ToInt32(Request.Params["draw"]);//画的页数
            string dbstr = Request.Params["X_DB_MATRIX"];
            int total;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("PageSize", rows);
            //int PageIndex = start / rows + 1;
            dic.Add("PageIndex", start / rows + 1);
            
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspManageStoredReport", Sqlparam, "bb", ConDB.AMSDBContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            //var result = xs.ConvertTo<vShippingNotice>(ds.Tables[1]);
            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }

        #region Matrix Project Data Maintenance
        public ActionResult ProjectIndex()
        {
            return View();
        }

        public ActionResult Projectload(string code)
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
            dic.Add("Flag", "Get");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspAssetFlowProjectMaintenance", Sqlparam, "bb", ConDB.AMSDBContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            //var result = xs.ConvertTo<vShippingNotice>(ds.Tables[1]);
            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }

        public ActionResult ProjectAdd()
        {
            return View();
        }

        public ActionResult ProjectSaveAdd(string poststr)
        {
            List<string> errMsg = new List<string>();
            try
            {
                string dbstr = Request.Params["X_DB_MATRIX"];
                int code = 0;
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, object> dic = jss.Deserialize<Dictionary<string, object>>(poststr);

                dic.Add("Flag", "AddSave");
                dic["UserName"] = _service.CurrentUser.Account;
                string sqlxml = XmlParams.CreateSqlXml(dic);
                string errormsg = _service.RunProcedure("uspAssetFlowProjectMaintenance", sqlxml, out code, ConDB.AMSDBContext);

                if (code == 0)
                {
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

        public ActionResult ProjectEdit(int ID)
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

            DataSet ds = _service.RunProcedure("uspAssetFlowProjectMaintenance", Sqlparam, "bb", ConDB.AMSDBContext);

            ViewData["ID"] = ds.Tables[0].Rows[0][0];
            ViewData["Name"] = ds.Tables[0].Rows[0][1];
            return View();
        }

        public ActionResult ProjectSaveEdit(string poststr)
        {
            List<string> errMsg = new List<string>();
            try
            {
                string dbstr = Request.Params["X_DB_MATRIX"];
                int code = 0;
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, object> dic = jss.Deserialize<Dictionary<string, object>>(poststr);
                dic.Add("Flag", "EditSave");
                dic["UserName"] = _service.CurrentUser.Account;
                ///dic.Add("IDS", ids);
                string sqlxml = XmlParams.CreateSqlXml(dic);
                //DataTable dt = _service.GetSelectItems(XmlParams.CreateSqlXml(dic), Request.Params["X_DB_MATRIX"]);
                string errormsg = _service.RunProcedure("uspAssetFlowProjectMaintenance", sqlxml, out code, ConDB.AMSDBContext);

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

        public ActionResult ProjectDelete(string ids)
        {
            List<string> errMsg = new List<string>();
            try
            {
                string dbstr = Request.Params["X_DB_MATRIX"];
                int code = 0;
                //JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Flag", "Delete");
                dic.Add("IDS", ids);
                dic["UserName"] = _service.CurrentUser.Account;
                string sqlxml = XmlParams.CreateSqlXml(dic);
                //DataTable dt = _service.GetSelectItems(XmlParams.CreateSqlXml(dic), Request.Params["X_DB_MATRIX"]);
                string errormsg = _service.RunProcedure("uspAssetFlowProjectMaintenance", sqlxml, out code, ConDB.AMSDBContext);

                if (code == 0)
                {
                    ///return Json(new JsonData(JsonData.FLAG_SUCCESS, null, "OK"));
                    return Json(new JsonData(JsonData.FLAG_SUCCESS, null, "Delete OK"));
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


        #endregion


    }
}