using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.Capacitaciones;

namespace AzucareraPomalca.Application.Services
{
    public interface ICapacitacionService : ICrudService<CapacitacionDto, CapacitacionSaveDto, int>, IPageService<CapacitacionDto, CapacitacionFilterDto>
    {
    }
}
