using AzucareraPomalca.Domain.Cores.Repositories;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Domain.Repositories
{
    public interface IPuestoRepository : ICrudRepository<Puesto, int>
    {
        Task<List<Puesto>> FindByGerenciaAsync(string nombreGerencia);
        Task<List<Puesto>> SearchByUnidadOrganizacionalAsync(Puesto request);
        Task<string> FindCodigoOrganizacionalByIdPuesto(int idPuesto);
    }
}
