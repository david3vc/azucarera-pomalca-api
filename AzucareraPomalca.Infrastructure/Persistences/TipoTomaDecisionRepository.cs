using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class TipoTomaDecisionRepository : CrudRepository<TipoTomaDecision, int>, ITipoTomaDecisionRepository
    {
        public TipoTomaDecisionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
