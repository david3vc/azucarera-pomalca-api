using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;
using Microsoft.EntityFrameworkCore;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class PuestoRepository : CrudRepository<Puesto, int>, IPuestoRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PuestoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<IReadOnlyList<Puesto>> FindAllAsync()
        {
            return await _dbContext.Set<Puesto>()
                .Include(t => t.PuestoSupervisor)
                .Include(t => t.ClaseOcupacional).ThenInclude(t => t.GrupoOcupacional)
                .Include(t => t.Gerencia)
                .Include(t => t.Division)
                .Include(t => t.Departamento)
                .Include(t => t.Seccion)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Puesto>> FindByGerenciaAsync(string nombreGerencia)
        {
            return await _dbContext.Set<Puesto>()
                .Where(t => t.Gerencia.Nombre.ToUpper().Contains(nombreGerencia.ToUpper()))
                .ToListAsync();
        }

        public override async Task<Puesto?> FindByIdAsync(int id)
        {
            return await _dbContext.Set<Puesto>()
                .Include(t => t.PuestoSupervisor)
                .Include(t => t.ClaseOcupacional).ThenInclude(t => t.GrupoOcupacional)
                .Include(t => t.Gerencia)
                .Include(t => t.Division)
                .Include(t => t.Departamento)
                .Include(t => t.Seccion)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Puesto>> SearchByUnidadOrganizacionalAsync(Puesto request)
        {
            return await _dbContext.Set<Puesto>()
                .Where(t =>
                    (t.IdGerencia == request.IdGerencia)
                    && (t.IdDivision == request.IdDivision)
                    && (t.IdDepartamento == request.IdDepartamento)
                    && (t.IdSeccion == request.IdSeccion)
                )
                .ToListAsync();
        }
    }
}
