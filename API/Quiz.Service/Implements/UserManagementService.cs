using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Quiz.DTO.UserManagement;
using Quiz.Infrastructure.Http;
using Quiz.Repository.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Quiz.Service.Implements
{
    public class UserManagementService : IUserManagementService
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole<string>> _roleManager;
        private readonly IConfiguration _config;

        public UserManagementService(UserManager<User> userManager, SignInManager<User> signInManager,
            RoleManager<IdentityRole<string>> roleManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
        }
        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                throw new TestException("Tài khoản không tồn tại");
            }
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded)
            {
                throw new TestException("Đăng nhập không đúng");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, request.UserName)
            };
            //Ma Hoa
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtTokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var configtoken = new JwtSecurityToken(_config["JwtTokens:Issuer"],
                _config["JwtTokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds
                );
            var token = new JwtSecurityTokenHandler().WriteToken(configtoken);
            return new AuthenticationResponse
            {
                Token = token,
                IsAuthSuccessful = true,
                IsFirstLogin = string.IsNullOrEmpty(user.PhoneNumber)
            };
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                throw new TestException("Tai khoan da ton tai");
            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                throw new TestException("Email da ton tai");
            }
            user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                DoB = request.DoB,
                Email = request.Email,
                CCCD = request.CCCD,
                Fullname = request.FullName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return new RegisterResponse()
                {
                    Success = true,
                    Message = "Đăng ký thành công"
                };
            }
            return new RegisterResponse()
            {
                Success = true,
                Message = "Đăng ký không thành công"
            };
        }
    }
}
