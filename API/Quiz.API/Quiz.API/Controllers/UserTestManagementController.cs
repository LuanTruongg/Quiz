using Microsoft.AspNetCore.Mvc;
using Quiz.DTO.UserTestManagement;
using Quiz.Infrastructure.Constraint;
using Quiz.Infrastructure.Http;
using Quiz.Service;
using ControllerBase = Quiz.Infrastructure.Http.ControllerBase;

namespace Quiz.API.Controllers
{
    [Route("user-test-management")]
    [ApiController]
    public class UserTestManagementController : ControllerBase
    {
        private readonly IUserTestManagementService _service;

        public UserTestManagementController(IUserTestManagementService service)
        {
            _service = service;

        }

        [HttpPost]
        [ProducesResponseType(typeof(AddUserTestResponse), 200)]
        public async Task<IActionResult> AddUserTest([FromBody] AddUserTestRequest request)
        {
            if (ModelState.IsValid)
            {
                return GetResponse(200, await _service.AddUserTestAsync(request));
            }
            throw new ErrorException(400, ErrorMessage.BadRequest);
        }

        [HttpPost("get-result-user-test")]
        [ProducesResponseType(typeof(GetResultUserTestResponse), 200)]
        public async Task<IActionResult> GetResultUserTest([FromBody] GetResultUserTestRequest request)
        {
            if (ModelState.IsValid)
            {
                return GetResponse(200, await _service.GetResultUserTestAsync(request));
            }
            throw new ErrorException(400, ErrorMessage.BadRequest);
        }
        [HttpGet("get-list-result-user-test/{userId}")]
        [ProducesResponseType(typeof(GetUserTestResponse), 200)]
        public async Task<IActionResult> GetListResultUserTest(string userId)
        {
            if (ModelState.IsValid)
            {
                return GetResponse(200, await _service.GetListResultUserTestAsync(userId));
            }
            throw new ErrorException(400, ErrorMessage.BadRequest);
        }
        [HttpGet("get-list-result-user-test-management")]
        [ProducesResponseType(typeof(GetUserTestResponse), 200)]
        public async Task<IActionResult> GetListResultUserTest([FromQuery] GetListResultUserTestRequest request)
        {
            if (ModelState.IsValid)
            {
                return GetResponse(200, await _service.GetListResultUserTestManagementAsync(request));
            }
            throw new ErrorException(400, ErrorMessage.BadRequest);
        }
    }
}
