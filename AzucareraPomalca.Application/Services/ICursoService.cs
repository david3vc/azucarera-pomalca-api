using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.Cursos;

namespace AzucareraPomalca.Application.Services
{
    public interface ICursoService : ICrudService<CursoDto, CursoSaveDto, int>, IPageService<CursoDto, CursoFilterDto>
    {
        Task<PageResponse<CursoDuroSugeridoDto>> CursosDurosSugeridosPaginatedAsync(PageRequest<CursoDuroSugeridoFilterDto> request);
    }
}
