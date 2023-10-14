using Microsoft.AspNetCore.Mvc;

namespace Quiz.UI.Controllers
{
    public class TestStructureController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
