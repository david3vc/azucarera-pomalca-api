using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class PuestoCursoRepository : CrudRepository<PuestoCurso, int>, IPuestoCursoRepository
    {
        public PuestoCursoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
