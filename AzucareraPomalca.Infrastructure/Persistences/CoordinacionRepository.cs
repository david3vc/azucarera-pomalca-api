using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class CoordinacionRepository : CrudRepository<Coordinacion, int>, ICoordinacionRepository
    {
        public CoordinacionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
