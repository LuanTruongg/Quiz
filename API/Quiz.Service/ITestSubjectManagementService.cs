using Quiz.DTO.TestSubjectManagement;

namespace Quiz.Service
{
	public interface ITestSubjectManagementService
	{
		Task<CreateTestSubjectResponse> CreateTestSubjectAsync(CreateTestSubjectRequest request);
        Task<string> GetListTestSubjectOfSubject(GetTestSubjectOfSubjectRequest request);
        Task<DeleteTestSubjectResponse> DeleteTestSubject(string testSubjectCode);
	}
}
