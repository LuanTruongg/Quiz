using Quiz.DTO.TestSubjectManagement;

namespace Quiz.Service
{
	public interface ITestSubjectManagementService
	{
		Task<CreateTestSubjectResponse> CreateTestSubjectAsync(CreateTestSubjectRequest request);
	}
}
