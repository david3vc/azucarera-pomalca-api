using AzucareraPomalca.Domain.Cores.Repositories;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Domain.Repositories
{
    public interface IUsuarioRepository : ICrudRepository<Usuario, int>
    {
    }
}
