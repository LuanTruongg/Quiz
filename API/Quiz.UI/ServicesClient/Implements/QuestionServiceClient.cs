using Quiz.DTO.BaseResponse;
using Quiz.DTO.QuestionManagement;

namespace Quiz.UI.ServicesClient.Implements
{
    public class QuestionServiceClient : IQuestionServiceClient
    {
        public Task<ApiResult<PagedResult<QuestionItem>>> GetListQuestionOfTestSubject()
        {
            throw new NotImplementedException();
        }
    }
}
