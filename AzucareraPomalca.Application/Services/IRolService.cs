using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.Roles;

namespace AzucareraPomalca.Application.Services
{
    public interface IRolService : ICrudService<RolDto, RolSaveDto, int>, IPageService<RolDto, RolFilterDto>
    {
    }
}
