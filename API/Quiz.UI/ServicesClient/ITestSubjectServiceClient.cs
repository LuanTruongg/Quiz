using Quiz.DTO.BaseResponse;
using Quiz.DTO.ModuleManagement;
using Quiz.DTO.TestSubjectManagement;
using Quiz.DTO.UserAnswerManagement;
using Quiz.DTO.UserTestManagement;

namespace Quiz.UI.ServicesClient
{
    public interface ITestSubjectServiceClient
    {
        Task<List<GetListQuestionOfTestResponse>> GetListQuestionOfTest(string testStructureId);
        Task<string> GetTestSubjectCode(string testStructureId);
        Task<AddUserAnswerResponse> SaveUserAnswer (List<UserAnswerRequest> request, string userTestId);
        Task<AddUserTestResponse> SaveUserTest (string testStructureId);
        Task<GetResultUserTestResponse> GetResultUserTest(string userTestId);
        Task<ApiResult<CreateTestSubjectResponse>> CreateTestSubject(CreateTestSubjectRequest request);
        Task<ApiResult<bool>> DeleteTestSubject(string id);
        Task<ApiResult<bool>> DeleteTestStructure(string id);
        Task<ApiResult<bool>> CreateSpeakingTestSubject(CreateTestSubjectSpeakingRequest request);
    }
}
