using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDPSubSystem.Common.Security
{
    public class JsonStatus
    {

        /*
           0：								Login Success(登录成功)
           1：								TIMEOUT(请求超时)
           2：							    AUTH_FAILED(票据码错误)
           3：              		            User_NOT_EXISTS(用户不存在 )
           4：               		        User_Lock(用户已锁定)
           5：               		        Password error(密码错误)
          -1：								UNKNOWN_EXCEPTION(未知错误)
       */
        public int Code { get; set; }
        /*
          IF  0 OK(操作成功) Then Return User Object
        */
        //public VmSsoUser Data { get; set; }

        /*
           0：								Login Success(登录成功)
           1：								TIMEOUT(请求超时)
           2：							    AUTH_FAILED(票据码错误)
           3：              		            User_NOT_EXISTS(用户不存在 )
           4：               		        User_Lock(用户已锁定)
           5：               		        Password error(密码错误)
          -1：								UNKNOWN_EXCEPTION(未知错误)
       */
        public string conn { get; set; }
        public string EMssage { get; set; }

        public string CMssage { get; set; }
    }
}