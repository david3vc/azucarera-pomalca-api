using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Cores.Repositories;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Domain.Repositories
{
    public interface ICursoRepository : ICrudRepository<Curso, int>
    {
        Task<PagedResult<CursoDuroSugerido>> ListarCursosDurosSugeridosAsync(Paging paging, CursoDuroSugerido request);
    }
}
