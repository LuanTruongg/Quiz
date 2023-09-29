﻿namespace Quiz.DTO.QuestionManagement
{
	public class GetQuestionListRequest
	{
		public string? SubjectId { get; set; }
		public int Page { get; set; }
		public int PageSize { get; set; }
		public bool? IsAscSorting { get; set; }
		public string? Search { get; set; }
	}
}
