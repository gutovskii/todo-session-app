using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoSession.BLL.Services.Interfaces;
using TodoSession.BLL.DTO.Auth;

namespace TodoSession.WebApi.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthUserDto dto)
        {
            var user = await _authService.Login(dto);
            if (user == null)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return new JsonResult(new { error = "not found" });
            }

            HttpContext.Session.SetInt32("UserId", user.Id);

            return Ok(new { ok = true });
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] AuthUserDto dto)
        {
            var newUser = await _authService.Register(dto);
            if (newUser == null)
            {
                Response.StatusCode = StatusCodes.Status409Conflict;
                return new JsonResult(new { error = "already exist" });
            }

            return Ok(new { ok = true });
        }

        [Route("logout")]
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Ok(new { ok = true });
        }
    }
}
