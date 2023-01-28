using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using POCApi.Core.Generic;

namespace POCApi.Core.Interfaces.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(long id, params Expression<Func<T, object>>[] includes);
        Task<T> Get(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        void Add(T entity);
        void Delete(T entity, bool forceHardDelete = false);
        void Update(T entity);
        Task<List<T>> List(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        Task<List<T>> List(List<Expression<Func<T, bool>>> expressions, params Expression<Func<T, object>>[] includes);
        Task<Collection<T>> List<TSortKey>(
            List<Expression<Func<T, bool>>> expressions,
            int pageNumber,
            int pageSize,
            Func<T, TSortKey> keySelector,
            bool isAscending,
            params Expression<Func<T, object>>[] includes);

        Task<List<T>> List(params Expression<Func<T, object>>[] includes);
        Task<bool> Exists(Expression<Func<T, bool>> expression);
        Task<bool> Exists(long id);
    }

}
