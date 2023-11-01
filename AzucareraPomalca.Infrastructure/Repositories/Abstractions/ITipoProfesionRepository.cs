using AzucareraPomalca.Domain;
using AzucareraPomalca.Infrastructure.Core.Repositories.Abstractions;

namespace AzucareraPomalca.Infrastructure.Repositories.Abstractions
{
    public interface ITipoProfesionRepository : ICrudRepository<TipoProfesion, int>
    {
    }
}
