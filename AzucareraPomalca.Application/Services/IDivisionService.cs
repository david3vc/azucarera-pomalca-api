using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.Divisiones;

namespace AzucareraPomalca.Application.Services
{
    public interface IDivisionService : ICrudService<DivisionDto, DivisionSaveDto, int>, IListService<DivisionSimpleDto>, IPageService<DivisionDto, DivisionFilterDto>
    {
        Task<IReadOnlyList<DivisionSimpleDto>> SimpleListByIdGerenciaAsync(int id);
    }
}
