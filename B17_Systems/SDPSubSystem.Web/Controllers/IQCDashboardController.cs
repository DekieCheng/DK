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
    public class IQCDashboardController : BaseController
    {
        // GET: Dashboard
        //VolcanoWebToolService _service = new VolcanoWebToolService();
        RawInputService _service = new RawInputService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult KanBanIQCheckRT()
        {
            string msg = "";
            DataSet ds = null;
            try
            {
                IDataParameter[] sqlparameter1 =
                     {
                      };
                ds = _service.RunProcedure("udpMaterialIQCCheckData", sqlparameter1, "result", ConDB.LifeSpanContext);


                msg = "OK";
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }


            return View(ds.Tables[0]);
        }

    }
}