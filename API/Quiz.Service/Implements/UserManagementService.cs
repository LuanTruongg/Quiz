﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Quiz.DTO.BaseResponse;
using Quiz.DTO.UserManagement;
using Quiz.Infrastructure.Http;
using Quiz.Repository;
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
        private readonly QuizDbContext _dbcontext;
        public UserManagementService(UserManager<User> userManager, SignInManager<User> signInManager,
            RoleManager<IdentityRole<string>> roleManager, IConfiguration config, QuizDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _dbcontext = dbContext;
        }
        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return new AuthenticationResponse
                {
                    Token = null,
                    IsAuthSuccessful = false,
                    ErrorMessage = "Tài khoản không tồn tại"
                };
                //throw new TestException("Tài khoản không tồn tại");
            }
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded)
            {
                return new AuthenticationResponse
                {
                    Token = null,
                    IsAuthSuccessful = false,
                    ErrorMessage = "Tài khoản hoặc mật khẩu không đúng"
                };
                //throw new TestException("");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("UserRoles", string.Join(";",roles)),
                new Claim(ClaimTypes.Name, user.Fullname),
                new Claim("UserId", user.Id)
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

        public async Task<ApiResult<GetProfileResponse>> GetMyProfileAsync(string userId)
        {
            var userExisting = await _userManager.FindByIdAsync(userId);
            if (userExisting == null)
            {
                return new ApiErrorResult<GetProfileResponse>("Not Found");
            }
            var result = new GetProfileResponse()
            {
                Fullname = userExisting.Fullname,
                DoB = userExisting.DoB,
                Address = userExisting.Address,
                CCCD = userExisting.CCCD,
                Money = userExisting.Money,
                Sex = userExisting.Sex,
                Email = userExisting.Email,
                PhoneNumber = userExisting.PhoneNumber
            };
            return new ApiSuccessResult<GetProfileResponse>(result);
        }

        public async Task<ApiResult<List<string>>> GetUserStructuresById(string userId)
        {
            var UserStructuresExisting = await _dbcontext.UserStructures
                .Where(x => x.UserId == userId)
                .Select(x => x.TestStructureId)
                .ToListAsync();
            return new ApiSuccessResult<List<string>>(UserStructuresExisting);
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

        public async Task<ApiResult<bool>> UserBuyingTestAsync(UserBuyingTestRequest request)
        {
            var userExisting = await _userManager.FindByIdAsync(request.UserId);
            if (userExisting == null)
            {
                return new ApiErrorResult<bool>("Không tìn thấy người dùng");
            }
            var testStructureExisting = _dbcontext.TestStructures.FirstOrDefault(x => x.TestStructureId == request.TestStructureId);
            if (testStructureExisting == null)
            {
                return new ApiErrorResult<bool>("Không tìn thấy bài thi");
            }
            if(userExisting.Money < testStructureExisting.Price)
            {
                return new ApiErrorResult<bool>("Số dư không đủ");
            }
            var newUserStructure = new UserStructure()
            {
                UserId = request.UserId,
                TestStructureId = request.TestStructureId,
            };
            try
            {
                await _dbcontext.UserStructures.AddAsync(newUserStructure);
                var totalMoney = userExisting.Money - testStructureExisting.Price;
                userExisting.Money = totalMoney;
                await _userManager.UpdateAsync(userExisting);
                await _dbcontext.SaveChangesAsync();
                return new ApiSuccessResult<bool>();
            }
            catch (Exception ex) { 
                return new ApiErrorResult<bool>(ex.Message);
            }
            
        }
    }
}
