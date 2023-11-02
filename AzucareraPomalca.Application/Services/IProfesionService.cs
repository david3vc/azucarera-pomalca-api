using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.Profesiones;

namespace AzucareraPomalca.Application.Services
{
    public interface IProfesionService : ICrudService<ProfesionDto, ProfesionSaveDto, int>, IPaginatedService<ProfesionDto, ProfesionFilterDto>
    {
    }
}
