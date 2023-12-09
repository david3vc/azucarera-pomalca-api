using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.Cursos;

namespace AzucareraPomalca.Application.Services
{
    public interface ICursoService : ICrudService<CursoDto, CursoSaveDto, int>, IPageService<CursoDto, CursoFilterDto>
    {
    }
}
