using Newtonsoft.Json;
using Quiz.DTO.BaseResponse;
using Quiz.DTO.QuestionManagement;
using Quiz.DTO.SubjectManagement;
using Quiz.DTO.TestStructureManagement;
using System.Net.Http;
using System.Text;

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

        public async Task<ApiResult<bool>> CreateQuestionOfModule(CreateQuestionRequestViewModel request)
        {
            var content = new AddQuestionRequest()
            {
                ModuleId = request.ModuleId,
                QuestionText = request.QuestionText,
                QuestionA = request.QuestionA,
                QuestionB = request.QuestionB,
                QuestionC = request.QuestionC,
                QuestionD = request.QuestionD,
                Answer = request.Answer,
                Difficult = request.Difficult
            };
            var json = JsonConvert.SerializeObject(content);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.PostAsync("/question-management", httpContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(responseContent);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(responseContent);
        }

        public async Task<ApiResult<bool>> DeleteQuestion(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.DeleteAsync($"question-management/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(body);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(body);
        }

        public async Task<ApiResult<bool>> EditQuestion(string id, EditQuestionRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.PutAsync($"/question-management/{id}", httpContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(responseContent);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(responseContent);
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

        public async Task<ApiResult<GetQuestionResponse>> GetQuestionById(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.GetAsync($"/question-management/{id}");
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<GetQuestionResponse>>(responseContent);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<GetQuestionResponse>>(responseContent);
        }
    }
}
