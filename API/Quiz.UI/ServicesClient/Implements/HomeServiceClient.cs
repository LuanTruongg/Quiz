using Newtonsoft.Json;
using Quiz.DTO.BaseResponse;
using Quiz.DTO.Common;
using Quiz.DTO.UserManagement;
using System.Net.Http.Headers;
using System.Text;

namespace Quiz.UI.ServicesClient.Implements
{
    public class HomeServiceClient : IHomeServiceClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeServiceClient(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<GetListDepartmentResponse>> GetListDepartments()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.GetAsync("/common/get-list-department");
            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<GetListDepartmentResponse>>(body);
        }

        public async Task<List<GetListMajorResponse>> GetListMajor(string departmentId)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.GetAsync($"/common/get-list-major/{departmentId}");
            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<GetListMajorResponse>>(body);
        }
    }
}
