using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.Empleados;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Services
{
    public interface IEmpleadoService : ICrudService<EmpleadoDto, EmpleadoSaveDto, int>, IPageService<EmpleadoDto, EmpleadoFilterDto>
    {
        Task<RespuestaSimpleDto> CreateMassiveAsync(List<EmpleadoSaveDto> listSaveDto);
        Task<EmpleadoDto> EditByDocumentoAsync(Empleado empleado, EmpleadoSaveDto saveDto);
    }
}
