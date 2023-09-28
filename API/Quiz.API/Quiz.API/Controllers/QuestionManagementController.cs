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
		//[HttpGet]
		//public IEnumerable<string> Get()
		//{
		//	return new string[] { "value1", "value2" };
		//}

		//[HttpGet("{id}")]
		//public string Get(int id)
		//{
		//	return "value";
		//}

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

		//[HttpPut("{id}")]
		//public void Put(int id, [FromBody] string value)
		//{
		//}

		//[HttpDelete("{id}")]
		//public void Delete(int id)
		//{
		//}
	}
}
