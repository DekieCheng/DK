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
using SDPSubSystem.Model.SMTBackEndAutoKitting;
using System.IO;
using SDPSubSystem.Common.Customized;
using SDPSubSystem.Model.ECTracker;
using SDPSubSystem.Model.GlueTracker;

namespace SDPSubSystem.Web.Controllers
{
    public class GlueTrackerDysonController : BaseController
    {

        RawInputService _service = new RawInputService();
        // GET: Master
        public ActionResult MainIndex()
        {
            return View();
        }

        public ActionResult MainLoad(string code)
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
            dic.Add("Code", code);
            dic.Add("Flag", "Get");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspGlueTracker", Sqlparam, "bb", ConDB.DysonContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            //var result = xs.ConvertTo<vShippingNotice>(ds.Tables[1]);
            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }

        public ActionResult MainEdit(int ID)
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

            DataSet ds = _service.RunProcedure("uspGlueTracker", Sqlparam, "bb", ConDB.DysonContext);
            GlueTrack_GlueMainDyson am = new GlueTrack_GlueMainDyson
            {
                ID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString()),
                GluePartNumber = ds.Tables[0].Rows[0][1],
                PartDescription = ds.Tables[0].Rows[0][2],
                BaanUsage = ds.Tables[0].Rows[0][3],
                DevicePartNumber = ds.Tables[0].Rows[0][4],
                StationTypeName = ds.Tables[0].Rows[0][5],
                CurrentUsage = ds.Tables[0].Rows[0][6]
            };

            return View(am);
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
                dic.Add("Flag", "EditSave");
                dic.Add("UserID", _service.CurrentUser.Account);
                ///dic.Add("IDS", ids);
                string sqlxml = XmlParams.CreateSqlXml(dic);
                //DataTable dt = _service.GetSelectItems(XmlParams.CreateSqlXml(dic), Request.Params["X_DB_MATRIX"]);
                string errormsg = _service.RunProcedure("uspGlueTracker", sqlxml, out code, ConDB.DysonContext);

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

            DataSet ds = _service.RunProcedure("uspGlueTracker", Sqlparam, "bb", ConDB.DysonContext);

            return JsonFormat(new { StationTypeName = ds.Tables[0] }, "yyyyMMdd");
        }

        public ActionResult ColorIndex()
        {
            return View();
        }

        public ActionResult ColorLoad(string code)
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
            dic.Add("Code", code);
            dic.Add("Flag", "Get");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspGlueTrack_DashboardColorDefinition", Sqlparam, "bb", ConDB.DysonContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            //var result = xs.ConvertTo<vShippingNotice>(ds.Tables[1]);
            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }

        public ActionResult ColorEdit(string ID, string ProjectName)
        {
            string dbstr = Request.Params["X_DB_MATRIX"];
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "GetEdit");
            dic.Add("GluePartNumber", ID);
            dic.Add("ProjectName", ProjectName);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspGlueTrack_DashboardColorDefinition", Sqlparam, "bb", ConDB.DysonContext);
            GlueTrack_DashboardColorDefinition am = new GlueTrack_DashboardColorDefinition
            {
                GluePartNumber = ds.Tables[0].Rows[0][0],
                ProjectName = ds.Tables[0].Rows[0][1],
                Red = ds.Tables[0].Rows[0][2],
                Orange = ds.Tables[0].Rows[0][3],
            };

            return View(am);
        }

        public ActionResult ColorSaveEdit(string poststr)
        {
            List<string> errMsg = new List<string>();
            try
            {
                string dbstr = Request.Params["X_DB_MATRIX"];
                int code = 0;
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, object> dic = jss.Deserialize<Dictionary<string, object>>(poststr);
                dic.Add("Flag", "EditSave");
                dic.Add("UserID", _service.CurrentUser.Account);
                ///dic.Add("IDS", ids);
                string sqlxml = XmlParams.CreateSqlXml(dic);
                //DataTable dt = _service.GetSelectItems(XmlParams.CreateSqlXml(dic), Request.Params["X_DB_MATRIX"]);
                string errormsg = _service.RunProcedure("uspGlueTrack_DashboardColorDefinition", sqlxml, out code, ConDB.DysonContext);

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



        public ActionResult MaintenanceIndex()
        {
            return View();
        }

        public ActionResult MaintenanceLoad(string code)
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
            dic.Add("Code", code);
            dic.Add("Flag", "Get");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspGlueTrack_ProductStructure", Sqlparam, "bb", ConDB.DysonContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            //var result = xs.ConvertTo<vShippingNotice>(ds.Tables[1]);
            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }

        public ActionResult MaintenanceEdit(int ID)
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

            DataSet ds = _service.RunProcedure("uspGlueTrack_ProductStructure", Sqlparam, "bb", ConDB.DysonContext);
            GlueTrack_ProductStructure am = new GlueTrack_ProductStructure
            {
                ID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString()),
                GluePartNumber = ds.Tables[0].Rows[0][1],
                ProjectName = ds.Tables[0].Rows[0][2],
                DevicePartNumber = ds.Tables[0].Rows[0][3],
                IsActive = ds.Tables[0].Rows[0][4],
            };

            return View(am);
        }

        public ActionResult MaintenanceSaveEdit(string poststr)
        {
            List<string> errMsg = new List<string>();
            try
            {
                string dbstr = Request.Params["X_DB_MATRIX"];
                int code = 0;
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, object> dic = jss.Deserialize<Dictionary<string, object>>(poststr);
                dic.Add("Flag", "EditSave");
                dic.Add("UserID", _service.CurrentUser.Account);
                ///dic.Add("IDS", ids);
                string sqlxml = XmlParams.CreateSqlXml(dic);
                //DataTable dt = _service.GetSelectItems(XmlParams.CreateSqlXml(dic), Request.Params["X_DB_MATRIX"]);
                string errormsg = _service.RunProcedure("uspGlueTrack_ProductStructure", sqlxml, out code, ConDB.DysonContext);


                if (code == 0)
                {
                    //更新料号的时候，同步数据
                    SyncBaanData_Glue_Dyson();

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

        public ActionResult MaintenanceAdd()
        {
            return View();
        }

        public ActionResult MaintenanceSaveAdd(string poststr)
        {
            List<string> errMsg = new List<string>();
            try
            {
                string dbstr = Request.Params["X_DB_MATRIX"];
                int code = 0;
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, object> dic = jss.Deserialize<Dictionary<string, object>>(poststr);
                dic.Add("Flag", "AddSave");
                dic.Add("UserID", _service.CurrentUser.Account);
                ///dic.Add("IDS", ids);
                string sqlxml = XmlParams.CreateSqlXml(dic);
                //DataTable dt = _service.GetSelectItems(XmlParams.CreateSqlXml(dic), Request.Params["X_DB_MATRIX"]);
                string errormsg = _service.RunProcedure("uspGlueTrack_ProductStructure", sqlxml, out code, ConDB.DysonContext);

                if (code == 0)
                {
                    //新增料号的时候，同步数据
                    SyncBaanData_Glue_Dyson();

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

        public ActionResult MaintenanceImport()
        {
            //mosdataService<MosCostCenter> mds = new mosdataService<MosCostCenter>();
            string errormsg = string.Empty;
            try
            {
                var files = Request.Files;
                if (files != null && files.Count > 0)
                {
                    var path = HttpContext.Server.MapPath("~/upload/" + DateTime.Today.ToString("yyyyMM") + "/GlueTracker/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string fileName = Path.GetFileName(files[0].FileName);
                    string saveFileName = Path.GetFileNameWithoutExtension(files[0].FileName) + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(files[0].FileName);
                    saveFileName = Path.Combine(path, saveFileName);
                    files[0].SaveAs(saveFileName);
                    Dictionary<string, object> dic = _service.GetUserInfoDic();
                    string sqlxml = ExcelToAll.ExcelToXml(saveFileName, dic);
                    //DataTable dt = ExcelToAll.ExcelToDataTable(saveFileName, dic);

                    int code = 0;
                    errormsg = _service.RunProcedure("uspGlueTrack_ProductStructureImport", sqlxml, out code, ConDB.DysonContext);
                    if (code == 0)
                    {
                        //导入的时候，同步数据
                        SyncBaanData_Glue_Dyson();

                        return JsonFormat(new JsonData(JsonData.FLAG_SUCCESS, null, errormsg));
                    }
                }
            }
            catch (Exception ex)
            {
                errormsg = ex.Message;
            }
            return JsonFormat(new JsonData(JsonData.FLAG_ERROR, null, errormsg));
        }



        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult GetDashboardData()
        {
            IDataParameter[] Sqlparam =
            {

            };

            DataSet ds = _service.RunProcedure("udpGetGlueUsageData", Sqlparam, "bb", ConDB.DysonContext);

            return JsonFormat(new { GlueUsageColumn = ds.Tables[0], GlueUsage = ds.Tables[1] }, "yyyyMMdd");
        }

        public ActionResult History()
        {
            return View();
        }

        public ActionResult GetHistoryData()
        {
            IDataParameter[] Sqlparam =
            {

            };

            DataSet ds = _service.RunProcedure("uspGlueTrackerChangeHistoryDashboard", Sqlparam, "bb", ConDB.DysonContext);

            return JsonFormat(new { Glue = ds.Tables[0] }, "yyyyMMdd");
        }
    }
}