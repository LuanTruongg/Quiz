using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.DTO.UserTestManagement
{
    public class GetUserTestResponse
    {
        public string UserTestId { get; set; }
        public string UserId { get; set; }
        public int CorrectAnswers { get; set; }
        public decimal Score { get; set; }
        public string TestStructureId { get; set; }
        public string TestStructureName{ get; set; }
        public int NumberOfQuestions { get; set; }
        public int Time { get; set; }
    }
}
