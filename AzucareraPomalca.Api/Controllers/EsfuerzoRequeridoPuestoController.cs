using AzucareraPomalca.Api.Exceptions;
using AzucareraPomalca.Application.Dtos.EsfuerzoRequeridoPuestos;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class EsfuerzoRequeridoPuestoController : Controller
    {
        private readonly IEsfuerzoRequeridoPuestoService _esfuerzoRequeridoPuestoService;

        public EsfuerzoRequeridoPuestoController(IEsfuerzoRequeridoPuestoService esfuerzoRequeridoPuestoService)
        {
            _esfuerzoRequeridoPuestoService = esfuerzoRequeridoPuestoService;
        }

        // GET: api/values/2
        [HttpGet("{idPuesto}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EsfuerzoRequeridoPuestoDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<NotFound, Ok<List<EsfuerzoRequeridoPuestoDto>>>> Get(int idPuesto)
        {
            var response = await _esfuerzoRequeridoPuestoService.GetEsfuerzoRequeridoPuestosByIdPuesto(idPuesto);

            return TypedResults.Ok(response);
        }
    }
}
