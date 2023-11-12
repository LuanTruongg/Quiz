using Microsoft.AspNetCore.Mvc;
using Quiz.DTO.BaseResponse;
using Quiz.DTO.SubjectManagement;
using Quiz.DTO.TestStructureManagement;
using Quiz.Repository.Model;
using Quiz.UI.ServicesClient;
using Quiz.UI.ServicesClient.Implements;

namespace Quiz.UI.Controllers
{
    public class SubjectManagementController : Controller
    {
        private readonly ISubjectServiceClient _subjectServiceClient;
        private readonly ITestStructureServiceClient _testStructureServiceClient;
        public SubjectManagementController(
            ISubjectServiceClient subjectServiceClient,
            ITestStructureServiceClient testStructureServiceClient)
        {
            _subjectServiceClient = subjectServiceClient;
            _testStructureServiceClient = testStructureServiceClient;
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
        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }
    }
}
