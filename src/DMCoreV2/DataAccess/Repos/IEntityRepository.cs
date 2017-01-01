using DMCoreV2.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DMCoreV2.DataAccess.Repos
{
    public interface IEntityRepository<T>
    {
        IQueryable<T> All { get; }
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProeprties);
        T Find(int id);
        void insertOrUpdate(T entity);
        void Delete(int id);
        void Save();
    }

    public interface IUserRepository<T> : IEntityRepository<AuthUser>
    {
    }
}
