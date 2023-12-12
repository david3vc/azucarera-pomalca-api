using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class TipoCompetenciaRepository : CrudRepository<TipoCompetencia, int>, ITipoCompetenciaRepository
    {
        public TipoCompetenciaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
