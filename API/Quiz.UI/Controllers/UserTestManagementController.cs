using Microsoft.AspNetCore.Mvc;
using Quiz.DTO.UserTestManagement;
using Quiz.UI.ServicesClient;

namespace Quiz.UI.Controllers
{
    public class UserTestManagementController : Controller
    {
        private readonly IUserTestManagementServiceClient _userTestManagementServiceClient;
        public UserTestManagementController(IUserTestManagementServiceClient userTestManagementServiceClient)
        {
            _userTestManagementServiceClient = userTestManagementServiceClient;
        }
        [HttpGet]
        public async Task<IActionResult> ListResultTested(GetListResultUserTestRequest request)
        {
            var result = await _userTestManagementServiceClient.GetListUserTestManagement(request);
            return View(result.ResultObj);
        }
    }
}
