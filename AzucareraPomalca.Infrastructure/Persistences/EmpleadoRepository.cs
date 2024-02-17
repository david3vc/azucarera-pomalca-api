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

        public override async Task<Empleado?> FindByIdAsync(int id)
        {
            return await _dbContext.Set<Empleado>()
                .Include(t => t.CondicionEmpleado)
                .Include(t => t.Puesto).ThenInclude(t => t.ClaseOcupacional).ThenInclude(t => t.GrupoOcupacional)
                .Include(t => t.Puesto).ThenInclude(t => t.Gerencia)
                .Include(t => t.Puesto).ThenInclude(t => t.Division)
                .Include(t => t.Puesto).ThenInclude(t => t.Departamento)
                .Include(t => t.Puesto).ThenInclude(t => t.Seccion)
                .Include(t => t.Puesto).ThenInclude(t => t.PuestosProfesiones.Where(t => t.State == true)).ThenInclude(t => t.Profesion)
                .Include(t => t.Puesto).ThenInclude(t => t.PuestosCursos.Where(t => t.State == true)).ThenInclude(t => t.Curso).ThenInclude(t => t.TipoCurso)
                .Include(t => t.Puesto).ThenInclude(t => t.PerfilCompetencias.Where(t => t.State == true)).ThenInclude(t => t.GradoDominio).ThenInclude(t => t.CompetenciaSimple).ThenInclude(t => t.TipoCompetencia)
                .Include(t => t.EmpleadoProfesiones.Where(t => t.State == true)).ThenInclude(t => t.Profesion)
                .Include(t => t.EmpleadoProfesiones.Where(t => t.State == true)).ThenInclude(t => t.GradoAcademico)
                .Include(t => t.CondicionEmpleado)
                .Include(t => t.ExperienciaLaborales.Where(t => t.State == true))
                .Include(t => t.EmpleadoCursos.Where(t => t.State == true)).ThenInclude(t => t.Curso).ThenInclude(t => t.TipoCurso)
                .Include(t => t.PerfilCompetenciaEmpleados.Where(t => t.State == true)).ThenInclude(t => t.GradoDominio).ThenInclude(t => t.CompetenciaSimple).ThenInclude(t => t.TipoCompetencia)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
