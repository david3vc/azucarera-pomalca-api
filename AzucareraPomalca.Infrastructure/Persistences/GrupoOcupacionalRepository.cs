using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class GrupoOcupacionalRepository : CrudRepository<GrupoOcupacional, int>, IGrupoOcupacionalRepository
    {
        public GrupoOcupacionalRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
