using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.PlanesCapacitacion;

namespace AzucareraPomalca.Application.Services
{
    public interface IPlanCapacitacionService :
        ICrudService<PlanCapacitacionDto, PlanCapacitacionSaveDto, int>,
        IPageService<PlanCapacitacionDto, PlanCapacitacionFilterDto>
    {
        Task<PlanCapacitacionDto> AprobarAsync(int id);
        Task<PlanCapacitacionDto> CerrarAsync(int id);
        Task<PlanResumenDto> CalcularResumenAsync(int id);
    }
}
