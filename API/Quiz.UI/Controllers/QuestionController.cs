using Microsoft.AspNetCore.Mvc;
using Quiz.DTO.QuestionManagement;
using Quiz.Repository.Model;
using Quiz.UI.ServicesClient;
using Quiz.UI.ServicesClient.Implements;

namespace Quiz.UI.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionServiceClient _questionServiceClient;
        private readonly ISubjectServiceClient _subjectServiceClient;
        public QuestionController(IQuestionServiceClient questionServiceClient, ISubjectServiceClient subjectServiceClient)
        {
            _questionServiceClient = questionServiceClient;
            _subjectServiceClient = subjectServiceClient;

        }
        public async Task<IActionResult> Index(GetListQuestionRequest request, string subjectName)
        {
            var filterRequest = new GetListQuestionRequest()
            {
                Page = request.Page == 0? 1 : request.Page,
                PageSize = request.PageSize == 0 ? 5 : request.PageSize,
                Search = request.Search,
                SubjectId = request.SubjectId,
            };
            ViewData["SubjectId"] = request.SubjectId;
            ViewData["SubjectName"] = subjectName;
            var listQuestion = await _questionServiceClient.GetListQuestionOfSubject(filterRequest);
            return View(listQuestion.ResultObj);
        }
        [HttpGet]
        public async Task<IActionResult> Create(string subjectId, string subjectName)
        {
            ViewData["SubjectId"] = subjectId;
            ViewData["SubjectName"] = subjectName;
            ViewBag.ListModule = await _subjectServiceClient.GetListModuleOfSubject(subjectId);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateQuestion(CreateQuestionRequestViewModel request)
        {
            var result = await _questionServiceClient.CreateQuestionOfModule(request);
            if (!result.IsSuccessed)
            {
                TempData["Notify"] = result.Message;
                return RedirectToAction(
                    "ListTestOfSubjectManagement",
                    "SubjectManagement",
                    new
                    {
                        subjectId = request.SubjectId,
                        subjectName = request.SubjectName,
                        page = 1,
                        pageSize = 5
                    }
                    );
            }
            else
            {
                TempData["Notify"] = "Thêm câu hỏi thành công";
                return RedirectToAction(
                    "ListTestOfSubjectManagement",
                    "SubjectManagement",
                    new
                    {
                        subjectId = request.SubjectId,
                        subjectName = request.SubjectName,
                        page = 1,
                        pageSize = 5
                    }
                    );
            }
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
