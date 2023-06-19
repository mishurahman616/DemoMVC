using Library.Domain.Entities;
using Library.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Dynamic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Library.Persistence.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey> where TKey : IComparable
    {
        protected DbContext _dbContext;
        protected DbSet<TEntity> _dbSet;
        protected int commandTimeOut { get; set; }

        public Repository(DbContext dbContext) 
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
            commandTimeOut = 300;
        }
        public virtual void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual void Edit(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual async Task EditAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                _dbSet.Attach(entity);
                _dbContext.Entry(entity).State = EntityState.Modified;
            });
        }

        public virtual IList<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, 
            bool isTrackingOff = false)
        {
            IQueryable<TEntity> query = _dbSet;
            if(filter is not null)
            {
                query = query.Where(filter);
            }
            if(include is not null)
            {
                query = include(query);
            }
            if(orderBy is not null)
            {
                query = orderBy(query);
            }
            if(isTrackingOff)
            {
                return query.AsNoTracking().ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual (IList<TEntity> data, int total, int totalDisplay) Get(
            Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, 
            int pageIndex = 1, int pageSize = 10, bool isTrackingOff = false)
        {
            IQueryable<TEntity> query = _dbSet;
            int total = query.Count();
            int totalDisplay = total;
            if(filter is not null)
            {
                query = query.Where(filter);
                totalDisplay = query.Count(filter);
            }
            if(include is not null)
            {
                query = include(query);
            }
            if(orderBy is not null)
            {
                query = orderBy(query).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            else
            {
                query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            if (isTrackingOff)
            {
                return (query.AsNoTracking().ToList(), total, totalDisplay);
            }
            else
            {
                return (query.ToList(), total, totalDisplay);
            }
        }

        public virtual IList<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if(query is not null)
                query= query.Where(filter);
            if(include is not null)
                query = include(query);
            return query.ToList();
        }

        public virtual IList<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual async Task<IList<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<IList<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, 
            bool isTrackingOff = false)
        {
            var query = _dbSet as IQueryable<TEntity>;
            if(filter is not null)
                query = query.Where(filter);
            if(include is not null)
                query = include(query);
            if(orderBy is not null)
                query = orderBy(query);
            if (isTrackingOff) return await query.AsNoTracking().ToListAsync();
            else return await query.ToListAsync();
        }

        public virtual async Task<(IList<TEntity> data, int total, int totalDisplay)> GetAsync(
            Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, 
            int pageIndex = 1, int pageSize = 10, bool isTrackingOff = false)
        {
            IQueryable<TEntity> query = _dbSet;
            int total = query.Count();
            int totalDisplay = total;
            if (filter != null)
            {
                query = query.Where(filter);
                totalDisplay = query.Count();
            }
            if (include != null)
                query = include(query);
            if (orderBy != null)
                query = orderBy(query).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            if (isTrackingOff) return  (await query.AsNoTracking().ToListAsync(), total, totalDisplay);
            else return (await query.ToListAsync(), total, totalDisplay);
        }

        public virtual async Task<IList<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if(filter !=null)
                query = query.Where(filter);
            if(include!= null) 
                query = include(query);
            return await query.ToListAsync();
        }

        public virtual async Task<IEnumerable<TResult>> GetAsync<TResult>(
            Expression<Func<TEntity, TResult>>? selector, 
            Expression<Func<TEntity, bool>> predicate = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, 
            bool disableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();
            if(include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null)
                return await orderBy(query).Select(selector!).ToListAsync(cancellationToken);
            else return await query.Select(selector!).ToListAsync(cancellationToken);
        }

        public virtual TEntity GetById(TKey id)
        {
            return _dbSet.Find(id);
        }

        public virtual async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            var query = _dbSet as IQueryable<TEntity>; 
            if(filter is not null)
            {
                query = query.Where(filter);
            }
            return query.Count();
        }

        public virtual async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            var query = _dbSet as IQueryable<TEntity>;
            if(filter is not null)
            {
                query = query.Where(filter);
            }
            return await query.CountAsync();
        }

        public virtual IList<TEntity> GetDynamic(
            Expression<Func<TEntity, bool>> filter = null, 
            string orderBy = null, 
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, 
            bool isTrackingOff = false)
        {
            IQueryable<TEntity> query = _dbSet;
            if(filter is not null) 
                query = query.Where(filter);
            if (include != null)
                query = include(query);
            if (orderBy != null)
                query = query.OrderBy(orderBy);
            if (isTrackingOff)
                return query.AsNoTracking().ToList();
            else return query.ToList();

        }

        public virtual (IList<TEntity> data, int total, int totalDisplay) GetDynamic(
            Expression<Func<TEntity, bool>> filter, 
            string orderBy = null, 
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, 
            int pageIndex = 1, int pageSize = 10, bool isTrackingOff = false)
        {
            IQueryable<TEntity> query = _dbSet;
            int total = query.Count();
            int totalDisplay = query.Count();
            if (filter is not null)
            {
                query = query.Where(filter);
                totalDisplay = query.Count();
            }    
            if(include is not null)
            {
                query = include(query);
            }
            if(orderBy is not null)
            {
                query = query.OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            else
            {
                query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            if(isTrackingOff) return (query.AsNoTracking().ToList(), total, totalDisplay);
            else return (query.ToList(), total, totalDisplay);
        }

        public virtual async Task<IList<TEntity>> GetDynamicAsync(
            Expression<Func<TEntity, bool>> filter = null, 
            string orderBy = null, 
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, 
            bool isTrackingOff = false)
        {
            IQueryable<TEntity> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if(include != null)
            {
                query = include(query);
            }
            if(orderBy != null)
            {
                query = query.OrderBy(orderBy);
            }
            if (isTrackingOff) return await query.AsNoTracking().ToListAsync();
            else return await query.ToListAsync();
        }

        public virtual async Task<(IList<TEntity> data, int total, int totalDisplay)> GetDynamicAsync(
            Expression<Func<TEntity, bool>> filter, 
            string orderBy = null, 
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, 
            int pageIndex = 1, int pageSize = 10, bool isTrackingOff = false)
        {
            IQueryable<TEntity> query = _dbSet;
            int total = query.Count();
            int totalDisplay = query.Count();
            if (filter is not null)
            {
                query = query.Where(filter);
                totalDisplay = query.Count();
            }
            if (include is not null)
            {
                query = include(query);
            }
            if (orderBy is not null)
            {
                query = query.OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            else
            {
                query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            if (isTrackingOff) return ( await query.AsNoTracking().ToListAsync(), total, totalDisplay);
            else return (await query.ToListAsync(), total, totalDisplay);
        }

        public virtual void Remove(TKey id)
        {
            var entityToDelete = _dbSet.Find(id);
            if(entityToDelete != null)
            {
               Remove(entityToDelete);
            }
        }

        public virtual void Remove(TEntity entity)
        {
            if(_dbSet.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);
            _dbSet.Remove(entity);

        }

        public virtual void Remove(Expression<Func<TEntity, bool>> filter)
        {
            _dbSet.RemoveRange(_dbSet.Where(filter));
        }

        public virtual async Task RemoveAsync(TKey id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
              await RemoveAsync(entity);
            }
        }

        public virtual async Task RemoveAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                if (_dbContext.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
                _dbSet.Remove(entity);
            });
        }

        public virtual async Task RemoveAsync(Expression<Func<TEntity, bool>> filter)
        {
            await Task.Run(() =>
            {
                _dbSet.RemoveRange(_dbSet.Where(filter));
            });
        }

        public virtual async Task<TResult> SingleOrDefault<TResult>(
            Expression<Func<TEntity, TResult>>? selector, 
            Expression<Func<TEntity, bool>> predicate = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, 
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();
            if(predicate != null) query = query.Where(predicate);
            if (include != null) query = include(query);
            if (orderBy != null) query = orderBy(query);
            return (query.Select(selector!).FirstOrDefault())!;
        }
    }
}
