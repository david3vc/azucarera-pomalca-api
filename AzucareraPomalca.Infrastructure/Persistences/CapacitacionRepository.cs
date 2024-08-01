using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class CapacitacionRepository : CrudRepository<Capacitacion, int>, ICapacitacionRepository
    {
        public CapacitacionRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
