using Microsoft.AspNetCore.Mvc;
using Quiz.DTO.QuestionManagement;
using Quiz.UI.ServicesClient;

namespace Quiz.UI.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionServiceClient _questionServiceClient;
        public QuestionController(IQuestionServiceClient questionServiceClient)
        {
            _questionServiceClient = questionServiceClient;
        }
        public async Task<IActionResult> Index(GetListQuestionRequest request)
        {
            var filterRequest = new GetListQuestionRequest()
            {
                Page = request.Page == 0? 1 : request.Page,
                PageSize = request.PageSize == 0 ? 5 : request.PageSize,
                Search = request.Search,
                SubjectId = request.SubjectId,
            };
            var listQuestion = await _questionServiceClient.GetListQuestionOfSubject(filterRequest);
            return View(listQuestion.ResultObj);
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
