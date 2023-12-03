using AzucareraPomalca.Application.Dtos.PuestosProfesiones;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class PuestoProfesionController: Controller
    {
        private readonly IPuestoProfesionService _puestoProfesionService;

        public PuestoProfesionController(IPuestoProfesionService puestoProfesionService)
        {
            _puestoProfesionService = puestoProfesionService;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<PuestoProfesionDto> Delete(int id)
        {
            return await _puestoProfesionService.DisabledAsync(id);
        }
    }
}
