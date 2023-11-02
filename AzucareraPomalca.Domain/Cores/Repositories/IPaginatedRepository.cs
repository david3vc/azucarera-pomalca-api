using AzucareraPomalca.Core.Paginations;

namespace AzucareraPomalca.Domain.Cores.Repositories
{
    public interface IPaginatedRepository<T>
    {
        Task<ResponsePagination<T>> PaginatedSearch(RequestPagination<T> request);
    }
}
