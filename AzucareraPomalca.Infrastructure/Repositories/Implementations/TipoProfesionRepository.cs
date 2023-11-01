using AzucareraPomalca.Domain;
using AzucareraPomalca.Infrastructure.Context;
using AzucareraPomalca.Infrastructure.Core.Repositories.Implementations;
using AzucareraPomalca.Infrastructure.Repositories.Abstractions;

namespace AzucareraPomalca.Infrastructure.Repositories.Implementations
{
    public class TipoProfesionRepository : CrudRepository<TipoProfesion, int>, ITipoProfesionRepository
    {
        public TipoProfesionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
