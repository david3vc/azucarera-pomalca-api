using AzucareraPomalca.Api.Exceptions;
using AzucareraPomalca.Application.Dtos.TomaDecisionPuestos;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class TomaDecisionPuestoController : Controller
    {
        private readonly ITomaDecisionPuestoService _tomaDecisionPuestoService;

        public TomaDecisionPuestoController(ITomaDecisionPuestoService tomaDecisionPuestoService)
        {
            _tomaDecisionPuestoService = tomaDecisionPuestoService;
        }

        // GET: api/values/2
        [HttpGet("{idPuesto}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TomaDecisionPuestoDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<NotFound, Ok<List<TomaDecisionPuestoDto>>>> Get(int idPuesto)
        {
            var response = await _tomaDecisionPuestoService.GetTomaDecisionPuestosByIdPuesto(idPuesto);

            return TypedResults.Ok(response);
        }
    }
}
