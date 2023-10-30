using Quiz.DTO.TestSubjectManagement;
using Quiz.DTO.UserAnswerManagement;
using Quiz.DTO.UserTestManagement;

namespace Quiz.UI.ServicesClient
{
    public interface ITestSubjectServiceClient
    {
        Task<List<GetListQuestionOfTestResponse>> GetListQuestionOfTest(string testSubjectCode);
        Task<string> GetTestSubjectCode(string testStructureId);
        Task<AddUserAnswerResponse> SaveUserAnswer (List<UserAnswerRequest> request, string userTestId);
        Task<AddUserTestResponse> SaveUserTest (string testStructureId);
        Task<GetResultUserTestResponse> GetResultUserTest(string userTestId);
    }
}
