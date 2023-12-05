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
using SDPSubSystem.Model.AssistModels;

namespace SDPSubSystem.Web.Controllers
{
    public class MaterialTrackerController : BaseController
    {
        RawInputService _service = new RawInputService();
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Load(string PartNumber, string ProductionOrderNumber)
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
            dic.Add("ProductionOrderNumber", ProductionOrderNumber);
            dic.Add("PartNumber", PartNumber);
            dic.Add("Flag", "Get");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspManageSMTMaterialTracker", Sqlparam, "bb", ConDB.IntelContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());

            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult SaveAdd(string poststr)
        {
            List<string> errMsg = new List<string>();
            Dictionary<string, string> ctlMsg = new Dictionary<string, string>();
            try
            {
                string dbstr = Request.Params["X_DB_MATRIX"];
                int code = 0;
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, object> dic = jss.Deserialize<Dictionary<string, object>>(poststr);

                dic["UserName"] = _service.CurrentUser.Account;

                //增加生产部EmpNO正确性校验
                if (!String.IsNullOrEmpty(dic["ProEmployee"].ToString()))
                {
                    Users users = BaseService.GetUserInfoByEmpNO(dic["ProEmployee"].ToString());
                    if (users == null)
                    {
                        errMsg.Add("ProEmployee:" + dic["ProEmployee"].ToString() + ", not correct");
                        return Json(new JsonData(JsonData.FLAG_ERROR, null, errMsg));
                    }
                }
                else
                {
                    errMsg.Add("ProEmployee can not be null");
                    return Json(new JsonData(JsonData.FLAG_ERROR, null, errMsg));
                }

                //增加QA EmpNO正确性校验
                if (!String.IsNullOrEmpty(dic["QAEmployee"].ToString()))
                {
                    Users users = BaseService.GetUserInfoByEmpNO(dic["QAEmployee"].ToString());
                    if (users == null)
                    {
                        errMsg.Add("QAEmployee:" + dic["QAEmployee"].ToString() + ", not correct");
                        return Json(new JsonData(JsonData.FLAG_ERROR, null, errMsg));
                    }
                }
                else
                {
                    errMsg.Add("QAEmployee can not be null");
                    return Json(new JsonData(JsonData.FLAG_ERROR, null, errMsg));
                }

                
                dic.Add("Flag", "AddSave");
                string sqlxml = XmlParams.CreateSqlXml(dic);
                string errormsg = _service.RunProcedure("uspManageSMTMaterialTracker", sqlxml, out code, ConDB.IntelContext);

                if (code == 0)
                {
                    ///return Json(new JsonData(JsonData.FLAG_SUCCESS, null, "OK"));
                    return Json(new JsonData(JsonData.FLAG_SUCCESS));
                }
                else
                {
                    errMsg.Add(errormsg);
                    return Json(new JsonData(JsonData.FLAG_ERROR, null, errMsg, ctlMsg));
                }
            }
            catch (Exception ex)
            {
                errMsg.Add(ex.Message);
            }
            return Json(new JsonData(JsonData.FLAG_ERROR, null, errMsg, ctlMsg));
        }

        public ActionResult GetDropDownListForPartNumber()
        {
            string dbstr = Request.Params["X_DB_MATRIX"];
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "GetDropDownListForPartNumber");
            //dic.Add("ID", ID);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspManageSMTMaterialTracker", Sqlparam, "bb", ConDB.IntelContext);

            return JsonFormat(new { PartNumber = ds.Tables[0] }, "yyyyMMdd");
        }

        public ActionResult getDetailDropByPartNumber(string PartNumber)
        {
            string dbstr = Request.Params["X_DB_MATRIX"];
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "getDetailDropByPartNumber");
            dic.Add("PartNumber", PartNumber);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspManageSMTMaterialTracker", Sqlparam, "bb", ConDB.IntelContext);

            return JsonFormat(new { ProductionOrderNumber = ds.Tables[0] }, "yyyyMMdd");
        }

        public ActionResult Export(string PartNumber, string ProductionOrderNumber)
        {
            List<string> errMsg = new List<string>();
            int start = 1;//起始数
            int rows = 99999999;//长度
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("PageSize", rows);
                //int PageIndex = start / rows + 1;
                dic.Add("PageIndex", start / rows + 1);
                dic.Add("ProductionOrderNumber", ProductionOrderNumber);
                dic.Add("PartNumber", PartNumber);
                dic.Add("Flag", "Get");
                IDataParameter[] Sqlparam =
                {
                    new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                    new SqlParameter("@msg",SqlDbType.NVarChar,2000),
                };
                Sqlparam[1].Direction = ParameterDirection.Output;

                DataSet ds = _service.RunProcedure("uspManageSMTMaterialTracker", Sqlparam, "Sheet1", ConDB.IntelContext);

                MemoryStream ms = ExcelOpt.TableToExcelXlsx(ds.Tables[1] == null ? new DataTable() : ds.Tables[1]);

                string time = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();

                return File(ms, "application/vnd.ms-excel", "Intel SMT Material Tracker " + time + ".xlsx");
                //HttpContext.Response.ContentType = "application/vnd.ms-excel";
                //HttpContext.Response.AddHeader("Content-Type", "application/octet-stream");
                //HttpContext.Response.AddHeader("Set-Cookie", "fileDownload=true; path=/");
                //HttpContext.Response.Clear();
                //HttpContext.Response.Buffer = true;
                //HttpContext.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("MachineUtilizationReport.xlsx", System.Text.Encoding.UTF8));
                //HttpContext.Response.Charset = "utf-8";
                //HttpContext.Response.BinaryWrite(ms.ToArray());
                //HttpContext.Response.Flush();
                //HttpContext.Response.Clear();
                //HttpContext.Response.End();
                //return null;

            }
            catch (Exception ex)
            {
                errMsg.Add(ex.Message);
            }
            return Json(new JsonData(JsonData.FLAG_ERROR, "Last END", errMsg));
        }

    }
}