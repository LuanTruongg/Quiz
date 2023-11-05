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
        public async Task<GetListSubjectPagingResponse> GetListSubjectPaging(PagingRequest request)
        {
            if (request.Page == 0 || request.PageSize == 0)
            {
                request.Page = 1;
                request.PageSize = 5;
            }
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.GetAsync($"/subject-management/get-list-subject-paging?Page={request.Page}&PageSize={request.PageSize}");
            var body = await response.Content.ReadAsStringAsync();
            var subject = JsonConvert.DeserializeObject<GetListSubjectPagingResponse>(body);
            return subject;
        }
    }
}
