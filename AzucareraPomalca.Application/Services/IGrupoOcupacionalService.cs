using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.GrupoOcupacionales;

namespace AzucareraPomalca.Application.Services
{
    public interface IGrupoOcupacionalService : IQueryService<GrupoOcupacionalDto, int>, IListService<GrupoOcupacionalSimpleDto>
    {
    }
}
