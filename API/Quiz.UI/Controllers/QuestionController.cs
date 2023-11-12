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
        public async Task<IActionResult> Index(GetListQuestionRequest request)
        {
            var filterRequest = new GetListQuestionRequest()
            {
                Page = request.Page == 0? 1 : request.Page,
                PageSize = request.PageSize == 0 ? 5 : request.PageSize,
                Search = request.Search,
                SubjectId = request.SubjectId,
            };
            var subject = await _subjectServiceClient.GetSubjectById(request.SubjectId);
            ViewData["SubjectId"] = subject.ResultObj.SubjectId;
            ViewData["SubjectName"] = subject.ResultObj.Name;
            var listQuestion = await _questionServiceClient.GetListQuestionOfSubject(filterRequest);
            return View(listQuestion.ResultObj);
        }
        [HttpGet]
        public async Task<IActionResult> Create(string subjectId)
        {
            var subject = await _subjectServiceClient.GetSubjectById(subjectId);
            ViewData["SubjectId"] = subject.ResultObj.SubjectId;
            ViewData["SubjectName"] = subject.ResultObj.Name;
            var listModule = await _subjectServiceClient.GetListModuleOfSubject(subjectId);
            ViewBag.ListModule = listModule.ResultObj;
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
                    "Index",
                    "Question",
                    new
                    {
                        subjectId = request.SubjectId,
                        page = 1,
                        pageSize = 5
                    }
                    );
            }
            else
            {
                TempData["Notify"] = "Thêm câu hỏi thành công";
                return RedirectToAction(
                    "Index",
                    "Question",
                    new
                    {
                        subjectId = request.SubjectId,
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
        [HttpGet]
        public async Task<IActionResult> Edit(string questionId, string subjectId)
        {
            ViewData["QuestionId"] = questionId;
            var subject = await _subjectServiceClient.GetSubjectById(subjectId);
            ViewData["SubjectId"] = subject.ResultObj.SubjectId;
            ViewData["SubjectName"] = subject.ResultObj.Name;

            var listModule = await _subjectServiceClient.GetListModuleOfSubject(subjectId);
            ViewBag.ListModule = listModule.ResultObj;
            var data = await _questionServiceClient.GetQuestionById(questionId);
            return View(data.ResultObj);
        }
        [HttpPost]
        public async Task<IActionResult> EditQuestion(GetQuestionResponse request)
        {
            if (string.IsNullOrEmpty(request.Answer))
            {
                TempData["Validation"] = "Vui lòng chọn đáp án đúng";
                return RedirectToAction(
                    "Edit",
                    "Question",
                    new
                    {
                        questionId = request.QuestionId,
                        subjectId = request.SubjectId
                    }
                    );
            }
            var content = new EditQuestionRequest()
            {
                ModuleId = request.ModuleId,
                QuestionText = request.QuestionText,
                QuestionA = request.QuestionA,
                QuestionB = request.QuestionB,
                QuestionC = request.QuestionC,
                QuestionD = request.QuestionD,
                Answer = Convert.ToChar(request.Answer),
                Difficult = request.Difficult
            };
            var result = await _questionServiceClient.EditQuestion(request.QuestionId, content);
            if (!result.IsSuccessed)
            {
                TempData["Notify"] = result.Message;
                return RedirectToAction(
                    "EditQuestion",
                    "Question",
                    new
                    {
                        subjectId = request.SubjectId,
                        page = 1,
                        pageSize = 5
                    }
                    );
            }
            else
            {
                TempData["Notify"] = "Cập nhật thành công";
                return RedirectToAction(
                    "Index",
                    "Question",
                    new
                    {
                        subjectId = request.SubjectId,
                        page = 1,
                        pageSize = 5
                    });
            }
        }
        public IActionResult Delete()
        {
            return View();
        }
    }
}
