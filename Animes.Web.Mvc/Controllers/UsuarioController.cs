using Animes.Application.DTOs.Requests;
using Animes.Application.DTOs.Responses;
using Animes.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Animes.Web.Mvc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<UsuarioController> _logger;
        public UsuarioController(IUsuarioService usuarioService, ILogger<UsuarioController> logger)
        {
            _logger = logger;
            _usuarioService = usuarioService;
        }
        [HttpGet("{userName}")]
        public async Task<ActionResult<CreateUserResponse>> GetByUserName(string userName)
        {
            _logger.LogInformation("Buscando o registro de {Entity} com o UserName: {UserName}.", "Usuario", userName);
            var result = await _usuarioService.GetUsuarioByUserName(userName);
            if(result != null)
            {
                _logger.LogInformation("Registro de {Entity} encontrado: {Data}.", "Usuario", result);
                return Ok(result);
            }
            _logger.LogWarning("Nenhum registro de {Entity} encontrado com o UserName: {UserName}.", "Usuario", userName);
            return NotFound();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<CreateUserResponse>> CreateUsuario([FromBody]CreateUserRequest createUserRequest)
        {
            _logger.LogInformation("Iniciando a criação do novo registro de {Entity} com os dados: {Data}.", "Usuario", createUserRequest); 
            var result = await _usuarioService.CreateUser(createUserRequest);
            _logger.LogInformation("Registro de {Entity} criado com sucesso. UserName: {UserName}.", "Usuario", result.UserName);
            return CreatedAtAction(nameof(GetByUserName), new { UserName = result.UserName }, result);
        }
    }
}