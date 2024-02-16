using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class EmpleadoCursoRepository : CrudRepository<EmpleadoCurso, int>, IEmpleadoCursoRepository
    {
        public EmpleadoCursoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
