using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.PerfilCompetencias;

namespace AzucareraPomalca.Application.Services
{
    public interface IPerfilCompetenciaService : ICrudService<PerfilCompetenciaDto, PerfilCompetenciaSaveDto, int>
    {
    }
}
