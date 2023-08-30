using System.Linq.Expressions;

namespace TaskThinkland.Api.Context.Repositories.GenericRepositories;

public interface IGenericRepository<TEntity> where TEntity : class
{
    ValueTask<TEntity> InsertAsync(TEntity entity);
    IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>>? expression = null);
    ValueTask<TEntity?> SelectSingleAsync(Expression<Func<TEntity, bool>> expression);
    ValueTask<TEntity> UpdateAsync(TEntity entity);
    ValueTask DeleteAsync(TEntity entity);
}