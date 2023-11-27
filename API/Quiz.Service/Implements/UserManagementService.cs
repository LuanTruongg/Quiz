using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Quiz.DTO.BaseResponse;
using Quiz.DTO.TestStructureManagement;
using Quiz.DTO.UserManagement;
using Quiz.Infrastructure.Http;
using Quiz.Repository;
using Quiz.Repository.Model;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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

        public async Task<ApiResult<bool>> UpdateMoneyAsync(AddMoneyRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return new ApiErrorResult<bool>("Tài khoản không tồn tại");
            }
            user.Money = user.Money + request.Money;
            try
            {
                _dbcontext.Users.Update(user);
                _dbcontext.SaveChanges();
                return new ApiSuccessResult<bool>();
            }catch (Exception ex)
            {
                return new ApiErrorResult<bool>(ex.Message);
            }
        }

        public async Task<ApiResult<bool>> AssignRolesAsync(string userId, RoleAssignRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("Tài khoản không tồn tại");
            }
            var removedRoles = request.Roles.Where(x => x.Selected == false).Select(x => x.Name).ToList();
            foreach (var roleName in removedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == true)
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
            }
            await _userManager.RemoveFromRolesAsync(user, removedRoles);

            var addedRoles = request.Roles.Where(x => x.Selected).Select(x => x.Name).ToList();
            foreach (var roleName in addedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == false)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }
            return new ApiSuccessResult<bool>();
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

        public async Task<ApiResult<bool>> EditUserAsync(EditUserRequest request, string userId)
        {
            var userExisting = await _userManager.FindByIdAsync(userId);
            if (userExisting is null)
            {
                return new ApiErrorResult<bool>("Người dùng không tồn tại");
            }
            userExisting.Address = request.Address;
            userExisting.CCCD = request.CCCD;
            userExisting.PhoneNumber = request.PhoneNumber;
            userExisting.DoB = request.DoB;
            userExisting.Email = request.Email;
            userExisting.Sex = request.Sex;
            userExisting.Fullname = request.Fullname;
            var result = await _userManager.UpdateAsync(userExisting);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Có lỗi trong quá trình xử lý");
        }

        public async Task<ApiResult<PagedResult<UserItem>>> GetListUserAsync(PagingRequest request)
        {
            var usersExisting = _dbcontext.Users.AsQueryable();
            if (request.Search != null)
            {
                usersExisting = usersExisting.Where(x => x.Email.Contains(request.Search));
            }

            int totalRow = usersExisting.Count();

            var data = await usersExisting.Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new UserItem()
                {
                    UserId = x.Id,
                    Fullname = x.Email,
                    CCCD = x.CCCD,
                    Email = x.Email,
                    DoB = x.DoB,
                    Money = x.Money,
                    Sex = x.Sex,
                    Address = x.Address,
                    PhoneNumber = x.PhoneNumber
                }).ToListAsync();

            var numberPage = request.Page <= 0 ? 1 : request.Page;
            var numberPageSize = request.PageSize <= 0 ? 10 : request.PageSize;

            var result = new PagedResult<UserItem>()
            {
                TotalRecords = totalRow,
                Page = numberPage,
                PageSize = numberPageSize,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<UserItem>>(result);
        }

        public async Task<ApiResult<PagedResult<UserStructureItem>>> GetListUserBoughtTestAsync(GetListUserStructureRequest request)
        {
            var testStructureExisting = _dbcontext.TestStructures.FirstOrDefault(x => x.TestStructureId == request.TestStructureId);
            if (testStructureExisting == null)
            {
                return new ApiErrorResult<PagedResult<UserStructureItem>>("Không tồn tại bài thi này");
            }
            var usersStructureExisting = _dbcontext.UserStructures.AsQueryable().Where(x=>x.TestStructureId == request.TestStructureId);
            if (request.Search != null)
            {
                usersStructureExisting = usersStructureExisting.Where(x => x.Email.Contains(request.Search));
            }

            int totalRow = usersStructureExisting.Count();

            var data = await usersStructureExisting
                .Join(_dbcontext.Users,
                us => us.UserId,
                u => u.Id,
                (us, u) => new UserStructureItem()
                {
                    UserId = us.UserId,
                    Email = u.Email,
                    FullName = u.Fullname,
                    PurchaseDate = us.PurchaseDate,
                })
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var numberPage = request.Page <= 0 ? 1 : request.Page;
            var numberPageSize = request.PageSize <= 0 ? 10 : request.PageSize;

            var result = new PagedResult<UserStructureItem>()
            {
                TotalRecords = totalRow,
                Page = numberPage,
                PageSize = numberPageSize,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<UserStructureItem>>(result);
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

        public async Task<ApiResult<IList<string>>> GetRolesUserAsync(string userId)
        {
            var userExisting = await _userManager.FindByIdAsync(userId);
            if (userExisting is null)
            {
                return new ApiErrorResult<IList<string>>("Người dùng không tồn tại");
            }
            var roles = await _userManager.GetRolesAsync(userExisting);

            return new ApiSuccessResult<IList<string>>(roles);
        }

        public async Task<ApiResult<UserItem>> GetUserByIdAsync(string userId)
        {
            var userExisting = await _userManager.FindByIdAsync(userId);
            if(userExisting is null)
            {
                return new ApiErrorResult<UserItem>("Người dùng không tồn tại");
            }
            var getRoles = new List<string>(await _userManager.GetRolesAsync(userExisting));
            var result = new UserItem()
            {
                UserId = userExisting.Id,
                Address = userExisting.Address,
                CCCD = userExisting.CCCD,
                DoB = userExisting.DoB,
                Email = userExisting.Email,
                Fullname = userExisting.Fullname,
                Money = userExisting.Money,
                Sex = userExisting.Sex,
                Roles = getRoles,
                PhoneNumber = userExisting.PhoneNumber,
            };
            return new ApiSuccessResult<UserItem>(result);
        }

        public async Task<ApiResult<List<string>>> GetUserStructuresById(string userId)
        {
            var UserStructuresExisting = await _dbcontext.UserStructures
                .Where(x => x.UserId == userId)
                .Select(x => x.TestStructureId)
                .ToListAsync();
            return new ApiSuccessResult<List<string>>(UserStructuresExisting);
        }

        public async Task<ApiResult<bool>> RegisterAsync(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                return new ApiErrorResult<bool>("Tài khoản đã tồn tại");
            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ApiErrorResult<bool>("Email đã được sử dụng");
            }
            if (request.Password != request.ConfirmPassword)
            {
                return new ApiErrorResult<bool>("Mật khẩu không trùng khớp");
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
                Sex = request.Sex,
                Address = request.Address,
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Đăng ký không thành công");
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
                PurchaseDate = DateTime.Now,
                Email = userExisting.Email
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
