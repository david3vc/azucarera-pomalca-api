using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.Competencias;

namespace AzucareraPomalca.Application.Services
{
    public interface ICompetenciaService : ICrudService<CompetenciaDto, CompetenciaSaveDto, int>, IPageService<CompetenciaDto, CompetenciaFilterDto>
    {
    }
}
