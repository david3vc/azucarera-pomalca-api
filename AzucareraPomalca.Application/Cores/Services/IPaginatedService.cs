using AzucareraPomalca.Core.Paginations;

namespace AzucareraPomalca.Application.Cores.Services
{
    public interface IPaginatedService<TDto, TDtoFilter>
    {
        Task<ResponsePagination<TDto>> PaginatedSearch(RequestPagination<TDtoFilter> request);
    }
}
