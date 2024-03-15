using AzucareraPomalca.Domain.Cores.Repositories;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Domain.Repositories
{
    public interface IEmpleadoCursoRepository : ICrudRepository<EmpleadoCurso, int>
    {
        Task<List<EmpleadoCurso>> CursosEmpleadoByIdEmpleado(int idEmpleado);
    }
}
