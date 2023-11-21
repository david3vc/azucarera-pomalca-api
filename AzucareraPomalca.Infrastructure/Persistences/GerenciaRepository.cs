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

        public async Task<Gerencia?> FindByNombreAsync(string nombre)
        {
            return await _dbContext.Set<Gerencia>()
                .Include(t => t.Puestos)
                .FirstOrDefaultAsync(t => t.Nombre.ToUpper().Contains(nombre.ToUpper()));
        }

        public async Task<List<Gerencia>> FindGerenciasSubalternasAsync()
        {
            return await _dbContext.Set<Gerencia>()
                .Include(t => t.Puestos)
                .Where(f => f.Nombre != "GERENCIA GENERAL")
                .ToListAsync();
        }
    }
}
