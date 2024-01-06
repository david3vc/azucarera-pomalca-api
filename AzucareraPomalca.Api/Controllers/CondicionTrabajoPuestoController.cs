using AzucareraPomalca.Api.Exceptions;
using AzucareraPomalca.Application.Dtos.CondicionTrabajoPuestos;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class CondicionTrabajoPuestoController : Controller
    {
        private readonly ICondicionTrabajoPuestoService _condicionTrabajoPuestoService;

        public CondicionTrabajoPuestoController(ICondicionTrabajoPuestoService condicionTrabajoPuestoService)
        {
            _condicionTrabajoPuestoService = condicionTrabajoPuestoService;
        }

        // GET: api/values/2
        [HttpGet("{idPuesto}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CondicionTrabajoPuestoDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<NotFound, Ok<List<CondicionTrabajoPuestoDto>>>> Get(int idPuesto)
        {
            var response = await _condicionTrabajoPuestoService.GetCondicionTrabajoPuestosByIdPuesto(idPuesto);

            return TypedResults.Ok(response);
        }
    }
}
