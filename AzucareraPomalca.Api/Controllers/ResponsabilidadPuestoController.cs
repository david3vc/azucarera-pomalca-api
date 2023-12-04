using AzucareraPomalca.Application.Dtos.ResponsabilidadesPuestos;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class ResponsabilidadPuestoController : Controller
    {
        private readonly IResponsabilidadPuestoService _responsabilidadPuestoService;

        public ResponsabilidadPuestoController(IResponsabilidadPuestoService responsabilidadPuestoService)
        {
            _responsabilidadPuestoService = responsabilidadPuestoService;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ResponsabilidadPuestoDto> Delete(int id)
        {
            return await _responsabilidadPuestoService.DisabledAsync(id);
        }
    }
}
