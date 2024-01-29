using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.Secciones;

namespace AzucareraPomalca.Application.Services
{
    public interface ISeccionService : ICrudService<SeccionDto, SeccionSaveDto, int>, IListService<SeccionSimpleDto>, IPageService<SeccionDto, SeccionFilterDto>
    {
        Task<IReadOnlyList<SeccionSimpleDto>> SimpleListByIdsAsync(SeccionSimpleFilterDto request);
    }
}
