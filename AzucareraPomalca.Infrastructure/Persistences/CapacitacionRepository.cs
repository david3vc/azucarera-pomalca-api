using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;
using Microsoft.EntityFrameworkCore;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class CapacitacionRepository : CrudRepository<Capacitacion, int>, ICapacitacionRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CapacitacionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<Capacitacion?> FindByIdAsync(int id)
        {
            return await _dbContext.Set<Capacitacion>()
                .Include(t => t.Modalidad)
                .Include(t => t.TipoFacilitador)
                .Include(t => t.Curso).ThenInclude(t => t.TipoCurso)
                .Include(t => t.CapacitacionEmpleados).ThenInclude(t => t.Empleado).ThenInclude(t => t.Puesto).ThenInclude(t => t.Gerencia)
                .Include(t => t.CapacitacionEmpleados).ThenInclude(t => t.Empleado).ThenInclude(t => t.Puesto).ThenInclude(t => t.Division)
                .Include(t => t.CapacitacionEmpleados).ThenInclude(t => t.Empleado).ThenInclude(t => t.Puesto).ThenInclude(t => t.Departamento)
                .Include(t => t.CapacitacionEmpleados).ThenInclude(t => t.Empleado).ThenInclude(t => t.Puesto).ThenInclude(t => t.Seccion)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
