using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.Menus;

namespace AzucareraPomalca.Application.Services
{
    public interface IMenuService : IQueryService<MenuDto, int>
    {
    }
}
