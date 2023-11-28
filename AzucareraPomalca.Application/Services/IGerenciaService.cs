using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.Gerencias;

namespace AzucareraPomalca.Application.Services
{
    public interface IGerenciaService : IQueryService<GerenciaDto, int>, IListService<GerenciaSimpleDto>
    {
    }
}
