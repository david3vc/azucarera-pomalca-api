using AzucareraPomalca.Domain.Cores.Repositories;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Domain.Repositories
{
    public interface IPermisoRepository : ICrudRepository<Permiso, int>
    {
        Task<IReadOnlyList<Permiso>> FindPermisolByIdPerfilAsync(int id);
    }
}
