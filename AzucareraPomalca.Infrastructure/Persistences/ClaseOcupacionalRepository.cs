using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;
using Microsoft.EntityFrameworkCore;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class ClaseOcupacionalRepository : CrudRepository<ClaseOcupacional, int>, IClaseOcupacionalRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ClaseOcupacionalRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<IReadOnlyList<ClaseOcupacional>> FindAllAsync()
        {
            return await _dbContext.Set<ClaseOcupacional>()
                .Include(t => t.GrupoOcupacional)
                .AsNoTracking()
                .ToListAsync();
        }

        public override async Task<ClaseOcupacional?> FindByIdAsync(int id)
        {
            return await _dbContext.Set<ClaseOcupacional>()
                .Include(t => t.GrupoOcupacional)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
