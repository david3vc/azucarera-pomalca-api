using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;
using Microsoft.EntityFrameworkCore;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class PermisoRepository : CrudRepository<Permiso, int>, IPermisoRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PermisoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<Permiso>> FindPermisolByIdPerfilAsync(int id)
        {
            return await _dbContext.Set<Permiso>()
                .Include(t => t.Menu)
                .Include(t => t.Rol)
                .Where(t => t.IdRol == id)
                .ToListAsync();
        }
    }
}
