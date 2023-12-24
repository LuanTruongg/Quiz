using Newtonsoft.Json;
using Quiz.DTO.BaseResponse;
using Quiz.DTO.Common;
using Quiz.DTO.ModuleManagement;
using Quiz.DTO.SubjectManagement;
using Quiz.DTO.TestStructureManagement;
using Quiz.DTO.UserManagement;
using Quiz.Repository.Model;
using System.Text;

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
            var response = await client.GetAsync($"/quiz/subject-management/get-list-subject-paging?" +
                $"Page={request.Page}&" +
                $"PageSize={request.PageSize}&" +
                $"Search={request.Search}&" +
                $"UserId={request.UserId}");
            var body = await response.Content.ReadAsStringAsync();
            var subject = JsonConvert.DeserializeObject<ApiResult<PagedResult<SubjectItem>>>(body);
            return subject;
        }
        public async Task<ApiResult<List<GetListModuleResponse>>> GetListModuleOfSubject(string subjectId)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.GetAsync($"/quiz/module-management?" +
                $"SubjectId={subjectId}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<List<GetListModuleResponse>>>(body);
            return JsonConvert.DeserializeObject<ApiErrorResult<List<GetListModuleResponse>>>(body);
        }

        public async Task<ApiResult<SubjectItem>> GetSubjectById(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.GetAsync($"/quiz/subject-management/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<SubjectItem>>(body);
            return JsonConvert.DeserializeObject<ApiErrorResult<SubjectItem>>(body);
        }

        public async Task<List<GetTeacherItem>> GetListTeacher()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.GetAsync($"/quiz/common/get-list-teacher");
            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<GetTeacherItem>>(body);
        }

        public async Task<List<GetTeacherItem>> GetListTeacherOfSubject(string subjectId)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.GetAsync($"/quiz/common/get-list-teacher-of-subject/{subjectId}");
            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<GetTeacherItem>>(body);
        }

        public async Task<ApiResult<bool>> AddTeacherForSubject(AddTeacherForSubjectRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.PostAsync("/quiz/subject-management/add-teacher-for-subject", httpContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(responseContent);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(responseContent);
        }

        public async Task<ApiResult<PagedResult<UserStructureItem>>> GetListUserBoughtTest(GetListUserStructureRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.GetAsync($"/quiz/user-management/get-list-user-bought-test" +
                $"?TestStructureId={request.TestStructureId}" +
                $"&Page={request.Page}" +
                $"&PageSize={request.PageSize}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<PagedResult<UserStructureItem>>>(body);
            return JsonConvert.DeserializeObject<ApiErrorResult<PagedResult<UserStructureItem>>>(body);
        }

        public async Task<ApiResult<bool>> AddSubject(AddSubjectRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.PostAsync("/quiz/subject-management", httpContent);
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(body);
            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(body);
        }

        public async Task<ApiResult<PagedResult<SubjectItem>>> GetListAllSubjectPaging(GetListSubjectPagingRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.GetAsync($"/quiz/subject-management/get-list-subject-paging?" +
                $"Page={request.Page}&" +
                $"PageSize={request.PageSize}&" +
                $"Search={request.Search}&");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<PagedResult<SubjectItem>>>(body);
            return JsonConvert.DeserializeObject<ApiErrorResult<PagedResult<SubjectItem>>>(body);
        }

        public async Task<ApiResult<bool>> DeleteSubject(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.DeleteAsync($"/quiz/subject-management/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(body);
            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(body);
        }

        public async Task<ApiResult<List<int>>> GetListTotalQuestionOfModule(string subjectId)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.GetAsync($"/quiz/module-management/total-question-of-module?SubjectId={subjectId}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<List<int>>>(body);
            return JsonConvert.DeserializeObject<ApiErrorResult<List<int>>>(body);
        }

        public async Task<ApiResult<bool>> CreateModule(CreateModuleRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.PostAsync($"/quiz/module-management", httpContent);
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(body);
            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(body);
        }
    }
}
