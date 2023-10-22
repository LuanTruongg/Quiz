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

namespace Quiz.UI.Controllers
{
    public class LoginController : ControllerBase
    {
        private readonly ILoginServiceClient _service;
        private readonly IConfiguration _configuration;
        public LoginController(ILoginServiceClient service, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
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
                ModelState.AddModelError("", result.ErrorMessage);
                return RedirectToAction("Index", "Login");
            }
            var userPrincipal = this.ValidateToken(result.Token);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = true,
            };
            HttpContext.Session.SetString("Token", result.Token);
            HttpContext.Session.SetString("UserId", userPrincipal.FindFirst("UserId").Value);
            HttpContext.Session.SetString("FullName", userPrincipal.Identity.Name);
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
