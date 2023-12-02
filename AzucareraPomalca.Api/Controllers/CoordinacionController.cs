using AzucareraPomalca.Application.Dtos.Coordinaciones;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class CoordinacionController : Controller
    {
        private readonly ICoordinacionService _coordinacionService;

        public CoordinacionController(ICoordinacionService coordinacionService)
        {
            _coordinacionService = coordinacionService;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<CoordinacionDto> Delete(int id)
        {
            return await _coordinacionService.DisabledAsync(id);
        }
    }
}
