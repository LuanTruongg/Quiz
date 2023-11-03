using Microsoft.AspNetCore.Mvc;
using Quiz.UI.Models;
using Quiz.UI.ServicesClient;
using System.Data;

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
            var UserRoles = HttpContext.Session.GetString("UserRoles");
            if (!string.IsNullOrEmpty(UserRoles))
            {
                List<string> listRoles = new List<string>();
                string[] listRolesSplit = UserRoles.Split(';');
                foreach (var role in listRolesSplit) {
                    listRoles.Add(role);
                }
                ViewBag.ListRoles = listRoles;
            }
            
            return View("Default", listDepartment);
        }
    }
}
