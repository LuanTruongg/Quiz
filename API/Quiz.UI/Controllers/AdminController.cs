using Microsoft.AspNetCore.Mvc;

namespace Quiz.UI.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
