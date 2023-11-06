using Quiz.DTO.BaseResponse;

namespace Quiz.DTO.QuestionManagement
{
	public class GetListQuestionResponse : PagingListResponse<QuestionItem>
	{
		
	}
	public class QuestionItem
	{
        public string QuestionId { get; init; }
        public string QuestionText { get; init; }
		public string QuestionA { get; set; }
		public string QuestionB { get; set; }
		public string QuestionC { get; set; }
		public string QuestionD { get; set; }
        public string Answer { get; set; }
        public string Difficult { get; set; }
        public string SubjectName { get; set; }
	}
}
