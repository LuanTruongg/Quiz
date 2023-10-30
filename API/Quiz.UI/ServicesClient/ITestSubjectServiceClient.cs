using Quiz.DTO.TestSubjectManagement;
using Quiz.DTO.UserTestManagement;

namespace Quiz.UI.ServicesClient
{
    public interface ITestSubjectServiceClient
    {
        Task<List<GetListQuestionOfTestResponse>> GetListQuestionOfTest(string testSubjectCode);
        Task<string> GetTestSubjectCode(string testStructureId);
        Task SaveUserAnswer (List<UserAnswerRequest> request, string userTestId);
        Task<AddUserTestResponse> SaveUserTest (string testStructureId);
    }
}
