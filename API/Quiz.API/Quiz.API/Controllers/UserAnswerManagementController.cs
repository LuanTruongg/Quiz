using Microsoft.AspNetCore.Mvc;
using Quiz.DTO.UserAnswerManagement;
using Quiz.DTO.UserTestManagement;
using Quiz.Infrastructure.Constraint;
using Quiz.Infrastructure.Http;
using Quiz.Service;
using ControllerBase = Quiz.Infrastructure.Http.ControllerBase;

namespace Quiz.API.Controllers
{
    [Route("user-answer-management")]
    [ApiController]
    public class UserAnswerManagementController : ControllerBase
    {
        private readonly IUserAnswerManagementService _service;

        public UserAnswerManagementController(IUserAnswerManagementService service)
        {
            _service = service;

        }

        [HttpPost]
        [ProducesResponseType(typeof(AddUserAnswerResponse), 200)]
        public async Task<IActionResult> Post([FromBody] List<AddUserAnswerRequest> request)
        {
            if (ModelState.IsValid)
            {
                return GetResponse(200, await _service.AddUserAnswerAsync(request));
            }
            throw new ErrorException(400, ErrorMessage.BadRequest);
        }

    }
}
