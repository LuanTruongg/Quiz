using Quiz.DTO.TestSubjectManagement;

namespace Quiz.Service
{
	public interface ITestSubjectManagementService
	{
		Task<CreateTestSubjectResponse> CreateTestSubjectAsync(CreateTestSubjectRequest request);
        Task<DeleteTestSubjectResponse> DeleteTestSubject(string testSubjectCode);
        Task<List<GetListQuestionOfTestResponse>> GetListQuestionOfTestAsync(GetListQuestionOfTestRequest request);
    }
}
