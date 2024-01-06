using AzucareraPomalca.Domain.Cores.Repositories;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Domain.Repositories
{
    public interface ITomaDecisionPuestoRepository : ICrudRepository<TomaDecisionPuesto, int>
    {
        Task<List<TomaDecisionPuesto>> GetTomaDecisionPuestosByIdPuesto(int idPuesto);
    }
}
