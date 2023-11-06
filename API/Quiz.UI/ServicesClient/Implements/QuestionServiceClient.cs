using Newtonsoft.Json;
using Quiz.DTO.BaseResponse;
using Quiz.DTO.QuestionManagement;
using Quiz.DTO.SubjectManagement;

namespace Quiz.UI.ServicesClient.Implements
{
    public class QuestionServiceClient : IQuestionServiceClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public QuestionServiceClient(
            IHomeServiceClient homeServiceClient,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiResult<PagedResult<QuestionItem>>> GetListQuestionOfSubject(GetListQuestionRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.GetAsync($"question-management" +
                $"?SubjectId={request.SubjectId}" +
                $"&Page={request.Page}" +
                $"&PageSize={request.PageSize}");
            var body = await response.Content.ReadAsStringAsync();
            var listQuestion = JsonConvert.DeserializeObject<ApiResult<PagedResult<QuestionItem>>>(body);
            return listQuestion;
        }
    }
}
