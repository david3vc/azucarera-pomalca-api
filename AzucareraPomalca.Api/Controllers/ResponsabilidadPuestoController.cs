using AzucareraPomalca.Api.Exceptions;
using AzucareraPomalca.Application.Dtos.ResponsabilidadesPuestos;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
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

        // GET: api/values/2
        [HttpGet("{idPuesto}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponsabilidadPuestoDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<NotFound, Ok<List<ResponsabilidadPuestoDto>>>> Get(int idPuesto)
        {
            var response = await _responsabilidadPuestoService.GetResponsabilidadPuestosByIdPuesto(idPuesto);

            return TypedResults.Ok(response);
        }
    }
}
