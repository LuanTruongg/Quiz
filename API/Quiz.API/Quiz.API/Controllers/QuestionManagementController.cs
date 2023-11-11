using Microsoft.AspNetCore.Mvc;
using Quiz.DTO.ModuleManagement;
using Quiz.DTO.QuestionManagement;
using Quiz.Infrastructure.Constraint;
using Quiz.Infrastructure.Http;
using Quiz.Service;
using ControllerBase = Quiz.Infrastructure.Http.ControllerBase;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Quiz.API.Controllers
{
	[Route("question-management")]
	[ApiController]
	public class QuestionManagementController : ControllerBase
	{
		private readonly IQuestionManagementService _service;

		public QuestionManagementController(IQuestionManagementService service)
		{
			_service = service;

		}
		[HttpGet]
		[ProducesResponseType(typeof(GetListQuestionResponse), 200)]
		public async Task<IActionResult> Get([FromQuery] GetListQuestionRequest request)
		{
			if (ModelState.IsValid)
			{
				return GetResponse(200, await _service.GetListQuestionAsync(request));
			}
			throw new ErrorException(400, ErrorMessage.BadRequest);
		}

		[HttpGet("{id}")]
        [ProducesResponseType(typeof(GetQuestionResponse), 200)]
        public async Task<IActionResult> Get(string id)
		{
            if (ModelState.IsValid)
            {
                return GetResponse(200, await _service.GetQuestionByIdAsync(id));
            }
            throw new ErrorException(400, ErrorMessage.BadRequest);
        }

		[HttpPost]
		[ProducesResponseType(typeof(AddQuestionResponse), 200)]
		public async Task<IActionResult> Post([FromBody] AddQuestionRequest request)
		{
			if (ModelState.IsValid)
			{
				return GetResponse(200, await _service.AddQuestionAsync(request));
			}
			throw new ErrorException(400, ErrorMessage.BadRequest);
		}

		[HttpPut("{id}")]
		[ProducesResponseType(typeof(EditQuestionResponse), 200)]
		public async Task<IActionResult> Put(string id, [FromBody] EditQuestionRequest request)
		{
			if (ModelState.IsValid)
			{
				return GetResponse(200, await _service.EditQuestionAsync(id, request));
			}
			throw new ErrorException(400, ErrorMessage.BadRequest);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(string id)
		{
			if (ModelState.IsValid)
			{
				return GetResponse(200, await _service.DeleteQuestionAsync(id));
			}
			throw new ErrorException(400, ErrorMessage.BadRequest);
		}
    }
}
