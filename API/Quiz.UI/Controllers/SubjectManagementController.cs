using Microsoft.AspNetCore.Mvc;
using Quiz.DTO.BaseResponse;
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
        public SubjectManagementController(
            ISubjectServiceClient subjectServiceClient,
            ITestStructureServiceClient testStructureServiceClient,
            IUserManagementServiceClient userManagementServiceClient)
        {
            _subjectServiceClient = subjectServiceClient;
            _testStructureServiceClient = testStructureServiceClient;
            _userManagementServiceClient = userManagementServiceClient;
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
            var listsubject = await _subjectServiceClient.GetListSubjectPaging(request);
            return View(listsubject.ResultObj);
        }
        public async Task<IActionResult> GetTeacherOfSubjectManagement(string subjectId)
        {
            var subject = await _subjectServiceClient.GetSubjectById(subjectId);
            ViewBag.SubjectId = subject.ResultObj.SubjectId;
            ViewBag.SubjectName = subject.ResultObj.Name;

            var listTeacherOfSubject = await _subjectServiceClient.GetListTeacherOfSubject(subjectId);
            ViewBag.ListTeacherOfSubject = listTeacherOfSubject;
            return View();
        }
        public async Task<IActionResult> AddTeacherForSubjectManagement(string subjectId)
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
        [HttpPost]
        public async Task<IActionResult> AddTeacherForSubject(AddTeacherForSubjectRequest request)
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
        [HttpGet]
        public async Task<IActionResult> GetListUserBougthTest(string testStructureId)
        {
            var request = new GetListUserStructureRequest()
            {
                TestStructureId = testStructureId,
                Page = 1,
                PageSize = 5
            };
            var result = await _subjectServiceClient.GetListUserBoughtTest(request);
            return View(result.ResultObj);          
        }
    }
}
