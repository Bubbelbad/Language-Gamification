using Application.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

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

        public async Task<T?> GetByIdAsync(TKey id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (id is Guid)
            {
                return await query.FirstOrDefaultAsync(e => EF.Property<string>(e, "Id").Equals(id.ToString()));
            }
            else if (id is int)
            {
                return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id").Equals(id));
            }
            else if (id is string)
            {
                return await query.FirstOrDefaultAsync(e => EF.Property<string>(e, "Id").Equals(id));
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

        public async Task<bool> DeleteAsync(TKey id)
        {
            if (id is Guid)
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
            else if (id is int)
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity == null)
                {
                    return false;
                }

                _dbSet.Remove(entity);
                await _realDatabase.SaveChangesAsync();
                return true;
            }
            else
            {
                throw new InvalidOperationException($"Unsupported key type: {typeof(TKey).Name}");
            }
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }
    }
}