using AzucareraPomalca.Domain.Cores.Repositories;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Domain.Repositories
{
    public interface IEmpleadoProfesionRepository : ICrudRepository<EmpleadoProfesion, int>
    {
        Task<List<EmpleadoProfesion>> ProfesionesEmpleadoByIdEmpleado(int idEmpleado);
    }
}
