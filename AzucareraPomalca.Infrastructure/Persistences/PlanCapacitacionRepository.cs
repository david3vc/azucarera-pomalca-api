using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;
using Microsoft.EntityFrameworkCore;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class PlanCapacitacionRepository : CrudRepository<PlanCapacitacion, int>, IPlanCapacitacionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PlanCapacitacionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PlanCapacitacion?> FindDetalleByIdAsync(int id)
        {
            return await _dbContext.Set<PlanCapacitacion>()
                .AsNoTracking()
                .Include(p => p.EstadoPlan)
                .Include(p => p.Capacitaciones).ThenInclude(c => c.Curso).ThenInclude(cu => cu.TipoCurso)
                .Include(p => p.Capacitaciones).ThenInclude(c => c.Modalidad)
                .Include(p => p.Capacitaciones).ThenInclude(c => c.TipoFacilitador)
                .Include(p => p.Capacitaciones).ThenInclude(c => c.Competencia)
                .Include(p => p.Capacitaciones).ThenInclude(c => c.CapacitacionEmpleados)
                    .ThenInclude(ce => ce.Empleado).ThenInclude(e => e.Puesto).ThenInclude(pu => pu.Gerencia)
                .Include(p => p.Capacitaciones).ThenInclude(c => c.CapacitacionEmpleados)
                    .ThenInclude(ce => ce.Empleado).ThenInclude(e => e.Puesto).ThenInclude(pu => pu.Division)
                .Include(p => p.Capacitaciones).ThenInclude(c => c.CapacitacionEmpleados)
                    .ThenInclude(ce => ce.Empleado).ThenInclude(e => e.Puesto).ThenInclude(pu => pu.Departamento)
                .Include(p => p.Capacitaciones).ThenInclude(c => c.CapacitacionEmpleados)
                    .ThenInclude(ce => ce.Empleado).ThenInclude(e => e.Puesto).ThenInclude(pu => pu.Seccion)
                .Include(p => p.Capacitaciones).ThenInclude(c => c.CapacitacionEmpleados)
                    .ThenInclude(ce => ce.Empleado).ThenInclude(e => e.Puesto)
                        .ThenInclude(pu => pu.ClaseOcupacional).ThenInclude(co => co.GrupoOcupacional)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
