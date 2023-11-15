using Microsoft.AspNetCore.Mvc;
using Quiz.DTO.UserManagement;
using Quiz.Repository.Model;
using Quiz.UI.ServicesClient;

namespace Quiz.UI.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly IUserManagementServiceClient _userManagementServiceClient;
        private readonly ILoginServiceClient _loginServiceClient;
        private readonly ITestStructureServiceClient _testStructureServiceClient;
        public UserManagementController(
            IUserManagementServiceClient userManagementServiceClient
            , ILoginServiceClient loginServiceClient
            , ITestStructureServiceClient testStructureServiceClient)
        {
            _userManagementServiceClient = userManagementServiceClient;
            _loginServiceClient = loginServiceClient;
            _testStructureServiceClient = testStructureServiceClient;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> BuyConfirm(string testStructureId, string userId)
        {
            var model = new UserBuyingTestRequest()
            {
                UserId = userId,
                TestStructureId = testStructureId
            };
            var user = await _loginServiceClient.GetMyProfile(userId);
            ViewBag.User = user.ResultObj;
            var testStructure = await _testStructureServiceClient.GetTestStructureById(testStructureId);
            ViewBag.TestStructure = testStructure.ResultObj;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Buy(UserBuyingTestRequest request)
        {
            var result = await _userManagementServiceClient.UserBuyingTest(request);
            if (result.IsSuccessed)
            {
                var testStructure = await _testStructureServiceClient.GetTestStructureById(request.TestStructureId);
                return RedirectToAction(
                    "ListTestOfSubject",
                    "TestStructure",
                     new
                     {
                         subjectId = testStructure.ResultObj.SubjectId,
                     });
            }
            return RedirectToAction(
                    "BuyConfirm",
                    "UserManagement",
                     new
                     {
                         testStructureId = request.TestStructureId,
                         userId = request.UserId
                     });
        }
        [HttpGet("/my-profile/{userId}/test-result")]
        public async Task<IActionResult> GetListTest(string userId)
        {
            var listResultUserTest = await _userManagementServiceClient.GetListUserResultById(userId);
            return View(listResultUserTest.ResultObj);
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
        public IActionResult GetProfile()
        {
            return View();
        }
    }
}
