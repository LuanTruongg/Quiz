using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Quiz.DTO.BaseResponse;
using Quiz.DTO.Common;
using Quiz.UI.Models;
using Quiz.UI.ServicesClient;
using System.Diagnostics;

namespace Quiz.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeServiceClient _homeServiceClient;

        public HomeController(ILogger<HomeController> logger, IHomeServiceClient homeServiceClient)
        {
            _logger = logger;
            _homeServiceClient = homeServiceClient;
        }

        public async Task<IActionResult> Index()
        {
            var UserRoles = HttpContext.Session.GetString("UserRoles");
            if (!string.IsNullOrEmpty(UserRoles))
            {
                List<string> listRoles = new List<string>();
                string[] listRolesSplit = UserRoles.Split(';');
                foreach (var role in listRolesSplit)
                {
                    listRoles.Add(role);
                }
                ViewBag.ListRoles = listRoles;
            }
            ViewBag.ListDepartment = await _homeServiceClient.GetListDepartments();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> HomeNav()
        {
            var listDepartment = await _homeServiceClient.GetListDepartments();
            return View(listDepartment);
        }
    }
}