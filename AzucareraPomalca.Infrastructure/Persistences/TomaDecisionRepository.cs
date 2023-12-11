using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class TomaDecisionRepository : CrudRepository<TomaDecision, int>, ITomaDecisionRepository
    {
        public TomaDecisionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
