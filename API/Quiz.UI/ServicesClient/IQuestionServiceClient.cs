using Quiz.DTO.BaseResponse;
using Quiz.DTO.QuestionManagement;

namespace Quiz.UI.ServicesClient
{
    public interface IQuestionServiceClient
    {
        Task<ApiResult<PagedResult<QuestionItem>>> GetListQuestionOfTestSubject();
    }
}
