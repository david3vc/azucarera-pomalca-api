using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class ResponsabilidadPuestoRepository : CrudRepository<ResponsabilidadPuesto, int>, IResponsabilidadPuestoRepository
    {
        public ResponsabilidadPuestoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
