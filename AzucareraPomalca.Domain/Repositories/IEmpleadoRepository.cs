using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Cores.Repositories;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Domain.Repositories
{
    public interface IEmpleadoRepository : ICrudRepository<Empleado, int>
    {
        Task<List<Empleado>> FindByIdPuestoAsync(int id);
        Task<Empleado?> FindByNumeroDocumentoAsync(string numeroDocumento);
        void GuardarMasivoAsync(List<DtEmpleado> empleados);
        Task<PagedResult<EmpleadoSugerido>> ListarEmpleadosSugeridosAsync(Paging paging, EmpleadoSugerido request);
    }
}
