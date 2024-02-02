using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.Empleados;

namespace AzucareraPomalca.Application.Services
{
    public interface IEmpleadoService : ICrudService<EmpleadoDto, EmpleadoSaveDto, int>, IPageService<EmpleadoDto, EmpleadoFilterDto>
    {
    }
}
