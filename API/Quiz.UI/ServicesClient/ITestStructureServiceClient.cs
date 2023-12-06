using Quiz.DTO.BaseResponse;
using Quiz.DTO.Common;
using Quiz.DTO.TestStructureManagement;
using Quiz.Repository.Model;

namespace Quiz.UI.ServicesClient
{
    public interface ITestStructureServiceClient
    {
        Task<List<GetListMajorResponse>> GetListMajors(string deparmentId);
        Task<GetListDepartmentResponse> GetNameDepartment(string id);
        Task<string> GetNameMajor(string majorId, string departmentId);
        Task<List<GetListSubjectResponse>> GetListSubject(string majorId);
        Task<ApiResult<PagedResult<TestStructureItem>>> GetListTestStructure(GetListTestStructureRequest request);
        Task<CreateTestStructureResponse> CreateTestStructure(CreateTestStructureRequest request);
        Task<ApiResult<TestStructureItem>> GetTestStructureById(string id);
        Task<ApiResult<Major>> GetMajor(string majorId);
    }
}
