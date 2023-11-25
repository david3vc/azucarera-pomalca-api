using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.Permisos;

namespace AzucareraPomalca.Application.Services
{
    public interface IPermisoService : ISaveService<PermisoDto, PermisoSaveDto, int>
    {
        Task<List<PermisoDto>> MenusByIdRolAsync(int id);
        Task<List<PermisoDto>> MenusAsync(int id);
    }
}
