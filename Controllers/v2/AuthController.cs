using HelloToAsp.Contracts;
using HelloToAsp.Dtos.Auth;
using Microsoft.AspNetCore.Mvc;

namespace HelloToAsp.Controllers.v2
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthManager authManager, ILogger<AuthController> logger)
        {
            _authManager = authManager;
            _logger = logger;
        }

        // POST: api/Auth/register
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Register([FromForm] RegUserDto userDto)
        {
            _logger.LogInformation($"Registration Attempt for {userDto.PhoneNumber}");

            //try
            //{

            var errors = await _authManager.Register(userDto);

            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            return Ok();

            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, $"Something Went Wrong in the {nameof(Register)} - Registration Attempt for {userDto.PhoneNumber}");

            //    return Problem($"Something Went Wrong in the {nameof(Register)}", statusCode: 500);
            //}

        }

        // POST: api/Auth/login
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Login([FromForm] LogUserDto userDto)
        {
            var authResponse = await _authManager.Login(userDto);

            if (authResponse == null)
            {
                return Unauthorized();
            }

            return Ok(authResponse);
        }

        // POST: api/Auth/RefreshToken
        [HttpPost]
        [Route("RefreshToken")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> RefreshToken([FromForm] AuthResponseDto request)
        {
            var authResponse = await _authManager.VerifyRefreshToken(request);

            if (authResponse == null)
            {
                return Unauthorized();
            }

            return Ok(authResponse);
        }
    }
}
