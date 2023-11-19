using Microsoft.AspNetCore.Mvc;
using Quiz.DTO.BaseResponse;
using Quiz.DTO.UserManagement;
using Quiz.Repository.Model;
using Quiz.Service;
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
        //public IActionResult Create()
        //{
        //    return View();
        //}
        [HttpGet("/list-user")]
        public async Task<IActionResult> GetListUser(PagingRequest request)
        {
            request.PageSize = 5;
            request.Page = 1;
            if (TempData["Notify"] != null)
            {
                ViewBag.SuccessMsg = TempData["Notify"];
            }
            var listUser = await _userManagementServiceClient.GetListUser(request);
            return View(listUser.ResultObj);

        }
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManagementServiceClient.GetUserById(id);
            if (user.IsSuccessed)
            {
                if (TempData["Notify"] != null)
                {
                    ViewBag.Error = TempData["Notify"];
                }
                ViewBag.User = user.ResultObj;
                return View();
            }
            return RedirectToAction("GetListUser");
        }
        
        public async Task<IActionResult> Edit(EditUserRequest request)
        {
            var user = await _userManagementServiceClient.EditUser(request);
            if (user.IsSuccessed)
            {
                ViewBag.User = user.ResultObj;
                TempData["Notify"] = "Cập nhật thành công";
                return RedirectToAction(
                    "GetListUser",
                    "UserManagement"
                    );
            }
            else
            {
                TempData["Notify"] = user.Message;
                return RedirectToAction(
                    "Details",
                    "UserManagement",
                    new { id = request.UserId }
                    );
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetRoles(string id)
        {
            var listRoles = await _userManagementServiceClient.GetRoles();
            ViewBag.ListRole = listRoles;
            var listRolesUser = await _userManagementServiceClient.GetUserRoles(id);
            ViewBag.ListRoleUser = listRolesUser.ResultObj;
            ViewBag.UserId = id;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RoleAssign(AssignRolesRequest request)
        {
            var assign = await _userManagementServiceClient.AssignRole(request);
            if (assign.IsSuccessed)
            {
                TempData["Notify"] = "Cập nhật thành công";
                return RedirectToAction(
                    "GetListUser",
                    "UserManagement"
                    );
            }
            else
            {
                TempData["Notify"] = assign.Message;
                return RedirectToAction(
                    "Details",
                    "UserManagement",
                    new { id = request.UserId }
                    );
            }
        }
    }
}
