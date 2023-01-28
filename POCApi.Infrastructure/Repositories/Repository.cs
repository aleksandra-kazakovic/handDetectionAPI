using Microsoft.EntityFrameworkCore;
using POCApi.Core.Base.Impl;
using POCApi.Core.Base.Interfaces;
using POCApi.Core.Exceptions;
using POCApi.Core.Exceptions.Common;
using POCApi.Core.Generic;
using POCApi.Core.Interfaces;
using POCApi.Core.Interfaces.IRepositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace POCApi.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : EntityBase<long>
    {
        protected readonly DbFactory _dbFactory;
        protected DbSet<T> _dbSet;
        protected DbSet<T> DbSet
        {
            get => _dbSet ?? (_dbSet = _dbFactory.DbContext.Set<T>());
        }

        public Repository(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<T> Get(long id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = DbSet.AsQueryable();
            if (includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            query = query.Where(x => x.Id == id);
            var entity = await query.AsNoTracking().FirstOrDefaultAsync();
            if (typeof(IDeletableEntity).IsAssignableFrom(typeof(T)))
            {
                if (entity == null || ((IDeletableEntity)entity).IsDeleted)
                {
                    entity = null;
                }
            }
            return entity;
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = DbSet.AsNoTracking().AsQueryable();
            if (includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.Where(expression).FirstOrDefaultAsync();
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
        }

        public void Delete(T entity, bool forceHardDelete = false)
        {
            if (!forceHardDelete && typeof(IDeletableEntity).IsAssignableFrom(typeof(T)))
            {
                ((IDeletableEntity)entity).IsDeleted = true;
                DbSet.Update(entity);
            }
            else
                DbSet.Remove(entity);
        }

        public async Task<List<T>> List(params Expression<Func<T, object>>[] includes)
        {
            return await List(new List<Expression<Func<T, bool>>> { }, includes);
        }

        public async Task<List<T>> List(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            return await List(new List<Expression<Func<T, bool>>> { expression }, includes);
        }

        public async Task<List<T>> List(List<Expression<Func<T, bool>>> expressions, params Expression<Func<T, object>>[] includes)
        {
            var list = new List<T>();
            var query = DbSet.AsQueryable();
            foreach (var exp in expressions)
            {
                query = query.Where(exp);
            }
            if (includes.Length == 0)
            {
                list = await query.AsNoTracking().ToListAsync();
            }
            else
            {
                foreach (var include in includes)
                {
                    query = query.AsNoTracking().Include(include);
                }
                list = await query.AsNoTracking().ToListAsync();
            }
            return list;
        }

        public async Task<Collection<T>> List<TSortKey>(
            List<Expression<Func<T, bool>>> expressions,
            int pageNumber,
            int pageSize,
            Func<T, TSortKey> sortBySelector,
            bool isAscending,
            params Expression<Func<T, object>>[] includes)
        {
            if (pageNumber <= 0 || pageSize <= 0 || pageSize > 5000)
            {
                // TODO: proper error message
                throw new ValidationException(ErrorDictionary.ErrInternalServerError);
            }
            var list = new List<T>();
            var query = DbSet.AsQueryable();
            foreach (var exp in expressions)
            {
                query = query.Where(exp);
            }
            if (includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            if (sortBySelector != null)
            {
                if (isAscending)
                {
                    query = query.OrderBy(sortBySelector).AsQueryable();
                }
                else
                {
                    query = query.OrderByDescending(sortBySelector).AsQueryable();
                }
            }
            list = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            long count = query.Count();
            return await Task.FromResult(new Collection<T>
            {
                TotalCount = count,
                Data = list
            });
        }

        public async Task<bool> Exists(Expression<Func<T, bool>> expression)
        {
            var query = DbSet.AsQueryable();
            query = query.Where(expression);
            return (await query.CountAsync()) > 0;
        }

        public async Task<bool> Exists(long id)
        {
            return await Exists(x => x.Id == id);
        }
    }
}
