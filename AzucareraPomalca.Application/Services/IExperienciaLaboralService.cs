using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.ExperienciaLaborales;

namespace AzucareraPomalca.Application.Services
{
    public interface IExperienciaLaboralService : ICrudService<ExperienciaLaboralDto, ExperienciaLaboralSaveDto, int>
    {
        Task<List<ExperienciaLaboralDto>> ExperienciaLaboralByIdEmpleado(int idEmpleado);
    }
}
