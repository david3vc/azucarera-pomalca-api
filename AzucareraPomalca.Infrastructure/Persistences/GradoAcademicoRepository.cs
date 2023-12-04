using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class GradoAcademicoRepository : CrudRepository<GradoAcademico, int>, IGradoAcademicoRepository
    {
        public GradoAcademicoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
