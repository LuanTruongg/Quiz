using Microsoft.AspNetCore.Mvc;
using Quiz.UI.ServicesClient;

namespace Quiz.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILoginServiceClient _service;
        private readonly IConfiguration _configuration;
        public AccountController(ILoginServiceClient service, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var profile = await _service.GetMyProfile(userId);
            ViewBag.Profile = profile.ResultObj;
            return View();
        }
    }
}
