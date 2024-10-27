using Animes.Application.DTOs.Requests;
using Animes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Animes.Web.Mvc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria um novo token")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [Produces("text/plain")]
        public async Task<ActionResult<string>> Login(LoginRequest loginRequest)
        {
            _logger.LogInformation("Iniciando a criação do novo Token do Usuario {UserName}.", loginRequest.UserName);
            var token = await _authService.Login(loginRequest);
            if(token != null)
            {
                _logger.LogInformation("Token criado com sucesso. UserName: {UserName}.", loginRequest.UserName);
                return Ok(token);
            }
            _logger.LogWarning("Falha ao criar Token do Usuario {UserName}", loginRequest.UserName);
            return Unauthorized();
        }
    }
}