using Microsoft.AspNetCore.Mvc;
using Quiz.DTO.BaseResponse;
using Quiz.DTO.SubjectManagement;
using Quiz.Infrastructure.Constraint;
using Quiz.Infrastructure.Http;
using Quiz.Service;
using ControllerBase = Quiz.Infrastructure.Http.ControllerBase;

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
		[ProducesResponseType(typeof(GetSubjectResponse), 200)]
		public async Task<IActionResult> Get()
		{
			if (ModelState.IsValid)
			{
				return GetResponse(200, await _service.GetListSubjectsAsync());
			}

			throw new ErrorException(400, ErrorMessage.BadRequest);
		}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetSubjectResponse), 200)]
        public async Task<IActionResult> GetSubjectById(string id)
        {
            if (ModelState.IsValid)
            {
                return GetResponse(200, await _service.GetSubjectByIdAsync(id));
            }

            throw new ErrorException(400, ErrorMessage.BadRequest);
        }
        [HttpPost] 
		[ProducesResponseType(typeof(AddSubjectResponse), 200)]
		public async Task<IActionResult> Post([FromBody] AddSubjectRequest request)
		{
			if (ModelState.IsValid)
			{
				return GetResponse(200, await _service.AddSubjectsAsync(request));
			}
			throw new ErrorException(400, ErrorMessage.BadRequest);
		}
		[HttpGet("get-list-subject-paging")]
		[ProducesResponseType(typeof(GetListSubjectPagingResponse), 200)]
		public async Task<IActionResult> GetListSubjectPaging([FromQuery] GetListSubjectPagingRequest request)
		{
			if (ModelState.IsValid)
			{
				return GetResponse(200, await _service.GetListSubjectsPagingAsync(request));
			}
			throw new ErrorException(400, ErrorMessage.BadRequest);
		}
        [HttpPost("add-teacher-for-subject")]
        [ProducesResponseType(typeof(GetListSubjectPagingResponse), 200)]
        public async Task<IActionResult> AddTeacherForSubject([FromBody] AddTeacherForSubjectRequest request)
        {
            if (ModelState.IsValid)
            {
                return GetResponse(200, await _service.AddTeachersForSubjectAsync(request));
            }
            throw new ErrorException(400, ErrorMessage.BadRequest);
        }
        [HttpPost("delete-teacher-for-subject")]
        [ProducesResponseType(typeof(GetListSubjectPagingResponse), 200)]
        public async Task<IActionResult> DeleteTeacherForSubject([FromBody] DeleteTeacherForSubjectRequest request)
        {
            if (ModelState.IsValid)
            {
                return GetResponse(200, await _service.DeleteTeachersForSubjectAsync(request));;
            }
            throw new ErrorException(400, ErrorMessage.BadRequest);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(GetListSubjectPagingResponse), 200)]
        public async Task<IActionResult> DeleteSubject(string id)
        {
            if (ModelState.IsValid)
            {
                return GetResponse(200, await _service.DeleteSubjectAsync(id));
            }
            throw new ErrorException(400, ErrorMessage.BadRequest);
        }

    }
}
