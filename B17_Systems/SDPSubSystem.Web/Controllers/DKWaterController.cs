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
using SDPSubSystem.Web.Common;
using SDPSubSystem.Services.Common;

namespace SDPSubSystem.Web.Controllers
{
    public class DKWaterController : BaseController
    {
        // GET: DKWater
        // GET: Dashboard
        //VolcanoWebToolService _service = new VolcanoWebToolService();
        RawInputService _service = new RawInputService();
        ImpersonService _impservice = new ImpersonService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            IDataParameter[] sqlparameter =
            {
               new SqlParameter("@CategoryName", "B17厂房总用电量")
            };
            DataSet ds = _impservice.RunProcedure("udpLNMainDashboardOfWaterMeter", sqlparameter, "bb", ConDB.PPAppContext);

            return JsonFormat(new { Block1 = ds.Tables[0] }, "yyyyMMdd");
        }

        public ActionResult main()
        {
            return View();
        }
    }
}