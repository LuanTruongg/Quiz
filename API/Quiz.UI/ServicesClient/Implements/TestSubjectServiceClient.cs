using Newtonsoft.Json;
using Quiz.DTO.BaseResponse;
using Quiz.DTO.ModuleManagement;
using Quiz.DTO.TestSubjectManagement;
using Quiz.DTO.UserAnswerManagement;
using Quiz.DTO.UserManagement;
using Quiz.DTO.UserTestManagement;
using Quiz.Repository.Model;
using System.Text;

namespace Quiz.UI.ServicesClient.Implements
{
    public class TestSubjectServiceClient : ITestSubjectServiceClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TestSubjectServiceClient(
            IHomeServiceClient homeServiceClient,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResult<CreateTestSubjectResponse>> CreateTestSubject(CreateTestSubjectRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/test-subject-management", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<CreateTestSubjectResponse>>(result);
            return JsonConvert.DeserializeObject<ApiErrorResult<CreateTestSubjectResponse>>(result);
        }

        public async Task<ApiResult<bool>> DeleteTestSubject(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.GetAsync($"/test-structure/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(body);
            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(body);
        }

        public async Task<List<GetListQuestionOfTestResponse>> GetListQuestionOfTest(string testSubjectCode)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.GetAsync($"/test-subject-management?TestSubjectCode={testSubjectCode}");
            var body = await response.Content.ReadAsStringAsync();
            var listQuestion = JsonConvert.DeserializeObject<List<GetListQuestionOfTestResponse>>(body);
            return listQuestion;
        }

        public async Task<GetResultUserTestResponse> GetResultUserTest(string userTestId)
        {
            var content = new GetResultUserTestRequest()
            {
                UserTestId = userTestId
            };

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);

            var json = JsonConvert.SerializeObject(content);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/user-test-management/get-result-user-test", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GetResultUserTestResponse>(result);
        }

        public async Task<string> GetTestSubjectCode(string testStructureId)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.GetAsync($"/common/get-test-subject-code/{testStructureId}");
            var body = await response.Content.ReadAsStringAsync();
            return body.ToString();
        }
        public async Task<AddUserAnswerResponse> SaveUserAnswer(List<UserAnswerRequest> request, string userTestId)
        {
            var content = new List<AddUserAnswerRequest>();
            foreach(var item in request)
            {
                var userAnswer = new AddUserAnswerRequest()
                {
                    QuestionId = item.QuestionId,
                    UserAnswerQuestion = item.UserAnswerQuestion,
                    UserTestId = userTestId
                };
                content.Add(userAnswer);
            }

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);

            var json = JsonConvert.SerializeObject(content);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/user-answer-management", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AddUserAnswerResponse>(result);
        }

        public async Task<AddUserTestResponse> SaveUserTest(string testStructureId)
        {
            var userId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            var content = new AddUserTestRequest()
            {
                UserId = userId,
                UserTestId = Guid.NewGuid().ToString(),
                TestStructureId = testStructureId,
                CorrectAnswers = 0,
                Score = 0
            };

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);

            var json = JsonConvert.SerializeObject(content);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/user-test-management", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<AddUserTestResponse>(result);
            return JsonConvert.DeserializeObject<AddUserTestResponse>(result);
        }

    }
}
