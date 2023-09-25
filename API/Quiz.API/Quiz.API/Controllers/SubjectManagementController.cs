using Microsoft.AspNetCore.Mvc;
using Quiz.DTO.SubjectManagement;
using Quiz.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Quiz.API.Controllers
{
	[Route("subject-management")]
	[ApiController]
	public class SubjectManagementController : ControllerBase
	{
		private readonly ISubjectManagementService _service;

		public SubjectManagementController(ISubjectManagementService service)
        {
			_service = service;

		}
        [HttpGet]
		public async Task<IActionResult> Get()
		{
			var result = await _service.GetListSubjectsAsync();
			return Ok(result);
		}

		// GET api/<SubjectManagementController>/5
		//[HttpGet("{id}")]
		//public string Get(int id)
		//{
		//	return "value";
		//}

		// POST api/<SubjectManagementController>
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] AddSubjectRequest request)
		{
			if (ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var result = await _service.AddSubjectsAsync(request);
			return Ok(result);
		}

		// PUT api/<SubjectManagementController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<SubjectManagementController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
