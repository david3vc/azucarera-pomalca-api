using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class EsfuerzoRequeridoPuestoRepository : CrudRepository<EsfuerzoRequeridoPuesto, int>, IEsfuerzoRequeridoPuestoRepository
    {
        public EsfuerzoRequeridoPuestoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
