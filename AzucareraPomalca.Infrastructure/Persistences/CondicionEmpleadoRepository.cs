using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class CondicionEmpleadoRepository : CrudRepository<CondicionEmpleado, int>, ICondicionEmpleadoRepository
    {
        public CondicionEmpleadoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
