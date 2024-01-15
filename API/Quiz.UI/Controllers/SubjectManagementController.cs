using Microsoft.AspNetCore.Mvc;
using Quiz.DTO.BaseResponse;
using Quiz.DTO.ModuleManagement;
using Quiz.DTO.SubjectManagement;
using Quiz.DTO.TestStructureManagement;
using Quiz.DTO.UserManagement;
using Quiz.Repository.Model;
using Quiz.UI.ServicesClient;
using Quiz.UI.ServicesClient.Implements;

namespace Quiz.UI.Controllers
{
    public class SubjectManagementController : Controller
    {
        private readonly ISubjectServiceClient _subjectServiceClient;
        private readonly ITestStructureServiceClient _testStructureServiceClient;
        private readonly IUserManagementServiceClient _userManagementServiceClient;
        private readonly IHomeServiceClient _homeServiceClient;
        private readonly IRolesService _rolesService;
        public SubjectManagementController(
            ISubjectServiceClient subjectServiceClient,
            ITestStructureServiceClient testStructureServiceClient,
            IUserManagementServiceClient userManagementServiceClient,
            IHomeServiceClient homeServiceClient,
            IRolesService rolesService)
        {
            _subjectServiceClient = subjectServiceClient;
            _testStructureServiceClient = testStructureServiceClient;
            _userManagementServiceClient = userManagementServiceClient;
            _homeServiceClient = homeServiceClient;
            _rolesService = rolesService;
        }
        public async Task<IActionResult> Index(string search, int page = 1, int pageSize = 5)
        {
            
            var userId = HttpContext.Session.GetString("UserId");
            var request = new GetListSubjectPagingRequest()
            {
                Page = page,
                PageSize = pageSize,
                UserId = userId,
                Search = search
            };
            ViewBag.Search = request.Search;
            var listsubject = await _subjectServiceClient.GetListSubjectPaging(request);
            return View(listsubject.ResultObj);
        }
        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> ListTestOfSubjectManagement(string subjectId, string search, int page = 1, int pageSize = 5)
        {
            var subject = await _subjectServiceClient.GetSubjectById(subjectId);
            ViewData["SubjectName"] = subject.ResultObj.Name;
            ViewData["SubjectId"] = subject.ResultObj.SubjectId;
            var request = new GetListTestStructureRequest()
            {
                Page = page,
                PageSize = pageSize,
                Search = search,
                SubjectId = subjectId,
            };
            if (TempData["Notify"] != null)
            {
                ViewBag.SuccessMsg = TempData["Notify"];
            }
            var listTestStructure = await _testStructureServiceClient.GetListTestStructure(request);
            ViewBag.ListTestStructure = listTestStructure;
            return View();
        }
        public async Task<IActionResult> ListSubjectManagement(string search, int page = 1, int pageSize = 5)
        {
            var userId = HttpContext.Session.GetString("UserId");
            var request = new GetListSubjectPagingRequest()
            {
                Page = page,
                PageSize = pageSize,
                UserId = userId,
                Search = search
            };
            if (TempData["Notify"] != null)
            {
                ViewBag.SuccessMsg = TempData["Notify"];
            }
            var listsubject = await _subjectServiceClient.GetListAllSubjectPaging(request);
            return View(listsubject.ResultObj);
        }
        public async Task<IActionResult> GetTeacherOfSubjectManagement(string subjectId)
        {
            var checkRoles = _rolesService.CheckAdmin(HttpContext);
            if (checkRoles is true)
            {
                var subject = await _subjectServiceClient.GetSubjectById(subjectId);
                ViewBag.SubjectId = subject.ResultObj.SubjectId;
                ViewBag.SubjectName = subject.ResultObj.Name;

                var listTeacherOfSubject = await _subjectServiceClient.GetListTeacherOfSubject(subjectId);
                ViewBag.ListTeacherOfSubject = listTeacherOfSubject;
                return View();
            }
            return Unauthorized();

        }
        public async Task<IActionResult> AddTeacherForSubjectManagement(string subjectId)
        {
            var checkRoles = _rolesService.CheckAdmin(HttpContext);
            if (checkRoles is true)
            {
                var subject = await _subjectServiceClient.GetSubjectById(subjectId);
                ViewBag.SubjectId = subject.ResultObj.SubjectId;
                ViewBag.SubjectName = subject.ResultObj.Name;
                if (TempData["Notify"] != null)
                {
                    ViewBag.Error = TempData["Notify"];
                }
                var listTeacher = await _subjectServiceClient.GetListTeacher();
                ViewBag.ListTeacher = listTeacher;
                var listTeacherOfSubject = await _subjectServiceClient.GetListTeacherOfSubject(subjectId);
                ViewBag.ListTeacherOfSubject = listTeacherOfSubject;
                return View();
            }
            return Unauthorized();

        }
        [HttpPost]
        public async Task<IActionResult> AddTeacherForSubject(AddTeacherForSubjectRequest request)
        {
            var checkRoles = _rolesService.CheckAdmin(HttpContext);
            if (checkRoles is true)
            {
                var result = await _subjectServiceClient.AddTeacherForSubject(request);
                if (result.IsSuccessed)
                {
                    TempData["Notify"] = "Thêm thành công";
                    return RedirectToAction("ListSubjectManagement", "SubjectManagement");
                }
                else
                {
                    TempData["Notify"] = result.Message;
                    return RedirectToAction("AddTeacherForSubjectManagement", "SubjectManagement");
                }
            }
            return Unauthorized();

        }
        [HttpGet]
        public async Task<IActionResult> DeleteTeacherOfSubject(string userId, string subjectId)
        {
            DeleteTeacherForSubjectRequest request = new DeleteTeacherForSubjectRequest()
            {
                SubjectId = subjectId,
                UserId = userId
            };
            var result = await _subjectServiceClient.DeleteTeacherForSubject(request);
            if (result.IsSuccessed)
            {
                return RedirectToAction(
                    "GetTeacherOfSubjectManagement",
                    "SubjectManagement",
                    new { subjectId = subjectId }
                    );
            }
            else
            {
                return RedirectToAction(
                    "GetTeacherOfSubjectManagement",
                    "SubjectManagement",
                    new { subjectId = subjectId }
                    );
            }
                
        }
        [HttpGet]
        public async Task<IActionResult> GetListUserBougthTest(string testStructureId, string subjectId)
        {
            var checkRoles = _rolesService.CheckTeacher(HttpContext);
            if (checkRoles is true)
            {
                var subject = await _subjectServiceClient.GetSubjectById(subjectId);
                ViewData["SubjectName"] = subject.ResultObj.Name;
                ViewData["SubjectId"] = subject.ResultObj.SubjectId;
                var request = new GetListUserStructureRequest()
                {
                    TestStructureId = testStructureId,
                    Page = 1,
                    PageSize = 5
                };
                var result = await _subjectServiceClient.GetListUserBoughtTest(request);
                return View(result.ResultObj);
            }
            return Unauthorized();
        }
        [HttpGet]
        public async Task<IActionResult> AddSubject()
        {
            var checkRoles = _rolesService.CheckAdmin(HttpContext);
            if (checkRoles is true)
            {
                if (TempData["Notify"] != null)
                {
                    ViewBag.Error = TempData["Notify"];
                }
                ViewBag.ListMajor = await _homeServiceClient.GetListAllMajor();
                return View();
            }
            return Unauthorized();
            
        }
        [HttpPost]
        public async Task<IActionResult> CreateSubject(AddSubjectRequest request)
        {
            var checkRoles = _rolesService.CheckAdmin(HttpContext);
            if (checkRoles is true)
            {
                var moduleRequest = new CreateModuleRequest()
                {
                    NumberOfMudule = request.Module,
                    SubjectId = request.SubjectId,
                };
                var result = await _subjectServiceClient.AddSubject(request);
                var addModule = await _subjectServiceClient.CreateModule(moduleRequest);
                if (result.IsSuccessed)
                {
                    TempData["Notify"] = "Thêm thành công";
                    return RedirectToAction("ListSubjectManagement", "SubjectManagement");
                }
                else
                {
                    TempData["Notify"] = result.Message;
                    return RedirectToAction("AddSubject", "SubjectManagement");
                }
            }
            return Unauthorized();
        }
        public async Task<IActionResult> DeleteSubjectManagement(string subjectId)
        {
            var checkRoles = _rolesService.CheckAdmin(HttpContext);
            if (checkRoles is true)
            {
                var result = await _subjectServiceClient.DeleteSubject(subjectId);
                if (result.IsSuccessed)
                {
                    TempData["Notify"] = "Xoá thành công";
                    return RedirectToAction("ListSubjectManagement", "SubjectManagement");
                }
                else
                {
                    TempData["Notify"] = result.Message;
                    return RedirectToAction("AddSubject", "SubjectManagement");
                }
            }
            return Unauthorized();
        }
    }
}
