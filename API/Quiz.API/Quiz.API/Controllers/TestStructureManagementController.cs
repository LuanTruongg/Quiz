﻿using Microsoft.AspNetCore.Mvc;
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
		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] GetListTestStructureRequest request)
		{
            if (ModelState.IsValid)
            {
                return GetResponse(200, await _service.GetListTestStructureAsync(request));
            }
            throw new ErrorException(400, ErrorMessage.BadRequest);
        }

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(string id)
		{
            if (ModelState.IsValid)
            {
                return GetResponse(200, await _service.GetTestStructureByIdAsync(id));
            }
            throw new ErrorException(400, ErrorMessage.BadRequest);
        }

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

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(string id)
		{
            if (ModelState.IsValid)
            {
                return GetResponse(200, await _service.DeleteTestStructureAsync(id));
            }
            throw new ErrorException(400, ErrorMessage.BadRequest);
        }
	}
}
