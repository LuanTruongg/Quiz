using Newtonsoft.Json;
using Quiz.DTO.BaseResponse;
using Quiz.DTO.TestStructureManagement;
using Quiz.DTO.TestSubjectManagement;
using Quiz.DTO.UserManagement;
using System.Net.Http;
using System.Text;

namespace Quiz.UI.ServicesClient.Implements
{
    public class UserManagementServiceClient : IUserManagementServiceClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserManagementServiceClient(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResult<List<string>>> GetListUserStructuresById(string userId)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.GetAsync($"/user-management/" +
                $"{userId}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<List<string>>>(body);
            return JsonConvert.DeserializeObject<ApiErrorResult<List<string>>>(body);
        }

        public async Task<ApiResult<bool>> UserBuyingTest(UserBuyingTestRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/user-management", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);
            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }
    }
}
