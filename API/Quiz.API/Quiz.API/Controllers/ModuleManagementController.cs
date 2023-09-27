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
		public async Task<IActionResult> GetListModule([FromQuery]GetListModuleRequest request)
		{
			if (ModelState.IsValid)
			{
				return GetResponse(200, await _service.GetListModuleAsync(request));
			}
			throw new ErrorException(400, ErrorMessage.BadRequest);
		}

		[HttpGet("{moduleId}")]
		[ProducesResponseType(typeof(GetModuleResponse), 200)]
		public async Task<IActionResult> GetModuleById(string moduleId)
		{
			if (ModelState.IsValid)
			{
				return GetResponse(200, await _service.GetModuleByIdAsync(moduleId));
			}
			throw new ErrorException(400, ErrorMessage.BadRequest);
		}

		[HttpPost]
		[ProducesResponseType(typeof(CreateModuleResponse), 200)]
		public async Task<IActionResult> Post([FromBody] CreateModuleRequest request)
		{
			if (ModelState.IsValid)
			{
				return GetResponse(200, await _service.CreateListModuleAsync(request));
			}
			throw new ErrorException(400, ErrorMessage.BadRequest);
		}

		[HttpPut("{id}")]
		[ProducesResponseType(typeof(EditModuleRequest), 200)]
		public async Task<IActionResult> Put(string id, [FromBody] EditModuleRequest request)
		{
			if (ModelState.IsValid)
			{
				return GetResponse(200, await _service.EditModuleAsync(id, request));
			}
			throw new ErrorException(400, ErrorMessage.BadRequest);
		}

		//// DELETE api/<ModuleManagementController>/5
		//[HttpDelete("{id}")]
		//public void Delete(int id)
		//{
		//}
	}
}
