using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class CondicionTrabajoRepository : CrudRepository<CondicionTrabajo, int>, ICondicionTrabajoRepository
    {
        public CondicionTrabajoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
