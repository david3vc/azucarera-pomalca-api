using AzucareraPomalca.Application.Dtos.ExperienciaLaborales;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class ExperienciaLaboralController : Controller
    {
        private readonly IExperienciaLaboralService _experienciaLaboralService;

        public ExperienciaLaboralController(IExperienciaLaboralService experienciaLaboralService)
        {
            _experienciaLaboralService = experienciaLaboralService;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ExperienciaLaboralDto> Delete(int id)
        {
            return await _experienciaLaboralService.DisabledAsync(id);
        }
    }
}
