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
    public class SMTBackEndAutoKittingController : BaseController
    {
        RawInputService _service = new RawInputService();
        
        #region MatrixLineDataMaintenance

        public ActionResult LineMatrixIndex()
        {
            return View();
        }
        
        public ActionResult LineMatrixload(string code)
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

            DataSet ds = _service.RunProcedure("uspManageEXLine", Sqlparam, "bb", ConDB.DWHDBContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            //var result = xs.ConvertTo<vShippingNotice>(ds.Tables[1]);
            //var dt = ds.Tables[1];
            //DataTable dtnew = new DataTable();   //新建一个对象
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    DataColumn ID = new DataColumn("ID", typeof(int));  //设置各列类型
            //    DataColumn Name = new DataColumn("Name", typeof(string));
            //    DataColumn Line = new DataColumn("Location", typeof(string));
            //    DataColumn Process = new DataColumn("Process", typeof(string));
            //    DataColumn Operator = new DataColumn("Operator", typeof(string));
            //    DataColumn Status = new DataColumn("Status", typeof(string));
            //    DataColumn CreationTime = new DataColumn("CreationTime", typeof(DateTime));
            //    DataColumn LastUpdate = new DataColumn("LastUpdate", typeof(DateTime));
            //    dtnew.Columns.Add(ID);  //向datatable中添加列
            //    dtnew.Columns.Add(Name);
            //    dtnew.Columns.Add(Line);
            //    dtnew.Columns.Add(Process);
            //    dtnew.Columns.Add(Operator);
            //    dtnew.Columns.Add(Status);
            //    dtnew.Columns.Add(CreationTime);
            //    dtnew.Columns.Add(LastUpdate);

            //    for (int i = 0; i < dt.Rows.Count; i++)   //遍历原数据向新datatable中赋值
            //    {
            //        dtnew.Rows.Add(dt.Rows[i].ItemArray);
            //    }
            //    foreach (DataRow dr in dtnew.Rows)  //对特定的行添加限制条件
            //    {
            //        dr[4] = _service.GetUserInfoByID(Convert.ToInt32(dr[4].ToString())).EName;
            //    }
            //}
            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }

        public ActionResult LineMatrixAdd()
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

            DataSet ds = _service.RunProcedure("uspManageEXLine", Sqlparam, "bb", ConDB.DWHDBContext);

            ViewData["Process"] = DBToSelectList(ds.Tables[0]);
            return View();
        }
        
        public ActionResult LineMatrixSaveAdd(string poststr)
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
                string errormsg = _service.RunProcedure("uspManageEXLine", sqlxml, out code, ConDB.DWHDBContext);

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
        public ActionResult LineMatrixEdit(int ID)
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

            DataSet ds = _service.RunProcedure("uspManageEXLine", Sqlparam, "bb", ConDB.DWHDBContext);
            udtEXLine am = new udtEXLine
            {
                ID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString()),
                Name = ds.Tables[0].Rows[0][1].ToString(),
                Location = ds.Tables[0].Rows[0][2].ToString(),
                ProcessID = Convert.ToInt32(ds.Tables[0].Rows[0][3].ToString())
            };

            ViewData["Process"] = DBToSelectList(ds.Tables[1], am.ProcessID);

            return View(am);
        }
        public ActionResult LineMatrixSaveEdit(string poststr)
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
                string errormsg = _service.RunProcedure("uspManageEXLine", sqlxml, out code, ConDB.DWHDBContext);

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
        public ActionResult LineMatrixDelete(string ids)
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
                string errormsg = _service.RunProcedure("uspManageEXLine", sqlxml, out code, ConDB.DWHDBContext);

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


        #region Parameter 维护扣数工位By Model, UPH, LeadTime
        public ActionResult ModelParameterIndex()
        {
            return View();
        }

        public ActionResult ModelParameterload(string code)
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

            DataSet ds = _service.RunProcedure("uspManageEXPartParameter", Sqlparam, "bb", ConDB.DWHDBContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            //var result = xs.ConvertTo<vShippingNotice>(ds.Tables[1]);
            //var dt = ds.Tables[1];
            //DataTable dtnew = new DataTable();   //新建一个对象
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    DataColumn ID = new DataColumn("ID", typeof(int));  //设置各列类型
            //    DataColumn Line = new DataColumn("Line", typeof(string));
            //    DataColumn Project = new DataColumn("Project", typeof(string));
            //    DataColumn Model = new DataColumn("Model", typeof(string));
            //    DataColumn OutputStationType = new DataColumn("OutputStationType", typeof(string));
            //    DataColumn LT = new DataColumn("LT", typeof(string));
            //    DataColumn UPH = new DataColumn("UPH", typeof(string));
            //    DataColumn Operator = new DataColumn("Operator", typeof(string));
            //    DataColumn Status = new DataColumn("Status", typeof(string));
            //    DataColumn CreationTime = new DataColumn("CreationTime", typeof(DateTime));
            //    DataColumn LastUpdate = new DataColumn("LastUpdate", typeof(DateTime));
            //    dtnew.Columns.Add(ID);  //向datatable中添加列
            //    dtnew.Columns.Add(Line);
            //    dtnew.Columns.Add(Project);
            //    dtnew.Columns.Add(Model);
            //    dtnew.Columns.Add(OutputStationType);
            //    dtnew.Columns.Add(LT);
            //    dtnew.Columns.Add(UPH);
            //    dtnew.Columns.Add(Operator);
            //    dtnew.Columns.Add(Status);
            //    dtnew.Columns.Add(CreationTime);
            //    dtnew.Columns.Add(LastUpdate);

            //    for (int i = 0; i < dt.Rows.Count; i++)   //遍历原数据向新datatable中赋值
            //    {
            //        dtnew.Rows.Add(dt.Rows[i].ItemArray);
            //    }
            //    foreach (DataRow dr in dtnew.Rows)  //对特定的行添加限制条件
            //    {
            //        dr[7] = _service.GetUserInfoByID(Convert.ToInt32(dr[7].ToString())).EName;
            //    }
            //}
            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }

        public ActionResult ModelParameterAdd()
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

            DataSet ds = _service.RunProcedure("uspManageEXPartParameter", Sqlparam, "bb", ConDB.DWHDBContext);

            ViewData["Line"] = DBToSelectList(ds.Tables[0]);
            ViewData["Project"] = DBToSelectList(ds.Tables[1]);
            ViewData["PartID"] = DBToSelectList(ds.Tables[2]);
            ViewData["OutputStationType"] = DBToSelectList(ds.Tables[3]);
            ViewData["NeedManualOutput"] = DBToSelectList(ds.Tables[4]);
            return View();
        }

        public ActionResult getDetailDropByProject(int PJID)
        {
            string dbstr = Request.Params["X_DB_MATRIX"];
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("PJID", PJID);
            dic.Add("Flag", "GetAdd");
            //dic.Add("ID", ID);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspManageEXPartParameter", Sqlparam, "bb", ConDB.DWHDBContext);
            
            //ViewData["PartID"] = DBToSelectList(ds.Tables[2]);
            //ViewData["OutputStationType"] = DBToSelectList(ds.Tables[3]);

            return JsonFormat(new { PartID = ds.Tables[2], OutputStationType = ds.Tables[3] }, "yyyyMMdd");
        }

        public ActionResult ModelParameterSaveAdd(string poststr)
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
                string errormsg = _service.RunProcedure("uspManageEXPartParameter", sqlxml, out code, ConDB.DWHDBContext);

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
        public ActionResult ModelParameterEdit(int ID)
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

            DataSet ds = _service.RunProcedure("uspManageEXPartParameter", Sqlparam, "bb", ConDB.DWHDBContext);
            udtEXPartParameter am = new udtEXPartParameter
            {
                ID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString()),
                LineID = Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString()),
                PJID = Convert.ToInt32(ds.Tables[0].Rows[0][2].ToString()),
                PartID = Convert.ToInt32(ds.Tables[0].Rows[0][3].ToString()),
                OutputStationTypeID = Convert.ToInt32(ds.Tables[0].Rows[0][4].ToString()),
                OutputLeadTime= Convert.ToInt32(ds.Tables[0].Rows[0][5].ToString()),
                UPH= Convert.ToInt32(ds.Tables[0].Rows[0][6].ToString()),
                NeedManualOutput = ds.Tables[0].Rows[0][7].ToString()
            };

            ViewData["Line"] = DBToSelectList(ds.Tables[1]);
            ViewData["Project"] = DBToSelectList(ds.Tables[2]);
            ViewData["PartID"] = DBToSelectList(ds.Tables[3]);
            ViewData["OutputStationType"] = DBToSelectList(ds.Tables[4]);
            ViewData["NeedManualOutput"] = DBToSelectList(ds.Tables[5], Convert.ToInt32(am.NeedManualOutput));

            return View(am);
        }

        public ActionResult ModelParameterEditGetSelectList(int ID)
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

            DataSet ds = _service.RunProcedure("uspManageEXPartParameter", Sqlparam, "bb", ConDB.DWHDBContext);
            
            return JsonFormat(new { PartID = ds.Tables[3], OutputStationType = ds.Tables[4] }, "yyyyMMdd");
        }
        public ActionResult ModelParameterSaveEdit(string poststr)
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
                string errormsg = _service.RunProcedure("uspManageEXPartParameter", sqlxml, out code, ConDB.DWHDBContext);

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
        public ActionResult ModelParameterDelete(string ids)
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
                string errormsg = _service.RunProcedure("uspManageEXPartParameter", sqlxml, out code, ConDB.DWHDBContext);

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

        


        #region PlanCreation 维护Plan(Line,日期，班次， , model, 数量, 是否与已存在的计划同时生产（计划序号），)， 允许更新优先级
        public ActionResult BuildPlanIndex()
        {
            return View();
        }

        public ActionResult GetBuildPlanIndexDropDownList()
        {
            string dbstr = Request.Params["X_DB_MATRIX"];
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "GetIndex");
            //dic.Add("ID", ID);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspManageEXBuildPlan", Sqlparam, "bb", ConDB.DWHDBContext);
            
            return JsonFormat(new { Shift = ds.Tables[0], Line = ds.Tables[1] }, "yyyyMMdd");
        }

        public ActionResult BuildPlanload(string Code1_PlanDate,string Code2_ShiftCode,string Code3_LineID)
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
            dic.Add("Code1_PlanDate", Code1_PlanDate);
            dic.Add("Code2_ShiftCode", Code2_ShiftCode);
            dic.Add("Code3_LineID", Code3_LineID);
            dic.Add("Flag", "Get");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspManageEXBuildPlan", Sqlparam, "bb", ConDB.DWHDBContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            //var result = xs.ConvertTo<vShippingNotice>(ds.Tables[1]);
            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }

        public ActionResult BuildPlanAdd(string Date,string Shift,string Line)
        {
            string dbstr = Request.Params["X_DB_MATRIX"];
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "GetAdd");
            dic.Add("Code1_PlanDate", Date);
            dic.Add("Code2_ShiftCode", Shift == "1" ? "Day" : "Night");
            dic.Add("Code3_LineID", Line);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspManageEXBuildPlan", Sqlparam, "bb", ConDB.DWHDBContext);
            
            ViewData["Date"] = Date;
            ViewData["Shift"] = DBToSelectList(ds.Tables[0], Convert.ToInt32(Shift == "" ? "0" : Shift));
            ViewData["Line"] = DBToSelectList(ds.Tables[1], Convert.ToInt32(Line == "" ? "0" : Line));
            ViewData["Project"] = DBToSelectList(ds.Tables[2]);
            ViewData["SortID"] =ds.Tables[5].Rows[0][0];
            //ViewData["Model"] = DBToSelectList(ds.Tables[3]);
            //ViewData["Status"] = DBToSelectList(ds.Tables[4],1);
            return View();
        }

        public ActionResult GetBuildPlanAddModelDropDownList(string PJID)
        {
            string dbstr = Request.Params["X_DB_MATRIX"];
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "GetAdd");
            dic.Add("PJID", PJID);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspManageEXBuildPlan", Sqlparam, "bb", ConDB.DWHDBContext);
            
            return JsonFormat(new { Model = ds.Tables[3]}, "yyyyMMdd");
        }

        public ActionResult GetBuildPlanAddSortID(string ID)
        {
            string dbstr = Request.Params["X_DB_MATRIX"];
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "GetAddSortIDByID");
            dic.Add("ID", ID);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspManageEXBuildPlan", Sqlparam, "bb", ConDB.DWHDBContext);

            return JsonFormat(new { SortID = ds.Tables[0].Rows[0][0] }, "yyyyMMdd");
        }

        public ActionResult BuildPlanSaveAdd(string poststr)
        {
            List<string> errMsg = new List<string>();
            try
            {
                string dbstr = Request.Params["X_DB_MATRIX"];
                int code = 0;
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, object> dic = jss.Deserialize<Dictionary<string, object>>(poststr);

                dic["UserName"] = _service.CurrentUser.Account;
                dic["ShiftCode"] = dic["ShiftCode"].ToString() == "1" ? "Day" : "Night";
                dic.Add("Flag", "AddSave");
                //dic.Add("IDS", ids);
                string sqlxml = XmlParams.CreateSqlXml(dic);
                //DataTable dt = _service.GetSelectItems(XmlParams.CreateSqlXml(dic), Request.Params["X_DB_MATRIX"]);
                string errormsg = _service.RunProcedure("uspManageEXBuildPlan", sqlxml, out code, ConDB.DWHDBContext);

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
        public ActionResult BuildPlanEdit(int ID)
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

            DataSet ds = _service.RunProcedure("uspManageEXBuildPlan", Sqlparam, "bb", ConDB.DWHDBContext);
            udtEXBuildPlan am = new udtEXBuildPlan
            {
                ID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString()),
                PlanDate = ds.Tables[0].Rows[0][1].ToString(),
                ShiftCode = ds.Tables[0].Rows[0][2].ToString(),
                Line = ds.Tables[0].Rows[0][3].ToString(),
                Project = ds.Tables[0].Rows[0][4].ToString(),
                Model = ds.Tables[0].Rows[0][5].ToString(),
                PlanQty= Convert.ToInt32(ds.Tables[0].Rows[0][6].ToString()),
                SortID = Convert.ToInt32(ds.Tables[0].Rows[0][7].ToString()),
                IssueQty = Convert.ToInt32(ds.Tables[0].Rows[0][8].ToString()),
                FinishedQty= Convert.ToInt32(ds.Tables[0].Rows[0][9].ToString()),
                needManualOutput= ds.Tables[0].Rows[0][10].ToString()
            };

            return View(am);
        }
        public ActionResult BuildPlanSaveEdit(string poststr)
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
                string errormsg = _service.RunProcedure("uspManageEXBuildPlan", sqlxml, out code, ConDB.DWHDBContext);

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
        public ActionResult Import()
        {
            //mosdataService<MosCostCenter> mds = new mosdataService<MosCostCenter>();
            string errormsg = string.Empty;
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
                    Dictionary<string, object> dic = _service.GetUserInfoDic();
                    dic["UserName"] = _service.CurrentUser.Account;
                    string sqlxml = ExcelToAll.ExcelToXml(saveFileName, dic);
                    //DataTable dt = ExcelToAll.ExcelToDataTable(saveFileName, dic);

                    int code = 0;
                    errormsg = _service.RunProcedure("uspManageEXImportPlan", sqlxml, out code, ConDB.DWHDBContext);
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

        public ActionResult BuildPlanDelete(string ids)
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
                string errormsg = _service.RunProcedure("uspManageEXBuildPlan", sqlxml, out code, ConDB.DWHDBContext);

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

        public ActionResult BuildPlanForcedClose(string ids)
        {
            List<string> errMsg = new List<string>();
            try
            {
                string dbstr = Request.Params["X_DB_MATRIX"];
                int code = 0;
                //JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Flag", "ForcedClose");
                dic.Add("IDS", ids);
                string sqlxml = XmlParams.CreateSqlXml(dic);
                //DataTable dt = _service.GetSelectItems(XmlParams.CreateSqlXml(dic), Request.Params["X_DB_MATRIX"]);
                string errormsg = _service.RunProcedure("uspManageEXBuildPlan", sqlxml, out code, ConDB.DWHDBContext);

                if (code == 0)
                {
                    ///return Json(new JsonData(JsonData.FLAG_SUCCESS, null, "OK"));
                    return Json(new JsonData(JsonData.FLAG_SUCCESS, null, "Force Close Success"));
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

        public ActionResult IssueUpdateIndex()
        {
            string dbstr = Request.Params["X_DB_MATRIX"];
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "GetIndex");
            //dic.Add("ID", ID);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspManageEXIssueUpdate", Sqlparam, "bb", ConDB.DWHDBContext);

            ViewData["IssueStation"] = DBToSelectList(ds.Tables[0]);
            return View();
        }

        public ActionResult GetIssueUpdateDropDownList(string PJID)
        {
            string dbstr = Request.Params["X_DB_MATRIX"];
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "GetDropDownList");
            dic.Add("PJID", PJID);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspManageEXIssueUpdate", Sqlparam, "bb", ConDB.DWHDBContext);

            return JsonFormat(new { Project = ds.Tables[0], PartID = ds.Tables[1] }, "yyyyMMdd");
        }

        public ActionResult IssueGetAll()
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
            dic.Add("Flag", "GetAll");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspManageEXIssueUpdate", Sqlparam, "bb", ConDB.DWHDBContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }

        public ActionResult IssueGet(string ChildData)
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
            dic.Add("Flag", "Get");
            List<ChildPN> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ChildPN>>(ChildData);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic,list)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspManageEXIssueUpdate", Sqlparam, "bb", ConDB.DWHDBContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }

        public ActionResult IssueUpdateEdit(int ID)
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

            DataSet ds = _service.RunProcedure("uspManageEXIssueUpdate", Sqlparam, "bb", ConDB.DWHDBContext);
            udtEXBuildPlan am = new udtEXBuildPlan
            {
                ID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString()),
                PlanDate = ds.Tables[0].Rows[0][1].ToString(),
                ShiftCode = ds.Tables[0].Rows[0][2].ToString(),
                Line = ds.Tables[0].Rows[0][3].ToString(),
                Project = ds.Tables[0].Rows[0][4].ToString(),
                Model = ds.Tables[0].Rows[0][5].ToString(),
                PlanQty = Convert.ToInt32(ds.Tables[0].Rows[0][6].ToString()),
                SortID = Convert.ToInt32(ds.Tables[0].Rows[0][7].ToString()),
                IssueQty = Convert.ToInt32(((ds.Tables[0].Rows[0][8] is DBNull) ? 0: ds.Tables[0].Rows[0][8]).ToString()),
                FinishedQty = Convert.ToInt32(((ds.Tables[0].Rows[0][9] is DBNull) ? 0 : ds.Tables[0].Rows[0][9]).ToString()),
                MinRequest = Convert.ToInt32(ds.Tables[0].Rows[0][10].ToString()),
                MaxRequest = Convert.ToInt32(ds.Tables[0].Rows[0][11].ToString()),
                NewIssueQty = Convert.ToInt32(((ds.Tables[0].Rows[0][12] is DBNull) ? 0 : ds.Tables[0].Rows[0][12]).ToString()),
                BPID = Convert.ToInt32(ds.Tables[0].Rows[0][13].ToString()),
            };

            return View(am);
        }

        public ActionResult IssueUpdateSaveEdit(string poststr)
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
                string errormsg = _service.RunProcedure("uspManageEXIssueUpdate", sqlxml, out code, ConDB.DWHDBContext);

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

        public ActionResult DashboardIndex()
        {
            return View();
        }

        public ActionResult GetReportIndexDropDownList()
        {
            string dbstr = Request.Params["X_DB_MATRIX"];
            Dictionary<string, object> dic = new Dictionary<string, object>();
            //dic.Add("PJID", PJID);
            dic.Add("Flag", "GetIndex");
            //dic.Add("ID", ID);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspManageEXDashboard", Sqlparam, "bb", ConDB.DWHDBContext);

            //ViewData["PartID"] = DBToSelectList(ds.Tables[2]);
            //ViewData["OutputStationType"] = DBToSelectList(ds.Tables[3]);

            return JsonFormat(new { Line = ds.Tables[0], Project = ds.Tables[1] }, "yyyyMMdd");
        }


        public ActionResult Dashboardload(string dateFrom,string dateTo,string Line,string Project)
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
            dic.Add("Line", Line);
            dic.Add("Project", Project);
            dic.Add("Flag", "Get");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspManageEXDashboard", Sqlparam, "bb", ConDB.DWHDBContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            //var result = xs.ConvertTo<vShippingNotice>(ds.Tables[1]);
            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }
    }
}