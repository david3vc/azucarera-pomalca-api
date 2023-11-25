using AzucareraPomalca.Application.Dtos.Menus;
using AzucareraPomalca.Application.Dtos.Roles;
using AzucareraPomalca.Application.Services;
using AzucareraPomalca.Application.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<MenuDto>> Get()
        {
            return await _menuService.FindAllAsync();
        }
    }
}
