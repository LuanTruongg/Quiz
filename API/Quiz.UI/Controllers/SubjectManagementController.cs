using Microsoft.AspNetCore.Mvc;
using Quiz.DTO.BaseResponse;
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
        public async Task<IActionResult> Index(PagingRequest request)
        {
            var listsubject = await _subjectServiceClient.GetListSubjectPaging(request);
            return View(listsubject);
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
