using AzucareraPomalca.Domain.Cores.Repositories;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Domain.Repositories
{
    public interface ISeccionRepository : ICrudRepository<Seccion, int>
    {
        Task<List<Seccion>> FindByIdGerenciaAsync(int id);
        Task<List<Seccion>> FindByIdDivisionAsync(int id);
        Task<List<Seccion>> FindByIdDepartamentoAsync(int id);
        Task<List<Seccion>> SearchByUnidadOrganizacional(Seccion request);
    }
}
