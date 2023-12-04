using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class ResponsabilidadRepository : CrudRepository<Responsabilidad, int>, IResponsabilidadRepository
    {
        public ResponsabilidadRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
