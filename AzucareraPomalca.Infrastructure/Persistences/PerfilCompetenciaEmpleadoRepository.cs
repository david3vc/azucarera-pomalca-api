using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class PerfilCompetenciaEmpleadoRepository : CrudRepository<PerfilCompetenciaEmpleado, int>, IPerfilCompetenciaEmpleadoRepository
    {
        public PerfilCompetenciaEmpleadoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
