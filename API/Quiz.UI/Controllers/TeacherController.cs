using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz.UI.ServicesClient;
using Quiz.UI.ServicesClient.Extensions;

namespace Quiz.UI.Controllers
{
    public class TeacherController : Controller
    {
        private readonly IRolesService _rolesService;
        public TeacherController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }
        public IActionResult Index()
        {
            var checkRoles = _rolesService.CheckTeacher(HttpContext);
            if (checkRoles is true)
            {
                return View();
            }
            return Unauthorized();
        }
    }
}
