using Newtonsoft.Json;
using Quiz.DTO.BaseResponse;
using Quiz.DTO.Common;
using Quiz.DTO.TestStructureManagement;
using Quiz.Repository.Model;
using Quiz.UI.Controllers;

namespace Quiz.UI.ServicesClient.Implements
{
    public class TestStructureServiceClient : ITestStructureServiceClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHomeServiceClient _homeServiceClient;
        public TestStructureServiceClient(
            IHomeServiceClient homeServiceClient,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _homeServiceClient = homeServiceClient;
        }

        public async Task<GetListDepartmentResponse> GetNameDepartment(string id)
        {
            var listDepartment = await _homeServiceClient.GetListDepartments();
            var nameDepartmentCurrent = listDepartment
                .Where(x => x.DepartmentId == id)
                .Select(x => new GetListDepartmentResponse()
                {
                    DepartmentId = x.DepartmentId,
                    Name = x.Name,
                }).FirstOrDefault();
            return nameDepartmentCurrent;
        }

        public async Task<List<GetListMajorResponse>> GetListMajors(string deparmentId)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.GetAsync($"/common/get-list-major/{deparmentId}");
            var body = await response.Content.ReadAsStringAsync();
            var major =  JsonConvert.DeserializeObject<List<Major>>(body);

            var result = major
                    .Select(x => new GetListMajorResponse()
                    {
                        MajorId = x.MajorId,
                        Name = x.Name
                    }).ToList();
            return result;
        }

        public async Task<string> GetNameMajor(string majorId, string departmentId)
        {
            var listMajor = await _homeServiceClient.GetListMajor(departmentId);
            var nameMajorCurrent = listMajor.Where(x => x.MajorId == majorId).FirstOrDefault().Name.ToString();
            return nameMajorCurrent;
        }

        public async Task<List<GetListSubjectResponse>> GetListSubject(string majorId)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.GetAsync($"/common/get-list-subject/{majorId}");
            var body = await response.Content.ReadAsStringAsync();
            var listSubject = JsonConvert.DeserializeObject<List<Subject>>(body);
            if(listSubject is null)
            {
                return new List<GetListSubjectResponse>();
            }
            var result = listSubject
                    .Select(x => new GetListSubjectResponse()
                    {
                        SubjectId = x.SubjectId,
                        Name = x.Name
                    }).ToList();
            return result;
        }

        public async Task<ApiResult<PagedResult<TestStructureItem>>> GetListTestStructure(GetListTestStructureRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseApiAddress"]);
            var response = await client.GetAsync($"/test-structure?" +
                $"SubjectId={request.SubjectId}" +
                $"&Page={request.Page}" +
                $"&PageSize={request.PageSize}");
            var body = await response.Content.ReadAsStringAsync();
            var listStructure = JsonConvert.DeserializeObject<ApiResult<PagedResult<TestStructureItem>>>(body);
            return listStructure;
        }
    }
}
