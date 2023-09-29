using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Repository.Model
{
    public class Question
    {
        public string QuestionId { get; set; }
        public string ModuleId { get; set; }
        public Module Module { get; set; }
        public string QuestionText { get; set; }
        public string QuestionA { get; set; }
        public string QuestionB { get; set; }
        public string QuestionC { get; set; }
        public string QuestionD { get; set; }
        public string? QuestionCustom { get; set; }
        public string Answer { get; set; }
        public string Difficult { get; set; }
		public List<TestSubject> TestSubjects { get; set; }
    }
}
