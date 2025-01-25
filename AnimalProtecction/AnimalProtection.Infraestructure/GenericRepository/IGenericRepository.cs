namespace AnimalProtecction.GenericRepository;

public interface IGenericRepository<T> where T : class
{
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(object id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(object id);
        Task<int> SaveAsync();
        Task<bool> ExistAsync();
        Task<int> CountAsync();
}