using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.Puestos;

namespace AzucareraPomalca.Application.Services
{
    public interface IPuestoService : ICrudService<PuestoDto, PuestoSaveDto, int>, IPageService<PuestoDto, PuestoFilterDto>
    {
    }
}
