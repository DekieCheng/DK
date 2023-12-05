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
    public class FEElectricityDashboardController : BaseController
    {
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
            DataSet ds = _impservice.RunProcedure("udpLNLoadElectricityByCategoryName", sqlparameter, "bb", ConDB.PPAppContext);


            IDataParameter[] sqlparameter2 =
            {
               new SqlParameter("@CategoryName", "空调系统用电")
            };
            DataSet ds2 = _impservice.RunProcedure("udpLNLoadElectricityByCategoryName", sqlparameter2, "bb", ConDB.PPAppContext);

            IDataParameter[] sqlparameter3 =
            {
               new SqlParameter("@CategoryName", "B17写字楼用电")
            };
            DataSet ds3 = _impservice.RunProcedure("udpLNLoadElectricityByCategoryName", sqlparameter3, "bb", ConDB.PPAppContext);

            IDataParameter[] sqlparameter4 =
            {
               new SqlParameter("@CategoryName", "B17太阳能用电量")
            };
            DataSet ds4 = _impservice.RunProcedure("udpLNLoadElectricityByCategoryName", sqlparameter4, "bb", ConDB.PPAppContext);

            IDataParameter[] sqlparameter5 =
            {
               new SqlParameter("@CategoryName", "B10 Mech用电")
            };
            DataSet ds5 = _impservice.RunProcedure("udpLNLoadElectricityByCategoryName", sqlparameter5, "bb", ConDB.PPAppContext);

            IDataParameter[] sqlparameter6 =
            {
               new SqlParameter("@CategoryName", "B17 Mech用电")
            };
            DataSet ds6 = _impservice.RunProcedure("udpLNLoadElectricityByCategoryName", sqlparameter6, "bb", ConDB.PPAppContext);

            IDataParameter[] sqlparameter7 =
            {
               new SqlParameter("@CategoryName", "压缩空气系统用电")
            };
            DataSet ds7 = _impservice.RunProcedure("udpLNLoadElectricityByCategoryName", sqlparameter7, "bb", ConDB.PPAppContext);

            IDataParameter[] sqlparameter8 =
            {
               new SqlParameter("@CategoryName", "楼顶排风&其它用电")
            };
            DataSet ds8 = _impservice.RunProcedure("udpLNLoadElectricityByCategoryName", sqlparameter8, "bb", ConDB.PPAppContext);


            IDataParameter[] sqlparameter9 =
            {
               new SqlParameter("@CategoryName", "B17 PCBA照明用电")
            };
            DataSet ds9 = _impservice.RunProcedure("udpLNLoadElectricityByCategoryName", sqlparameter9, "bb", ConDB.PPAppContext);

            IDataParameter[] sqlparameter10 =
            {
               new SqlParameter("@CategoryName", "B17一楼原材料仓用电")
            };
            DataSet ds10 = _impservice.RunProcedure("udpLNLoadElectricityByCategoryName", sqlparameter10, "bb", ConDB.PPAppContext);


            IDataParameter[] sqlparameter11 =
            {
               new SqlParameter("@CategoryName", "B17一楼PCBA车间用电")
            };
            DataSet ds11 = _impservice.RunProcedure("udpLNLoadElectricityByCategoryName", sqlparameter11, "bb", ConDB.PPAppContext);

            IDataParameter[] sqlparameter12 =
            {
               new SqlParameter("@CategoryName", "B17二楼车间用电")
            };
            DataSet ds12 = _impservice.RunProcedure("udpLNLoadElectricityByCategoryName", sqlparameter12, "bb", ConDB.PPAppContext);


            IDataParameter[] sqlparameter13 =
            {
               new SqlParameter("@CategoryName", "B17三楼A车间用电")
            };
            DataSet ds13 = _impservice.RunProcedure("udpLNLoadElectricityByCategoryName", sqlparameter13, "bb", ConDB.PPAppContext);


            IDataParameter[] sqlparameter14 =
            {
               new SqlParameter("@CategoryName", "B17三楼B车间用电")
            };
            DataSet ds14 = _impservice.RunProcedure("udpLNLoadElectricityByCategoryName", sqlparameter14, "bb", ConDB.PPAppContext);


            IDataParameter[] sqlparameter15 =
            {
               new SqlParameter("@CategoryName", "B17四楼A车间用电")
            };
            DataSet ds15 = _impservice.RunProcedure("udpLNLoadElectricityByCategoryName", sqlparameter15, "bb", ConDB.PPAppContext);


            IDataParameter[] sqlparameter16 =
            {
               new SqlParameter("@CategoryName", "B17四楼B车间用电")
            };
            DataSet ds16 = _impservice.RunProcedure("udpLNLoadElectricityByCategoryName", sqlparameter16, "bb", ConDB.PPAppContext);

            return JsonFormat(new { Block1 = ds.Tables[0], Block2 = ds2.Tables[0], Block3 = ds3.Tables[0], Block4 = ds4.Tables[0], Block5 = ds5.Tables[0], Block6 = ds6.Tables[0], Block7 = ds7.Tables[0], Block8 = ds8.Tables[0], Block9 = ds9.Tables[0], Block10 = ds10.Tables[0], Block11 = ds11.Tables[0], Block12 = ds12.Tables[0], Block13 = ds13.Tables[0], Block14 = ds14.Tables[0], Block15 = ds15.Tables[0], Block16 = ds16.Tables[0] }, "yyyyMMdd");
        }

        public ActionResult main()
        {
            return View();
        }

    }
}