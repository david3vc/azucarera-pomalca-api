using AzucareraPomalca.Domain.Cores.Repositories;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Domain.Repositories
{
    public interface IDepartamentoRepository : ICrudRepository<Departamento, int>
    {
        Task<List<Departamento>> FindByIdGerenciaAsync(int id);
        Task<List<Departamento>> FindByIdDivisionAsync(int id);
        Task<List<Departamento>> SearchByUnidadOrganizacional(Departamento request);
    }
}
