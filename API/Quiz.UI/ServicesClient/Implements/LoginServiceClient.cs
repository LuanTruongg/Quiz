using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Quiz.DTO.BaseResponse;
using Quiz.DTO.UserManagement;
using Quiz.Repository.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text;

namespace Quiz.UI.ServicesClient.Implements
{
    public class LoginServiceClient : ILoginServiceClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginServiceClient(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<AuthenticationResponse> Authenticate(AuthenticationRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.PostAsync("/quiz/identity/login", httpContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<AuthenticationResponse>(responseContent);
            return JsonConvert.DeserializeObject<AuthenticationResponse>(responseContent);
        }

        public async Task<string> GetListRoleFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var oldTokenDecoded = handler.ReadJwtToken(token);
            var listRole = "";
            foreach (var item in oldTokenDecoded.Claims)
            { 
                if (item.Type == "UserRoles")
                {
                    listRole =item.Value.ToString();
                }
            }
            return listRole;
        }

        public async Task<ApiResult<GetProfileResponse>> GetMyProfile(string userId)
        {
            var json = JsonConvert.SerializeObject(userId);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.PostAsync("/quiz/identity/my-profile", httpContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<GetProfileResponse>>(responseContent);
            return JsonConvert.DeserializeObject<ApiErrorResult<GetProfileResponse>>(responseContent);
        }

        public async Task<ApiResult<bool>> Register(RegisterRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.PostAsync("/quiz/identity/register", httpContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(responseContent);
            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(responseContent);
        }

        public async Task<ApiResult<bool>> UpdateMoney(AddMoneyRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.PutAsync("/quiz/identity/update-money", httpContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(responseContent);
            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(responseContent);
        }
    }
}
