using Microsoft.AspNetCore.Mvc;
using Quiz.UI.ServicesClient;

namespace Quiz.UI.Controllers
{
    public class AdminController : Controller
    {
        private readonly IRolesService _rolesService;
        public AdminController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }
        public IActionResult Index()
        {
            var checkRoles = _rolesService.CheckAdmin(HttpContext);
            if (checkRoles is true)
            {
                return View();
            }
            return Unauthorized();
        }
    }
}
