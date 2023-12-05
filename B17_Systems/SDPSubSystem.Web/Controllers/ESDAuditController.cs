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
using SDPSubSystem.Model.ESDAudit;
using System.Text.RegularExpressions;
using SDPSubSystem.Services.ESDAudit;
using SDPSubSystem.Model.AssistModels;

namespace SDPSubSystem.Web.Controllers
{
    public class ESDAuditController : BaseController
    {
        RawInputService _service = new RawInputService();
        
        #region 设备类型 基础数据维护

        public ActionResult DeviceTypeIndex()
        {
            return View();
        }
        
        public ActionResult DeviceTypeload(string code)
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

            DataSet ds = _service.RunProcedure("udp_ESD_DeviceType", Sqlparam, "bb", ConDB.ActionCheckerDBContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }

        public ActionResult DeviceTypeAdd()
        {
            string dbstr = Request.Params["X_DB_MATRIX"];
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "GetAdd");
            //dic.Add("ID", ID);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("udp_ESD_DeviceType", Sqlparam, "bb", ConDB.ActionCheckerDBContext);

            ViewData["SpecID"] = DBToSelectList(ds.Tables[0]);
            return View();
        }
        
        public ActionResult DeviceTypeSaveAdd(string poststr)
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
                //dic.Add("IDS", ids);
                string sqlxml = XmlParams.CreateSqlXml(dic);
                //DataTable dt = _service.GetSelectItems(XmlParams.CreateSqlXml(dic), Request.Params["X_DB_MATRIX"]);
                string errormsg = _service.RunProcedure("udp_ESD_DeviceType", sqlxml, out code, ConDB.ActionCheckerDBContext);

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
        public ActionResult DeviceTypeEdit(int ID)
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

