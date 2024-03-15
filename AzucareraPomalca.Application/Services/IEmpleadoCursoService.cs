using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.EmpleadoCursos;

namespace AzucareraPomalca.Application.Services
{
    public interface IEmpleadoCursoService : ICrudService<EmpleadoCursoDto, EmpleadoCursoSaveDto, int>
    {
        Task<List<EmpleadoCursoDto>> CursosEmpleadoByIdEmpleado(int idEmpleado);
    }
}
