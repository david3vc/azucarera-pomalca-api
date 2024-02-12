using AzucareraPomalca.Application.Dtos.EmpleadoCursos;
using AzucareraPomalca.Application.Dtos.PuestosCursos;
using AzucareraPomalca.Application.Services;
using AzucareraPomalca.Application.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class EmpleadoCursoController : Controller
    {
        private readonly IEmpleadoCursoService _empleadoCursoService;

        public EmpleadoCursoController(IEmpleadoCursoService empleadoCursoService)
        {
            _empleadoCursoService = empleadoCursoService;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<EmpleadoCursoDto> Delete(int id)
        {
            return await _empleadoCursoService.DisabledAsync(id);
        }
    }
}
