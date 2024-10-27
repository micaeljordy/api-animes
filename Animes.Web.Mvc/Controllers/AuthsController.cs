using Animes.Application.DTOs.Requests;
using Animes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Animes.Web.Mvc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthsController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthsController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Login(LoginRequest loginRequest)
        {
            var token = await _authService.Login(loginRequest);
            if(token != null)
            {
                return token;
            }
            return Unauthorized();
        }
    }
}