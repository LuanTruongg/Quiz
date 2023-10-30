using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.DTO.UserAnswerManagement
{
    public class AddUserAnswerRequest
    {
        public string QuestionId { get; set; }
        public string UserAnswerQuestion { get; set; }
        public string UserTestId { get; set; }
    }
}
