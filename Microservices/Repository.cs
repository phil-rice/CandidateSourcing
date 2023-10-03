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
    public interface IRepository<T,Id> where T : class
    {
        Task<List<T>> GetAllAsync(Boolean eagerLoad = false);
        Task<T> GetByIdAsync(Id id, Boolean eagerLoad = true);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task<bool> DeleteAsync(Id id);
    }
    abstract public class Repository<C, T,Id> : IRepository<T,Id>
        where T : class
        where C : DbContext
    {
        private readonly C _context;
        private readonly Func<DbSet<T>, IQueryable<T>> eagerLoadFn;
        private readonly DbSet<T> _dbSet;
        private readonly Func<Id,Expression<Func<T, bool>>> idEquals;

        protected Repository(C context, Func<C,DbSet<T>> dbSet, Func<Id, Expression<Func<T, bool>>> idEquals, Func<DbSet<T>, IQueryable<T>> eagerLoadFn)
        {
            _context = context;
            _dbSet = dbSet(context);
            this.idEquals = idEquals;
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
            return eagerLoad ? eagerLoadFn(set) : set;
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

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
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


