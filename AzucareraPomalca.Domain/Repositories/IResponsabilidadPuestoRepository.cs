using AzucareraPomalca.Domain.Cores.Repositories;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Domain.Repositories
{
    public interface IResponsabilidadPuestoRepository : ICrudRepository<ResponsabilidadPuesto, int>
    {
        Task<List<ResponsabilidadPuesto>> GetResponsabilidadPuestosByIdPuesto(int idPuesto);
    }
}
