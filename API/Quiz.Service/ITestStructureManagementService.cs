using Quiz.DTO.TestStructureManagement;
using Quiz.DTO.TestSubjectManagement;

namespace Quiz.Service
{
	public interface ITestStructureManagementService
	{
        Task<GetListTestStructureResponse> GetListTestStructureAsync(GetListTestStructureRequest request);
        Task<CreateTestStructureResponse> AddTestStructureAsync(CreateTestStructureRequest request);
    }
}
