using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;
using Microsoft.EntityFrameworkCore;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class DivisionRepository : CrudRepository<Division, int>, IDivisionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DivisionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<IReadOnlyList<Division>> FindAllAsync()
        {
            return await _dbContext.Set<Division>()
                .Include(t => t.Gerencia)
                .AsNoTracking()
                .ToListAsync();
        }

        public override async Task<Division?> FindByIdAsync(int id)
        {
            return await _dbContext.Set<Division>()
                .Include(t => t.Gerencia)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
