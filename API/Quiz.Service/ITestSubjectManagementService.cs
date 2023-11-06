using Quiz.DTO.BaseResponse;
using Quiz.DTO.TestSubjectManagement;

namespace Quiz.Service
{
	public interface ITestSubjectManagementService
	{
		Task<ApiResult<CreateTestSubjectResponse>> CreateTestSubjectAsync(CreateTestSubjectRequest request);
        Task<DeleteTestSubjectResponse> DeleteTestSubject(string testSubjectCode);
        Task<List<GetListQuestionOfTestResponse>> GetListQuestionOfTestAsync(GetListQuestionOfTestRequest request);
    }
}
