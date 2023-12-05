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

namespace SDPSubSystem.Web.Controllers
{
    public class ECTrackerController : BaseController
    {

        RawInputService _service = new RawInputService();
        // GET: Master
        public ActionResult Index()
        {
            string dbstr = Request.Params["X_DB_MATRIX"];
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "GetDropDownList");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspECOTracker", Sqlparam, "bb", ConDB.PlataReportContext);

            ViewData["Status"] = DBToSelectList(ds.Tables[0]);

            string Project = Request.Params["Project"];

            ViewData["CustomerProject"] = DBToSelectListName(ds.Tables[1],Project);

            string Month = Request.Params["Month"];
            string Year = Request.Params["Year"];

            ViewData["Month"] = Month;
            ViewData["Year"] = Year;

            return View();
        }



        public ActionResult Load(string GNECO, string FlexECO, string Status,string CustomerProject,string Month,string Year)
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
            dic.Add("GNECO", GNECO);
            dic.Add("FlexECO", FlexECO);
            dic.Add("StatusID", Status);
            dic.Add("CustomerProject", CustomerProject);
            dic.Add("Month", Month);
            dic.Add("Year", Year);
            dic.Add("Flag", "Get");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspECOTracker", Sqlparam, "bb", ConDB.PlataReportContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            //var result = xs.ConvertTo<vShippingNotice>(ds.Tables[1]);
            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }

        /**
         * 
         * 可调用回调函数的导出方式
         */
        public ActionResult exportData(string GNECO, string FlexECO, string Status, string CustomerProject)
        {
            string dbstr = Request.Params["X_DB_MATRIX"];
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("GNECO", GNECO);
            dic.Add("FlexECO", FlexECO);
            dic.Add("StatusID", Status);
            dic.Add("CustomerProject", CustomerProject);
            dic.Add("Flag", "GetExportList");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspECOTracker", Sqlparam, "bb", ConDB.PlataReportContext);


            MemoryStream ms = ExcelOpt.TableToExcelXlsx(ds.Tables[0] == null ? new DataTable() : ds.Tables[0]);

            HttpContext.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Response.AddHeader("Content-Type", "application/octet-stream");
            HttpContext.Response.AddHeader("Set-Cookie", "fileDownload=true; path=/");
            HttpContext.Response.Clear();
            HttpContext.Response.Buffer = true;
            HttpContext.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("ECOData.xlsx", System.Text.Encoding.UTF8));
            HttpContext.Response.Charset = "utf-8";
            HttpContext.Response.BinaryWrite(ms.ToArray());
            HttpContext.Response.Flush();
            HttpContext.Response.Clear();
            HttpContext.Response.End();
            return null;

            //return File(ms, "application/vnd.ms-excel", Week+"_MPS.xlsx");

        }

        public ActionResult LoadAffectedItems(string AgileHdrID)
        {
            string dbstr = Request.Params["X_DB_MATRIX"];
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Code", AgileHdrID);
            dic.Add("Flag", "GetAffectedItems");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspECOTracker", Sqlparam, "bb", ConDB.PlataReportContext);

            var result = ds.Tables[0];
            return JsonFormat(new { Items = result }, "yyyyMMdd");
        }


        public ActionResult Import()
        {
            //mosdataService<MosCostCenter> mds = new mosdataService<MosCostCenter>();
            string errormsg = string.Empty;
            try
            {
                var files = Request.Files;
                if (files != null && files.Count > 0)
                {
                    var path = HttpContext.Server.MapPath("~/upload/" + DateTime.Today.ToString("yyyyMM") + "/ECTracker/");
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
                    errormsg = _service.RunProcedure("uspECOTrackerImport", sqlxml, out code, ConDB.PlataReportContext);
                    if (code == 0)
                    {
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

            DataSet ds = _service.RunProcedure("uspECOTracker", Sqlparam, "bb", ConDB.PlataReportContext);
            udtECOtracker am = new udtECOtracker
            {
                ID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString()),
                GNECO = ds.Tables[0].Rows[0][1],
                FlexECO = ds.Tables[0].Rows[0][2],
                ECType = ds.Tables[0].Rows[0][3],
                ChangeType = ds.Tables[0].Rows[0][4],
                ECReceivedDate = ds.Tables[0].Rows[0][5],
                CustomerProject = ds.Tables[0].Rows[0][6],
                ECDescription = ds.Tables[0].Rows[0][7],
                EstimateImplementationDate = ds.Tables[0].Rows[0][8],
                ActualImplementationDate = ds.Tables[0].Rows[0][9],
                Status = ds.Tables[0].Rows[0][10],
                Owner = ds.Tables[0].Rows[0][11],
            };


            //ViewData["ECType"] = DBToSelectList(ds.Tables[1], "ECO");
            //ViewData["Status"] = DBToSelectList(ds.Tables[1],am.Status.ToString());
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
                ///dic.Add("IDS", ids);
                string sqlxml = XmlParams.CreateSqlXml(dic);
                //DataTable dt = _service.GetSelectItems(XmlParams.CreateSqlXml(dic), Request.Params["X_DB_MATRIX"]);
                string errormsg = _service.RunProcedure("uspECOTracker", sqlxml, out code, ConDB.PlataReportContext);

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
        
        public ActionResult GetCFTMatrixDropdownList()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "GetCFTMatrixDropdownList");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspECOTracker", Sqlparam, "bb", ConDB.PlataReportContext);

            return JsonFormat(new { CFT = ds.Tables[0] }, "yyyyMMdd");
        }

        public ActionResult GetYearDropdownList()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "GetYearDropdownList");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspECOTracker", Sqlparam, "bb", ConDB.PlataReportContext);

            return JsonFormat(new { YearDrop = ds.Tables[0] }, "yyyyMMdd");
        }

        public ActionResult CFTMatrix()
        {
            return View();
        }

        public ActionResult MatrixLoad(string code)
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

            DataSet ds = _service.RunProcedure("uspECOTrackerCFTMatrix", Sqlparam, "bb", ConDB.PlataReportContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            //var result = xs.ConvertTo<vShippingNotice>(ds.Tables[1]);
            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }

        public ActionResult CFTMatrixImport()
        {
            //mosdataService<MosCostCenter> mds = new mosdataService<MosCostCenter>();
            string errormsg = string.Empty;
            try
            {
                var files = Request.Files;
                if (files != null && files.Count > 0)
                {
                    var path = HttpContext.Server.MapPath("~/upload/" + DateTime.Today.ToString("yyyyMM") + "/ECTracker/");
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
                    errormsg = _service.RunProcedure("uspECOTrackerCFTMatrixImport", sqlxml, out code, ConDB.PlataReportContext);
                    if (code == 0)
                    {
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

        public ActionResult GetDashboardData(string year)
        {
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@Y", year),
            };

            DataSet ds = _service.RunProcedure("uspECODashBoard", Sqlparam, "bb", ConDB.PlataReportContext);

            return JsonFormat(new { ECO = ds.Tables[0] }, "yyyyMMdd");
        }
    }
}