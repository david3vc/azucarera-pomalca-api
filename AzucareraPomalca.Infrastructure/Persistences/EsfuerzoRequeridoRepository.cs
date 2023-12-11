using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class EsfuerzoRequeridoRepository : CrudRepository<EsfuerzoRequerido, int>, IEsfuerzoRequeridoRepository
    {
        public EsfuerzoRequeridoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
