using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.Coordinaciones;

namespace AzucareraPomalca.Application.Services
{
    public interface ICoordinacionService : ICrudService<CoordinacionDto, CoordinacionSaveDto, int>
    {
    }
}
