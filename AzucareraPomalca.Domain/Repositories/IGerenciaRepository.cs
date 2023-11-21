using AzucareraPomalca.Domain.Cores.Repositories;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Domain.Repositories
{
    public interface IGerenciaRepository : ICrudRepository<Gerencia, int>
    {
        Task<Gerencia?> FindByNombreAsync(string nombre);
        Task<List<Gerencia>> FindGerenciasSubalternasAsync();
    }
}
