using SDPSubSystem.Data;
using SDPSubSystem.Services;
using SDPSubSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDPSubSystem.Web.Controllers
{
    public class HumiController : BaseController
    {
        RawInputService _service = new RawInputService();
        // GET: Humi
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Gethumidata()
        {
            string msg = "";
            DataSet ds = null;
            try
            {
                IDataParameter[] sqlparameter1 =
                     {
                      };
                ds = _service.RunProcedure("udp_getDekTempdata", sqlparameter1, "result", ConDB.DWHDBContext);
                msg = "OK";
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
            return JsonFormat(new JsonData(JsonData.FLAG_ERROR, new { data = ds.Tables[0]}, msg));

        }
    }
}