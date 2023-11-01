using AzucareraPomalca.Utils.Paginations;

namespace AzucareraPomalca.Infrastructure.Core.Paginations.Abstractions
{
    public interface IPaginator<T>
    {
        Task<ResponsePagination<T>> Paginate(IQueryable<T> query, RequestPagination<T> request);
    }
}
