using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.Divisiones;

namespace AzucareraPomalca.Application.Services
{
    public interface IDivisionService : IQueryService<DivisionDto, int>, IListService<DivisionSimpleDto>
    {
        Task<IReadOnlyList<DivisionSimpleDto>> SimpleListByIdGerenciaAsync(int id);
    }
}
