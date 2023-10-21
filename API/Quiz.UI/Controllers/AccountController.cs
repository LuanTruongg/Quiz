using Microsoft.AspNetCore.Mvc;

namespace Quiz.UI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
