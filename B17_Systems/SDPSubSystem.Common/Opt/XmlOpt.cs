using SDPSubSystem.Model.AssistModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SDPSubSystem.Common.Opt
{
    public class XmlOpt
    {
        /// <summary>
        /// 读取语言xml获取列表数据
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<LangMsg> GetLangMessage(string path)
        {
            List<LangMsg> result = new List<LangMsg>();
            XDocument doc = XDocument.Load(path + "//language.xml");
            List<Language> langs = (from d in doc.Descendants("data")
                                    select new Language
                                    {
                                        LangID = int.Parse(d.Attribute("lang").Value),
                                        Name = d.Value,
                                        FileName = d.Attribute("file").Value
                                    }).OrderBy(d => d.LangID).ToList();
            foreach (var lang in langs)
            {
                XDocument xd = XDocument.Load(path + "//" + lang.FileName);
                result.AddRange((from d in xd.Descendants("data")
                                 select new LangMsg
                                 {
                                     Code = d.Attribute("code").Value,
                                     Message = d.Value,
                                     LangID = lang.LangID
                                 }).OrderBy(d => d.Code).ToList());
            }
            return result;
        }
        /// <summary>
        /// 获取语言
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<Language> GetLanguage(string path)
        {
            XDocument doc = XDocument.Load(path + "//language.xml");
            List<Language> langs = (from d in doc.Descendants("data")
                                    select new Language
                                    {
                                        LangID = int.Parse(d.Attribute("lang").Value),
                                        Name = d.Value,
                                        FileName = d.Attribute("file").Value
                                    }).OrderBy(d => d.LangID).ToList();
            return langs;
        }

        /// <summary>
        /// 读取无需授权xml的数据
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<MvcEntity> GetNoAuthorize(string path)
        {
            List<MvcEntity> result = new List<MvcEntity>();
            XDocument doc = XDocument.Load(path + "//NoAuthorizeRequired.xml");
            return (from d in doc.Descendants("data")
                    select new MvcEntity
                    {
                        Area = d.Attribute("Area").Value,
                        Controller = d.Attribute("Controller").Value,
                        Action = d.Attribute("Action").Value
                    }).OrderBy(d => d.Area).ThenBy(d => d.Controller).ThenBy(d => d.Action).ToList();
        }
    }
}
