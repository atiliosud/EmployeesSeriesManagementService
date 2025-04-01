using System.Linq.Expressions;
using Esms.Business.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Esms.SQLite.Repositories;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
    protected readonly EsmsDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public RepositoryBase(EsmsDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<List<TEntity>> GetAll()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, string includes = "", string includes2 = "")
    {
        if(string.IsNullOrEmpty(includes))
            return await _dbSet.Where(filter).AsNoTracking().ToListAsync();
        else
        {

            if (string.IsNullOrEmpty(includes2))
            {
                return await _dbSet.Where(filter).Include(includes).AsNoTracking().ToListAsync();
            }
            else
            {

                return await _dbSet.Where(filter)
                    .Include(includes)
                    .Include(includes2)
                    .AsNoTracking().ToListAsync();
            }
        }
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();

    }

    public async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
