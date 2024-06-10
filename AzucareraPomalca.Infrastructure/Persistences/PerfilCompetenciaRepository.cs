using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;
using Microsoft.EntityFrameworkCore;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class PerfilCompetenciaRepository : CrudRepository<PerfilCompetencia, int>, IPerfilCompetenciaRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PerfilCompetenciaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<PerfilCompetencia?> FindByIdAsync(int id)
        {
            return await _dbContext.Set<PerfilCompetencia>()
                .Include(t => t.GradoDominio).ThenInclude(t => t.CompetenciaSimple).ThenInclude(t => t.TipoCompetencia)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
