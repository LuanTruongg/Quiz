using Quiz.Infrastructure.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.DTO.TestSubjectManagement
{
    public class GetListQuestionOfTestResponse
    {
        public string QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string AnswerA { get; set; }
        public string AnswerB { get; set; }
        public string AnswerC { get; set; }
        public string AnswerD { get; set; }
        public string Image { get; set; }
        public string Audio { get; set; }
        public string? QuestionCustom { get; set; }
    }
}
