using Newtonsoft.Json;
using Quiz.DTO.BaseResponse;
using Quiz.DTO.Common;
using Quiz.DTO.SubjectManagement;
using Quiz.Repository.Model;

namespace Quiz.UI.ServicesClient.Implements
{
    public class SubjectServiceClient : ISubjectServiceClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SubjectServiceClient(
            IHomeServiceClient homeServiceClient,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiResult<PagedResult<SubjectItem>>> GetListSubjectPaging(GetListSubjectPagingRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.GetAsync($"/subject-management/get-list-subject-paging?" +
                $"Page={request.Page}&" +
                $"PageSize={request.PageSize}&" +
                $"UserId={request.UserId}");
            var body = await response.Content.ReadAsStringAsync();
            var subject = JsonConvert.DeserializeObject<ApiResult<PagedResult<SubjectItem>>>(body);
            return subject;
        }
    }
}
