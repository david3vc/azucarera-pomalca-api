using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.Departamentos;

namespace AzucareraPomalca.Application.Services
{
    public interface IDepartamentoService : ICrudService<DepartamentoDto, DepartamentoSaveDto, int>, IListService<DepartamentoSimpleDto>, IPageService<DepartamentoDto, DepartamentoFilterDto>
    {
        Task<IReadOnlyList<DepartamentoSimpleDto>> SimpleListByIdsAsync(DepartamentoSimpleFilterDto request);
    }
}