            DataSet ds = _service.RunProcedure("udp_ESD_DeviceType", Sqlparam, "bb", ConDB.ActionCheckerDBContext);
            udtEsdDeviceType am = new udtEsdDeviceType
            {
                ID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString()),
                Desc = ds.Tables[0].Rows[0][1].ToString(),
                SpecID = Convert.ToInt32(ds.Tables[0].Rows[0][2].ToString()),
                TypeID = Convert.ToInt32(ds.Tables[0].Rows[0][3].ToString()),
            };

            ViewData["SpecID"] = DBToSelectList(ds.Tables[1], am.SpecID);

            return View(am);
        }
        public ActionResult DeviceTypeSaveEdit(string poststr)
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
                string errormsg = _service.RunProcedure("udp_ESD_DeviceType", sqlxml, out code, ConDB.ActionCheckerDBContext);

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
        public ActionResult DeviceTypeDelete(string ids)
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
                string sqlxml = XmlParams.CreateSqlXml(dic);
                //DataTable dt = _service.GetSelectItems(XmlParams.CreateSqlXml(dic), Request.Params["X_DB_MATRIX"]);
                string errormsg = _service.RunProcedure("udp_ESD_DeviceType", sqlxml, out code, ConDB.ActionCheckerDBContext);

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


        #region 设备 基础数据维护
        public ActionResult DeviceIndex()
        {
            return View();
        }

        public ActionResult Deviceload(string code)
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

            DataSet ds = _service.RunProcedure("udp_ESD_Device", Sqlparam, "bb", ConDB.ActionCheckerDBContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }

        public ActionResult DeviceAdd()
        {
            string dbstr = Request.Params["X_DB_MATRIX"];
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "GetAdd");
            //dic.Add("ID", ID);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("udp_ESD_Device", Sqlparam, "bb", ConDB.ActionCheckerDBContext);

            ViewData["DeviceTypeID"] = DBToSelectList(ds.Tables[0]);
            return View();
        }

        public ActionResult DeviceSaveAdd(string poststr)
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
                //dic.Add("IDS", ids);
                string sqlxml = XmlParams.CreateSqlXml(dic);
                //DataTable dt = _service.GetSelectItems(XmlParams.CreateSqlXml(dic), Request.Params["X_DB_MATRIX"]);
                string errormsg = _service.RunProcedure("udp_ESD_Device", sqlxml, out code, ConDB.ActionCheckerDBContext);

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
        public ActionResult DeviceEdit(int ID)
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

            DataSet ds = _service.RunProcedure("udp_ESD_Device", Sqlparam, "bb", ConDB.ActionCheckerDBContext);
            udtEsdDevice am = new udtEsdDevice
            {
                ID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString()),
                FlexID = ds.Tables[0].Rows[0][1].ToString(),
                Desc = ds.Tables[0].Rows[0][2].ToString(),
                Location = ds.Tables[0].Rows[0][3].ToString(),
                DeviceTypeID = Convert.ToInt32(ds.Tables[0].Rows[0][4].ToString())
            };

            ViewData["DeviceTypeID"] = DBToSelectList(ds.Tables[1],am.DeviceTypeID);

            return View(am);
        }
        
        public ActionResult DeviceSaveEdit(string poststr)
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
                string errormsg = _service.RunProcedure("udp_ESD_Device", sqlxml, out code, ConDB.ActionCheckerDBContext);

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
        public ActionResult DeviceDelete(string ids)
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
                string sqlxml = XmlParams.CreateSqlXml(dic);
                //DataTable dt = _service.GetSelectItems(XmlParams.CreateSqlXml(dic), Request.Params["X_DB_MATRIX"]);
                string errormsg = _service.RunProcedure("udp_ESD_Device", sqlxml, out code, ConDB.ActionCheckerDBContext);

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


        #region 测值标准 基础数据维护
        public ActionResult SpecificationIndex()
        {
            return View();
        }

        public ActionResult Specificationload(string code)
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

            DataSet ds = _service.RunProcedure("udp_ESD_Specification", Sqlparam, "bb", ConDB.ActionCheckerDBContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            
            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }

        public ActionResult SpecificationAdd()
        {
            return View();
        }

        public ActionResult SpecificationSaveAdd(string poststr)
        {
            poststr = System.Web.HttpUtility.UrlDecode(poststr, System.Text.Encoding.UTF8);
            List<string> errMsg = new List<string>();
            try
            {
                string dbstr = Request.Params["X_DB_MATRIX"];
                int code = 0;
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, object> dic = jss.Deserialize<Dictionary<string, object>>(poststr);

                dic["UserName"] = _service.CurrentUser.Account;

                dic.Add("Flag", "AddSave");
                //dic.Add("IDS", ids);
                string sqlxml = XmlParams.CreateSqlXml(dic);
                //DataTable dt = _service.GetSelectItems(XmlParams.CreateSqlXml(dic), Request.Params["X_DB_MATRIX"]);
                string errormsg = _service.RunProcedure("udp_ESD_Specification", sqlxml, out code, ConDB.ActionCheckerDBContext);

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
        public ActionResult SpecificationEdit(int ID)
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

            DataSet ds = _service.RunProcedure("udp_ESD_Specification", Sqlparam, "bb", ConDB.ActionCheckerDBContext);
            udtEsdSpec am = new udtEsdSpec
            {
                ID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString()),
                Specification = ds.Tables[0].Rows[0][1].ToString(),
                Min1 = Convert.ToSingle(ds.Tables[0].Rows[0][2].ToString()),
                Max1 = Convert.ToSingle(ds.Tables[0].Rows[0][3].ToString()),
                Unit1 = ds.Tables[0].Rows[0][4].ToString(),
                Min2 = Convert.ToSingle(ds.Tables[0].Rows[0][5].ToString()),
                Max2 = Convert.ToSingle(ds.Tables[0].Rows[0][6].ToString()),
                Unit2 = ds.Tables[0].Rows[0][7].ToString()
            };

            return View(am);
        }
        public ActionResult SpecificationSaveEdit(string poststr)
        {
            poststr = System.Web.HttpUtility.UrlDecode(poststr, System.Text.Encoding.UTF8);
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
                string errormsg = _service.RunProcedure("udp_ESD_Specification", sqlxml, out code, ConDB.ActionCheckerDBContext);

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
        public ActionResult SpecificationDelete(string ids)
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
                string sqlxml = XmlParams.CreateSqlXml(dic);
                //DataTable dt = _service.GetSelectItems(XmlParams.CreateSqlXml(dic), Request.Params["X_DB_MATRIX"]);
                string errormsg = _service.RunProcedure("udp_ESD_Specification", sqlxml, out code, ConDB.ActionCheckerDBContext);

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

        public ActionResult TestESD()
        {
            return View();
        }

        public ActionResult Testload(string code)
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

            DataSet ds = _service.RunProcedure("udp_ESD_TestRecord", Sqlparam, "bb", ConDB.ActionCheckerDBContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());

            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }

        public ActionResult TestAdd()
        {
            return View();
        }

        public ActionResult TestVerifySaveAdd(string poststr)
        {
            List<string> errMsg = new List<string>();
            Dictionary<string, string> ctlMsg = new Dictionary<string, string>();
            try
            {
                string dbstr = Request.Params["X_DB_MATRIX"];
                int code = 0;
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, object> dic = jss.Deserialize<Dictionary<string, object>>(poststr);

                dic["UserName"] = _service.CurrentUser.EmployeeNO;

                dic.Add("Flag", "AddSave");
                //dic.Add("IDS", ids);
                string sqlxml = XmlParams.CreateSqlXml(dic);
                //DataTable dt = _service.GetSelectItems(XmlParams.CreateSqlXml(dic), Request.Params["X_DB_MATRIX"]);
                string errormsg = _service.RunProcedure("udp_ESD_TestRecord", sqlxml, out code, ConDB.ActionCheckerDBContext);

                if (code == 0)
                {
                    ///return Json(new JsonData(JsonData.FLAG_SUCCESS, null, "OK"));
                    return Json(new JsonData(JsonData.FLAG_SUCCESS));
                }
                else if (code==201)
                {
                    ctlMsg.Add("code", "201");
                    return Json(new JsonData(JsonData.FLAG_ERROR, null, errormsg, ctlMsg));
                }
                else if(code==202)
                {
                    ctlMsg.Add("code", "202");
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

        public ActionResult TestSaveAddOwner(string poststr)
        {
            List<string> errMsg = new List<string>();
            try
            {
                string dbstr = Request.Params["X_DB_MATRIX"];
                int code = 0;
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, object> dic = jss.Deserialize<Dictionary<string, object>>(poststr);

                string owner = Regex.Replace(dic["Owner"].ToString(), @"\s", "");
                if (BaseService.GetUserInfoByEmpEmailAddress(owner) == null)
                {
                    errMsg.Add("该用户不存在！");
                    return Json(new JsonData(JsonData.FLAG_ERROR, null, errMsg));
                }
                dic["Owner"] = BaseService.GetUserInfoByEmpEmailAddress(owner).MailAddress;
                dic.Add("Flag", "AddSaveOwner");
                //dic.Add("IDS", ids);
                string sqlxml = XmlParams.CreateSqlXml(dic);
                //DataTable dt = _service.GetSelectItems(XmlParams.CreateSqlXml(dic), Request.Params["X_DB_MATRIX"]);
                string errormsg = _service.RunProcedure("udp_ESD_TestRecord", sqlxml, out code, ConDB.ActionCheckerDBContext);

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

        public ActionResult TestRecordEdit(int TestID)
        {
            string dbstr = Request.Params["X_DB_MATRIX"];
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "GetEdit");
            dic.Add("ID", TestID);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("udp_ESD_TestRecord", Sqlparam, "bb", ConDB.ActionCheckerDBContext);
            udtEsdTestRecord am = new udtEsdTestRecord
            {
                Owner = ds.Tables[0].Rows[0][0].ToString()
            };

            ViewData["TestID"] = TestID;
            return View(am);
        }

        public ActionResult TestESDSummaryByOwner()
        {
            return View();
        }

        public ActionResult SummaryByOwnerLoadList(string dateFrom, string dateTo, string FlexID, string Owner)
        {
            int start = Convert.ToInt32(Request.Params["start"]);//起始数
            int rows = Convert.ToInt32(Request.Params["length"]);//长度
            int draw = Convert.ToInt32(Request.Params["draw"]);//画的页数
            string dbstr = Request.Params["X_DB_MATRIX"];
            int total;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "SummaryByOwnerGetDataList");
            dic.Add("PageSize", rows);
            //int PageIndex = start / rows + 1;
            dic.Add("PageIndex", start / rows + 1);
            dic.Add("dateFrom", dateFrom);
            dic.Add("dateTo", dateTo);
            dic.Add("FlexID", FlexID);
            dic.Add("Owner", Owner);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("udp_ESD_TestRecord", Sqlparam, "bb", ConDB.ActionCheckerDBContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            //var result = xs.ConvertTo<vShippingNotice>(ds.Tables[1]);
            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }

        public ActionResult TestESDSummaryByOwnerTestEdit(int ID)
        {
            string dbstr = Request.Params["X_DB_MATRIX"];
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "GetEditSummaryByOwner");
            dic.Add("ID", ID);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("udp_ESD_TestRecord", Sqlparam, "bb", ConDB.ActionCheckerDBContext);
            udtEsdTestRecord am = new udtEsdTestRecord
            {
                RootCause = ds.Tables[0].Rows[0][0].ToString(),
                Action = ds.Tables[0].Rows[0][1].ToString(),
                CompletionDate = Convert.ToDateTime(string.IsNullOrEmpty(ds.Tables[0].Rows[0][2].ToString()) ? null : ds.Tables[0].Rows[0][2].ToString())
            };

            ViewData["TestID"] = ID;
            return View(am);
        }

        public ActionResult TestSaveAddSummaryByOwner(string poststr)
        {
            List<string> errMsg = new List<string>();
            try
            {
                string dbstr = Request.Params["X_DB_MATRIX"];
                int code = 0;
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, object> dic = jss.Deserialize<Dictionary<string, object>>(poststr);
                dic.Add("Flag", "EditSaveByOwner");
                ///dic.Add("IDS", ids);
                string sqlxml = XmlParams.CreateSqlXml(dic);
                //DataTable dt = _service.GetSelectItems(XmlParams.CreateSqlXml(dic), Request.Params["X_DB_MATRIX"]);
                string errormsg = _service.RunProcedure("udp_ESD_TestRecord", sqlxml, out code, ConDB.ActionCheckerDBContext);

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

        public ActionResult TestESDSummaryByVerifier()
        {
            return View();
        }

        public ActionResult SummaryByVerifierLoadList(string dateFrom, string dateTo, string FlexID, string Owner)
        {
            int start = Convert.ToInt32(Request.Params["start"]);//起始数
            int rows = Convert.ToInt32(Request.Params["length"]);//长度
            int draw = Convert.ToInt32(Request.Params["draw"]);//画的页数
            string dbstr = Request.Params["X_DB_MATRIX"];
            int total;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "SummaryByVerifierGetDataList");
            dic.Add("PageSize", rows);
            //int PageIndex = start / rows + 1;
            dic.Add("PageIndex", start / rows + 1);
            dic.Add("dateFrom", dateFrom);
            dic.Add("dateTo", dateTo);
            dic.Add("FlexID", FlexID);
            dic.Add("Owner", Owner);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("udp_ESD_TestRecord", Sqlparam, "bb", ConDB.ActionCheckerDBContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            //var result = xs.ConvertTo<vShippingNotice>(ds.Tables[1]);
            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }

        public ActionResult TestESDSummaryByVerifierTestEdit(int ID)
        {
            string dbstr = Request.Params["X_DB_MATRIX"];
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "GetEditSummaryByVerifier");
            dic.Add("ID", ID);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("udp_ESD_TestRecord", Sqlparam, "bb", ConDB.ActionCheckerDBContext);
            udtEsdTestRecord am = new udtEsdTestRecord
            {
                VerifyResult = ds.Tables[0].Rows[0][0].ToString(),
                Status = ds.Tables[0].Rows[0][1].ToString(),
                Remark = ds.Tables[0].Rows[0][2].ToString()
            };
            ViewData["TestID"] = ID;
            return View(am);
        }

        public ActionResult TestSaveAddSummaryByVerifier(string poststr)
        {
            List<string> errMsg = new List<string>();
            try
            {
                string dbstr = Request.Params["X_DB_MATRIX"];
                int code = 0;
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, object> dic = jss.Deserialize<Dictionary<string, object>>(poststr);
                dic.Add("Flag", "EditSaveByVerifier");
                ///dic.Add("IDS", ids);
                string sqlxml = XmlParams.CreateSqlXml(dic);
                //DataTable dt = _service.GetSelectItems(XmlParams.CreateSqlXml(dic), Request.Params["X_DB_MATRIX"]);
                string errormsg = _service.RunProcedure("udp_ESD_TestRecord", sqlxml, out code, ConDB.ActionCheckerDBContext);

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

        public ActionResult TestRecordExport(string ids)
        {
            List<string> errMsg = new List<string>();
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Flag", "ExportGetList");
                dic.Add("IDS", ids);
                IDataParameter[] Sqlparam =
                {
                    new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                    new SqlParameter("@msg",SqlDbType.NVarChar,2000),
                };
                Sqlparam[1].Direction = ParameterDirection.Output;

                DataSet ds = _service.RunProcedure("udp_ESD_TestRecord", Sqlparam, "bb", ConDB.ActionCheckerDBContext);


                string path = System.Web.HttpContext.Current.Server.MapPath("~/Template");

                //string user = _service.CurrentUser.EName;
                //string path1 = System.Web.HttpContext.Current.Server.MapPath("~/Templates");

                path += "\\DMNQAI-021-F2.03.xlsx";
                //path1 += "\\ooo.xlsx";
                //string mailaddress = ds.Tables[0].Rows[0]["MailAddress"].ToString();
                MemoryStream ms = ESDAuditService.GetExportFile(path, ds);

                string time = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.Hour.ToString()+"-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();

                return File(ms, "application/vnd.ms-excel", "DMNQAI-021-F2.03 ESD System Audit Measurement Report " +time+ ".xlsx");

            }
            catch (Exception ex)
            {
                errMsg.Add(ex.Message);
            }
            return Json(new JsonData(JsonData.FLAG_ERROR, "Last END", errMsg));
        }

        public static void TestRecordDailyReport()
        {
            List<string> errMsg = new List<string>();
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Flag", "GetDailyReport");
                //dic.Add("IDS", ids);
                IDataParameter[] Sqlparam =
                {
                    new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                    new SqlParameter("@msg",SqlDbType.NVarChar,2000),
                };
                Sqlparam[1].Direction = ParameterDirection.Output;

                DataSet ds = new RawInputService().RunProcedure("udp_ESD_SendEmail", Sqlparam, "bb", ConDB.ActionCheckerDBContext);


                string path = System.Web.HttpContext.Current.Server.MapPath("~/Template");

                //string user = _service.CurrentUser.EName;
                //string path1 = System.Web.HttpContext.Current.Server.MapPath("~/Templates");

                path += "\\DMNQAI-021-F2.03.xlsx";
                //path1 += "\\ooo.xlsx";
                //string mailaddress = ds.Tables[0].Rows[0]["MailAddress"].ToString();
                MemoryStream ms = ESDAuditService.GetExportFile(path, ds);

                ESDAuditService.ESDDailyReportEmail(ms);
                //return File(ms, "application/vnd.ms-excel", "DMNQAI-021-F2.03 ESD System Audit Measurement Report " + time + ".xlsx");

            }
            catch (Exception ex)
            {
                errMsg.Add(ex.Message);
            }
            //return Json(new JsonData(JsonData.FLAG_ERROR, "Last END", errMsg));
        }

        public static void TestRecordFailReport()
        {
            List<string> errMsg = new List<string>();
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Flag", "GetFailNoticeReport");
                //dic.Add("IDS", ids);
                IDataParameter[] Sqlparam =
                {
                    new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                    new SqlParameter("@msg",SqlDbType.NVarChar,2000),
                };
                Sqlparam[1].Direction = ParameterDirection.Output;

                DataSet ds = new RawInputService().RunProcedure("udp_ESD_SendEmail", Sqlparam, "bb", ConDB.ActionCheckerDBContext);


                string path = System.Web.HttpContext.Current.Server.MapPath("~/Template");

                //string user = _service.CurrentUser.EName;
                //string path1 = System.Web.HttpContext.Current.Server.MapPath("~/Templates");

                path += "\\DMNQAI-021-F2.03.xlsx";
                //path1 += "\\ooo.xlsx";
                //string mailaddress = ds.Tables[0].Rows[0]["MailAddress"].ToString();
                MemoryStream ms = ESDAuditService.GetExportFile(path, ds);

                ESDAuditService.ESDFailReportEmail(ms, ds);
                //return File(ms, "application/vnd.ms-excel", "DMNQAI-021-F2.03 ESD System Audit Measurement Report " + time + ".xlsx");

            }
            catch (Exception ex)
            {
                errMsg.Add(ex.Message);
            }
        }

        public ActionResult WristStrapTesting()
        {
            return View();
        }


        public ActionResult WristStrapTestingLoad(string date,string projectID,string line)
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
            dic.Add("ProjectID", projectID);
            dic.Add("Line", line);
            dic.Add("date", date);
            dic.Add("Flag", "Get");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("udp_ESD_WristStrap", Sqlparam, "bb", ConDB.ActionCheckerDBContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());

            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }

        public ActionResult GetDropDownList()
        {
            string dbstr = Request.Params["X_DB_MATRIX"];
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "GetProjects");
            //dic.Add("ID", ID);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("udp_ESD_WristStrap", Sqlparam, "bb", ConDB.ActionCheckerDBContext);

            return JsonFormat(new { project = ds.Tables[0]}, "yyyyMMdd");
        }

        public ActionResult WristStrapTestingAdd()
        {
            string dbstr = Request.Params["X_DB_MATRIX"];
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "GetProjects");
            //dic.Add("ID", ID);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("udp_ESD_WristStrap", Sqlparam, "bb", ConDB.ActionCheckerDBContext);
            ViewData["ProjectID"] = DBToSelectList(ds.Tables[0]);
            return View();
        }

        public ActionResult WristStrapTestingSaveAdd(string poststr)
        {
            List<string> errMsg = new List<string>();
            Dictionary<string, string> ctlMsg = new Dictionary<string, string>();
            try
            {
                string dbstr = Request.Params["X_DB_MATRIX"];
                int code = 0;
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, object> dic = jss.Deserialize<Dictionary<string, object>>(poststr);

                dic["UserName"] = _service.CurrentUser.CName;

                //增加EmpNO正确性校验
                if (!String.IsNullOrEmpty(dic["EmpNO"].ToString()))
                {
                    Users users = BaseService.GetUserInfoByEmpNO(dic["EmpNO"].ToString());
                    if (users == null)
                    {
                        errMsg.Add("EmpNO:" + dic["EmpNO"].ToString() + ", not correct");
                        return Json(new JsonData(JsonData.FLAG_ERROR, null, errMsg));
                    }
                }
                else
                {
                    errMsg.Add("EmpNO can not be null");
                    return Json(new JsonData(JsonData.FLAG_ERROR, null, errMsg));
                }

                dic["EmpName"] = BaseService.GetUserInfoByEmpNO(dic["EmpNO"].ToString()).CName; 

                dic.Add("Flag", "AddSave");
                string sqlxml = XmlParams.CreateSqlXml(dic);
                string errormsg = _service.RunProcedure("udp_ESD_WristStrap", sqlxml, out code, ConDB.ActionCheckerDBContext);

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

        public ActionResult WristStrapConfirm(string ids)
        {
            List<string> errMsg = new List<string>();
            try
            {
                string dbstr = Request.Params["X_DB_MATRIX"];
                int code = 0;
                //JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Flag", "Confirm");
                dic["leaderCName"] = _service.CurrentUser.CName;
                dic.Add("IDS", ids);
                string sqlxml = XmlParams.CreateSqlXml(dic);
                //DataTable dt = _service.GetSelectItems(XmlParams.CreateSqlXml(dic), Request.Params["X_DB_MATRIX"]);
                string errormsg = _service.RunProcedure("udp_ESD_WristStrap", sqlxml, out code, ConDB.ActionCheckerDBContext);

                if (code == 0)
                {
                    ///return Json(new JsonData(JsonData.FLAG_SUCCESS, null, "OK"));
                    return Json(new JsonData(JsonData.FLAG_SUCCESS, null, "Confirm OK"));
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

        public ActionResult WristStrapExport(string ids,string date,string projectID,string line)
        {
            List<string> errMsg = new List<string>();
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Flag", "ExportGetList");
                dic.Add("IDS", ids);
                dic.Add("date", date);
                dic.Add("ProjectID", projectID);
                dic.Add("Line", line);
                IDataParameter[] Sqlparam =
                {
                    new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                    new SqlParameter("@msg",SqlDbType.NVarChar,2000),
                };
                Sqlparam[1].Direction = ParameterDirection.Output;

                DataSet ds = _service.RunProcedure("udp_ESD_WristStrap", Sqlparam, "bb", ConDB.ActionCheckerDBContext);


                string path = System.Web.HttpContext.Current.Server.MapPath("~/Template");

                path += "\\DMNQAI-074-F1.01 ESD Wrist Strap Testing Record.xls";
                MemoryStream ms = ESDAuditService.GetExportFile_WristStrap(path, ds, date);

                string time = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();

                return File(ms, "application/vnd.ms-excel", "DMNQAI-074-F1.01 ESD Wrist Strap Testing Record " + time + ".xls");

            }
            catch (Exception ex)
            {
                errMsg.Add(ex.Message);
            }
            return Json(new JsonData(JsonData.FLAG_ERROR, "Last END", errMsg));
        }

    }
}