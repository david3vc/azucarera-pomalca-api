using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.CapacitacionesEmpleados;

namespace AzucareraPomalca.Application.Services
{
    public interface ICapacitacionEmpleadoService : ICrudService<CapacitacionEmpleadoDto, CapacitacionEmpleadoSaveDto, int>
    {
    }
}
