using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class NivelRepository : CrudRepository<Nivel, int>, INivelRepository
    {
        public NivelRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
