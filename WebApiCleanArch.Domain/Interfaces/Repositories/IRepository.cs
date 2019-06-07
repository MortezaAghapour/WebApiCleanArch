using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApiCleanArch.Domain.Entities.Base;
using WebApiCleanArch.Domain.Interfaces.GeneralIntefaces;

namespace WebApiCleanArch.Domain.Interfaces.Repositories
{
    public interface IRepository<T> where T :class,IEntity
    {
        IQueryable<T> Table { get; }
        IQueryable<T> TableAsNoTracking { get; }

        bool Add(T entity, bool saveNow = true);
        Task<bool> AddAsync(T entity, CancellationToken cancellationToken, bool saveNow = true);
        bool AddRange(IEnumerable<T> entities, bool saveNow = true);
        Task<bool> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken, bool saveNow = true);
        bool Attach(T entity);
        bool Detach(T entity);
        T GetById(params object[] ids);
        Task<T> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);
        bool LoadCollection<TProperty>(T entity, Expression<Func<T, IEnumerable<TProperty>>> collectionProperty) where TProperty : class;
        Task<bool> LoadCollectionAsync<TProperty>(T entity, Expression<Func<T, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken) where TProperty : class;
        bool LoadReference<TProperty>(T entity, Expression<Func<T, IEnumerable<TProperty>>> referenceProperty) where TProperty : class;
        Task<bool> LoadReference<TProperty>(T entity, Expression<Func<T, IEnumerable<TProperty>>> referenceProperty, CancellationToken cancellationToken) where TProperty : class;
        bool Remove(T entity, bool saveNow = true);
        Task<bool> RemoveAsync(T entity, CancellationToken cancellationToken, bool saveNow = true);
        bool RemoveRange(IEnumerable<T> entities, bool saveNow = true);
        Task<bool> RemoveRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken, bool saveNow = true);
        bool Update(T entity, bool saveNow = true);
        Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken, bool saveNow = true);
        bool UpdateRange(IEnumerable<T> entities, bool saveNow = true);
        Task<bool> UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken, bool saveNow = true);
    }
}
