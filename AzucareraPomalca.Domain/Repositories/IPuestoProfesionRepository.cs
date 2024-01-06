using AzucareraPomalca.Domain.Cores.Repositories;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Domain.Repositories
{
    public interface IPuestoProfesionRepository : ICrudRepository<PuestoProfesion, int>
    {
        Task<List<PuestoProfesion>> ProfesionesPuestoByIdPuesto(int idPuesto);
    }
}
