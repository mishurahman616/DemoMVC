using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Repositories
{
    public interface IRepositoryBase<TEntity, TKey> where TEntity : class, IEntity<TKey> where TKey : IComparable
    {
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void Edit(TEntity entity);
        Task EditAsync(TEntity entity);
        TEntity GetById(TKey id);
        Task<TEntity> GetByIdAsync(TKey id);
        IList<TEntity> GetAll();
        Task<IList<TEntity>> GetAllAsync();
        int GetCount(Expression<Func<TEntity, bool>> filter=null);
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter=null);
        void Remove(TKey id);
        void Remove(TEntity entity);
        void Remove(Expression<Func<TEntity, bool>> filter);
        Task RemoveAsync(TKey id);
        Task RemoveAsync(TEntity entity);
        Task RemoveAsync(Expression<Func<TEntity, bool>> filter);



    }
}
