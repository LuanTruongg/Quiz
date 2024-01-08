using Microsoft.AspNetCore.Mvc;
using Quiz.DTO.ModuleManagement;
using Quiz.DTO.TestStructureManagement;
using Quiz.DTO.TestSubjectManagement;
using Quiz.DTO.QuestionManagement;
using Quiz.Repository.Model;
using Quiz.UI.ServicesClient;
using Quiz.UI.ServicesClient.Implements;

namespace Quiz.UI.Controllers
{
    public class TestStructureController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITestStructureServiceClient _testStructureServiceClient;
        private readonly ISubjectServiceClient _subjectServiceClient;
        private readonly ITestSubjectServiceClient _testSubjectServiceClient;
        private readonly IUserManagementServiceClient _userManagementServiceClient;
        private readonly IQuestionServiceClient _questionServiceClient;

        public TestStructureController(
            ILogger<HomeController> logger, 
            ITestStructureServiceClient testStructureServiceClient, 
            ITestSubjectServiceClient testSubjectServiceClient,
            ISubjectServiceClient subjectServiceClient,
            IUserManagementServiceClient userManagementServiceClient,
            IQuestionServiceClient questionServiceClient)
        {
            _logger = logger;
            _testStructureServiceClient = testStructureServiceClient;
            _testSubjectServiceClient = testSubjectServiceClient;
            _subjectServiceClient = subjectServiceClient;
            _userManagementServiceClient = userManagementServiceClient;
            _questionServiceClient = questionServiceClient;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ListMajor(string id)
        {
            ViewBag.Department = await _testStructureServiceClient.GetNameDepartment(id);
            var listMajor = await _testStructureServiceClient.GetListMajors(id);
            ViewBag.ListMajor = listMajor;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ListSubject(string majorId, string departmentId)
        {
            var major = await _testStructureServiceClient.GetMajor(majorId);
            ViewData["SubjectName"] = major.ResultObj.Name;
            var listSubject = await _testStructureServiceClient.GetListSubject(majorId);
            ViewBag.ListSubject = listSubject;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ListTestOfSubject(string subjectId, string search, int page = 1, int pageSize = 5)
        {
            var request = new GetListTestStructureRequest()
            {
                Page = page,
                PageSize = pageSize,
                Search = search,
                SubjectId = subjectId,
            };
            var subject = await _subjectServiceClient.GetSubjectById(subjectId);
            ViewData["SubjectId"] = subject.ResultObj.SubjectId.ToString();
            ViewData["SubjectName"] = subject.ResultObj.Name;
            var userId = HttpContext.Session.GetString("UserId");
            if (!string.IsNullOrEmpty(userId))
            {
                var userStructures = await _userManagementServiceClient.GetListUserStructuresById(userId);
                ViewBag.UserStructures = userStructures.ResultObj;
            }
            var listTestStructure = await _testStructureServiceClient.GetListTestStructure(request);
            ViewBag.ListTestStructure = listTestStructure;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Create(string subjectId)
        {
            var subject = await _subjectServiceClient.GetSubjectById(subjectId);
            ViewData["SubjectId"] = subject.ResultObj.SubjectId;
            ViewData["SubjectName"] = subject.ResultObj.Name;

            var listModule = await _subjectServiceClient.GetListModuleOfSubject(subjectId);
            ViewBag.ListModule = listModule.ResultObj;
            var totalQuestion = await _subjectServiceClient.GetListTotalQuestionOfModule(subjectId);
            ViewBag.TotalQuestion = totalQuestion.ResultObj;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreataStructureAndTestRequest request)
        {
            var requestStructure = new CreateTestStructureRequest()
            {
                Name = request.Name,
                NumberOfQuestion = request.NumberOfQuestion,
                SubjectId = request.SubjectId,
                Time = request.Time,
                IsFree = request.IsFree,
                Price = request.IsFree == true ? 0 : request.Price
            };
            var testStructureIdCreated = await _testStructureServiceClient.CreateTestStructure(requestStructure);
            var requestTestSubject = new CreateTestSubjectRequest()
            {
                ListModuleId = request.ListModuleId,
                ListNumQuestion = request.ListNumQuestion,
                TestStructureId = testStructureIdCreated.TestStructureId,
                TestSubjectCode = testStructureIdCreated.TestStructureId
            };
            var result = await _testSubjectServiceClient.CreateTestSubject(requestTestSubject);
            if (!result.IsSuccessed)
            {
                await _testSubjectServiceClient.DeleteTestSubject(testStructureIdCreated.TestStructureId);
                await _testSubjectServiceClient.DeleteTestStructure(testStructureIdCreated.TestStructureId);
                TempData["Notify"] = "Tạo bài thi không thành công";
                return RedirectToAction(
                    "ListTestOfSubjectManagement",
                    "SubjectManagement",
                    new
                    {
                        subjectId = request.SubjectId,
                        page = 1,
                        pageSize = 5
                    }
                    );
            }
            TempData["Notify"] = "Tạo bài thi thành công";
            return RedirectToAction(
                "ListTestOfSubjectManagement",
                "SubjectManagement",
                new { subjectId = request.SubjectId, 
                    page = 1,
                    pageSize = 5 }
                );
        }

        [HttpGet]
        public async Task<IActionResult> CreateSpeaking(string subjectId)
        {
            var subject = await _subjectServiceClient.GetSubjectById(subjectId);
            ViewData["SubjectId"] = subject.ResultObj.SubjectId;
            ViewData["SubjectName"] = subject.ResultObj.Name;

            var listModule = await _subjectServiceClient.GetListModuleOfSubject(subjectId);
            ViewBag.ListModule = listModule.ResultObj;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateSpeakingTest(string htmlEditor , CreateSpeakingTestRequest request)
        {
            var requestStructure = new CreateTestStructureRequest()
            {
                Name = request.Name,
                NumberOfQuestion = 1,
                SubjectId = request.SubjectId,
                Time = 0,
                IsFree = request.IsFree,
                Price = request.IsFree == true ? 0 : request.Price
            };
            var testStructureIdCreated = await _testStructureServiceClient.CreateTestStructure(requestStructure);

            //Create Quesiton
            var newQuestion = new CreateQuestionRequestViewModel()
            {
                ModuleId = request.ModuleId,
                QuestionText = request.htmlEditor,
                Difficult = "Easy",
                Answer = 'A',
                QuestionCustom = "Speaking",
                QuestionA = "none",
                QuestionB = "none",
                QuestionC = "none",
                QuestionD = "none",
                
            };
            var createQuestion = await _questionServiceClient.CreateQuestionOfModuleReturn(newQuestion, "", "");

            //Create Test
            var requestTestSubject = new CreateTestSubjectSpeakingRequest()
            {
                TestStructureId = testStructureIdCreated.TestStructureId,
                TestSubjectCode = testStructureIdCreated.TestStructureId,
                QuestionId = createQuestion.ResultObj.ToString(),
                ModuleId = request.ModuleId
            };
            var result = await _testSubjectServiceClient.CreateSpeakingTestSubject(requestTestSubject);
            return Ok();
        }
    }
}
