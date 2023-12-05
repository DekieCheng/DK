using SDPSubSystem.Common.Opt;
using SDPSubSystem.Common.Security;
using SDPSubSystem.Model.AssistModels;
using SDPSubSystem.Model.SysModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Data.CacheData
{
    public class DataCache
    {
        protected static UnitOfWork unitOfWork = new UnitOfWork(new EFSYSContext());
        protected static Repository<SYS_DBConnection> _repository = null;

        #region 通用
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="code"></param>
        public static void RemoveCache(string code)
        {
            CacheOpt.RemoveCache(code);
        }

        #endregion


        #region Language
        /// <summary>
        /// 语言列表
        /// </summary>
        public static List<Language> Languages
        {
            get
            {
                var obj = CacheOpt.GetCache("Language");
                if (obj == null)
                {
                    obj = XmlOpt.GetLanguage(System.Web.HttpContext.Current.Server.MapPath("~/bin/App_Data/"));
                    CacheOpt.SetCache("Language", obj);
                }
                return obj as List<Language>;
            }
        }

        /// <summary>
        /// 语言缓存列表
        /// </summary>
        public static List<LangMsg> LangMessages
        {
            get
            {
                var obj = CacheOpt.GetCache("LangMsg");
                if (obj == null)
                {
                    obj = XmlOpt.GetLangMessage(System.Web.HttpContext.Current.Server.MapPath("~/bin/App_Data/"));
                    CacheOpt.SetCache("LangMsg", obj);
                }
                return obj as List<Model.AssistModels.LangMsg>;
            }
        }
        /// <summary>
        /// 根据语言代码和代号获取语言信息
        /// </summary>
        /// <param name="LangID"></param>
        /// <param name="Code"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string GetMessage(int LangID,string Code,params string[] args)
        {
            var LangMsg = LangMessages.FirstOrDefault(d => d.LangID == LangID && d.Code == Code);
            return LangMsg == null ? "" : string.Format(LangMsg.Message, args);
        }
        /// <summary>
        /// 根据语言代码代号列表获取语言信息列表
        /// </summary>
        /// <param name="LangID"></param>
        /// <param name="codes"></param>
        /// <returns></returns>
        public static List<LangMsg> GetMessages(int LangID,List<string> codes)
        {
            return LangMessages.Where(d => codes.Contains(d.Code) && d.LangID == LangID).ToList();
        }
        
        #endregion

        #region No Authorize Required
        /// <summary>
        /// 无需授权访问的Url对象
        /// </summary>
        public static List<MvcEntity> NoAuthorize
        {
            get
            {
                var obj = CacheOpt.GetCache("NoAuthorize");
                if (obj == null)
                {
                    obj = XmlOpt.GetNoAuthorize(System.Web.HttpContext.Current.Server.MapPath("~/bin/App_Data/"));
                    CacheOpt.SetCache("NoAuthorize", obj);
                }
                return obj as List<MvcEntity>;
            }
        }

        public static bool IsExistsNoAuthorize(MvcEntity entity)
        {
            return NoAuthorize.Count(d => d.Area.ToUpper() == entity.Area.ToUpper() && d.Controller.ToUpper() == entity.Controller.ToUpper() && d.Action.ToUpper() == entity.Action.ToUpper()) > 0;
        }
        #endregion

        #region DBConnections

        public static List<SYS_DBConnection> DBConnections
        {
            get
            {
                var obj = CacheOpt.GetCache("DBConns");
                if (obj == null)
                {
                    _repository = unitOfWork.Repository<SYS_DBConnection>();
                    obj = _repository.Entities.AsNoTracking().Where(d => d.StatusID == 1).ToList();
                    //foreach (var o in (List<SYS_DBConnection>)obj)
                    //{//连接池解密
                    //    o.DBConn = DESSecurity.Decrypt(o.DBConn);
                    //}
                    CacheOpt.SetCache("DBConns", obj);
                }
                return obj as List<SYS_DBConnection>;
            }
        }
        /// <summary>
        /// 根据数据库连接Code获取连接池
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetDbConn(string code)
        {
            var model = DBConnections.FirstOrDefault(d => d.Code == code);
            return model == null ? "" : model.DBConn;
        }

        #endregion
    }
}
