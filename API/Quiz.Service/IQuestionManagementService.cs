using Quiz.DTO.QuestionManagement;

namespace Quiz.Service
{
	public interface IQuestionManagementService
	{
		Task<AddQuestionResponse> AddQuestionAsync(AddQuestionRequest request);
		Task<GetQuestionListResponse> GetListQuestionAsync(GetQuestionListRequest request);
		Task<string> DeleteQuestionAsync(string id);
		Task<EditQuestionResponse> EditQuestionAsync(string id, EditQuestionRequest request);
	}
}
