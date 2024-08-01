using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class CapacitacionEmpleadoRepository : CrudRepository<CapacitacionEmpleado, int>, ICapacitacionEmpleadoRepository
    {
        public CapacitacionEmpleadoRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
