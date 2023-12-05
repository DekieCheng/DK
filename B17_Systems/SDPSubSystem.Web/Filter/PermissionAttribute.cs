using SDPSubSystem.Model.SysModels;
using SDPSubSystem.Services.SysServices;
using SDPSubSystem.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SDPSubSystem.Web.Filter
{
    public class PermissionAttribute: ActionFilterAttribute
    {
        private static SYS_Menu_PermissionService _service = new SYS_Menu_PermissionService();
        private bool _isVerifyPermission = true;
        private bool _isUsageLogs = true;
        private string _permissionKey = string.Empty;
        /// <summary>
        /// 菜单权限的Key
        /// </summary>
        public string PermissionKey
        {
            set { _permissionKey = value; }
            get { return _permissionKey; }
        }

        /// <summary>
        /// 是否验证该用户是否有该Action的权限
        /// </summary>
        public bool IsVerifyPermission {
            set { _isVerifyPermission = value; }
            get { return _isVerifyPermission; }
        }
        /// <summary>
        /// 是否记录该功能被使用一次（记录到SYS_UsaeLogs表中)
        /// </summary>
        public bool IsUsageLogs
        {
            set { _isUsageLogs = value; }
            get { return _isUsageLogs; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="isVerifyPermission"></param>
        public PermissionAttribute(string permissionKey,bool isVerifyPermission = true, bool isUsageLogs = true)
        {
            this._permissionKey = permissionKey;
            this._isVerifyPermission = isVerifyPermission;
            this._isUsageLogs = isUsageLogs;
        }
        /// <summary>
        /// 在Action执行前判断是否有权限
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!_isVerifyPermission) { return; }
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            
            if (!_service.IsHavePagePermission(_permissionKey))
            {
                filterContext.HttpContext.Response.StatusCode = 401;
                filterContext.Result = new RedirectResult("/Home/NoPermission");
            }
        }
        /// <summary>
        /// 记录SYS_UsageLogs
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (!_isUsageLogs) { return; }
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            _service.SaveUsageLogs(PermissionKey, MvcFunctions.GetIP());
        }
    }
}