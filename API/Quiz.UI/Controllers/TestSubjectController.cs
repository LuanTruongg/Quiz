using Microsoft.AspNetCore.Mvc;

namespace Quiz.UI.Controllers
{
    public class TestSubjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
