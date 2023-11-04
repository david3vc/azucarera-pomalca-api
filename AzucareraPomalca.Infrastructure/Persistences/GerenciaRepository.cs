using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;
using Microsoft.EntityFrameworkCore;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class GerenciaRepository : CrudRepository<Gerencia, int>, IGerenciaRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public GerenciaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<IReadOnlyList<Gerencia>> FindAllAsync()
        {
            return await _dbContext.Set<Gerencia>()
                .Include(t => t.Divisiones).ThenInclude(t => t.Departamentos).ThenInclude(t => t.Secciones)
                .Include(t => t.Departamentos).ThenInclude(t => t.Secciones)
                .Include(t => t.Secciones)
                .AsNoTracking()
                .ToListAsync();
        }

        public override async Task<Gerencia?> FindByIdAsync(int id)
        {
            return await _dbContext.Set<Gerencia>()
                .Include(t => t.Divisiones).ThenInclude(t => t.Departamentos).ThenInclude(t => t.Secciones)
                .Include(t => t.Departamentos).ThenInclude(t => t.Secciones)
                .Include(t => t.Secciones)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
