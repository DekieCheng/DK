using SDPSubSystem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Data
{
    public class Validations
    {
        public static List<string> EXCEPTS = new List<string>() { "ID", "CreateBy", "CreationTime", "UpdateBy", "LastUpdate", "StatusID" };
        /// <summary>
        /// 校验实体对像
        /// </summary>
        /// <param name="model">要校验的对象</param>
        /// <param name="LangID">信息提示的语言编号</param>
        /// <param name="dic">校验输出的信息（Key为字段，Value为提示信息）</param>
        /// <returns></returns>
        public static bool ValidateAttribute(BaseEntity model,int LangID,out Dictionary<string,string> dic)
        {
            return ValidateAttribute(model, LangID, new List<string>(),new List<string>(), out dic);
        }
        /// <summary>
        /// 校验实体对像
        /// </summary>
        /// <param name="model">要校验的对象</param>
        /// <param name="LangID">信息提示的语言编号</param>
        /// <param name="Fields">要校验的字段</param>
        /// <param name="dic">校验输出的信息（Key为字段，Value为提示信息）</param>
        /// <returns></returns>
        public static bool ValidateAttribute(BaseEntity model, int LangID, List<string> Fields,out Dictionary<string, string> dic)
        {
            return ValidateAttribute(model, LangID, Fields, new List<string>(), out dic);
        }
        /// <summary>
        /// 校验实体对象
        /// </summary>
        /// <param name="model">要校验的对象</param>
        /// <param name="LangID">信息提示的语言编号</param>
        /// <param name="Fields">要校验的字段</param>
        /// <param name="exceptFields">不校验的字段</param>
        /// <param name="dic">校验输出的信息（Key为字段，Value为提示信息）</param>
        /// <returns></returns>
        public static bool ValidateAttribute(BaseEntity model,int LangID,List<string> Fields,List<string> exceptFields, out Dictionary<string,string> dic)
        {
            dic = new Dictionary<string, string>();
            foreach (var item in model.GetType().GetProperties())
            {
                if (EXCEPTS.Contains(item.Name) || exceptFields.Contains(item.Name)) continue;
                if (Fields.Count>0 && !Fields.Contains(item.Name)) continue;
                
                if (item.GetCustomAttributes(typeof(RequiredAttribute), false).Length > 0 && (item.GetValue(model) == null || (item.GetValue(model) != null && string.IsNullOrEmpty(item.GetValue(model).ToString()))))
                {
                    dic.Add(item.Name, CacheData.DataCache.GetMessage(LangID, "E000001")); continue;
                }
                var obj = item.GetCustomAttributes(typeof(StringLengthAttribute), false);
                if (obj != null && obj.Length > 0 && item.GetValue(model) != null
                    && ((StringLengthAttribute[])obj)[0].MaximumLength < item.GetValue(model).ToString().Length)
                {
                    dic.Add(item.Name, CacheData.DataCache.GetMessage(LangID, "E000002")); continue;
                }
            }
            return dic.Count == 0;
        }
    }
}
