using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Inventory.Application.Interfaces;
using Inventory.Infrastructure.Data;

namespace Inventory.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>(); // Dynamically resolves to the correct DbSet<T>
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            if (entity != null)
            {
                if (entity is ISoftDeletable softDeletableEntity)
                {
                    // Perform Soft Delete for Master Data
                    softDeletableEntity.IsDeleted = true;
                    _dbSet.Update(entity);
                }
                else
                {
                    // Perform Hard Delete for Transaction Tables (if ever explicitly called)
                    _dbSet.Remove(entity);
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}