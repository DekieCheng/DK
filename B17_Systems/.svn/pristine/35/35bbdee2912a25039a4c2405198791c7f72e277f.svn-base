using SDPSubSystem.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Data
{
    public class UnitOfWork :IDisposable
    {
        private readonly DbContext _context;
        private bool disposed;
        private Dictionary<string, object> repositories;

        public UnitOfWork(DbContext context)
        {
            this._context = context;
        }

        #region dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool v)
        {
            if (!disposed)
            {
                if (v)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }
        #endregion

        public void Save()
        {
            _context.SaveChanges();
        }

        public Repository<T> Repository<T>() where T : BaseEntity
        {
            if (repositories == null)
            {
                repositories = new Dictionary<string, object>();
            }
            var type = typeof(T).Name;
            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
                repositories.Add(type, repositoryInstance);
            }
            return (Repository<T>)repositories[type];
        }
    }
}
