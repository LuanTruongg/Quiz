using Newtonsoft.Json;
using Quiz.DTO.BaseResponse;
using Quiz.DTO.UserManagement;
using Quiz.DTO.UserTestManagement;

namespace Quiz.UI.ServicesClient.Implements
{
    public class UserTestManagementServiceClient : IUserTestManagementServiceClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserTestManagementServiceClient(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiResult<PagedResult<GetUserTestResponse>>> GetListUserTestManagement(GetListResultUserTestRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.GetAsync($"/quiz/user-test-management/get-list-result-user-test-management" +
                $"?Search={request.Search}" +
                $"&TestStructureId={request.TestStructureId}" +
                $"&Page={request.Page}" +
                $"&PageSize={request.PageSize}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<PagedResult<GetUserTestResponse>>>(body);
            return JsonConvert.DeserializeObject<ApiErrorResult<PagedResult<GetUserTestResponse>>>(body);
        }
    }
}
