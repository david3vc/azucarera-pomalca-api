using AzucareraPomalca.Application.Dtos.Misiones;
using AzucareraPomalca.Application.Dtos.Profesiones;
using AzucareraPomalca.Application.Services;
using AzucareraPomalca.Application.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class MisionController : Controller
    {
        private readonly IMisionService _misionService;

        public MisionController(IMisionService misionService)
        {
            _misionService = misionService;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<MisionDto> Delete(int id)
        {
            return await _misionService.DisabledAsync(id);
        }
    }
}
