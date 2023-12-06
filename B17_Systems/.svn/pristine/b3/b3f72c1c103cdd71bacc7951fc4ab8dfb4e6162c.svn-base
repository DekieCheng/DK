using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDPSubSystem.Web.Models
{
    public class JsonData
    {
        /// <summary>
        /// 成功
        /// </summary>
        public const string FLAG_SUCCESS = "success";

        /// <summary>
        /// 错误
        /// </summary>
        public const string FLAG_ERROR = "error";

        /// <summary>
        /// 警告
        /// </summary>
        public const string FLAG_WARNING = "waring";

        /// <summary>
        /// 正常
        /// </summary>
        public const string FLAG_NORMAL = "normal";

        public JsonData(string flag)
        {
            this.Flag = flag;
            ControlMessage = new Dictionary<string, string>();
        }

        /// <summary>
        /// 处理返回JSON数据信息
        /// </summary>
        /// <param name="flag">处理标记</param>
        /// <param name="data">返回数据</param>
        public JsonData(string flag, object data)
            : this(flag)
        {

            this.Data = data;
        }

        public bool IsSuccess
        {
            get
            {
                return this.Flag != FLAG_ERROR;
            }
        }

        /// <summary>
        /// 处理返回JSON数据信息
        /// </summary>
        /// <param name="flag">处理标记</param>
        /// <param name="data">返回数据</param>
        /// <param name="msg">返回信息</param>
        public JsonData(string flag, object data, object msg)
            : this(flag, data)
        {
            this.Msg = msg;
        }


        public JsonData(string flag, object data, object msg, Dictionary<string, string> contorlMsg)
            : this(flag, data, msg)
        {
            this.ControlMessage = contorlMsg;
        }


        /// <summary>
        /// 处理标志
        /// </summary>
        public string Flag { get; set; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 返回的信息
        /// </summary>
        public object Msg { get; set; }

        public Dictionary<string, string> ControlMessage { get; set; }
    }
}