using SDPSubSystem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Data
{
    public class Repository<T> where T: BaseEntity
    {
        /// <summary>
        /// 数据上下文
        /// </summary>
        private readonly DbContext _context;
        string errorMessage = string.Empty;

        /// <summary>
        /// 实体问题字段
        /// </summary>
        private DbSet<T> entities;

        /// <summary>
        /// 实体封装
        /// </summary>
        public DbSet<T> Entities
        {
            get
            {
                if (entities == null)
                {
                    return entities = _context.Set<T>();
                }
                else
                {
                    return entities;//AsNoTracking
                }
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context"></param>
        public Repository(DbContext context)
        {
            this._context = context;
        }
        /// <summary>
        /// 新增单个实体数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        public void Insert(T entity, bool isSave = true)
        {
            try
            {
                if (entity == null) { throw new ArgumentNullException("entity"); }
                else
                {
                    this.Entities.Add(entity);
                    if (isSave) _context.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validation in ex.EntityValidationErrors)
                {
                    foreach (var item in validation.ValidationErrors)
                    {
                        errorMessage += string.Format("Property:{0} Error:{1}", item.PropertyName, item.ErrorMessage) + Environment.NewLine;
                    }
                }
                throw new Exception(errorMessage, ex);
            }
        }

        /// <summary>
        /// 修改单个实体数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        public void Update(T entity, bool isSave = true)
        {
            try
            {
                if (entity == null) { throw new ArgumentNullException("entity"); }
                else
                {
                    this._context.Entry(entity).State = EntityState.Modified;
                    if (isSave) _context.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validation in ex.EntityValidationErrors)
                {
                    foreach (var item in validation.ValidationErrors)
                    {
                        errorMessage += string.Format("Property:{0} Error:{1}", item.PropertyName, item.ErrorMessage) + Environment.NewLine;
                    }
                }
                throw new Exception(errorMessage, ex);
            }
        }
        /// <summary>
        /// 修改实体指定的字段值
        /// </summary>
        /// <param name="entity">变化实体对象 需包括唯一主键</param>
        /// <param name="fields">要修改的字段</param>
        public void Update(T entity, List<string> fields, bool isSave = true)
        {
            try
            {
                foreach (var item in entity.GetType().GetProperties())
                {
                    if (fields.Contains(item.Name)) continue;
                    if (item.GetCustomAttributes(typeof(RequiredAttribute), false).Length > 0 && item.GetValue(entity) == null)
                    {
                        if (item.PropertyType == typeof(string))
                            item.SetValue(entity, "a");
                        else if (item.PropertyType == typeof(DateTime))
                            item.SetValue(entity, DateTime.Now);
                    }
                }
                this.Entities.Attach(entity);
                foreach (var item in fields)
                {
                    _context.Entry<T>(entity).Property(item).IsModified = true;
                }
                if (isSave) _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validation in ex.EntityValidationErrors)
                {
                    foreach (var item in validation.ValidationErrors)
                    {
                        errorMessage += string.Format("Property:{0} Error:{1}", item.PropertyName, item.ErrorMessage) + Environment.NewLine;
                    }
                }
                throw new Exception(errorMessage, ex);
            }
        }

        /// <summary>
        /// 删除单个实体数据
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity, bool isSave = true)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                else
                {
                    this._context.Entry(entity).State = EntityState.Deleted;
                    if (isSave) _context.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validation in ex.EntityValidationErrors)
                {
                    foreach (var item in validation.ValidationErrors)
                    {
                        errorMessage += string.Format("Property:{0} Error:{1}", item.PropertyName, item.ErrorMessage) + Environment.NewLine;
                    }
                }
                throw new Exception(errorMessage, ex);
            }
        }
        /// <summary>
        /// 根据ID 删除实体
        /// </summary>
        /// <param name="Ids"></param>
        /// <param name="isSave"></param>
        public void DeleteByID(List<int> Ids,bool isSave = true)
        {
            try
            {
                this.Entities.RemoveRange(this.Entities.Where(d => Ids.Contains(d.ID)));
                if (isSave) _context.SaveChanges();
            }
            catch(DbEntityValidationException ex)
            {
                foreach (var validation in ex.EntityValidationErrors)
                {
                    foreach (var item in validation.ValidationErrors)
                    {
                        errorMessage += string.Format("Property:{0} Error:{1}", item.PropertyName, item.ErrorMessage) + Environment.NewLine;
                    }
                }
                throw new Exception(errorMessage, ex);
            }
        }

    }
}
