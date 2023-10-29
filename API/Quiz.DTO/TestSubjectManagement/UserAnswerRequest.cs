using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.DTO.TestSubjectManagement
{
    public class UserAnswerRequest
    {
        public string UserAnswerId { get; set; }
        public string QuestionId { get; set; }
        public string UserAnswerQuestion { get; set; }
        public string UserTestId { get; set; }
    }
}
