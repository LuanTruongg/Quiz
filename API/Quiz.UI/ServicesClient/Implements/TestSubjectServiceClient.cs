using Newtonsoft.Json;
using Quiz.DTO.TestStructureManagement;
using Quiz.DTO.TestSubjectManagement;
using Quiz.Repository.Model;
using System.Net.Http;
using static System.Net.Mime.MediaTypeNames;

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
        public async Task<List<GetListQuestionOfTestResponse>> GetListQuestionOfTest(string testSubjectCode)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.GetAsync($"/test-subject-management?TestSubjectCode={testSubjectCode}");
            var body = await response.Content.ReadAsStringAsync();
            var listQuestion = JsonConvert.DeserializeObject<List<GetListQuestionOfTestResponse>>(body);
            return listQuestion;
        }

        public async Task<string> GetTestSubjectCode(string testStructureId)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.GetAsync($"/common/get-test-subject-code/{testStructureId}");
            var body = await response.Content.ReadAsStringAsync();
            //var testSubjectCode = JsonConvert.DeserializeObject<string>(body);
            return body.ToString();
        }
    }
}
