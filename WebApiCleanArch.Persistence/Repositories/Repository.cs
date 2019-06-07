using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiCleanArch.Common.Exceptions;
using WebApiCleanArch.Domain.Entities.Base;
using WebApiCleanArch.Domain.Interfaces.GeneralIntefaces;
using WebApiCleanArch.Domain.Interfaces.Repositories;
using WebApiCleanArch.Persistence.DbContexts;

namespace WebApiCleanArch.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        #region Fields

        private readonly MyDbContext _myDbContext;
        protected DbSet<T> Entities => _myDbContext.Set<T>();
        public IQueryable<T> Table => Entities;
        public IQueryable<T> TableAsNoTracking => Entities.AsNoTracking();
        #endregion

        #region Constructors

        public Repository(MyDbContext dashboardContext)
        {
            _myDbContext = dashboardContext;

        }


        #endregion

        #region Methods

        #region Async Methods

        public virtual Task<T> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return Entities.FindAsync(ids, cancellationToken);
        }

        public async Task<bool> AddAsync(T entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            MyException.NotNull(entity, nameof(entity));
            try
            {
                await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
                if (saveNow)
                    await _myDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                return true;
            }
            catch
            {
                return false;
            }


        }
        public virtual async Task<bool> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            MyException.NotNull(entities, nameof(entities));
            try
            {
                await Entities.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
                if (saveNow)
                    await _myDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                return true;
            }
            catch
            {
                return false;
            }

        }
        public virtual async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            MyException.NotNull(entity, nameof(entity));
            try
            {
                Entities.Update(entity);
                if (saveNow)
                    await _myDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                return true;
            }
            catch
            {
                return false;
            }

        }
        public virtual async Task<bool> UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            MyException.NotNull(entities, nameof(entities));
            try
            {
                Entities.UpdateRange(entities);
                if (saveNow)
                    await _myDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public virtual async Task<bool> RemoveAsync(T entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            MyException.NotNull(entity, nameof(entity));
            try
            {
                Entities.Remove(entity);
                if (saveNow)
                    await _myDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                return true;
            }
            catch
            {
                return false;
            }

        }
        public virtual async Task<bool> RemoveRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            MyException.NotNull(entities, nameof(entities));
            try
            {
                Entities.RemoveRange(entities);
                if (saveNow)
                    await _myDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                return true;
            }
            catch
            {
                return false;
            }

        }
        #endregion

        #region Sync Methods

        public virtual T GetById(params object[] ids)
        {
            return Entities.Find(ids);
        }

        public bool Add(T entity, bool saveNow = true)
        {
            MyException.NotNull(entity, nameof(entity));
            try
            {
                Entities.AddAsync(entity);
                if (saveNow)
                    _myDbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public virtual bool AddRange(IEnumerable<T> entities, bool saveNow = true)
        {
            MyException.NotNull(entities, nameof(entities));
            try
            {
                Entities.AddRangeAsync(entities);
                if (saveNow)
                    _myDbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public virtual bool Update(T entity, bool saveNow = true)
        {
            MyException.NotNull(entity, nameof(entity));
            try
            {
                Entities.Update(entity);
                if (saveNow)
                    _myDbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public virtual bool UpdateRange(IEnumerable<T> entities, bool saveNow = true)
        {
            MyException.NotNull(entities, nameof(entities));
            try
            {
                Entities.UpdateRange(entities);
                if (saveNow)
                    _myDbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual bool Remove(T entity, bool saveNow = true)
        {
            MyException.NotNull(entity, nameof(entity));
            try
            {
                Entities.Remove(entity);
                if (saveNow)
                    _myDbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public virtual bool RemoveRange(IEnumerable<T> entities, bool saveNow = true)
        {
            MyException.NotNull(entities, nameof(entities));
            try
            {
                Entities.RemoveRange(entities);
                if (saveNow)
                    _myDbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Attach & Detach

        public virtual bool Detach(T entity)
        {
            MyException.NotNull(entity, nameof(entity));
            try
            {
                var en = _myDbContext.Entry(entity);
                if (en != null)
                    en.State = EntityState.Detached;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual bool Attach(T entity)
        {
            MyException.NotNull(entity, nameof(entity));
            try
            {
                if (_myDbContext.Entry(entity).State != EntityState.Detached) return false;
                Entities.Attach(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion


        #region Explicit loading

        public virtual async Task<bool> LoadCollectionAsync<TProperty>(T entity, Expression<Func<T, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken) where TProperty : class
        {

            try
            {
                Attach(entity);
                var collections = _myDbContext.Entry(entity).Collection(collectionProperty);
                if (!collections.IsLoaded)
                {
                    await collections.LoadAsync(cancellationToken).ConfigureAwait(false);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        public virtual bool LoadCollection<TProperty>(T entity, Expression<Func<T, IEnumerable<TProperty>>> collectionProperty) where TProperty : class
        {

            try
            {
                Attach(entity);
                var collections = _myDbContext.Entry(entity).Collection(collectionProperty);
                if (!collections.IsLoaded)
                {
                    collections.Load();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual async Task<bool> LoadReference<TProperty>(T entity,
            Expression<Func<T, IEnumerable<TProperty>>> referenceProperty, CancellationToken cancellationToken) where TProperty : class
        {
            try
            {
                Attach(entity);
                var reference = _myDbContext.Entry(entity).Reference(referenceProperty);
                if (!reference.IsLoaded)
                {
                    await reference.LoadAsync(cancellationToken).ConfigureAwait(false);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        public virtual bool LoadReference<TProperty>(T entity,
            Expression<Func<T, IEnumerable<TProperty>>> referenceProperty) where TProperty : class
        {
            try
            {
                Attach(entity);
                var reference = _myDbContext.Entry(entity).Reference(referenceProperty);
                if (!reference.IsLoaded)
                {
                    reference.Load();
                }

                return true;
            }
            catch
            {
                return false;

            }
        }

        #endregion

        #endregion
    }
}
