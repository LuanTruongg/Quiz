using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quiz.DTO.Common;
using Quiz.DTO.UserManagement;
using Quiz.Infrastructure.Constraint;
using Quiz.Infrastructure.Http;
using Quiz.Service;
using ControllerBase = Quiz.Infrastructure.Http.ControllerBase;

namespace Quiz.API.Controllers
{
    [Route("common")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly ICommonService _service;

        public CommonController(ICommonService service)
        {
            _service = service;

        }
        [HttpGet("get-list-major/{id?}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(GetListMajorResponse), 200)]
        public async Task<IActionResult> GetListMajor(string id)
        {
            if (ModelState.IsValid)
            {
                return GetResponse(200, await _service.GetListMajorAsync(id));
            }
            throw new ErrorException(400, ErrorMessage.BadRequest);
        }

        [HttpGet("get-list-department")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(GetListDepartmentResponse), 200)]
        public async Task<IActionResult> GetListDepartment()
        {
            if (ModelState.IsValid)
            {
                return GetResponse(200, await _service.GetListDepartmentAsync());
            }
            throw new ErrorException(400, ErrorMessage.BadRequest);
        }
        [HttpGet("get-list-subject/{majorId?}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(GetListDepartmentResponse), 200)]
        public async Task<IActionResult> GetListSubject(string majorId)
        {
            if (ModelState.IsValid)
            {
                return GetResponse(200, await _service.GetListSubjectAsync(majorId));
            }
            throw new ErrorException(400, ErrorMessage.BadRequest);
        }
        [HttpGet("get-test-subject-code/{testStructureId?}")]
        [ProducesResponseType(typeof(GetListDepartmentResponse), 200)]
        public async Task<IActionResult> GetTestSubJectCode(string testStructureId)
        {
            if (ModelState.IsValid)
            {
                return GetResponse(200, await _service.GetTestSubjectCode(testStructureId));
            }
            throw new ErrorException(400, ErrorMessage.BadRequest);
        }
        [HttpGet("get-roles")]
        public async Task<IActionResult> GetRoles()
        {
            if (ModelState.IsValid)
            {
                return GetResponse(200, await _service.GetRolesAsync());
            }
            throw new ErrorException(400, ErrorMessage.BadRequest);
        }
        [HttpGet("get-list-teacher")]
        public async Task<IActionResult> GetListTecher()
        {
            if (ModelState.IsValid)
            {
                return GetResponse(200, await _service.GetListTeacherAsync());
            }
            throw new ErrorException(400, ErrorMessage.BadRequest);
        }
        [HttpGet("get-list-teacher-of-subject/{subjectId}")]
        public async Task<IActionResult> GetListTecherOfSubject(string subjectId)
        {
            if (ModelState.IsValid)
            {
                return GetResponse(200, await _service.GetListTeacherOfSubjectAsync(subjectId));
            }
            throw new ErrorException(400, ErrorMessage.BadRequest);
        }
    }
}
