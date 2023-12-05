using SDPSubSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDPSubSystem.Web.Common
{
    public static class HtmlHelperExpressions
    {
        private static BaseService _server = new BaseService();
        /// <summary>
        /// 根据编码获取对应语言的字符串
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetLang(this HtmlHelper helper,string code)
        {
            return _server.GetMessage(code);
        }
        /// <summary>
        /// 根据编码获取对应语言的字符串
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="code"></param>
        /// <param name="LangID"></param>
        /// <returns></returns>
        public static string GetLang(this HtmlHelper helper, string code,int LangID)
        {
            return _server.GetMessage(code, LangID);
        }
        /// <summary>
        /// 获取当前用户或默认的语言
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static int GetLangID(this HtmlHelper helper)
        {
            return _server.CurrentUser == null ? 1 : _server.CurrentUser.LangID;
        }
    }
}