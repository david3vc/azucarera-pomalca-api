using AzucareraPomalca.Core.Paginations;
using AzucareraPomalca.Domain.Cores.Paginations;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class RolRepository : CrudRepository<Rol, int>, IRolRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IPaginator<Rol> _paginator;

        public RolRepository(ApplicationDbContext dbContext, IPaginator<Rol> paginator) : base(dbContext)
        {
            _paginator = paginator;
            _dbContext = dbContext;
        }

        public async Task<ResponsePagination<Rol>> PaginatedSearch(RequestPagination<Rol> request)
        {
            var filter = request.Filter;

            var query = _dbContext.Set<Rol>().AsQueryable();


            if (filter is not null)
            {
                query = query
                    .Where(f =>
                        (string.IsNullOrWhiteSpace(filter.Descripcion) || f.Descripcion.ToUpper().Contains(filter.Descripcion.ToUpper()))
                        && (string.IsNullOrWhiteSpace(filter.Nombre) || f.Nombre.ToUpper().Contains(filter.Nombre.ToUpper()))
                    );
            }

            query = query.OrderByDescending(f => f.Id);

            return await _paginator.Paginate(query, request);
        }
    }
}
