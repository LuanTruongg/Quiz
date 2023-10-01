using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.DTO.QuestionManagement
{
	public class EditQuestionRequest
	{
		public string QuestionText { get; init; }
		public string QuestionA { get; set; }
		public string QuestionB { get; set; }
		public string QuestionC { get; set; }
		public string QuestionD { get; set; }
		public char Answer { get; set; }
		public string Difficult { get; set; }
		public string ModuleId { get; set; }
	}
}
