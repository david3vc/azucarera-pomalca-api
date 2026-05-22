using AzucareraPomalca.Domain.Cores.Repositories;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Domain.Repositories
{
    public interface IPlanCapacitacionRepository : ICrudRepository<PlanCapacitacion, int>
    {
        /// <summary>
        /// Carga el plan con su detalle completo (líneas + participantes + cadena
        /// organizacional) usando Include/ThenInclude explícito. Necesario porque el
        /// include genérico de CrudRepository sólo resuelve un nivel.
        /// </summary>
        Task<PlanCapacitacion?> FindDetalleByIdAsync(int id);
    }
}
