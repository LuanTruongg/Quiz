using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.DTO.UserTestManagement
{
    public class AddUserTestRequest
    {
        public string UserId { get; set; }
        public string TestStructureId { get; set; }
        public string UserTestId { get; set; }
        public int CorrectAnswers { get; set; }
        public decimal Score { get; set; }
    }
}
