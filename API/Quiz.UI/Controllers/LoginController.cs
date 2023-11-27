using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Quiz.Infrastructure.Constraint;
using Quiz.Infrastructure.Http;
using ControllerBase = Quiz.Infrastructure.Http.ControllerBase;
using Quiz.UI.ServicesClient;
using Quiz.DTO.UserManagement;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace Quiz.UI.Controllers
{
    public class LoginController : ControllerBase
    {
        private readonly ILoginServiceClient _service;
        private readonly IConfiguration _configuration;
        public LoginController(ILoginServiceClient service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }
        public IActionResult Index(string message)
        {
            ViewData["Message"] = message;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginDefault(AuthenticationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            var result = await _service.Authenticate(request);
            if (result.Token == null)
            {
                return RedirectToAction("Index", "Login", new { message = result.ErrorMessage });
            }
            var userPrincipal = this.ValidateToken(result.Token);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = true,
            };
            var roles = await _service.GetListRoleFromToken(result.Token);
            HttpContext.Session.SetString("Token", result.Token);
            HttpContext.Session.SetString("UserId", userPrincipal.FindFirst("UserId").Value);
            HttpContext.Session.SetString("FullName", userPrincipal.Identity.Name);
            HttpContext.Session.SetString("UserRoles", roles);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, authProperties);
            return RedirectToAction("Index", "Home");
        }

        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;
            SecurityToken validateToken;

            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;
            validationParameters.ValidAudience = _configuration["JwtTokens:Issuer"];
            validationParameters.ValidIssuer = _configuration["JwtTokens:Issuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtTokens:Key"]));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validateToken);
            return principal;
        }

        public IActionResult Register()
        {
            if(TempData["Notify"] != null)
            {
                ViewData["Message"] = TempData["Notify"];
            }           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterDefault(RegisterRequest request)
        {
            request.UserName = request.Email;
            var result = await _service.Register(request);
            if (result.IsSuccessed)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                TempData["Notify"] = result.Message;
                return RedirectToAction("Register", "Login");
            }
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
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("FullName");
            HttpContext.Session.Remove("UserRoles");
            return RedirectToAction("Index", "Home");
        }
    }
}
