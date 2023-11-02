using AzucareraPomalca.Core.Paginations;

namespace AzucareraPomalca.Domain.Cores.Paginations
{
    public interface IPaginator<T>
    {
        Task<ResponsePagination<T>> Paginate(IQueryable<T> query, RequestPagination<T> request);
    }
}
