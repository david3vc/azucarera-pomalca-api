using AzucareraPomalca.Core.Paginations;
using AzucareraPomalca.Domain.Cores.Paginations;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;
using Microsoft.EntityFrameworkCore;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class ProfesionRepository : CrudRepository<Profesion, int>, IProfesionRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IPaginator<Profesion> _paginator;

        public ProfesionRepository(ApplicationDbContext dbContext, IPaginator<Profesion> paginator) : base(dbContext)
        {
            _dbContext = dbContext;
            _paginator = paginator;
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

        public async Task<ResponsePagination<Profesion>> PaginatedSearch(RequestPagination<Profesion> request)
        {
            var filter = request.Filter;

            var query = _dbContext.Set<Profesion>().AsQueryable();


            if (filter is not null)
            {
                query = query
                    .Where(f =>
                        (string.IsNullOrWhiteSpace(filter.Codigo) || f.Codigo.ToUpper().Contains(filter.Codigo.ToUpper()))
                        && (string.IsNullOrWhiteSpace(filter.Nombre) || f.Nombre.ToUpper().Contains(filter.Nombre.ToUpper()))
                    );
            }

            query = query.OrderByDescending(f => f.Id);

            return await _paginator.Paginate(query, request);
        }
    }
}
