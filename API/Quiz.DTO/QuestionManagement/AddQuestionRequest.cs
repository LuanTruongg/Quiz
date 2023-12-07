using Microsoft.AspNetCore.Http;

namespace Quiz.DTO.QuestionManagement
{
	public class AddQuestionRequest
	{
		public string QuestionText { get; init; }
        public string QuestionA { get; set; }
		public string QuestionB { get; set; }
		public string QuestionC { get; set; }
		public string QuestionD { get; set; }
		public char Answer { get; set; }
		public string Difficult { get; set; }
		public string ModuleId { get; set; }
        public string Image { get; set; }
        public string Audio { get; set; }
    }
}
