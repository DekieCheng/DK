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
using SDPSubSystem.Common;
using SDPSubSystem.Model.AssistModels;
using SDPSubSystem.Common.Security;

namespace SDPSubSystem.Web.Controllers
{
    public class ChangeoverRegisterController : BaseController
    {
        RawInputService _service = new RawInputService();
        
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult getDetailDrop()
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

            DataSet ds = _service.RunProcedure("uspChangeoverRegister", Sqlparam, "bb", ConDB.PlataContext);

            

            return JsonFormat(new { Line = ds.Tables[1], Project = ds.Tables[0] }, "yyyyMMdd");
        }

        public ActionResult VerifyUser(string Userad,string Userpwd)
        {
            List<string> errMsg = new List<string>();
            try
            {
                string msg = "";
                bool flag = false;
                flag=DomainManager.fnCheckDomainUser(Userad.Trim(), Userpwd.Trim(), out msg);

                if (flag)
                {
                    ///return Json(new JsonData(JsonData.FLAG_SUCCESS, null, "OK"));
                    return Json(new JsonData(JsonData.FLAG_SUCCESS, null, "Add OK"));
                }
                else
                {
                    errMsg.Add(msg);
                    return Json(new JsonData(JsonData.FLAG_ERROR, null, errMsg));
                }
            }
            catch (Exception ex)
            {
                errMsg.Add(ex.Message);
            }
            return Json(new JsonData(JsonData.FLAG_ERROR, null, errMsg));
        }

        public ActionResult GetUserInfo(string Userad)
        {
            Users user=BaseService.GetUserInfoByDMNAccount(Userad);

            return JsonFormat(new { User=user.CName+","+user.EmployeeNO }, "yyyyMMdd");
        }

        public string getDatabase(string Project)
        {
            int code;
            string dbstr = Request.Params["X_DB_MATRIX"];
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "getDatabase");
            dic.Add("Project", Project);

            string sqlxml = XmlParams.CreateSqlXml(dic);
            string msg = _service.RunProcedure("uspChangeoverRegister", sqlxml, out code, ConDB.PlataContext);

