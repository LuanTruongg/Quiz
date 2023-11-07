using Microsoft.AspNetCore.Mvc;
using Quiz.DTO.ModuleManagement;
using Quiz.DTO.TestStructureManagement;
using Quiz.DTO.TestSubjectManagement;
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

        public TestStructureController(
            ILogger<HomeController> logger, 
            ITestStructureServiceClient testStructureServiceClient, 
            ITestSubjectServiceClient testSubjectServiceClient,
            ISubjectServiceClient subjectServiceClient)
        {
            _logger = logger;
            _testStructureServiceClient = testStructureServiceClient;
            _testSubjectServiceClient = testSubjectServiceClient;
            _subjectServiceClient = subjectServiceClient;
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
            ViewData["SubjectName"] = await _testStructureServiceClient.GetNameMajor(majorId, departmentId);
            var listSubject = await _testStructureServiceClient.GetListSubject(majorId);
            ViewBag.ListSubject = listSubject;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ListTestOfSubject(string subjectId, string subjectName, string search, int page = 1, int pageSize = 5)
        {
            var request = new GetListTestStructureRequest()
            {
                Page = page,
                PageSize = pageSize,
                Search = search,
                SubjectId = subjectId,
            };
            ViewData["SubjectName"] = subjectName;
            var listTestStructure = await _testStructureServiceClient.GetListTestStructure(request);
            ViewBag.ListTestStructure = listTestStructure;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Create(string subjectId,string subjectName)
        {
            ViewData["SubjectId"] = subjectId;
            ViewData["SubjectName"] = subjectName;
            ViewBag.ListModule = await _subjectServiceClient.GetListModuleOfSubject(subjectId);
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
                Time = request.Time
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
            }
            TempData["Notify"] = "Tạo bài thi thành công";
            return RedirectToAction(
                "ListTestOfSubjectManagement",
                "SubjectManagement",
                new { subjectId = request.SubjectId, 
                    subjectName = request.SubjectName ,
                    page = 1,
                    pageSize = 5 }
                );
        }
    }
}
