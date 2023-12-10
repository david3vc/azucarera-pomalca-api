using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class TipoCondicionTrabajoRepository : CrudRepository<TipoCondicionTrabajo, int>, ITipoCondicionTrabajoRepository
    {
        public TipoCondicionTrabajoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
