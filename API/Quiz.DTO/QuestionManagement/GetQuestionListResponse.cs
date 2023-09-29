using Quiz.DTO.BaseResponse;

namespace Quiz.DTO.QuestionManagement
{
	public class GetQuestionListResponse : PagingListResponse<QuestionItem>
	{
		
	}
	public class QuestionItem
	{
		public string QuestionText { get; init; }
		public string QuestionA { get; set; }
		public string QuestionB { get; set; }
		public string QuestionC { get; set; }
		public string QuestionD { get; set; }
		public string SubjectName { get; set; }
	}
}
