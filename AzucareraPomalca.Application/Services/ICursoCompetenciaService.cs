using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.CursoCompetencias;

namespace AzucareraPomalca.Application.Services
{
    public interface ICursoCompetenciaService : ICrudService<CursoCompetenciaDto, CursoCompetenciaSaveDto, int>, IPageService<CursoCompetenciaDto, CursoCompetenciaFilterDto>
    {
        Task<List<CursoCompetenciaDto>> FindByCompetenciaAsync(int idCompetencia);
    }
}
