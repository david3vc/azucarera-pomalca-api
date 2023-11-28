using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.ClaseOcupacionales;

namespace AzucareraPomalca.Application.Services
{
    public interface IClaseOcupacionalService : IQueryService<ClaseOcupacionalDto, int>, IListService<ClaseOcupacionalSimpleDto>
    {
        Task<IReadOnlyList<ClaseOcupacionalSimpleDto>> SimpleListByIdGrupoOcupacionalAsync(int id);
    }
}
