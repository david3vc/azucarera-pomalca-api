using AzucareraPomalca.Application.Dtos.CondicionTrabajos;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class CondicionTrabajoController : Controller
    {
        private readonly ICondicionTrabajoService _condicionTrabajoService;

        public CondicionTrabajoController(ICondicionTrabajoService condicionTrabajoService)
        {
            _condicionTrabajoService = condicionTrabajoService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<CondicionTrabajoDto>> GetSimple()
        {
            return await _condicionTrabajoService.SimpleListAsync();
        }
    }
}
