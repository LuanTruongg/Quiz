﻿using Microsoft.AspNetCore.Mvc;
using Quiz.DTO.TestSubjectManagement;
using Quiz.Infrastructure.Constraint;
using Quiz.Infrastructure.Http;
using Quiz.Repository.Model;
using Quiz.Service;
using ControllerBase = Quiz.Infrastructure.Http.ControllerBase;

namespace Quiz.API.Controllers
{
	[Route("test-subject-management")]
	[ApiController]
	public class TestSubjectManagementController : ControllerBase
	{
		private readonly ITestSubjectManagementService _service;

		public TestSubjectManagementController(ITestSubjectManagementService service)
		{
			_service = service;

		}
		//// GET: api/<TestSubjectManagementController>
		[HttpGet]
		public async Task<IActionResult> Get([FromQuery]GetListQuestionOfTestRequest request)
		{
            if (ModelState.IsValid)
            {
                return GetResponse(200, await _service.GetListQuestionOfTestAsync(request));
            }
            throw new ErrorException(400, ErrorMessage.BadRequest);
        }

		//// GET api/<TestSubjectManagementController>/5
		//[HttpGet("{id}")]
		//public string Get(int id)
		//{
		//	return "value";
		//}

		// POST api/<TestSubjectManagementController>
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateTestSubjectRequest request)
		{
			if (ModelState.IsValid)
			{
				return GetResponse(200, await _service.CreateTestSubjectAsync(request));
			}
			throw new ErrorException(400, ErrorMessage.BadRequest);
		}
		[HttpPost("create-speaking")]
		public async Task<IActionResult> CreateSpeaking([FromBody] CreateTestSubjectSpeakingRequest request)
		{
			if (ModelState.IsValid)
			{
				return GetResponse(200, await _service.CreateSpeakingTestSubjectAsync(request));
			}
			throw new ErrorException(400, ErrorMessage.BadRequest);
		}

		//// PUT api/<TestSubjectManagementController>/5
		//[HttpPut("{id}")]
		//public void Put(int id, [FromBody] string value)
		//{
		//}

		[HttpDelete("{testSubjectCode}")]
		public async Task<IActionResult> Delete(string testSubjectCode)
		{
			if (ModelState.IsValid)
			{
				return GetResponse(200, await _service.DeleteTestSubject(testSubjectCode));
			}
			throw new ErrorException(400, ErrorMessage.BadRequest);
		}
	}
}
