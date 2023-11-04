using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;
using Microsoft.EntityFrameworkCore;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class DepartamentoRepository : CrudRepository<Departamento, int>, IDepartamentoRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DepartamentoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<IReadOnlyList<Departamento>> FindAllAsync()
        {
            return await _dbContext.Set<Departamento>()
                .Include(t => t.Gerencia)
                .Include(t => t.Division)
                .AsNoTracking()
                .ToListAsync();
        }

        public override async Task<Departamento?> FindByIdAsync(int id)
        {
            return await _dbContext.Set<Departamento>()
                .Include(t => t.Gerencia)
                .Include(t => t.Division)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
