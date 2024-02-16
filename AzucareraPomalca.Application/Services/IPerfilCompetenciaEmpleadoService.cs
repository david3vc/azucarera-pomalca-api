using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.PerfilCompetenciaEmpleados;

namespace AzucareraPomalca.Application.Services
{
    public interface IPerfilCompetenciaEmpleadoService : ICrudService<PerfilCompetenciaEmpleadoDto, PerfilCompetenciaEmpleadoSaveDto, int>
    {
    }
}
