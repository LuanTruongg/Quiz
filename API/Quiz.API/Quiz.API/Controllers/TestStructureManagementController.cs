using Microsoft.AspNetCore.Mvc;
using Quiz.DTO.TestStructureManagement;
using Quiz.Infrastructure.Constraint;
using Quiz.Infrastructure.Http;
using Quiz.Service;
using ControllerBase = Quiz.Infrastructure.Http.ControllerBase;

namespace Quiz.API.Controllers
{
	[Route("test-structure")]
	[ApiController]
	public class TestStructureManagementController : ControllerBase
	{
		private readonly ITestStructureManagementService _service;

		public TestStructureManagementController(ITestStructureManagementService service)
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
		public async Task<IActionResult> Post([FromBody] CreateTestStructureRequest request)
		{
			if (ModelState.IsValid)
			{
				return GetResponse(200, await _service.AddTestStructureAsync(request));
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
