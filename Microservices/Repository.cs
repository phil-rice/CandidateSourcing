using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace xingyi.microservices.repository
{
    public interface IRepository<T, Id, Where> where T : class
    {
        Task<List<T>> GetAllAsync(Boolean eagerLoad = false);
        Task<T> GetByIdAsync(Id id, Boolean eagerLoad = true);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task<bool> DeleteAsync(Id id);
    }
    abstract public class Repository<C, T, Id, Where> : IRepository<T, Id, Where>
        where T : class
        where C : DbContext
    {
        protected readonly C _context;
        private readonly Func<DbSet<T>, IQueryable<T>> nonEagerLoadFn;
        private readonly Func<DbSet<T>, IQueryable<T>> eagerLoadFn;
        protected readonly DbSet<T> _dbSet;
        private readonly Func<Id, Expression<Func<T, bool>>> idEquals;

        public void cleanDb()
        {
            foreach (var entity in _context.ChangeTracker.Entries())
            {
                entity.State = EntityState.Detached;
            }
        }
        protected bool IsEntityBeingTracked<TEntity>(TEntity entity) where TEntity : class
        {
            return _context.ChangeTracker.Entries<TEntity>().Any(e => e.Entity == entity);
        }
        protected void AddIfNotTracked<TEntity>(TEntity entity) where TEntity : class
        {
            if (!IsEntityBeingTracked(entity))
            {
                _context.Set<TEntity>().Add(entity);
            }
        }

        protected Repository(C context, Func<C, DbSet<T>> dbSet, Func<Id, Expression<Func<T, bool>>> idEquals,
            Func<DbSet<T>, IQueryable<T>> nonEagerLoadFn,
            Func<DbSet<T>, IQueryable<T>> eagerLoadFn
            )
        {
            _context = context;
            _dbSet = dbSet(context);
            this.idEquals = idEquals;
            this.nonEagerLoadFn = nonEagerLoadFn;
            this.eagerLoadFn = eagerLoadFn;
        }

        public static JsonSerializerSettings settings = new JsonSerializerSettings
        {
            //We need this to serialise many to many relationships
            //For example Job hasMany SectionTemplate, and SectionTemplate links back to the Job
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore
        };


        private IQueryable<T> load(Boolean eagerLoad, DbSet<T> set)
        {
            return eagerLoad ? eagerLoadFn(set) : nonEagerLoadFn(set);
        }

        public async Task<List<T>> GetAllAsync(Boolean eagerLoad)
        {
            return await load(eagerLoad, _dbSet).ToListAsync();
        }

        public async Task<T> GetByIdAsync(Id id, Boolean eagerLoad)
        {
            return await load(eagerLoad, _dbSet).FirstOrDefaultAsync(idEquals(id));
        }

        public async Task<T> AddAsync(T entity)
        {
            // Assuming entities that use this repository have a property named "Id"
            // If that's not the case, you might need more specific logic here or move this logic to a specific repository.
            var propertyInfo = entity.GetType().GetProperty("Id");
            if (propertyInfo != null && (Guid)propertyInfo.GetValue(entity) == Guid.Empty)
            {
                propertyInfo.SetValue(entity, Guid.NewGuid());
            }

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        protected virtual async Task<T> populateEntityForUpdate(T entity) { return entity; }
        public async Task UpdateAsync(T entity)
        {
            var updated = await populateEntityForUpdate(entity);
            _dbSet.Update(updated);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Id id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}


