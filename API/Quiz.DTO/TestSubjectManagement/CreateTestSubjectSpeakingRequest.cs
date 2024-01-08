using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.DTO.TestSubjectManagement
{
    public class CreateTestSubjectSpeakingRequest
    {
        public string TestSubjectCode { get; set; }
        public string TestStructureId { get; set; }
        public string QuestionId { get; set; }
        public string ModuleId { get; set; }
    }
}
