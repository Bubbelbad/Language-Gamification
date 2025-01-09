namespace Application.Interfaces
{
    public interface IGenericRepository<T, TKey>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(TKey id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(TKey id);
    }
}
