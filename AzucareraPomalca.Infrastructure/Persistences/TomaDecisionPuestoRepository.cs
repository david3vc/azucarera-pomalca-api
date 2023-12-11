using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class TomaDecisionPuestoRepository : CrudRepository<TomaDecisionPuesto, int>, ITomaDecisionPuestoRepository
    {
        public TomaDecisionPuestoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
