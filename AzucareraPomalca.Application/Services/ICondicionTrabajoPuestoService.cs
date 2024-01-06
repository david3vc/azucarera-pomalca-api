using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.CondicionTrabajoPuestos;

namespace AzucareraPomalca.Application.Services
{
    public interface ICondicionTrabajoPuestoService : ICrudService<CondicionTrabajoPuestoDto, CondicionTrabajoPuestoSaveDto, int>
    {
        Task<List<CondicionTrabajoPuestoDto>> GetCondicionTrabajoPuestosByIdPuesto(int idPuesto);
    }
}
