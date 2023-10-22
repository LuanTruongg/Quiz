using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz.DTO.UserManagement;
using Quiz.Infrastructure.Constraint;
using Quiz.Infrastructure.Http;
using Quiz.Service;
using ControllerBase = Quiz.Infrastructure.Http.ControllerBase;

namespace Quiz.API.Controllers
{
    [Route("identity")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserManagementService _service;

        public AccountController(IUserManagementService service)
        {
            _service = service;

        }
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthenticationResponse), 200)]
        public async Task<IActionResult> Login([FromBody] AuthenticationRequest request)
        {
            if (ModelState.IsValid)
            {
                return GetResponse(200, await _service.AuthenticateAsync(request));
            }
            throw new ErrorException(400, ErrorMessage.BadRequest);
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (ModelState.IsValid)
            {
                return GetResponse(200, await _service.RegisterAsync(request));
            }
            throw new ErrorException(400, ErrorMessage.BadRequest);
        }
    }
}
