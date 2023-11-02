using AzucareraPomalca.Domain.Cores.Repositories;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Domain.Repositories
{
    public interface IProfesionRepository : ICrudRepository<Profesion, int>, IPaginatedRepository<Profesion>
    {
    }
}