            return DESSecurity.Decrypt(msg);
        }


        public ActionResult CreateNewRecord(string Project)
        {
            string databaseConnStr = getDatabase(Project);


            int code=0;
            string dbstr = Request.Params["X_DB_MATRIX"];
            Dictionary<string, object> dic = new Dictionary<string, object>();

            dic.Add("Flag", "CreateNewRecord");
            dic.Add("Creater", _service.CurrentUser.CName + "," + _service.CurrentUser.EmployeeNO);
            string sqlxml = XmlParams.CreateSqlXml(dic);

            List<string> errMsg = new List<string>();
            try
            {
                _service.RunProcedure("uspChangeoverRegister", sqlxml, out code, databaseConnStr);
                
            }
            catch (Exception ex)
            {
                errMsg.Add(ex.Message);
                return Json(new JsonData(JsonData.FLAG_ERROR, null, errMsg));
            }


            return Json(new JsonData(JsonData.FLAG_SUCCESS, null, code));
        }

        public ActionResult SaveTableRecord(string poststr)
        {
            
            List<string> errMsg = new List<string>();
            try
            {
                string dbstr = Request.Params["X_DB_MATRIX"];
                int code = 0;
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, object> dic = jss.Deserialize<Dictionary<string, object>>(poststr);
                dic.Add("Flag", "SaveTableRecord");

                string databaseConnStr = getDatabase(dic["Project"].ToString());

                string sqlxml = XmlParams.CreateSqlXml(dic);
                string errormsg = _service.RunProcedure("uspChangeoverRegister", sqlxml, out code, databaseConnStr);

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

        #region 获取DataTable前几条数据 
        /// <summary> 
        /// 获取DataTable前几条数据 
        /// </summary> 
        /// <param name="TopItem">前N条数据</param> 
        /// <param name="oDT">源DataTable</param> 
        /// <returns></returns> 
        public DataTable DtSelectTop(int TopItem, DataTable oDT)
        {
            if (oDT.Rows.Count < TopItem) return oDT;

            DataTable NewTable = oDT.Clone();
            DataRow[] rows = oDT.Select("1=1");
            for (int i = 0; i < TopItem; i++)
            {
                NewTable.ImportRow((DataRow)rows[i]);
            }
            return NewTable;
        }
        #endregion

        public ActionResult Load(string code)
        {
            int start = Convert.ToInt32(Request.Params["start"]);//起始数
            int rows = Convert.ToInt32(Request.Params["length"]);//长度
            int draw = Convert.ToInt32(Request.Params["draw"]);//画的页数
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

            DataTable dt=SelectAllDB();
            DataTable mdt = new DataTable();
            int total = 0;

            List<string> errMsg = new List<string>();
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string dbstr = dt.Rows[i][1].ToString();
                    dbstr = DESSecurity.Decrypt(dbstr);

                    DataSet ds = _service.RunProcedure("uspChangeoverRegister", Sqlparam, "bb", dbstr);

                    total = total + Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                    //var result = xs.ConvertTo<vShippingNotice>(ds.Tables[1]);
                    var dtt = ds.Tables[1];

                    if (mdt.Columns.Count == 0)
                    {
                        mdt = dtt.Copy();
                    }
                    else
                    {
                        mdt.Merge(dtt);
                    }
                }
            }
            catch (Exception ex)
            {
                errMsg.Add(ex.Message);
                return Json(new JsonData(JsonData.FLAG_ERROR, null, errMsg));
            }
            
            
            return JsonFormat(new DataTablesResultInfo(DtSelectTop(rows,mdt), draw, total, total));
        }

        public ActionResult LoadTable(string code,string project)
        {
            string databaseConnStr = getDatabase(project);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Code", code);
            dic.Add("Flag", "GetTable");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspChangeoverRegister", Sqlparam, "bb", databaseConnStr);

            return JsonFormat(new { TableData = ds.Tables[0] }, "yyyyMMdd");
        }


        public ActionResult Edit(int ID,string Project)
        {
            string databaseConnStr = getDatabase(Project);
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

            DataSet ds = _service.RunProcedure("uspChangeoverRegister", Sqlparam, "bb", databaseConnStr);
            ViewData["Project"] = ds.Tables[0].Rows[0]["Project"];
            ViewData["Line"] = ds.Tables[0].Rows[0]["Line"];
            ViewData["CurrPN"] = ds.Tables[0].Rows[0]["CurrPN"];
            ViewData["NextPN"] = ds.Tables[0].Rows[0]["NextPN"];
            ViewData["FFWO"] = ds.Tables[0].Rows[0]["FFWO"];
            ViewData["EE1"] = ds.Tables[0].Rows[0]["EE1"];
            ViewData["EE2"] = ds.Tables[0].Rows[0]["EE2"];
            ViewData["EE3"] = ds.Tables[0].Rows[0]["EE3"];
            ViewData["PE"] = ds.Tables[0].Rows[0]["PE"];
            ViewData["Production"] = ds.Tables[0].Rows[0]["Production"];
            ViewData["ID"] = ID;

            return View();
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

                string databaseConnStr = getDatabase(dic["Project"].ToString());

                dic.Add("Flag", "AddSave");
                dic.Add("Creater", _service.CurrentUser.CName + "," + _service.CurrentUser.EmployeeNO);
                //dic.Add("IDS", ids);
                string sqlxml = XmlParams.CreateSqlXml(dic);
                //DataTable dt = _service.GetSelectItems(XmlParams.CreateSqlXml(dic), Request.Params["X_DB_MATRIX"]);
                string errormsg = _service.RunProcedure("uspChangeoverRegister", sqlxml, out code, databaseConnStr);

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
        public ActionResult SaveEdit(string poststr)
        {
            List<string> errMsg = new List<string>();
            try
            {
                string dbstr = Request.Params["X_DB_MATRIX"];
                int code = 0;
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, object> dic = jss.Deserialize<Dictionary<string, object>>(poststr);

                string databaseConnStr = getDatabase(dic["Project"].ToString());

                dic.Add("Flag", "EditSave");
                string sqlxml = XmlParams.CreateSqlXml(dic);
                //DataTable dt = _service.GetSelectItems(XmlParams.CreateSqlXml(dic), Request.Params["X_DB_MATRIX"]);
                string errormsg = _service.RunProcedure("uspChangeoverRegister", sqlxml, out code, databaseConnStr);

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
        public ActionResult Delete(string ids,string Project)
        {
            string databaseConnStr = getDatabase(Project);
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
                string errormsg = _service.RunProcedure("uspChangeoverRegister", sqlxml, out code, databaseConnStr);

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

        public ActionResult DeleteTable(string poststr)
        {
            
            List<string> errMsg = new List<string>();
            try
            {
                string dbstr = Request.Params["X_DB_MATRIX"];
                int code = 0;
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, object> dic = jss.Deserialize<Dictionary<string, object>>(poststr);

                string databaseConnStr = getDatabase(dic["Project"].ToString());

                dic.Add("Flag", "DeleteTable");
                string sqlxml = XmlParams.CreateSqlXml(dic);
                //DataTable dt = _service.GetSelectItems(XmlParams.CreateSqlXml(dic), Request.Params["X_DB_MATRIX"]);
                string errormsg = _service.RunProcedure("uspChangeoverRegister", sqlxml, out code, databaseConnStr);

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

        public DataTable SelectAllDB()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "SelectEncryDB");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspChangeoverRegister", Sqlparam, "bb", ConDB.PlataContext);
            DataTable dt = ds.Tables[0];
            return dt;
        }

            public void EncryDB()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "SelectEncryDB");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspChangeoverRegister", Sqlparam, "bb", ConDB.PlataContext);
            DataTable dt = ds.Tables[0];
            for (int i=0;i< dt.Rows.Count;i++)
            {
                string dbstr = dt.Rows[i][1].ToString();
                dbstr = DESSecurity.Encrypt(dbstr);

                Dictionary<string, object> dix = new Dictionary<string, object>();
                dix.Add("Flag", "UpdateEncryDB");
                dix.Add("Project", dt.Rows[i][0].ToString());
                dix.Add("dbstr", dbstr);
                IDataParameter[] Sqlpar =
                {
                    new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dix)),
                    new SqlParameter("@msg",SqlDbType.NVarChar,2000),
                };
                Sqlpar[1].Direction = ParameterDirection.Output;
                _service.RunProcedure("uspChangeoverRegister", Sqlpar, "bb", ConDB.PlataContext);

            }
        }
    }
}