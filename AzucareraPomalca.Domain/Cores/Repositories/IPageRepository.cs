using AzucareraPomalca.Domain.Cores.Models;
using System.Linq.Expressions;

namespace AzucareraPomalca.Domain.Cores.Repositories
{
    public interface IPageRepository<T>
    {
        Task<PagedResult<T>> FindAllPaginatedAsync(Paging paging,
                                       Expression<Func<T, bool>> predicate);
        Task<PagedResult<T>> FindAllPaginatedAsync(Paging paging,
                                       Expression<Func<T, bool>>? predicate = null,
                                       Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                       List<Expression<Func<T, object>>>? includes = null,
                                       bool disableTracking = true);
    }
}
