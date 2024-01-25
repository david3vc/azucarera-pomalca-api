using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.Gerencias;

namespace AzucareraPomalca.Application.Services
{
    public interface IGerenciaService : ICrudService<GerenciaDto, GerenciaSaveDto, int>, IListService<GerenciaSimpleDto>, IPageService<GerenciaDto, GerenciaFilterDto>
    {
    }
}
