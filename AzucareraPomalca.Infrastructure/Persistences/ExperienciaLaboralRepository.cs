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
    public class ExperienciaLaboralRepository : CrudRepository<ExperienciaLaboral, int>, IExperienciaLaboralRepository
    {
        public ExperienciaLaboralRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
