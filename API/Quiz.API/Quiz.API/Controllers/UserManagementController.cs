using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quiz.DTO.BaseResponse;
using Quiz.DTO.UserManagement;
using Quiz.DTO.UserTestManagement;
using Quiz.Infrastructure.Constraint;
using Quiz.Infrastructure.Http;
using Quiz.Service;
using ControllerBase = Quiz.Infrastructure.Http.ControllerBase;

namespace Quiz.API.Controllers
{
    [Route("user-management")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;
        public UserManagementController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }
        [HttpPost]
        public async Task<IActionResult> AddUserStructure([FromBody] UserBuyingTestRequest request)
        {
            if (ModelState.IsValid)
            {
                return GetResponse(200, await _userManagementService.UserBuyingTestAsync(request));
            }
            throw new ErrorException(400, ErrorMessage.BadRequest);
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserStructureById(string userId)
        {
            if (ModelState.IsValid)
            {
                return GetResponse(200, await _userManagementService.GetUserStructuresById(userId));
            }
            throw new ErrorException(400, ErrorMessage.BadRequest);
        }
        [HttpGet("list-user")]
        public async Task<IActionResult> GetListUser([FromQuery] PagingRequest request)
        {
            if (ModelState.IsValid)
            {
                return GetResponse(200, await _userManagementService.GetListUserAsync(request));
            }
            throw new ErrorException(400, ErrorMessage.BadRequest);
        }
        [HttpGet("list-user/{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            if (ModelState.IsValid)
            {
                return GetResponse(200, await _userManagementService.GetUserByIdAsync(userId));
            }
            throw new ErrorException(400, ErrorMessage.BadRequest);
        }
        [HttpPut("list-user/{userId}")]
        public async Task<IActionResult> Edit([FromBody] EditUserRequest request, string userId)
        {
            if (ModelState.IsValid)
            {
                return GetResponse(200, await _userManagementService.EditUserAsync(request, userId));
            }
            throw new ErrorException(400, ErrorMessage.BadRequest);
        }
        [HttpGet("list-user/{userId}/get-roles")]
        public async Task<IActionResult> GetRolesUser(string userId)
        {
            if (ModelState.IsValid)
            {
                return GetResponse(200, await _userManagementService.GetRolesUserAsync(userId));
            }
            throw new ErrorException(400, ErrorMessage.BadRequest);
        }
        [HttpPut("list-user/{userId}/assign-roles")]
        public async Task<IActionResult> AssignRolesUser(string userId, [FromBody] RoleAssignRequest request)
        {
            if (ModelState.IsValid)
            {
                return GetResponse(200, await _userManagementService.AssignRolesAsync(userId, request));
            }
            throw new ErrorException(400, ErrorMessage.BadRequest);
        }
        [HttpGet("get-list-user-bought-test")]
        [ProducesResponseType(typeof(GetUserTestResponse), 200)]
        public async Task<IActionResult> GetListUserBoughtTest([FromQuery] GetListUserStructureRequest request)
        {
            if (ModelState.IsValid)
            {
                return GetResponse(200, await _userManagementService.GetListUserBoughtTestAsync(request));
            }
            throw new ErrorException(400, ErrorMessage.BadRequest);
        }
		[HttpPost("edit-profile/{id?}")]
		public async Task<IActionResult> AddUserStructure(string id, [FromBody] EditUserRequest request)
		{
			if (ModelState.IsValid)
			{
				return GetResponse(200, await _userManagementService.EditUserAsync(request, id));
			}
			throw new ErrorException(400, ErrorMessage.BadRequest);
		}
	}
}
