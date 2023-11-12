﻿using Quiz.DTO.BaseResponse;
using Quiz.DTO.QuestionManagement;

namespace Quiz.Service
{
	public interface IQuestionManagementService
	{
		Task<ApiResult<bool>> AddQuestionAsync(AddQuestionRequest request);
		Task<ApiResult<PagedResult<QuestionItem>>> GetListQuestionAsync(GetListQuestionRequest request);
		Task<string> DeleteQuestionAsync(string id);
		Task<ApiResult<bool>> EditQuestionAsync(string id, EditQuestionRequest request);
        Task<ApiResult<GetQuestionResponse>> GetQuestionByIdAsync(string id);
    }
}
