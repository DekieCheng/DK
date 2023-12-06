using SDPSubSystem.Data;
using SDPSubSystem.Model;
using SDPSubSystem.Model.AssistModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDPSubSystem.Model.ChangeOver;
using SDPSubSystem.Data;

namespace SDPSubSystem.Services.ChangeOver
{
    public class ChangeOverService<T> : BaseService where T : BaseEntity
    {
        /// <summary>
        /// 工作单元，用于创建数据存储的统一接口
        /// </summary>
        protected UnitOfWork unitOfWork = null;

        protected Repository<T> repository;
        //public DYEFService()
        //{
        //    this.repository = unitOfWork.Repository<T>();
        //}
        public ChangeOverService(string constr)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new DYEFContext(base.GetDbConn(constr)));
            this.repository = unitOfWork.Repository<T>();
        }

        public udtChangeOver GetSingle(int ID,string connstr)
        {
            udtChangeOver obj = (udtChangeOver)CommonSql.GetSingle("select * from udtChangeOver with (nolock) where ID=" + ID, connstr);
            //udtChangeOver uco = Newtonsoft.Json.JsonConverter<udtChangeOver>(obj);
            return obj;
        }
    }
}
