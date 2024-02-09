using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.EmpleadoProfesiones;

namespace AzucareraPomalca.Application.Services
{
    public interface IEmpleadoProfesionService : ICrudService<EmpleadoProfesionDto, EmpleadoProfesionSaveDto, int>
    {
    }
}
