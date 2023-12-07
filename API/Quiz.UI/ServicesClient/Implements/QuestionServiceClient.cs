using Newtonsoft.Json;
using Quiz.DTO.BaseResponse;
using Quiz.DTO.QuestionManagement;
using System.Web;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace Quiz.UI.ServicesClient.Implements
{
    public class QuestionServiceClient : IQuestionServiceClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public QuestionServiceClient(
            IHomeServiceClient homeServiceClient,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment webHostEnvironment)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ApiResult<bool>> CreateQuestionOfModule(CreateQuestionRequestViewModel request, string imgName, string audioName)
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
                Difficult = request.Difficult,
                Image = imgName,
                Audio = audioName
            };

            var json = JsonConvert.SerializeObject(content);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.PostAsync("/quiz/question-management", httpContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(responseContent);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(responseContent);
        }
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }

        public async Task<ApiResult<bool>> DeleteQuestion(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.DeleteAsync($"/quiz/question-management/{id}");
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
            var response = await client.PutAsync($"/quiz/question-management/{id}", httpContent);
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
            var response = await client.GetAsync($"/quiz/question-management" +
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
            var response = await client.GetAsync($"/quiz/question-management/{id}");
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<GetQuestionResponse>>(responseContent);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<GetQuestionResponse>>(responseContent);
        }
    }
}
