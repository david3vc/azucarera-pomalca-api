using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class CompetenciaRepository : CrudRepository<Competencia, int>, ICompetenciaRepository
    {
        public CompetenciaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
