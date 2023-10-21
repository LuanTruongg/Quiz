using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Quiz.Infrastructure.Constraint;
using Quiz.Infrastructure.Http;
using ControllerBase = Quiz.Infrastructure.Http.ControllerBase;
using Quiz.UI.ServicesClient;
using Quiz.DTO.UserManagement;

namespace Quiz.UI.Controllers
{
    public class LoginController : ControllerBase
    {
        private readonly ILoginServiceClient _service;

        public LoginController(ILoginServiceClient service)
        {
            _service = service;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginDefault(AuthenticationRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.Authenticate(request);
                if (result != null)
                {
                    return RedirectToAction("Index", "Home");
                };
            }
            return RedirectToAction("Index", "Login");
            //throw new ErrorException(400, ErrorMessage.BadRequest);

        }
        //public async Task Login()
        //{
        //    await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
        //    {
        //        RedirectUri = Url.Action("GoogleResponse")
        //    });
        //}
        //public async Task<IActionResult> GoogleResponse()
        //{
        //    var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        //    var claims = result.Principal.Identities
        //        .FirstOrDefault()
        //        .Claims
        //        .Select(claim => new
        //        {
        //            claim.Issuer,
        //            claim.OriginalIssuer,
        //            claim.Value,
        //            claim.Type
        //        });
        //    return Json(claims);
        //    //return RedirectToAction("Index", "Home");
        //}
        //public async Task<IActionResult> Logout()
        //{
        //    await HttpContext.SignOutAsync();
        //    return RedirectToAction("Home");
        //}
    }
}
