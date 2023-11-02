using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.TipoProfesiones;

namespace AzucareraPomalca.Application.Services
{
    public interface ITipoProfesionService : IQueryService<TipoProfesionDto, int>
    {
    }
}
