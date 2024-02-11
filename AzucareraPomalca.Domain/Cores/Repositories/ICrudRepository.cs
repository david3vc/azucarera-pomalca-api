using System.Linq.Expressions;

namespace AzucareraPomalca.Domain.Cores.Repositories
{
    public interface ICrudRepository<T, ID> : IPageRepository<T>
    {
        Task<IReadOnlyList<T>> FindAllAsync();
        Task<T?> FindByIdAsync(ID id);
        Task<T> SaveAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T?> FindByIdAsync(Expression<Func<T, bool>> predicate,
                                       List<Expression<Func<T, object>>>? includes = null,
                                       bool disableTracking = true);
        Task<T?> FindFirstOrDefaultAsync(Expression<Func<T, bool>> predicate,
                                       List<Expression<Func<T, object>>>? includes = null,
                                       bool disableTracking = true);
        Task<IReadOnlyList<T>> FindAllAsync(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> FindAllAsync(Expression<Func<T, bool>>? predicate = null,
                                       Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                       List<Expression<Func<T, object>>>? includes = null,
                                       bool disableTracking = true);
    }
}
