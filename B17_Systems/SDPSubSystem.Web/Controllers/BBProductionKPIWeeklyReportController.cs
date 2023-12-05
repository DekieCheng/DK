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

namespace SDPSubSystem.Web.Controllers
{
    public class BBProductionKPIWeeklyReportController : BaseController
    {
        RawInputService _service = new RawInputService();

        public ActionResult DLAttritionIndex()
        {
            return View();
        }

        public ActionResult DLAttritionLoad()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "Get");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspBBKPIDashboardDLAttrition", Sqlparam, "bb", ConDB.PPAppContext);

            //return JsonFormat(new { Data = ds.Tables[0], xData= ds.Tables[1], barData=ds.Tables[2],  linedataForDL= ds.Tables[3], linedataForTarget= ds.Tables[4] }, "yyyyMMdd");
            return JsonFormat(new {  xData= ds.Tables[0], barData=ds.Tables[1],  linedataForDL= ds.Tables[2], linedataForTarget= ds.Tables[3] }, "yyyyMMdd");
        }

        public ActionResult DownTimeIndex()
        {
            return View();
        }

        public ActionResult DownTimeLoad()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "Get");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspBBKPIDashboardDownTime", Sqlparam, "bb", ConDB.PPAppContext);

            return JsonFormat(new { Data = ds.Tables[0], Data2 = ds.Tables[1] }, "yyyyMMdd");
        }

        public ActionResult OutputIndex()
        {
            return View();
        }

        public ActionResult OutputLoad()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "Get");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspBBKPIDashboardOutput", Sqlparam, "bb", ConDB.PPAppContext);

            return JsonFormat(new { Data = ds.Tables[0] }, "yyyyMMdd");
        }


        public ActionResult YieldIndex()
        {
            return View();
        }

        public ActionResult YieldLoad()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "Get");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspBBKPIDashboardYield", Sqlparam, "bb", ConDB.PPAppContext);

            return JsonFormat(new { Data = ds.Tables[0], Title = ds.Tables[1] }, "yyyyMMdd");
        }

        public ActionResult DLERateIndex()
        {
            return View();
        }

        public ActionResult DLERateLoad()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "Get");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspBBKPIDashboardDLERate", Sqlparam, "bb", ConDB.PPAppContext);

            return JsonFormat(new { Data = ds.Tables[0] }, "yyyyMMdd");
        }

        public ActionResult ScrapIndex()
        {
            return View();
        }

        public ActionResult ScrapLoad()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "Get");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspBBKPIDashboardScrap", Sqlparam, "bb", ConDB.PPAppContext);

            return JsonFormat(new { Data = ds.Tables[0], Title = ds.Tables[1],Legend= ds.Tables[2] }, "yyyyMMdd");
        }

        public ActionResult IDMIndex()
        {
            return View();
        }

        public ActionResult IDMLoad()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "Get");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspBBKPIDashboardIDM", Sqlparam, "bb", ConDB.PPAppContext);

            return JsonFormat(new { Data = ds.Tables[0], Title = ds.Tables[1] }, "yyyyMMdd");
        }

        public ActionResult SixSScoreIndex()
        {
            return View();
        }

        public ActionResult SixSScoreLoad()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Flag", "Get");
            IDataParameter[] Sqlparam =
            {
                new SqlParameter("@sqlxml", XmlParams.CreateSqlXml(dic)),
                new SqlParameter("@msg",SqlDbType.NVarChar,2000),
            };
            Sqlparam[1].Direction = ParameterDirection.Output;

            DataSet ds = _service.RunProcedure("uspBBKPIDashboard6S", Sqlparam, "bb", ConDB.PPAppContext);

            return JsonFormat(new { barData = ds.Tables[0], xData = ds.Tables[1], linedataForDL = ds.Tables[2], linedataForTarget = ds.Tables[3], Title = ds.Tables[4], Legend = ds.Tables[5] }, "yyyyMMdd");
        }

    }
}