using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class ProfesionRepository : CrudRepository<Profesion, int>, IProfesionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProfesionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<IReadOnlyList<Profesion>> FindAllAsync()
        {
            return await _dbContext.Set<Profesion>()
                .Include(t => t.TipoProfesion)
                .AsNoTracking()
                .ToListAsync();
        }

        public override async Task<Profesion?> FindByIdAsync(int id)
        {
            return await _dbContext.Set<Profesion>()
                .Include(t => t.TipoProfesion)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        //public override async Task<PagedResult<Profesion>> FindAllPaginatedAsync(Paging pagin,
        //    Expression<Func<Profesion, bool>> predicate)
        //{

        //}
    }
}
