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
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        [HttpGet("{userName}")]
        public async Task<ActionResult<CreateUserResponse>> GetByUserName(string userName)
        {
            var result = await _usuarioService.GetUsuarioByUserName(userName);
            if(result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<CreateUserResponse>> CreateUsuario([FromBody]CreateUserRequest createUserRequest)
        {
            var result = await _usuarioService.CreateUser(createUserRequest);

            return CreatedAtAction(nameof(GetByUserName), new { UserName = result.UserName }, result);
        }
    }
}