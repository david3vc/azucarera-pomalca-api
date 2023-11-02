using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class TipoProfesionRepository : CrudRepository<TipoProfesion, int>, ITipoProfesionRepository
    {
        public TipoProfesionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
