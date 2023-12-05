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
using SDPSubSystem.Model.AssistModels;

namespace SDPSubSystem.Web.Controllers
{
    public class QualityManualController : BaseController
    {
        RawInputService _service = new RawInputService();


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult load(string code)
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

            DataSet ds = _service.RunProcedure("uspQualityManualManage", Sqlparam, "bb", ConDB.PPAppContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            
            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }

        public ActionResult loadData(string code)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Code", code);
            dic.Add("Flag", "Get");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;
            DataSet ds = _service.RunProcedure("uspQualityManualManage", Sqlparam, "bb", ConDB.PPAppContext);


            return JsonFormat(new { Data = ds.Tables[1] }, "yyyyMMdd");
        }

        

        public ActionResult Upload()
        {
            
            return View();
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
                    var path = HttpContext.Server.MapPath("~/upload/QualityManual/");
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

        public ActionResult SaveUpload(string poststr)
        {
            List<string> errMsg = new List<string>();
            try
            {
                string dbstr = Request.Params["X_DB_MATRIX"];
                int code = 0;
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, object> dic = jss.Deserialize<Dictionary<string, object>>(poststr);
                dic["UserName"] = _service.CurrentUser.Account;
                dic.Add("Flag", "UploadSave");
                string sqlxml = XmlParams.CreateSqlXml(dic);
                string errormsg = _service.RunProcedure("uspQualityManualManage", sqlxml, out code, ConDB.PPAppContext);

                if (code == 0)
                {
                    //删除原文件
                    //var path = HttpContext.Server.MapPath("~/upload/QualityManual/")+ errormsg;
                    //if (System.IO.File.Exists(path))
                    //{
                    //    System.IO.File.Delete(path);
                    //}

                    if (System.IO.File.Exists(errormsg))
                    {
                        System.IO.File.Delete(errormsg);
                    }

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

        

        public ActionResult SaveAdd(string poststr)
        {
            List<string> errMsg = new List<string>();
            try
            {
                string dbstr = Request.Params["X_DB_MATRIX"];
                int code = 0;
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, object> dic = jss.Deserialize<Dictionary<string, object>>(poststr);
                dic["UserName"] = _service.CurrentUser.Account;
                dic.Add("Flag", "AddSave");
                string sqlxml = XmlParams.CreateSqlXml(dic);
                string errormsg = _service.RunProcedure("uspQualityManualManage", sqlxml, out code, ConDB.PPAppContext);

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


        public ActionResult Delete(string ids)
        {
            List<string> errMsg = new List<string>();
            try
            {
                string dbstr = Request.Params["X_DB_MATRIX"];
                int code = 0;
                //JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic["UserName"] = _service.CurrentUser.Account;
                dic.Add("Flag", "Delete");
                dic.Add("IDS", ids);
                string sqlxml = XmlParams.CreateSqlXml(dic);
                //DataTable dt = _service.GetSelectItems(XmlParams.CreateSqlXml(dic), Request.Params["X_DB_MATRIX"]);
                string errormsg = _service.RunProcedure("uspQualityManualManage", sqlxml, out code, ConDB.PPAppContext);

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


        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult loadDataForDashboard()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "GetDataForDashboard");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;
            DataSet ds = _service.RunProcedure("uspQualityManualManage", Sqlparam, "bb", ConDB.PPAppContext);


            return JsonFormat(new { Data = ds.Tables[1] }, "yyyyMMdd");
        }

        public void DownloadFile(string filePath)
        {
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

    }
}