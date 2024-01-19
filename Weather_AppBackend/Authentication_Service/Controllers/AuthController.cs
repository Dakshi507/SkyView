using Microsoft.AspNetCore.Mvc;
using Authentication_Service.Service;
using Authentication_Service.Exceptions;
using Serilog;

namespace Authentication_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var authenticationResult = _authService.AuthenticateUser(model.Username, model.Password);
                    Log.Information("User logged in successfully: {Username}", model.Username);
                    return Ok(authenticationResult);
                }
                else
                {
                    Log.Warning("Invalid input data");
                    return BadRequest("Invalid input data");
                }
            }
            catch (AuthenticationException ex)
            {
                Log.Warning("Authentication error: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred while logging in user: {Username}", model.Username);
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred");
            }
        }

    }

    public class LoginRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

