using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repository
{
    public class InventoryRepository<T> : IRepository<T> where T : class, IBaseEntity
    {
        private readonly ApplicationDbContext _dbContext;

        public InventoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await _dbContext.Set<T>().ToListAsync();
            return entities.Where(x => !x.IsDeleted);
        }



        public async Task<T?> GetByIdAsync(long id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            return entity;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<T> UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;

        }
        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

    }
}
