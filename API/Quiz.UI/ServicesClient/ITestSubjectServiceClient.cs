using Quiz.DTO.TestSubjectManagement;

namespace Quiz.UI.ServicesClient
{
    public interface ITestSubjectServiceClient
    {
        Task<List<GetListQuestionOfTestResponse>> GetListQuestionOfTest(string testSubjectCode);
        Task<string> GetTestSubjectCode(string testStructureId);
    }
}
