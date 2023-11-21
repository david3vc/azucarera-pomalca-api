using AzucareraPomalca.Application.Cores.Dtos;

namespace AzucareraPomalca.Application.Cores.Services
{
    public interface IPageService<TDto, TDtoFilter>
    {
        Task<PageResponse<TDto>> FindAllPaginatedAsync(PageRequest<TDtoFilter> request);
    }
}
