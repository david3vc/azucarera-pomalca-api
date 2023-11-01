using AzucareraPomalca.Application.Core.Services;
using AzucareraPomalca.Application.Dtos.TipoProfesiones;

namespace AzucareraPomalca.Application.Services.Abstractions
{
    public interface ITipoProfesionService : IQueryService<TipoProfesionDto, int>
    {
    }
}
