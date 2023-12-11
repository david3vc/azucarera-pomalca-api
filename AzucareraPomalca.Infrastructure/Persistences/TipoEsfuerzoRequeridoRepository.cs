using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class TipoEsfuerzoRequeridoRepository : CrudRepository<TipoEsfuerzoRequerido, int>, ITipoEsfuerzoRequeridoRepository
    {
        public TipoEsfuerzoRequeridoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
