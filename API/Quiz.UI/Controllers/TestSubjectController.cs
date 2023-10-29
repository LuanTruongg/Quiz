using Microsoft.AspNetCore.Mvc;
using Quiz.DTO.TestSubjectManagement;
using Quiz.UI.ServicesClient;

namespace Quiz.UI.Controllers
{
    public class TestSubjectController : Controller
    {
        private readonly ITestSubjectServiceClient _testSubjectServiceClient;
        public TestSubjectController(ITestSubjectServiceClient testSubjectServiceClient)
        {
            _testSubjectServiceClient = testSubjectServiceClient;
        }
        public async Task<IActionResult> Index(string testStructureId)
        {
            var testSubjectCode = await _testSubjectServiceClient.GetTestSubjectCode(testStructureId);
            ViewBag.ListQuestion = await _testSubjectServiceClient.GetListQuestionOfTest(testSubjectCode);
            return View();
        }
        [HttpPost]
        public IActionResult SubmitTest()
        {
            return View();
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
