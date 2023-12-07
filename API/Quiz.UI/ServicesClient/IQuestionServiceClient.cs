using Quiz.DTO.BaseResponse;
using Quiz.DTO.QuestionManagement;

namespace Quiz.UI.ServicesClient
{
    public interface IQuestionServiceClient
    {
        Task<ApiResult<PagedResult<QuestionItem>>> GetListQuestionOfSubject(GetListQuestionRequest request);
        Task<ApiResult<bool>> CreateQuestionOfModule(CreateQuestionRequestViewModel request, string imgName, string audioName);
        Task<ApiResult<bool>> EditQuestion(string id, EditQuestionRequest request);
        Task<ApiResult<GetQuestionResponse>> GetQuestionById(string id);
        Task<ApiResult<bool>> DeleteQuestion(string id);
    }
}
