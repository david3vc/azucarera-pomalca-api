using AzucareraPomalca.Application.Dtos.EmpleadoProfesiones;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class EmpleadoProfesionController : Controller
    {
        private readonly IEmpleadoProfesionService _empleadoProfesionService;

        public EmpleadoProfesionController(IEmpleadoProfesionService empleadoProfesionService)
        {
            _empleadoProfesionService = empleadoProfesionService;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<EmpleadoProfesionDto> Delete(int id)
        {
            return await _empleadoProfesionService.DisabledAsync(id);
        }
    }
}
