using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> ListTest(string id)
        {
            ViewData["DepartmentName"] = await _testStructureServiceClient.GetNameDepartment(id);
            return View();
        }
    }
}
