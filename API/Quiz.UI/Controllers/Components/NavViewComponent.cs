using Microsoft.AspNetCore.Mvc;
using Quiz.UI.Models;
using Quiz.UI.ServicesClient;

namespace Quiz.UI.Controllers.Components
{
    public class NavViewComponent : ViewComponent
    {
        private readonly IHomeServiceClient _homeServiceClient;

        public NavViewComponent(IHomeServiceClient homeServiceClient)
        {
            _homeServiceClient = homeServiceClient;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listDepartment = await _homeServiceClient.GetListDepartments();
            return View("Default", listDepartment);
        }
    }
}
