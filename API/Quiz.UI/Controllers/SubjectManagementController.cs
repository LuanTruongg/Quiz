using Microsoft.AspNetCore.Mvc;
using Quiz.DTO.BaseResponse;
using Quiz.DTO.SubjectManagement;
using Quiz.UI.ServicesClient;

namespace Quiz.UI.Controllers
{
    public class SubjectManagementController : Controller
    {
        private readonly ISubjectServiceClient _subjectServiceClient;
        public SubjectManagementController(ISubjectServiceClient subjectServiceClient)
        {
            _subjectServiceClient = subjectServiceClient;
        }
        public async Task<IActionResult> Index(string search, int page = 1, int pageSize = 1)
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
        public IActionResult Details()
        {
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
