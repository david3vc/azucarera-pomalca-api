using AzucareraPomalca.Domain.Cores.Repositories;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Domain.Repositories
{
    public interface ICursoCompetenciaRepository : ICrudRepository<CursoCompetencia, int>
    {
        Task<List<CursoCompetencia>> FindByCompetenciaAsync(int idCompetencia);
        Task<List<CursoCompetencia>> FindByCursoAsync(int idCurso);
    }
}
