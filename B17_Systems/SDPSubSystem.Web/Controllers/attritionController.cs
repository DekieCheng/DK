﻿using SDPSubSystem.Data;
using SDPSubSystem.Services;
using SDPSubSystem.Services.DWHService;
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

namespace SDPSubSystem.Web.Controllers
{
    public class attritionController : BaseController
    {
        // GET: attrition
        CommonService _service = new CommonService();
        
        #region index page 
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetattrtionData(string timetype)
        {
            IDataParameter[] sqlparameter =
            {
               new SqlParameter("@Line", "0"),
               new SqlParameter("@TimeType",timetype),
               new SqlParameter("@startdate",null),
               new SqlParameter("@endDate",null),
               new SqlParameter("@reportType",0)
            };
            DataSet ds = _service.RunProcedure("MES_GetAttritionrateByline", sqlparameter, "bb", ConDB.DWHDBContext);
            IDataParameter[] sqlparameter1 =
            {
               new SqlParameter("@Line", "0"),
               new SqlParameter("@TimeType",timetype),
               new SqlParameter("@startdate",null),
               new SqlParameter("@endDate",null),
               new SqlParameter("@reportType",1)
            };
            DataSet ds1 = _service.RunProcedure("MES_GetAttritionrateByline", sqlparameter1, "bb", ConDB.DWHDBContext);

            return JsonFormat(new { Time = ds.Tables[0], Line = ds1.Tables[0] }, "yyyyMMdd");
        }

        #endregion


        public ActionResult GetAttrtionDataByTime(string startdate,string endDate,string timeType)
        {
            IDataParameter[] sqlparameter =
            {
               new SqlParameter("@Line", "0"),
               new SqlParameter("@TimeType",timeType),
               new SqlParameter("@startdate",startdate),
               new SqlParameter("@endDate",endDate),
               new SqlParameter("@reportType",1)
            };
            DataSet ds = _service.RunProcedure("MES_GetAttritionrateByline", sqlparameter, "bb", ConDB.DWHDBContext);
            

            return JsonFormat(new { Line = ds.Tables[0] }, "yyyyMMdd");
        }


        //[Permission("4a52725f-cf1c-4e8f-b25e-a6fafd684e23")]
        public ActionResult station()
        {

            IDataParameter[] sqlparameter =
            {

            };
            DataSet ds = _service.RunProcedure("MES_GetEEKPIline", sqlparameter, "bb", ConDB.DWHDBContext);
            ViewData["Line"] = DBToSelectList( ds.Tables[0]);


            //string startTime = Request.QueryString["startTime"];
            //string endTime = Request.QueryString["endTime"];
            //string Line = Request.QueryString["Line"];
            //IDataParameter[] sqlparameter1 =
            //{
            //   new SqlParameter("@Line",Line),
            //   new SqlParameter("@TimeType",1),
            //   new SqlParameter("@startdate",startTime),
            //   new SqlParameter("@endDate",endTime),
            //   new SqlParameter("@reportType",2)
            //};
            //DataSet ds1 = _service.RunProcedure("MES_GetAttritionrateByline", sqlparameter1, "bb", ConDB.DWHDBContext);

            
            return View();
        }

        public ActionResult GetstationData(string poststr)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, object> dic = jss.Deserialize<Dictionary<string, object>>(poststr);

            IDataParameter[] sqlparameter =
            {
               new SqlParameter("@Line", dic["Line"].ToString()),
               new SqlParameter("@TimeType",Convert.ToInt32( dic["TimeType"].ToString())),
               new SqlParameter("@startdate",dic["StartTime"].ToString().Length==0?null: dic["StartTime"].ToString()),
               new SqlParameter("@endDate",dic["EndTime"].ToString().Length==0?null:dic["EndTime"].ToString()),
               new SqlParameter("@reportType",2)
            };
            DataSet ds = _service.RunProcedure("MES_GetAttritionrateByline", sqlparameter, "bb", ConDB.DWHDBContext);

            IDataParameter[] sqlparameter1 =
            {
               new SqlParameter("@Line", dic["Line"].ToString()),
               new SqlParameter("@TimeType",Convert.ToInt32(dic["TimeType"].ToString())),
               new SqlParameter("@startdate",dic["StartTime"].ToString().Length==0?null: dic["StartTime"].ToString()),
               new SqlParameter("@endDate",dic["EndTime"].ToString().Length==0?null:dic["EndTime"].ToString()),
               new SqlParameter("@reportType",3)
            };
            DataSet ds1 = _service.RunProcedure("MES_GetAttritionrateByline_second", sqlparameter1, "bb", ConDB.DWHDBContext);
            
