using Microsoft.AspNetCore.Mvc;
using Quiz.DTO.ModuleManagement;
using Quiz.Infrastructure.Constraint;
using Quiz.Infrastructure.Http;
using Quiz.Service;
using ControllerBase = Quiz.Infrastructure.Http.ControllerBase;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Quiz.API.Controllers
{
	[Route("module-management")]
	[ApiController]
	public class ModuleManagementController : ControllerBase
	{
		private readonly IModuleManagementService _service;

		public ModuleManagementController(IModuleManagementService service)
		{
			_service = service;

		}
		[HttpGet]
		[ProducesResponseType(typeof(GetModuleResponse), 200)]
		public async Task<IActionResult> Get([FromQuery]GetModuleRequest request)
		{
			if (ModelState.IsValid)
			{
				return GetResponse(200, await _service.GetListModuleAsync(request));
			}
			throw new ErrorException(400, ErrorMessage.BadRequest);
		}

		//// GET api/<ModuleManagementController>/5
		//[HttpGet("{id}")]
		//public string Get(int id)
		//{
		//	return "value";
		//}

		//[HttpPost]
		//public async Task<IActionResult> Post([FromBody] string value)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		return BadRequest(ModelState);
		//	}
		//	var result = await _service.GetListModuleAsync(request);
		//	if (result.IsSuccessed)
		//	{
		//		return Ok(result);
		//	}
		//	return NotFound();
		//}

		//// PUT api/<ModuleManagementController>/5
		//[HttpPut("{id}")]
		//public void Put(int id, [FromBody] string value)
		//{
		//}

		//// DELETE api/<ModuleManagementController>/5
		//[HttpDelete("{id}")]
		//public void Delete(int id)
		//{
		//}
	}
}
