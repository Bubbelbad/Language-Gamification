using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IGenericRepository<T, TKey>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(TKey id, params Expression<Func<T, object>>[] includeProperties);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(TKey id);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    }
}
