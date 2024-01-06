using AzucareraPomalca.Domain.Cores.Repositories;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Domain.Repositories
{
    public interface IPuestoCursoRepository : ICrudRepository<PuestoCurso, int>
    {
        Task<List<PuestoCurso>> GetPuestoCursosByIdPuesto(int idPuesto);
    }
}
