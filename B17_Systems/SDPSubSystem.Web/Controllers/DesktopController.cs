﻿using SDPSubSystem.Common.Customized;
using SDPSubSystem.Common.Opt;
using SDPSubSystem.Data;
using SDPSubSystem.Model.Desktop;
using SDPSubSystem.Services;
using SDPSubSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SDPSubSystem.Web.Controllers
{
    public class DesktopController : BaseController
    {
        RawInputService _service = new RawInputService();

        public ActionResult IndexForCampus()
        {
            return View();
        }

        //TODO:测试Task List 列表
        public ActionResult GetCampusDesktopData()
        {
            string dbstr = Request.Params["X_DB_MATRIX"];
            Dictionary<string, object> dic = new Dictionary<string, object>();
            IDataParameter[] Sqlparam =
                {
                   new SqlParameter("@source", ""),
                   new SqlParameter("@building",""),
                   new SqlParameter("@category",""),
                   new SqlParameter("@Type",4)
                };


            DataSet ds = _service.RunProcedure("MES_CatchITDesktopKPI", Sqlparam, "bb", ConDB.ITDMDBContext);

            return JsonFormat(new { TotalData = ds.Tables[0], FailedData = ds.Tables[1], CoverageByBuilding = ds.Tables[2], SumTotalData = ds.Tables[3], SumFailedTotalData = ds.Tables[4], SumCoverageData = ds.Tables[5] }, "yyyyMMdd");
        }

        public ActionResult GetDesktopFailedDataForCampus(string source, string category, string date, string building)
        {
            ViewBag.source = source;
            ViewBag.category = category;
            ViewBag.date = date;
            ViewBag.building = building;
            return View();

        }

        public ActionResult GetDesktopFailedDataForCampus2(string poststr)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, object> dic = jss.Deserialize<Dictionary<string, object>>(poststr);

            string msg = "";
            DataSet ds = null;
            try
            {
                IDataParameter[] sqlparameter =
                {
                   new SqlParameter("@source", dic["source"].ToString()),
                   new SqlParameter("@building",dic["building"].ToString()),
                   new SqlParameter("@date",dic["date"].ToString()),
                   new SqlParameter("@category",dic["category"].ToString()),
                   new SqlParameter("@Type",5)
                };
                ds = _service.RunProcedure("MES_CatchITDesktopKPI", sqlparameter, "result", ConDB.ITDMDBContext);
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
            msg = "OK";
            return JsonFormat(new JsonData(JsonData.FLAG_ERROR, new { FailedData = ds.Tables[0] }, msg));

        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetDashboardIndexDropDownList()
        {
            string dbstr = Request.Params["X_DB_MATRIX"];
            Dictionary<string, object> dic = new Dictionary<string, object>();
            //dic.Add("PJID", PJID);
            //dic.Add("Flag", "GetIndex");
            //dic.Add("ID", ID);
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspManageDesktopByBuilding", Sqlparam, "bb", ConDB.ITDMDBContext);

            //ViewData["PartID"] = DBToSelectList(ds.Tables[2]);
            //ViewData["OutputStationType"] = DBToSelectList(ds.Tables[3]);

            return JsonFormat(new { Building = ds.Tables[0] }, "yyyyMMdd");
        }

        public ActionResult GetDesktopData(string Building)
        {
            string msg = "";
            DataSet ds = null;
            DataSet ds1 = null;
            try
            {
                IDataParameter[] sqlparameter =
                {
                   new SqlParameter("@source", "AD"),
                   new SqlParameter("@building",Building),
                   new SqlParameter("@category",""),
                   new SqlParameter("@Type",1)
                };
                ds = _service.RunProcedure("MES_CatchITDesktopKPI", sqlparameter, "result", ConDB.ITDMDBContext);


                IDataParameter[] sqlparameter1 =
                {
                   new SqlParameter("@source", "NONAD"),
                   new SqlParameter("@building",Building),
                   new SqlParameter("@date",""),
                   new SqlParameter("@category",""),
                   new SqlParameter("@Type",1)
                };
                ds1 = _service.RunProcedure("MES_CatchITDesktopKPI", sqlparameter1, "result", ConDB.ITDMDBContext);
                msg = "OK";
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
            return JsonFormat(new JsonData(JsonData.FLAG_ERROR, new { AD = ds.Tables[0], NONAD = ds1.Tables[0] }, msg));

        }

        public ActionResult GetDesktopLineData(string Building)
        {
            string msg = "";
            DataSet ds = null;
            try
            {
                IDataParameter[] sqlparameter =
                {
                   new SqlParameter("@source", ""),
                   new SqlParameter("@building",Building),
                   new SqlParameter("@date",""),
                   new SqlParameter("@category",""),
                   new SqlParameter("@Type",2)
                };
                ds = _service.RunProcedure("MES_CatchITDesktopKPI", sqlparameter, "result", ConDB.ITDMDBContext);
                msg = "OK";
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
            return JsonFormat(new JsonData(JsonData.FLAG_ERROR, new { Line = ds.Tables[0] }, msg));

        }

        public ActionResult GetDesktopFailedData(string source, string category, string date, string building)
        {
            ViewBag.source = source;
            ViewBag.category = category;
            ViewBag.date = date;
            ViewBag.building = building;
            return View();

        }

        public ActionResult GetDesktopFailedData2(string poststr)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, object> dic = jss.Deserialize<Dictionary<string, object>>(poststr);

            string msg = "";
            DataSet ds = null;
            try
            {
                IDataParameter[] sqlparameter =
                {
                   new SqlParameter("@source", dic["source"].ToString()),
                   new SqlParameter("@building",dic["building"].ToString()),
                   new SqlParameter("@date",dic["date"].ToString()),
                   new SqlParameter("@category",dic["category"].ToString()),
                   new SqlParameter("@Type",3)
                };
                ds = _service.RunProcedure("MES_CatchITDesktopKPI", sqlparameter, "result", ConDB.ITDMDBContext);
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
            msg = "OK";
            return JsonFormat(new JsonData(JsonData.FLAG_ERROR, new { FailedData = ds.Tables[0] }, msg));

        }

        public ActionResult Owner()
        {
            return View();
        }

        public ActionResult loadOwner(string code)
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

            DataSet ds = _service.RunProcedure("udpmanageDesktopComputerInfo", Sqlparam, "bb", ConDB.ITDMDBContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            //var result = xs.ConvertTo<vShippingNotice>(ds.Tables[1]);
            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
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

            DataSet ds = _service.RunProcedure("udpmanageDesktopComputerInfo", Sqlparam, "bb", ConDB.ITDMDBContext);
            udtDesktopComputerInfo dc = new udtDesktopComputerInfo
            {
                ID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString()),
                ComputerName = ds.Tables[0].Rows[0][1].ToString(),
                Building = ds.Tables[0].Rows[0][2].ToString(),
                Owner = Convert.ToInt32(ds.Tables[0].Rows[0][3].ToString() == "" ? "0" : (ds.Tables[0].Rows[0][3].ToString())),
                Remark = ds.Tables[0].Rows[0][4].ToString(),
                BuildingID = Convert.ToInt32(ds.Tables[0].Rows[0][5].ToString()),
            };
            ViewData["Owner"] = DBToSelectList(ds.Tables[1], dc.Owner);
            ViewData["Building"] = DBToSelectList(ds.Tables[2], dc.BuildingID);

            return View(dc);
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
                dic.Add("Operator", _service.CurrentUser.Account);
                string sqlxml = XmlParams.CreateSqlXml(dic);
                //DataTable dt = _service.GetSelectItems(XmlParams.CreateSqlXml(dic), Request.Params["X_DB_MATRIX"]);
                string errormsg = _service.RunProcedure("udpmanageDesktopComputerInfo", sqlxml, out code, ConDB.ITDMDBContext);

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



        public ActionResult OwnerMatrixIndex()
        {
            return View();
        }

        public ActionResult OwnerMatrixAdd()
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

            DataSet ds = _service.RunProcedure("udpmanageDesktopComputerOwnerInfo", Sqlparam, "bb", ConDB.ITDMDBContext);

            return View();
        }
        public ActionResult OwnerMatrixLoad(string code)
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

            DataSet ds = _service.RunProcedure("udpmanageDesktopComputerOwnerInfo", Sqlparam, "bb", ConDB.ITDMDBContext);

            total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            //var result = xs.ConvertTo<vShippingNotice>(ds.Tables[1]);
            var result = ds.Tables[1];
            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }


        public ActionResult OwnerMatrixEdit(int ID)
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

            DataSet ds = _service.RunProcedure("udpmanageDesktopComputerOwnerInfo", Sqlparam, "bb", ConDB.ITDMDBContext);
            udtDesktopComputerOwnerMatrix am = new udtDesktopComputerOwnerMatrix
            {
                ID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString()),
                Name = ds.Tables[0].Rows[0][1].ToString(),
                Email = ds.Tables[0].Rows[0][2].ToString()
            };

            return View(am);
        }
        public ActionResult OwnerMatrixSaveAdd(string poststr)
        {
            List<string> errMsg = new List<string>();
            try
            {
                string dbstr = Request.Params["X_DB_MATRIX"];
                int code = 0;
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, object> dic = jss.Deserialize<Dictionary<string, object>>(poststr);

                string Name = dic["Name"].ToString();
                string mail = _service.GetUserInfoByEName(Name).MailAddress;

                dic["Email"] = mail;

                dic.Add("Flag", "AddSave");
                //dic.Add("IDS", ids);
                string sqlxml = XmlParams.CreateSqlXml(dic);
                //DataTable dt = _service.GetSelectItems(XmlParams.CreateSqlXml(dic), Request.Params["X_DB_MATRIX"]);
                string errormsg = _service.RunProcedure("udpmanageDesktopComputerOwnerInfo", sqlxml, out code, ConDB.ITDMDBContext);

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
        public ActionResult OwnerMatrixSaveEdit(string poststr)
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
                string errormsg = _service.RunProcedure("udpmanageDesktopComputerOwnerInfo", sqlxml, out code, ConDB.ITDMDBContext);

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
        public ActionResult OwnerMatrixDelete(string ids)
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
                string errormsg = _service.RunProcedure("udpmanageDesktopComputerOwnerInfo", sqlxml, out code, ConDB.ITDMDBContext);

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

        public ActionResult DashboardByB17Dept()
        {
            return View();
        }


        public ActionResult GetDesktopDashboardByB17Dept(string poststr)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, object> dic = jss.Deserialize<Dictionary<string, object>>(poststr);

            string msg = "";
            DataSet ds = null;
            try
            {
                IDataParameter[] sqlparameter =
                {
                   new SqlParameter("@source", ""),
                   new SqlParameter("@building",dic["building"].ToString()),
                   new SqlParameter("@date",""),
                   new SqlParameter("@category",""),
                   new SqlParameter("@Dept",dic["Dept"].ToString()),
                   new SqlParameter("@Type","1")
                };
                ds = _service.RunProcedure("Mes_CatchDestkopKOIByDept", sqlparameter, "result", ConDB.ITDMDBContext);
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
            msg = "OK";
            return JsonFormat(new JsonData(JsonData.FLAG_ERROR, new { data = ds.Tables[0] }, msg));

        }

        public ActionResult GetDesktopFailedDataByDept(string Dept, string category, string building)
        {
            ViewBag.Dept = Dept;
            ViewBag.category = category;
            ViewBag.building = building;
            return View();

        }

        public ActionResult GetDesktopFailedDataByDept2(string poststr)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, object> dic = jss.Deserialize<Dictionary<string, object>>(poststr);

            string msg = "";
            DataSet ds = null;
            try
            {
                IDataParameter[] sqlparameter =
                {
                   new SqlParameter("@Dept", dic["Dept"].ToString()),
                   new SqlParameter("@building",dic["building"].ToString()),
                   new SqlParameter("@category",dic["category"].ToString()),
                   new SqlParameter("@Type",2)
                };
                ds = _service.RunProcedure("Mes_CatchDestkopKOIByDept", sqlparameter, "result", ConDB.ITDMDBContext);
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
            msg = "OK";
            return JsonFormat(new JsonData(JsonData.FLAG_ERROR, new { FailedData = ds.Tables[0] }, msg));

        }

        public ActionResult Building()
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
                    errormsg = _service.RunProcedure("uspImportComputerBuildingMasterData", sqlxml, out code, ConDB.ITDMDBContext);
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


    }
}
