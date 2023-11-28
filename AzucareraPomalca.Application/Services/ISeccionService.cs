using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.Secciones;

namespace AzucareraPomalca.Application.Services
{
    public interface ISeccionService : IQueryService<SeccionDto, int>, IListService<SeccionSimpleDto>
    {
        Task<IReadOnlyList<SeccionSimpleDto>> SimpleListByIdsAsync(SeccionSimpleFilterDto request);
    }
}
