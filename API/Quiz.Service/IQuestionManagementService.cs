﻿using Quiz.DTO.QuestionManagement;

namespace Quiz.Service
{
	public interface IQuestionManagementService
	{
		Task<AddQuestionResponse> AddQuestionAsync(AddQuestionRequest request);
	}
}