            return JsonFormat(new { Time = ds.Tables[0],Line=ds1.Tables[0] }, "yyyyMMdd");
        }
        
        public ActionResult GetstationDataByLine()
        {
            string poststr = Request.QueryString["poststr"];
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, object> dic = jss.Deserialize<Dictionary<string, object>>(poststr);
            
            IDataParameter[] sqlparameter1 =
            {
               new SqlParameter("@Line", dic["Line"].ToString()),
               new SqlParameter("@TimeType",Convert.ToInt32(dic["TimeType"].ToString())),
               new SqlParameter("@startdate",dic["StartTime"].ToString().Length==0?null: dic["StartTime"].ToString()),
               new SqlParameter("@endDate",dic["EndTime"].ToString().Length==0?null:dic["EndTime"].ToString()),
               new SqlParameter("@reportType",3)
            };
            DataSet ds1 = _service.RunProcedure("MES_GetAttritionrateByline", sqlparameter1, "bb", ConDB.DWHDBContext);

            int total = Convert.ToInt32(ds1.Tables[0].Rows[0][0].ToString());
            var result = ds1.Tables[0];
            int draw = Convert.ToInt32(Request.Params["draw"]);


            return JsonFormat(new DataTablesResultInfo(result, draw, total, total));
        }

        public ActionResult kpibyline()
        {
            return View();
        }
        public ActionResult getKPIData()
        {
            IDataParameter[] sqlparameter1 =
            {
            };
            DataSet ds1 = _service.RunProcedure("MES_CatchEEKPI", sqlparameter1, "bb", ConDB.DWHDBContext);
            return JsonFormat(new { KPIData = ds1.Tables[0] }, "yyyyMMdd");
        }

        public ActionResult attrtionreport()
        {
            return View();
        }
        public ActionResult Getattrtionreportdata(string timetype)
        {
            IDataParameter[] sqlparameter =
            {
               new SqlParameter("@Line", "0"),
               new SqlParameter("@TimeType",timetype),
               new SqlParameter("@startdate",null),
               new SqlParameter("@endDate",null),
               new SqlParameter("@reportType",0)
            };
            DataSet ds = _service.RunProcedure("MES_GetAttritionrateByline", sqlparameter, "bb", ConDB.DWHDBContext);
            IDataParameter[] sqlparameter1 =
            {
               new SqlParameter("@Line", "0"),
               new SqlParameter("@TimeType",timetype),
               new SqlParameter("@startdate",null),
               new SqlParameter("@endDate",null),
               new SqlParameter("@reportType",1)
            };
            DataSet ds1 = _service.RunProcedure("MES_GetAttritionrateByline", sqlparameter1, "bb", ConDB.DWHDBContext);

            return JsonFormat(new { Time = ds.Tables[0], Line = ds1.Tables[0] }, "yyyyMMdd");
        }

        public ActionResult picattrtion()
        {
            return View();
        }
        public ActionResult Getattrtionpicdata(string timetype)
        {
            IDataParameter[] sqlparameter =
            {
               new SqlParameter("@Line", "0"),
               new SqlParameter("@TimeType",timetype),
               new SqlParameter("@startdate",null),
               new SqlParameter("@endDate",null),
               new SqlParameter("@reportType",0)
            };
            DataSet ds = _service.RunProcedure("MES_GetAttritionrateByline", sqlparameter, "bb", ConDB.DWHDBContext);
            IDataParameter[] sqlparameter1 =
            {
               new SqlParameter("@Line", "0"),
               new SqlParameter("@TimeType",timetype),
               new SqlParameter("@startdate",null),
               new SqlParameter("@endDate",null),
               new SqlParameter("@reportType",1)
            };
            DataSet ds1 = _service.RunProcedure("MES_GetAttritionrateByline", sqlparameter1, "bb", ConDB.DWHDBContext);

            return JsonFormat(new { Time = ds.Tables[0], Line = ds1.Tables[0] }, "yyyyMMdd");
        }

        public ActionResult AttritionByLine()
        {
            string linename = Request.QueryString["Line"].ToString();
            ViewData["Line"] = linename;
            return View();
        }
        public ActionResult GetattritionDatabyLine(string Line)
        {
            IDataParameter[] sqlparameter1 =
            {
                 new SqlParameter("@Line", Line)
            };
            DataSet ds1 = _service.RunProcedure("MES_GetAttritionrateByline_SLDDashboard", sqlparameter1, "bb", ConDB.DWHDBContext);
            return JsonFormat(new { attrition = ds1.Tables[0] }, "yyyyMMdd");
        }
    }
}