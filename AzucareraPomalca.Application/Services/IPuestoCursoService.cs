using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.PuestosCursos;

namespace AzucareraPomalca.Application.Services
{
    public interface IPuestoCursoService : ICrudService<PuestoCursoDto, PuestoCursoSaveDto, int>
    {
    }
}
