using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Quiz.DTO.QuestionManagement;
using Quiz.Repository.Model;
using Quiz.UI.ServicesClient;
using Quiz.UI.ServicesClient.Implements;
using System.Collections;
using System.Text;

namespace Quiz.UI.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionServiceClient _questionServiceClient;
        private readonly ISubjectServiceClient _subjectServiceClient;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRolesService _rolesService;
        public QuestionController(
            IQuestionServiceClient questionServiceClient, 
            ISubjectServiceClient subjectServiceClient, 
            IWebHostEnvironment webHostEnvironment, 
            IRolesService rolesService)
        {
            _questionServiceClient = questionServiceClient;
            _subjectServiceClient = subjectServiceClient;
            _webHostEnvironment = webHostEnvironment;
            _rolesService = rolesService;
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
            var checkRoles = _rolesService.CheckTeacher(HttpContext);
            if (checkRoles is true)
            {
                var subject = await _subjectServiceClient.GetSubjectById(subjectId);
                ViewData["SubjectId"] = subject.ResultObj.SubjectId;
                ViewData["SubjectName"] = subject.ResultObj.Name;
                var listModule = await _subjectServiceClient.GetListModuleOfSubject(subjectId);
                ViewBag.ListModule = listModule.ResultObj;
                return View();
            }
            return Unauthorized();

        }
        [HttpPost]
        public async Task<IActionResult> CreateQuestion(CreateQuestionRequestViewModel request)
        {
            var checkRoles = _rolesService.CheckTeacher(HttpContext);
            if (checkRoles is true)
            {
                IFormFile file;
                string uniqueFileNameImg = "";
                string uniqueFileNameAudio = "";
                try
                {
                    if (request.Image != null)
                    {
                        uniqueFileNameImg = GetUniqueFileName(request.Image.FileName);
                        var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploaded");
                        var filePath = Path.Combine(uploads, uniqueFileNameImg);
                        request.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                    if (request.Audio != null)
                    {
                        uniqueFileNameAudio = GetUniqueFileName(request.Audio.FileName);
                        var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploaded");
                        var filePath = Path.Combine(uploads, uniqueFileNameAudio);
                        request.Audio.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                var result = await _questionServiceClient.CreateQuestionOfModule(request, uniqueFileNameImg, uniqueFileNameAudio);
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
                        });
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
            return Unauthorized();

        }
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string questionId, string subjectId)
        {
            var checkRoles = _rolesService.CheckTeacher(HttpContext);
            if (checkRoles is true)
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
            return Unauthorized();

        }
        [HttpPost]
        public async Task<IActionResult> EditQuestion(GetQuestionResponse request)
        {
            var checkRoles = _rolesService.CheckTeacher(HttpContext);
            if (checkRoles is true)
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
            return Unauthorized();

        }
        public async Task<IActionResult> Delete(string id,string subjectId)
        {
            var checkRoles = _rolesService.CheckTeacher(HttpContext);
            if (checkRoles is true)
            {
                var result = await _questionServiceClient.DeleteQuestion(id);
                if (!result.IsSuccessed)
                {
                    TempData["Notify"] = result.Message;
                    return RedirectToAction(
                        "Index",
                        "Question",
                        new
                        {
                            subjectId = subjectId,
                            page = 1,
                            pageSize = 5
                        }
                        );
                }
                else
                {
                    TempData["Notify"] = "Xoá thành công";
                    return RedirectToAction(
                        "Index",
                        "Question",
                        new
                        {
                            subjectId = subjectId,
                            page = 1,
                            pageSize = 5
                        });
                }
            }
            return Unauthorized();
        }
    }
}
