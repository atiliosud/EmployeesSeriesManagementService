namespace Esms.Business.Services;

public interface IServiceBase<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<List<T>> GetAll();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}
