using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class RolRepository : CrudRepository<Rol, int>, IRolRepository
    {
        //private readonly ApplicationDbContext _dbContext;

        public RolRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            //_dbContext = dbContext;
        }
    }
}
