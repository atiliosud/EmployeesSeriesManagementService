using System.Linq.Expressions;

namespace Esms.Business.Repositories;

public interface IRepositoryBase<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(int id);
    Task<List<TEntity>> GetAll();
    Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, string includes = "", string includes2 = "");
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task SaveAsync();
}