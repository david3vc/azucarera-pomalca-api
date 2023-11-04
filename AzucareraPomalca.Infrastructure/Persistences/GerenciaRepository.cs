using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class GerenciaRepository : CrudRepository<Gerencia, int>, IGerenciaRepository
    {
        public GerenciaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
