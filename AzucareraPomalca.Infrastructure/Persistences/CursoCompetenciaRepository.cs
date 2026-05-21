using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;
using Microsoft.EntityFrameworkCore;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class CursoCompetenciaRepository : CrudRepository<CursoCompetencia, int>, ICursoCompetenciaRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CursoCompetenciaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<CursoCompetencia?> FindByIdAsync(int id)
        {
            return await _dbContext.Set<CursoCompetencia>()
                .Include(t => t.Curso).ThenInclude(t => t.TipoCurso)
                .Include(t => t.Competencia).ThenInclude(t => t.TipoCompetencia)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<CursoCompetencia>> FindByCompetenciaAsync(int idCompetencia)
        {
            return await _dbContext.Set<CursoCompetencia>()
                .Include(t => t.Curso).ThenInclude(t => t.TipoCurso)
                .Where(t => t.IdCompetencia == idCompetencia && t.State)
                .ToListAsync();
        }

        public async Task<List<CursoCompetencia>> FindByCursoAsync(int idCurso)
        {
            return await _dbContext.Set<CursoCompetencia>()
                .Include(t => t.Competencia)
                .Where(t => t.IdCurso == idCurso && t.State)
                .ToListAsync();
        }
    }
}
