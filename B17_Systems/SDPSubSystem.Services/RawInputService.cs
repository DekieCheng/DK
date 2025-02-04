﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SDPSubSystem.Services
{
    public class RawInputService:BaseService
    {
        public DataTable GetSelectItems(string SqlXml, string MatrixCode = null)
        {
            return base.Query("udpB17GetDrpList", SqlXml,  base.GetDbConn(MatrixCode)).Tables[0];
        }

        /// <summary>
        /// 获取用户当前登录用户的基本信息（用于存储过程参数）
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> GetUserInfoDic(Dictionary<string, object> dic = null)
        {
            if (dic == null)
            {
                dic = new Dictionary<string, object>();
            }
            dic.Add("EmployeeNO", CurrentUser.EmployeeNO);
            dic.Add("SiteCode", CurrentUser.SiteCode);
            dic.Add("Building", CurrentUser.Building);
            dic.Add("Account", CurrentUser.Account);
            dic.Add("LangID", CurrentUser.LangID);
            dic.Add("SysCode", CurrentUser.SysCode);
            return dic;
        }

    }
}
