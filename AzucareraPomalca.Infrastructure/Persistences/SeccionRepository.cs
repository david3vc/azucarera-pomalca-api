using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;
using Microsoft.EntityFrameworkCore;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class SeccionRepository : CrudRepository<Seccion, int>, ISeccionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SeccionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<IReadOnlyList<Seccion>> FindAllAsync()
        {
            return await _dbContext.Set<Seccion>()
                .Include(t => t.Gerencia)
                .Include(t => t.Division)
                .Include(t => t.Departamento)
                .AsNoTracking()
                .ToListAsync();
        }

        public override async Task<Seccion?> FindByIdAsync(int id)
        {
            return await _dbContext.Set<Seccion>()
                .Include(t => t.Gerencia)
                .Include(t => t.Division)
                .Include(t => t.Departamento)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
