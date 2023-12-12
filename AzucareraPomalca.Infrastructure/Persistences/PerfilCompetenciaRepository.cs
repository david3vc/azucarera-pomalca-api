using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class PerfilCompetenciaRepository : CrudRepository<PerfilCompetencia, int>, IPerfilCompetenciaRepository
    {
        public PerfilCompetenciaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
