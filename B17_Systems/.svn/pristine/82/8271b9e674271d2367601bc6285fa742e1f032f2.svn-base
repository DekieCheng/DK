using SDPSubSystem.Data;
using SDPSubSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Services
{
    public class EFSYSService<T> : BaseService where T : BaseEntity
    {
        /// <summary>
        /// 工作单元，用于创建数据存储的统一接口
        /// </summary>
        protected UnitOfWork unitOfWork = new UnitOfWork(new EFSYSContext());

        protected Repository<T> repository;
        public EFSYSService()
        {
            this.repository = unitOfWork.Repository<T>();
        }
        /// <summary>
        /// 获取Model的分页查询
        /// </summary>
        /// <typeparam name="Tkey">排序键</typeparam>
        /// <param name="predicate">查询条件</param>
        /// <param name="start">记录开始数</param>
        /// <param name="rows">截取记录数</param>
        /// <param name="Total">总记录数</param>
        /// <param name="orderby">排序条件</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        public virtual List<T> Get<Tkey>(Expression<Func<T, bool>> predicate, int start, int rows, out int Total, Func<T, Tkey> orderby = null, bool isAsc = true)
        {
            Total = repository.Entities.Count(predicate);
            if (orderby == null)
            {
                return repository.Entities.AsNoTracking().Where(predicate).OrderByDescending(d => d.ID).Skip(start).Take(rows).ToList();
            }
            else
            {
                if (isAsc)
                {
                    return repository.Entities.AsNoTracking().Where(predicate).OrderBy(orderby).Skip(start).Take(rows).ToList();
                }
                else
                {
                    return repository.Entities.AsNoTracking().Where(predicate).OrderByDescending(orderby).Skip(start).Take(rows).ToList();
                }
            }
        }

        /// <summary>
        /// 根据条件获取表数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        public virtual List<T> Get(Expression<Func<T, bool>> predicate)
        {
            return repository.Entities.AsNoTracking().Where(predicate).ToList();
        }
        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        public virtual T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return repository.Entities.AsNoTracking().FirstOrDefault(predicate);
        }
        /// <summary>
        /// 新增记录
        /// </summary>
        /// <param name="model"></param>
        public virtual void Add(T model, out Dictionary<string, string> dic)
        {
            model.CreateBy = string.IsNullOrEmpty(model.CreateBy) ? (CurrentUser != null ? CurrentUser.EmployeeNO : model.CreateBy) : model.CreateBy;
            model.CreationTime = DateTime.Now;
            model.StatusID = 1;
            int LangID = CurrentUser != null ? CurrentUser.LangID :0;
            if (Validations.ValidateAttribute(model, LangID, out dic))
            {
                repository.Insert(model);
            }
        }

        /// <summary>
        /// 修改记录
        /// </summary>
        /// <param name="model"></param>
        public virtual void Update(T model, out Dictionary<string, string> dic)
        {
            model.UpdateTime = DateTime.Now;
            model.UpdateBy = string.IsNullOrEmpty(model.UpdateBy) ? (CurrentUser != null ? CurrentUser.EmployeeNO : model.UpdateBy) : model.UpdateBy;
            int LangID = CurrentUser != null ? CurrentUser.LangID : 0;
            if (Validations.ValidateAttribute(model, LangID, out dic))
            {
                repository.Update(model);
            }
        }
        /// <summary>
        /// 修改记录（只修改指定字段）
        /// </summary>
        /// <param name="model">要修改的实体</param>
        /// <param name="fields">要更新的字段</param>
        public virtual void Update(T model, List<string> fields, out Dictionary<string, string> dic)
        {
            model.UpdateTime = DateTime.Now;
            model.UpdateBy = string.IsNullOrEmpty(model.UpdateBy) ? (CurrentUser != null ? CurrentUser.EmployeeNO : model.UpdateBy) : model.UpdateBy;
            if (!fields.Contains("UpdateBy")) { fields.Add("UpdateBy"); }
            if (!fields.Contains("UpdateTime")) { fields.Add("UpdateTime"); }
            int LangID = CurrentUser != null ? CurrentUser.LangID : 0;
            if (Validations.ValidateAttribute(model, LangID, fields, out dic))
            {
                repository.Update(model, fields);
            }
        }
        /// <summary>
        /// 根据ID批量逻辑删除数据(字段标识删除)
        /// </summary>
        /// <param name="ids"></param>
        public virtual void LogicDelete(List<int> ids,string UpdateBy = null)
        {
            List<T> models = repository.Entities.Where(d => ids.Contains(d.ID)).ToList();
            foreach (T m in models)
            {
                m.StatusID = 0;
                m.UpdateTime = DateTime.Now;
                m.UpdateBy = string.IsNullOrEmpty(UpdateBy) ? (CurrentUser != null ? CurrentUser.EmployeeNO : m.UpdateBy) : UpdateBy;
                repository.Update(m, false);
            }
            unitOfWork.Save();
        }

        /// <summary>
        /// 物理删除实体
        /// </summary>
        /// <param name="model"></param>
        public virtual void Delete(T model)
        {
            repository.Delete(model);
        }
        /// <summary>
        /// 根据ID批量物理删除
        /// </summary>
        /// <param name="ids"></param>
        public virtual void DeleteByID(List<int> ids)
        {
            repository.DeleteByID(ids);
        }
    }
}
