using Esms.Business.Repositories;

namespace Esms.Business.Services.V1;

public class ServiceBase<T> : IServiceBase<T> where T : class
{
    private readonly IRepositoryBase<T> _repository;

    public ServiceBase(IRepositoryBase<T> repository)
    {
        _repository = repository;
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<List<T>> GetAll()
    {
        return await _repository.GetAll();
    }


    public async Task<T> AddAsync(T entity)
    {
        await _repository.AddAsync(entity);
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if(entity != null)
        {
            await _repository.DeleteAsync(entity);
        }
    }
}
