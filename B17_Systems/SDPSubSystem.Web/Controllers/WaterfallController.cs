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
    public class WaterfallController : BaseController
    {

        RawInputService _service = new RawInputService();
        // GET: Master
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMPSWaterfallDataByCondition(string DateFrom, string DateTo, string Project, string FG)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Msg", typeof(string));
            DataRow dr = dt.NewRow();
            dr["Msg"] = "";
            try
            {
                IDataParameter[] sqlparameter =
                {
                   new SqlParameter("@StartDate",DateFrom),
                   new SqlParameter("@EndDate",DateTo),
                   new SqlParameter("@Project",Project),
                   new SqlParameter("@FG",FG)
                };
                DataSet ds = _service.RunProcedure("udpmps_GetWaterFallReport", sqlparameter, "bb", ConDB.DashboardContext);
                return JsonFormat(new { MPS = ds.Tables[0] }, "yyyyMMdd");
            }
            catch (Exception ex)
            {
                dr["Msg"] = ex.Message;
                //dr["Msg"] = "Incorrect syntax near the keyword 'into'.";
                dt.Rows.Add(dr);
                return JsonFormat(new { MPS = dt }, "yyyyMMdd");
            }
        }

        public ActionResult getProject()
        {
            string dbstr = Request.Params["X_DB_MATRIX"];
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "GetProject");
            //dic.Add("ID", ID);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspMPSWaterfall", Sqlparam, "bb", ConDB.DashboardContext);


            return JsonFormat(new { Project = ds.Tables[0] }, "yyyyMMdd");
        }

        public ActionResult getDetailDropByProject(string Project)
        {
            string dbstr = Request.Params["X_DB_MATRIX"];
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Project", Project);
            dic.Add("Flag", "getDetailDropByProject");
            //dic.Add("ID", ID);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspMPSWaterfall", Sqlparam, "bb", ConDB.DashboardContext);

            //ViewData["PartID"] = DBToSelectList(ds.Tables[2]);
            //ViewData["OutputStationType"] = DBToSelectList(ds.Tables[3]);

            return JsonFormat(new { FG = ds.Tables[0]}, "yyyyMMdd");
        }


        public ActionResult ImportIndex()
        {
            return View();
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
                    var path = HttpContext.Server.MapPath("~/upload/" + DateTime.Today.ToString("yyyyMM") + "/Waterfall/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string fileName = Path.GetFileName(files[0].FileName);
                    string saveFileName = Path.GetFileNameWithoutExtension(files[0].FileName) + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(files[0].FileName);
                    saveFileName = Path.Combine(path, saveFileName);
                    files[0].SaveAs(saveFileName);
                    Dictionary<string, object> dic = _service.GetUserInfoDic();

                    fileName=fileName.Substring(0,fileName.LastIndexOf(".")).Replace("_","-");
                    dic.Add("DemandName", fileName);

                    //List<string> cols = new List<string>();
                    //DataTable dt = ExcelToAll.ExcelToDataTableByColumns(saveFileName, dic, cols, 0);
                    //string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(dt);


                    string sqlxml = ExcelToAll_waterfall.ExcelToXml(saveFileName, dic);

                    int code = 0;
                    errormsg = _service.RunProcedure("mpsImportFile", sqlxml, out code, ConDB.DashboardContext);
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

        public ActionResult download(string DateFrom, string DateTo, string Project, string FG)
        {
            IDataParameter[] sqlparameter =
            {
               new SqlParameter("@StartDate",DateFrom),
               new SqlParameter("@EndDate",DateTo),
               new SqlParameter("@Project",Project),
               new SqlParameter("@FG",FG)
            };

            DataSet ds = _service.RunProcedure("udpmps_GetWaterFallReport", sqlparameter, "bb", ConDB.DashboardContext);

            MemoryStream ms = WaterfallExcelOpt.TableToExcelXlsx(ds.Tables[0] == null ? new DataTable() : ds.Tables[0]);
            HttpContext.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Response.AddHeader("Content-Type", "application/octet-stream");
            HttpContext.Response.AddHeader("Set-Cookie", "fileDownload=true; path=/");
            HttpContext.Response.Clear();
            HttpContext.Response.Buffer = true;
            HttpContext.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(Project + "_MPS.xlsx", System.Text.Encoding.UTF8));
            HttpContext.Response.Charset = "utf-8";
            HttpContext.Response.BinaryWrite(ms.ToArray());
            HttpContext.Response.Flush();
            HttpContext.Response.Clear();
            HttpContext.Response.End();
            return null;
        }
    }
}