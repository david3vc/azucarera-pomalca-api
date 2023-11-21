using AzucareraPomalca.Domain.Cores.Repositories;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Domain.Repositories
{
    public interface IEmpleadoRepository : ICrudRepository<Empleado, int>
    {
        Task<List<Empleado>> FindByIdPuestoAsync(int id);
    }
}
