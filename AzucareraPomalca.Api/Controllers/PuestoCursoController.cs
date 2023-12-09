using AzucareraPomalca.Application.Dtos.PuestosCursos;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class PuestoCursoController : Controller
    {
        private readonly IPuestoCursoService _puestoCursoService;

        public PuestoCursoController(IPuestoCursoService puestoCursoService)
        {
            _puestoCursoService = puestoCursoService;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<PuestoCursoDto> Delete(int id)
        {
            return await _puestoCursoService.DisabledAsync(id);
        }
    }
}
