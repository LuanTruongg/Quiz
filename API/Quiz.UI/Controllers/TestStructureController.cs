using Microsoft.AspNetCore.Mvc;
using Quiz.Repository.Model;
using Quiz.UI.ServicesClient;
using Quiz.UI.ServicesClient.Implements;

namespace Quiz.UI.Controllers
{
    public class TestStructureController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITestStructureServiceClient _testStructureServiceClient;
        private readonly IHomeServiceClient _homeServiceClient;
        public TestStructureController(
            ILogger<HomeController> logger, 
            ITestStructureServiceClient testStructureServiceClient, 
            IHomeServiceClient homeServiceClient)
        {
            _logger = logger;
            _testStructureServiceClient = testStructureServiceClient;
            _homeServiceClient = homeServiceClient;
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
        public async Task<IActionResult> ListTestOfSubject(string subjectId, string subjectName)
        {
            ViewData["SubjectName"] = subjectName;
            var listTestStructure = await _testStructureServiceClient.GetListTestStructure(subjectId);
            ViewBag.ListTestStructure = listTestStructure;
            return View();
        }
    }
}
