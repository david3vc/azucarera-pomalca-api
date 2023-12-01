using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class FuncionEspecificaRepository : CrudRepository<FuncionEspecifica, int>, IFuncionEspecificaRepository
    {
        public FuncionEspecificaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
