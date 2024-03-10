using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class TablaComunRepository : CrudRepository<TablaComun, int>, ITablaComunRepository
    {
        public TablaComunRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
