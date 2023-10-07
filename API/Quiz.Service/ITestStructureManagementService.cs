using Quiz.DTO.TestStructureManagement;

namespace Quiz.Service
{
	public interface ITestStructureManagementService
	{
		Task<CreateTestStructureResponse> AddTestStructureAsync(CreateTestStructureRequest request);
	}
}
