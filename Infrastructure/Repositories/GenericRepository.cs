using Application.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : class
    {
        private readonly ApplicationDbContext _realDatabase;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext database)
        {
            _realDatabase = database;
            _dbSet = _realDatabase.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(TKey id)
        {
            if (id is Guid)
            {
                return await _dbSet.FindAsync(id.ToString());
            }
            else if (id is int)
            {
                return await _dbSet.FindAsync(id);
            }
            else
            {
                throw new InvalidOperationException($"Unsupported key type: {typeof(TKey).Name}");
            }
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _realDatabase.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _realDatabase.Set<T>().Update(entity);
            await _realDatabase.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id.ToString());
            if (entity == null)
            {
                return false;
            }

            _dbSet.Remove(entity);
            await _realDatabase.SaveChangesAsync();
            return true;
        }
    }
}