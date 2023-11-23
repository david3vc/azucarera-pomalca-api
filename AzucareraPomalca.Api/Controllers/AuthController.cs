using AzucareraPomalca.Api.Exceptions;
using AzucareraPomalca.Application.Dtos.Usuarios;
using AzucareraPomalca.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AzucareraPomalca.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public AuthController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // POST api/values
        [HttpPost("Login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsuarioSecurityDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorValidationModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<BadRequest, Ok<UsuarioSecurityDto>>> Post([FromBody] UsuarioAuthDto userAuth)
        {
            UsuarioSecurityDto userSecurity = await _usuarioService.LoginAsync(userAuth);

            return TypedResults.Ok(userSecurity);
        }
    }
}
