using LogiDriveBE.AUTH.Aao;
using LogiDriveBE.UTILS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogiDriveBE.Controllers.Authentication
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<OperationResponse<string>>> Login([FromBody] LoginModel model)
        {
            var response = await _authService.AuthenticateAsync(model.Email, model.Password);

            if (response.Code != 200)
            {
                return StatusCode(response.Code, response);
            }

            return Ok(response);
        }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
