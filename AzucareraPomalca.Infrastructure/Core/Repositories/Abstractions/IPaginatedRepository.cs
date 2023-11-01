using AzucareraPomalca.Utils.Paginations;

namespace AzucareraPomalca.Infrastructure.Core.Repositories.Abstractions
{
    public interface IPaginatedRepository<T>
    {
        IPaginatedRepository<T> PaginatedSearch(RequestPagination<T> request);
    }
}
