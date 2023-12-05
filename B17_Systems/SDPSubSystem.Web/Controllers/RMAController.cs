using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SDPSubSystem.Common.Customized;
using SDPSubSystem.Common.Opt;
using SDPSubSystem.Data;
using SDPSubSystem.Services;
using SDPSubSystem.Web.Models;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;

namespace SDPSubSystem.Web.Controllers
{
    public class RMAController : BaseController
    {
        RawInputService _service = new RawInputService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getDetailDropForCustomer()
        {
            string dbstr = Request.Params["X_DB_MATRIX"];
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "GetCustomer");
            //dic.Add("ID", ID);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspManageRMADetailDrop", Sqlparam, "bb", ConDB.DashboardContext);

            return JsonFormat(new { Customer = ds.Tables[0] }, "yyyyMMdd");
        }

        public ActionResult Import()
        {
            //mosdataService<MosCostCenter> mds = new mosdataService<MosCostCenter>();
            string errormsg = string.Empty;
            try
            {
                var files = Request.Files;
                var Customer = Request.Params["Customer"];
                if (files != null && files.Count > 0)
                {
                    var path = HttpContext.Server.MapPath("~/upload/RMA/" + DateTime.Today.ToString("yyyyMM") + "/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string fileName = Path.GetFileName(files[0].FileName);
                    string saveFileName = Path.GetFileNameWithoutExtension(files[0].FileName) + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(files[0].FileName);
                    saveFileName = Path.Combine(path, saveFileName);
                    files[0].SaveAs(saveFileName);
                    Dictionary<string, object> dic = _service.GetUserInfoDic();
                    dic["UserName"] = _service.CurrentUser.Account;
                    dic.Add("Customer", Customer);
                    dic.Add("FileName", fileName);
                    string sqlxml = ExcelToAll.ExcelToXmlWithAll(saveFileName, dic);
                    //DataTable dt = ExcelToAll.ExcelToDataTable(saveFileName, dic);

                    int code = 0;
                    errormsg = _service.RunProcedure("uspImportRMARecieveFileMasterData", sqlxml, out code, ConDB.DashboardContext);
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

        public ActionResult ImportLoad(string code)
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

            DataSet ds = _service.RunProcedure("uspRMAManage", Sqlparam, "bb", ConDB.DashboardContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            //var result = xs.ConvertTo<vShippingNotice>(ds.Tables[1]);
            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }

        /**
         * 可调用回调函数的导出方式
         */
        public ActionResult templateDownload(string Code)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("~/Template");

            path += "\\RMA_System_B17_ImportTemplate.xlsx";
            MemoryStream ms = ExcelOpt.GetExportFile_RMATemplate(path);

            HttpContext.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Response.AddHeader("Content-Type", "application/octet-stream");
            HttpContext.Response.AddHeader("Set-Cookie", "fileDownload=true; path=/");
            HttpContext.Response.Clear();
            HttpContext.Response.Buffer = true;
            HttpContext.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("RMATemplate.xlsx", System.Text.Encoding.UTF8));
            HttpContext.Response.Charset = "utf-8";
            HttpContext.Response.BinaryWrite(ms.ToArray());
            HttpContext.Response.Flush();
            HttpContext.Response.Clear();
            HttpContext.Response.End();
            return null;

        }

        public ActionResult ReImportUpdate()
        {
            return View();
        }

        public ActionResult getDetailDropForFileName()
        {
            string dbstr = Request.Params["X_DB_MATRIX"];
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "GetFileName");
            //dic.Add("ID", ID);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspManageRMADetailDrop", Sqlparam, "bb", ConDB.DashboardContext);

            return JsonFormat(new { FileName = ds.Tables[0] }, "yyyyMMdd");
        }

        public ActionResult exportInitialData(string code)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("PageSize", 99999999);
            //int PageIndex = start / rows + 1;
            dic.Add("PageIndex", 1);
            dic.Add("Code", code);
            dic.Add("Flag", "Get");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspRMAManage", Sqlparam, "bb", ConDB.DashboardContext);


            MemoryStream ms = ExcelOpt.TableToExcelXlsx(ds.Tables[1] == null ? new DataTable() : ds.Tables[1]);

            HttpContext.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Response.AddHeader("Content-Type", "application/octet-stream");
            HttpContext.Response.AddHeader("Set-Cookie", "fileDownload=true; path=/");
            HttpContext.Response.Clear();
            HttpContext.Response.Buffer = true;
            HttpContext.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(code + ".xlsx", System.Text.Encoding.UTF8));
            HttpContext.Response.Charset = "utf-8";
            HttpContext.Response.BinaryWrite(ms.ToArray());
            HttpContext.Response.Flush();
            HttpContext.Response.Clear();
            HttpContext.Response.End();
            return null;

