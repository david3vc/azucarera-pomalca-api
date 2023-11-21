using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;
using Microsoft.EntityFrameworkCore;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class EmpleadoRepository : CrudRepository<Empleado, int>, IEmpleadoRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmpleadoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Empleado>> FindByIdPuestoAsync(int id)
        {
            return await _dbContext.Set<Empleado>()
                .Where(t => t.IdPuesto == id)
                .ToListAsync();
        }
    }
}
