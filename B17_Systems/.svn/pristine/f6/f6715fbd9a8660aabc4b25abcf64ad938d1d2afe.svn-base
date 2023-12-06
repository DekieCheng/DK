using SDPSubSystem.Model.AssistModels;
using SDPSubSystem.Model.SysModels;
using SDPSubSystem.Services.SysServices;
using SDPSubSystem.Web.Filter;
using SDPSubSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SDPSubSystem.Web.Controllers
{
    public class HomeController : BaseController
    {
        private SYS_Menu_PermissionService _service = new SYS_Menu_PermissionService();
        private SYS_Menu_SystemsService _sysService = new SYS_Menu_SystemsService();
        
        /// <summary>
        /// 子系统页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            SYS_Menu_Systems system = _sysService.GetSingle(d => d.Code == _service.CurrentUser.SysCode);
            ViewData["LangID"] = _service.CurrentUser.LangID;
            ViewData["Menus"] = _service.GetAccessPages();
            ViewData["MatrixCode"] = (system != null && system.IsMatrix == "Y" ? (GetMatrix(d => d.MatrixCode == system.MatrixCode, SysEnums.SelectOptions.None)) : new List<SelectListItem>());
            return View(system);
        }
        /// <summary>
        /// 用于主系统跳转用户注册
        /// </summary>
        /// <returns></returns>
        public ActionResult Logon()
        {
            bool IsLogin = false;
            try
            {
                IsLogin = _service.SetLogin(HttpContext.Response,Request.Params["CB058676508846EE9811D3A6D30FA5C9"]);
                if (IsLogin)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("ErrorPage", "Home");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home", new { msg = ex.Message });
            }
        }

        public ActionResult LogOff()
        {
            _service.CurrentUser = null;
            FormsAuthentication.SignOut();
            return View();
        }

        public string GetPageUrl(string PageUrl)
        {
            string url =  _service.GetSDPUrl() + PageUrl + (PageUrl.IndexOf('?') > 0 ? "&" : "?");
            return url + "CB058676508846EE9811D3A6D30FA5C9=" + _sysService.GetEncryptionUserInfo();
        }

        /// <summary>
        /// 系统的Home主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Main()
        {
            SYS_Menu_Systems system = _sysService.GetSingle(d => d.Code == _service.CurrentUser.SysCode);
            return View(system);
        }

        public ActionResult NoPermission()
        {
            return View();
        }

        public ActionResult ErrorPage(string msg = "")
        {
            ViewData["msg"] = msg;
            return View();
        }
    }
}