            //return File(ms, "application/vnd.ms-excel", Week+"_MPS.xlsx");

        }

        public ActionResult ReImport()
        {
            //mosdataService<MosCostCenter> mds = new mosdataService<MosCostCenter>();
            string errormsg = string.Empty;
            try
            {
                var files = Request.Files;
                var Customer = Request.Params["Customer"];
                if (files != null && files.Count > 0)
                {
                    var path = HttpContext.Server.MapPath("~/upload/RMA/" + DateTime.Today.ToString("yyyyMM") + "/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string fileName = Path.GetFileName(files[0].FileName);
                    string saveFileName = Path.GetFileNameWithoutExtension(files[0].FileName) + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(files[0].FileName);
                    saveFileName = Path.Combine(path, saveFileName);
                    files[0].SaveAs(saveFileName);
                    Dictionary<string, object> dic = _service.GetUserInfoDic();
                    dic["UserName"] = _service.CurrentUser.Account;
                    dic.Add("Customer", Customer);
                    dic.Add("FileName", fileName);
                    string sqlxml = ExcelToAll.ExcelToXmlWithAll(saveFileName, dic);
                    //DataTable dt = ExcelToAll.ExcelToDataTable(saveFileName, dic);

                    int code = 0;
                    errormsg = _service.RunProcedure("uspReImportRMARecieveFileMasterData", sqlxml, out code, ConDB.DashboardContext);
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

        public ActionResult Log()
        {
            return View();
        }

        public ActionResult getDetailDropForYear()
        {
            string dbstr = Request.Params["X_DB_MATRIX"];
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "GetYear");
            //dic.Add("ID", ID);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspManageRMADetailDrop", Sqlparam, "bb", ConDB.DashboardContext);

            return JsonFormat(new { Year = ds.Tables[0] }, "yyyyMMdd");
        }

        public ActionResult getDetailDropForMonth()
        {
            string dbstr = Request.Params["X_DB_MATRIX"];
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "GetMonth");
            //dic.Add("ID", ID);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspManageRMADetailDrop", Sqlparam, "bb", ConDB.DashboardContext);

            return JsonFormat(new { Month = ds.Tables[0] }, "yyyyMMdd");
        }

        public ActionResult LogLoad(string Customer,string Year)
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
            dic.Add("Customer", Customer);
            dic.Add("Year", Year);
            dic.Add("Flag", "Get");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspRMAManageLog", Sqlparam, "bb", ConDB.DashboardContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            //var result = xs.ConvertTo<vShippingNotice>(ds.Tables[1]);
            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }

        public ActionResult exportLogData(string Customer, string Year)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("PageSize", 99999999);
            //int PageIndex = start / rows + 1;
            dic.Add("PageIndex", 1);
            dic.Add("Customer", Customer);
            dic.Add("Year", Year);
            dic.Add("Flag", "Get");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspRMAManageLog", Sqlparam, "bb", ConDB.DashboardContext);


            string path = System.Web.HttpContext.Current.Server.MapPath("~/Template");

            path += "\\RMALog_DMN-PGM2005-00.xlsx";
            MemoryStream ms = ExcelOpt.GetExportFile_RMALog(path, ds, Customer,Year);

            string time = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();


            HttpContext.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Response.AddHeader("Content-Type", "application/octet-stream");
            HttpContext.Response.AddHeader("Set-Cookie", "fileDownload=true; path=/");
            HttpContext.Response.Clear();
            HttpContext.Response.Buffer = true;
            HttpContext.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("RMALog_"+ Customer +"_"+Year + "_" +time+ ".xlsx", System.Text.Encoding.UTF8));
            HttpContext.Response.Charset = "utf-8";
            HttpContext.Response.BinaryWrite(ms.ToArray());
            HttpContext.Response.Flush();
            HttpContext.Response.Clear();
            HttpContext.Response.End();
            return null;

            //return File(ms, "application/vnd.ms-excel", Week+"_MPS.xlsx");

        }

        public ActionResult Tracker()
        {
            return View();
        }

        public ActionResult getDetailDropForModel(string Customer)
        {
            string dbstr = Request.Params["X_DB_MATRIX"];
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "GetModel");
            dic.Add("Customer", Customer);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspManageRMADetailDrop", Sqlparam, "bb", ConDB.DashboardContext);

            return JsonFormat(new { Model = ds.Tables[0] }, "yyyyMMdd");
        }

        public ActionResult TrackerLoad(string dateFrom, string dateTo, string Customer, string Model)
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
            dic.Add("dateFrom", dateFrom);
            dic.Add("dateTo", dateTo);
            dic.Add("Customer", Customer);
            dic.Add("Model", Model);
            dic.Add("Flag", "Get");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspRMAManageTracker", Sqlparam, "bb", ConDB.DashboardContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            //var result = xs.ConvertTo<vShippingNotice>(ds.Tables[1]);
            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }

        public ActionResult exportTrackerData(string dateFrom, string dateTo, string Customer, string Model)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("PageSize", 99999999);
            //int PageIndex = start / rows + 1;
            dic.Add("PageIndex", 1);
            dic.Add("dateFrom", dateFrom);
            dic.Add("dateTo", dateTo);
            dic.Add("Customer", Customer);
            dic.Add("Model", Model);
            dic.Add("Flag", "Get");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspRMAManageTracker", Sqlparam, "bb", ConDB.DashboardContext);

            string time = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();

            MemoryStream ms = ExcelOpt.TableToExcelXlsx(ds.Tables[1] == null ? new DataTable() : ds.Tables[1]);

            HttpContext.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Response.AddHeader("Content-Type", "application/octet-stream");
            HttpContext.Response.AddHeader("Set-Cookie", "fileDownload=true; path=/");
            HttpContext.Response.Clear();
            HttpContext.Response.Buffer = true;
            HttpContext.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("RMAIssueTrackerList_" + time + ".xlsx", System.Text.Encoding.UTF8));
            HttpContext.Response.Charset = "utf-8";
            HttpContext.Response.BinaryWrite(ms.ToArray());
            HttpContext.Response.Flush();
            HttpContext.Response.Clear();
            HttpContext.Response.End();
            return null;

            //return File(ms, "application/vnd.ms-excel", Week+"_MPS.xlsx");

        }

        public ActionResult ImportUpdate()
        {
            //mosdataService<MosCostCenter> mds = new mosdataService<MosCostCenter>();
            string errormsg = string.Empty;
            try
            {
                var files = Request.Files;
                if (files != null && files.Count > 0)
                {
                    var path = HttpContext.Server.MapPath("~/upload/RMA/TrackerUpdate/" + DateTime.Today.ToString("yyyyMM") + "/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string fileName = Path.GetFileName(files[0].FileName);
                    string saveFileName = Path.GetFileNameWithoutExtension(files[0].FileName) + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(files[0].FileName);
                    saveFileName = Path.Combine(path, saveFileName);
                    files[0].SaveAs(saveFileName);

                    Dictionary<string, object> dic = _service.GetUserInfoDic();
                    dic["ExcelFilePath"] = saveFileName;
                    string sqlxml = ExcelToAll.ExcelToXmlWithAll(saveFileName, dic);

                    int code = 0;
                    errormsg = _service.RunProcedure("usp_RMATrackerImportFile_Update", sqlxml, out code, ConDB.DashboardContext);
                    //errormsg = _service.RunProcedure("uspWaterfallImport_json", jsonstr, out code, ConDB.DashboardContext);
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

        public ActionResult TrackerEdit(int ID)
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

            DataSet ds = _service.RunProcedure("uspRMAManageTracker", Sqlparam, "bb", ConDB.DashboardContext);
            ViewData["ID"] = ds.Tables[0].Rows[0][0];
            ViewData["BulkPN"] = ds.Tables[0].Rows[0][1];
            ViewData["RMANumber"] = ds.Tables[0].Rows[0][2];
            ViewData["FAResult"] = ds.Tables[0].Rows[0][3];
            ViewData["FAReportPath"] = ds.Tables[0].Rows[0][4];
            ViewData["RootCause"] = ds.Tables[0].Rows[0][5];
            ViewData["IssueCategory"] = ds.Tables[0].Rows[0][6];
            ViewData["ImproveAction"] = ds.Tables[0].Rows[0][7];
            ViewData["Owner"] = ds.Tables[0].Rows[0][8];
            ViewData["Status"] = ds.Tables[0].Rows[0][9];
            ViewData["CALink"] = ds.Tables[0].Rows[0][10];
            ViewData["SN"] = ds.Tables[0].Rows[0][11];

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
                string errormsg = _service.RunProcedure("uspRMAManageTracker", sqlxml, out code, ConDB.DashboardContext);

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

        public ActionResult UploadFile()
        {
            //mosdataService<MosCostCenter> mds = new mosdataService<MosCostCenter>();
            string errormsg = string.Empty;
            List<string> errMsg = new List<string>();
            try
            {
                var files = Request.Files;
                if (files != null && files.Count > 0)
                {
                    var path = HttpContext.Server.MapPath("~/upload/" + DateTime.Today.ToString("yyyyMM") + "/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string fileName = Path.GetFileName(files[0].FileName);
                    string saveFileName = Path.GetFileNameWithoutExtension(files[0].FileName) + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(files[0].FileName);
                    saveFileName = Path.Combine(path, saveFileName);
                    files[0].SaveAs(saveFileName);

                    return JsonFormat(new JsonData(saveFileName, null, files[0].FileName));
                }
            }
            catch (Exception ex)
            {
                errormsg = ex.Message;
            }
            return JsonFormat(new JsonData(JsonData.FLAG_ERROR, null, errormsg));
        }

        public void DownloadFile(string filePath)
        {

            //FileInfo file = new FileInfo(uploadDrawingPath);
            //HttpContext.Response.Charset = "GB2312";
            //HttpContext.Response.ContentEncoding = Encoding.GetEncoding("GB2312");
            //HttpContext.Response.ContentType = "application/ms-excel/msword";
            //HttpContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName));
            //HttpContext.Response.AddHeader("Content-length", file.Length.ToString());
            //HttpContext.Response.WriteFile(uploadDrawingPath);
            //HttpContext.Response.End();


            FileInfo DownloadFile = new FileInfo(filePath);
            if (DownloadFile.Exists)
            {
                Response.Clear();
                Response.ClearHeaders();
                Response.Buffer = false;
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(DownloadFile.Name, System.Text.Encoding.UTF8));
                Response.AppendHeader("Content-Length", DownloadFile.Length.ToString());
                Response.WriteFile(DownloadFile.FullName);
                Response.Flush();
                Response.End();
            }

        }

        public ActionResult Finance()
        {
            return View();
        }

        public ActionResult FinanceLoad(string dateFrom, string dateTo, string Customer, string Model)
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
            dic.Add("dateFrom", dateFrom);
            dic.Add("dateTo", dateTo);
            dic.Add("Customer", Customer);
            dic.Add("Model", Model);
            dic.Add("Flag", "Get");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspRMAManageFinance", Sqlparam, "bb", ConDB.DashboardContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            //var result = xs.ConvertTo<vShippingNotice>(ds.Tables[1]);
            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }

        public ActionResult exportFinanceData(string dateFrom, string dateTo, string Customer, string Model)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("PageSize", 99999999);
            //int PageIndex = start / rows + 1;
            dic.Add("PageIndex", 1);
            dic.Add("dateFrom", dateFrom);
            dic.Add("dateTo", dateTo);
            dic.Add("Customer", Customer);
            dic.Add("Model", Model);
            dic.Add("Flag", "Get");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspRMAManageFinance", Sqlparam, "bb", ConDB.DashboardContext);

            string path = System.Web.HttpContext.Current.Server.MapPath("~/Template");

            path += "\\RMAFinanceReportTemplate.xlsx";
            MemoryStream ms = ExcelOpt.GetExportFile_RMAFinance(path, ds);

            string time = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();


            HttpContext.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Response.AddHeader("Content-Type", "application/octet-stream");
            HttpContext.Response.AddHeader("Set-Cookie", "fileDownload=true; path=/");
            HttpContext.Response.Clear();
            HttpContext.Response.Buffer = true;
            HttpContext.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("RMAFinanceList " + time + ".xlsx", System.Text.Encoding.UTF8));
            HttpContext.Response.Charset = "utf-8";
            HttpContext.Response.BinaryWrite(ms.ToArray());
            HttpContext.Response.Flush();
            HttpContext.Response.Clear();
            HttpContext.Response.End();
            return null;

            //return File(ms, "application/vnd.ms-excel", Week+"_MPS.xlsx");

        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult DashboardLoad(string poststr)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, object> dic = jss.Deserialize<Dictionary<string, object>>(poststr);
            dic.Add("Flag", "Get");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspRMAManageDashboard", Sqlparam, "bb", ConDB.DashboardContext);

            return JsonFormat(new { Data = ds.Tables[0] }, "yyyyMMdd");
        }

        public ActionResult JumpIndex()
        {
            return View();
        }

        public ActionResult DPPM()
        {
            return View();
        }

        public ActionResult Getobadppm()
        {
            string msg = "";
            DataSet ds = null;
            try
            {
                IDataParameter[] sqlparameter1 =
                     {
                      };
                //udpGetWorkOrderAging
                ds = _service.RunProcedure("MES_GetOBADPPMForAllProject", sqlparameter1, "result", ConDB.DashboardContext);
                msg = "OK";
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
            return JsonFormat(new JsonData(JsonData.FLAG_ERROR, new { data = ds.Tables[0], project = ds.Tables[1], dppm = ds.Tables[2] }, msg));
        }

    }
}