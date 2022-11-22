using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;
using ECommerce.Models;
using ECommerce.Data;
using ECommerce.Service;

namespace ECommerce.API.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService service, ILogger<AuthController> logger)
        {
            this._service = service;
            this._logger = logger;
        }

        [Route("auth/register")]
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] UserRegisterDTO newUser)
        {
            _logger.LogInformation("auth/register triggered");
            try
            {
                _logger.LogInformation("auth/register completed successfully");
                Guid id = await _service.CreateNewUserAndGetIdAsync(newUser);
                return id != Guid.Empty ? Ok(id) : BadRequest("username already taken");
            }
            catch
            {
                _logger.LogWarning("auth/register completed with errors");
                return BadRequest();
            }
        }


        [Route("auth/login")]
        [HttpPost]
        public async Task<ActionResult<UserDTO>> Login([FromBody] UserDTO LR)
        {
            _logger.LogInformation("auth/login triggered");
            try
            {
                if (LR.email == null || LR.password == null)
                {
                    _logger.LogInformation("Null values for email/password");
                    return BadRequest();

                } 

                _logger.LogInformation("auth/login completed successfully");
                return Ok();
            }
            catch
            {
                _logger.LogWarning("auth/login completed with errors");
                return BadRequest();
            }
        }

        [Route("auth/logout")]
        [HttpPost]
        public ActionResult Logout()
        { 
            _logger.LogInformation("auth/logout triggered");
            _logger.LogInformation("auth/logout completed successfully");
            return Ok();
        }

    }
}
