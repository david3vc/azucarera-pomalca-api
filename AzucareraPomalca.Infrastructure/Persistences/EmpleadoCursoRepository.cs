using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class EmpleadoCursoRepository : CrudRepository<EmpleadoCurso, int>, IEmpleadoCursoRepository
    {
        public EmpleadoCursoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
