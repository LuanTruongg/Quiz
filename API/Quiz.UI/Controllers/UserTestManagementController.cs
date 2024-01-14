using Microsoft.AspNetCore.Mvc;
using Quiz.DTO.UserTestManagement;
using Quiz.Repository.Model;
using Quiz.UI.ServicesClient;
using Quiz.UI.ServicesClient.Implements;

namespace Quiz.UI.Controllers
{
    public class UserTestManagementController : Controller
    {
        private readonly IUserTestManagementServiceClient _userTestManagementServiceClient;
        private readonly ISubjectServiceClient _subjectServiceClient;
        public UserTestManagementController(IUserTestManagementServiceClient userTestManagementServiceClient, ISubjectServiceClient subjectServiceClient)
        {
            _userTestManagementServiceClient = userTestManagementServiceClient;
            _subjectServiceClient = subjectServiceClient;

        }
        [HttpGet]
        public async Task<IActionResult> ListResultTested(GetListResultUserTestRequest request, DateTime dateSearch)
        {
            var subject = await _subjectServiceClient.GetSubjectById(request.SubjectId);
            ViewData["TestStructureId"] = request.TestStructureId;
            ViewData["SubjectId"] = subject.ResultObj.SubjectId;
            ViewData["SubjectName"] = subject.ResultObj.Name;
            if(request.Page == 0 || request.PageSize == 0)
            {
                request.Page = 1;
                request.PageSize = 5;
            }
            if (dateSearch != null)
            {
                request.DateSubmit = dateSearch.ToString("dd/MM/yyyy");
            }
            var result = await _userTestManagementServiceClient.GetListUserTestManagement(request);
            return View(result.ResultObj);
        }
    }
}
