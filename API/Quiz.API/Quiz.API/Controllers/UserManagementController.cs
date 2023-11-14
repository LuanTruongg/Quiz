using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quiz.DTO.UserManagement;
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
    }
}
