using AzucareraPomalca.Application.Dtos.TomaDecisiones;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class TomaDecisionController : Controller
    {
        private readonly ITomaDecisionService _tomaDecisionService;

        public TomaDecisionController(ITomaDecisionService tomaDecisionService)
        {
            _tomaDecisionService = tomaDecisionService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<TomaDecisionDto>> GetSimple()
        {
            return await _tomaDecisionService.SimpleListAsync();
        }
    }
}
