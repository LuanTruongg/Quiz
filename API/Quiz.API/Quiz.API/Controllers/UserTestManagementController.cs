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
        public async Task<IActionResult> GetListMajor([FromBody] AddUserTestRequest request)
        {
            if (ModelState.IsValid)
            {
                return GetResponse(200, await _service.AddUserTestAsync(request));
            }
            throw new ErrorException(400, ErrorMessage.BadRequest);
        }
    }
}
