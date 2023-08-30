using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TaskThinkland.Api.Context.Repositories.GenericRepositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _dbContext;
    protected DbSet<TEntity> DbSet;

    public GenericRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        DbSet = _dbContext.Set<TEntity>();
    }

    public async ValueTask<TEntity> InsertAsync(TEntity entity)
    {
        var entry = await this.DbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entry.Entity;
    }

    public IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>>? expression = null)
        => expression is null ? this.DbSet : this.DbSet.Where(expression);

    public async ValueTask<TEntity?> SelectSingleAsync(Expression<Func<TEntity, bool>> expression)
        => await this.DbSet.SingleOrDefaultAsync(expression);

    public async ValueTask<TEntity> UpdateAsync(TEntity entity)
    {
        var entry = DbSet.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entry.Entity;
    }

    public async ValueTask DeleteAsync(TEntity entity)
    {
        this.DbSet.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}