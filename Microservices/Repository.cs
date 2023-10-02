using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace xingyi.microservices.repository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task<bool> DeleteAsync(Guid id);
    }
    abstract public class Repository<C,T> : IRepository<T>
        where T : class
        where C: DbContext
    {
        private readonly C _context;
        private readonly DbSet<T> _dbSet;

        public Repository(C context, Func<C, DbSet<T>> setFn)
        {
            _context = context;
            _dbSet = setFn(context);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
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

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}


