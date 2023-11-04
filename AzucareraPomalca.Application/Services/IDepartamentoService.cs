using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.Departamentos;

namespace AzucareraPomalca.Application.Services
{
    public interface IDepartamentoService : IQueryService<DepartamentoDto, int>
    {
    }
}
