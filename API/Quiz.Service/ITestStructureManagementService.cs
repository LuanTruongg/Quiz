using Quiz.DTO.BaseResponse;
using Quiz.DTO.TestStructureManagement;
using Quiz.DTO.TestSubjectManagement;

namespace Quiz.Service
{
	public interface ITestStructureManagementService
	{
        Task<ApiResult<TestStructureItem>> GetTestStructureByIdAsync(string id);
        Task<ApiResult<PagedResult<TestStructureItem>>> GetListTestStructureAsync(GetListTestStructureRequest request);
        Task<CreateTestStructureResponse> AddTestStructureAsync(CreateTestStructureRequest request);
        Task<ApiResult<bool>> DeleteTestStructureAsync(string id);
    }
}
